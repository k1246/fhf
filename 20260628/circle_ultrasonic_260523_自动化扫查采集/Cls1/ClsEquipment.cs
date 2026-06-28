using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO.Ports;

namespace SmartStudio.Cls
{
    /// <summary>
    /// 定义静态的全局变量，整个程序都可以访问
    /// </summary>
    public static class Device_A
    {
        //设备配置声明
        public static Process_Control Work_control;//工作过程控制
        public static Sys sys;//系统
        public static Controler controler;//控制器
        public static Controler S7_200_Smart;//控制器
        public static Acquisition acquisition;//采集器
        public static Option option;//系统参数
        public static Recipe recipe;//配方
        public static Report report;//报告
        public static Sensor Position;//计数器
        public static Recipe_UpDown_Manual recipe_UpDown_Manual;
        public struct Process_Control//工作过程控制
        {
            public int WorkStatus;//工作状态 0：手动 1：自动 
            public int FlowStep;//自动工作流程步
            public int FlowStepSave;//临时记忆流程步
        }
        public struct Sys//系统结构体
        {
            public string AppName;//程序名称
            public string AppVer;//软件版本
            public string CustomerLogoPath;//客户LOGO文件路径及名称
            public string CustomerName;//客户名称
            public string Manufacturer;//厂家名称
            public string ManufacturerAddress;//厂家地址
            public string ManufacturerPhone;//厂家电话
            public string Contacts;//联系人
            public string tip;//提示信息
            public string alarmTip;//报警信息
            public string Logo01;
            public string Logo02;

            public string OperatorNumber;//操作者编号

            public string DataCommandText_CreatLoginTable;//数据库SQL命令： 生成登录表
            public string DataCommandText_CreatResultTable_1;//数据库SQL命令： 生成结果表1
            public string DataCommandText_CreatResultTable_2;//数据库SQL命令： 生成结果表2
            public string DataCommandText_CreatResultTable_3;//数据库SQL命令： 生成结果表3
            public string DataCommandText_CreatResultTable_4;//数据库SQL命令： 生成结果表4
            public string DataCommandText_CreatResultTable_5;//数据库SQL命令： 生成结果表5
            public string DataCommandText_Select_01;
            public string DataCommandText_Select_02;
            public string DataCommandText_Select_03;
            public string DataCommandText_Select_04;
            public string DataCommandText_Select_05;
            public string DataCommandText_Insert_01;
            public string DataCommandText_Insert_02;
            public string DataCommandText_Insert_03;
            public string DataCommandText_Insert_04;
            public string DataCommandText_Insert_05;
            public string DataCommandText_Delete_01;
            public string DataCommandText_Delete_02;
            public string DataCommandText_Delete_03;
            public string DataCommandText_Delete_04;
            public string DataCommandText_Delete_05;
            public string DataCommandText_Update_01;
            public string DataCommandText_Update_02;
            public string DataCommandText_Update_03;
            public string DataCommandText_Update_04;
            public string DataCommandText_Update_05;
            public string DataCommandText_Reserve01;
            public string DataCommandText_Reserve02;
            public string DataCommandText_Reserve03;
            public string DataCommandText_Reserve04;
            public string DataCommandText_Reserve05;
            public string DataCommandText_Reserve06;

            public string OperatorName;//操作者登录姓名
            public string OperatorPsssword;//操作者登录密码
            public string OperatorCard;//操作者身份文件
            public string AccessLevel;//权限 0：开发者 1：系统管理员 2：管理员 3：操作员

            public string Reserve01;//预留
            public string Reserve02;//预留
            public string Reserve03;//预留
            public string Reserve04;//预留
            public string Reserve05;//预留
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
            public string IP;
            public string DSN;
            public string PortNumber;

            public string Reserve01;//预留
            public string Reserve02;//预留
            public string Reserve03;//预留
            public string Reserve04;//预留
            public string Reserve05;//预留
        }
        public struct Sensor //传感器结构体
        {
            public string DeviceName;// 名称
            public string Ver;//版本
            public string Manufacturer;//制造商
            public string ComType;//通讯接口类型 0、无 1、串口 2、网口 3、PCI 4、USB 5、其它
            //串口参数
            public string portName;
            public int baudRate;
            public Parity parity;
            public int dataBits;
            public StopBits stopBits;
            public int ReadTimeout;
            public bool RtsEnable;
            public int AddressNumber;//通讯地址

            public string Name;//名称
            public string Unit;//单位
            public int Decimal;//小数点保留
            public bool Enable;//使能
            public double MaxVal;//传感器范围
            public double MinVal;//传感器范围
            public string SignalTypes;//信号类型 4-20ma、0-5V等
            public double Calibration;//增益
            public double Offset;//传感器0点
            public double SoftLimitUp;//软上限位
            public double SoftLimitDown;//软下限位
            public int Filter;//平均滤波点数
            public double Original_value;//原始值
            public double Display_value;//显示值
            public int Multiple;//倍数，用于单位切换，只影响显示值
            public string Reserve01;//预留
            public string Reserve02;//预留
            public string Reserve03;//预留
            public string Reserve04;//预留
            public string Reserve05;//预留
        };
        public struct Option//系统参数结构体
        {
            public string Printername;//打印机名称
            public string FolderPath;//打印模板路径
        };
        public struct Recipe //配方结构体
        {
            public int Type;//试验类型 1、升降机构 2、滑动机构
            //public int TestItem;//试验项 1、手动试验 2、。。。
            public string TestNumber;//试验编号
            public string Model;//产品型号
            public string TesterNumber;//操作工号
            public string TotalH;//升降调节距离
            public string Reserve05;//预留
            
        };
        public struct Report //报告结构体
        {
            public int EndResult;//总结果 0：无 1：合格 2：不合格
            public int ID;
            public DateTime WorkDateTime;//测试时间
            public string Operator;//操作员
            public string Team;//班组
            public string Specification;//规格
            public string Reserve01;//预留
            public string Reserve02;//预留
            public string Reserve03;//预留
            public string Reserve04;//预留
            public string Reserve05;//预留

        }

        public struct Recipe_UpDown_Manual //升降手动
        {
            public string type;//上升检测 下降检测
            public string F0;//加载负荷
        };
        public struct Recipe_UpDown_Immobilization //升降离合器寿命
        {
            public string N0;//连续工作行程数
            public string T0;//首次制动时间
            public string N1;//单行程制动次数
            public string T2;//单次制动接通时间
            public string T3;//制动间间隔时间
            public string F0;//加载负荷
        };
        public struct Recipe_UpDown_ClutchRun //升降离合器磨合
        {
            public string N0;//连续工作行程个数
            public string T0;//小循环间歇时间
            public string N1;//大循环个数

            public string T2;//离合磨合时间
            public string T3;//间歇时间
            public string F0;//判断负荷，一般98N
            public string Speed;//运动速度
            public string Dis;//升降磨合距离
        };
        public struct Recipe_UpDown_MaxLoad //升降极限负荷
        {
            public string Pspeed;//预负荷速度
            public string Threshold;//溃缩判断阈值
            public string Preload;//预负荷
            public string Fspeed;//力控速度
            public string T0;//力控目标保持时间
            public string F0;//力控目标
            public string type;//是3s还是30s试验
        };
        public struct Recipe_UpDown_Predelivery //升降出厂
        {
            public string N0;//连续工作行程数
            public string T0;//首次制动时间
            public string N1;//单行程制动次数
            public string T2;//单次制动接通时间

            public string Load;//额定负载
            public string CylNumber;//行程次数
            public string RestTime;//休息时间
            public string Cyclic;//循环
        };
        public struct Recipe_UpDown_Runnig //升降磨合
        {
            public string N0;//连续工作行程数
            public string T0;//首次制动时间
            public string N1;//单行程制动次数
            public string T2;//单次制动接通时间

            public string Load;//额定负载
            public string N3;//行程次数
            public string RestTime;//休息时间
            public string N4;//总循环次数
            public string Th;//单程间歇时间
        }
        public struct Recipe_UpDown_LifeTest //升降寿命
        {
            public string N0;//连续工作行程数
            public string T0;//首次制动时间
            public string N1;//单行程制动次数
            public string T2;//单次制动接通时间

            public string Load;//额定负载
            public string N3;//行程次数
            public string RestTime;//休息时间
            public string N4;//总循环次数
            public string Th;//单程间歇时间

        };

        public struct Recipe_Sliding_Manual //滑动手动
        {
            public string type;//上升检测 下降检测
            public string Module;//电动机构型号
            public string F0;//加载负荷
            public string T0;
        };
        public struct Recipe_Sliding_Immobilization //滑动制动
        {
            public string N0;//连续工作行程数
            public string T0;//首次制动时间
            public string N1;//单行程制动次数
            public string T2;//单次制动接通时间
            public string T3;//制动间间隔时间
            public string T4;//运行时间
            public string F0;//加载扭矩
        };
        public struct Recipe_Sliding_ClutchRun //滑动离合器磨合
        {
            public int Type;//试验类型 1、升降机构 2、滑动机构
            public int TestItem;//试验项 1、手动试验 2、。。。
            public string TestNumber;//试验编号
            public string Model;//产品型号
            public string OperatorNumber;//操作工号
            public string Reserve04;//预留
            public string Reserve05;//预留
        };
        public struct Recipe_Sliding_Runnig //滑动磨合
        {
            public string N0;//连续工作行程数
            public string T0;//首次制动时间
            public string N1;//单行程制动次数
            public string T2;//单次制动接通时间
            public string RunTime;//单程运行时间
            public string Load;//额定扭矩
            public string N3;//行程次数
            public string RestTime;//休息时间
            public string N4;//总循环次数
            public string Th;//单程间歇时间
        };
        public struct Recipe_Sliding_LifeTest //滑动寿命试验
        {
            public string N0;//连续工作行程数
            public string T0;//首次制动时间
            public string N1;//单行程制动次数
            public string T2;//单次制动接通时间
            public string RunTime;//单程运行时间
            public string Load;//额定扭矩
            public string N3;//行程次数
            public string RestTime;//休息时间
            public string N4;//循环
            public string Th;//单程间歇时间
        };
        public struct Recipe_Sliding_MaxLoad //滑动极限负荷
        {
            public string Pspeed;//连续工作行程数
            public string Threshold;//首次制动时间
            public string Preload;//单行程制动次数
            public string Fspeed;//单次制动接通时间
            public string T0;//试验编号
            public string F0;//产品型号
        };
        public struct Recipe_Sliding_Predelivery //滑动出厂
        {
            public string N0;//连续工作行程数
            public string T0;//首次制动时间
            public string N1;//单行程制动次数
            public string T2;//单次制动接通时间
            public string RunTime;//单程运行时间
            public string Load;//额定负载
            public string CylNumber;//行程次数
            public string RestTime;//休息时间
            public string Cyclic;//循环

        };


    }
}
