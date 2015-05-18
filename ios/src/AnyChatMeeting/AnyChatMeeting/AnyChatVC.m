//
//  VideoVC.m
//  AnyChatMeetings
//
//  Created by alexChen .
//  Copyright (c) 2015年 GuangZhou BaiRui NetWork Technology Co.,Ltd. All rights reserved.
//

#import "AnyChatVC.h"

@interface AnyChatVC ()

@end

@implementation AnyChatVC

@synthesize anyChat;
@synthesize videoVC;
@synthesize theOnLineLoginState;
@synthesize theVersionLab;
@synthesize theStateInfo;
@synthesize theUserName;
@synthesize theServerIP;
@synthesize theServerPort;
@synthesize theRoomNO;
@synthesize theLoginBtn;
@synthesize theHideKeyboardBtn;
@synthesize theMyUserName;
@synthesize theMyUserID;
@synthesize onlineUserMArray;
@synthesize theOnChatUserIDStrMArray;


- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self){
    }
    return self;
}

#pragma mark - Life cycle

- (void)viewDidLoad
{
    [super viewDidLoad];
    
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(AnyChatNotifyHandler:) name:@"ANYCHATNOTIFY" object:nil];
    
    //关闭AnyChatSDK 日志(在 SDK 初始化之前调用)
    [AnyChatPlatform ActiveCallLog:NO];
    
    [AnyChatPlatform InitSDK:0];
    
    anyChat = [[AnyChatPlatform alloc] init];
    anyChat.notifyMsgDelegate = self;
    
    
    //创建默认视频参数
    [[SettingVC sharedSettingVC] createObjPlistFileToDocumentsPath];
    
    self.onlineUserMArray = [NSMutableArray arrayWithCapacity:5];
    self.theOnChatUserIDStrMArray = [[NSMutableArray alloc] initWithObjects:@"0",@"0",@"0", nil];
}

- (void)viewWillAppear:(BOOL)animated
{
    [super viewWillAppear:YES];
    
    [self setUI];
}

#pragma mark - Memory Warning Method

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
}


#pragma mark - Shared Instance

kGCD_SINGLETON_FOR_CLASS(AnyChatVC);


#pragma mark -
#pragma mark - TextField Delegate

- (BOOL)textFieldShouldBeginEditing:(UITextField *)textField
{
    return YES;
}

- (BOOL)textFieldShouldReturn:(UITextField *)textField
{
    return YES;
}


#pragma mark - AnyChatNotifyMessageDelegate

// 连接服务器消息
- (void) OnAnyChatConnect:(BOOL) bSuccess
{
    if (bSuccess)
    {
        theStateInfo.text = @"• Success connected to server";
    }
}

// 用户登陆消息
- (void) OnAnyChatLogin:(int) dwUserId : (int) dwErrorCode
{
    if(dwErrorCode == GV_ERR_SUCCESS)
    {
        //更新系统默认视频参数设置
        [[SettingVC sharedSettingVC] updateUserVideoSettings];
        
        theOnLineLoginState = YES;
        self.theMyUserID = dwUserId;
        self.theMyUserName = self.theUserName.text;
        [self saveSettings];  //登陆信息归档
        theStateInfo.text = [NSString stringWithFormat:@" Login successed. Self UserId: %d", dwUserId];
        [theLoginBtn setBackgroundImage:[UIImage imageNamed:@"btn_logout_01"] forState:UIControlStateNormal];
        
        if([theRoomNO.text length] == 0)
        {
            theRoomNO.text = [self GetRoomNO];
        }
        [AnyChatPlatform EnterRoom:[theRoomNO.text intValue] :@""];
    }
    else
    {
        theOnLineLoginState = NO;
        theStateInfo.text = [NSString stringWithFormat:@"• Login failed(ErrorCode:%i)",dwErrorCode];
    }
    
}

// 当前用户进入房间消息
- (void) OnAnyChatEnterRoom:(int) dwRoomId : (int) dwErrorCode
{
    //更新用户自定义视频参数设置
    [[SettingVC sharedSettingVC] updateUserVideoSettings];
    
    if (dwErrorCode != 0)
    {
        theStateInfo.text = [NSString stringWithFormat:@"• Enter room failed(ErrorCode:%i)",dwErrorCode];
    }

    self.onlineUserMArray = [self getOnlineUserArray];
    
}

// 房间在线用户消息
- (void) OnAnyChatOnlineUser:(int) dwUserNum : (int) dwRoomId
{
    self.onlineUserMArray = [self getOnlineUserArray];
    
    videoVC = [[VideoVC alloc] init];
    
    //  local Video
    [videoVC StartLocalVideoChat];
    //  iRemote Video
    [videoVC myEnterRoomChat];
    
    [self.navigationController pushViewController:videoVC animated:YES];
}

// 用户进入房间消息
- (void) OnAnyChatUserEnterRoom:(int) dwUserId
{
    NSString *dwUserIdStr = [NSString stringWithFormat:@"%d",dwUserId];
    
    NSInteger isNilArrayOfIndex = [self.theOnChatUserIDStrMArray indexOfObject:@"0"];
    if (isNilArrayOfIndex != NSNotFound)
    {
        [self.theOnChatUserIDStrMArray replaceObjectAtIndex:isNilArrayOfIndex
                                                 withObject:dwUserIdStr];
        
        if (videoVC == nil) {
            videoVC = [[VideoVC alloc] init];
        }
        //进行视频
        videoVC.pvBlock(dwUserId,((int)isNilArrayOfIndex + 1),videoVC);
    }
    

    
    self.onlineUserMArray = [self getOnlineUserArray];
}

// 用户退出房间消息
- (void) OnAnyChatUserLeaveRoom:(int) dwUserId
{
    if (videoVC == nil)
    {
        videoVC = [[VideoVC alloc] init];
    }

    NSString *dwUserIdStr = [NSString stringWithFormat:@"%d",dwUserId];

    if ([self.theOnChatUserIDStrMArray containsObject:dwUserIdStr])
    {   //find LeaveRoom userID in Array Of Index
        NSInteger userIDIndexOfInt = [self.theOnChatUserIDStrMArray indexOfObject:dwUserIdStr];
        //DEL LeaveRoom userID & set val=“0”
        [self.theOnChatUserIDStrMArray replaceObjectAtIndex:userIDIndexOfInt withObject:@"0"];
        
        //用户离开后，把停止的最后一帧 设置为图片
        videoVC.aiBlock(((int)userIDIndexOfInt+1),videoVC);
        
        
    }
    
    NSMutableArray *OnLineWaitingChatUserIDMArray = [self getOnLineWaitingChatUserIDMArray];
    
}

// 网络断开消息
- (void) OnAnyChatLinkClose:(int) dwErrorCode
{
    [videoVC FinishVideoChat];
    [AnyChatPlatform LeaveRoom:-1];
    [AnyChatPlatform Logout];
    
    theStateInfo.text = [NSString stringWithFormat:@"• OnLinkClose(ErrorCode:%i)",dwErrorCode];
    theOnLineLoginState = NO;
    [theLoginBtn setBackgroundImage:[UIImage imageNamed:@"btn_login_01"] forState:UIControlStateNormal];
    
    [self.navigationController popToRootViewControllerAnimated:YES];
    UIAlertView *networkAlertView = [[UIAlertView alloc] initWithTitle:@"网络断开,请重新登录."
                                                        message:@"Network disconnection."
                                                       delegate:self
                                              cancelButtonTitle:nil
                                              otherButtonTitles:@"确定",nil];
    [networkAlertView show];
}


#pragma mark -
#pragma mark - Get & Save Settings Method

- (id) GetServerIP
{
    NSString* serverIP;
    NSString *documentPath = [NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES) objectAtIndex:0];
    NSString *filePath = [documentPath stringByAppendingPathComponent:kAnyChatSettingsFileName];
    if([[NSFileManager defaultManager] fileExistsAtPath:filePath])
    {
        NSMutableArray* array = [[NSMutableArray alloc]initWithContentsOfFile:filePath];
        serverIP =  [array objectAtIndex:0];
        
        if([serverIP length] == 0)
        {
            theServerIP.text = @"demo.anychat.cn";
            serverIP = theServerIP.text;
        }
    }
    else
    {
        theServerIP.text = @"demo.anychat.cn";
        serverIP = theServerIP.text;
    }
    return serverIP;
}

- (id) GetServerPort
{
    NSString* serverPort;
    NSString *documentPath = [NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES) objectAtIndex:0];
    NSString *filePath = [documentPath stringByAppendingPathComponent:kAnyChatSettingsFileName];
    if([[NSFileManager defaultManager] fileExistsAtPath:filePath])
    {
        NSMutableArray* array = [[NSMutableArray alloc]initWithContentsOfFile:filePath];
        serverPort = [array objectAtIndex:1];
        
        if([serverPort intValue] == 0 || [serverPort length] == 0)
        {
            theServerPort.text = @"8906";
            serverPort = theServerPort.text;
        }
    }
    else
    {
        theServerPort.text = @"8906";
        serverPort = theServerPort.text;
    }
    return serverPort;
}

- (id) GetUserName
{
    NSString* userName;
    NSString *documentPath = [NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES) objectAtIndex:0];
    NSString *filePath = [documentPath stringByAppendingPathComponent:kAnyChatSettingsFileName];
    if([[NSFileManager defaultManager] fileExistsAtPath:filePath])
    {
        NSMutableArray* array = [[NSMutableArray alloc]initWithContentsOfFile:filePath];
        userName =  [array objectAtIndex:2];
        
        if([userName length] == 0)
        {
            theUserName.text = @"AnyChat";
            userName = theServerIP.text;
        }
    }
    else
    {
        theUserName.text = @"AnyChat";
        userName = theUserName.text;
    }
    return userName;
}

- (id) GetRoomNO
{
    NSString* roomNO;
    NSString *documentPath = [NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES) objectAtIndex:0];
    NSString *filePath = [documentPath stringByAppendingPathComponent:kAnyChatSettingsFileName];
    if([[NSFileManager defaultManager] fileExistsAtPath:filePath])
    {
        NSMutableArray* array = [[NSMutableArray alloc]initWithContentsOfFile:filePath];
        roomNO = [array objectAtIndex:3];
        
        if([roomNO intValue] == 0 || [roomNO length] == 0)
        {
            theRoomNO.text = @"1";
            roomNO = theRoomNO.text;
        }
    }
    else
    {
        theRoomNO.text = @"1";
        roomNO = theRoomNO.text;
    }
    return roomNO;
}

- (void)saveSettings
{
    NSString *documentPath = [NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES) objectAtIndex:0];
    NSString *filePath = [documentPath stringByAppendingPathComponent:kAnyChatSettingsFileName];
    [[NSArray arrayWithObjects:theServerIP.text,theServerPort.text,theUserName.text,theRoomNO.text, nil] writeToFile:filePath atomically:YES];
}


#pragma mark - Instance Method


- (void)AnyChatNotifyHandler:(NSNotification*)notify
{
    NSDictionary* dict = notify.userInfo;
    [anyChat OnRecvAnyChatNotify:dict];
}

//获取等候视频的在线用户
- (NSMutableArray *) getOnLineWaitingChatUserIDMArray
{
    self.onlineUserMArray = [self getOnlineUserArray];
    NSMutableArray *waitingIDStrMArray = [NSMutableArray arrayWithArray:self.onlineUserMArray];
    NSPredicate *thePredicate = [NSPredicate predicateWithFormat:@"NOT (SELF in %@)", self.theOnChatUserIDStrMArray];
    [waitingIDStrMArray filterUsingPredicate:thePredicate];
    
    return waitingIDStrMArray;
}

- (NSMutableArray *) getOnlineUserArray
{
    NSMutableArray *onLineUserList = [[NSMutableArray alloc] initWithArray:[AnyChatPlatform GetOnlineUser]];
    return onLineUserList;
}

- (IBAction)hideKeyBoard
{
    [self.theServerIP resignFirstResponder];
    [self.theServerPort resignFirstResponder];
    [self.theUserName resignFirstResponder];
    [self.theRoomNO resignFirstResponder];
}

- (IBAction)OnLoginBtnClicked:(id)sender
{
    if (theOnLineLoginState == YES)
    {
        [self OnLogout];
    }
    else
    {
        [self OnLogin];
    }
}

- (void) OnLogin
{
    if (theOnLineLoginState == NO)
    {
        [self showLoadingAnimated];
        
        if([theServerIP.text length] == 0)
        {
            theServerIP.text = [self GetServerIP];
        }
        if([theServerPort.text length] == 0)
        {
            theServerPort.text = [self GetServerPort];
        }
        if([theUserName.text length] == 0)
        {
            theUserName.text = [self GetUserName];
        }
        if([theRoomNO.text length] == 0)
        {
            theRoomNO.text = [self GetRoomNO];
        }
        
        [AnyChatPlatform Connect:theServerIP.text : [theServerPort.text intValue]];
        [AnyChatPlatform Login:theUserName.text : @""];
    }
    
    [self hideKeyBoard];
}

- (void) OnLogout
{
    if (theOnLineLoginState == YES)
    {
        [AnyChatPlatform Logout];
        
        theOnLineLoginState = NO;
        
        theStateInfo.text = @"• Logout Server.";
        [theLoginBtn setBackgroundImage:[UIImage imageNamed:@"btn_login_01"] forState:UIControlStateNormal];
    }
}

- (BOOL)prefersStatusBarHidden
{
    return YES;
}

- (IBAction)VideoSetting:(id)sender
{
    [self.navigationController pushViewController:[SettingVC sharedSettingVC] animated:YES];
}


#pragma mark - AlertView delegate

- (void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex
{
}


#pragma mark - AlertView method

- (NSString *)showInfoAlertView:(NSString *)Title : (NSString *)subTitle
{
    UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:Title
                                                        message:subTitle
                                                       delegate:self
                                              cancelButtonTitle:nil
                                              otherButtonTitles:nil,nil];
    [alertView show];
    
    [self performSelector:@selector(dimissAlertView:) withObject:alertView afterDelay:1.5];
    
    return subTitle;
}

- (void) dimissAlertView:(UIAlertView *)alert {
    if(alert){
        [alert dismissWithClickedButtonIndex:[alert cancelButtonIndex] animated:YES];
    }
}


#pragma mark - Loading Animation Method

- (void)showLoadingAnimated
{
    HUD = [[MBProgressHUD alloc] initWithView:self.navigationController.view];
    [self.navigationController.view addSubview:HUD];
    
    HUD.delegate = self;
    HUD.dimBackground = YES;
    HUD.labelText = @"AnyChatMeetings";
    HUD.detailsLabelText = @"Loading...";
    
    [HUD showWhileExecuting:@selector(onLoginLoadingAnimatedRunTime) onTarget:self withObject:nil animated:YES];
}

- (void)onLoginLoadingAnimatedRunTime
{
    int theTimes = 0;
    while (theOnLineLoginState == NO && theTimes < 6)
    {
        sleep(1);
        theTimes++;
        
        if (theTimes == 5 ) {
            [self timeOutMsg];
        }
    }
    
}

- (void) timeOutMsg
{
    if (theOnLineLoginState == NO)
    {
        theStateInfo.text = @"Login timeout,please check the Network and Setting.";
    }
}


#pragma mark - Rotation

- (BOOL)shouldAutorotate
{
    return NO;
}


#pragma mark - UI Controls

- (void)setUI
{
    [self.navigationController setNavigationBarHidden:YES];
    
    theUserName.text = [self GetUserName];
    theServerIP.text = [self GetServerIP];
    theServerPort.text = [self GetServerPort];
    theRoomNO.text = [self GetRoomNO];
    
    [theServerIP addTarget:self action:@selector(textFieldShouldReturn:) forControlEvents:UIControlEventEditingDidEndOnExit];
    [theServerPort addTarget:self action:@selector(textFieldShouldReturn:) forControlEvents:UIControlEventEditingDidEndOnExit];
    [theUserName addTarget:self action:@selector(textFieldShouldReturn:) forControlEvents:UIControlEventEditingDidEndOnExit];
    [theRoomNO addTarget:self action:@selector(textFieldShouldReturn:) forControlEvents:UIControlEventEditingDidEndOnExit];
    
    if (k_sysVersion < 7.0)
    {
        [[UIApplication sharedApplication] setStatusBarHidden:YES];
    }

    theVersionLab.text = [AnyChatPlatform GetSDKVersion];
    
    [self prefersStatusBarHidden];
    
}


@end