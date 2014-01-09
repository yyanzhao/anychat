using System;
using System.Text;
using System.Runtime.InteropServices;

namespace ANYCHATAPI
{
 /***********************************************************************************
    * ** AnyChat Platform Server SDK
    * *********************************************************************************/
    public class AnyChatServerSDK
    {
        public const string AnyChatServerSDKDll = "AnyChatServerSDK.dll";

        //#define BRAS_SERVERAPPMSG_CONNECTED		1		///< ����AnyChat�������ɹ�
		public const int BRAS_SERVERAPPMSG_CONNECTED 	= 1;
        //#define BRAS_SERVERAPPMSG_DISCONNECT		2		///< ��AnyChat�������Ͽ�����
		public const int BRAS_SERVERAPPMSG_DISCONNECT 	= 2;
		
		// ��������Ϣ���Ͷ��壨�ص��¼���BRAS_OnServerAppMessageEx_CallBack ������
		// #define BRAS_MESSAGE_CORESERVERCONN		10		///< ����ķ�������������Ϣ��wParamΪerrorcode
		public const int BRAS_MESSAGE_CORESERVERCONN	= 10;
		// #define BRAS_MESSAGE_RECORDSERVERCONN	11		///< ��¼���������������Ϣ��wParamΪerrorcode��lParamΪrecordserverid
		public const int BRAS_MESSAGE_RECORDSERVERCONN	= 11;
		// #define BRAS_MESSAGE_LOGINSERVERCONN		12		///< ���¼��������������Ϣ��wParamΪerrorcode��lParamΪloginserverid
		public const int BRAS_MESSAGE_LOGINSERVERCONN	= 12;
		// #define BRAS_MESSAGE_ROOMSERVERCONN		13		///< �뷿���������������Ϣ��wParamΪerrorcode��lParamΪroomserverid
		public const int BRAS_MESSAGE_ROOMSERVERCONN	= 13;
		// #define BRAS_MESSAGE_MEDIASERVERCONN		14		///< ����ý���������������Ϣ��wParamΪerrorcode��lParamΪmediaserverid
		public const int BRAS_MESSAGE_MEDIASERVERCONN	= 14;
		
		// ��Ƶ�����¼����Ͷ��壨API��BRAS_VideoCallControl ���������OnVideoCallEvent�ص�������
		//#define BRAS_VIDEOCALL_EVENT_REQUEST    1        ///< ��������
		public const int BRAS_VIDEOCALL_EVENT_REQUEST 	= 1;
		//#define BRAS_VIDEOCALL_EVENT_REPLY      2        ///< ��������ظ�
		public const int BRAS_VIDEOCALL_EVENT_REPLY 	= 2;
		//#define BRAS_VIDEOCALL_EVENT_START      3        ///< ��Ƶ���лỰ��ʼ�¼�
		public const int BRAS_VIDEOCALL_EVENT_START 	= 3;
		//#define BRAS_VIDEOCALL_EVENT_FINISH     4        ///< �Ҷϣ����������лỰ
		public const int BRAS_VIDEOCALL_EVENT_FINISH 	= 4;
		
        // �û���Ϣ�������Ͷ��壨API��BRAS_UserInfoControl ���������OnUserInfoControl�ص�������
        //#define BRAS_USERINFO_CTRLCODE_KICKOUT		1	///< ��ָ���û���ϵͳ���ߵ�
        public const int BRAS_USERINFO_CTRLCODE_KICKOUT =   1;
        //#define BRAS_USERINFO_CTRLCODE_SYNCDATA		2	///< ��ָ���û�������ͬ�����ͻ���
        public const int BRAS_USERINFO_CTRLCODE_SYNCDATA=	2;
        //#define BRAS_USERINFO_CTRLCODE_BLOCKIP		4	///< ��ֹIP��ַ���ӷ�������lpStrValueΪIP��ַ�ַ�����֧��ͨ�����*��
        public const int BRAS_USERINFO_CTRLCODE_BLOCKIP	=	4;
        //#define BRAS_USERINFO_CTRLCODE_UNBLOCKIP	    5	///< ����IP��ַ���ӷ�������lpStrValueΪIP��ַ�ַ�����֧��ͨ�����*��
        public const int BRAS_USERINFO_CTRLCODE_UNBLOCKIP=  5;
        //#define BRAS_USERINFO_CTRLCODE_ADDGROUP		20	///< �����û����飬wParamΪ����Id��lpStrValueΪ��������
        public const int BRAS_USERINFO_CTRLCODE_ADDGROUP=   20;
        //#define BRAS_USERINFO_CTRLCODE_DELGROUP		21	///< ɾ���û����飬wParamΪ����Id
        public const int BRAS_USERINFO_CTRLCODE_DELGROUP=   21;
        //#define BRAS_USERINFO_CTRLCODE_ADDFRIEND	    22	///< �����û����ѣ�wParamΪ����Id
        public const int BRAS_USERINFO_CTRLCODE_ADDFRIEND=  22;
        //#define BRAS_USERINFO_CTRLCODE_DELFRIEND	    23	///< ɾ���û����ѣ�wParamΪ����Id
        public const int BRAS_USERINFO_CTRLCODE_DELFRIEND=  23;
        //#define BRAS_USERINFO_CTRLCODE_SETGROUPRELATION	24	///< ���ú��������Ĺ�����ϵ��wParamΪ����Id��lParamΪ����Id����ʾ��������ĳ������
        public const int BRAS_USERINFO_CTRLCODE_SETGROUPRELATION=   24;
        //#define BRAS_USERINFO_CTRLCODE_APPDEFINE	    100	///< Ӧ�ò��Զ�����ʼָ��
        public const int BRAS_USERINFO_CTRLCODE_APPDEFINE = 100;


		// ��ʼ����־��API��BRAS_InitSDK ���������
		public const int BRAS_INITFLAGS_MULTITHREADS	= 1;	///< ���߳�ģʽ
		
		
        /**
		 *    �ص���������
		 */

        // ������Ӧ�ó�����Ϣ�ص���������
        //typedef void (CALLBACK* BRAS_OnServerAppMessage_CallBack)(DWORD dwMsg, LPVOID lpUserValue);
        public delegate void OnServerAppMessageCallBack(int msg, int userValue);
		
		// ������Ӧ�ó�����Ϣ����չ���ص���������
        //typedef void (CALLBACK* BRAS_OnServerAppMessageEx_CallBack)(DWORD dwNotifyMessage, DWORD wParam, DWORD lParam, LPVOID lpUserValue);
        public delegate void OnServerAppMessageExCallBack(int dwNotifyMessage, int wParam, int lParam, int userValue);

		// SDK��ʱ���ص��������壨�ϲ�Ӧ�ÿ����ڸûص��д�����ʱ���񣬶�����Ҫ���⿪���̣߳����Ƕ�ʱ����
		//typedef void (CALLBACK* BRAS_OnTimerEvent_CallBack)(LPVOID lpUserValue);
        public delegate void OnTimerEventCallBack(int userValue);

		// �û�������֤�ص���������
		// typedef DWORD (CALLBACK* BRAS_VerifyUser_CallBack)(IN LPCTSTR lpUserName,IN LPCTSTR lpPassword, OUT LPDWORD lpUserID, OUT LPDWORD lpUserLevel, OUT LPTSTR lpNickName,IN DWORD dwNCLen, LPVOID lpUserValue);
        public delegate int VerifyUserCallBack(string userName, string password, ref int userID, ref int userLevel, IntPtr nickName, int len,int userValue);

        // �û�������뷿��ص���������
        //typedef DWORD (CALLBACK* BRAS_PrepareEnterRoom_CallBack)(DWORD dwUserId, DWORD dwRoomId, LPCTSTR lpRoomName,LPCTSTR lpPassword, LPVOID lpUserValue);
        public delegate int PrepareEnterRoomCallBack(int userId, int roomId, string roomName, string password, int userValue);

        // �û���¼�ɹ��ص���������
        //typedef void (CALLBACK* BRAS_OnUserLoginAction_CallBack)(DWORD dwUserId, LPCTSTR szUserName, DWORD dwLevel, LPCTSTR szIpAddr, LPVOID lpUserValue);
        public delegate void OnUserLoginActionCallBack(int userId, string userName, int level, string addr, int userValue);

        // �û�ע���ص���������
        //typedef void (CALLBACK* BRAS_OnUserLogoutAction_CallBack)(DWORD dwUserId, LPVOID lpUserValue);
        public delegate void OnUserLogoutActionCallBack(int userId, int userValue);
		
		// �û�ע���ص��������壨��չ��
        //typedef void (CALLBACK* BRAS_OnUserLogoutActionEx_CallBack)(DWORD dwUserId, DWORD dwErrorCode, LPVOID lpUserValue);
        public delegate void OnUserLogoutActionExCallBack(int userId, int errorcode, int userValue);

        // �û����뷿��ص���������
        //typedef void (CALLBACK* BRAS_OnUserEnterRoomAction_CallBack)(DWORD dwUserId, DWORD dwRoomId, LPVOID lpUserValue);
        public delegate void OnUserEnterRoomActionCallBack(int userId, int roomId, int userValue);

        // �û��뿪����ص���������
        //typedef void (CALLBACK* BRAS_OnUserLeaveRoomAction_CallBack)(DWORD dwUserId, DWORD dwRoomId, LPVOID lpUserValue);
        public delegate void OnUserLeaveRoomActionCallBack(int userId, int roomId, int userValue);

        // �ϲ�ҵ���Զ������ݻص���������
        //typedef void (CALLBACK* BRAS_OnRecvUserFilterData_CallBack)(DWORD dwUserId, LPCTSTR lpBuf, DWORD dwLen, LPVOID lpUserValue);
        public delegate void OnRecvUserFilterDataCallBack(int userId, IntPtr buf, int len, int userValue);

        // �յ��û���������ͨ�����ݻص���������
        //typedef void (CALLBACK* BRAS_OnRecvUserTextMsg_CallBack)(DWORD dwRoomId, DWORD dwSrcUserId, DWORD dwTarUserId, BOOL bSecret, LPCTSTR lpTextMessage, DWORD dwLen, LPVOID lpUserValue);
        public delegate void OnRecvUserTextMsgCallBack(int roomId, int srcUserId, int tarUserId, bool bSecret, string textMessage, int len, int userValue);

		// ͸��ͨ�����ݻص���������
		//typedef void (CALLBACK * BRAS_OnTransBuffer_CallBack)(DWORD dwUserId, LPBYTE lpBuf, DWORD dwLen, LPVOID lpUserValue);
		public delegate void OnTransBufferCallBack(int dwUserId, IntPtr lpBuf, int dwLen, int lpUserValue);
		
		// ͸��ͨ��������չ�ص���������
		//typedef void (CALLBACK * BRAS_OnTransBufferEx_CallBack)(DWORD dwUserId, LPBYTE lpBuf, DWORD dwLen, DWORD wParam, DWORD lParam, DWORD dwTaskId, LPVOID lpUserValue);
		public delegate void OnTransBufferExCallBack(int dwUserId, IntPtr lpBuf, int dwLen, int wParam, int lParam, int dwTaskId, int lpUserValue);
		
		// �ļ�����ص���������
		//typedef void (CALLBACK * BRAS_OnTransFile_CallBack)(DWORD dwUserId, LPCTSTR lpFileName, LPCTSTR lpTempFilePath, DWORD dwFileLength, DWORD wParam, DWORD lParam, DWORD dwTaskId, LPVOID lpUserValue);
		public delegate void OnTransFileCallBack(int dwUserId, string lpFileName, string lpTempFilePath, int dwFileLength, int wParam, int lParam, int dwTaskId, int lpUserValue);

		// ������¼��ص���������
		//typedef void (CALLBACK * BRAS_OnServerRecord_CallBack)(DWORD dwUserId, DWORD dwParam, DWORD dwRecordServerId, DWORD dwElapse, LPCTSTR lpRecordFileName, LPVOID lpUserValue);
		public delegate void OnServerRecordCallBack(int dwUserId, int dwParam, int dwRecordServerId, int dwElapse, string lpRecordFileName, int lpUserValue);
		
		// ��Ƶͨ����Ϣ֪ͨ�ص���������
		//typedef DWORD (CALLBACK * BRAS_OnVideoCallEvent_CallBack)(DWORD dwEventType, DWORD dwSrcUserId, DWORD dwTarUserId, DWORD dwErrorCode, DWORD dwFlags, DWORD dwParam, LPCTSTR lpUserStr, LPVOID lpUserValue);
		public delegate int OnVideoCallEventCallBack(int dwEventType, int dwSrcUserId, int dwTarUserId, int dwErrorCode, int dwFlags, int dwParam, string lpUserStr, int lpUserValue);
		

		/**
		 *    API ��������
		 */

        // ���÷�����Ӧ�ó�����Ϣ�ص�����
        //BRAS_API DWORD BRAS_SetOnServerAppMessageCallBack(BRAS_OnServerAppMessage_CallBack lpFunction, LPVOID lpUserValue=NULL);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetOnServerAppMessageCallBack", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SetOnServerAppMessageCallBack(OnServerAppMessageCallBack function, int userValue);
		
		// ���÷�����Ӧ�ó�����Ϣ����չ���ص�����
        //BRAS_API DWORD BRAS_SetOnServerAppMessageExCallBack(BRAS_OnServerAppMessageEx_CallBack lpFunction, LPVOID lpUserValue=NULL);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetOnServerAppMessageExCallBack", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SetOnServerAppMessageExCallBack(OnServerAppMessageExCallBack function, int userValue);

        // ����SDK��ʱ���ص�������dwElapse����ʱ���������λ��ms��
        // BRAS_API DWORD BRAS_SetTimerEventCallBack(DWORD dwElapse, BRAS_OnTimerEvent_CallBack lpFunction, LPVOID lpUserValue=NULL);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetTimerEventCallBack", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SetTimerEventCallBack(int elapse, OnTimerEventCallBack function, int userValue);

        // �����û�������֤�ص�����
        //BRAS_API DWORD BRAS_SetVerifyUserCallBack(BRAS_VerifyUser_CallBack lpFunction, LPVOID lpUserValue=NULL);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetVerifyUserCallBack", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SetVerifyUserCallBack(VerifyUserCallBack function, int userValue);

        // �����û�������뷿��ص�����
        // BRAS_API DWORD BRAS_SetPrepareEnterRoomCallBack(BRAS_PrepareEnterRoom_CallBack lpFunction, LPVOID lpUserValue=NULL);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetPrepareEnterRoomCallBack", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SetPrepareEnterRoomCallBack(PrepareEnterRoomCallBack function, int userValue);

        // �����û���¼�ɹ��ص�����
        //BRAS_API DWORD BRAS_SetOnUserLoginActionCallBack(BRAS_OnUserLoginAction_CallBack lpFunction, LPVOID lpUserValue=NULL);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetOnUserLoginActionCallBack", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SetOnUserLoginActionCallBack(OnUserLoginActionCallBack function, int userValue);

        // �����û�ע���ص�����
        //BRAS_API DWORD BRAS_SetOnUserLogoutActionCallBack(BRAS_OnUserLogoutAction_CallBack lpFunction, LPVOID lpUserValue=NULL);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetOnUserLogoutActionCallBack", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SetOnUserLogoutActionCallBack(OnUserLogoutActionCallBack function, int userValue);
		
		// �����û�ע���ص���������չ��
        //BRAS_API DWORD BRAS_SetOnUserLogoutActionExCallBack(BRAS_OnUserLogoutActionEx_CallBack lpFunction, LPVOID lpUserValue=NULL);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetOnUserLogoutActionExCallBack", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SetOnUserLogoutActionExCallBack(OnUserLogoutActionExCallBack function, int userValue);

        // �����û����뷿��ص�����
        //BRAS_API DWORD BRAS_SetOnUserEnterRoomActionCallBack(BRAS_OnUserEnterRoomAction_CallBack lpFunction, LPVOID lpUserValue=NULL);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetOnUserEnterRoomActionCallBack", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SetOnUserEnterRoomActionCallBack(OnUserEnterRoomActionCallBack function, int userValue);

        // �����û��뿪����ص�����
        //BRAS_API DWORD BRAS_SetOnUserLeaveRoomActionCallBack(BRAS_OnUserLeaveRoomAction_CallBack lpFunction, LPVOID lpUserValue=NULL);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetOnUserLeaveRoomActionCallBack", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SetOnUserLeaveRoomActionCallBack(OnUserLeaveRoomActionCallBack function, int userValue);

        // �����û��ϲ�ҵ���Զ������ݻص�����
        // BRAS_API DWORD BRAS_SetOnRecvUserFilterDataCallBack(BRAS_OnRecvUserFilterData_CallBack lpFunction, LPVOID lpUserValue=NULL);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetOnRecvUserFilterDataCallBack", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SetOnRecvUserFilterDataCallBack(OnRecvUserFilterDataCallBack function, int userValue);

        // �����û���������ͨ�����ݻص�����
        //BRAS_API DWORD BRAS_SetOnRecvUserTextMsgCallBack(BRAS_OnRecvUserTextMsg_CallBack lpFunction, LPVOID lpUserValue=NULL);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetOnRecvUserTextMsgCallBack", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SetOnRecvUserTextMsgCallBack(OnRecvUserTextMsgCallBack function, int userValue);
		
		
		// ����͸��ͨ�����ݻص�����
		//BRAS_API DWORD BRAS_SetOnTransBufferCallBack(BRAS_OnTransBuffer_CallBack lpFunction, LPVOID lpUserValue=NULL);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetOnTransBufferCallBack", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BRAS_SetOnTransBufferCallBack(OnTransBufferCallBack function, int lpUserValue);
		
		// ����͸��ͨ��������չ�ص�����
		//BRAS_API DWORD BRAS_SetOnTransBufferExCallBack(BRAS_OnTransBufferEx_CallBack lpFunction, LPVOID lpUserValue=NULL);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetOnTransBufferExCallBack", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BRAS_SetOnTransBufferExCallBack(OnTransBufferExCallBack function, int lpUserValue);
		
		// �����ļ�����ص�����
		//BRAS_API DWORD BRAS_SetOnTransFileCallBack(BRAS_OnTransFile_CallBack lpFunction, LPVOID lpUserValue=NULL);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetOnTransFileCallBack", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BRAS_SetOnTransFileCallBack(OnTransFileCallBack function, int lpUserValue);
		
		// ���÷�����¼��֪ͨ�ص�����
		//BRAS_API DWORD BRAS_SetOnServerRecordCallBack(BRAS_OnServerRecord_CallBack lpFunction, LPVOID lpUserValue=NULL);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetOnServerRecordCallBack", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BRAS_SetOnServerRecordCallBack(OnServerRecordCallBack function, int lpUserValue);

		// ������Ƶͨ����Ϣ֪ͨ�ص�����
		//BRAS_API DWORD BRAS_SetOnVideoCallEventCallBack(BRAS_OnVideoCallEvent_CallBack lpFunction, LPVOID lpUserValue=NULL);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetOnVideoCallEventCallBack", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BRAS_SetOnVideoCallEventCallBack(OnVideoCallEventCallBack function, int lpUserValue);
		
		
		// ��ȡSDK�汾��Ϣ
		//BRAS_API DWORD BRAS_GetSDKVersion(DWORD& dwMainVer, DWORD& dwSubVer, TCHAR* lpCompileTime, DWORD dwBufLen);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_GetSDKVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_GetSDKVersion(ref int dwMainVer, ref int dwSubVer, StringBuilder lpCompileTime, int bufLen);
		
        // ��ʼ��SDK
        //BRAS_API DWORD BRAS_InitSDK(DWORD dwReserved);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_InitSDK", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_InitSDK(int reserved);

        // �ͷ���Դ
        //BRAS_API DWORD BRAS_Release(void);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_Release", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_Release();

        // ��ָ���û���������
        //BRAS_API DWORD BRAS_SendBufToUser(DWORD dwUserId, LPCTSTR lpBuf, DWORD dwLen);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SendBufToUser", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SendBufToUser(int userId, byte[] buf, int len);

        // ��ָ������������û���������
        //BRAS_API DWORD BRAS_SendBufToRoom(DWORD dwRoomId, LPCTSTR lpBuf, DWORD dwLen);
        [DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SendBufToRoom", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SendBufToRoom(int dwRoomId, byte[] lpBuf, int dwLen);
		
		// ��ָ���û�����͸��ͨ������
		//BRAS_API DWORD BRAS_TransBuffer(DWORD dwUserId, LPBYTE lpBuf, DWORD dwLen);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_TransBuffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BRAS_TransBuffer(int dwUserId, byte[] lpBuf, int dwLen);
		
		// ��ָ���û�������չ����������
		//BRAS_API DWORD BRAS_TransBufferEx(DWORD dwUserId, LPBYTE lpBuf, DWORD dwLen, DWORD wParam, DWORD lParam, DWORD dwFlags, DWORD& dwTaskId);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_TransBufferEx", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BRAS_TransBufferEx(int dwUserId, byte[] lpBuf, int dwLen, int wParam, int lParam, int dwFlags, ref int dwTaskId);
		
		// ��ָ���û������ļ�
		//BRAS_API DWORD BRAS_TransFile(DWORD dwUserId, LPCTSTR lpLocalPathName, DWORD wParam, DWORD lParam, DWORD dwFlags, DWORD& dwTaskId);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_TransFile", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BRAS_TransFile(int dwUserId, string lpLocalPathName, int wParam, int lParam, int dwFlags, ref int dwTaskId);
		
		// ���Ķ�¼�����
		//BRAS_API DWORD BRAS_StreamRecordCtrl(DWORD dwUserId, BOOL bStartRecord, DWORD dwFlags, DWORD dwParam, DWORD dwRecordServerId);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_StreamRecordCtrl", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BRAS_StreamRecordCtrl(int dwUserId, int bStartRecord, int dwFlags, int dwParam, int dwRecordServerId);
		
		// ����͸��ͨ�����ݸ�¼�������
		//BRAS_API DWORD BRAS_TransBuffer2RecordServer(DWORD dwUserId, LPBYTE lpBuf, DWORD dwLen, DWORD dwParam, DWORD dwRecordServerId);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_TransBuffer2RecordServer", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BRAS_TransBuffer2RecordServer(int dwUserId, byte[] lpBuf, int dwLen, int dwParam, int dwRecordServerId);
		
		// ��Ƶ�����¼����ƣ����󡢻ظ����Ҷϵȣ�
		//BRAS_API DWORD BRAS_VideoCallControl(DWORD dwEventType, DWORD dwUserId, DWORD dwErrorCode, DWORD dwFlags=0, DWORD dwParam=0, LPCTSTR lpUserStr=NULL);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_VideoCallControl", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BRAS_VideoCallControl(int dwEventType, int dwUserId, int dwErrorCode, int dwFlags, int dwParam, string lpUserStr);
		

        // �����û�����ϸ��Ϣ
		//BRAS_API DWORD BRAS_SetUserInfo(DWORD dwUserId, DWORD dwInfoId, LPCTSTR lpInfoValue, DWORD dwFlags=0);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetUserInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SetUserInfo(int dwUserId, int dwInfoId, string lpInfoValue, int dwFlags);

        // ��ȡ�û�����ϸ��Ϣ
		//BRAS_API DWORD BRAS_GetUserInfo(DWORD dwUserId, DWORD dwInfoId, TCHAR* lpInfoValue, DWORD dwSize);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_GetUserInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_GetUserInfo(int dwUserId, int dwInfoId, StringBuilder lpInfoValue, int dwSize);

        // �û���Ϣ����
		//BRAS_API DWORD BRAS_UserInfoControl(DWORD dwUserId, DWORD dwCtrlCode, DWORD wParam=0, DWORD lParam=0, LPCTSTR lpStrValue=NULL);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_UserInfoControl", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_UserInfoControl(int dwUserId, int dwCtrlCode, int wParam, int lParam, string lpStrValue);

        // SDK�ں˲�������
		//BRAS_API DWORD BRAS_SetSDKOption(DWORD optname, CHAR* optval, DWORD optlen);
		[DllImport(AnyChatServerSDKDll, EntryPoint = "BRAS_SetSDKOption", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BRAS_SetSDKOption(int optname, StringBuilder optval, int optlen);

    }
}