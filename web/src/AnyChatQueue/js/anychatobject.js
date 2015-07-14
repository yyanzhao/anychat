// AnyChat for Web SDK
// 不要对该文件进行任何修改，当升级SDK时，新版本将会直接覆盖旧版本

/********************************************
 *			业务对象常量定义				*
 *******************************************/

 
 
// 对象类型定义
var ANYCHAT_OBJECT_TYPE_AREA		=	4;		// 服务区域
var ANYCHAT_OBJECT_TYPE_QUEUE		=	5;		// 队列对象
var ANYCHAT_OBJECT_TYPE_AGENT		=	6;		// 客服对象
var ANYCHAT_OBJECT_TYPE_CLIENTUSER	=	8;		// 客户端用户对象

// 通用标识定义
var ANYCHAT_OBJECT_FLAGS_AGENT=0x00000002;		// 坐席用户

var ANYCHAT_INVALID_OBJECT_ID		=	-1;

// 坐席服务状态定义
var ANYCHAT_AGENT_STATUS_CLOSEED	=	0;		// 关闭，不对外提供服务
var ANYCHAT_AGENT_STATUS_WAITTING	=	1;		// 等待中，可随时接受用户服务
var ANYCHAT_AGENT_STATUS_WORKING	=	2;		// 工作中，正在为用户服务
var ANYCHAT_AGENT_STATUS_PAUSED		=	3;		// 暂停服务

/**
 *	对象属性定义
 */

// 对象公共信息类型定义
var ANYCHAT_OBJECT_INFO_FLAGS		=	7;		// 对象属性标志
var ANYCHAT_OBJECT_INFO_NAME		=	8;		// 对象名称
var ANYCHAT_OBJECT_INFO_PRIORITY	=	9;		// 对象优先级
var ANYCHAT_OBJECT_INFO_ATTRIBUTE	=	10;		// 对象业务属性
var  ANYCHAT_OBJECT_INFO_DESCRIPTION	=11;	///< 对象描述

// 服务区域信息类型定义
var ANYCHAT_AREA_INFO_AGENTCOUNT	=	401;	// 服务区域客服用户数
var ANYCHAT_AREA_INFO_GUESTCOUNT	=	402;	// 服务区域内访客的用户数（没有排入队列的用户）
var ANYCHAT_AREA_INFO_QUEUEUSERCOUNT=	403;	// 服务区域内排队的用户数
var ANYCHAT_AREA_INFO_QUEUECOUNT	=	404;	// 服务区域内队列的数量

// 队列状态信息类型定义
var ANYCHAT_QUEUE_INFO_MYSEQUENCENO	=	501;	// 自己在该队列中的序号
var ANYCHAT_QUEUE_INFO_BEFOREUSERNUM=	502;	// 排在自己前面的用户数
var ANYCHAT_QUEUE_INFO_MYENTERQUEUETIME=503;	// 进入队列的时间
var ANYCHAT_QUEUE_INFO_LENGTH		=	504;	// 队列长度（有多少人在排队），整型
var ANYCHAT_QUEUE_INFO_FIRSTUSERID	=	505;	// 队列第一个用户的ID，整型
var ANYCHAT_QUEUE_INFO_FIRSTUSERENTTIME=506;	// 第一个用户进入队列的时间，整型
var ANYCHAT_QUEUE_INFO_AGVWAITTIME	=	507;	// 平均等待时间，整型，单位：秒
var ANYCHAT_QUEUE_INFO_WAITTIMESECOND  =508;	// 自己在队列中的等待时间(单位:秒)

// 客服状态信息类型定义
var ANYCHAT_AGENT_INFO_SERVICESTATUS	=601;	// 服务状态，整型
var ANYCHAT_AGENT_INFO_SERVICEUSERID	=602;	// 当前服务的用户ID，整型
var ANYCHAT_AGENT_INFO_SERVICEBEGINTIME	=603;	// 当前服务的开始时间，整型
var ANYCHAT_AGENT_INFO_SERVICETOTALTIME	=604;	// 累计服务时间，整型，单位：秒
var ANYCHAT_AGENT_INFO_SERVICETOTALNUM	=605;	// 累计服务的用户数，整型




/**
 *	对象方法定义
 */

// 对象公共参数控制常量定义
var ANYCHAT_OBJECT_CTRL_CREATE		=	2;		// 创建一个对象
var ANYCHAT_OBJECT_CTRL_SYNCDATA	=	3;		// 同步对象数据给指定用户，dwObjectId=-1，表示同步该类型的所有对象
var ANYCHAT_OBJECT_CTRL_DEBUGOUTPUT	=	4;		// 对象调试信息输出

// 服务区域控制常量定义
var ANYCHAT_AREA_CTRL_USERENTER		=	401;	// 进入服务区域，dwParam1为userid，dwParam2为flags
var ANYCHAT_AREA_CTRL_USERLEAVE		=	402;	// 离开服务区域，dwParam1为userid，dwParam2为errorcode

// 队列参数控制常量定义
var ANYCHAT_QUEUE_CTRL_USERENTER	=	501;	// 进入队列
var ANYCHAT_QUEUE_CTRL_USERLEAVE	=	502;	// 离开队列

// 客服参数控制常量定义
var ANYCHAT_AGENT_CTRL_SERVICESTATUS=	601;	// 坐席服务状态控制（暂停服务、工作中、关闭）
var ANYCHAT_AGENT_CTRL_SERVICEREQUEST=	602;	// 服务请求
var ANYCHAT_AGENT_CTRL_ASKREPLY		=	603;	// 请求回复
var ANYCHAT_AGENT_CTRL_FINISHSERVICE=	604;	// 结束服务
var ANYCHAT_AGENT_CTRL_EVALUATION	=	605;	// 服务评价，wParam为客服userid，lParam为评分，lpStrValue为留言



/**
 *	对象异步事件定义
 */

// 对象公共事件常量定义
var ANYCHAT_OBJECT_EVENT_UPDATE		=	1;		// 对象数据更新
var ANYCHAT_OBJECT_EVENT_SYNCDATAFINISH=2;		// 对象数据同步结束	

// 服务区域事件常量定义
var ANYCHAT_AREA_EVENT_STATUSCHANGE	=	401;	// 服务区域状态变化
var ANYCHAT_AREA_EVENT_ENTERRESULT	=	402;	// 进入服务区域结果
var ANYCHAT_AREA_EVENT_USERENTER	=	403;	// 用户进入服务区域
var ANYCHAT_AREA_EVENT_USERLEAVE	=	404;	// 用户离开服务区域
var ANYCHAT_AREA_EVENT_LEAVERESULT	=	405;	// 离开服务区域结果


// 队列事件常量定义
var ANYCHAT_QUEUE_EVENT_STATUSCHANGE=	501;	// 队列状态变化
var ANYCHAT_QUEUE_EVENT_ENTERRESULT	=	502;	// 进入队列结果
var ANYCHAT_QUEUE_EVENT_USERENTER	=	503;	// 用户进入队列
var ANYCHAT_QUEUE_EVENT_USERLEAVE	=	504;	// 用户离开队列
var ANYCHAT_QUEUE_EVENT_LEAVERESULT	=	505;	// 离开队列结果


// 坐席事件常量定义
var ANYCHAT_AGENT_EVENT_STATUSCHANGE =	601;	// 坐席状态变化
var ANYCHAT_AGENT_EVENT_SERVICENOTIFY=	602;	// 坐席服务通知（哪个用户到哪个客服办理业务）
var ANYCHAT_AGENT_EVENT_WAITINGUSER  =	603;	// 暂时没有客户，请等待

