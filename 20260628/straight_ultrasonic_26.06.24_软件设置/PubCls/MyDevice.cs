using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO.Ports;
using System.Speech.Synthesis;//第一步，引用语音
using Seagull.BarTender.Print;//引用BarTender打印
using ACS.SPiiPlusNET;      // ACS .NET Library
namespace FlatUI_TestPlatform.PubCls
{
    /// <summary>
    /// 定义静态的全局变量，整个程序都可以访问
    /// </summary>

    public static class MyDevice
    {
        //设备配置声明
        public static Sys sys;//系统
        public static Config config;//系统
        public static User user;//登录的用户
        public static Option option;//系统参数
        public static Recipe1 recipe1;//第一种配方表
        public static Recipe2 recipe2;//第二种配方表
        public static Recipe2 recipe3;//第三种配方表

        public static Report1 report1;//第一种报告
        public static Report2 report2;//第二种报告
        public static Report3 report3;//第三种报告

        #region 声明静态传感器
        public static Sensor PosX,PosZ,PosY ;//位置传感器
        public static Sensor Load3X, Load3Z, Load3Y, LoadY, LoadP1, LoadP2, LoadP3, LoadP4;//三轴力、柱式力、压电式力
        public static Sensor a;
        #endregion

        public static Controler controler;//控制器
        public static Controler S7_200_Smart;//控制器
        public static Acquisition acquisition;//采集器
        public static Work_Control Work_control;//工作过程控制
        public static SpeechSynthesizer speech = new SpeechSynthesizer();//第二步：实例化语音
        public struct Sys//系统结构体
        {
            public string tip;//提示信息
            public string alarmTip;//报警信息

            public string OperatorNumber;//操作者编号
            public string OperatorName;//操作者登录姓名
            public string AccessLevel;//权限 0：开发者 1：系统管理员 2：管理员 3：操作员
            public bool DebugOnline;//是否处于调试模式
        }
        public struct Config//配置结构体
        {
            public string AppName;//程序名称
            public string Abbreviation;//简称
            public string AppVer;//软件版本
            public string CustomerLogoPath;//客户LOGO文件路径及名称
            public string CustomerName;//客户名称
            public string Manufacturer;//厂家名称

            public Axis AxisX;//X轴编号
            public Axis AxisY;//Y轴编号
            public Axis AxisZ;//Z轴编号
            public int AxisX_Dir;//X轴方向
            public int AxisY_Dir;//Y轴方向
            public int AxisZ_Dir;//Z轴方向
            public int TotalAxis;//总轴数
            public string Reserve08;//预留
            public string Reserve09;//预留
            public string Reserve10;//预留
            public bool PrinterEnable;//是否有打印机功能
            public bool SpeechEnable;//语音助理功能

            public bool LoginEnable;//是否免登录
            public bool RecipeEnable;
            public bool ReviewEnable;
            public bool OptionEnable;
            public bool MaintainEnable;
            public bool ToolsEnable;
            public bool UserEnable;
            public bool ConfigEnable;
            public bool DebugEnable;
            public bool DemoEnable;

        }
        public struct User//用户结构体
        {
            public string UserID;//数据库中ID号
            public string UserNo;//员工编号
            public string UserName;//操作者登录姓名
            public string UserPsssword;//操作者登录密码
            public string UserLevel;//权限 0：开发者 1：系统管理员 2：管理员 3：操作员
        }

        public struct Option//设置结构体
        {
            public string Printername;//打印机名称
            public string FolderPath;//打印模板路径

            public string CutPicPath;//截图保存路径
            public string PhotoPath;//拍照保存路径

            public string Reserve03;//预留
            public string Reserve04;//预留
            public string Reserve05;//预留
            public string Reserve06;//预留
            public string Reserve07;//预留
            public string Reserve08;//预留
            public string Reserve09;//预留
            public string Reserve10;//预留
        };
        public struct Recipe1 //第一种配方结构体
        {
            public string Reserve01;//预留
            public string Reserve02;//预留
            public string Reserve03;//预留
            public string Reserve04;//预留
            public string Reserve05;//预留
            public string Reserve06;//预留
            public string Reserve07;//预留
            public string Reserve08;//预留
            public string Reserve09;//预留
            public string Reserve10;//预留
        };
        public struct Recipe2 //第二种配方结构体
        {
            public string Reserve01;//预留
            public string Reserve02;//预留
            public string Reserve03;//预留
            public string Reserve04;//预留
            public string Reserve05;//预留
            public string Reserve06;//预留
            public string Reserve07;//预留
            public string Reserve08;//预留
            public string Reserve09;//预留
            public string Reserve10;//预留
        };
        public struct Recipe3 //第三种配方结构体
        {
            public string Reserve01;//预留
            public string Reserve02;//预留
            public string Reserve03;//预留
            public string Reserve04;//预留
            public string Reserve05;//预留
            public string Reserve06;//预留
            public string Reserve07;//预留
            public string Reserve08;//预留
            public string Reserve09;//预留
            public string Reserve10;//预留
        };
        public struct Report1 //第一种报告结构体
        {
            public string Reserve01;//预留
            public string Reserve02;//预留
            public string Reserve03;//预留
            public string Reserve04;//预留
            public string Reserve05;//预留
            public string Reserve06;//预留
            public string Reserve07;//预留
            public string Reserve08;//预留
            public string Reserve09;//预留
            public string Reserve10;//预留
        }
        public struct Report2 //第二种报告结构体
        {
            public string Reserve01;//预留
            public string Reserve02;//预留
            public string Reserve03;//预留
            public string Reserve04;//预留
            public string Reserve05;//预留
            public string Reserve06;//预留
            public string Reserve07;//预留
            public string Reserve08;//预留
            public string Reserve09;//预留
            public string Reserve10;//预留
        }
        public struct Report3 //第三种报告结构体
        {
            public string Reserve01;//预留
            public string Reserve02;//预留
            public string Reserve03;//预留
            public string Reserve04;//预留
            public string Reserve05;//预留
            public string Reserve06;//预留
            public string Reserve07;//预留
            public string Reserve08;//预留
            public string Reserve09;//预留
            public string Reserve10;//预留
        }
        public struct Work_Control//工作过程控制
        {
            public bool running;//运行中
            public int WorkStatus;//工作状态 0：手动 1：自动 
            public int FlowStep;//自动工作流程步
            public int FlowStepSave;//临时记忆流程步
        }
        public struct Controler//控制器结构体
        {
            public string Name;// 名称
            public string Ver;//版本
            public string Manufacturer;//制造商
            public string ComType;//通讯接口类型 0、无 1、串口 2、网口 3、PCI 4、USB 5、其它
            //串口参数
            public string portName;
            public int baudRate;
            public Parity parity;
            public int dataBits;
            public StopBits stopBits;
            public int AddressNumber;//通讯地址
            //网口参数
            public string LocalIP;
            public string LocalDSN;
            public string LocalPort;
            public string RemoteIP;
            public string RemoteDSN;
            public string RemotePort;
            public bool LinkStatus;// 链接状态
            public string Reserve02;//预留
            public string Reserve03;//预留
            public string Reserve04;//预留
            public string Reserve05;//预留
        }
        public struct Acquisition//采集器结构体
        {
            public string Name;// 名称
            public string Manufacturer;//制造商
            public int    Mode;//模式，如差分、单端
            public int    Range;//量程，如10V
            public int    SamplingRate;//采样率，如100,250
            public int    TriggerMode;//触发方式
            public bool   Save;//数据是否存储
            public string SaveDir;//数据存储路径

            #region 通讯参数
            //串口参数
            public string portName;
            public int baudRate;
            public Parity parity;
            public int dataBits;
            public StopBits stopBits;
            public int AddressNumber;//通讯地址
            //网口参数
            public string IP;
            public string DSN;
            public string PortNumber;
            //其它接口参数
            #endregion

        }
        public struct Sensor //传感器结构体
        {
            public string CardIndex;//采集卡索引或名称
            public string CHx;//采集通道编号
            public bool   Enable;//使能采集
            public string Tag;//标签
            public int    UnitCode;//传感器单位编码
            public int    Dot;//示值小数位数
            public double MaxScale;//传感器最大量程
            public double MinScale;//传感器最小量程
            public double Calibration;//标定
            public double Offset;//零点偏移
            public double SoftLimitUp;//软上限位
            public double SoftLimitDown;//软下限位
            public int    Filter;//平均滤波点数
            public double OriginalVal;//采集的原始值
            public double DisplayVal;//显示值
            public int    Multiple;//倍数，用于单位切换，只影响显示值
            public string Describe;//描述
        };
    }
}
