// JavaScript Document
var USER_ONLINE_STATUS=1;						// 用户上线
var USER_OFFLINE_STATUS=0;						// 用户下线

var USERINFO_NAME=1;							// 用户昵称信息
var USERINFO_IP=2;								// 用户IP地址信息

//会话提示信息
function ForSession(message,statue) {
    $("#Initiative_Call_Div").hide();		// 隐藏正在呼叫层
    $("#SessionPrompt_Div").show();
    $("#SessionPrompt_Div").html("<iframe frameborder=0 scrolling=no style='background-color:transparent; z-index: 9999;width: 100%;height: 100%;top:0;left: 0;position: absolute; '></iframe><b>"+message+"</b>");
    if(statue)$('#SessionPrompt_Div').animate({opacity: "hide"}, 1500); 
}

//取消主动呼叫
function CancelCall() {
	console.warn('取消主动呼叫');
	$('#LOADING_GREY_DIV').hide();
	/**取消主动呼叫*/
	BRAC_VideoCallControl(BRAC_VIDEOCALL_EVENT_REPLY,mTargetUserId,GV_ERR_SESSION_QUIT,0,0,"");
	ForSession("取消呼叫...",true);
	if(userType==1){
		/**离开队列*/
		BRAC_ObjectControl(ANYCHAT_OBJECT_TYPE_QUEUE, queueid, ANYCHAT_QUEUE_CTRL_USERLEAVE,0 ,0,0,0,"");
		$('#callLayer').hide();//隐藏排队信息窗口
		$("#queueMsg2").hide();
		$("#queueMsg1").show();
		$("#enterRoom").show();//显示队列列表
	}else if(userType==2){
		/**客服结束服务*/
		BRAC_ObjectControl(ANYCHAT_OBJECT_TYPE_AGENT, mSelfUserId, ANYCHAT_AGENT_CTRL_FINISHSERVICE, 0,0,0,0,"");	
		$("#Initiative_Call_Div").hide();
	}
}

//呼叫用户
function VideoCallRequest(ID) {
	console.warn('呼叫用户');
    /**向指定的用户发送会话邀请*/
	BRAC_VideoCallControl(BRAC_VIDEOCALL_EVENT_REQUEST,mTargetUserId,0,0,0,"");  
}

//同意会话
function AcceptRequestBtnClick() {
	console.warn('同意会话');
	/**呼叫请求回复触发*/
	BRAC_VideoCallControl(BRAC_VIDEOCALL_EVENT_REPLY,mTargetUserId,0,0,0,"");
}

//拒绝会话
function RejectRequestBtnClick() {
	console.warn('拒绝会话');
	/**目标用户拒绝会话触发*/
	BRAC_VideoCallControl(BRAC_VIDEOCALL_EVENT_REPLY,mTargetUserId,GV_ERR_SESSION_REFUSE,0,0,"");  
	/**离开队列*/
	BRAC_ObjectControl(ANYCHAT_OBJECT_TYPE_QUEUE, queueid, ANYCHAT_QUEUE_CTRL_USERLEAVE,0 ,0,0,0,"");
	$("#enterRoom").show();//显示队列列表
    ForSession("拒绝对方请求...",true);
}
//收到视频呼叫请求
function onVideoCallControlRequest(dwUserId, dwErrorCode, dwFlags, dwParam, szUserStr)
{
	console.warn('收到视频呼叫请求');
	/**获取客服姓名*/
	 var UserName = BRAC_GetUserInfo(dwUserId,USERINFO_NAME); 
	 
	 $("#queueMsg1").hide();
	 $("#queueMsg2").show();
	 mTargetUserId=dwUserId;
	 $("#queueMsg2 p:first-child").html("客服 <b style=\"color:red;\">"+UserName+"</b> 请求与您视频通话，是否接受？");
}

//视频呼叫请求回复
function onVideoCallControlReply(dwUserId, dwErrorCode, dwFlags, dwParam, szUserStr)
{
	console.warn('视频呼叫请求回复');
	switch(dwErrorCode)
	{
		case GV_ERR_SUCCESS://成功的情况
		    onSendVideoCallRequestSucess(dwUserId);
			break;
		case GV_ERR_SESSION_QUIT:
			/**离开队列*/
			BRAC_ObjectControl(ANYCHAT_OBJECT_TYPE_QUEUE, queueid, ANYCHAT_QUEUE_CTRL_USERLEAVE,0 ,0,0,0,"");
			$('#callLayer').hide();
			$('#enterRoom').show();
			clearInterval(waitTimeSet);
			ForSession("用户主动放弃会话", true);
			startServiceTag = false;
			break;
		case GV_ERR_SESSION_OFFLINE:
			$("#Initiative_Call_Div").hide();
		    ForSession("目标用户不在线",true);
			break;
		case GV_ERR_SESSION_BUSY:
			$("#Initiative_Call_Div").hide();
			/*CancelCall();*/
			ForSession("目标用户忙", true);
			startServiceTag = false;
			break; 
		case GV_ERR_SESSION_REFUSE:
			$('#LOADING_GREY_DIV').hide();
			ForSession("目标用户拒绝会话", true);
			startServiceTag = false;
			break; 
		case GV_ERR_SESSION_TIMEOUT:
			$("#Initiative_Call_Div").hide();
			CancelCall();
		 	ForSession("会话请求超时",true);
			break; 
		case GV_ERR_SESSION_DISCONNECT:
			ForSession("网络断线",true);
			break; 
		default:
			break;
	}
}

//通话开始
function onVideoCallControlStart(dwUserId, dwErrorCode, dwFlags, dwParam, szUserStr)
{
	console.warn('通话开始');
	if(userType==2){//客服
		$("#Initiative_Call_Div").hide();//隐藏主动呼叫对话框
		$('#LOADING_GREY_DIV').hide();
	}
	
	 /**请求进入指定的房间*/
	BRAC_EnterRoom(dwParam, "", 0);
}

//视频通话结束
function onVideoCallControlFinish(dwUserId, dwErrorCode, dwFlags, dwParam, szUserStr)
{console.warn('视频通话结束');
	BRAC_LeaveRoom(mSelfUserId);
	
	//关闭对方视频
	startVideo(mTargetUserId, GetID("removeVideoPos"), "ANYCHAT_VIDEO_REMOTE",0);
	if(userType==1){
		/**离开队列*/
		BRAC_ObjectControl(ANYCHAT_OBJECT_TYPE_QUEUE, queueid, ANYCHAT_QUEUE_CTRL_USERLEAVE,0 ,0,0,0,"");
		$("#videoCall").hide();//隐藏视频窗口
		$("#enterRoom").show();//显示队列列表
		clearInterval(waitTimeSet);
		setMarTop($("#selectList").height());
	}else if(userType==2){
		/**客服结束服务*/
		BRAC_ObjectControl(ANYCHAT_OBJECT_TYPE_AGENT, mSelfUserId, ANYCHAT_AGENT_CTRL_FINISHSERVICE, 0,0,0,0,"");	
		$('#localAudioVolume').width(0);
		$('#remoteAudioVolume').width(0);
	}
	clearInterval(mRefreshVolumeTimer); // 清除实时音量显示计时器
	$("#removeVideoPos").text("");
	$("#localVideoPos").text("");
	ForSession("会话结束...", true); // 提示层

	//startServiceTag = false;
}

//视频呼叫请求发送成功
function onSendVideoCallRequestSucess(mTargetUserId)
{	
	console.warn('视频呼叫请求发送成功');
	this.mTargetUserId=mTargetUserId;
	if(userType==2){
		/**用户姓名*/
		var UserName = BRAC_GetUserInfo(mTargetUserId,USERINFO_NAME); 
	    GetID("Initiative_Call_Div_Content").innerHTML = "正在呼叫 <b style=\"red;\">" + UserName + "</b> 用户，等待对方响应<br /><img src='./img/others/LoadImg.gif'  style='width: 145px;height:30px;' />";
	    $("#Initiative_Call_Div").show();
	    $('#LOADING_GREY_DIV').show();
	    //startServiceTag=false;
	}
}