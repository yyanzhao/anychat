﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AnyChatMultiCamera
{
    /// <summary>
    /// 连接信息类
    /// </summary>
    public class ConnectInfo
    {
        /// <summary>
        /// 服务器IP
        /// </summary>
        public string ServerIP { get; set; }
        /// <summary>
        /// 服务器端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 房间号ID
        /// </summary>
        public int RoomID { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 是否打开远程桌面
        /// </summary>
        public bool isOpenRemoteDesktop { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppGuid { get; set; }

        /// <summary>
        /// 登录方式
        /// </summary>
        public LoginType loginType { get; set; }

        /// <summary>
        /// 签名服务器Url
        /// </summary>
        public string signServerUrl { get; set; }
    }

    /// <summary>
    /// 登录方式
    /// </summary>
    public enum LoginType
    {
        /// <summary>
        /// 普通登录
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 签名方式登录
        /// </summary>
        Sign = 2
    }

}
