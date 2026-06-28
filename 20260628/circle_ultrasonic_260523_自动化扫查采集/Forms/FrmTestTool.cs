//#define CCD_HIKROBOT
//#define CCD_DAHENG
using ACS.SPiiPlusNET;      // ACS .NET Library
using csDriverOEMMC;
using csDriverOEMPA;
using csDriverOEMPA1;
//251118
//using csDriverOEMPA;
//using csDriverOEMPA1;
using csDriverOEMPA2;
using csDriverOEMPAmax;
using csDriverOEMPAmini;
using DocumentFormat.OpenXml.InkML;
using FlatUI_TestPlatform.PubCls;
using HslControls;
using System;
//using System.Windows.Media;
//using DocumentFormat.OpenXml.Vml;

//251114
using System.Collections.Concurrent;  // ← 添加这行
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using WizardOEMPA;
using static FlatUI_TestPlatform.Forms.FrmTestTool;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
//using WizardOEMPA;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Data.Matlab;

using System.Reflection;
using System.Windows.Controls.Primitives;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices.ComTypes;
using OEMFormExample;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Configuration.Internal;
using System.Xml.Linq;
using static Seagull.BarTender.SystemDatabase.ReprintRange;
using static Seagull.BarTender.Print.LabelFormat;


namespace FlatUI_TestPlatform.Forms
{
    public partial class FrmTestTool : Form
    {
        #region ACS变量声明
        public Api _ACS = new Api();
        bool[] bEnable = new bool[8];//三个轴使能状态
        private const int MAX_AXIS_COUNT = 32;
        private const int MAX_BUFFER_CNT = 64;
        private const int MAX_UI_LIMIT_CNT = 8;//最大3轴限位
        private const int MAX_UI_IO_CNT = 8;
        private int m_nTotalBuffer = 0;//
        private Axis[] m_arrAxisList = null;
        private bool m_bConnected = false;
        // For update values
        private MotorStates m_nMotorState;
        private MotorStates m_nMotorFault;
        private ProgramStates m_nProgramState;
        private object m_objReadVar = null;
        private Array m_arrReadVector = null;
        private double[] m_lfRPos, m_lfFPos, m_lfPE, m_lfFVEL;
        private int m_nValues, m_nOutputState;
        //指示灯控件数组
        private HslControls.HslLanternSimple[] m_lblLeftLimit;//左限位
        private HslControls.HslLanternSimple[] m_lblRightLimit;//右限位
        private HslControls.HslLanternSimple[] m_lblEnable;//使能
        private HslControls.HslLanternSimple[] m_lblZeroPos;//零点
        private HslControls.HslLanternSimple[] m_lblFault;//故障
        private HslControls.HslLanternSimple[] m_lblMoving;//运动中
                                                           //使能按钮控件数组
        private HslControls.HslButton[] m_hslBtnEnable;//使能按钮
        private MotionFlags b_motionFlags = MotionFlags.ACSC_NONE;//绝对运动
        private int iBufferNo = 0;//当前运行程序编号
        #endregion
        private TextBox[] txtDouble;
        private TextBox[] txtInt;
        public int OutBit0State, OutBit1State, OutBit2State;//输出状态
        //[DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = false)]
        //private static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);
        #region 离散扫描系统参数声明
        //251113离散扫描系统
        private CancellationTokenSource _scanCancellationTokenSource;
        private Task _scanTask;
        private readonly object _scanLock = new object();
        private readonly IScanService _scanService;
        private ScanResultRecorder _resultRecorder;
        private double[,] _currentMotionMatrix;
        private readonly ClutchControlService _clutchService;
        #endregion


        #region 超声采集参数声明
        public csHWDeviceOEMPA hwDeviceOEMPA;
        public csHWDeviceOEMPA csHWDeviceOEMPA;
        public bool bConnectEnter;
        public int sizeTime, dataLostAscan, dataLostCscan;
        public bool bProcessConnection;
        public string wizardFileNamePitchCatch;
        private csWizardPitchCatchTemplate wizardPitchCatchTemplate;
        private csWizardTemplate wizardTemplate;
        public string wizardFileName;
        private bool wizardCompleted;
        private bool wizardCompletedPitchCatch;
        public csHWDeviceOEMPA1 hwDeviceOEMPA1;
        int m_iCycleCount;
        public int m_iHWDeviceId = -1;
        public bool connect, pulser, bIO, bSlave;
        bool checkBoxPulserEnable;
        int iThisDialogCount;
        FrmTestTool nextDevice;
        FrmTestTool prevDevice;
        public bool m_bCallback;
        static FrmTestTool firstDialog = null;
        bool[] m_bAcquisitionCscanAmp;
        bool[] m_bAcquisitionCscanTof;
        short[] m_sAcquisitionCscanAmp;
        short[] m_sAcquisitionCscanTof;
        public int m_iComboSynchronisation;
        double encoderAscanX, encoderAscanY, encoderCscanX, encoderCscanY;
        delegate void delegateUpdateRoot(ref csRoot root);
        private event delegateUpdateRoot mEvent;
        static int iDialogCount = 0;
        public Int64 dataSizeAscan;
        public Int64 dataTimerAscan;
        public int dataTimeAscan;
        public bool callbackCustomized;
        public String pFileName;
        public int cycleCount;
        double gain, start, range;
        System.Windows.Forms.Timer theTimer;
        public Int64 dataSizeCscan;
        public Int64 dataTimerCscan;
        public int dataTimeCscan;
        private /*static*/ object LockingVar = new object();
        static FormMsgBox msgBox = null;

        public int[] Ascan_Data;
        #endregion

        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = false)]

        private static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        //static extern uint GetCurrentProcessId();
        //private uint GetCurrentProcessId()
        //{
        //    return (uint)System.Diagnostics.Process.GetCurrentProcess().Id;
        //}
        [DllImport("UTCalculator.dll", EntryPoint = "fFunction2")]
        private extern static bool FocalLaw_FlatPart(
            [In] double dPrecision,
            [In] double dAngle, [In] double dWedgeVelocity, [In] double dSpecimenVelocityOut,
            [In] double dApX, [In] double dApY, [In] double dApZ,
            [In] double dRefX, [In] double dRefY, [In] double dRefZ,
            [In] double dDirX, [In] double dDirY, [In] double dDirZ,
            [Out] out double dX, [Out] out double dY, [Out] out double dZ,
            [In] int iErrorSizeMax, IntPtr wcError, [Out] out int iErrorSize);


        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(
            [In] IntPtr hProcess,
            [Out] out bool wow64Process
        );

        [DllImport("kernel32.dll")]
        static extern uint GetCurrentProcessId();

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetDllDirectory(string lpPathName);
        private void UpdateRoot(ref csRoot root)
        {
            m_bCallback = true;
            if (root.csExternalTriggerSequence != csEnumDigitalInput.csDigitalInputOff)
            {
                if ((root.csRequestIO != csEnumOEMPARequestIO.csOEMPANotRequired) &&
                    (root.csRequestIO != csEnumOEMPARequestIO.csOEMPAOnDigitalInputOnly))
                {
                    comboBoxSynchronisation.SelectedIndex = 3;
                    textBoxResolution.Text = String.Format("{0:F2} step/mm", (double)root.lSWEncoderResolution1 / (double)root.dwSWEncoderDivider1);
                }
            }
            else
            {
                switch (root.csTriggerMode)
                {
                    case csEnumOEMPATrigger.csOEMPAInternal:
                        comboBoxSynchronisation.SelectedIndex = 0;
                        break;
                    case csEnumOEMPATrigger.csOEMPAEncoder:
                        if ((root.csRequestIO != csEnumOEMPARequestIO.csOEMPANotRequired) &&
                            (root.csRequestIO != csEnumOEMPARequestIO.csOEMPAOnDigitalInputOnly))
                        {
                            comboBoxSynchronisation.SelectedIndex = 1;
                            textBoxResolution.Text = String.Format("{0:F2} step/mm", (double)root.lSWEncoderResolution1 / (double)root.dwSWEncoderDivider1);
                            textBoxStep.Text = String.Format("{0:F2} mm", root.dTriggerEncoderStep * 1.0e3);
                        }
                        break;
                    default:
                        if ((root.csRequestIO != csEnumOEMPARequestIO.csOEMPANotRequired) &&
                            (root.csRequestIO != csEnumOEMPARequestIO.csOEMPAOnDigitalInputOnly))
                        {
                            comboBoxSynchronisation.SelectedIndex = 2;
                            textBoxResolution.Text = String.Format("{0:F2} step/mm", (double)root.lSWEncoderResolution1 / (double)root.dwSWEncoderDivider1);
                        }
                        break;
                }
            }
            m_iComboSynchronisation = comboBoxSynchronisation.SelectedIndex;
            m_bCallback = false;
        }
        public FrmTestTool(FrmTestTool previousDevice)
        {
            //InitializeComponent();
            int iMonitorPort;
            uint ui = GetCurrentProcessId();

            SetDllDirectory(Application.StartupPath);
            //SetDllDirectory(dllPath);
            //251118
            
            InitializeComponent();
            InitializeChannelComboBoxes(); // 添加这行

            Control.CheckForIllegalCrossThreadCalls = false;
            txtDouble = new TextBox[] { txtSpeedX, txtSpeedY, txtSpeedZ, txtTravelX, txtTravelY, txtTravelZ, txtLineSpeed, txtAnglePosTarget, txtAnglePosSpeed };
            txtInt = new TextBox[] { txtTriggerTimeLine, txtNumInsertPoints, txtAngleJogDelayTime };
            foreach (TextBox txt in txtDouble)
            {
                txt.TextChanged += txtDouble_TextChanged; // 统一的事件处理
            }
            foreach (TextBox txt in txtInt)
            {
                txt.TextChanged += txtInt_TextChanged; // 统一的事件处理
            }


            #region 创建扫描系统控制对象
            //251113扫描系统事件捕获
            _scanService = new ChannelScanService(); // 或者其他的IScanService实现
            _clutchService = new ClutchControlService(_ACS);
            _resultRecorder = new ScanResultRecorder(_scanService, UpdateChart);

            // 启动扫描服务轮询
            _scanService.StartService();
            #endregion


            mEvent += new delegateUpdateRoot(UpdateRoot);
            iThisDialogCount = iDialogCount;
            prevDevice = previousDevice;
            nextDevice = null;
            if (iDialogCount == 0)
            {
                firstDialog = this;
                if (!csHWDeviceOEMPA.IsMultiProcessRegistered())
                    csHWDeviceOEMPA.RegisterMultiProcess("FrmTestTool");
            }
            iDialogCount++;

            bIO = false;
            bSlave = false;
            bConnectEnter = false;
            connect = false;
            pulser = false;
            checkBoxPulserEnable = true;
            dataSizeAscan = 0;
            //dataSizeCscan = 0;
            dataTimerAscan = 0;
            //dataTimerCscan = 0;
            dataTimeAscan = 0;
            //dataTimeCscan = 0;
            callbackCustomized = false;
            m_bCallback = false;
            pFileName = null;
            cycleCount = 0;
            gain = 0.0;
            start = 0.0;
            range = 0.0;
            dataLostAscan = 0;
            dataLostCscan = 0;
            encoderAscanX = 0;
            encoderAscanY = 0;
            encoderCscanX = 0;
            encoderCscanY = 0;
            hwDeviceOEMPA = null;
            m_iHWDeviceId = -1;
            m_iCycleCount = 0;
            wizardCompleted = false;
            wizardCompletedPitchCatch = false;
            initWizardFileName();

            theTimer = new System.Windows.Forms.Timer();
            //Adds the event and the event handler for the method that will 
            //process the timer event to the timer
            theTimer.Tick += new EventHandler((sender, e) => TimerEventProcess(sender, e, this));
            // Sets the timer interval to 5 seconds
            theTimer.Interval = 1000;
            theTimer.Start();

            UpdateDialog();

            textBoxWriteGain.Text = "0.0 dB";
            textBoxWriteStart.Text = "0.0 us";
            textBoxWriteRange.Text = "30.0 us";
            textBoxResolution.Text = "1 step/mm";
            textBoxStep.Text = "1.0 mm";
            comboBoxOEMType.SelectedIndex = 0;
            comboBoxSynchronisation.SelectedIndex = 0;
            textBoxPort.Text = "5001";
            SetThisComputerAddress();
            bProcessConnection = false;


            if (InternalCheckIsWow64())
            {
                this.Text = "OEMPAFormExample (x86, PID ";
                this.Text += ui.ToString();
                this.Text += ")";
            }
            else
            {
                this.Text = "OEMPAFormExample (PID ";
                this.Text += ui.ToString();
                this.Text += " / ";
                iMonitorPort = csHWDevice.GetMonitorPort(m_iHWDeviceId);
                this.Text += "monitor=";
                this.Text += iMonitorPort.ToString();
                if (iDialogCount > 1)
                {
                    this.Text += " / ";
                    iMonitorPort = csHWDevice.GetMonitorPort(m_iHWDeviceId);
                    this.Text += iDialogCount.ToString();
                }
                this.Text += ")";
            }
            //MessageBox.Show("DevId = " + m_iHWDeviceId.ToString());

        }

        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!IsWow64Process(p.Handle, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else
            {
                return false;
            }
        }
        private void InitializeChannelComboBoxes()
        {
            // 初始化A扫描发射阵元选择 (0-63)
            if (comboxBoxTransmit != null)
            {
                comboxBoxTransmit.Items.Clear();
                for (int i = 0; i < 64; i++)
                {
                    comboxBoxTransmit.Items.Add($"发射 {i}");
                }
                comboxBoxTransmit.SelectedIndex = 0;
                Debug.WriteLine($"初始化 comboBoxTransmit: {comboxBoxTransmit.Items.Count} 个选项");
            }
            else
            {
                Debug.WriteLine("comboBoxTransmit 为 null");
            }

            // 初始化A扫描接收阵元选择 (0-63)
            if (comboxBoxReceive != null)
            {
                comboxBoxReceive.Items.Clear();
                for (int i = 0; i < 64; i++)
                {
                    comboxBoxReceive.Items.Add($"接收 {i}");
                }
                comboxBoxReceive.SelectedIndex = 0;
                Debug.WriteLine($"初始化 comboBoxReceive: {comboxBoxReceive.Items.Count} 个选项");
            }
            else
            {
                Debug.WriteLine("comboBoxReceive 为 null");
            }

            // 初始化B扫描发射阵元选择 (0-63)
            if (comboxBoxBScanTransmit != null)
            {
                comboxBoxBScanTransmit.Items.Clear();
                comboxBoxBScanTransmit.Items.Add("无"); // 第一个选项为"无"
                for (int i = 0; i < 64; i++)
                {
                    comboxBoxBScanTransmit.Items.Add($"发射 {i}");
                }
                comboxBoxBScanTransmit.SelectedIndex = 0; // 默认选择"无"
                Debug.WriteLine($"初始化 comboBoxBScanTransmit: {comboxBoxBScanTransmit.Items.Count} 个选项");
            }
            else
            {
                Debug.WriteLine("comboBoxBScanTransmit 为 null");
            }
        }

        public void SetThisComputerAddress()
        {
            textBoxIPAddress.Text = "192.168.1.122";
        }

        private static void TimerEventProcess(Object myObject, EventArgs myEventArgs, FrmTestTool myForm)
        {
            Console.WriteLine("TimerEventProcess started at " + DateTime.Now); // 验证方法执行
            int iComError;
            csSWDevice swDevice;
            String strAux;
            bool connect2, pulser2, dataLost = false;
            Int64 size;
            double dThrougput;
            long dwAscanLostCount, dwCscanLostCount, dwEncoderLostCount, dwUSB3LostCount;

            if (myForm.hwDeviceOEMPA == null)
            {
                Console.WriteLine("hwDeviceOEMPA is null, exiting.");
                return;
            }
            swDevice = myForm.hwDeviceOEMPA.GetSWDevice();







            connect2 = swDevice.IsConnected();
            pulser2 = swDevice.IsPulserEnabled();
            Console.WriteLine($"IsConnected: {connect2} at {DateTime.Now}"); // 调试连接状态
            if (connect2)
                strAux = "Connected";
            else
                strAux = "Disconnected";
            Console.WriteLine($"strAux after connection status: '{strAux}'"); // 调试 strAux
            Console.WriteLine($"textBoxStatus Enabled: {myForm.textBoxStatus.Enabled}, Visible: {myForm.textBoxStatus.Visible}"); // 调试文本框状态

            // 线程安全更新 UI
            if (myForm.InvokeRequired)
            {
                myForm.Invoke(new Action(() => myForm.textBoxStatus.Text = strAux));
            }
            else
            {
                myForm.textBoxStatus.Text = strAux;
            }
            myForm.textBoxStatus.Text = strAux;

            if (connect2)
                lock (myForm.LockingVar)
                {
                    if (myForm.checkBoxMaster.Checked)
                        connect2 = true;
                    if (myForm.bIO && (myForm.comboBoxSynchronisation.SelectedIndex > 0))
                    {
                        strAux = String.Format(" - Encoder: X={0:F1} mm, Y={1:F1} mm", myForm.encoderAscanX * 1000.0, myForm.encoderAscanY * 1000.0);
                        myForm.textBoxStatus.Text += strAux;
                    }
                    if (myForm.checkBoxPulser.Checked)
                    {
                        size = myForm.dataSizeAscan - myForm.dataTimerAscan;
                        if (myForm.dataTimeAscan < myForm.sizeTime)
                        {
                            dThrougput = size * 1000;
                            dThrougput /= (myForm.sizeTime - myForm.dataTimeAscan);
                            dThrougput /= (1024 * 1024);
                        }
                        else
                            dThrougput = 0.0;
                        strAux = String.Format(" - Ascan: {0:F3} MB/s", dThrougput);
                        myForm.textBoxStatus.Text += strAux;
                    }
                    if (myForm.checkBoxPulser.Checked)
                    {
                        size = myForm.dataSizeCscan - myForm.dataTimerCscan;
                        if (myForm.dataTimeCscan < myForm.sizeTime)
                        {
                            dThrougput = size * 1000;
                            dThrougput /= (myForm.sizeTime - myForm.dataTimeCscan);
                            dThrougput /= (1024);
                        }
                        else
                            dThrougput = 0.0;
                        strAux = String.Format(" - Cscan: {0:F3} KB/s", dThrougput);
                        myForm.textBoxStatus.Text += strAux;
                    }
                    if (myForm.callbackCustomized)
                    {
                        myForm.callbackCustomized = false;
                        myForm.textBoxFileName.Text = myForm.pFileName;
                        //myForm.textBoxFileStatus.Text = "Cycles:" + myForm.cycleCount + "  Gain:" + myForm.gain;
                        if (myForm.gain >= 0)
                            strAux = String.Format("Cycles: {0} - Gain: {1:F1} dB - Start: {2:F1} us - Range: {3:F1} us", myForm.cycleCount, myForm.gain, myForm.start * 1e6, myForm.range * 1e6);
                        else
                            strAux = String.Format("Cycles: {0}", myForm.cycleCount);
                        myForm.textBoxFileStatus.Text = strAux;
                    }
                    iComError = swDevice.GetStreamError();
                    if (iComError > 0)
                        myForm.textBoxStatus.Text += " - ComError: " + iComError;
                    dwAscanLostCount = swDevice.GetLostCountAscan();
                    dwCscanLostCount = swDevice.GetLostCountCscan();
                    dwEncoderLostCount = swDevice.GetLostCountEncoder();
                    dwUSB3LostCount = swDevice.GetLostCountUSB3();
                    if ((dwAscanLostCount > 0) || (dwCscanLostCount > 0) || (dwEncoderLostCount > 0))
                    {
                        myForm.textBoxStatus.Text += " - DataLost:";
                        if (dwAscanLostCount > 0)
                        {
                            myForm.textBoxStatus.Text += " A=" + dwAscanLostCount;
                            dataLost = true;
                        }
                        if (dwCscanLostCount > 0)
                        {
                            myForm.textBoxStatus.Text += " C=" + dwCscanLostCount;
                            dataLost = true;
                        }
                        if (dwEncoderLostCount > 0)
                        {
                            myForm.textBoxStatus.Text += " E=" + dwEncoderLostCount;
                            dataLost = true;
                        }
                        if (dwUSB3LostCount > 0)
                        {
                            myForm.textBoxStatus.Text += " U=" + dwUSB3LostCount;
                            dataLost = true;
                        }
                    }
                }
            if ((connect2 != myForm.connect) || (pulser2 != myForm.pulser))
            {
                myForm.pulser = pulser2;
                myForm.checkBoxPulserEnable = false;
                myForm.checkBoxPulser.Checked = pulser2;
                myForm.checkBoxPulserEnable = true;
                myForm.connect = connect2;
                myForm.UpdateDialog();
            }
            myForm.dataTimerAscan = myForm.dataSizeAscan;
            myForm.dataTimerCscan = myForm.dataSizeCscan;
            myForm.dataTimeAscan = System.Environment.TickCount;
            myForm.dataTimeCscan = System.Environment.TickCount;
            if ((myForm != null) && (myForm.labelDataLost.Visible != dataLost))
            {
                myForm.labelDataLost.Visible = dataLost;
            }
        }
        private void FrmTestTool_Load(object sender, EventArgs e)
        {
            #region 配置参数初始化
            //str = Application.StartupPath + "\\Config.ini";                     //INI文件的物理地址
            //strIniFileName = System.IO.Path.GetFileNameWithoutExtension(str);   //获取INI文件的文件名
            //统一ini文件的读取方式 2024年4月29日
            if (File.Exists(PubCls.MyIniFile.FilePath))                                               //判断是否存在该INI文件
            {
                string a = PubCls.MyIniFile.ReadData("Option", "AxisUnitY", "", PubCls.MyIniFile.FilePath);
                txtSpeedX.Text = PubCls.MyIniFile.ReadData("Option", "SpeedX", "", PubCls.MyIniFile.FilePath);
                txtSpeedY.Text = PubCls.MyIniFile.ReadData("Option", "SpeedY", "", PubCls.MyIniFile.FilePath);
                txtSpeedZ.Text = PubCls.MyIniFile.ReadData("Option", "SpeedZ", "", PubCls.MyIniFile.FilePath);
                txtTravelX.Text = PubCls.MyIniFile.ReadData("Option", "TravelX", "", PubCls.MyIniFile.FilePath);
                txtTravelY.Text = PubCls.MyIniFile.ReadData("Option", "TravelY", "", PubCls.MyIniFile.FilePath);
                txtTravelZ.Text = PubCls.MyIniFile.ReadData("Option", "TravelZ", "", PubCls.MyIniFile.FilePath);

                txtAngleJogDelayTime.Text = PubCls.MyIniFile.ReadData("Option", "AngleJogDelayTime", "", PubCls.MyIniFile.FilePath);
                txtAnglePosTarget.Text = PubCls.MyIniFile.ReadData("Option", "AnglePosTarget", "", PubCls.MyIniFile.FilePath);
                txtAnglePosSpeed.Text = PubCls.MyIniFile.ReadData("Option", "AnglePosSpeed", "", PubCls.MyIniFile.FilePath);

                MyDevice.PosX.Offset = Convert.ToDouble(PubCls.MyIniFile.ReadData("Temp", "PosXOffset", "", PubCls.MyIniFile.FilePath));
                MyDevice.PosY.Offset = Convert.ToDouble(PubCls.MyIniFile.ReadData("Temp", "PosYOffset", "", PubCls.MyIniFile.FilePath));
                MyDevice.PosZ.Offset = Convert.ToDouble(PubCls.MyIniFile.ReadData("Temp", "PosZOffset", "", PubCls.MyIniFile.FilePath));
            }

            //查看是否存在此文件夹，如没有则新建
            if (!Directory.Exists(MyDevice.option.PhotoPath))
            {
                Directory.CreateDirectory(MyDevice.option.PhotoPath);
            }
            if (!Directory.Exists(MyDevice.option.CutPicPath))
            {
                Directory.CreateDirectory(MyDevice.option.CutPicPath);
            }
            #endregion

            #region ACS功能加载
            _ACSFormLoad();
            #endregion

            hslBtnCWX.Text = "\uF061" + " R";//->
            hslBtnCWX.Font = new System.Drawing.Font("FontAwesome", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            hslBtnCCWX.Text = "\uF060" + " R";//<-
            hslBtnCCWX.Font = new System.Drawing.Font("FontAwesome", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            hslBtnCWZ.Text = "\uF01A" + " Z";//V
            hslBtnCWZ.Font = new System.Drawing.Font("FontAwesome", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            hslBtnCCWZ.Text = "\uF01B" + " Z";//^
            hslBtnCCWZ.Font = new System.Drawing.Font("FontAwesome", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            hslbtnHallAll.Text = "\uF0C8";
            hslbtnHallAll.Font = new System.Drawing.Font("FontAwesome", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            OutBit0State = 0;
            OutBit1State = 0;
            OutBit2State = 0;

            comboBoxOEMType.SelectedIndex = 2;
        }

        private void FrmTestTool_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ACSFormClosed();//ACS退出
        }

          #region ACS部分
        #region ACS窗体事件
        private void tmrMonitor_Tick(object sender, EventArgs e)
        {
            if (m_bConnected)
            {
                try
                {
                    //for (int i = 0; i < 4; i++)
                    //{
                    //    m_lfFPos[i] = _ACS.GetFPosition((Axis)i);
                    //    m_lfFVEL[i] = (double)_ACS.ReadVariable("FVEL", ProgramBuffer.ACSC_NONE, i, i);
                    //}
                    //变更为按轴号读取，非所有均读，因为轴号可能是隔着的编号 2024年4月29日
                    m_lfFPos[Convert.ToInt16(MyDevice.config.AxisX)] = _ACS.GetFPosition(MyDevice.config.AxisX);
                    m_lfFVEL[Convert.ToInt16(MyDevice.config.AxisX)] = (double)_ACS.ReadVariable("FVEL", ProgramBuffer.ACSC_NONE, Convert.ToInt16(MyDevice.config.AxisX), Convert.ToInt16(MyDevice.config.AxisX));
                    m_lfFPos[Convert.ToInt16(MyDevice.config.AxisY)] = _ACS.GetFPosition(MyDevice.config.AxisY);
                    m_lfFVEL[Convert.ToInt16(MyDevice.config.AxisY)] = (double)_ACS.ReadVariable("FVEL", ProgramBuffer.ACSC_NONE, Convert.ToInt16(MyDevice.config.AxisY), Convert.ToInt16(MyDevice.config.AxisY));
                    m_lfFPos[Convert.ToInt16(MyDevice.config.AxisZ)] = _ACS.GetFPosition(MyDevice.config.AxisZ);
                    m_lfFVEL[Convert.ToInt16(MyDevice.config.AxisZ)] = (double)_ACS.ReadVariable("FVEL", ProgramBuffer.ACSC_NONE, Convert.ToInt16(MyDevice.config.AxisZ), Convert.ToInt16(MyDevice.config.AxisZ));

                    hslLabelFPOSX.TextValue = String.Format("{0:0.0000}", m_lfFPos[Convert.ToInt16(MyDevice.config.AxisX)]);
                    hslLabelFPOSY.TextValue = String.Format("{0:0.0000}", m_lfFPos[Convert.ToInt16(MyDevice.config.AxisY)]);
                    hslLabelFPOSZ.TextValue = String.Format("{0:0.0000}", m_lfFPos[Convert.ToInt16(MyDevice.config.AxisZ)]);
                    hslLabelFVELX.TextValue = String.Format("{0:0.0000}", m_lfFVEL[Convert.ToInt16(MyDevice.config.AxisX)]);
                    hslLabelFVELY.TextValue = String.Format("{0:0.0000}", m_lfFVEL[Convert.ToInt16(MyDevice.config.AxisY)]);
                    hslLabelFVELZ.TextValue = String.Format("{0:0.0000}", m_lfFVEL[Convert.ToInt16(MyDevice.config.AxisZ)]);
                    hslLabelFPOSX2.TextValue = String.Format("{0:0.0000}", m_lfFPos[Convert.ToInt16(MyDevice.config.AxisX)] - MyDevice.PosX.Offset);
                    hslLabelFPOSY2.TextValue = String.Format("{0:0.0000}", -m_lfFPos[Convert.ToInt16(MyDevice.config.AxisY)] + MyDevice.PosY.Offset);
                    hslLabelFPOSZ2.TextValue = String.Format("{0:0.0000}", m_lfFPos[Convert.ToInt16(MyDevice.config.AxisZ)] - MyDevice.PosZ.Offset);

                    // 内置程序执行状态 
                    m_nProgramState = _ACS.GetProgramState((ProgramBuffer)iBufferNo);

                    //限位状态
                    m_objReadVar = _ACS.ReadVariableAsVector("FAULT", ProgramBuffer.ACSC_NONE, 0, MyDevice.config.TotalAxis - 1, -1, -1);//从0-m_nTotalAxis - 1
                    if (m_objReadVar != null)
                    {
                        m_arrReadVector = m_objReadVar as Array;
                        if (m_arrReadVector != null)
                        {
                            //更新限位状态
                            UpdateLimitState(Convert.ToInt16(MyDevice.config.AxisX), (int)m_arrReadVector.GetValue(Convert.ToInt16(MyDevice.config.AxisX)));
                            UpdateLimitState(Convert.ToInt16(MyDevice.config.AxisY), (int)m_arrReadVector.GetValue(Convert.ToInt16(MyDevice.config.AxisY)));
                            UpdateLimitState(Convert.ToInt16(MyDevice.config.AxisZ), (int)m_arrReadVector.GetValue(Convert.ToInt16(MyDevice.config.AxisZ)));
                        }
                    }
                    if (_ACS.GetOutput(0, 8) == 1)
                    {
                        hslOut00.LanternBackground = Color.Red;
                    }
                    else
                    {
                        hslOut00.LanternBackground = Color.DarkGray;
                    }
                    if (_ACS.GetOutput(0, 9) == 1)
                    {
                        hslOut01.LanternBackground = Color.Red;
                        OutBit1State = 1;

                    }
                    else
                    {
                        hslOut01.LanternBackground = Color.DarkGray;
                        OutBit1State = 0;

                    }
                    UpdateAxisState(Convert.ToInt16(MyDevice.config.AxisX));
                    UpdateAxisState(Convert.ToInt16(MyDevice.config.AxisY));
                    UpdateAxisState(Convert.ToInt16(MyDevice.config.AxisZ));
                    BtnEnableState(m_bConnected, bEnable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void hslBtnClearError_Click(object sender, EventArgs e)
        {
            //清除错误
            Axis[] axes = { MyDevice.config.AxisX, MyDevice.config.AxisY, MyDevice.config.AxisZ };//X,Y,Z轴
            _ACS.FaultClear(MyDevice.config.AxisX);
            _ACS.FaultClear(MyDevice.config.AxisY);
            _ACS.FaultClear(MyDevice.config.AxisZ);
        }

        private void hslBtnSetZeroX_Click(object sender, EventArgs e)
        {
            //由于X、Y轴对应关系，所以X轴和Y轴的零点设置一致，不可以进行绝对零点设置，否则会造成两个轴角度的混乱
            MyDevice.PosX.Offset = m_lfFPos[Convert.ToInt16(MyDevice.config.AxisX)];
            PubCls.MyIniFile.WriteData("Temp", "PosXOffset", MyDevice.PosX.Offset.ToString(), PubCls.MyIniFile.FilePath);
            MyDevice.PosY.Offset = MyDevice.PosX.Offset;
            PubCls.MyIniFile.WriteData("Temp", "PosYOffset", MyDevice.PosY.Offset.ToString(), PubCls.MyIniFile.FilePath);
            //偏差值写入Dbuffer中，用于坐标的变换
            _ACS.WriteVariable(MyDevice.PosX.Offset, "PosXOffset", ProgramBuffer.ACSC_NONE);
        }

        private void hslBtnSetZeroY_Click(object sender, EventArgs e)
        {
            MyDevice.PosY.Offset = m_lfFPos[Convert.ToInt16(MyDevice.config.AxisY)];
            PubCls.MyIniFile.WriteData("Temp", "PosYOffset", MyDevice.PosY.Offset.ToString(), PubCls.MyIniFile.FilePath);
        }

        private void hslBtnSetZeroZ_Click(object sender, EventArgs e)
        {
            //MyDevice.PosZ.Offset = m_lfFPos[Convert.ToInt16(MyDevice.config.AxisZ)];
            //PubCls.MyIniFile.WriteData("Temp", "PosZOffset", MyDevice.PosZ.Offset.ToString(), PubCls.MyIniFile.FilePath);
            //绝对清零
            if (!m_bConnected)
            {
                MessageBox.Show("请先连接控制器!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _ACS.SetFPosition(MyDevice.config.AxisZ, 0);
        }

        private void hslBtnSetZeroAll_Click(object sender, EventArgs e)
        {
            if (!m_bConnected)
            {
                MessageBox.Show("请先连接控制器!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _ACS.SetFPosition(MyDevice.config.AxisX, 0);
            // Asynchronous set feedback position value of 0 to axis 0
            ACSC_WAITBLOCK wbX = _ACS.SetFPositionAsync(MyDevice.config.AxisX, 0);
            _ACS.SetFPosition(MyDevice.config.AxisY, 0);
            // Asynchronous set feedback position value of 0 to axis 0
            ACSC_WAITBLOCK wbY = _ACS.SetFPositionAsync(MyDevice.config.AxisY, 0);
            _ACS.SetFPosition(MyDevice.config.AxisZ, 0);
            // Asynchronous set feedback position value of 0 to axis 0
            ACSC_WAITBLOCK wbZ = _ACS.SetFPositionAsync(MyDevice.config.AxisZ, 0);
        }

        private void hslBtnEmg_Click(object sender, EventArgs e)
        {
            if (!m_bConnected)
            {
                MessageBox.Show("尚未连接控制器", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //终止所有当前执行的运动
            _ACS.KillAll();
            //停止所有在指定缓冲区中执行ACSPL+程序。
            _ACS.StopBuffer(ProgramBuffer.ACSC_NONE);
        }

        private void hslBtnEnableX_Click(object sender, EventArgs e)
        {
            int timeout = 1000;
            if (hslBtnEnableX.Text == "使能")
            {
                _ACS.Enable(MyDevice.config.AxisX);
                _ACS.WaitMotorEnabled(MyDevice.config.AxisX, 1, timeout);
                hslBtnEnableX.Text = "关闭";
            }
            else
            {
                _ACS.Disable(MyDevice.config.AxisX);
                _ACS.WaitMotorEnabled(MyDevice.config.AxisX, 0, timeout);
                hslBtnEnableX.Text = "使能";
            }
        }

        private void hslBtnEnableY_Click(object sender, EventArgs e)
        {
            int timeout = 1000;
            if (hslBtnEnableY.Text == "使能")
            {
                _ACS.Enable(MyDevice.config.AxisY);
                _ACS.WaitMotorEnabled(MyDevice.config.AxisY, 1, timeout);
                hslBtnEnableY.Text = "关闭";
            }
            else
            {
                _ACS.Disable(MyDevice.config.AxisY);
                _ACS.WaitMotorEnabled(MyDevice.config.AxisY, 0, timeout);
                hslBtnEnableY.Text = "使能";
            }
        }

        private void hslBtnEnableZ_Click(object sender, EventArgs e)
        {
            int timeout = 1000;
            if (hslBtnEnableZ.Text == "使能")
            {
                _ACS.Enable(MyDevice.config.AxisZ);
                _ACS.WaitMotorEnabled(MyDevice.config.AxisZ, 1, timeout);
                hslBtnEnableZ.Text = "关闭";
            }
            else
            {
                _ACS.Disable(MyDevice.config.AxisZ);
                _ACS.WaitMotorEnabled(MyDevice.config.AxisZ, 0, timeout);
                hslBtnEnableZ.Text = "使能";
            }
        }

        private void hslBtnEanbleAll_Click(object sender, EventArgs e)
        {
            // create axes array, terminate with Axis.ACSC_NONE
            Axis[] axes = { MyDevice.config.AxisX, MyDevice.config.AxisY, MyDevice.config.AxisZ, Axis.ACSC_NONE };
            // Enable of axes 0 and 1
            _ACS.EnableM(axes);
            //BtnEnableState(m_bConnected, true);
        }

        private void hslBtnGoHomeX_Click(object sender, EventArgs e)
        {
            //DialogResult dr = MessageBox.Show("是否进行龙门回零位", "信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //if (dr == DialogResult.Yes)
            //{
            //	HX_GoGantry(AxisX);
            //}
            //else
            //{
            //	return;
            //}
            HX_GoHomimg(MyDevice.config.AxisX);
        }

        private void hslBtnGoHomeY_Click(object sender, EventArgs e)
        {
            HX_GoHomimg(MyDevice.config.AxisY);
        }

        private void hslBtnGoHomeZ_Click(object sender, EventArgs e)
        {
            HX_GoHomimg(MyDevice.config.AxisZ);
        }

        private void hslBtnConnect_Click(object sender, EventArgs e)
        {
            if (!m_bConnected)
            {
                try
                {
                    _ACS.OpenCommEthernet("10.0.0.100", 701);//  (default : 10.0.0.100 701)
                    //_ACS.OpenCommSimulator();
                    m_bConnected = true;
                    //MessageBox.Show("连接成功");
                    //BtnEnableState(m_bConnected, false);
                    tmrMonitor.Interval = 500;
                    tmrMonitor.Start();
                    //轴列表变量初始化
                    m_arrAxisList = new Axis[MyDevice.config.TotalAxis + 1];
                    for (int i = 0; i < MyDevice.config.TotalAxis; i++)
                    {
                        m_arrAxisList[i] = (Axis)i;
                    }
                    // Insert '-1' at the last
                    m_arrAxisList[MyDevice.config.TotalAxis] = Axis.ACSC_NONE;
                    //HX_AcsPowup();
                }
                catch (Exception comex)
                {
                    MessageBox.Show("初始化失败：" + comex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //System.Diagnostics.Debug.WriteLine("Connection fail" + comex.Message);
                    m_bConnected = false;
                    //BtnEnableState(m_bConnected, false);
                    return;
                }
            }
        }

        private void hslBtnGantryAdjust_Click(object sender, EventArgs e)
        {
            HX_AcsPowup();
        }

        private void hslBtnDisConnect_Click(object sender, EventArgs e)
        {
            if (m_bConnected) _ACS.CloseComm();
            tmrMonitor.Stop();
            hslBtnConnect.Enabled = true;
            m_bConnected = false;
        }

        private void txtSpeedX_Click(object sender, EventArgs e)
        {
            HslControls.Forms.FormHslDigitalInput formHslDigital = new HslControls.Forms.FormHslDigitalInput();
            formHslDigital.OnOk = m =>
            {
                txtSpeedX.Text = m;
                formHslDigital.Close();
            };
            formHslDigital.DigitalInput.EnableNegative = false;                // 禁用负号
            formHslDigital.DigitalInput.EnableSpot = true;                     // 可用小数点
            formHslDigital.DigitalInput.InputCheck = m =>                      // 添加验证100-200的范围
            {
                int value = int.Parse(m);
                if (value >= 0 && value <= 500) return true;
                return false;
            };
            formHslDigital.ShowDialog();
        }

        private void txtSpeedZ_Click(object sender, EventArgs e)
        {
            HslControls.Forms.FormHslDigitalInput formHslDigital = new HslControls.Forms.FormHslDigitalInput();
            formHslDigital.OnOk = m =>
            {
                txtSpeedZ.Text = m;
                formHslDigital.Close();
            };
            formHslDigital.DigitalInput.EnableNegative = false;                // 禁用负号
            formHslDigital.DigitalInput.EnableSpot = true;                     // 可用小数点
            formHslDigital.DigitalInput.InputCheck = m =>                      // 添加验证100-200的范围
            {
                int value = int.Parse(m);
                if (value >= 0 && value <= 500) return true;
                return false;
            };
            formHslDigital.ShowDialog();
        }

        private void txtTravelX_Click(object sender, EventArgs e)
        {
            HslControls.Forms.FormHslDigitalInput formHslDigital = new HslControls.Forms.FormHslDigitalInput();
            formHslDigital.OnOk = m =>
            {
                txtTravelX.Text = m;
                formHslDigital.Close();
            };
            formHslDigital.DigitalInput.EnableNegative = false;                // 禁用负号
            formHslDigital.DigitalInput.EnableSpot = true;                     // 可用小数点
            formHslDigital.DigitalInput.InputCheck = m =>                      // 添加验证100-200的范围
            {
                int value = int.Parse(m);
                if (value >= 0 && value <= 200) return true;
                return false;
            };
            formHslDigital.ShowDialog();
        }

        private void txtTravelZ_Click(object sender, EventArgs e)
        {
            HslControls.Forms.FormHslDigitalInput formHslDigital = new HslControls.Forms.FormHslDigitalInput();
            formHslDigital.OnOk = m =>
            {
                txtTravelZ.Text = m;
                formHslDigital.Close();
            };
            formHslDigital.DigitalInput.EnableNegative = false;                // 禁用负号
            formHslDigital.DigitalInput.EnableSpot = true;                     // 可用小数点
            formHslDigital.DigitalInput.InputCheck = m =>                      // 添加验证100-200的范围
            {
                int value = int.Parse(m);
                if (value >= 0 && value <= 200) return true;
                return false;
            };
            formHslDigital.ShowDialog();
        }

        private void hslBtnCWX_Click(object sender, EventArgs e)
        {
            if (MotionFlag1.Checked)
            {
                PTP_Move(MotionFlags.ACSC_NONE, Convert.ToInt16(MyDevice.config.AxisX), MyDevice.config.AxisX_Dir * (-Math.Abs(Convert.ToDouble(txtTravelX.Text))), Convert.ToDouble(txtSpeedX.Text));
            }
            if (MotionFlag2.Checked)
            {
                PTP_Move(MotionFlags.ACSC_AMF_RELATIVE, Convert.ToInt16(MyDevice.config.AxisX), MyDevice.config.AxisX_Dir * (-Math.Abs(Convert.ToDouble(txtTravelX.Text))), Convert.ToDouble(txtSpeedX.Text));
            }
        }

        private void hslBtnCCWX_Click(object sender, EventArgs e)
        {
            if (MotionFlag1.Checked)
            {
                PTP_Move(MotionFlags.ACSC_NONE, Convert.ToInt16(MyDevice.config.AxisX), MyDevice.config.AxisX_Dir * (Math.Abs(Convert.ToDouble(txtTravelX.Text))), Convert.ToDouble(txtSpeedX.Text));
            }
            if (MotionFlag2.Checked)
            {
                PTP_Move(MotionFlags.ACSC_AMF_RELATIVE, Convert.ToInt16(MyDevice.config.AxisX), MyDevice.config.AxisX_Dir * (Math.Abs(Convert.ToDouble(txtTravelX.Text))), Convert.ToDouble(txtSpeedX.Text));
            }
        }

        private void hslBtnCWZ_Click(object sender, EventArgs e)
        {
            if (MotionFlag1.Checked)
            {
                PTP_Move(MotionFlags.ACSC_NONE, Convert.ToInt16(MyDevice.config.AxisZ), MyDevice.config.AxisZ_Dir * Math.Abs(Convert.ToDouble(txtTravelZ.Text)), Convert.ToDouble(txtSpeedZ.Text));
            }
            if (MotionFlag2.Checked)
            {
                PTP_Move(MotionFlags.ACSC_AMF_RELATIVE, Convert.ToInt16(MyDevice.config.AxisZ), MyDevice.config.AxisZ_Dir * Math.Abs(Convert.ToDouble(txtTravelZ.Text)), Convert.ToDouble(txtSpeedZ.Text));
            }
        }

        private void hslBtnCCWZ_Click(object sender, EventArgs e)
        {
            if (MotionFlag1.Checked)
            {
                PTP_Move(MotionFlags.ACSC_NONE, Convert.ToInt16(MyDevice.config.AxisZ), MyDevice.config.AxisZ_Dir * (-Math.Abs(Convert.ToDouble(txtTravelZ.Text))), Convert.ToDouble(txtSpeedZ.Text));
            }
            if (MotionFlag2.Checked)
            {
                PTP_Move(MotionFlags.ACSC_AMF_RELATIVE, Convert.ToInt16(MyDevice.config.AxisZ), MyDevice.config.AxisZ_Dir * (-Math.Abs(Convert.ToDouble(txtTravelZ.Text))), Convert.ToDouble(txtSpeedZ.Text));
            }
        }

        private void hslbtnHallAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_arrAxisList != null) _ACS.HaltM(m_arrAxisList);
                _ACS.StopBuffer(ProgramBuffer.ACSC_BUFFER_3);
                _ACS.StopBuffer(ProgramBuffer.ACSC_BUFFER_4);
                _ACS.StopBuffer(ProgramBuffer.ACSC_NONE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSpeedX_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(PubCls.MyIniFile.FilePath))                                          //判断是否存在该INI文件
            {
                PubCls.MyIniFile.WriteData("Option", "SpeedX", txtSpeedX.Text, PubCls.MyIniFile.FilePath);
            }
            else
            {
                MyLogNetFile.logNet.WriteWarn("配置保存时异常");
                MessageBox.Show("你所要修改的设置文件不存在，请确认后再进行修改操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void txtSpeedY_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(PubCls.MyIniFile.FilePath))                                          //判断是否存在该INI文件
            {
                PubCls.MyIniFile.WriteData("Option", "SpeedY", txtSpeedY.Text, PubCls.MyIniFile.FilePath);
            }
            else
            {
                MyLogNetFile.logNet.WriteWarn("配置保存时异常");
                MessageBox.Show("你所要修改的设置文件不存在，请确认后再进行修改操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSpeedZ_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(PubCls.MyIniFile.FilePath))                                          //判断是否存在该INI文件
            {
                PubCls.MyIniFile.WriteData("Option", "SpeedZ", txtSpeedZ.Text, PubCls.MyIniFile.FilePath);
            }
            else
            {
                MyLogNetFile.logNet.WriteWarn("配置保存时异常");
                MessageBox.Show("你所要修改的设置文件不存在，请确认后再进行修改操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtTravelX_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(PubCls.MyIniFile.FilePath))                                          //判断是否存在该INI文件
            {
                PubCls.MyIniFile.WriteData("Option", "TravelX", txtTravelX.Text, PubCls.MyIniFile.FilePath);
            }
            else
            {
                MyLogNetFile.logNet.WriteWarn("配置保存时异常");
                MessageBox.Show("你所要修改的设置文件不存在，请确认后再进行修改操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtTravelY_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(PubCls.MyIniFile.FilePath))                                          //判断是否存在该INI文件
            {
                PubCls.MyIniFile.WriteData("Option", "TravelY", txtTravelY.Text, PubCls.MyIniFile.FilePath);
            }
            else
            {
                MyLogNetFile.logNet.WriteWarn("配置保存时异常");
                MessageBox.Show("你所要修改的设置文件不存在，请确认后再进行修改操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtTravelZ_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(PubCls.MyIniFile.FilePath))                                          //判断是否存在该INI文件
            {
                PubCls.MyIniFile.WriteData("Option", "TravelZ", txtTravelZ.Text, PubCls.MyIniFile.FilePath);
            }
            else
            {
                MyLogNetFile.logNet.WriteWarn("配置保存时异常");
                MessageBox.Show("你所要修改的设置文件不存在，请确认后再进行修改操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MotionFlag1_CheckedChanged(object sender, EventArgs e)
        {
            b_motionFlags = MotionFlags.ACSC_NONE;
        }

        private void MotionFlag2_CheckedChanged(object sender, EventArgs e)
        {
            b_motionFlags = MotionFlags.ACSC_AMF_RELATIVE;
        }

        private void hslBtnCWX_MouseDown(object sender, MouseEventArgs e)
        {
            double lfVelocity = 0.0f;
            try
            {
                if (MotionJOG.Checked)
                {
                    lfVelocity = Convert.ToDouble(txtSpeedX.Text.Trim());
                    //if (lfVelocity < 0) lfVelocity = lfVelocity * (MyDevice.config.AxisX_Dir);
                    _ACS.Jog(MotionFlags.ACSC_AMF_VELOCITY, MyDevice.config.AxisX, lfVelocity * (-MyDevice.config.AxisX_Dir));
                }
                else
                {
                    //_ACS.Jog(0, AxisX, (double)GlobalDirection.ACSC_POSITIVE_DIRECTION);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void hslBtnCWX_MouseUp(object sender, MouseEventArgs e)
        {
            if (MotionJOG.Checked)
            {
                _ACS.Halt(MyDevice.config.AxisX);
            }
        }

        private void hslBtnCCWX_MouseDown(object sender, MouseEventArgs e)
        {
            double lfVelocity = 0.0f;
            try
            {
                if (MotionJOG.Checked)
                {
                    lfVelocity = Convert.ToDouble(txtSpeedX.Text.Trim());
                    //if (lfVelocity > 0) lfVelocity = lfVelocity * (MyDevice.config.AxisX_Dir);     // Negative direction : Using - (minus) velocity

                    _ACS.Jog(MotionFlags.ACSC_AMF_VELOCITY, MyDevice.config.AxisX, lfVelocity * (MyDevice.config.AxisX_Dir));
                }
                else
                {
                    //_ACS.Jog(0, AxisX, (double)GlobalDirection.ACSC_NEGATIVE_DIRECTION);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void hslBtnCCWX_MouseUp(object sender, MouseEventArgs e)
        {
            if (MotionJOG.Checked)
            {
                _ACS.Halt(MyDevice.config.AxisX);
            }
        }

        private void hslBtnCWZ_MouseDown(object sender, MouseEventArgs e)
        {
            double lfVelocity = 0.0f;
            try
            {
                if (MotionJOG.Checked)
                {
                    lfVelocity = Convert.ToDouble(txtSpeedZ.Text.Trim());
                    //if (lfVelocity < 0) lfVelocity = lfVelocity * (-1);
                    //if (lfVelocity > 0) lfVelocity = lfVelocity * (MyDevice.config.AxisZ_Dir);     // Negative direction : Using - (minus) velocity

                    _ACS.Jog(MotionFlags.ACSC_AMF_VELOCITY, MyDevice.config.AxisZ, lfVelocity * (MyDevice.config.AxisZ_Dir));
                }
                else
                {
                    //_ACS.Jog(0, AxisZ, (double)GlobalDirection.ACSC_POSITIVE_DIRECTION);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void hslBtnCWZ_MouseUp(object sender, MouseEventArgs e)
        {
            if (MotionJOG.Checked)
            {
                _ACS.Halt(MyDevice.config.AxisZ);
            }
        }

        private void hslBtnCCWZ_MouseUp(object sender, MouseEventArgs e)
        {
            if (MotionJOG.Checked)
            {
                _ACS.Halt(MyDevice.config.AxisZ);
            }
        }

        private void hslBtnCCWZ_MouseDown(object sender, MouseEventArgs e)
        {
            double lfVelocity = 0.0f;
            try
            {
                if (MotionJOG.Checked)
                {
                    lfVelocity = Convert.ToDouble(txtSpeedZ.Text.Trim());
                    //if (lfVelocity > 0) lfVelocity = lfVelocity * (-1);     // Negative direction : Using - (minus) velocity
                    //if (lfVelocity < 0) lfVelocity = lfVelocity * (MyDevice.config.AxisZ_Dir);
                    _ACS.Jog(MotionFlags.ACSC_AMF_VELOCITY, MyDevice.config.AxisZ, lfVelocity * (-MyDevice.config.AxisZ_Dir));
                }
                else
                {
                    //_ACS.Jog(0, AxisZ, (double)GlobalDirection.ACSC_NEGATIVE_DIRECTION);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void hslBtnLock_Click(object sender, EventArgs e)
        {
            try
            {
                // 读取当前离合器状态
                int currentState = _ACS.GetOutput(0, 9);

                if (currentState == 1) // 离合器当前是接合状态
                {
                    // 断开离合器
                    _ACS.SetOutput(0, 9, 0);
                    hslBtnLock.Text = "断开";
                    //OutBit1State = 0;

                    // 可选：添加状态提示
                    //MessageBox.Show("离合器已断开，可单独控制主轴");
                }
                else // 离合器当前是断开状态
                {
                    // 接合离合器
                    _ACS.SetOutput(0, 9, 1);
                    hslBtnLock.Text = "接合";
                    //OutBit1State = 1;

                    // 可选：添加状态提示
                    //MessageBox.Show("离合器已接合，主辅轴同步运动");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"控制离合器时出错: {ex.Message}");
            }
        }
        #endregion

        #region ACS函数
        public int HX_GoHomimg(Axis sAxis)
        {
            try
            {
                int bufnum = -1;//程序编号
                                //buffer 6 Y轴回零
                                //buffer 7 Z轴回零
                if (sAxis == MyDevice.config.AxisX) bufnum = 5;
                if (sAxis == MyDevice.config.AxisY) bufnum = 6;
                if (sAxis == MyDevice.config.AxisZ) bufnum = 7;
                _ACS.RunBuffer((ProgramBuffer)bufnum, null);
                //Delay(1000);

                //检索程序缓冲区的当前状态
                ProgramStates pstate;
                //ACSC_PST_COMPILED – a program in the specified buffer is compiled
                //ACSC_PST_RUN – a program in the specified buffer is running
                //ACSC_PST_AUTO – an auto routine in the specified buffer is running
                //ACSC_PST_DEBUG – a program in the specified buffer is executed in debug mode, i.e.breakpoints are active
                //ACSC_PST_SUSPEND – a program in the specified buffer is suspended after the step execution or due to breakpoint in debug mode
                do
                {
                    pstate = _ACS.GetProgramState((ProgramBuffer)bufnum);
                } while (pstate == ProgramStates.ACSC_PST_RUN);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ACS控制器可能没有设置相关程序", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return 0;
        }
        public int HX_GoGantry(Axis sAxis)
        {
            int bufnum = -1;//程序编号
            if (HX_CheckGantryFlag() == 1)
            {
                return 1;
            }
            if (sAxis == MyDevice.config.AxisX) bufnum = 5;
            _ACS.StopBuffer((ProgramBuffer)bufnum);
            _ACS.RunBuffer((ProgramBuffer)bufnum, null);
            //Delay(1000);
            //检索程序缓冲区的当前状态
            ProgramStates pstate;
            //ACSC_PST_COMPILED – a program in the specified buffer is compiled
            //ACSC_PST_RUN – a program in the specified buffer is running
            //ACSC_PST_AUTO – an auto routine in the specified buffer is running
            //ACSC_PST_DEBUG – a program in the specified buffer is executed in debug mode, i.e.breakpoints are active
            //ACSC_PST_SUSPEND – a program in the specified buffer is suspended after the step execution or due to breakpoint in debug mode
            do
            {
                pstate = _ACS.GetProgramState((ProgramBuffer)bufnum);
            } while (pstate == ProgramStates.ACSC_PST_RUN);
            return 0;
        }
        public int HX_CheckGantryFlag()
        {
            object var;
            //读龙门状态
            var = _ACS.ReadVariable("MFLAGS", ProgramBuffer.ACSC_NONE, 1, 1, 0, 0);
            if ((Convert.ToInt32(var) & 0x02000000) == 0)
            {
                //WriteToEdit("MFLAGS.25位是0");
                return 0;
            }
            else
            {
                //WriteToEdit("MFLAGS.25位是1");
                return 1;
            }
        }
        public int HX_AcsPowup()
        {
            //开始调偏,程序地址为4
            _ACS.RunBuffer(ProgramBuffer.ACSC_BUFFER_4, null);
            //延时
            //Delay(1000);
            //检索程序缓冲区的当前状态
            ProgramStates pstate;
            //ACSC_PST_COMPILED – a program in the specified buffer is compiled
            //ACSC_PST_RUN – a program in the specified buffer is running
            //ACSC_PST_AUTO – an auto routine in the specified buffer is running
            //ACSC_PST_DEBUG – a program in the specified buffer is executed in debug mode, i.e.breakpoints are active
            //ACSC_PST_SUSPEND – a program in the specified buffer is suspended after the step execution or due to breakpoint in debug mode
            do
            {
                pstate = _ACS.GetProgramState(ProgramBuffer.ACSC_BUFFER_4);
            } while (pstate == ProgramStates.ACSC_PST_RUN);
            //调偏结束，可以后续工作
            return 1;
        }
        // 更新轴限位状态
        private void UpdateLimitState(int axisNo, int fault)
        {
            if (axisNo < MAX_UI_LIMIT_CNT)
            {
                if ((fault & (int)SafetyControlMasks.ACSC_SAFETY_LL) != 0) m_lblLeftLimit[axisNo].LanternBackground = Color.Red; else m_lblLeftLimit[axisNo].LanternBackground = Color.Lime;
                if ((fault & (int)SafetyControlMasks.ACSC_SAFETY_RL) != 0) m_lblRightLimit[axisNo].LanternBackground = Color.Red; else m_lblRightLimit[axisNo].LanternBackground = Color.Lime;
            }
        }
        //更新轴状态
        private void UpdateAxisState(int axisNo)
        {
            if (axisNo < MAX_UI_LIMIT_CNT)
            {
                m_nMotorState = _ACS.GetMotorState((Axis)axisNo);
                SafetyControlMasks fault = _ACS.GetFault((Axis)axisNo);
                if ((m_nMotorState & MotorStates.ACSC_MST_ENABLE) != 0) { m_lblEnable[axisNo].LanternBackground = Color.Lime; bEnable[axisNo] = true; } else { m_lblEnable[axisNo].LanternBackground = Color.DarkGray; bEnable[axisNo] = false; }
                if ((m_nMotorState & MotorStates.ACSC_MST_INPOS) != 0) m_lblZeroPos[axisNo].LanternBackground = Color.Lime; else m_lblZeroPos[axisNo].LanternBackground = Color.DarkGray;
                if (SafetyControlMasks.ACSC_ALL != 0)
                {
                    m_lblFault[axisNo].LanternBackground = Color.Lime;
                }
                else
                {
                    //由于ACSC_ALL是定值，所以不会跳入此条，所以系统会有警告，此故障报警部分代码需要重新测试完善。//2024年4月11日
                    m_lblFault[axisNo].LanternBackground = Color.DarkGray;
                }
                if ((m_nMotorState & MotorStates.ACSC_MST_MOVE) != 0) m_lblMoving[axisNo].LanternBackground = Color.Lime; else m_lblMoving[axisNo].LanternBackground = Color.DarkGray;
                if ((m_nMotorState & MotorStates.ACSC_MST_ENABLE) != 0) { m_hslBtnEnable[axisNo].Text = "关闭"; } else { m_hslBtnEnable[axisNo].Text = "使能"; }
            }
        }
        //点对点运动控制
        private void PTP_Move(MotionFlags motionFlags, int axisNo, double pos, double vel)
        {
            //连接判断
            if (!m_bConnected)
            {
                MessageBox.Show("请先连接控制器!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //使能判断
            if (!bEnable[axisNo])
            {
                MessageBox.Show("轴未使能，当前操作不能进行!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //状态判断，如果有程序，需要结束
            //m_nMotorState = _ACS.GetMotorState((Axis)axisNo);
            //if ((m_nMotorState & MotorStates.ACSC_MST_MOVE)!=0)
            //{
            //	_ACS.Kill((Axis)axisNo);
            //	//Delay(1000);
            //}
            //设定速度、加速度、减速度
            _ACS.SetVelocity((Axis)axisNo, vel);
            //开始运动
            //_ACS.ToPoint(MotionFlags.ACSC_AMF_RELATIVE, (Axis)axisNo, pos);
            _ACS.ToPoint(motionFlags, (Axis)axisNo, pos);
        }
        //按钮使能状态
        private void BtnEnableState(bool bConnected, bool[] bEnable)
        {
            if (bConnected)
            {
                hslBtnConnect.Enabled = false;
                hslBtnEanbleAll.Enabled = true;
                hslBtnClearError.Enabled = true;
                hslBtnSetZeroAll.Enabled = true;
                hslBtnEmg.Enabled = true;
                hslBtnEnableX.Enabled = true;
                hslBtnEnableY.Enabled = true;
                hslBtnEnableZ.Enabled = true;
                hslBtnSetZeroX.Enabled = true;
                hslBtnSetZeroY.Enabled = true;
                hslBtnSetZeroZ.Enabled = true;
                hslbtnHallAll.Enabled = true;
                hslBtnLoadLine.Enabled = true;
                hslBtnLock.Enabled = true;
                hslbtnRunBuffer4.Enabled = true;
                hslBtnPauseLineScan.Enabled = true;
                hslBtnStopLineScan.Enabled = true;

                //按三个轴进行更新使能状态 2024年4月29日
                if (bEnable[Convert.ToInt16(MyDevice.config.AxisX)])
                {
                    hslBtnCWX.Enabled = true;
                    hslBtnCCWX.Enabled = true;
                    hslBtnGoHomeX.Enabled = true;
                }
                else
                {
                    hslBtnCWX.Enabled = false;
                    hslBtnCCWX.Enabled = false;
                    hslBtnGoHomeX.Enabled = false;
                }
                if (bEnable[Convert.ToInt16(MyDevice.config.AxisZ)])
                {
                    hslBtnCWZ.Enabled = true;
                    hslBtnCCWZ.Enabled = true;
                    hslBtnGoHomeZ.Enabled = true;
                }
                else
                {
                    hslBtnCWZ.Enabled = false;
                    hslBtnCCWZ.Enabled = false;
                    hslBtnGoHomeZ.Enabled = false;
                }
                if ((bEnable[Convert.ToInt16(MyDevice.config.AxisX)]) && (bEnable[Convert.ToInt16(MyDevice.config.AxisZ)]))
                {
                    hslBtnStartLineScan.Enabled = true;
                    hslBtnPauseLineScan.Enabled = true;
                    hslBtnStopLineScan.Enabled = true;
                }
                else
                {
                    hslBtnStartLineScan.Enabled = false;
                    hslBtnPauseLineScan.Enabled = false;
                    hslBtnStopLineScan.Enabled = false;
                }
                if ((bEnable[Convert.ToInt16(MyDevice.config.AxisX)]))
                {
                    hslbtnRunBuffer4.Enabled = true;
                }
                else
                {
                    hslbtnRunBuffer4.Enabled = false;
                }
            }
            else
            {
                hslBtnConnect.Enabled = true;
                hslBtnEanbleAll.Enabled = false;
                hslBtnClearError.Enabled = false;
                hslBtnSetZeroAll.Enabled = false;
                hslBtnEmg.Enabled = false;
                hslBtnCWX.Enabled = false;
                hslBtnCCWX.Enabled = false;
                //hslBtnCWY.Enabled = false;
                //hslBtnCCWY.Enabled = false;
                hslBtnCWZ.Enabled = false;
                hslBtnCCWZ.Enabled = false;
                hslbtnHallAll.Enabled = false;
                hslBtnEnableX.Enabled = false;
                hslBtnEnableY.Enabled = false;
                hslBtnEnableZ.Enabled = false;
                hslBtnSetZeroX.Enabled = false;
                hslBtnSetZeroY.Enabled = false;
                hslBtnSetZeroZ.Enabled = false;
                hslBtnGoHomeX.Enabled = false;
                hslBtnGoHomeY.Enabled = false;
                hslBtnGoHomeZ.Enabled = false;
                hslBtnLock.Enabled = false;
                hslbtnRunBuffer4.Enabled = false;
                hslBtnLoadLine.Enabled = false;
                hslBtnStartLineScan.Enabled = false;
                hslBtnPauseLineScan.Enabled = false;
                hslBtnStopLineScan.Enabled = false;
            }
        }

        #region 角度定位
        private void txtAngleJogDelayTime_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(PubCls.MyIniFile.FilePath))                                          //判断是否存在该INI文件
            {
                PubCls.MyIniFile.WriteData("Option", "AngleJogDelayTime", txtAngleJogDelayTime.Text, PubCls.MyIniFile.FilePath);
            }
            else
            {
                MyLogNetFile.logNet.WriteWarn("配置保存时异常");
                MessageBox.Show("你所要修改的设置文件不存在，请确认后再进行修改操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtAnglePosTarget_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(PubCls.MyIniFile.FilePath))                                          //判断是否存在该INI文件
            {
                PubCls.MyIniFile.WriteData("Option", "AnglePosTarget", txtAnglePosTarget.Text, PubCls.MyIniFile.FilePath);
            }
            else
            {
                MyLogNetFile.logNet.WriteWarn("配置保存时异常");
                MessageBox.Show("你所要修改的设置文件不存在，请确认后再进行修改操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtAnglePosSpeed_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(PubCls.MyIniFile.FilePath))                                          //判断是否存在该INI文件
            {
                PubCls.MyIniFile.WriteData("Option", "AnglePosSpeed", txtAnglePosSpeed.Text, PubCls.MyIniFile.FilePath);
            }
            else
            {
                MyLogNetFile.logNet.WriteWarn("配置保存时异常");
                MessageBox.Show("你所要修改的设置文件不存在，请确认后再进行修改操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void hslbtnRunBuffer4_Click(object sender, EventArgs e)
        {
            ProgramStates ProState3 = _ACS.GetProgramState(ProgramBuffer.ACSC_BUFFER_3);
            if ((ProState3 & ProgramStates.ACSC_PST_RUN) != 0)
            {
                MessageBox.Show("程序3正在运行，请先停止程序后再进行操作！");
                return;
            }
            //写入参数
            double AngleJogDelayTime = Convert.ToDouble(txtAngleJogDelayTime.Text);
            double AnglePosTarget = Convert.ToDouble(txtAnglePosTarget.Text);
            double AnglePosSpeed = Convert.ToDouble(txtAnglePosSpeed.Text);

            _ACS.WriteVariable(AngleJogDelayTime, "AngleJogDelayTime", ProgramBuffer.ACSC_NONE);
            _ACS.WriteVariable(AnglePosTarget, "AnglePosTarget", ProgramBuffer.ACSC_NONE);
            _ACS.WriteVariable(AnglePosSpeed, "AnglePosSpeed", ProgramBuffer.ACSC_NONE);
            //执行
            ProgramStates ProState = _ACS.GetProgramState(ProgramBuffer.ACSC_BUFFER_4);
            if ((ProState & ProgramStates.ACSC_PST_RUN) != 0)
            {
                MessageBox.Show("程序正在运行，请先停止程序后再进行操作！");
            }
            else if ((ProState & ProgramStates.ACSC_PST_COMPILED) != 0)
            {
                _ACS.RunBuffer(ProgramBuffer.ACSC_BUFFER_4, null);
            }
            else
            {
                MessageBox.Show("程序未编译或为空，请先编译程序或下载程序后再进行操作！");
            }
        }
        #endregion

        #region 自动线性扫描
        private void btnBrowseLineFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "txt|*.txt";
            if (open.ShowDialog() == DialogResult.OK)
            {
                txtCoordinateLinePath.Text = open.FileName;
            }
        }

        private void hslBtnLoadLine_Click(object sender, EventArgs e)
        {
            if (IsPathValid(txtCoordinateLinePath.Text))
            {
                //方法一、从文本文件里读取数据，并初始化数组
                int MyMatrixRowsCount = File.ReadAllLines(txtCoordinateLinePath.Text).Length;
                //定义二维数组，用来做为坐标参数,第一列为X坐标，第二列为Y坐标，第三列为Z坐标
                double[,] newMatrix = new double[MyMatrixRowsCount, 3];
                int n = int.Parse(txtNumInsertPoints.Text);
                using (StreamReader reader = new StreamReader(txtCoordinateLinePath.Text))
                {
                    string line;
                    int row = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        if (values.Length == 3)
                        {
                            newMatrix[row, 0] = double.Parse(values[0]);
                            newMatrix[row, 1] = double.Parse(values[1]);
                            newMatrix[row, 2] = double.Parse(values[2]);
                            row++;
                        }
                    }
                }
                // 创建新数组并计算行数
                //double[,] MyMatrixInsert = CreateInterpolatedMatrix(MyMatrix, n);
                //MyMatrixRowsCount = MyMatrixInsert.GetLength(0);
                _currentMotionMatrix = newMatrix;

            }
            else
            {
                MessageBox.Show("文件路径无效！", "路径错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void hslBtnStartLineScan_Click(object sender, EventArgs e)
        {
            // 防止重复点击
            lock (_scanLock)
            {
                if (_scanTask != null && !_scanTask.IsCompleted)
                {
                    MessageBox.Show("扫描任务正在运行中！");
                    return;
                }
            }

            try
            {
                // 立即禁用按钮，防止重复点击
                hslBtnStartLineScan.Enabled = false;

                // 安全措施：确保离合器合上（主辅轴同步）后再开始扫描
                if (_clutchService != null && _clutchService.IsClutchDisengaged)
                {
                    await _clutchService.EngageClutchAsync();
                    Console.WriteLine("扫描开始前：离合器已合上，确保主辅轴同步");
                }



                // 创建取消令牌
                _scanCancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = _scanCancellationTokenSource.Token;

                // 在后台线程执行扫描任务
                _scanTask = Task.Run(() => StartDiscreteScanAsync(cancellationToken), cancellationToken);

                // 等待任务完成并处理结果
                await _scanTask;

                MessageBox.Show("离散扫描完成！");
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("扫描已被用户取消");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"扫描过程中发生错误：{ex.Message}");
            }
            finally
            {
                try
                {
                    // 安全措施1：确保离合器合上（主辅轴同步）
                    if (_clutchService != null && _clutchService.IsClutchDisengaged)
                    {
                        await _clutchService.EngageClutchAsync();
                        //Console.WriteLine("扫描结束：离合器已自动合上");
                    }

                    // 安全措施2：确保扫描服务停止
                    //_scanService.StopService();

                    // 安全措施3：释放取消令牌资源
                    _scanCancellationTokenSource?.Dispose();
                    _scanCancellationTokenSource = null;
                }
                catch (Exception safeEx)
                {
                    // 安全操作中的异常不应影响主流程
                    Console.WriteLine($"安全清理过程中发生错误: {safeEx.Message}");
                }
                finally
                {
                    // 确保按钮重新启用（在UI线程上）
                    if (hslBtnStartLineScan.InvokeRequired)
                    {
                        hslBtnStartLineScan.Invoke(new Action(() => hslBtnStartLineScan.Enabled = true));
                    }
                    else
                    {
                        hslBtnStartLineScan.Enabled = true;
                    }
                }
            }
        }


        #region 停止扫描服务
        private async void StopScan()
        {
            try
            {
                // 禁用停止按钮防止重复点击
                hslBtnStopLineScan.Enabled = false;

                // 1. 取消扫描任务
                _scanCancellationTokenSource?.Cancel();

                // 2. 并行执行停止操作
                await Task.Run(() =>
                {
                    StopAllMotors();
                    EnsureClutchSafeState();
                });

                // 3. 停止扫描服务
                //_scanService?.StopService();

                // 4. 等待扫描任务完全停止
                if (_scanTask != null && !_scanTask.IsCompleted)
                {
                    await Task.WhenAny(_scanTask, Task.Delay(2000)); // 最多等待2秒
                }

                // 5. 更新UI
                UpdateUIAfterStop();

                MessageBox.Show("扫描已安全停止", "提示",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"停止扫描时发生错误: {ex.Message}", "错误",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                hslBtnStopLineScan.Enabled = true;
            }
        }
        private void UpdateUIAfterStop()
        {
            // 在UI线程上更新状态
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateUIAfterStop));
                return;
            }

            // 更新按钮状态
            hslBtnStartLineScan.Enabled = true;
            hslBtnStopLineScan.Enabled = false;

        }
        private void EnsureClutchSafeState()
        {
            try
            {
                // 根据你的安全策略决定离合器状态
                // 选项1：接合离合器（保持同步）
                _clutchService.EngageClutchAsync().Wait(1000); // 等待最多1秒

                // 或者选项2：断开离合器（避免机械冲击）
                // _clutchService.DisengageClutchAsync().Wait(1000);

                Console.WriteLine("离合器已设置为安全状态");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"设置离合器安全状态时发生错误: {ex.Message}");
            }
        }
        private void StopAllMotors()
        {
            try
            {
                // 停止所有运动轴
                _ACS.Kill((Axis)Convert.ToInt16(MyDevice.config.AxisX));
                _ACS.Kill((Axis)Convert.ToInt16(MyDevice.config.AxisZ));
                //_ACS.Kill((Axis)Convert.ToInt16(MyDevice.config.AxisY)); // 如果有角度轴

                // 可选：停止所有缓冲区程序
                _ACS.StopBuffer(ProgramBuffer.ACSC_NONE);

                Console.WriteLine("所有电机已紧急停止");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"停止电机时发生错误: {ex.Message}");
                // 继续执行其他停止操作，不抛出异常
            }
        }

        #endregion
        #region 断开离合操作
        public class ClutchControlService
        {
            private readonly Api _acs;
            private readonly int _clutchPort;
            private readonly int _clutchBit;

            public ClutchControlService(Api acs, int port = 0, int bit = 9)
            {
                _acs = acs;
                _clutchPort = port;
                _clutchBit = bit;
            }

            // 修正：基于硬件实际行为
            // 1 = 断开离合器，0 = 接合离合器

            // 离合器状态
            public bool IsClutchEngaged => _acs.GetOutput(_clutchPort, _clutchBit) == 0;  // 改为0
            public bool IsClutchDisengaged => !IsClutchEngaged;

            // 基础控制方法 - 修正版本
            public async Task EngageClutchAsync()
            {
                _acs.SetOutput(_clutchPort, _clutchBit, 0);  // 接合离合器 = 设置0
                await Task.Delay(200);
                //Console.WriteLine("离合器已接合（主辅轴同步）");
            }

            public async Task DisengageClutchAsync()
            {
                _acs.SetOutput(_clutchPort, _clutchBit, 1);  // 断开离合器 = 设置1
                await Task.Delay(200);
                //Console.WriteLine("离合器已断开（可单独控制主轴）");
            }

            // 安全控制：在离合器断开状态下执行操作（逻辑保持不变）
            public async Task ExecuteWithDisengagedClutchAsync(Func<Task> action)
            {
                bool wasEngaged = IsClutchEngaged;

                try
                {
                    if (wasEngaged)
                    {
                        await DisengageClutchAsync();  // 现在这个函数确实会断开离合器
                    }

                    await action();
                }
                finally
                {
                    // 恢复原始状态
                    if (wasEngaged)
                    {
                        await EngageClutchAsync();  // 现在这个函数确实会接合离合器
                    }
                }
            }
        }
        #endregion

        #region 离散扫描工作流
        private async Task StartDiscreteScanAsync(CancellationToken cancellationToken)
        {

            // 检查取消请求
            cancellationToken.ThrowIfCancellationRequested();

            // 获取对应轴的运动状态
            MotorStates motorState0 = _ACS.GetMotorState((Axis)Convert.ToInt16(MyDevice.config.AxisX));
            MotorStates motorState1 = _ACS.GetMotorState((Axis)Convert.ToInt16(MyDevice.config.AxisZ));

            // 检查是否有轴在运动（移除对RunBuffer4的依赖）
            bool isAnyAxisMoving = (motorState0 & MotorStates.ACSC_MST_MOVE) != 0 ||
                                    (motorState1 & MotorStates.ACSC_MST_MOVE) != 0;

            if (isAnyAxisMoving)
            {
                throw new InvalidOperationException("硬件占用，请先停止程序后再进行操作！");
            }

            // 获取矩阵行数
            int rowCount = _currentMotionMatrix.GetLength(0);

            // 超时设置
            TimeSpan movementTimeout = TimeSpan.FromSeconds(300);
            TimeSpan totalTimeout = TimeSpan.FromMinutes(60);

            DateTime totalStartTime = DateTime.Now;

            for (int i = 0; i < rowCount; i++)
            {
                // 检查取消请求
                cancellationToken.ThrowIfCancellationRequested();

                // 检查总超时
                if (DateTime.Now - totalStartTime > totalTimeout)
                {
                    throw new TimeoutException($"操作总超时（{totalTimeout.TotalSeconds}秒）");
                }

                // 执行三个轴的异步运动
                await MoveAllAxesAsync(i, movementTimeout, cancellationToken);

                // 通知扫描系统并等待完成
                await WaitForScanCompletionAsync(i, cancellationToken);
            }


        }
        #endregion

        #region 异步移动所有轴
        // 异步移动所有轴
        private async Task MoveAllAxesAsync(int pointIndex, TimeSpan timeout, CancellationToken cancellationToken)
        {
            // 同时启动三个轴的运动任务


            var tasks = new List<Task>();

            // 只有非0值才创建运动任务
            if (_currentMotionMatrix[pointIndex, 0] != 0)
            {
                var moveTask0 = PTP_MoveAsync(MotionFlags.ACSC_AMF_RELATIVE, Convert.ToInt16(MyDevice.config.AxisX), _currentMotionMatrix[pointIndex, 0], Convert.ToDouble(txtSpeedX.Text), cancellationToken);
                tasks.Add(moveTask0);
            }

            if (_currentMotionMatrix[pointIndex, 1] != 0)
            {
                var moveTask1 = PTP_MoveAsync(MotionFlags.ACSC_AMF_RELATIVE, Convert.ToInt16(MyDevice.config.AxisZ), _currentMotionMatrix[pointIndex, 1], Convert.ToDouble(txtSpeedZ.Text), cancellationToken);
                tasks.Add(moveTask1);
            }

            // 只有有任务时才等待
            if (tasks.Count > 0)
            {
                await Task.WhenAll(tasks);
            }

            // 调用封装的方法

            //await Task.Delay(200, cancellationToken);
            int timeoutMs = timeout.TotalMilliseconds > int.MaxValue ?
               int.MaxValue : (int)timeout.TotalMilliseconds;


            // 等待所有轴真正停止（带超时）
            // 直接在 MoveAllAxesAsync 中替换：
            _ACS.WaitMotionEnd((Axis)Convert.ToInt16(MyDevice.config.AxisX), timeoutMs);
            _ACS.WaitMotionEnd((Axis)Convert.ToInt16(MyDevice.config.AxisZ), timeoutMs);
            //开始控制相对夹角


            double targetAngleMovement = _currentMotionMatrix[pointIndex, 2];
  
            if (Math.Abs(targetAngleMovement) > 0.1) // 只有需要运动时才调整
            {
                await _clutchService.ExecuteWithDisengagedClutchAsync(async () =>
                {
                    bool something = _clutchService.IsClutchDisengaged;
                    
                    // 离合器已自动断开，辅轴保持不动
                    // 只运动主轴来调整相对角度
                    await PTP_MoveAsync(
                        MotionFlags.ACSC_AMF_RELATIVE,
                        Convert.ToInt16(MyDevice.config.AxisX),
                        targetAngleMovement,
                        Convert.ToDouble(txtAnglePosSpeed.Text),
                        cancellationToken
                    );

                    // 等待主轴运动完成
                    _ACS.WaitMotionEnd((Axis)Convert.ToInt16(MyDevice.config.AxisX), timeoutMs);


                    //Console.WriteLine($"相对角度已调整: {targetAngleMovement}°");
                });
            }
        }

        // 异步运动方法（需要您根据ACS SDK实现）
        private async Task PTP_MoveAsync(MotionFlags flags, int axis, double position, double speed, CancellationToken cancellationToken)
        {
            // 这里需要根据您的ACS SDK实现异步运动
            // 如果SDK没有提供异步方法，可以使用Task.Run包装
            await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                PTP_Move(flags, axis, position, speed);
            }, cancellationToken);
        }

        #endregion


        #region 等待扫描完成
        // 异步等待扫描完成
        private async Task WaitForScanCompletionAsync(int pointIndex, CancellationToken cancellationToken)
        {
            // 使用扫描服务请求扫描，并等待结果
            bool scanSuccess = await _scanService.RequestScanAsync(pointIndex, cancellationToken);

            if (!scanSuccess)
            {
                throw new InvalidOperationException($"点位 {pointIndex} 扫描失败");
            }

            // 扫描成功，继续执行
            //Console.WriteLine($"点位 {pointIndex} 扫描成功，继续下一个点位");
        }

        #endregion

        #region 建立完整的扫描系统
        //
        public interface IScanService
        {
            // 请求扫描
            Task<bool> RequestScanAsync(int pointIndex, CancellationToken cancellationToken = default);

            // 扫描状态事件
            event EventHandler<ScanCompletedEventArgs> ScanCompleted;
            event EventHandler<ScanFailedEventArgs> ScanFailed;

            // 服务状态
            bool IsScanning { get; }
            void StartService();
            void StopService();
        }


        //完成后广播的信号
        public class ScanCompletedEventArgs : EventArgs
        {
            public int PointIndex { get; set; }
            public bool Success { get; set; }
            public double Value { get; set; }

        }
        //失败后广播的信号
        public class ScanFailedEventArgs : EventArgs
        {
            public int PointIndex { get; set; }
            public string ErrorMessage { get; set; }
            public Exception Exception { get; set; }
        }

        // 扫描服务实现
        public class ChannelScanService : IScanService, IDisposable
        {
            // 使用 BlockingCollection 替代 Channel
            private readonly BlockingCollection<ScanRequest> _scanQueue;
            private readonly CancellationTokenSource _serviceCts;
            private Task _processingTask;
            private bool _isRunning;

            // 添加缺失的事件声明
            // 定义了成功失败两个事件
            public event EventHandler<ScanCompletedEventArgs> ScanCompleted;
            public event EventHandler<ScanFailedEventArgs> ScanFailed;
            public bool IsScanning => _isRunning;

            public ChannelScanService()
            {
                // 使用 BlockingCollection 作为替代
                _scanQueue = new BlockingCollection<ScanRequest>(new ConcurrentQueue<ScanRequest>(), 100);
                _serviceCts = new CancellationTokenSource();
            }

            public void StartService()
            {
                if (_isRunning) return;
        
                _isRunning = true;
                _processingTask = ProcessScanRequestsAsync(_serviceCts.Token);
            }

            public void StopService()
            {
                if (!_isRunning) return;

                _isRunning = false;
                _serviceCts.Cancel();
                _scanQueue.CompleteAdding();  // 停止队列
                // 可选：取消所有事件订阅
                // ScanCompleted = null;
                // ScanFailed = null;
            }

            //外部引用，可将一个任务加入扫描系统的轮询列表里
            public async Task<bool> RequestScanAsync(int pointIndex, CancellationToken cancellationToken = default)
            {
                var request = new ScanRequest
                {
                    PointIndex = pointIndex,
                    // 专门用于等待扫描结果

                    /// <summary>
                    /// 任务完成源 - 用于协调运动系统与扫描系统的同步
                    /// 
                    /// 工作机制：
                    /// 1. 运动系统创建 ScanRequest 并 await CompletionSource.Task 进入等待状态
                    /// 2. 扫描系统处理完成后调用 SetResult(true/false) 或 SetException(ex)
                    /// 3. 运动系统从等待中恢复，获取扫描结果并继续执行
                    /// 
                    /// 解除等待的条件：
                    /// ✅ CompletionSource.SetResult(true)  - 扫描成功，返回 true
                    /// ✅ CompletionSource.SetResult(false) - 扫描失败，返回 false  
                    /// ✅ CompletionSource.SetException(ex) - 扫描异常，抛出指定异常
                    /// ❌ 其他情况会一直等待（可能造成死锁）
                    /// 
                    /// 典型使用场景：
                    /// - 运动系统：await RequestScanAsync(pointIndex) 
                    /// - 扫描系统：处理完成后设置结果 request.CompletionSource.SetResult(success)
                    /// </summary>
                    CompletionSource = new TaskCompletionSource<bool>()
                };

                // 添加到队列（替代 Channel.WriteAsync）
                _scanQueue.Add(request, cancellationToken);

                return await request.CompletionSource.Task;
            }


            //建立后台轮询服务
            private async Task ProcessScanRequestsAsync(CancellationToken cancellationToken)
            {
                try
                {
                    // 使用异步方式处理队列
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        try
                        {
                            // 异步等待队列中的项目（带超时）
                            ScanRequest request = null;
                            if (_scanQueue.TryTake(out request, 50, cancellationToken)) // 100ms超时
                            {
                                await ProcessSingleRequestAsync(request, cancellationToken);
                            }
                            else
                            {
                                // 队列为空，短暂等待后继续检查
                                await Task.Delay(50, cancellationToken);
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            break;
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // 正常退出
                }
            }

            // 接收扫描任务并发配给实际的扫描函数
            private async Task ProcessSingleRequestAsync(ScanRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    // 实际的扫描功能
                    var result = await ExecuteScanAsync(request.PointIndex, cancellationToken);

                    request.CompletionSource.SetResult(result.Item1);

                    ScanCompleted?.Invoke(this, new ScanCompletedEventArgs
                    {
                        PointIndex = request.PointIndex,
                        Success = result.Item1,
                        Value = result.Item2
                    });
                }
                catch (Exception ex)
                {
                    request.CompletionSource.SetException(ex);
                    ScanFailed?.Invoke(this, new ScanFailedEventArgs
                    {
                        PointIndex = request.PointIndex,
                        ErrorMessage = ex.Message
                    });
                }
            }

            // 扫描业务逻辑 - 这里您实现具体的扫描硬件控制
            private async Task<(bool, double)> ExecuteScanAsync(int pointIndex, CancellationToken cancellationToken)
            {
                try
                {
                    // TODO: 实现您的扫描硬件控制逻辑
                    //Console.WriteLine($"开始扫描点位 {pointIndex}");
                    // 扫查间隔时间
                    // 模拟扫描过程
                    //await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

                    // 模拟扫描结果（实际中根据硬件返回）
                    bool scanSuccess = new Random().Next(0, 10) > -1; // 80%成功率

                    double value = Math.Sin(pointIndex);

                    //MessageBox.Show($"点位 {pointIndex} 扫描{(scanSuccess ? "成功" : "失败")}");


                    //Console.WriteLine($"点位 {pointIndex} 扫描{(scanSuccess ? "成功" : "失败")}");
                    return (scanSuccess, value);
                }
                catch (OperationCanceledException)
                {
                    //Console.WriteLine($"点位 {pointIndex} 扫描被取消");
                    throw;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"点位 {pointIndex} 扫描异常: {ex.Message}");
                    throw;
                }
            }

            public void Dispose()
            {
                StopService();
                _scanQueue?.Dispose();
                _serviceCts?.Dispose();
            }
        }
        
        // 扫描请求模型
        public class ScanRequest
        {
            public int PointIndex { get; set; }
            public TaskCompletionSource<bool> CompletionSource { get; set; }
            public CancellationToken CancellationToken { get; set; }
        }


        public class ScanResult
        {
            public int PointIndex { get; set; }
            public double Value { get; set; }
            public bool Success { get; set; }
        }
        #endregion


        #region 记录扫描数据并更新图表
        public class ScanResultRecorder
        {
            private readonly List<ScanResult> _scanResults = new List<ScanResult>();
            private readonly IScanService _scanService;
            private readonly Action<ScanResult> _updateChartAction;

            // 用于线程安全的锁
            private readonly object _lockObject = new object();

            public ScanResultRecorder(IScanService scanService, Action<ScanResult> updateChartAction)
            {
                _scanService = scanService;
                _updateChartAction = updateChartAction;

                // 订阅扫描事件
                _scanService.ScanCompleted += OnScanCompleted;
                _scanService.ScanFailed += OnScanFailed;
            }

            private void OnScanCompleted(object sender, ScanCompletedEventArgs e)
            {
                var result = new ScanResult
                {
                    PointIndex = e.PointIndex,
                    Value = e.Value, // 假设事件参数包含实际扫描值
                    Success = true,
                };

                ProcessResult(result);
            }

            private void OnScanFailed(object sender, ScanFailedEventArgs e)
            {
                var result = new ScanResult
                {
                    PointIndex = e.PointIndex,
                    Value = 0, // 失败记录为0
                    Success = false,
                };

                ProcessResult(result);
            }

            private void ProcessResult(ScanResult result)
            {
                lock (_lockObject)
                {
                    // 1. 记录到后端列表
                    _scanResults.Add(result);

                    //Console.WriteLine($"📊 记录扫描结果: 点位{result.PointIndex}, 数值={result.Value}, 状态={(result.Success ? "成功" : "失败")}");
                }

                // 2. 更新图表（在UI线程上）
                _updateChartAction?.Invoke(result);
            }

            // 数据查询方法
            public IReadOnlyList<ScanResult> GetAllResults()
            {
                lock (_lockObject)
                {
                    return _scanResults.AsReadOnly();
                }
            }

            public List<ScanResult> GetSuccessfulResults()
            {
                lock (_lockObject)
                {
                    return _scanResults.Where(r => r.Success).ToList();
                }
            }

            public List<ScanResult> GetFailedResults()
            {
                lock (_lockObject)
                {
                    return _scanResults.Where(r => !r.Success).ToList();
                }
            }

            public int TotalCount => _scanResults.Count;
            public int SuccessCount => _scanResults.Count(r => r.Success);
            public double SuccessRate => TotalCount > 0 ? (double)SuccessCount / TotalCount : 0;

            public void Clear()
            {
                lock (_lockObject)
                {
                    _scanResults.Clear();
                }
            }

            public void Dispose()
            {
                _scanService.ScanCompleted -= OnScanCompleted;
                _scanService.ScanFailed -= OnScanFailed;
            }
        }
        #endregion

        #region 更新图表方法
        private void UpdateChart(ScanResult result)
        {
            //转跳到ui线程
            if (chartScanResults.InvokeRequired)
            {
                chartScanResults.Invoke(new Action(() => UpdateChart(result)));
                return;
            }

            // 更新扫描数值趋势图
            var valueSeries = chartScanResults.Series["ScanValues"];
            if (valueSeries != null)
            {
                valueSeries.Points.AddXY(result.PointIndex, result.Value);
            }

        }
        #endregion




        private void hslBtnPauseLineScan_Click(object sender, EventArgs e)
        {
            ProgramStates ProState = _ACS.GetProgramState(ProgramBuffer.ACSC_BUFFER_3);
            if ((ProState & ProgramStates.ACSC_PST_RUN) != 0)
            {
                _ACS.SuspendBuffer(ProgramBuffer.ACSC_BUFFER_3);
            }

        }
        private void hslBtnStopLineScan1_Click(object sender, EventArgs e)
        {
            _scanCancellationTokenSource?.Cancel();
        }
        #endregion

        private void txtDouble_TextChanged(object sender, EventArgs e)
        {
            TextBox changedTextBox = sender as TextBox; // 获取被更改的TextBox
            IsValidDouble(changedTextBox);
        }
        private void txtInt_TextChanged(object sender, EventArgs e)
        {
            TextBox changedTextBox = sender as TextBox; // 获取被更改的TextBox
            IsValidInteger(changedTextBox);
        }
        private void _ACSFormLoad()
        {
            hslBtnConnect.Enabled = true;
            m_lblLeftLimit = new HslControls.HslLanternSimple[MAX_UI_LIMIT_CNT];//左限位控件数组
            m_lblLeftLimit[Convert.ToInt16(MyDevice.config.AxisX)] = lblLLX;
            m_lblLeftLimit[Convert.ToInt16(MyDevice.config.AxisY)] = lblLLY;
            m_lblLeftLimit[Convert.ToInt16(MyDevice.config.AxisZ)] = lblLLZ;
            m_lblRightLimit = new HslControls.HslLanternSimple[MAX_UI_LIMIT_CNT];//右限位控件数组
            m_lblRightLimit[Convert.ToInt16(MyDevice.config.AxisX)] = lblRLX;
            m_lblRightLimit[Convert.ToInt16(MyDevice.config.AxisY)] = lblRLY;
            m_lblRightLimit[Convert.ToInt16(MyDevice.config.AxisZ)] = lblRLZ;
            m_lblEnable = new HslControls.HslLanternSimple[MAX_UI_LIMIT_CNT];//使能控件数组
            m_lblEnable[Convert.ToInt16(MyDevice.config.AxisX)] = lblEnableX;
            m_lblEnable[Convert.ToInt16(MyDevice.config.AxisY)] = lblEnableY;
            m_lblEnable[Convert.ToInt16(MyDevice.config.AxisZ)] = lblEnableZ;
            m_lblZeroPos = new HslControls.HslLanternSimple[MAX_UI_LIMIT_CNT];//零点状态控件数组
            m_lblZeroPos[Convert.ToInt16(MyDevice.config.AxisX)] = lblZeroPosX;
            m_lblZeroPos[Convert.ToInt16(MyDevice.config.AxisY)] = lblZeroPosY;
            m_lblZeroPos[Convert.ToInt16(MyDevice.config.AxisZ)] = lblZeroPosZ;
            m_lblFault = new HslControls.HslLanternSimple[MAX_UI_LIMIT_CNT];//故障状态控件数组
            m_lblFault[Convert.ToInt16(MyDevice.config.AxisX)] = lblFaultX;
            m_lblFault[Convert.ToInt16(MyDevice.config.AxisY)] = lblFaultY;
            m_lblFault[Convert.ToInt16(MyDevice.config.AxisZ)] = lblFaultZ;
            m_lblMoving = new HslControls.HslLanternSimple[MAX_UI_LIMIT_CNT];//运动中状态控件数组
            m_lblMoving[Convert.ToInt16(MyDevice.config.AxisX)] = lblMovingX;
            m_lblMoving[Convert.ToInt16(MyDevice.config.AxisY)] = lblMovingY;
            m_lblMoving[Convert.ToInt16(MyDevice.config.AxisZ)] = lblMovingZ;
            m_hslBtnEnable = new HslControls.HslButton[MAX_UI_LIMIT_CNT];
            m_hslBtnEnable[Convert.ToInt16(MyDevice.config.AxisX)] = hslBtnEnableX;
            m_hslBtnEnable[Convert.ToInt16(MyDevice.config.AxisY)] = hslBtnEnableY;
            m_hslBtnEnable[Convert.ToInt16(MyDevice.config.AxisZ)] = hslBtnEnableZ;
            m_lfRPos = new double[8];
            m_lfFPos = new double[8];
            m_lfPE = new double[8];
            m_lfFVEL = new double[8];
            BtnEnableState(false, bEnable);
            MotionJOG.Checked = true;
        }

        private void hslButton1_Click(object sender, EventArgs e)
        {
            _ACS.OpenCommSimulator();
        }

        private void hslBtnMoveTo_Click(object sender, EventArgs e)
        {
            //运动到目标角度
            // 轴号
            short axis = Convert.ToInt16(MyDevice.config.AxisX);

            // 1. 当前工件角度
            double curPos = m_lfFPos[axis] - MyDevice.PosX.Offset;

            // ===== 目标角度合法性检测 =====
            string txt = txtMoveTo.Text.Trim();
            if (!double.TryParse(txt, out double target))
            {
                MessageBox.Show("目标角度必须是数字！");
                return;
            }

            // 根据机械实际范围自己改
            double MIN_ANGLE = -360.0;   // 允许最小值
            double MAX_ANGLE = 360.0;    // 允许最大值
            if (target < MIN_ANGLE || target > MAX_ANGLE)
            {
                MessageBox.Show($"目标角度超出允许范围 ({MIN_ANGLE}° ~ {MAX_ANGLE}°)！");
                return;
            }

            // 3. 相对运动量
            double delta = curPos - target;

            // 4. 速度
            if (!double.TryParse(txtSpeedX.Text, out double speed) || speed <= 0)
            {
                MessageBox.Show("速度必须是 > 0 的数字！");
                return;
            }

            // 5. 下发运动
            PTP_Move(MotionFlags.ACSC_AMF_RELATIVE,
                     axis,
                     MyDevice.config.AxisX_Dir * delta,
                     speed);

        }

        private async void hslbtnRunBuffer4double_Click(object sender, EventArgs e)
        {
            // 禁用按钮防止重复点击
            hslbtnRunBuffer4double.Enabled = false;

            try
            {
                // 检查程序3状态（在UI事件中保留）
                ProgramStates ProState3 = _ACS.GetProgramState(ProgramBuffer.ACSC_BUFFER_3);
                if ((ProState3 & ProgramStates.ACSC_PST_RUN) != 0)
                {
                    MessageBox.Show("程序3正在运行，请先停止程序后再进行操作！");
                    return;
                }

                // 从UI获取参数
                double angleJogDelayTime = Convert.ToDouble(txtAngleJogDelayTime.Text);
                double anglePosTarget = Convert.ToDouble(txtAnglePosTarget.Text);
                double anglePosSpeed = Convert.ToDouble(txtAnglePosSpeed.Text);

                // 调用封装的方法
                bool success = await RunBuffer4.WritePositionAndAdjustTwiceAsync(
                    _ACS, anglePosTarget, anglePosSpeed, angleJogDelayTime);

                if (success)
                {
                    //MessageBox.Show("连续调整执行完成！");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"执行失败: {ex.Message}");
            }
            finally
            {
                hslbtnRunBuffer4double.Enabled = true;
            }
        }



        public class RunBuffer4
        {
            //获取程序运行状态
            private static readonly object _lockObject = new object();
            private static bool _isContinuousAdjustmentRunning = false;

            public static bool IsContinuousAdjustmentRunning
            {
                get { lock (_lockObject) return _isContinuousAdjustmentRunning; }
                private set { lock (_lockObject) _isContinuousAdjustmentRunning = value; }
            }

            public static event Action<bool> OnContinuousAdjustmentCompleted;

            /// <summary>
            /// 写入定位位置并连续调整两次（提高精度）
            /// </summary>
            /// <param name="acs">ACS运动控制器实例</param>
            /// <param name="anglePosTarget">目标角度位置</param>
            /// <param name="anglePosSpeed">角度运动速度</param>
            /// <param name="angleJogDelayTime">延迟时间</param>
            /// <returns>执行是否成功</returns>
            public static async Task<bool> WritePositionAndAdjustTwiceAsync(
                ACS.SPiiPlusNET.Api acs,  // 直接使用实际类型
                double anglePosTarget,
                double anglePosSpeed,
                double angleJogDelayTime = 0)
            {
                if (IsContinuousAdjustmentRunning)
                {
                    throw new InvalidOperationException("连续调整任务正在执行中，请等待完成");
                }

                try
                {
                    IsContinuousAdjustmentRunning = true;

                    // 写入参数
                    await Task.Run(() =>
                    {
                        acs.WriteVariable(angleJogDelayTime, "AngleJogDelayTime", ProgramBuffer.ACSC_NONE);
                        acs.WriteVariable(anglePosTarget, "AnglePosTarget", ProgramBuffer.ACSC_NONE);
                        acs.WriteVariable(anglePosSpeed, "AnglePosSpeed", ProgramBuffer.ACSC_NONE);
                    });

                    // 检查程序4状态并执行连续调整
                    ProgramStates proState4 = await Task.Run(() => acs.GetProgramState(ProgramBuffer.ACSC_BUFFER_4));
                    if ((proState4 & ProgramStates.ACSC_PST_RUN) != 0)
                    {
                        throw new InvalidOperationException("程序4正在运行，请先停止程序后再进行操作！");
                    }
                    else if ((proState4 & ProgramStates.ACSC_PST_COMPILED) != 0)
                    {
                        // 第一次调整
                        await Task.Run(() => acs.RunBuffer(ProgramBuffer.ACSC_BUFFER_4, null));
                        await WaitForBufferCompletionAsync(acs, ProgramBuffer.ACSC_BUFFER_4);

                        // 第二次调整
                        await Task.Run(() => acs.RunBuffer(ProgramBuffer.ACSC_BUFFER_4, null));
                        await WaitForBufferCompletionAsync(acs, ProgramBuffer.ACSC_BUFFER_4);

                        return true;
                    }
                    else
                    {
                        throw new InvalidOperationException("程序4未编译或为空，请先编译程序或下载程序后再进行操作！");
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    IsContinuousAdjustmentRunning = false;
                    OnContinuousAdjustmentCompleted?.Invoke(true);
                }
            }

            private static async Task WaitForBufferCompletionAsync(ACS.SPiiPlusNET.Api acs, ProgramBuffer buffer)
            {
                int maxWaitTime = 60000;
                int checkInterval = 50;

                for (int i = 0; i < maxWaitTime / checkInterval; i++)
                {
                    ProgramStates state = await Task.Run(() => acs.GetProgramState(buffer));
                    if ((state & ProgramStates.ACSC_PST_RUN) == 0)
                    {
                        return;
                    }
                    await Task.Delay(checkInterval);
                }
                throw new TimeoutException($"缓冲区 {buffer} 执行超时");
            }
        }

        private void hslbtnClearResults_Click(object sender, EventArgs e)
        {
            ClearScanResultsAndChart();
        }


        private void ClearScanResultsAndChart()
        {
            // 1. 清空数据记录
            _resultRecorder.Clear();

            // 2. 清空图表（需要在UI线程）
            if (chartScanResults.InvokeRequired)
            {
                chartScanResults.Invoke(new Action(ClearScanResultsAndChart));
                return;
            }

            // 清空图表数据点
            foreach (var series in chartScanResults.Series)
            {
                series.Points.Clear();
            }

            Console.WriteLine("扫描数据和图表已清空");
        }

        private void hslPanelHead2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _ACSFormClosed()
        {
            _ACS.CloseComm();
        }

        #endregion

        #endregion

        #region 常用公共函数部分
        public static bool Delay(int delayTime)
        {
            DateTime now = DateTime.Now;
            int s;
            do
            {
                TimeSpan spand = DateTime.Now - now;
                s = spand.Milliseconds;
                Application.DoEvents();
            }
            while (s < delayTime);
            return true;
        }

        private static bool IsPathValid(string path)
        {
            // 检查路径是否为空或格式是否不正确
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            // 检查路径是否存在
            if (!File.Exists(path) && !Directory.Exists(Path.GetDirectoryName(path)))
            {
                return false; // 文件或目录不存在
            }

            // 额外的格式检查（根据规则需要调整）
            try
            {
                var fileInfo = new FileInfo(path);
                return true; // 路径有效
            }
            catch (Exception)
            {
                return false; // 发生异常，路径无效
            }
        }
        
        // 判断输入是否为数字和小数点
        private bool IsValidDouble(System.Windows.Forms.TextBox sender)
        {
            // 使用正则表达式进行匹配：只允许数字和一个小数点
            if (Regex.IsMatch(sender.Text, @"^[-+]?[0-9]*\.?[0-9]+$"))
            {
                sender.BackColor = SystemColors.Window;
                return true;
            }
            else
            {
                sender.BackColor = Color.Red;
                return false;
            }
        }

        //检查输入是否为0到9之间的正整数
        private bool IsValidInteger(System.Windows.Forms.TextBox sender)
        {
            // 使用正则表达式进行匹配：只允许数字和一个小数点
            if (Regex.IsMatch(sender.Text, @"^[0-9]\d*$"))
            {
                sender.BackColor = SystemColors.Window;
                return true;
            }
            else
            {
                sender.BackColor = Color.Red;
                return false;
            }
        }


        #region 超声采集部分
        private void buttonTemplate_Click(object sender, EventArgs e)
        {
            switch (comboBoxWizardType.SelectedIndex)
            {
                case 0:
                    PulseEchoTemplate(true); break;
                case 1:
                    PulseEchoTemplate(false); break;
                case 2:
                case 3:
                case 4:
                    PitchCatchTemplate(); break;
            }
        }

        private bool CheckWizardTemplate()
        {
            if (wizardPitchCatchTemplate == null)
            {
                wizardPitchCatchTemplate = new csWizardPitchCatchTemplate(hwDeviceOEMPA);
                if (wizardPitchCatchTemplate == null)
                    return false;
                wizardTemplate = wizardPitchCatchTemplate;
            }
            return true;
        }

        private void PulseEchoTemplate(bool bLinear)
        {
            IniFile ini = new IniFile(wizardFileName);
            DialogResult dialogResult;
            int iErrorChannelProbe, iErrorChannelScan;

            if (!CheckWizardTemplate())
                return;
            wizardTemplate.Scan.Linear = bLinear;
            if (!File.Exists(wizardFileName))
                ini.TemplateWrite(wizardTemplate);
            wizardTemplate.TemplateEdit(wizardFileName, true);
            dialogResult = MessageBox.Show("Do you want to complete ?", "Wizard", MessageBoxButtons.YesNo);
            unsafe
            {
                if (dialogResult == DialogResult.Yes)
                {
                    iErrorChannelProbe = iErrorChannelScan = 0;
                    if (!ini.TemplateRead(ref wizardTemplate))
                    {
                        MessageBox.Show("Error to load template!");
                        return;
                    }
                    //You can change what you want here, for example uncomment following lines:
                    //wizardTemplate.Specimen.Velocity = 6302.0;
                    //wizardTemplate.Probe.Frequency = 5100000.0;
                    if (!wizardTemplate.TemplateToWizard())
                        MessageBox.Show("Error to save wizard from template!");
                    else if (!wizardTemplate.WizardUpdateScan(&iErrorChannelProbe, &iErrorChannelScan))
                        MessageBox.Show("Error to save wizard from template!");
                    else if ((iErrorChannelProbe > 0) || (iErrorChannelScan > 0))
                    {
                        MessageBox.Show("Error to update scan!");
                    }
                    else
                    {
                        wizardCompleted = true;
                        wizardCompletedPitchCatch = false;
                        File.Delete(wizardFileNamePitchCatch);
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
            UpdateDialog();
        }

        private void PitchCatchTemplate()
        {
            IniFile ini = new IniFile(wizardFileNamePitchCatch);
            DialogResult dialogResult;

            if (!CheckWizardTemplate())
                return;
            wizardPitchCatchTemplate.Scan.PitchCatchDefinition = (csEnumPitchCatchDefinition)comboBoxWizardType.SelectedIndex - 2;
            if (!File.Exists(wizardFileNamePitchCatch))
                ini.TemplateWrite(wizardPitchCatchTemplate);
            wizardPitchCatchTemplate.TemplateEdit(wizardFileNamePitchCatch, true);
            dialogResult = MessageBox.Show("Do you want to complete ?", "Wizard", MessageBoxButtons.YesNo);
            unsafe
            {
                if (dialogResult == DialogResult.Yes)
                {
                    if (!ini.TemplateRead(ref wizardPitchCatchTemplate))
                    {
                        MessageBox.Show("Error to load template!");
                        return;
                    }
                    //You can change what you want here, for example uncomment following lines:
                    //wizardPitchCatchTemplate.Specimen.Velocity = 6302.0;
                    //wizardPitchCatchTemplate.Probe.Frequency = 5100000.0;
                    if (!wizardPitchCatchTemplate.TemplateToWizard())
                        MessageBox.Show("Error to save wizard from template!");
                    else
                    {
                        wizardCompletedPitchCatch = true;
                        wizardCompleted = false;
                        File.Delete(wizardFileName);
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
            UpdateDialog();
        }

        private void UpdateDialog()
        {
            bool bEnableWizard = connect;
            if (bSlave)
                bIO = true;
            else
                bIO = connect && hwDeviceOEMPA.GetSWDeviceOEMPA().IsIOBoardEnabled();

            if (checkBoxConnect.Checked && !connect)
                checkBoxConnect.Checked = false;

            textBoxIPAddress.Enabled = !connect;
            textBoxPort.Enabled = !connect;

            buttonRead.Visible = connect;
            buttonWrite.Visible = connect;
            textBoxRead.Visible = connect;
            textBoxWriteGain.Visible = connect;
            textBoxWriteStart.Visible = connect;
            textBoxWriteRange.Visible = connect;
            buttonRead.Visible = connect;
            buttonWrite.Visible = connect;
            checkBoxPulser.Visible = connect;
            buttonLoad.Visible = connect;
            buttonSave.Visible = connect;
            textBoxFileName.Visible = connect;
            textBoxFileStatus.Visible = connect;
            buttonStatus.Visible = connect;
            buttonMsgBox.Visible = true;

            comboBoxSynchronisation.Visible = connect;
            buttonResetEncoder.Visible = connect;
            buttonWriteResolution.Visible = connect;
            buttonWriteStep.Visible = connect;
            textBoxResolution.Visible = connect;
            textBoxStep.Visible = connect;
            labelResolution.Visible = connect;
            labelStep.Visible = connect;

            buttonTemplate.Visible = connect;

            comboBoxWizardType.Visible = connect;
            if (((wizardPitchCatchTemplate == null) || !wizardCompletedPitchCatch) &&
                (((wizardTemplate == null) || !wizardCompleted)))
                bEnableWizard = false;
            buttonWizardToFile.Visible = bEnableWizard;
            buttonWizardToHw.Visible = bEnableWizard;

            if ((iThisDialogCount != 0) || (nextDevice == null))
                checkBoxMaster.Visible = false;
            else
            {
                checkBoxMaster.Visible = true;
                if (hwDeviceOEMPA.GetHardwareLink() == csEnumHardwareLink.csMaster)
                    checkBoxMaster.Checked = true;
                else
                    checkBoxMaster.Checked = false;
            }
            if (nextDevice == null)
                buttonNewDevice.Visible = true;
            else
                buttonNewDevice.Visible = false;

            textBoxStatus.Enabled = false;
            textBoxRead.Enabled = false;
            textBoxFileName.Enabled = false;
            textBoxFileStatus.Enabled = false;

            comboBoxOEMType.Enabled = !connect;
            if (bSlave)
            {
                comboBoxSynchronisation.Enabled = false;
                buttonResetEncoder.Enabled = false;
                buttonWriteResolution.Enabled = false;
                buttonWriteStep.Enabled = false;
                textBoxResolution.Enabled = false;
                textBoxStep.Enabled = false;
                labelResolution.Enabled = false;
                labelStep.Enabled = false;
            }
            else
            {
                comboBoxSynchronisation.Enabled = bIO;
                buttonResetEncoder.Enabled = bIO;
                buttonWriteResolution.Enabled = bIO;
                buttonWriteStep.Enabled = bIO;
                textBoxResolution.Enabled = bIO;
                textBoxStep.Enabled = bIO;
                labelResolution.Enabled = bIO;
                labelStep.Enabled = bIO;
            }

            m_bCallback = true;
            if (connect && hwDeviceOEMPA.GetSWDeviceOEMPA().IsUSB3Connected())
            {
                groupBoxUSB3.Visible = true;
                radioButtonUSB3.Visible = true;
                radioButtonEthernet.Visible = true;
                radioButtonUSB3.Checked = hwDeviceOEMPA.GetSWDeviceOEMPA().IsUSB3Enabled();
                radioButtonEthernet.Checked = !hwDeviceOEMPA.GetSWDeviceOEMPA().IsUSB3Enabled();
            }
            else
            {
                groupBoxUSB3.Visible = false;
                radioButtonUSB3.Visible = false;
                radioButtonEthernet.Visible = false;
                radioButtonUSB3.Checked = false;
                radioButtonEthernet.Checked = true;
            }
            m_bCallback = false;

        }




        private void hslBtnStopLineScan_Click(object sender, EventArgs e)
        {
            ProgramStates ProState = _ACS.GetProgramState(ProgramBuffer.ACSC_BUFFER_3);
            if ((ProState & ProgramStates.ACSC_PST_RUN) != 0)
            {
                _ACS.StopBuffer(ProgramBuffer.ACSC_BUFFER_3);
            }
        }

 



        unsafe private void InitDialog()
        {
            csHWDevice hwDevice;
            csAcquisitionFifo fifoAscan, fifoCscan;
            csCustomizedOEMPA api;
            bool bFifoAscanEnable = false;
            bool bFifoCscanEnable = false;
            Int64 iBufferByteSize = (Int64)2 * (Int64)8 * (Int64)1024 * (Int64)1024;

            if (hwDeviceOEMPA == null)
                return;
            hwDevice = hwDeviceOEMPA.GetHWDevice();
            fifoAscan = hwDevice.GetAcquisitionFifo(csEnumAcquisitionFifo.csFifoAscan);
            if (bFifoAscanEnable && !fifoAscan.Alloc(1024, iBufferByteSize))//function to enable the fifo for ascan.
                MessageBox.Show("Error fifoAscan.Alloc");
            fifoCscan = hwDevice.GetAcquisitionFifo(csEnumAcquisitionFifo.csFifoCscan);
            if (bFifoCscanEnable && !fifoCscan.Alloc(1024, 1024 * 1024))//function to enable the fifo for cscan.
                MessageBox.Show("Error fifoCscan.Alloc");
            hwDevice.SetAcquisitionParameter(this);
            hwDevice.SetAcquisitionAscan_0x00010103(AcquisitionAscan_0x00010103);
            //启用FMC采集函数
            //hwDevice.SetAcquisitionAscan_0x00020203(AcquisitionAscan_0x00020203);


            hwDevice.SetAcquisitionCscan_0x00010X02(AcquisitionCscan_0x00010X02);//在这把设备里面相关函数拿出来
            hwDevice.SetAcquisitionIO_0x00010101(AcquisitionIO_0x00010101, false);
            hwDevice.SetAcquisitionInfo(AcquisitionInfo);
            api = hwDeviceOEMPA.GetCustomizedOEMPA();
            api.SetCallbackCustomizedDriverAPI(CallbackCustomizedOEMPA);
            hwDeviceOEMPA.test();//test function is used to test all callback.

            //hwDeviceOEMPA.EnableFMC(true);

            //int startElement = 0;
            //int stopElement = 63;
            //int step = 1;

            //hwDeviceOEMPA.SetFMCElement(ref startElement, ref stopElement, ref step);

            //int cycleCount = stopElement - startElement + 1;
            //hwDeviceOEMPA.SetCycleCount(ref cycleCount);
        }

        public int AcquisitionInfo(Object pAcquisitionParameter, String pInfo)
        {
            if (pInfo.IndexOf("HW Error : acquisition data lost") == 0)
            {
                //to avoid to display "acquisition data lost" with a pop-up (otherwise because of the ballsnow effect you will have a lot of new acq data lost).
                if (pInfo.IndexOf("A") > 0 || pInfo.IndexOf("A+C") > 0 || pInfo.IndexOf("FW FIFO") > 0)
                    dataLostAscan++;
                if (pInfo.IndexOf("C") > 0 || pInfo.IndexOf("A+C") > 0)
                    dataLostCscan++;
                return 0;
            }
            if (pInfo.IndexOf("HW Error : stream bad address") == 0)
                return 0;
            //MessageBox.Show(pInfo);
            return 0;
        }

        public int CallbackCustomizedOEMPA(Object pAcquisitionParameter, ref csEnumStepCustomizedAPI eStep, String pFile, ref int iCycleCount)
        {
            csCustomizedOEMPA api;
            csCycle cycle;
            csFocalLaw focalLaw;
            bool bWriteHWUpdate = false;//false if you dont want update data before updating the HW device, true if you want to update data.
            bool bFileSaveUpdate = false;//false if you dont want update data before saving OEMPA file, true if you want to update data.
            csRoot root;
            bool bRet = true;

            api = hwDeviceOEMPA.GetCustomizedOEMPA();
            if (iCycleCount > 0)
            {
                switch (eStep)
                {
                    case csEnumStepCustomizedAPI.csWriteFile_Enter://called just before saving OEMPA file.
                        if (api.GetRoot(out root))
                        {
                            if (bFileSaveUpdate)//it is possible to change parameters before saving the OEMPA file.
                            {
                                root.ullSavedParameters |= (ulong)csEnumCustomizedFlags.csOEMPA_CSCAN_ENABLE_TOF | (ulong)csEnumCustomizedFlags.csOEMPA_ASCAN_REQUEST;
                                root.bEnableCscanTof = true;
                                root.csAscanRequest = csEnumAscanRequest.eAscanSampled;
                                root.dAscanRequestFrequency = 491.0;
                                api.SetRoot(ref root);
                            }
                        }
                        break;
                    case csEnumStepCustomizedAPI.csWriteHW_Enter:
                        callbackCustomized = true;
                        pFileName = pFile;
                        cycleCount = iCycleCount;
                        if (api.GetRoot(out root))
                        {
                            //1.1.5.3l takes too much time: delegateUpdateRoot d = this.UpdateRoot;
                            //1.1.5.3l takes too much time: this.Invoke(d, new object[] { root });
                            mEvent.BeginInvoke(ref root, null, null);
                            if (root.iCycleCount > 0)
                            {
                                m_iCycleCount = root.iCycleCount;
                                m_bAcquisitionCscanAmp = new bool[4 * m_iCycleCount];
                                m_bAcquisitionCscanTof = new bool[4 * m_iCycleCount];
                                m_sAcquisitionCscanAmp = new short[4 * m_iCycleCount];
                                m_sAcquisitionCscanTof = new short[4 * m_iCycleCount];
                                for (int iGate2 = 0; iGate2 < 4; iGate2++)
                                {
                                    for (int iCycle2 = 0; iCycle2 < m_iCycleCount; iCycle2++)
                                    {
                                        m_bAcquisitionCscanAmp[iGate2 + 4 * iCycle2] = false;
                                        m_bAcquisitionCscanTof[iGate2 + 4 * iCycle2] = false;
                                        m_sAcquisitionCscanAmp[iGate2 + 4 * iCycle2] = 0;
                                        m_sAcquisitionCscanTof[iGate2 + 4 * iCycle2] = 0;
                                    }
                                }
                            }
                            else
                            {
                                m_iCycleCount = 0;
                                m_bAcquisitionCscanAmp = null;
                                m_bAcquisitionCscanTof = null;
                                m_sAcquisitionCscanAmp = null;
                                m_sAcquisitionCscanTof = null; ;
                            }
                        }
                        gain = -1.0;
                        if (api.GetCycle(0, out cycle))
                        {
                            gain = cycle.dGainDigital;//read the amplification (first cycle).
                            start = cycle.dStart;
                            range = cycle.dRange;
                            if (bWriteHWUpdate)//it is possible to change parameters before updating the HW with them.
                            {
                                cycle.dGainDigital = 20.0;//update the amplification.
                                if (!api.SetCycle(0, cycle))
                                    bRet = false;
                            }
                        }
                        if (api.GetFocalLaw(true, 0, out focalLaw))
                        {
                            focalLaw.afPrm[0] += 0.0f;
                            api.SetFocalLaw(true, 0, focalLaw);
                        }
                        break;
                }
            }
            Unreferenced.Parameter(bRet);
            return 0;
        }


        protected void initWizardFileName()
        {
            string aux;

            if (!csWizardTemplate.GetWizardFolder(ref wizardFileName))
            {
                if (hwDeviceOEMPA == null)
                    wizardFileName = @"C:\Users\Public\csWizard.txt";
                else
                    wizardFileName = string.Format("C:\\Users\\Public\\csWizard_{0}.txt", hwDeviceOEMPA.GetDeviceId());
            }
            else
            {
                if (hwDeviceOEMPA == null)
                    aux = string.Format("csWizard.txt");
                else
                    aux = string.Format("csWizard_{0}.txt", hwDeviceOEMPA.GetDeviceId());
                wizardFileName = wizardFileName + aux;
            }

            if (!csWizardPitchCatchTemplate.GetWizardFolder(ref wizardFileNamePitchCatch))
            {
                if (hwDeviceOEMPA == null)
                    wizardFileName = @"C:\Users\Public\csWizardPitchCatch_.txt";
                else
                    wizardFileNamePitchCatch = string.Format("C:\\Users\\Public\\csWizardPitchCatch_{0}.txt", hwDeviceOEMPA.GetDeviceId());
            }
            else
            {
                if (hwDeviceOEMPA == null)
                    aux = string.Format("csWizardPitchCatch.txt");
                else
                    aux = string.Format("csWizardPitchCatch_{0}.txt", hwDeviceOEMPA.GetDeviceId());
                wizardFileNamePitchCatch = wizardFileNamePitchCatch + aux;
            }
        }

        private void btnTestDLL_Click(object sender, EventArgs e)
        {
            try
            {
                var testDevice = new csHWDeviceOEMPA1();
                MessageBox.Show("DLL加载成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DLL加载失败: {ex.Message}");
            }
        }

        // 1. 后台线程控制器
        private CancellationTokenSource cts;
        private Thread captureThread;

        /*-------------------------- 原事件 ----------------------------*/
        private bool _isProcessingPulserChange = false;
        private volatile bool _allowFmcCapture = false;
        private volatile int _fmcWrittenColumns = 0;
        private TaskCompletionSource<bool> _fmcDoneTcs;
        private const int FMC_TOTAL_COLUMNS = 64 * 64;
        private void checkBoxPulser_CheckedChanged(object sender, EventArgs e)
        {
            if (_isProcessingPulserChange) return;
            if (!checkBoxPulserEnable) return;

            _isProcessingPulserChange = true;
            FMC_lengthwrite(this, EventArgs.Empty);//创建FMC维度
            try
            {
                bool start = checkBoxPulser.Checked;
                var swDevice = hwDeviceOEMPA.GetSWDevice();

                //swDevice.EnablePulser(start);
                _currentElementIndex = 0;
                _fmcWrittenColumns = 0;

                if (start)
                {
                    var dr = MessageBox.Show("是否保存 FMC 数据？", "数据保存",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        // 立即显示保存对话框
                        using (SaveFileDialog dialog = new SaveFileDialog())
                        {
                            dialog.Filter = "MAT文件 (*.mat)|*.mat";
                            dialog.FileName = $"FMC_Data_{DateTime.Now:yyyyMMdd_HHmmss}";

                            if (dialog.ShowDialog() == DialogResult.OK)
                            {

                                // 在后台保存
                                Task.Run(async () => // 采集的第一个线程
                                {
                                    string filePath = dialog.FileName; // 保存文件路径到局部变量
                                    try
                                    {
                                        //if (FMC_DataMatrix.GetLength(1) == 4096) {
                                        //    SaveFMCDataAsMat(filePath);
                                        //    continue;
                                        //}
                                        //_currentElementIndex = 0;
                                        //_fmcWrittenColumns = 0;
                                        _fmcDoneTcs = new TaskCompletionSource<bool>();
                                        _allowFmcCapture = true;

                                        //FMC_lengthwrite(this, EventArgs.Empty);

                                        swDevice.EnablePulser(true);

                                        await _fmcDoneTcs.Task;
                                        SaveFMCDataAsMat(filePath);
                                        swDevice.EnablePulser(false);
                                        _allowFmcCapture = false;
                                        //SaveFMCDataAsMat(filePath);
                                        checkBoxPulser.Checked = false;
                                        // 使用明确的委托类型
                                        this.Invoke(new Action(() =>
                                        {
                                            _isProcessingPulserChange = true;
                                            checkBoxPulser.Checked = false;
                                            _isProcessingPulserChange = false;
                                            MessageBox.Show($"保存成功!\n文件: {filePath}", "完成",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }));
                                    }
                                    catch (Exception ex)
                                    {
                                        // 使用明确的委托类型
                                        this.Invoke(new Action(() =>
                                        {
                                            _isProcessingPulserChange = true;
                                            checkBoxPulser.Checked = false;
                                            _isProcessingPulserChange = false;
                                            MessageBox.Show($"保存失败: {ex.Message}", "错误",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }));
                                    }
                                });
                            }
                            else
                            {
                                // 用户取消保存，直接停止激发
                                swDevice.EnablePulser(false);
                                _isProcessingPulserChange = true;
                                checkBoxPulser.Checked = false;
                                _isProcessingPulserChange = false;
                            }
                        }
                    }
                    else
                    {
                        swDevice.EnablePulser(true);
                        StartRealTimeAcquisition();
                    }
                }
                else
                {
                    swDevice.EnablePulser(false);
                    StopAcquisition();
                }
            }
            catch (Exception ex)
            {
                HandleAcquisitionError(ex);
            }
            finally
            {
                _isProcessingPulserChange = false;
            }
        }

        // 简化的错误处理
        private void HandleAcquisitionError(Exception ex)
        {
            var swDevice = hwDeviceOEMPA.GetSWDevice();
            try
            {
                swDevice.EnablePulser(false);
                _isProcessingPulserChange = true;
                checkBoxPulser.Checked = false;
            }
            catch
            {
                // 忽略错误
            }
            finally
            {
                _isProcessingPulserChange = false;
            }

            MessageBox.Show($"操作失败：{ex.Message}", "错误",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // 停止采集
        private void StopAcquisition()
        {
            try
            {
                if (cts != null && !cts.IsCancellationRequested)
                    cts.Cancel();

                // 等待任务结束（短超时）
                _captureTask?.Wait(500);
                _processingTask?.Wait(500);
            }
            catch (AggregateException) { }
            catch (Exception) { }
            finally
            {
                // 清理
                if (_acqQueue != null)
                {
                    while (_acqQueue.TryTake(out _)) { }
                }

                if (_imgQueue != null)
                {
                    while (_imgQueue.TryTake(out _)) { }
                }

                _captureTask = null;
                _processingTask = null;
                cts?.Dispose();
                cts = null;
            }
        }

        //实时A扫、B扫显示
        private void StartRealTimeAcquisition()
        {
            try
            {
                // 清理历史数据与队列
                _bScanHistory.Clear();

                // 停掉旧的（若有）
                //StopAcquisition();

                cts = new CancellationTokenSource();

                //// 采集任务（Producer）
                //_captureTask = Task.Run(() => RealtimeCaptureLoop(cts.Token), cts.Token);

                //// 处理任务（Consumer -> Compute image）
                //_processingTask = Task.Run(() => RealtimeProcessingLoop(cts.Token), cts.Token);

                // UI 刷新定时器（主线程启动）
                StartUiTimer();

                // 更新状态（可选）
                // UpdateStatus("实时采集模式已启动", SystemColors.ControlText);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动实时采集失败：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // 后台实时成像处理循环
        // 队列，用于采集线程 -> 处理线程
        private BlockingCollection<FMCFrame> dataQueue = new BlockingCollection<FMCFrame>(boundedCapacity: 10);
        // 每一帧的数据包装
        private class FMCFrame
        {
            public int[,] Matrix { get; set; }  // FMC 数据矩阵
            public double fs { get; set; }      // 采样频率
            public int step { get; set; }       // 步长
            public int maxPt { get; set; }      // 最大绘制点数
        }

        //private void RealtimeProcessingLoop(CancellationToken token)
        //{
        //    while (!token.IsCancellationRequested)
        //    {
        //        try
        //        {
        //            // 1. 获取最新一帧 FMC 数据
        //            var data = _acqQueue.Take(token); // 阻塞式，不会空转耗CPU
        //            //int[,] FMC_DataMatrix = data.Matrix;
        //            double fs = 50;
        //            int step = FMC_DataMatrix.GetLength(0);
        //            int maxPt = 500;

        //            // 2. 成像/处理 （占用CPU部分）
        //            // 你之前的 UpdateRealTimeDisplays 就用在这里
        //            // 但不能直接调用，会跨线程，需要 Invoke
        //            this.BeginInvoke(new Action(() =>
        //            {
        //                UpdateRealTimeDisplays(FMC_DataMatrix, fs, step, maxPt);
        //            }));
        //        }
        //        catch (OperationCanceledException)
        //        {
        //            break; // 正常退出
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine("RealtimeProcessingLoop 错误: " + ex.Message);
        //        }
        //    }
        //}

        //private System.Windows.Forms.Timer _uiTimer;

        private void StartUiTimer()
        {
            if (_uiTimer != null)
            {
                _uiTimer.Stop();
                _uiTimer.Dispose();
            }

            _uiTimer = new System.Windows.Forms.Timer();
            _uiTimer.Interval = UI_INTERVAL_MS;


            int fs = 50000000;
            int length_pt = FMC_DataMatrix.GetLength(0);
            //int[] frame = null;
            _uiTimer.Tick += (s, e) =>
            {
                // ===== A扫 =====
                if (FMC_DataMatrix != null)
                {
                    //DrawAscanRealtime(ascanData, fs, 4, 800);
                    DrawAscanRealtime(FMC_DataMatrix, fs, length_pt);
                }

                // ===== B扫 =====
                //UpdateBScanDisplay(FMC_DataMatrix);

                // 触发后台计算
                ComputeBScanBitmapInBackground();

                // UI只显示最新B扫图
                Bitmap bmpToShow = null;

                lock (_bscanImageLock)
                {
                    if (_hasNewBscanBitmap && _latestBscanBitmap != null)
                    {
                        bmpToShow = (Bitmap)_latestBscanBitmap.Clone();
                        _hasNewBscanBitmap = false;
                    }
                }

                if (bmpToShow != null)
                {
                    var old = pictureBoxBscan.Image;
                    pictureBoxBscan.Image = bmpToShow;
                    old?.Dispose();
                }

                // ===== FPS =====
                UpdateFrameRate();
            };

            _uiTimer.Start();
        }
        private readonly object _bscanImageLock = new object();
        private Bitmap _latestBscanBitmap = null;
        private volatile bool _hasNewBscanBitmap = false;
        private DateTime _lastBscanComputeTime = DateTime.MinValue;
        private void ComputeBScanBitmapInBackground()
        {
            if ((DateTime.Now - _lastBscanComputeTime).TotalMilliseconds < 200)
                return; // B扫 5 FPS

            _lastBscanComputeTime = DateTime.Now;

            Task.Run(() =>
            {
                try
                {
                    int[,] bscanData = GetBscanDataFromFMC(FMC_DataMatrix);
                    if (bscanData == null) return;

                    double[,] displayMatrix = ConvertBScanToDisplayMatrix(bscanData);
                    Bitmap bmp = CreateBScanBitmap(displayMatrix);
                    if (bmp == null) return;

                    lock (_bscanImageLock)
                    {
                        _latestBscanBitmap?.Dispose();
                        _latestBscanBitmap = bmp;
                        _hasNewBscanBitmap = true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"后台B扫计算错误: {ex.Message}");
                }
            });
        }
        private double[,] ConvertBScanToDisplayMatrix(int[,] bscanData)
        {
            int numSamples = bscanData.GetLength(0);
            int numStations = bscanData.GetLength(1);

            double[,] displayMatrix = new double[numStations, numSamples];

            for (int station = 0; station < numStations; station++)
            {
                for (int sample = 0; sample < numSamples; sample++)
                {
                    displayMatrix[station, sample] = bscanData[sample, station];
                }
            }

            return displayMatrix;
        }

        /*-------------------------- 后台线程 ----------------------------*/
        /*-------------------------- 后台线程：只改调用参数 ----------------------------*/
        // 线程与取消
        //private CancellationTokenSource cts = null;

        // Producer/Consumer 队列
        private BlockingCollection<int[]> _acqQueue = new BlockingCollection<int[]>(boundedCapacity: 8);   // 原始帧队列
        private BlockingCollection<Bitmap> _imgQueue = new BlockingCollection<Bitmap>(boundedCapacity: 2); // 最终渲染位图队列

        // 处理任务句柄
        private Task _captureTask;
        private Task _processingTask;

        // 控制参数
        private const int UI_INTERVAL_MS = 40; // 25 FPS


        //实时采集循环（支持A扫和B扫）
        private void RealtimeCaptureLoop(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    int[] frame = GetOneFrame(); // 尽量确保这是非阻塞或有超时的设备读取函数
                    if (frame == null)
                    {
                        // 没数据时短暂等待，避免忙等
                        Thread.Sleep(1);
                        continue;
                    }

                    // 尝试放入队列，若队列满我们这里可以选择丢帧（TryAdd）或阻塞（Add）
                    // 我用 TryAdd(0) 尽量保持采集实时性：若队列满则丢掉最旧的一帧以保存最新
                    if (!_acqQueue.TryAdd(frame))
                    {
                        // 队列满，弹出一个旧帧再加入，保持队列为最新帧
                        _acqQueue.TryTake(out _);
                        _acqQueue.TryAdd(frame);
                    }
                }
            }
            catch (OperationCanceledException) { /* 正常退出 */ }
            catch (Exception ex)
            {
                Debug.WriteLine("采集线程异常: " + ex.Message);
            }
        }


        // 更新实时显示
        //private void UpdateRealTimeDisplays(int[,] FMC_DataMatrix, double fs, int step, int maxPt)
        //{
        //    try
        //    {
        //        // 1. 更新A扫描显示（特定发射-接收对）
        //        UpdateAScanDisplay(FMC_DataMatrix, fs, step, maxPt);

        //        // 2. 更新B扫描显示（特定发射阵元的所有接收阵元）
        //        UpdateBScanDisplay(FMC_DataMatrix);

        //        // 3. 更新帧率信息
        //        UpdateFrameRate();
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"更新显示错误: {ex.Message}");
        //    }
        //}

        // 更新A扫描显示（特定发射-接收对）
        //private void UpdateAScanDisplay(int[,] FMC_DataMatrix, double fs, int step, int maxPt)
        //{
        //    if (FMC_DataMatrix == null || FMC_DataMatrix.Length == 0) return;

        //    try
        //    {
        //        // 获取选择的发射和接收阵元
        //        int transmitIndex = GetSelectedTransmitChannel();
        //        int receiveIndex = GetSelectedReceiveChannel();

        //        // 从FMC数据中提取特定发射-接收对的数据
        //        int[] ascanData = GetAscanDataFromFMC(FMC_DataMatrix, transmitIndex, receiveIndex);

        //        if (ascanData != null && ascanData.Length > 0)
        //        {
        //            DrawAscanRealtime(ascanData, fs, step, maxPt);

        //            // 更新A扫描标题显示当前通道
        //            UpdateAScanTitle(transmitIndex, receiveIndex);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"A扫描显示错误: {ex.Message}");
        //    }
        //}



        // 更新B扫描显示（特定发射阵元的所有接收阵元）
        //private void UpdateBScanDisplay(int[,] FMC_DataMatrix)
        //{
        //    try
        //    {
        //        int transmitIndex = GetSelectedBScanTransmit();
        //        if (transmitIndex >= 0)
        //        {
        //            // 提取该发射阵元的所有接收阵元数据
        //            int[][] bscanData = GetBscanDataFromFMC(FMC_DataMatrix, transmitIndex);
        //            if (bscanData != null)
        //            {
        //                // 添加到B扫描历史
        //                AddToBScanHistory(bscanData);

        //                // 更新B扫描图像
        //                UpdateBScanImage();

        //                // 更新B扫描标题
        //                UpdateBScanTitle(transmitIndex);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"B扫描显示错误: {ex.Message}");
        //    }
        //}

        // 从FMC数据中提取特定发射-接收对的A扫描数据
        private int[] GetAscanDataFromFMC(int[,] FMC_DataMatrix, int transmitIndex, int receiveIndex)
        {
            if (FMC_DataMatrix == null) return null;

            try
            {
                // 计算在二维数组中的列索引
                int columnIndex = transmitIndex * 64 + receiveIndex;

                // 检查列索引是否有效
                if (columnIndex < 0 || columnIndex >= FMC_DataMatrix.GetLength(1))
                {
                    Debug.WriteLine($"列索引超出范围: {columnIndex}, 数据列数: {FMC_DataMatrix.GetLength(1)}");
                    return null;
                }

                // 提取该列的所有行数据（即该发射-接收对的A扫描数据）
                int sampleCount = FMC_DataMatrix.GetLength(0);
                int[] ascanData = new int[sampleCount];

                for (int i = 0; i < sampleCount; i++)
                {
                    ascanData[i] = FMC_DataMatrix[i, columnIndex];
                }

                Debug.WriteLine($"提取A扫描数据: Tx{transmitIndex}->Rx{receiveIndex}, 列索引:{columnIndex}, 采样点数:{sampleCount}");
                return ascanData;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"提取A扫描数据错误: {ex.Message}");
                return null;
            }
        }

        // 从FMC数据中提取特定发射阵元的所有接收阵元数据
        private int[,] GetBscanDataFromFMC(int[,] FMC_DataMatrix)
        {
            if (FMC_DataMatrix == null)
            {
                Debug.WriteLine("FMC数据为null");
                return null;
            }

            try
            {
                int sampleCount = FMC_DataMatrix.GetLength(0);
                int totalColumns = FMC_DataMatrix.GetLength(1) / 64;
                //int columnIndex = elementIndex * 64 + elementIndex;

                //if (columnIndex < 0 || columnIndex >= totalColumns)
                //{
                //    Debug.WriteLine($"B扫描自发自收列索引超出范围: element={elementIndex}, column={columnIndex}, 总列数={totalColumns}");
                //    return null;
                //}

                int[,] bscandata = new int[sampleCount, totalColumns];
                for (int j = 0; j < totalColumns; j++)
                {
                    int BsacnIndex = j * 64 + j;
                    for (int i = 0; i < sampleCount; i++)
                    {
                        bscandata[i, j] = FMC_DataMatrix[i, BsacnIndex];
                    }
                }


                return bscandata;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"提取B扫描数据错误: {ex.Message}");
                return null;
            }
        }

        // 获取选择的发射阵元（A扫描）
        private int GetSelectedTransmitChannel()
        {
            if (comboxBoxTransmit != null && comboxBoxTransmit.SelectedIndex >= 0)
            {
                return comboxBoxTransmit.SelectedIndex;
            }
            return 0;
        }

        // 获取选择的接收阵元（A扫描）
        private int GetSelectedReceiveChannel()
        {
            if (comboxBoxReceive != null && comboxBoxReceive.SelectedIndex >= 0)
            {
                return comboxBoxReceive.SelectedIndex;
            }
            return 0;
        }

        // 获取B扫描选择的发射阵元
        private int GetSelectedBScanTransmit()
        {
            if (comboxBoxBScanTransmit != null && comboxBoxBScanTransmit.SelectedIndex >= 0)
            {
                return comboxBoxBScanTransmit.SelectedIndex;
            }
            return 0;
        }
        // 更新A扫描标题
        private void UpdateAScanTitle(int transmitIndex, int receiveIndex)
        {
            if (Ascan != null && Ascan.Titles.Count > 0)
            {
                Ascan.Titles[0].Text = $"A扫描 - 发射:{transmitIndex} 接收:{receiveIndex}";
            }
        }

        // 更新B扫描标题
        private void UpdateBScanTitle(int transmitIndex)
        {
            if (pictureBoxBscan != null)
            {
                // 如果PictureBox有Parent，可以设置父控件的Text
                if (pictureBoxBscan.Parent != null)
                {
                    pictureBoxBscan.Parent.Text = $"B扫描 - 发射阵元:{transmitIndex}";
                }
            }
        }
        // B扫描历史管理（存储每个接收阵元的数据）
        private List<int[][]> _bScanHistory = new List<int[][]>();
        private const int MAX_BSCAN_HISTORY = 500;

        private void AddToBScanHistory(int[][] bscanData)
        {
            if (bscanData == null) return;

            // 深拷贝数据
            int[][] dataCopy = new int[64][];
            for (int i = 0; i < 64; i++)
            {
                if (bscanData[i] != null)
                {
                    dataCopy[i] = new int[bscanData[i].Length];
                    Array.Copy(bscanData[i], dataCopy[i], bscanData[i].Length);
                }
            }

            _bScanHistory.Add(dataCopy);

            if (_bScanHistory.Count > MAX_BSCAN_HISTORY)
            {
                _bScanHistory.RemoveAt(0);
            }
        }
        private void UpdateBScanImage()
        {
            if (FMC_DataMatrix == null) return;

            try
            {
                // 获取选择的发射阵元
                int transmitIndex = GetSelectedBScanTransmit();
                if (transmitIndex < 0) return;

                int timeSteps = FMC_DataMatrix.GetLength(0); // 时间步长 (1501)
                int totalChannels = FMC_DataMatrix.GetLength(1); // 总通道数 (4096)

                Debug.WriteLine($"B扫描数据: 时间步长={timeSteps}, 总通道数={totalChannels}, 发射阵元={transmitIndex}");

                // 创建显示矩阵：64个接收通道 × 时间步长
                // 修正：矩阵行=接收通道，列=时间
                double[,] displayMatrix = new double[64, timeSteps];

                // 从FMC_DataMatrix提取该发射阵元的所有接收通道数据
                for (int receiveIndex = 0; receiveIndex < 64; receiveIndex++)
                {
                    int channelIndex = transmitIndex * 64 + receiveIndex;

                    if (channelIndex < totalChannels)
                    {
                        for (int timeIndex = 0; timeIndex < timeSteps; timeIndex++)
                        {
                            // 接收通道0在顶部，接收通道63在底部
                            displayMatrix[receiveIndex, timeIndex] = FMC_DataMatrix[timeIndex, channelIndex];
                        }
                    }
                }

                // 创建彩色位图（修正坐标轴）
                Bitmap bmp = CreateBScanBitmap(displayMatrix);

                // 更新PictureBox
                UpdatePictureBoxWithImage(bmp);

                Debug.WriteLine($"B扫描图像生成完成: {displayMatrix.GetLength(1)}x{displayMatrix.GetLength(0)}");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"更新B扫描图像错误: {ex.Message}");
            }
        }




        //251022fhf
        private Bitmap CreateBScanBitmap(double[,] displayMatrix)
        {
            int receiveChannels = displayMatrix.GetLength(0); // 接收通道数 (64) - X轴
            int timeSteps = displayMatrix.GetLength(1);       // 时间步长 (1501) - Y轴

            // 使用固定尺寸便于显示
            int displayWidth = 800;  // X轴：接收通道
            int displayHeight = 400; // Y轴：时间/深度

            Bitmap bmp = new Bitmap(displayWidth, displayHeight);

            // 找到数据的动态范围
            double minVal = double.MaxValue;
            double maxVal = double.MinValue;

            for (int channel = 0; channel < receiveChannels; channel++)
            {
                for (int time = 0; time < timeSteps; time++)
                {
                    double val = Math.Abs(displayMatrix[channel, time]);
                    if (val < minVal) minVal = val;
                    if (val > maxVal) maxVal = val;
                }
            }

            Debug.WriteLine($"B扫描数据范围: min={minVal}, max={maxVal}");

            double range = maxVal - minVal;
            if (range < 1e-10) range = 100;

            // 正确的坐标轴映射：
            // X轴（显示宽度）：接收通道（0-63）
            // Y轴（显示高度）：时间/深度（0-1500采样点）
            for (int displayY = 0; displayY < displayHeight; displayY++)
            {
                // Y轴：时间/深度（从上到下增加）
                int originalTime = displayY * timeSteps / displayHeight;

                for (int displayX = 0; displayX < displayWidth; displayX++)
                {
                    // X轴：接收通道（从左到右增加）
                    int originalChannel = displayX * receiveChannels / displayWidth;

                    if (originalChannel < receiveChannels && originalTime < timeSteps)
                    {
                        double value = Math.Abs(displayMatrix[originalChannel, originalTime]);
                        double normalized = (value - minVal) / range;

                        // 使用灰度或彩色映射
                        //Color color = GetGrayColormap(normalized);
                        Color color = GetColorFromColormap(normalized);

                        bmp.SetPixel(displayX, displayY, color);
                    }
                    else
                    {
                        bmp.SetPixel(displayX, displayY, Color.Black);
                    }
                }
            }

            // 添加正确的坐标轴标签
            AddBScanLabels(bmp, receiveChannels, timeSteps);

            return bmp;
        }

        // 修正坐标轴标签
        private void AddBScanLabels(Bitmap bmp, int receiveChannels, int timeSteps)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            using (Font font = new Font("Arial", 8))
            //using (Brush brush = new SolidBrush(Color.White))
            //using (Brush blackBrush = new SolidBrush(Color.Black))
            {
                // 添加标题
                //int transmitIndex = GetSelectedBScanTransmit();
                //string title = $"B扫描 - 发射阵元{transmitIndex}";
                //SizeF titleSize = g.MeasureString(title, font);
                //g.FillRectangle(blackBrush, 5, 5, titleSize.Width + 4, titleSize.Height + 4);
                //g.DrawString(title, font, brush, 7, 7);

                //// 添加尺寸信息
                //string sizeInfo = $"{receiveChannels}接收通道 × {timeSteps}采样点";
                //SizeF sizeSize = g.MeasureString(sizeInfo, font);
                //g.FillRectangle(blackBrush, 5, 25, sizeSize.Width + 4, sizeSize.Height + 4);
                //g.DrawString(sizeInfo, font, brush, 7, 27);

                //// 添加坐标轴标签（修正方向）
                //g.FillRectangle(blackBrush, bmp.Width - 50, bmp.Height - 20, 45, 15);
                //g.DrawString("接收通道→", font, brush, bmp.Width - 48, bmp.Height - 18);

                //g.FillRectangle(blackBrush, 5, bmp.Height - 35, 60, 15);
                //g.DrawString("时间/深度↓", font, brush, 7, bmp.Height - 33);

                //// 添加接收通道刻度（X轴）
                //for (int i = 0; i < receiveChannels; i += 16)
                //{
                //    int xPos = i * bmp.Width / receiveChannels;
                //    string label = $"Rx{i}";
                //    g.FillRectangle(blackBrush, xPos, bmp.Height - 20, 25, 12);
                //    g.DrawString(label, font, brush, xPos + 2, bmp.Height - 18);
                //}

                //// 添加时间刻度（Y轴）
                //for (int i = 0; i < timeSteps; i += 500)
                //{
                //    int yPos = i * bmp.Height / timeSteps;
                //    string label = $"{i}";
                //    g.FillRectangle(blackBrush, 5, yPos, 25, 12);
                //    g.DrawString(label, font, brush, 7, yPos);
                //}
            }
        }



        // 灰度颜色映射
        private Color GetGrayColormap(double value)
        {
            value = Math.Max(0, Math.Min(1, value));
            int intensity = (int)(value * 255);
            return Color.FromArgb(intensity, intensity, intensity);
        }

        // Jet颜色映射
        private Color GetColorFromColormap(double value)
        {
            value = Math.Max(0, Math.Min(1, value));

            double r, g, b;

            if (value < 0.125)
            {
                r = 0;
                g = 0;
                b = 0.5 + 4.0 * value;
            }
            else if (value < 0.375)
            {
                r = 0;
                g = 4.0 * (value - 0.125);
                b = 1.0;
            }
            else if (value < 0.625)
            {
                r = 4.0 * (value - 0.375);
                g = 1.0;
                b = 1.0 - 4.0 * (value - 0.375);
            }
            else if (value < 0.875)
            {
                r = 1.0;
                g = 1.0 - 4.0 * (value - 0.625);
                b = 0;
            }
            else
            {
                r = 1.0 - 4.0 * (value - 0.875);
                g = 0;
                b = 0;
            }

            r = Math.Max(0, Math.Min(1, r));
            g = Math.Max(0, Math.Min(1, g));
            b = Math.Max(0, Math.Min(1, b));

            return Color.FromArgb(
                (int)(r * 255),
                (int)(g * 255),
                (int)(b * 255)
            );
        }



        // 更新PictureBox
        private void UpdatePictureBoxWithImage(Bitmap bmp)
        {
            if (pictureBoxBscan == null)
            {
                bmp?.Dispose();
                return;
            }

            if (pictureBoxBscan.InvokeRequired)
            {
                this.Invoke(new Action<Bitmap>(UpdatePictureBoxWithImage), bmp);
                return;
            }

            try
            {
                var oldImage = pictureBoxBscan.Image;
                pictureBoxBscan.Image = bmp;
                oldImage?.Dispose();
                pictureBoxBscan.Refresh();

                Debug.WriteLine($"✅ B扫描图像显示: {bmp.Width}x{bmp.Height}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ 更新PictureBox错误: {ex.Message}");
                bmp?.Dispose();
            }
        }



        // 帧率显示（保持不变）
        private DateTime _lastUpdateTime = DateTime.Now;
        private int _frameCount = 0;

        private void UpdateFrameRate()
        {
            _frameCount++;
            TimeSpan elapsed = DateTime.Now - _lastUpdateTime;

            if (elapsed.TotalSeconds >= 1.0)
            {
                double fps = _frameCount / elapsed.TotalSeconds;

                if (labelFrameRate != null)
                {
                    labelFrameRate.Text = $"帧率: {fps:F1} FPS";
                }

                _frameCount = 0;
                _lastUpdateTime = DateTime.Now;
            }
        }
        /*-------------------------- 画图 ----------------------------*/
        /*-------------------------- 画图：新增可选抽参 ----------------------------*/
        private void DrawAscanRealtime(int[,] FMC_DataMatrix, double fs, int maxPt)
        {
            if (FMC_DataMatrix == null) return;

            //int newLen = Math.Min(data.Length / step_1, maxPt);

            int transmitIndex = GetSelectedTransmitChannel();
            int receiveIndex = GetSelectedReceiveChannel();
            //int[] data = new int[FMC_DataMatrix.GetLength(0)];
            //for (int i = 0; i < FMC_DataMatrix.GetLength(0); i++) {
            //    data[i] = FMC_DataMatrix[i, transmitIndex*64+ receiveIndex];
            //}

            //maxPt = FMC_DataMatrix.GetLength(0);
            //int newLen = Math.Max(FMC_DataMatrix.GetLength(0) / step_1, maxPt);
            int newLen = maxPt;
            double[] t = new double[newLen];
            double[] d = new double[newLen];  // 改成 double！

            for (int i = 0, j = 0; j < newLen; i += 1, j++)
            {
                //t[j] = i / fs;
                t[j] = i;

                d[j] = FMC_DataMatrix[i, transmitIndex * 64 + receiveIndex];  // 自动转 double
            }

            var ser = Ascan.Series[0];
            ser.Color = Color.Blue;  // 或者 Color.Red, Color.Green 等
            ser.BorderWidth = 2;     // 确保线宽足够
            ser.Points.SuspendUpdates();
            ser.Points.Clear();

            // 关键：DataBindXY 在 ResumeUpdates 前！
            ser.Points.DataBindXY(t, d);

            ser.Points.ResumeUpdates();  // 现在才刷新！

            // 轴跟随
            var ca = Ascan.ChartAreas[0];
            ca.AxisX.Minimum = t[0];
            ca.AxisX.Maximum = t[newLen - 1];
            ca.AxisX.Title = "时间步长";
            ca.AxisY.Title = "幅值";
            ca.AxisX.LabelStyle.Format = "0";  // 强制整数，不显示小数点
        }

        /*-------------------------- 取数据 ----------------------------*/
        /*-------------------------- 取数据 ----------------------------*/
        private bool _singleShot = false;   // true=只采一帧就停
                                            // 将Ascan数据保存为文本格式
        private int[] GetOneFrame()
        {
            // 用你项目里【已经能拿到 A 扫】的那一句
            // 下面 3 种写法按你实际类名选一种，把注释打开即可：

            // ① 如果设备类里有【属性】Ascan_Data
            // return hwDeviceOEMPA.Ascan_Data ?? Array.Empty<int>();

            // ② 如果设备类里有【方法】GetAscanData()
            // return hwDeviceOEMPA.GetAscanData() ?? Array.Empty<int>();

            // ③ 如果通过【回调/事件】最新帧，这里直接读全局变量
            //return FMC_DataMatrix ?? Array.Empty<int>();
            return FMC_DataMatrix.Cast<int>().ToArray() ?? new int[0];
        }



        private void SaveAscanDataToMat()
        {
            try
            {
                if (FMC_DataMatrix == null || FMC_DataMatrix.GetLength(0) == 0)
                {
                    MessageBox.Show("没有可保存的FMC数据，请先采集数据", "警告",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "MAT文件 (*.mat)|*.mat|所有文件 (*.*)|*.*";
                saveFileDialog.Title = "保存FMC数据";
                saveFileDialog.FileName = $"FMC_Data_{DateTime.Now:yyyyMMdd_HHmmss}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // 显示保存中提示
                    //MessageBox.Show("正在保存FMC数据，请稍候...", "保存中",
                    //    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 直接保存（因为数据量可能很大，在UI线程中保存）
                    SaveFMCDataAsMat(filePath);

                    MessageBox.Show($"FMC数据已保存到：{filePath}\n数据尺寸: {FMC_DataMatrix.GetLength(0)}×{FMC_DataMatrix.GetLength(1)}",
                        "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存数据时出错：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveFMCDataAsMat(string filePath)
        {
            try
            {
                // 检查MathNet.Numerics是否可用
                try
                {
                    // 使用MathNet.Numerics保存（如果已安装）
                    SaveWithMathNet(filePath);
                }
                catch (Exception mathNetEx)
                {
                    Debug.WriteLine($"MathNet保存失败，使用自定义格式: {mathNetEx.Message}");
                    // 如果MathNet不可用，使用自定义二进制格式
                    //SaveWithCustomFormat(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"保存MAT文件失败: {ex.Message}");
            }
        }

        // 使用MathNet.Numerics保存
        private void SaveWithMathNet(string filePath)
        {
            // 将int[,]转换为double[,]
            int rows = FMC_DataMatrix.GetLength(0);
            int cols = FMC_DataMatrix.GetLength(1);
            double[,] doubleData = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    doubleData[i, j] = FMC_DataMatrix[i, j];
                }
            }

            // 创建矩阵
            var fmcMatrix = Matrix<double>.Build.DenseOfArray(doubleData);

            // 创建时间轴数据
            double[] timeArray = CreateTimeArray(rows);
            var timeMatrix = Matrix<double>.Build.DenseOfColumnArrays(timeArray);

            // 保存到MAT文件
            var variables = new Dictionary<string, Matrix<double>>
        {
            { "FMC_DataMatrix", fmcMatrix },
            { "time_axis", timeMatrix }
        };

            MatlabWriter.Write(filePath, variables);
            Debug.WriteLine($"使用MathNet保存成功: {filePath}");
        }
        // 创建时间轴数组
        private double[] CreateTimeArray(int sampleCount)
        {
            double start_time = GetStartTime();
            double timeStep = 0.02;
            double[] timeArray = new double[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                timeArray[i] = start_time + i * timeStep;
            }
            return timeArray;
        }

        private double GetStartTime()
        {
            try
            {
                if (textBoxWriteStart != null && !string.IsNullOrEmpty(textBoxWriteStart.Text))
                    return double.Parse(textBoxWriteStart.Text.Split(' ')[0]);
                else
                    return 0.0;
            }
            catch
            {
                return 0.0;
            }
        }





        private void comboBoxPCScanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bProcessConnection)
                return;

        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            double dGain = -1.0;
            double dStart = -1.0;
            double dRange = -1.0;
            int iCycleCount = -1;
            bool bRet = true;

            unsafe
            {
                if (hwDeviceOEMPA.LockDevice())
                {
                    if (!hwDeviceOEMPA.GetCycleCount(&iCycleCount))
                        bRet = false;
                    if (!hwDeviceOEMPA.GetGainDigital(0, &dGain))
                        bRet = false;
                    if (!hwDeviceOEMPA.GetAscanStart(0, &dStart))
                        bRet = false;
                    if (!hwDeviceOEMPA.GetAscanRange(0, &dRange, (csEnumCompressionType*)0, (int*)0, (int*)0))
                        bRet = false;
                    if (!hwDeviceOEMPA.UnlockDevice())
                        bRet = false;
                }
                else
                    bRet = false;
            }
            if (!bRet)
                MessageBox.Show("Communication error!");
            else
            {
                //textBoxRead.Text = "Cycles: " + iCycleCount + " - Gain: " + dGain + " dB" + " - Start: " + dStart + " us" + " - Range: " + dRange + " us";
                textBoxRead.Text = String.Format("Cycles: {0} - Gain: {1:F1} dB - Start: {2:F1} us - Range: {3:F1} us", iCycleCount, dGain, dStart * 1e6, dRange * 1e6);
            }

        }

        private void buttonWrite_Click(object sender, EventArgs e)//将设置的参数写入
        {
            double dGain, dStart, dRange;
            bool bRet = true;

            if (checkBoxMaster.Checked || (prevDevice != null))
            {
                buttonWrite_MasterClick();
                return;
            }
            //use Lock/Unlock for each device
            if (!GetGainStartRange(out dGain, out dStart, out dRange))
            {
                MessageBox.Show("Error to convert string to double.");
                return;
            }
            if (hwDeviceOEMPA.LockDevice())
            {
                bRet = SetGainStartRange(dGain, dStart, dRange);
                if (!hwDeviceOEMPA.UnlockDevice())
                    bRet = false;
            }
            else
                bRet = false;
            if (!bRet)
                MessageBox.Show("Communication error!");
        }

        public bool SetGainStartRange(double dGain, double dStart, double dRange)//设置参数
        {
            bool bRet = true;
            int iPointCount, iPointFactor;
            csEnumCompressionType eCompressionType;

            eCompressionType = csEnumCompressionType.csCompression;
            if (!hwDeviceOEMPA.SetGainDigital(0, ref dGain))
                bRet = false;
            if (!hwDeviceOEMPA.SetAscanStart(0, ref dStart))
                bRet = false;
            iPointCount = 0;
            iPointFactor = 0;
            if (!hwDeviceOEMPA.SetAscanRange(0, ref dRange, out eCompressionType, out iPointCount, out iPointFactor))
                bRet = false;
            return bRet;
        }

        public bool GetGainStartRange(out double dGain, out double dStart, out double dRange)
        {
            bool bRet = false;

            dGain = 0.0;
            dStart = 0.0;
            dRange = 0.0;
            if (ConvertToDouble(textBoxWriteGain.Text, 1.0, out dGain) &&
                ConvertToDouble(textBoxWriteStart.Text, 1.0, out dStart) &&
                ConvertToDouble(textBoxWriteRange.Text, 1.0, out dRange))
            {
                textBoxWriteGain.Text = dGain.ToString(CultureInfo.InvariantCulture) + " dB";
                textBoxWriteStart.Text = dStart.ToString(CultureInfo.InvariantCulture) + " us";
                textBoxWriteRange.Text = dRange.ToString(CultureInfo.InvariantCulture) + " us";
                bRet = true;
                dStart = dStart * 1.0e-6;
                dRange = dRange * 1.0e-6;
            }
            return bRet;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            String strFileName;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|binary files (*.bin)|*.bin|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            //openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                strFileName = openFileDialog1.FileName;
                csCustomizedOEMPA _csCustomizedAPI = hwDeviceOEMPA.GetCustomizedOEMPA();
                _csCustomizedAPI.ReadFileWriteHW(strFileName);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            String strFileName;
            SaveFileDialog dlg = new SaveFileDialog();

            //dlg.InitialDirectory = "c:\\";
            dlg.Filter = "txt files (*.txt)|*.txt|binary files (*.bin)|*.bin|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            //dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                strFileName = dlg.FileName;
                csCustomizedOEMPA _csCustomizedAPI = hwDeviceOEMPA.GetCustomizedOEMPA();
                _csCustomizedAPI.ReadHWWriteFile(strFileName);
            }
        }

        private void Status_OEM(object sender, EventArgs e)
        {
            String strFileName, line, aux, strUsb3;
            SaveFileDialog dlg = new SaveFileDialog();
            ushort wTemperature;
            ushort[,] awTemperature;
            int iSensorCountMax = 0, iSensorCount = 0, iBoardCount = 0;
            uint ulDigitalInput = 0;
            bool bRet = true, bIO = false;
            csVersion version;
            csOptionsCom com;
            csOptionsTCP tcp;
            csOptionsFlash flash;
            char verChar;

            if (!hwDeviceOEMPA.GetSWDeviceOEMPA().GetTemperatureCount(out iBoardCount, out iSensorCountMax))
                return;
            awTemperature = new ushort[iBoardCount, iSensorCountMax];
            if (awTemperature == null)
                return;

            //dlg.InitialDirectory = "c:\\";
            dlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            //dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                strFileName = dlg.FileName;
                unsafe
                {
                    fixed (ushort* pwTemperature = awTemperature)
                    {
                        if (hwDeviceOEMPA.LockDevice())
                        {
                            ulDigitalInput = 0;
                            bIO = hwDeviceOEMPA.GetDigitalInput(&ulDigitalInput);
                            for (int iIndexBoard = 0; iIndexBoard < iBoardCount; iIndexBoard++)
                            {
                                if (!hwDeviceOEMPA.GetSWDeviceOEMPA().GetTemperatureSensorCount(iIndexBoard, out iSensorCount))
                                    return;
                                for (int iIndexSensor = 0; iIndexSensor < iSensorCount; iIndexSensor++)
                                {
                                    pwTemperature[iIndexBoard * iSensorCountMax + iIndexSensor] = 0xffff;
                                    if (!hwDeviceOEMPA.GetTemperatureSensor(iIndexBoard, iIndexSensor, &pwTemperature[iIndexBoard * iSensorCountMax + iIndexSensor]))
                                        bRet = false;//error
                                }
                            }
                            if (!hwDeviceOEMPA.UnlockDevice())
                                bRet = false;
                        }
                        else
                            bRet = false;
                    }
                }
                if (!bRet)
                {
                    MessageBox.Show("Communication error!");
                    return;
                }
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(strFileName))
                {
                    csKernelDriver.GetVersion(out aux);
                    verChar = (char)csKernelDriver.GetVersionLetter();
                    aux += verChar;
                    line = String.Format("====SOFTWARE====\r\nDriverVersion={0}\r\n====HARDWARE====", aux);
                    file.WriteLine(line);
                    if (hwDeviceOEMPA.GetSWDeviceOEMPA().GetFWUSB3Version() > 0)
                        strUsb3 = String.Format("FWUSB3=0x{0,8:X}\r\n", hwDeviceOEMPA.GetSWDeviceOEMPA().GetFWUSB3Version());
                    else
                        strUsb3 = "";
                    line = String.Format("SystemType={0}:{1}\r\nIOBoard={2}\r\nEncoderDecimal={3}\r\nFWId = 0x{4,4:X}\r\n{5}PulserPowerMax={6}\r\nPulserPowerCurrent={7}",
                        hwDeviceOEMPA.GetSWDeviceOEMPA().GetApertureCountMax(),
                        hwDeviceOEMPA.GetSWDeviceOEMPA().GetElementCountMax(),
                        hwDeviceOEMPA.GetSWDeviceOEMPA().IsIOBoardEnabled(),
                        hwDeviceOEMPA.GetSWDeviceOEMPA().IsEncoderDecimal(),
                        hwDeviceOEMPA.GetSWDeviceOEMPA().GetFirmwareId(),
                        strUsb3,
                        hwDeviceOEMPA.GetSWDeviceOEMPA().GetPulserPowerMax(),
                        hwDeviceOEMPA.GetSWDeviceOEMPA().GetPulserPowerCurrent()
                        );
                    file.WriteLine(line);
                    hwDeviceOEMPA.GetSWDeviceOEMPA().GetSerialNumber(out aux);
                    line = String.Format("SerialNumber={0}", aux);
                    file.WriteLine(line);
                    hwDeviceOEMPA.GetSWDeviceOEMPA().GetEmbeddedVersion(out version);
                    line = String.Format("EmbeddedVersion={0}.{1}.{2}.{3}", version.uMajorMSB, version.uMajorLSB, version.uMinorMSB, version.uMinorLSB);
                    file.WriteLine(line);
                    hwDeviceOEMPA.GetSWDeviceOEMPA().GetOptionsCom(out com);
                    line = String.Format("EthernetSpeed={0}", com.uEthernetSpeed);
                    file.WriteLine(line);
                    hwDeviceOEMPA.GetSWDeviceOEMPA().GetOptionsTCP(out tcp);
                    line = String.Format("TCP={0}.{1}.{2}.{3}", tcp.uOption, tcp.uMss, tcp.uWndSize, tcp.uScale);
                    file.WriteLine(line);
                    hwDeviceOEMPA.GetSWDeviceOEMPA().GetOptionsFlash(out flash);
                    line = String.Format("flash => Manufacturer=0x{0:X2} memType={1:X2} memCapacity={2:X2}", flash.uManufacturer, flash.uMemoryType, flash.uMemoryCapacity);
                    file.WriteLine(line);
                    line = String.Format("TimeOffsetCorrectionSupported={0}", hwDeviceOEMPA.GetSWDeviceOEMPA().IsTimeOffsetSupported());
                    file.WriteLine(line);
                    if (bIO)
                    {
                        line = String.Format("DigitalInputs = 0x{0,2:X}\r\n", ulDigitalInput);
                        file.WriteLine(line);
                    }
                    line = "\nBoard\tSensor\tTemperature\n";
                    file.WriteLine(line);
                    for (int iIndexBoard = 0; iIndexBoard < iBoardCount; iIndexBoard++)
                    {
                        if (!hwDeviceOEMPA.GetSWDeviceOEMPA().GetTemperatureSensorCount(iIndexBoard, out iSensorCount))
                            return;
                        for (int iIndexSensor = 0; iIndexSensor < iSensorCount; iIndexSensor++)
                        {
                            wTemperature = awTemperature[iIndexBoard, iIndexSensor];
                            if (wTemperature == 0xffff)
                                continue;
                            line = String.Format("{0}\t{1}\t{2} °C", iIndexBoard, iIndexSensor, wTemperature);
                            file.WriteLine(line);
                        }
                    }
                    for (int iGate = 0; iGate < 4; iGate++)
                    {
                        line = String.Format("\n\nGate {0} (cycle Amp Tof)\n", iGate);
                        file.WriteLine(line);
                        for (int iCycle = 0; iCycle < m_iCycleCount; iCycle++)
                        {
                            if (m_bAcquisitionCscanAmp != null && m_bAcquisitionCscanAmp[iGate + 4 * iCycle])
                            {
                                if (m_bAcquisitionCscanAmp != null && m_bAcquisitionCscanTof[iGate + 4 * iCycle])
                                {
                                    line = String.Format("\t{0}\t{1}\t{2}\n", iCycle, m_sAcquisitionCscanAmp[iGate + 4 * iCycle], m_sAcquisitionCscanTof[iGate + 4 * iCycle]);
                                    file.WriteLine(line);
                                }
                                else
                                {
                                    line = String.Format("\t{0}\t{1}\n", iCycle, m_sAcquisitionCscanAmp[iGate + 4 * iCycle]);
                                    file.WriteLine(line);
                                }
                            }
                        }
                    }
                    file.Close();
                }
            }
        }

        private void buttonStatus_Click(object sender, EventArgs e)//状态读取
        {
            csHWDeviceOEMPA1 hwDeviceOEMPA1 = hwDeviceOEMPA as csHWDeviceOEMPA1;
            csHWDeviceOEMPA2 hwDeviceOEMPA2 = hwDeviceOEMPA as csHWDeviceOEMPA2;

            Status_OEM(sender, e);
            switch (hwDeviceOEMPA.GetSWDevice().GetHardware())
            {
                default: return;
                case csEnumHardware.csOEMPA1:
                case csEnumHardware.csOEMMC1:
                    if (hwDeviceOEMPA1 == null)
                        return;
                    break;
                case csEnumHardware.csOEMPA2:
                    if (hwDeviceOEMPA2 == null)
                        return;
                    break;
            }
        }

        private void comboBoxSynchronisation_SelectedIndexChanged(object sender, EventArgs e)
        {
            csEnumOEMPATrigger eTrig = csEnumOEMPATrigger.csOEMPAInternal;
            csEnumOEMPARequestIO eReqIO = csEnumOEMPARequestIO.csOEMPAOnDigitalInputOnly;
            csEnumDigitalInput digInput;
            csEnumEncoderType eEncoder1Type, eEncoder2Type;
            bool bRet = true;

            if (m_bCallback || hwDeviceOEMPA == null || !hwDeviceOEMPA.GetSWDevice().IsConnected())
                return;

            // 1. 自动写分辨率和步距
            if (m_iComboSynchronisation == 0 && comboBoxSynchronisation.SelectedIndex > 0)
            {
                buttonWriteResolution_Click(sender, e);
                buttonWriteStep_Click(sender, e);
            }

            switch (comboBoxSynchronisation.SelectedIndex)
            {
                case 1: eTrig = csEnumOEMPATrigger.csOEMPAEncoder; eReqIO = csEnumOEMPARequestIO.csOEMPAOnDigitalInputAndCycle; break;
                case 2: eTrig = csEnumOEMPATrigger.csOEMPAInternal; eReqIO = csEnumOEMPARequestIO.csOEMPAOnDigitalInputAndCycle; break;
                case 3: eTrig = csEnumOEMPATrigger.csOEMPAExternalSequence; eReqIO = csEnumOEMPARequestIO.csOEMPAOnDigitalInputAndCycle; break;
                case 4: eTrig = csEnumOEMPATrigger.csOEMPAExternalCycleSequence; eReqIO = csEnumOEMPARequestIO.csOEMPAOnDigitalInputAndCycle; break;
                case 0: eTrig = csEnumOEMPATrigger.csOEMPAInternal; eReqIO = csEnumOEMPARequestIO.csOEMPAOnDigitalInputOnly; break;
            }

            if (!hwDeviceOEMPA.LockDevice())
            {
                MessageBox.Show("LockDevice 失败");
                return;
            }

            try
            {
                // 1. 释放引脚
                digInput = csEnumDigitalInput.csDigitalInputOff;
                hwDeviceOEMPA.SetEncoderWire1(0, ref digInput);
                hwDeviceOEMPA.SetEncoderWire2(0, ref digInput);
                hwDeviceOEMPA.SetEncoderWire1(1, ref digInput);
                hwDeviceOEMPA.SetEncoderWire2(1, ref digInput);
                hwDeviceOEMPA.SetExternalTriggerCycle(ref digInput);
                hwDeviceOEMPA.SetExternalTriggerSequence(ref digInput);

                // 2. 设置触发
                hwDeviceOEMPA.SetTriggerMode(ref eTrig);
                if (!bSlave) hwDeviceOEMPA.SetRequestIO(ref eReqIO);

                // 3. 编码器专用配置
                if (comboBoxSynchronisation.SelectedIndex == 1)
                {
                    eEncoder1Type = eEncoder2Type = csEnumEncoderType.csEncoderQuadrature;
                    hwDeviceOEMPA.SetEncoderType(ref eEncoder1Type, ref eEncoder2Type);

                    // 引脚
                    digInput = csEnumDigitalInput.csDigitalInput01; hwDeviceOEMPA.SetEncoderWire1(0, ref digInput);
                    digInput = csEnumDigitalInput.csDigitalInput02; hwDeviceOEMPA.SetEncoderWire2(0, ref digInput);
                    digInput = csEnumDigitalInput.csDigitalInput03; hwDeviceOEMPA.SetEncoderWire1(1, ref digInput);
                    digInput = csEnumDigitalInput.csDigitalInput04; hwDeviceOEMPA.SetEncoderWire2(1, ref digInput);

                    // 【关键】分辨率和步距在 buttonWrite 里已写，这里不再重复
                }

                // 4. 最后统一 Flush
                bRet = hwDeviceOEMPA.Flush();
            }
            finally
            {
                hwDeviceOEMPA.UnlockDevice();
            }

            UpdateDialog();
            if (!bRet) MessageBox.Show("通信错误！");
            m_iComboSynchronisation = comboBoxSynchronisation.SelectedIndex;
        }



        public int iround(double x)
        {
            int nx = (int)x;
            if (x > 0)
            {
                if ((x - nx) > 0.5)
                    return nx + 1;
                else
                    return nx;
            }
            else
            {
                if ((x - nx) < -0.5)
                    return nx - 1;
                else
                    return nx;
            }
        }

        private void buttonWriteResolution_Click(object sender, EventArgs e)//写入1step/mm
        {
            bool bRet = UpdateResolution();
            if (!bRet)
                MessageBox.Show("Communication error!");
        }

        private bool UpdateResolution()
        {
            bool bRet = true;
            Int32 lResolutionX;
            double dAux;

            if (m_bCallback) return false;

            if (!ConvertToDouble(textBoxResolution.Text, 1.0, out dAux))
                return false;

            lResolutionX = iround(dAux);
            textBoxResolution.Text = String.Format("{0} step/mm", lResolutionX);

            if (hwDeviceOEMPA.LockDevice())
            {
                try
                {
                    if (!hwDeviceOEMPA.GetSWDevice().GetSWEncoder(0).lSetResolution(lResolutionX))
                        bRet = false;
                    if (!hwDeviceOEMPA.GetSWDevice().GetSWEncoder(1).lSetResolution(lResolutionX))
                        bRet = false;

                    // 【关键！】必须 Flush！
                    if (!hwDeviceOEMPA.Flush())
                        bRet = false;
                }
                finally
                {
                    hwDeviceOEMPA.UnlockDevice();
                }
            }

            return bRet;
        }
        private void buttonWriteStep_Click(object sender, EventArgs e)
        {
            if (m_bCallback) return;

            if (!double.TryParse(textBoxStep.Text.Replace("mm", "").Trim(), out double stepMm))
            {
                MessageBox.Show("步距格式错误！");
                return;
            }

            double stepInMeters = stepMm / 1000.0;

            if (hwDeviceOEMPA.LockDevice())
            {
                try
                {
                    if (!hwDeviceOEMPA.SetTriggerEncoderStep(ref stepInMeters))
                    {
                        MessageBox.Show("SetTriggerEncoderStep 失败！");
                        return;
                    }

                    if (!hwDeviceOEMPA.Flush())
                    {
                        MessageBox.Show("Flush 失败！");
                        return;
                    }

                    Debug.WriteLine($"步距设置成功：{stepMm} mm");
                }
                finally
                {
                    hwDeviceOEMPA.UnlockDevice();
                }
            }
        }



        private void checkBoxMaster_CheckedChanged(object sender, EventArgs e)
        {
            FrmTestTool dlg;
            bool bError = false;

            if (checkBoxMaster.Checked)
            {
                dlg = firstDialog;
                while (dlg.nextDevice != null)
                {
                    if (!dlg.nextDevice.hwDeviceOEMPA.SlaveConnect(m_iHWDeviceId))
                        bError = true;
                    dlg = dlg.nextDevice;
                    dlg.UpdateSlave(true);
                }
                if (bError)
                    MessageBox.Show("Error to enable the master");
            }
            else
            {
                dlg = firstDialog;
                while (dlg.nextDevice != null)
                {
                    if (dlg.nextDevice.hwDeviceOEMPA.GetHardwareLink() == csEnumHardwareLink.csSlave)
                    {
                        if (!dlg.nextDevice.hwDeviceOEMPA.SlaveDisconnect())
                            bError = true;
                    }
                    dlg = dlg.nextDevice;
                    dlg.UpdateSlave(false);
                }
                if (bError)
                    MessageBox.Show("Error to disable the master");
            }
        }
        private void UpdateSlave(bool bEnable)
        {
            bSlave = bEnable;
            if (bEnable)
                comboBoxSynchronisation.SelectedIndex = 4;
            else
                comboBoxSynchronisation.SelectedIndex = 0;
            UpdateDialog();
        }



        private void textBoxIPAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonMsgBox_Click(object sender, EventArgs e)
        {
            if (msgBox == null)
                msgBox = new FormMsgBox(hwDeviceOEMPA);
            if (msgBox != null)
                msgBox.Show();
        }

        private void buttonResetEncoder_Click(object sender, EventArgs e)
        {
            bool bRet = true;

            if (hwDeviceOEMPA.LockDevice())
            {
                if (!hwDeviceOEMPA.ResetTrackingTable())
                    bRet = false;
                if (!hwDeviceOEMPA.ResetEncoder(0))
                    bRet = false;
                if (!hwDeviceOEMPA.UnlockDevice())
                    bRet = false;
            }
            else
                bRet = false;
            if (!bRet)
                MessageBox.Show("Communication error!");
        }


        private void buttonWizardToFile_Click(object sender, EventArgs e)
        {
            switch (comboBoxWizardType.SelectedIndex)
            {
                case 0:
                case 1:
                    WizardToFilePulseEcho(); break;
                case 2:
                case 3:
                case 4:
                    WizardToFilePitchCatch(); break;
            }
        }

        private void WizardToFilePulseEcho()
        {
            string file;
            SaveFileDialog dlg = new SaveFileDialog();
            DialogResult result;

            if ((wizardTemplate == null) || !wizardCompleted)
                return;
            dlg.Title = "Save OEMPA Files";
            dlg.CheckPathExists = true;
            dlg.DefaultExt = "txt";
            dlg.Filter = "Binary files (*.bin)|*.bin|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dlg.FilterIndex = 2;
            if (wizardTemplate == null)
                return;
            result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                file = dlg.FileName;
                File.Delete(file);
                if (!wizardTemplate.WizardToFile(hwDeviceOEMPA, file))
                    MessageBox.Show("Error to save the file!");
                else
                    wizardTemplate.EditFile(file, false);
            }
        }

        private void WizardToFilePitchCatch()
        {
            string file;
            SaveFileDialog dlg = new SaveFileDialog();
            DialogResult result;

            if ((wizardPitchCatchTemplate == null) || !wizardCompletedPitchCatch)
                return;
            dlg.Title = "Save OEMPA Files";
            dlg.CheckPathExists = true;
            dlg.DefaultExt = "txt";
            dlg.Filter = "Binary files (*.bin)|*.bin|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dlg.FilterIndex = 2;
            if (wizardPitchCatchTemplate == null)
                return;
            result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                file = dlg.FileName;
                File.Delete(file);
                if (!wizardPitchCatchTemplate.WizardToFile(file))
                    MessageBox.Show("Error to save the file!");
                else
                    wizardPitchCatchTemplate.EditFile(file, false);
            }
        }

        private bool buttonWrite_MasterClick()
        {
            FrmTestTool dlg;
            bool bError = false;
            double[] adGain;
            double[] adStart;
            double[] adRange;
            int iCount = 0, iIndex;
            bool bRet = true;

            dlg = firstDialog;
            while (dlg != null)
            {
                iCount++;
                dlg = dlg.nextDevice;
            }

            adGain = new double[iCount];
            adStart = new double[iCount];
            adRange = new double[iCount];

            iIndex = 0;
            dlg = firstDialog;
            while (dlg != null)
            {
                if (!dlg.GetGainStartRange(out adGain[iIndex], out adStart[iIndex], out adRange[iIndex]))
                {
                    MessageBox.Show("Error GetGainStartRange");
                    return false;
                }
                iIndex++;
                dlg = dlg.nextDevice;
            }
            if (bError)
                return false;
            //use Lock/Unlock for the master device only
            if (hwDeviceOEMPA.LockDevice())
            {
                iIndex = 0;
                dlg = firstDialog;
                while (dlg != null)
                {
                    bRet = dlg.SetGainStartRange(adGain[iIndex], adStart[iIndex], adRange[iIndex]);
                    if (!bRet)
                    {
                        MessageBox.Show("Error SetGainStartRange");
                        break;
                    }
                    iIndex++;
                    dlg = dlg.nextDevice;
                }
                if (!hwDeviceOEMPA.UnlockDevice())
                    bRet = false;
            }
            else
                bRet = false;
            if (!bRet)
                MessageBox.Show("Communication error!");
            return false;
        }

        #region 触发方式的事件函数


        #endregion


        public bool ConvertToDouble(string strAux, double dFactor, out double dAux)
        {
            bool bRet = true;

            Console.WriteLine($"Received strAux: '{strAux}'"); // 添加此行

            dAux = 0.0;
            if (string.IsNullOrWhiteSpace(strAux))
            {
                Console.WriteLine("Error: Input string is null or empty.");
                return false;
            }
            //remove the unit
            if (strAux.IndexOf(' ') >= 0)
            {
                strAux = strAux.Substring(0, strAux.IndexOf(' '));
            }
            strAux = Regex.Replace(strAux, "[^0-9.,-]", ""); // 移除非数字、点、逗号和负号外的字符
            strAux = Number(strAux);
            try
            {
                dAux = Convert.ToDouble(strAux, CultureInfo.InvariantCulture) * dFactor;
            }
            catch (Exception ex)
            {
                Unreferenced.Parameter(ex);
                Console.WriteLine($"Conversion failed: {ex.Message}");
                bRet = false;
            }
            return bRet;
        }

        private String Number(String strIn)
        {
            String strAux;
            int iIndex;

            //iIndex = strIn.IndexOf(" ");
            //if (iIndex < 0)
            //    return strIn;
            strAux = strIn;//.Substring(0, iIndex);
            iIndex = strAux.IndexOf(",");
            if (iIndex < 0)
                return strAux;
            strAux = strAux.Replace(',', '.');
            return strAux;
        }


        // 设备加锁/解锁（确保线程安全）
        private bool LockDevice()
        {
            return hwDeviceOEMPA.LockDevice();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            double dRange = 0;
            double dGain = 0;
            double dStart = 0;
            if (ConvertToDouble(textBoxWriteGain.Text, 1.0, out dGain) &&
                ConvertToDouble(textBoxWriteStart.Text, 1.0, out dStart) &&
                ConvertToDouble(textBoxWriteRange.Text, 1.0, out dRange))
            {
                textBoxWriteGain.Text = dGain.ToString(CultureInfo.InvariantCulture) + " dB";
                textBoxWriteStart.Text = dStart.ToString(CultureInfo.InvariantCulture) + " us";
                textBoxWriteRange.Text = dRange.ToString(CultureInfo.InvariantCulture) + " us";
                //bRet = true;
                dStart = dStart * 1.0e-6;
                dRange = dRange * 1.0e-6;
            }
            double dt = 2e-8;
            int counts = 0;


            //counts = (int)((dRange / dt) + 1);
            counts = (int)Math.Round(dRange / dt) + 1;

            // 2. 动态创建矩阵
            FMC_DataMatrix = new int[counts, 64 * 64];
        }

        private void FMC_lengthwrite_logic()
        {
            double dRange = 0;
            double dGain = 0;
            double dStart = 0;

            if (ConvertToDouble(textBoxWriteGain.Text, 1.0, out dGain) &&
                ConvertToDouble(textBoxWriteStart.Text, 1.0, out dStart) &&
                ConvertToDouble(textBoxWriteRange.Text, 1.0, out dRange))
            {
                textBoxWriteGain.Text = dGain.ToString(CultureInfo.InvariantCulture) + " dB";
                textBoxWriteStart.Text = dStart.ToString(CultureInfo.InvariantCulture) + " us";
                textBoxWriteRange.Text = dRange.ToString(CultureInfo.InvariantCulture) + " us";
                dStart *= 1.0e-6;
                dRange *= 1.0e-6;
            }

            double dt = 2e-8;
            int counts = (int)Math.Round(dRange / dt) + 1;

            FMC_DataMatrix = new int[counts, 64 * 64];
        }

        // 事件绑定调用
        private void FMC_lengthwrite(object sender, EventArgs e)
        {
            FMC_lengthwrite_logic();
        }

        private void checkBoxConnect_CheckedChanged(object sender, EventArgs e)
        {
            csHWDevice hwDevice;
            csSWDevice swDevice;
            csSWDeviceOEMPA swDeviceOEMPA;
            String strIPAddress = textBoxIPAddress.Text;
            String strAddress;
            ushort usValue;
            bool bRet = true;
            UInt32 dwProcessId;

            try
            {
                usValue = Convert.ToUInt16(textBoxPort.Text);//设置端口号连接
            }
            catch (Exception ex)
            {
                Unreferenced.Parameter(ex);
                usValue = 5001;
                bRet = false;
            }
            if (!bRet)
            {
                MessageBox.Show("Error to convert string to ushort!");
                return;
            }
            if (hwDeviceOEMPA == null)
                return;
            if (bConnectEnter)
                return;
            bConnectEnter = true;
            //theTimer.Stop();
            hwDevice = hwDeviceOEMPA.GetHWDevice();
            swDevice = hwDeviceOEMPA.GetSWDevice();
            swDeviceOEMPA = hwDeviceOEMPA.GetSWDeviceOEMPA();
            if (checkBoxConnect.Checked)
            {
                if (!swDevice.IsConnected())
                {
                    if (csHWDeviceOEMPA.IsMultiProcessConnected(strIPAddress, out dwProcessId))
                        csHWDeviceOEMPA.DisconnectMultiProcess(strIPAddress, dwProcessId);
                    dataLostAscan = 0;
                    dataLostCscan = 0;
                    switch (swDevice.GetHardware())
                    {
                        default:
                            bConnectEnter = false;
                            return;
                        case csEnumHardware.csOEMPA1:
                        case csEnumHardware.csOEMMC1: strAddress = strIPAddress + ":" + usValue.ToString(); break;
                        case csEnumHardware.csOEMPAX: strAddress = strIPAddress + ":" + usValue.ToString(); break;
                        case csEnumHardware.csOEMPAmini:
                        case csEnumHardware.csOEMPAmax:
                            if (swDevice.GetCommunication() == csEnumCommunication.csTCP)
                                strAddress = strIPAddress + ":" + usValue.ToString();
                            else
                                strAddress = strIPAddress;
                            break;
                        case csEnumHardware.csOEMMC2:
                        case csEnumHardware.csOEMMCu:
                        case csEnumHardware.csOEMMCuF: strAddress = strIPAddress + ":" + usValue.ToString(); break;
                    }
                    bRet = swDeviceOEMPA.SetAddress(strAddress);
                    bRet = bRet && swDevice.SetConnectionState(csEnumConnectionState.csConnected, true);
                    if (bRet)
                    {
                        bProcessConnection = true;
                        comboBoxWizardType.SelectedIndex = -1;

                        IniFile ini2 = new IniFile(wizardFileNamePitchCatch);
                        CheckWizardTemplate();
                        if ((wizardPitchCatchTemplate != null) && ini2.TemplateRead(ref wizardPitchCatchTemplate))
                            comboBoxWizardType.SelectedIndex = (int)wizardPitchCatchTemplate.Scan.PitchCatchDefinition + 2;
                        else
                        {
                            File.Delete(wizardFileNamePitchCatch);
                            IniFile ini = new IniFile(wizardFileName);
                            if ((wizardTemplate != null) && ini.TemplateRead(ref wizardTemplate))
                            {
                                comboBoxWizardType.SelectedIndex = 0;
                                if (ini.IsTemplateSector())
                                    comboBoxWizardType.SelectedIndex = 1;
                            }
                            else
                                File.Delete(wizardFileName);
                        }
                        bProcessConnection = false;
                    }
                }
            }
            else
            {
                if (swDevice.IsConnected())
                {
                    bRet = swDevice.SetConnectionState(csEnumConnectionState.csDisconnected, true);
                }
            }
            //theTimer.Enabled = true;
            if (swDevice.IsConnected())
                checkBoxConnect.Checked = true;
            else
                checkBoxConnect.Checked = false;
            bConnectEnter = false;
            if (!bRet)
                MessageBox.Show("Communication error!");




            //// 添加连接结果提示
            //if (bRet && swDevice.IsConnected())
            //{
            //    MessageBox.Show("连接成功！", "连接状态", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    MessageBox.Show("连接失败！", "连接状态", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}


        }

        private void comboBoxOEMType_SelectedIndexChanged(object sender, EventArgs e)
        {
            String strIPAddress = textBoxIPAddress.Text;
            csSWDevice swDevice;


            hwDeviceOEMPA = null;
            hwDeviceOEMPA1 = null;
            switch (comboBoxOEMType.SelectedIndex)
            {

                case 0:
                    hwDeviceOEMPA1 = new csHWDeviceOEMPA1();
                    hwDeviceOEMPA = hwDeviceOEMPA1;
                    textBoxIPAddress.Text = "192.168.1.11";
                    textBoxPort.Visible = true;
                    textBoxPort.Text = "5001";
                    break;
                case 1:
                    hwDeviceOEMPA = new csHWDeviceOEMPA2();
                    textBoxIPAddress.Text = "pcie://192.168.1.11";
                    textBoxPort.Visible = false;
                    break;
                case 2:
                    hwDeviceOEMPA = new csHWDeviceOEMPAmini();
                    swDevice = hwDeviceOEMPA.GetSWDevice();
                    if (swDevice.GetCommunication() == csEnumCommunication.csTCP)
                    {
                        textBoxPort.Visible = true;
                        textBoxIPAddress.Text = "192.168.1.11";
                        textBoxPort.Text = "5001";
                    }
                    else
                    {
                        textBoxPort.Visible = false;
                        textBoxIPAddress.Text = "udp://192.168.1.11";
                    }
                    break;
                case 3:
                    hwDeviceOEMPA = new csHWDeviceOEMPAmax();
                    swDevice = hwDeviceOEMPA.GetSWDevice();
                    textBoxPort.Visible = true;
                    textBoxIPAddress.Text = "192.168.1.11";
                    textBoxPort.Text = "4096";
                    break;
                case 4:
                    hwDeviceOEMPA = new csHWDeviceOEMMC2();
                    swDevice = hwDeviceOEMPA.GetSWDevice();
                    textBoxPort.Visible = true;
                    textBoxIPAddress.Text = "192.168.1.11";
                    textBoxPort.Text = "5001";
                    break;
                case 5:
                    hwDeviceOEMPA = new csHWDeviceOEMMCu();
                    swDevice = hwDeviceOEMPA.GetSWDevice();
                    textBoxPort.Visible = true;
                    textBoxIPAddress.Text = "192.168.1.11";
                    textBoxPort.Text = "5001";
                    break;
                case 6:
                    hwDeviceOEMPA = new csHWDeviceOEMMCuF();
                    swDevice = hwDeviceOEMPA.GetSWDevice();
                    textBoxPort.Visible = true;
                    textBoxIPAddress.Text = "192.168.1.11";
                    textBoxPort.Text = "5001";
                    break;
                default: MessageBox.Show("Error of deviceType!"); return;
            }

            if (hwDeviceOEMPA == null)
            {
                MessageBox.Show("设备对象创建失败，请检查csDriverOEMPA.dll及其依赖项！");
                return;
            }

            m_iHWDeviceId = hwDeviceOEMPA.GetDeviceId();
            m_iCycleCount = 0;
            initWizardFileName();
            InitDialog();
        }

        private void hslPanelTextBack1_Paint(object sender, PaintEventArgs e)
        {

        }
        private List<ScanPointData> _allPointDataList;
        public class ScanPointData
        {
            public int Index { get; set; }

            // txt 原始三列
            public double MainSubMove { get; set; }   // 第一列：主轴+副轴一起动
            public double ZMove { get; set; }         // 第二列：Z轴
            public double SubOnlyMove { get; set; }   // 第三列：副轴单独动

            // 实际记录位置
            public double PositionMain { get; set; }
            public double PositionSub { get; set; }
            public double PositionZ { get; set; }

            // 速度
            public double SpeedMain { get; set; }
            public double SpeedSub { get; set; }
            public double SpeedZ { get; set; }

            // FMC数据
            public int[,] FMCData { get; set; }
        }
        private bool _isProcessingMotionCapture = false;
        private async void checkBoxMotionCapture_CheckedChanged(object sender, EventArgs e)
        {

            if (_isProcessingMotionCapture) return;
            if (!checkBoxMotionCapture.Checked) return;

            _isProcessingMotionCapture = true;
            checkBoxMotionCapture.Enabled = false;
            try
            {
                // 1. 加载圆形/离散扫描路径
                hslBtnLoadLine.PerformClick();

                if (_ACS == null)
                {
                    MessageBox.Show("运动控制器未初始化！");
                    return;
                }

                if (_currentMotionMatrix == null || _currentMotionMatrix.GetLength(0) == 0)
                {
                    MessageBox.Show("扫描路径未加载！");
                    return;
                }

                checkBoxMotionCapture.Enabled = false;

                // 2. 初始化 FMC 数据长度
                //FMC_lengthwrite_logic();

                //double scanInterval = GetScanIntervalFromUI();

                int totalPoints = _currentMotionMatrix.GetLength(0);

                _allPointDataList = new List<ScanPointData>();

                TimeSpan movementTimeout = TimeSpan.FromSeconds(300);
                int timeoutMs = (int)movementTimeout.TotalMilliseconds;

                var swDevice = hwDeviceOEMPA.GetSWDevice();
                //int minCaptureMs = 5;      // 至少采集5秒
                //int captureTimeoutMs = 30; // 最长等待30秒
                _scanCancellationTokenSource = new CancellationTokenSource();

                CancellationToken cancellationToken =
                    _scanCancellationTokenSource.Token;
                for (int i = 0; i < totalPoints; i++)
                {

                    textBoxMotionCapture.Text = $"正在运行第 {i + 1}/{totalPoints} 点";
                    Application.DoEvents();

                    //await MoveAllAxesAsync(i, movementTimeout, cts.Token);
                    using (var cts = new CancellationTokenSource())
                    {
                        await MoveAllAxesAsync(i, movementTimeout, cts.Token);
                        //await WaitForScanCompletionAsync(i, cancellationToken);

                        // 运动完成后等待平台稳定
                        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);
                    }

                    var tcs = new TaskCompletionSource<bool>();
                    // =========================
                    // 当前点 FMC 采集
                    // =========================
                    FMC_lengthwrite_logic();

                    _currentElementIndex = 0;
                    _fmcWrittenColumns = 0;
                    _fmcDoneTcs = new TaskCompletionSource<bool>();
                    _allowFmcCapture = true;

                    swDevice.EnablePulser(true);
                    
                    await Task.Run(async () =>
                    {
                        while (_currentElementIndex < 64 * 64)
                        {
                            await Task.Delay(5);
                        }

                        tcs.TrySetResult(true);
                    });
                    //await _fmcDoneTcs.Task;
                    await tcs.Task;
                    // 复制当前点 FMC 数据
                    int rows = FMC_DataMatrix.GetLength(0);
                    int cols = FMC_DataMatrix.GetLength(1);

                    int[,] fmcCopy = new int[rows, cols];
                    Array.Copy(FMC_DataMatrix, fmcCopy, FMC_DataMatrix.Length);

                    double mainSubMove = _currentMotionMatrix[i, 0];
                    double zMove = _currentMotionMatrix[i, 1];
                    double subOnlyMove = _currentMotionMatrix[i, 2];

                    double speedMain = Convert.ToDouble(txtSpeedX.Text);
                    double speedSub = Convert.ToDouble(txtSpeedY.Text);
                    double speedZ = Convert.ToDouble(txtSpeedZ.Text);

                    _allPointDataList.Add(new ScanPointData
                    {
                        Index = i + 1,

                        MainSubMove = mainSubMove,
                        ZMove = zMove,
                        SubOnlyMove = subOnlyMove,

                        PositionMain = mainSubMove,
                        PositionSub = mainSubMove + subOnlyMove,
                        PositionZ = zMove,

                        SpeedMain = speedMain,
                        SpeedSub = speedSub,
                        SpeedZ = speedZ,

                        FMCData = fmcCopy
                    });


                    swDevice.EnablePulser(false);
                    _allowFmcCapture = false;

                    //// 复制当前点 FMC 数据
                    //int rows = FMC_DataMatrix.GetLength(0);
                    //int cols = FMC_DataMatrix.GetLength(1);

                    //int[,] fmcCopy = new int[rows, cols];
                    //Array.Copy(FMC_DataMatrix, fmcCopy, FMC_DataMatrix.Length);

                    //double mainSubMove = _currentMotionMatrix[i, 0];
                    //double zMove = _currentMotionMatrix[i, 1];
                    //double subOnlyMove = _currentMotionMatrix[i, 2];

                    //double speedMain = Convert.ToDouble(txtSpeedX.Text);
                    //double speedSub = Convert.ToDouble(txtSpeedY.Text);
                    //double speedZ = Convert.ToDouble(txtSpeedZ.Text);

                    //_allPointDataList.Add(new ScanPointData
                    //{
                    //    Index = i + 1,

                    //    MainSubMove = mainSubMove,
                    //    ZMove = zMove,
                    //    SubOnlyMove = subOnlyMove,

                    //    PositionMain = mainSubMove,
                    //    PositionSub = mainSubMove + subOnlyMove,
                    //    PositionZ = zMove,

                    //    SpeedMain = speedMain,
                    //    SpeedSub = speedSub,
                    //    SpeedZ = speedZ,

                    //    FMCData = fmcCopy
                    //});
                }

                SaveallScandataToMat();

                MessageBox.Show("圆形扫描及 FMC 数据保存完成！");
            }
            catch (Exception ex)
            {
                try
                {
                    hwDeviceOEMPA.GetSWDevice().EnablePulser(false);
                    _allowFmcCapture = false;
                }
                catch { }

                MessageBox.Show($"圆形扫描失败：{ex.Message}");
            }
            finally
            {
                _allowFmcCapture = false;

                try
                {
                    var swDevice = hwDeviceOEMPA?.GetSWDevice();
                    swDevice?.EnablePulser(false);
                }
                catch { }

                checkBoxMotionCapture.Enabled = true;
                checkBoxMotionCapture.Checked = false;
                textBoxMotionCapture.Text = "扫描完成";

                _isProcessingMotionCapture = false;
            }
        }
        private void SaveallScandataToMat()
        {
            //if (_allPointDataList == null || _allPointDataList.Count == 0)
            //{
            //    MessageBox.Show("没有可保存的数据！");
            //    return;
            //}

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "MAT文件 (*.mat)|*.mat";
                dialog.FileName = $"CircleScan_All_{DateTime.Now:yyyyMMdd_HHmmss}.mat";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                var variables = new Dictionary<string, Matrix<double>>();

                foreach (var point in _allPointDataList)
                {
                    string prefix = $"Point_{point.Index:D2}";

                    int rows = point.FMCData.GetLength(0);
                    int cols = point.FMCData.GetLength(1);

                    double[,] fmcDouble = new double[rows, cols];

                    for (int r = 0; r < rows; r++)
                    {
                        for (int c = 0; c < cols; c++)
                        {
                            fmcDouble[r, c] = point.FMCData[r, c];
                        }
                    }

                    double[,] motion = new double[1, 9]
                    {
                {
                    point.MainSubMove,
                    point.ZMove,
                    point.SubOnlyMove,

                    point.PositionMain,
                    point.PositionSub,
                    point.PositionZ,

                    point.SpeedMain,
                    point.SpeedSub,
                    point.SpeedZ
                }
                    };

                    variables.Add(
                        $"{prefix}_FMC",
                        Matrix<double>.Build.DenseOfArray(fmcDouble));

                    variables.Add(
                        $"{prefix}_Motion",
                        Matrix<double>.Build.DenseOfArray(motion));
                }

                MatlabWriter.Write(dialog.FileName, variables);

                MessageBox.Show($"保存完成：{dialog.FileName}");
            }
        }
        private void buttonTestEncoder_Click(object sender, EventArgs e)
        {
            // 1. 配置
            comboBoxSynchronisation.SelectedIndex = 1;
            textBoxResolution.Text = "1";
            textBoxStep.Text = "1.0";

            buttonWriteResolution_Click(null, null);
            buttonWriteStep_Click(null, null);

            // 2. 开启激发
            checkBoxPulser.Checked = true;
            //hwDeviceOEMPA.GetSWDevice().AcquisitionAscan_0x00010103();

            MessageBox.Show("配置 + 采集已启动！转动轮盘 → 看 Output 窗口");
        }
        // 在 checkBoxPulser_CheckedChanged 或 一键测试里加：


        private bool UnlockDevice()
        {
            return hwDeviceOEMPA.UnlockDevice();
        }



        /// <summary>
        /// 创建插值矩阵
        /// </summary>
        /// <param name="originalMatrix"></param>
        /// <param name="insertCount"></param>
        /// <returns></returns>
        public static double[,] CreateInterpolatedMatrix(double[,] originalMatrix, int insertCount)
        {
            int originalRows = originalMatrix.GetLength(0);
            int originalCols = originalMatrix.GetLength(1);

            // 计算新数组的行数
            int newRows = originalRows + (originalRows - 1) * insertCount;
            double[,] newMatrix = new double[newRows, originalCols];

            // 填充新数组
            int newRowIndex = 0;
            for (int i = 0; i < originalRows; i++)
            {
                // 复制原数组的当前行
                for (int j = 0; j < originalCols; j++)
                {
                    newMatrix[newRowIndex, j] = Math.Round(originalMatrix[i, j], 3);
                }
                newRowIndex++;

                // 插入n行均分值
                if (i < originalRows - 1) // 如果不是最后一行
                {
                    for (int k = 1; k <= insertCount; k++)
                    {
                        for (int j = 0; j < originalCols; j++)
                        {
                            // 计算当前插入行的均分值
                            double interpolatedValue = Math.Round(
                                originalMatrix[i, j] + (originalMatrix[i + 1, j] - originalMatrix[i, j]) * (double)k / (insertCount + 1),
                                3);
                            newMatrix[newRowIndex, j] = interpolatedValue;
                        }
                        newRowIndex++;
                    }
                }
            }

            return newMatrix;
        }
        //private CancellationTokenSource _cts = new();  // 取消后台任务
        private CancellationTokenSource _cts = new CancellationTokenSource();  // 取消后台任务
        private System.Windows.Forms.Timer _uiTimer;  // UI 刷新定时器
        #region FMC 数据采集
        //取决于开没开启FMC数据采集
        //public int[,] FMC_DataMatrix = new int[1501,64*64];
        public int[,] FMC_DataMatrix;
        private int _transmitIndex = 0; // 发射索引
        private int _currentElementIndex = 0; //已采集的阵元数量
        private int numcount = 0;
        unsafe public int AcquisitionAscan_0x00010103(
            Object pAcquisitionParameter,
            ref csAcqInfoEx acqInfo,
            ref csHeaderStream_0x0001 streamHeader,
            ref csSubStreamAscan_0x0103 ascanHeader,
            void* pBufferMax, void* pBufferMin, void* pBufferSat)
        {


            IntPtr address = (IntPtr)pBufferMax;
            byte* ptr = (byte*)address.ToPointer();



            // 1. 快速锁共享变量
            lock (LockingVar)
            {
                dataSizeAscan += ascanHeader.dataSize * ascanHeader.dataCount;
                encoderAscanX = acqInfo.dEncoder[0];
                encoderAscanY = acqInfo.dEncoder[1];
                sizeTime = System.Environment.TickCount;
            }


            ProcessData(ptr, ascanHeader, acqInfo);
            _currentElementIndex++;
            _fmcWrittenColumns++;
            return 0;
        }

        //public List<int[]> FMC_ColumnData = new List<int[]>();
        private unsafe void ProcessData(byte* ptr, csSubStreamAscan_0x0103 ascanHeader, csAcqInfoEx acqInfo)
        {
            try
            {

                //int num_q = 0;

                //int columnIndex = 0;
                int columnIndex = _currentElementIndex; //程序初始化，会导致_currentElementIndex +1
                //if (columnIndex < 0 || columnIndex >= 64 * 64) return;
                if (columnIndex < 0 || columnIndex >= FMC_TOTAL_COLUMNS)
                {
                    _allowFmcCapture = false;
                    _fmcDoneTcs?.TrySetResult(true);
                    return;
                }
                int[] Ascan_Data = new int[ascanHeader.dataCount];
                //int samples = Ascan_Data.Length;
                //int channels = 4096; // 64 * 64
                //FMC_DataMatrix = new int[samples, channels];
                //if (columnIndex == 0)
                //{
                //    // 初始化矩阵
                //    //int samples = Ascan_Data.GetLength(0);  // 根据你的配置
                //    int samples = Ascan_Data.Length;
                //    int channels = 4096; // 64 * 64
                //    FMC_DataMatrix = new int[samples, channels];
                //}
                // 3. 快速复制数据

                if (ascanHeader.sign)
                {
                    short* sPtr = (short*)ptr;
                    for (int i = 0; i < ascanHeader.dataCount; i++)
                    {
                        Ascan_Data[i] = sPtr[i];
                        FMC_DataMatrix[i, columnIndex] = Ascan_Data[i];
                    }
                }
                else
                {
                    ushort* uPtr = (ushort*)ptr;
                    for (int i = 0; i < ascanHeader.dataCount; i++)
                    {
                        //Ascan_Data[i] = uPtr[i];
                        //FMC_DataMatrix[i, columnIndex] = Ascan_Data[i];
                    }
                }
                //if (FMC_DataMatrix == null)
                //{
                //    // 初始化矩阵
                //    int samples = Ascan_Data.GetLength(0);  // 根据你的配置
                //    int channels = 4096; // 64 * 64
                //    FMC_DataMatrix = new int[samples, channels];
                //}

                // 4. 快速填充矩阵（用 Array.Copy）
                //for (int i = 0; i < ascanHeader.dataCount; i++)
                //{
                //    FMC_DataMatrix[i, columnIndex] = Ascan_Data[i];
                //}
                //_currentElementIndex++;
                //_fmcWrittenColumns++;

                if (_fmcWrittenColumns >= FMC_TOTAL_COLUMNS)
                {
                    _allowFmcCapture = false;
                    _fmcDoneTcs?.TrySetResult(true);
                }

                //FMC_ColumnData.Add(Ascan_Data.ToArray());  // 复制防止修改
                Debug.WriteLine($"数据处理完成: 列 {columnIndex}");
                //num_q=num_q+1;
            }
            catch (Exception ex)
            {
                //Debug.WriteLine($"ProcessData 错误: {ex.Message}");
            }
        }
        // 正确的获取时间点数的方法
        // 正确的获取时间点数的方法
        private int GetTimePoints()
        {
            double dRange = -1.0;
            int timePoints = 1501; // 默认值

            unsafe
            {
                if (hwDeviceOEMPA.LockDevice())
                {
                    // 正确调用GetAscanRange函数
                    bool success = hwDeviceOEMPA.GetAscanRange(0, &dRange, (csEnumCompressionType*)0, (int*)0, (int*)0);
                    hwDeviceOEMPA.UnlockDevice();

                    if (success && dRange > 0)
                    {
                        // 获取时间分辨率dt - 需要根据您的硬件确定如何获取
                        double dt = 2e-8;
                        double rangeInMicroseconds = dRange * 1e6; // 转换为微秒

                        // 计算时间点数
                        timePoints = (int)(rangeInMicroseconds / dt) + 1;

                        // 限制在合理范围内
                        if (timePoints < 100) timePoints = 100;
                        if (timePoints > 10000) timePoints = 10000;
                    }
                }
            }

            return timePoints;
        }

        // 在需要的地方使用
        private void UpdateFMCDataMatrix()
        {
            int timePoints = GetTimePoints();

            // 重新分配FMC数据矩阵
            FMC_DataMatrix = new int[timePoints, 64 * 64];



            Console.WriteLine($"FMC数据矩阵维度: {timePoints} × {64 * 64}");
        }


        //处理IO事件，暂时用不到
        public int AcquisitionIO_0x00010101(Object pAcquisitionParameter, ref csHeaderStream_0x0001 streamHeader, ref csHeaderIO_0x0001 ioHeader)
        {
            return 0;
        }

        //C扫数据采集
        unsafe public int AcquisitionCscan_0x00010X02(Object pAcquisitionParameter, ref csAcqInfoEx acqInfo, ref csHeaderStream_0x0001 streamHeader, ref csSubStreamCscan_0x0X02 cscanHeader, ref csCscanAmp_0x0102[] bufferAmp, ref csCscanAmpTof_0x0202[] bufferAmpTof)
        {
            int iCycle, iGateId;
            byte byAmp;
            bool sign;
            short sTof;
            bool bUpdateCscan = false;//true if you want to dump cscan data in the status file.

            lock (LockingVar)
            {
                switch (cscanHeader.version)
                {
                    case 1: dataSizeCscan += cscanHeader.count * sizeof(long); break;
                    case 2: dataSizeCscan += 2 * cscanHeader.count * sizeof(long); break;
                    default: break;
                }
                encoderCscanX = acqInfo.dEncoder[0];
                encoderCscanY = acqInfo.dEncoder[1];
                sizeTime = System.Environment.TickCount;
                if (bUpdateCscan)
                {
                    //for information here is how to get cscan
                    iCycle = cscanHeader.cycle;//get current cycle for which cscan are delivered.
                    if (cscanHeader.version == 1)
                    {
                        //cscan amplitude only
                        for (int iDataIndex = 0; iDataIndex < cscanHeader.count; iDataIndex++)
                        {
                            iGateId = bufferAmp[iDataIndex].gateId;//get the gateId: 0=first gate, 1=second gate, ...
                            byAmp = bufferAmp[iDataIndex].byAmp;//get the amplitude
                            sign = bufferAmp[iDataIndex].sign;//get the sign: false=unsigned data, true=signed data.
                            if (m_bAcquisitionCscanAmp != null)
                                m_bAcquisitionCscanAmp[iGateId + 4 * iCycle] = true;
                            if (m_sAcquisitionCscanAmp != null)
                                m_sAcquisitionCscanAmp[iGateId + 4 * iCycle] = byAmp;
                        }
                    }
                    else if (cscanHeader.version == 2)
                    {
                        //cscan amplitude + time of flight
                        for (int iDataIndex = 0; iDataIndex < cscanHeader.count; iDataIndex++)
                        {
                            iGateId = bufferAmpTof[iDataIndex].gateId;//get the gateId: 0=first gate, 1=second gate, ...
                            byAmp = bufferAmpTof[iDataIndex].byAmp;//get the amplitude
                            sign = bufferAmpTof[iDataIndex].sign;//get the sign: false=unsigned data, true=signed data.
                            sTof = (short)bufferAmpTof[iDataIndex].wTof;//get the time of flight
                            if (m_bAcquisitionCscanAmp != null)
                                m_bAcquisitionCscanAmp[iGateId + 4 * iCycle] = true;
                            if (m_sAcquisitionCscanAmp != null)
                                m_sAcquisitionCscanAmp[iGateId + 4 * iCycle] = byAmp;
                            if (m_bAcquisitionCscanTof != null)
                                m_bAcquisitionCscanTof[iGateId + 4 * iCycle] = true;
                            if (m_sAcquisitionCscanTof != null)
                                m_sAcquisitionCscanTof[iGateId + 4 * iCycle] = sTof;
                        }
                    }
                }

            }
            return 0;


        }
        public static List<double> GenerateDoubleSequence(double start, double step, double end)
        {
            var list = new List<double>();
            for (double current = start; current <= end + step * 0.5; current += step)
            {
                list.Add(current);
            }
            return list;
        }

        /// <summary>
        /// 全矩阵数据采集
        /// </summary>
        /// <param name="pAcquisitionParameter"></param>
        /// <param name="streamHeader"></param>
        /// <param name="ioHeader"></param>
        /// <returns></returns>
        /// 


        //unsafe public int AcquisitionAscan_0x00020203(Object pAcquisitionParameter, ref csHeaderStream_0x0002 streamHeader, ref csSubStreamAscan_0x0203 ascanHeader, void* pBufferMax, void* pBufferMin, void* pBufferSat)
        //{ 
        //}



        #endregion

        #endregion

    }
    #endregion

    public class Unreferenced
    {
        [System.Diagnostics.Conditional("DEBUG")]
        static public void Parameter(params object[] o)
        {
            return;
        }
    }


}

