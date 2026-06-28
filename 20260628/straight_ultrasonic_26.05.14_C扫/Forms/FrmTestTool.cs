using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Windows.Media.Media3D;
using ACS.SPiiPlusNET;      // ACS .NET Library
using System.Windows.Threading;
using FlatUI_TestPlatform.PubCls;
using HslControls;
using System.Text.RegularExpressions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Data.Matlab;
using System.Collections.Concurrent;


using System.Globalization;


using System.Reflection;

using csDriverOEMPA;
using csDriverOEMPA1;
using csDriverOEMPA2;
using csDriverOEMPAmini;
using csDriverOEMPAmax;
using csDriverOEMMC;
using WizardOEMPA;
using System.Windows.Controls.Primitives;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices.ComTypes;
using System.Diagnostics;
using OEMFormExample;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Configuration.Internal;
using System.Xml.Linq;
using static Seagull.BarTender.SystemDatabase.ReprintRange;

using static Seagull.BarTender.Print.LabelFormat;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using static FlatUI_TestPlatform.Forms.FrmAscan;




//using DocumentFormat.OpenXml.Vml;

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
        #region 连续扫描系统变量
        private double[,] _currentMotionMatrix;  // 存储加载的运动矩阵

        //申明服务
        private AutoScanManager _scanManager;
        private VirtualScanService _scanService;
        private ScanResultRecorder _resultRecorder;


        #endregion
        #region 超声扫描系统变量
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
        double gain, start, range, iPointFactor;
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
        // 存储各闸门数据
        private Dictionary<int, byte[,]> gateData = new Dictionary<int, byte[,]>();

        // 存储启用的闸门ID
        private List<int> enabledGates = new List<int>();

        // 当前选中的闸门
        private int selectedGateId = 0;
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
            int iMonitorPort;
            uint ui = GetCurrentProcessId();
            InitializeComponent();

            InitializeChannelComboBoxes(); // 添加这行
            //初始化线性扫描对象
            InitializeScanManager();


            //InitializeCscanDisplay();

            //把无参的东西放到有参的东西
            Control.CheckForIllegalCrossThreadCalls = false;
            txtDouble = new TextBox[] { txtSpeedX, txtSpeedY, txtSpeedZ, txtTravelX, txtTravelY, txtTravelZ, txtLineSpeed };
            txtInt = new TextBox[] { txtTriggerTimeLine, txtScanInterval };
            foreach (TextBox txt in txtDouble)
            {
                txt.TextChanged += txtDouble_TextChanged; // 统一的事件处理
            }
            foreach (TextBox txt in txtInt)
            {
                txt.TextChanged += txtInt_TextChanged; // 统一的事件处理
            }






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
            dataSizeCscan = 0;
            dataTimerAscan = 0;
            dataTimerCscan = 0;
            dataTimeAscan = 0;
            dataTimeCscan = 0;
            callbackCustomized = false;
            m_bCallback = false;
            pFileName = null;
            cycleCount = 0;
            gain = 0.0;
            start = 0.0;
            range = 0.0;
            iPointFactor = 0.0;
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

        // 初始化通道选择控件
        private void InitializeChannelComboBoxes()
        {

            comboBoxTransmit.Items.Clear();
            for (int i = 0; i < 64; i++)
            {
                comboBoxTransmit.Items.Add($"发射 {i+1}");
            }
            comboBoxTransmit.SelectedIndex = 0;

            comboBoxReceive.Items.Clear();
            for (int i = 0; i < 64; i++)
            {
                comboBoxReceive.Items.Add($"接收 {i+1}");
            }
            comboBoxReceive.SelectedIndex = 0;
            


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

        public FrmTestTool()
        {
            
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            txtDouble = new TextBox[] { txtSpeedX, txtSpeedY, txtSpeedZ, txtTravelX, txtTravelY, txtTravelZ , txtLineSpeed };
            txtInt = new TextBox[] { txtTriggerTimeLine, txtScanInterval };
            foreach (TextBox txt in txtDouble)
            {
                txt.TextChanged += txtDouble_TextChanged; // 统一的事件处理
            }
            foreach (TextBox txt in txtInt)
            {
                txt.TextChanged += txtInt_TextChanged; // 统一的事件处理
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

            hslBtnCWY.Text = "\uF061" + " Y";//->
            hslBtnCWY.Font = new System.Drawing.Font("FontAwesome", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            hslBtnCCWY.Text = "\uF060" + " Y";//<-
            hslBtnCCWY.Font = new System.Drawing.Font("FontAwesome", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            hslBtnCWX.Text = "\uF063" + " X";//^
            hslBtnCWX.Font = new System.Drawing.Font("FontAwesome", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            hslBtnCCWX.Text = "\uF062" + " X";//V
            hslBtnCCWX.Font = new System.Drawing.Font("FontAwesome", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            hslBtnCWZ.Text = "\uF01A" + " Z";//V
            hslBtnCWZ.Font = new System.Drawing.Font("FontAwesome", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            hslBtnCCWZ.Text = "\uF01B" + " Z";//^
            hslBtnCCWZ.Font = new System.Drawing.Font("FontAwesome", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            hslbtnHallAll.Text = "\uF0C8";
            hslbtnHallAll.Font = new System.Drawing.Font("FontAwesome", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));



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
                    hslLabelFPOSY2.TextValue = String.Format("{0:0.0000}", m_lfFPos[Convert.ToInt16(MyDevice.config.AxisY)] - MyDevice.PosY.Offset);
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
                    }
                    else
                    {
                        hslOut01.LanternBackground = Color.DarkGray;
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
                                                                                                  //_ACS.FaultClearM(axes);
            _ACS.FaultClear(MyDevice.config.AxisX);
            _ACS.FaultClear(MyDevice.config.AxisY);
            _ACS.FaultClear(MyDevice.config.AxisZ);
        }

        private void hslBtnSetZeroX_Click(object sender, EventArgs e)
        {
            //相对清零
            //MyDevice.PosX.Offset = m_lfFPos[Convert.ToInt16(MyDevice.config.AxisX)];
            //PubCls.MyIniFile.WriteData("Temp", "PosXOffset", MyDevice.PosX.Offset.ToString(), PubCls.MyIniFile.FilePath);

            //绝对清零
            if (!m_bConnected)
            {
                MessageBox.Show("请先连接控制器!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _ACS.SetFPosition(MyDevice.config.AxisX, 0);
        }

        private void hslBtnSetZeroY_Click(object sender, EventArgs e)
        {
            //MyDevice.PosY.Offset = m_lfFPos[Convert.ToInt16(MyDevice.config.AxisY)];
            //PubCls.MyIniFile.WriteData("Temp", "PosYOffset", MyDevice.PosY.Offset.ToString(), PubCls.MyIniFile.FilePath);
            //绝对清零
            if (!m_bConnected)
            {
                MessageBox.Show("请先连接控制器!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _ACS.SetFPosition(MyDevice.config.AxisY, 0);
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

        private void hslBtnEnableX_Click(object sender, EventArgs e)//电机使能/禁能的控制按钮事件处理方法，主要用于工业自动化控制中控制电机驱动器的状态
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

            HX_GoHomimg(MyDevice.config.AxisX);
        }

        private void hslBtnGoHomeY_Click(object sender, EventArgs e)
        {
            HX_GoHomimg(MyDevice.config.AxisY);
        }

        private void hslBtnGoHomeZ_Click(object sender, EventArgs e)//Z轴回原点（归零）
        {
            HX_GoHomimg(MyDevice.config.AxisZ);
        }

        private void hslBtnConnect_Click(object sender, EventArgs e)
        {
            if (!m_bConnected)
            {
                try
                {
                    _ACS.OpenCommEthernet("10.0.0.100", 701);//  (default : 10.0.0.100 701)169.254.176.4
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

        private void hslBtnGantryAdjust_Click(object sender, EventArgs e)//龙门调整（Gantry Adjust） 或电源启动按钮的事件处理方法
        {
            HX_AcsPowup();
        }

        private void hslBtnDisConnect_Click(object sender, EventArgs e)//断开连接（Disconnect） 按钮的事件处理方法
        {
            if (m_bConnected) _ACS.CloseComm();
            tmrMonitor.Stop();
            hslBtnConnect.Enabled = true;
            m_bConnected = false;
        }

        private void txtSpeedX_Click(object sender, EventArgs e)//速度设置文本框（txtSpeedX）的点击事件处理方法，用于弹出一个数字输入对话框来设置X轴的速度。
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

        private void txtSpeedY_Click(object sender, EventArgs e)//y轴的速度设置
        {
            HslControls.Forms.FormHslDigitalInput formHslDigital = new HslControls.Forms.FormHslDigitalInput();
            formHslDigital.OnOk = m =>
            {
                txtSpeedY.Text = m;
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

        private void txtTravelX_Click(object sender, EventArgs e)//X轴行程设置文本框（txtTravelX） 的点击事件处理方法，用于设置X轴的运动行程距离。//代码重复
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

        private void txtTravelY_Click(object sender, EventArgs e)
        {
            HslControls.Forms.FormHslDigitalInput formHslDigital = new HslControls.Forms.FormHslDigitalInput();
            formHslDigital.OnOk = m =>
            {
                txtTravelY.Text = m;
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

        private void hslBtnCWX_Click(object sender, EventArgs e)// X轴顺时针（CW - Clockwise）移动按钮 的事件处理方法，用于控制X轴进行负方向的移动。
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

        private void hslBtnCCWX_Click(object sender, EventArgs e)//X轴逆时针（CCW - Counter Clockwise）移动按钮 的事件处理方法
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

        private void hslBtnCWY_Click(object sender, EventArgs e)
        {
            if (MotionFlag1.Checked)
            {
                PTP_Move(MotionFlags.ACSC_NONE, Convert.ToInt16(MyDevice.config.AxisY), MyDevice.config.AxisY_Dir * (-Math.Abs(Convert.ToDouble(txtTravelY.Text))), Convert.ToDouble(txtSpeedY.Text));
            }
            if (MotionFlag2.Checked)
            {
                PTP_Move(MotionFlags.ACSC_AMF_RELATIVE, Convert.ToInt16(MyDevice.config.AxisY), MyDevice.config.AxisY_Dir * (-Math.Abs(Convert.ToDouble(txtTravelY.Text))), Convert.ToDouble(txtSpeedY.Text));
            }
        }

        private void hslBtnCCWY_Click(object sender, EventArgs e)
        {
            if (MotionFlag1.Checked)
            {
                PTP_Move(MotionFlags.ACSC_NONE, Convert.ToInt16(MyDevice.config.AxisY), MyDevice.config.AxisY_Dir * (Math.Abs(Convert.ToDouble(txtTravelY.Text))), Convert.ToDouble(txtSpeedY.Text));
            }
            if (MotionFlag2.Checked)
            {
                PTP_Move(MotionFlags.ACSC_AMF_RELATIVE, Convert.ToInt16(MyDevice.config.AxisY), MyDevice.config.AxisY_Dir * (Math.Abs(Convert.ToDouble(txtTravelY.Text))), Convert.ToDouble(txtSpeedY.Text));
            }
        }

        private void hslbtnHallAll_Click(object sender, EventArgs e)
        {
            try
            {
                // There is no halt all command, so you need to user HaltM function
                // 
                // ex) You want to stop 0, 2, 5 axis
                //int[] m_arrAxisList = new int[] { 0, 2, 5, -1 };
                // 
                if (m_arrAxisList != null) _ACS.HaltM(m_arrAxisList);
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

        //x、y、z重复代码很多，存在简化优化的可能

        private void MotionFlag1_CheckedChanged(object sender, EventArgs e)//运动模式1（绝对位置模式） 单选按钮的选中状态改变事件处理方法
        {
            b_motionFlags = MotionFlags.ACSC_NONE;
        }

        private void MotionFlag2_CheckedChanged(object sender, EventArgs e)
        {
            b_motionFlags = MotionFlags.ACSC_AMF_RELATIVE;
        }

        private void hslBtnCWX_MouseDown(object sender, MouseEventArgs e)//X轴顺时针（CW）按钮的鼠标按下（MouseDown）事件，用于实现点动（JOG）控制功能。
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

        private void hslBtnCCWX_MouseDown(object sender, MouseEventArgs e)//jog模式的点动控制
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

        private void hslBtnCCWX_MouseUp(object sender, MouseEventArgs e)//逆时针点动模式控制
        {
            if (MotionJOG.Checked)
            {
                _ACS.Halt(MyDevice.config.AxisX);
            }
        }

        private void hslBtnCWY_MouseDown(object sender, MouseEventArgs e)
        {
            double lfVelocity = 0.0f;
            try
            {
                if (MotionJOG.Checked)
                {
                    lfVelocity = Convert.ToDouble(txtSpeedY.Text.Trim());
                    //if (lfVelocity <0) lfVelocity = lfVelocity * (MyDevice.config.AxisY_Dir);

                    _ACS.Jog(MotionFlags.ACSC_AMF_VELOCITY, MyDevice.config.AxisY, lfVelocity * (-MyDevice.config.AxisY_Dir));
                }
                else
                {
                    //_ACS.Jog(0, AxisY, (double)GlobalDirection.ACSC_POSITIVE_DIRECTION);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void hslBtnCWY_MouseUp(object sender, MouseEventArgs e)
        {
            if (MotionJOG.Checked)
            {
                _ACS.Halt(MyDevice.config.AxisY);
            }
        }

        private void hslBtnCCWY_MouseUp(object sender, MouseEventArgs e)
        {
            if (MotionJOG.Checked)
            {
                _ACS.Halt(MyDevice.config.AxisY);
            }
        }

        private void hslBtnCCWY_MouseDown(object sender, MouseEventArgs e)
        {
            double lfVelocity = 0.0f;
            try
            {
                if (MotionJOG.Checked)
                {
                    lfVelocity = Convert.ToDouble(txtSpeedY.Text.Trim());
                    //if (lfVelocity > 0) lfVelocity = lfVelocity * (MyDevice.config.AxisY_Dir);     // Negative direction : Using - (minus) velocity

                    _ACS.Jog(MotionFlags.ACSC_AMF_VELOCITY, MyDevice.config.AxisY, lfVelocity * (MyDevice.config.AxisY_Dir));
                }
                else
                {
                    //_ACS.Jog(0, AxisY, (double)GlobalDirection.ACSC_NEGATIVE_DIRECTION);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //System.Diagnostics.Debug.WriteLine(ex.Message);
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
        #endregion

        #region ACS函数
        public int HX_GoHomimg(Axis sAxis)//归零代码
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
        public int HX_GoGantry(Axis sAxis)//龙门同步校准（Gantry Alignment） 的方法，用于执行龙门双轴的同步运动程序。
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
            } 
            while (pstate == ProgramStates.ACSC_PST_RUN);
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
        public int HX_AcsPowup()//执行龙门调偏（校正）程序 的方法
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
        private void UpdateLimitState(int axisNo, int fault)//更新限位状态显示 的方法，用于实时显示各轴的限位触发状态
        {
            if (axisNo < MAX_UI_LIMIT_CNT)
            {
                if ((fault & (int)SafetyControlMasks.ACSC_SAFETY_LL) != 0) m_lblLeftLimit[axisNo].LanternBackground = Color.Red; else m_lblLeftLimit[axisNo].LanternBackground = Color.Lime;
                if ((fault & (int)SafetyControlMasks.ACSC_SAFETY_RL) != 0) m_lblRightLimit[axisNo].LanternBackground = Color.Red; else m_lblRightLimit[axisNo].LanternBackground = Color.Lime;
            }
        }
        //更新轴状态
        private void UpdateAxisState(int axisNo)//更新单个轴状态 的方法，用于实时显示轴的多种状态信息。
        {
            if (axisNo < MAX_UI_LIMIT_CNT)
            {
                m_nMotorState = _ACS.GetMotorState((Axis)axisNo);
                SafetyControlMasks fault = _ACS.GetFault((Axis)axisNo);
                if ((m_nMotorState & MotorStates.ACSC_MST_ENABLE) != 0) { m_lblEnable[axisNo].LanternBackground = Color.Lime; bEnable[axisNo] = true; }
                else { m_lblEnable[axisNo].LanternBackground = Color.DarkGray; bEnable[axisNo] = false; }

                if ((m_nMotorState & MotorStates.ACSC_MST_INPOS) != 0) m_lblZeroPos[axisNo].LanternBackground = Color.Lime; 
                else m_lblZeroPos[axisNo].LanternBackground = Color.DarkGray;

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
        private void BtnEnableState(bool bConnected, bool[] bEnable)// 控制界面按钮使能状态 的方法，根据设备连接状态和各轴使能状态来动态启用或禁用相关功能按钮。
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
                if (bEnable[Convert.ToInt16(MyDevice.config.AxisY)])
                {
                    hslBtnCWY.Enabled = true;
                    hslBtnCCWY.Enabled = true;
                    hslBtnGoHomeY.Enabled = true;
                }
                else
                {
                    hslBtnCWY.Enabled = false;
                    hslBtnCCWY.Enabled = false;
                    hslBtnGoHomeY.Enabled = false;
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
                if ((bEnable[Convert.ToInt16(MyDevice.config.AxisX)]) && (bEnable[Convert.ToInt16(MyDevice.config.AxisY)]) && (bEnable[Convert.ToInt16(MyDevice.config.AxisZ)]))
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
                hslBtnCWY.Enabled = false;
                hslBtnCCWY.Enabled = false;
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
                hslBtnLoadLine.Enabled = false;
                hslBtnPauseLineScan.Enabled = false;
                hslBtnStopLineScan.Enabled = false;
                hslBtnStartLineScan.Enabled = false;
            }
        }

        private void btnBrowseLineFile_Click(object sender, EventArgs e)//文件浏览按钮功能，用于选择线扫描的坐标文件
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "txt|*.txt";
            if (open.ShowDialog() == DialogResult.OK)
            {
                txtCoordinateLinePath.Text = open.FileName;
            }
        }

        private void hslBtnLoadLine_Click(object sender, EventArgs e)//文件加载按钮功能：读取坐标文件并准备扫描数据。
        {
            try
            {
                if (IsPathValid(txtCoordinateLinePath.Text))
                {
                    // 方法一、从文本文件里读取数据，并初始化数组
                    int MyMatrixRowsCount = File.ReadAllLines(txtCoordinateLinePath.Text).Length;

                    // 定义二维数组，用来做为坐标参数,第一列为X坐标，第二列为Y坐标，第三列为Z坐标
                    double[,] MyMatrix = new double[MyMatrixRowsCount, 6];

                    using (StreamReader reader = new StreamReader(txtCoordinateLinePath.Text))
                    {
                        string line;
                        int row = 0;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] values = line.Split(',');
                            if (values.Length == 6)
                            {
                                MyMatrix[row, 0] = double.Parse(values[0]);
                                MyMatrix[row, 1] = double.Parse(values[1]);
                                MyMatrix[row, 2] = double.Parse(values[2]);
                                MyMatrix[row, 3] = double.Parse(values[3]);
                                MyMatrix[row, 4] = double.Parse(values[4]);
                                MyMatrix[row, 5] = double.Parse(values[5]);
                                row++;
                            }
                        }
                    }

                    // 将加载的矩阵保存到全局变量
                    _currentMotionMatrix = MyMatrix;

                    // 简单提示
                    MessageBox.Show($"成功加载 {MyMatrixRowsCount} 个扫描点", "提示",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 启用开始扫描按钮
                    hslBtnStartLineScan.Enabled = true;
                }
                else
                {
                    MessageBox.Show("文件路径无效或文件不存在！", "错误",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载运动参数失败: {ex.Message}", "错误",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                hslBtnStartLineScan.Enabled = false;
            }
        }

        private async void hslBtnStartLineScan_Click(object sender, EventArgs e)//开始扫描按钮功能：启动自动线扫描任务。
        {
            if (_currentMotionMatrix == null)
            {
                MessageBox.Show("请先加载运动矩阵！");
                return;
            }

            if (_scanManager.IsScanning)
            {
                MessageBox.Show("扫描正在进行中！");
                return;
            }

            try
            {
                UpdateScanUIState(true);
                //扫描间隔
                double scanInterval = GetScanIntervalFromUI();

                // 使用您的方法风格：直接传入 _ACS
                bool success = await _scanManager.StartAutoScanAsync(
                    _ACS,  // 直接传入 ACS 实例
                    _currentMotionMatrix,
                    scanInterval,
                    (Axis)Convert.ToInt16(MyDevice.config.AxisX),
                    (Axis)Convert.ToInt16(MyDevice.config.AxisY),
                    (Axis)Convert.ToInt16(MyDevice.config.AxisZ),

                    MyDevice.PosX.Offset,     // X轴偏移
                    MyDevice.PosY.Offset,     // Y轴偏移
                    MyDevice.PosZ.Offset      // Z轴偏移
                );

                if (!success)
                {
                    UpdateScanUIState(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动扫描失败: {ex.Message}");
                UpdateScanUIState(false);
            }
        }
        #region 连续扫描系统配套
        //初始化函数
        private void InitializeScanManager()//创建扫描系统核心组件。
        {
            // 创建虚拟扫描服务
            _scanService = new VirtualScanService(failureRate: -1);
            _scanManager = new AutoScanManager(_scanService);

            // 创建结果记录器，连接到Ascan图表
            //_resultRecorder = new ScanResultRecorder(_scanService, UpdateAscanChart);

        }

        private double GetScanIntervalFromUI()
        {
            // 从您的界面输入框获取扫描间隔
            if (double.TryParse(txtScanInterval.Text, out double interval) && interval > 0)
            {
                return interval;
            }
            
            // 默认值或显示错误
            MessageBox.Show("扫描间隔无效，使用默认值50", "提示",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return 50; // 默认50ms
        }

        private void UpdateScanUIState(bool scanning)//更新扫描UI状态：根据扫描状态启用/禁用相关按钮。
        {
            hslBtnStartLineScan.Enabled = !scanning;
            hslBtnStopLineScan.Enabled = scanning;
            hslBtnLoadLine.Enabled = !scanning;  // 扫描时禁止重新加载
        }
        #endregion
        #region 扫描主要功能函数
        public class AutoScanManager
        {
            private CancellationTokenSource _cts;
            private bool _isScanning = false;
            private Axis _currentScanAxis;
            private ACS.SPiiPlusNET.Api _acs;
            private readonly IScanService _scanService;
            private int _currentPointIndex = 0;

            // 位置偏移参数
            private double _PositionOffsetX;
            private double _PositionOffsetY;
            private double _PositionOffsetZ;

            // 定时器相关
            private System.Threading.Timer _scanTimer;
            private double _scanIntervalMs = 100; // 默认100ms(两次运动之间的间隔)
            private DateTime _lastScanTime;
            private double _lastScanPosition;

            public bool IsScanning => _isScanning;
            public event Action<double> ScanTriggered;

            public AutoScanManager(IScanService scanService = null)//创建自动扫描管理器，初始化扫描服务。
            {
                _scanService = scanService ?? new VirtualScanService();
            }

            public async Task<bool> StartAutoScanAsync(//开始自动扫描：启动多坐标点自动扫描任务。
                ACS.SPiiPlusNET.Api acs,
                double[,] motionMatrix,
                double scanIntervalMs, // 改为时间间隔（毫秒）
                Axis scanAxis,
                Axis adjustAxis1,
                Axis adjustAxis2,
                double positionOffsetX = 0,
                double positionOffsetY = 0,
                double positionOffsetZ = 0)

            {
                if (_isScanning)
                {
                    throw new InvalidOperationException("扫描正在进行中");
                }

                _isScanning = true;
                _cts = new CancellationTokenSource();
                _currentScanAxis = scanAxis;
                _acs = acs;
                _scanIntervalMs = scanIntervalMs;
                _currentPointIndex = 0;

                _PositionOffsetX = positionOffsetX;
                _PositionOffsetY = positionOffsetY;
                _PositionOffsetZ = positionOffsetZ;

                try
                {
                    SetupScanTimer(); // 设置定时器
                    MessageBox.Show($"开始自动扫描，扫描间隔: {_scanIntervalMs}ms", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await Task.Run(() => ExecuteScanSequenceAsync(acs, motionMatrix, scanAxis, adjustAxis1, adjustAxis2, _cts.Token));

                    CleanupScanTimer();
                    MessageBox.Show("自动扫描完成", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                catch (OperationCanceledException)
                {
                    CleanupScanTimer();
                    MessageBox.Show("扫描已取消", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                catch (Exception ex)
                {
                    CleanupScanTimer();
                    MessageBox.Show($"扫描失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                finally
                {
                    _isScanning = false;
                    _cts?.Dispose();
                    _cts = null;
                    _currentScanAxis = Axis.ACSC_NONE;
                    _acs = null;
                }
            }

            private void SetupScanTimer()
            {
                try
                {
                    // Console.WriteLine($"🔧 设置扫描定时器，间隔: {_scanIntervalMs}ms");

                    _lastScanTime = DateTime.Now;
                    _lastScanPosition = GetCurrentPosition();

                    // 创建定时器，每隔指定时间触发扫描
                    _scanTimer = new System.Threading.Timer(ScanTimerCallback, null, 0, (int)_scanIntervalMs);

                    // Console.WriteLine("✅ 扫描定时器启动完成");
                }
                catch (Exception ex)
                {
                    // Console.WriteLine($"❌ 设置扫描定时器失败: {ex.Message}");
                    throw;
                }
            }

            private void CleanupScanTimer()//清理扫描定时器：释放扫描用的定时器资源。
            {
                try
                {
                    // Console.WriteLine("🧹 清理扫描定时器...");

                    _scanTimer?.Dispose();
                    _scanTimer = null;

                    // Console.WriteLine("✅ 扫描定时器清理完成");
                }
                catch (Exception ex)
                {
                    // Console.WriteLine($"❌ 清理扫描定时器失败: {ex.Message}");
                }
            }

            private void ScanTimerCallback(object state)//扫描定时器回调：定时检查并触发扫描。
            {
                if (!_isScanning || _acs == null)
                {
                    return;
                }

                try
                {
                    // 检查时间间隔
                    var currentTime = DateTime.Now;
                    var timeSinceLastScan = (currentTime - _lastScanTime).TotalMilliseconds;

                    if (timeSinceLastScan >= _scanIntervalMs)
                    {
                        double currentPosition = GetCurrentPosition();

                        // 检查位置是否发生变化（避免在静止时重复扫描）
                        if (Math.Abs(currentPosition - _lastScanPosition) > 0.001)
                        {
                            // Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] 定时扫描触发，位置: {currentPosition:F3}mm, 时间间隔: {timeSinceLastScan:F0}ms");

                            StartScan(currentPosition);

                            _lastScanTime = currentTime;
                            _lastScanPosition = currentPosition;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Console.WriteLine($"❌ 定时扫描回调异常: {ex.Message}");
                }
            }

            private double GetCurrentPosition()//获取当前位置：读取当前扫描轴的实时位置。
            {
                try
                {
                    if (_acs != null && _currentScanAxis != Axis.ACSC_NONE)
                    {
                        double position = _acs.GetFPosition(_currentScanAxis) - _PositionOffsetX;
                        return position;
                    }
                    else
                    {
                        return 0.0;
                    }
                }
                catch (Exception ex)
                {
                    // Console.WriteLine($"获取位置失败: {ex.Message}");
                    return 0.0;
                }
            }

            private void StartScan(double triggerPosition)//开始扫描：触发单次扫描操作。
            {
                try
                {
                    // 触发扫描事件
                    ScanTriggered?.Invoke(triggerPosition);

                    // 调用虚拟扫描服务
                    _ = PerformScanAsync(_currentPointIndex, triggerPosition);

                    _currentPointIndex++;
                }
                catch (Exception ex)
                {
                    // Console.WriteLine($"扫描执行失败: {ex.Message}");
                }
            }

            private async Task PerformScanAsync(int pointIndex, double position)//执行扫描：调用扫描服务进行单点扫描
            {
                try
                {
                    double scanValue = await _scanService.PerformScanAsync(pointIndex, position);
                    // Console.WriteLine($"扫描完成 - 点位{pointIndex}: 数值={scanValue:F3}, 位置={position:F3}mm");
                }
                catch (Exception ex)
                {
                    // Console.WriteLine($"扫描失败 - 点位{pointIndex}: {ex.Message}");
                }
            }

            public void StopAutoScan(ACS.SPiiPlusNET.Api acs, Axis scanAxis, Axis adjustAxis1, Axis adjustAxis2)//停止自动扫描：强制中断当前扫描任务。
            {
                _cts?.Cancel();
                try
                {
                    CleanupScanTimer();
                    acs.Kill(scanAxis);
                    acs.Kill(adjustAxis1);
                    acs.Kill(adjustAxis2);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"停止运动时发生错误: {ex.Message}");
                }
            }

            private async Task ExecuteScanSequenceAsync(//执行扫描序列：按顺序执行所有扫描点。
                ACS.SPiiPlusNET.Api acs,
                double[,] motionMatrix,
                Axis scanAxis,
                Axis adjustAxis1,
                Axis adjustAxis2,
                CancellationToken cancellationToken)

            {
                int totalPoints = motionMatrix.GetLength(0);
                for (int i = 0; i < totalPoints; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await ExecuteScanPointAsync(acs, motionMatrix, i, scanAxis, adjustAxis1, adjustAxis2, cancellationToken);
                }
            }

            private async Task ExecuteScanPointAsync(//执行单点扫描：处理一个扫描点的完整操作。
                ACS.SPiiPlusNET.Api acs,
                double[,] motionMatrix,
                int index,
                Axis scanAxis,
                Axis adjustAxis1,
                Axis adjustAxis2,
                CancellationToken cancellationToken)
            {
                double targetScan = motionMatrix[index, 0];
                double targetAdjust1 = motionMatrix[index, 1];
                double targetAdjust2 = motionMatrix[index, 2];
                double speedScan = motionMatrix[index, 3];
                double speedAdjust1 = motionMatrix[index, 4];
                double speedAdjust2 = motionMatrix[index, 5];


                if (Math.Abs(targetAdjust1) > 0.001)
                    await MoveAxisWithoutScanAsync(acs, adjustAxis1, targetAdjust1, speedAdjust1, cancellationToken);

                if (Math.Abs(targetAdjust2) > 0.001)
                    await MoveAxisWithoutScanAsync(acs, adjustAxis2, targetAdjust2, speedAdjust2, cancellationToken);

                if (Math.Abs(targetScan) > 0.001)
                    await MoveAxisWithScanningAsync(acs, scanAxis, targetScan, speedScan, cancellationToken);

                await Task.Delay(100, cancellationToken);
            }

            private async Task MoveAxisWithoutScanAsync(//移动轴但不扫描：单纯移动轴到指定位置。
                ACS.SPiiPlusNET.Api acs,
                Axis axis,
                double target,
                double speed,
                CancellationToken cancellationToken)
            {
                await Task.Run(() =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    acs.SetVelocity(axis, speed);
                    acs.ToPoint(MotionFlags.ACSC_AMF_RELATIVE, axis, target);
                    acs.WaitMotionEnd(axis, 10000);
                }, cancellationToken);
            }

            private async Task MoveAxisWithScanningAsync(
                ACS.SPiiPlusNET.Api acs,
                Axis axis,
                double target,
                double speed,
                CancellationToken cancellationToken)
            {
                await Task.Run(() =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    // Console.WriteLine($"🚀 开始扫描运动，目标位置: {target}");

                    // 简单的点到点运动，定时器会自动处理扫描触发
                    MotionFlags flags = MotionFlags.ACSC_AMF_RELATIVE;
                    acs.SetVelocity(axis, speed);
                    acs.ToPoint(flags, axis, target);
                    acs.WaitMotionEnd(axis, 15000);

                    // Console.WriteLine("✅ 扫描运动完成");
                }, cancellationToken);
            }
        }



        // 进度信息类
        public class ScanProgress
        {
            public int CurrentIndex { get; set; }
            public int TotalCount { get; set; }
            public int Percentage { get; set; }
            public string Message { get; set; }
            public double CurrentX { get; set; }
            public double CurrentY { get; set; }
            public double CurrentZ { get; set; }
        }




        #endregion

        #region 通用的扫描服务接口
        // 扫描服务接口
        public interface IScanService
        {
            event EventHandler<ScanCompletedEventArgs> ScanCompleted;
            event EventHandler<ScanFailedEventArgs> ScanFailed;
            Task<double> PerformScanAsync(int pointIndex, double position, CancellationToken cancellationToken = default);
        }

        // 事件参数类
        public class ScanCompletedEventArgs : EventArgs
        {
            public int PointIndex { get; set; }
            public double Value { get; set; }
            public double Position { get; set; }
        }

        public class ScanFailedEventArgs : EventArgs
        {
            public int PointIndex { get; set; }
            public string ErrorMessage { get; set; }
            public double Position { get; set; }
        }
        #endregion

        #region 虚拟扫描服务
        // 虚拟扫描服务实现
        public class VirtualScanService : IScanService
        {
            private readonly Random _random = new Random();
            private int _failureRate; // 失败率百分比

            public event EventHandler<ScanCompletedEventArgs> ScanCompleted;
            public event EventHandler<ScanFailedEventArgs> ScanFailed;

            public VirtualScanService(int failureRate = 5)
            {
                _failureRate = failureRate;
            }

            public async Task<double> PerformScanAsync(int pointIndex, double position, CancellationToken cancellationToken = default)
            {
                try
                {
                    // 模拟扫描耗时 (50-200ms)
                    int scanTime = _random.Next(50, 200);
                    await Task.Delay(scanTime, cancellationToken);

                    // 模拟随机失败
                    if (_random.Next(100) < _failureRate)
                    {
                        throw new InvalidOperationException($"虚拟扫描在点位 {pointIndex} 失败");
                    }

                    // 生成正弦函数值 + 随机噪声
                    double sineValue = Math.Sin(position * Math.PI / 50.0); // 周期为100mm
                    double noise = (_random.NextDouble() - 0.5) * 0.2; // ±0.1的噪声
                    double finalValue = sineValue + noise;

                    // 触发完成事件
                    ScanCompleted?.Invoke(this, new ScanCompletedEventArgs
                    {
                        PointIndex = pointIndex,
                        Value = finalValue,
                        Position = position
                    });

                    return finalValue;
                }
                catch (Exception ex)
                {
                    // 触发失败事件
                    ScanFailed?.Invoke(this, new ScanFailedEventArgs
                    {
                        PointIndex = pointIndex,
                        ErrorMessage = ex.Message,
                        Position = position
                    });
                    throw;
                }
            }
        }

        #endregion


        #region 更新图表主要功能函数


        //扫描请求
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

        private void UpdateAscanChart(ScanResult result)
        {
            if (Ascan.InvokeRequired)
            {
                Ascan.Invoke(new Action<ScanResult>(UpdateAscanChart), result);
                return;
            }

            // 更新Ascan图表
            // 这里需要根据您Ascan图表的实际API来调整
            // 假设Ascan有AddDataPoint方法

            // 或者如果Ascan是DataVisualization.Charting.Chart：
            var series = Ascan.Series["Series1"];
            if (series != null)
            {
                series.Points.AddXY(result.PointIndex, result.Value);
            }
        }


        #endregion





        private void txtDouble_TextChanged(object sender, EventArgs e)//文本框内容改变事件：当文本框文字变化时验证是否为有效数字。
        {
            TextBox changedTextBox = sender as TextBox; // 获取被更改的TextBox
            IsValidDouble(changedTextBox);
        }
        private void txtInt_TextChanged(object sender, EventArgs e)
        {

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

        private void _ACSFormClosed()
        {
            _ACS.CloseComm();
        }
        #endregion
        #endregion


        #region 超声采集成像部分


        private void hslBtnPauseLineScan_Click(object sender, EventArgs e)//暂停线扫描按钮：暂停正在运行的扫描程序。
        {
            ProgramStates ProState = _ACS.GetProgramState(ProgramBuffer.ACSC_BUFFER_3);
            if ((ProState & ProgramStates.ACSC_PST_RUN) != 0)
            {
                _ACS.SuspendBuffer(ProgramBuffer.ACSC_BUFFER_3);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTemplate_Click(object sender, EventArgs e)//模板生成按钮：根据选择的类型生成不同扫描模板。
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

        private bool CheckWizardTemplate()//检查模板向导：确保扫描模板对象已创建。
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

        private void PulseEchoTemplate(bool bLinear)//脉冲回波模板：生成或编辑脉冲回波扫描配置。
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

        private void PitchCatchTemplate()//一发一收模板：配置一发一收（Pitch-Catch）扫描参数。
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




        private void hslBtnStopLineScan_Click(object sender, EventArgs e)//停止线扫描按钮：强制停止正在进行的扫描任务。
        {
            if (_scanManager?.IsScanning == true)
            {
                _scanManager.StopAutoScan(
                    _ACS,  // 传入 ACS 实例
                    MyDevice.config.AxisX,
                    MyDevice.config.AxisY,
                    MyDevice.config.AxisZ
                );
                UpdateScanUIState(false);
            }
        }

        private void checkBoxConnect_CheckedChanged(object sender, EventArgs e)//勾选框控制超声设备的网络连接和断开，加载相应配置。
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


            ///* =====  FMC 一次性启用 ===== */
            //csSWDeviceOEMPA swOEM = hwDeviceOEMPA.GetSWDeviceOEMPA();
            //if (swOEM.IsFullMatrixCaptureReadWrite())        // 先确认支持
            //{
            //    hwDeviceOEMPA.EnableFMC(true);               // 打开 FMC 位
            //    hwDeviceOEMPA.EnableMultiHWChannel(true);    // 打开多通道
            //    //hwDeviceOEMPA.SetFMCElementStart(0);         // 接收起始晶片
            //    //hwDeviceOEMPA.SetFMCElementStop(63);         // 接收结束晶片
            //    //hwDeviceOEMPA.SetCycleCount(64);             // 发射次数 = 64
            //    /*  其他固定参数（增益、脉宽…）仍用你的配置文件  */
            //}
            //else
            //{
            //    MessageBox.Show("当前硬件不支持 FMC，已回退到普通相控阵模式",
            //                    "FMC 检测", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            ///* =================================== */


            // 添加连接结果提示
            //if (bRet && swDevice.IsConnected())
            //{
            //    MessageBox.Show("连接成功！", "连接状态", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    MessageBox.Show("连接失败！", "连接状态", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}


        }

        private static bool IsPathValid(string path)//检查路径有效性：验证文件路径是否合法可用。
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
        private bool IsValidDouble(System.Windows.Forms.TextBox sender)//验证双精度数字：检查文本框输入是否为有效浮点数。
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
        private bool IsValidInteger(System.Windows.Forms.TextBox sender)//验证双精度数字：检查文本框输入是否为有效浮点数。
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
            hwDevice.SetAcquisitionCscan_0x00010X02(AcquisitionCscan_0x00010X02);//在这把设备里面相关函数拿出来
            hwDevice.SetAcquisitionIO_0x00010101(AcquisitionIO_0x00010101, false);
            hwDevice.SetAcquisitionInfo(AcquisitionInfo);
            api = hwDeviceOEMPA.GetCustomizedOEMPA();
            api.SetCallbackCustomizedDriverAPI(CallbackCustomizedOEMPA);
            hwDeviceOEMPA.test();//test function is used to test all callback.

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
                                Task.Run(async() => // 采集的第一个线程
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

               

                cts = new CancellationTokenSource();

                // 采集任务（Producer）
                //_captureTask = Task.Run(() => RealtimeCaptureLoop(cts.Token), cts.Token);

                //// 处理任务（Consumer -> Compute image）
                //_processingTask = Task.Run(() => RealtimeProcessingLoop(cts.Token), cts.Token);

                // UI 刷新定时器（主线程启动）
                StartUiTimer();

              
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动实时采集失败：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private double _fs = 50e6;
        //private unsafe void TestGetAscanRange()
        //{
            
        //    int plPointFactor = 0;
        //    int plPointCount = 0;

        //    bool ok = hwDeviceOEMPA.GetAscanRange(
        //        &plPointCount,
        //        &plPointFactor
        //    );

            

        //    _fs = 100e6/ plPointFactor; // 100e6是超声设备原有的采样频率

            
        //}


        private void StartUiTimer()
        {
            //TestGetAscanRange();
            if (_uiTimer != null)
            {
                _uiTimer.Stop();
                _uiTimer.Dispose();
            }

            _uiTimer = new System.Windows.Forms.Timer();
            _uiTimer.Interval = UI_INTERVAL_MS;
            
            //int pointCount = hwDeviceOEMPA.GetAscanRange(int* IPointCount);
            double fs = 100e6/ iPointFactor;
            //int length_pt = FMC_DataMatrix.GetLength(0);
            //int[] frame = null;
            _uiTimer.Tick += (s, e) =>
            {
                textBoxlabelFPS.Text = DateTime.Now.ToString("HH:mm:ss.fff");
                // ===== A扫 =====
                if (FMC_DataMatrix != null)
                {
                    ////DrawAscanRealtime(ascanData, fs, 4, 800);
                    int length_pt = FMC_DataMatrix.GetLength(0);
                    //double v0 = FMC_DataMatrix[0, 0];
                    //double v1 = FMC_DataMatrix[length_pt / 2, 0];
                    //double v2 = FMC_DataMatrix[length_pt - 1, 0];

                    //textBoxlabelFPS.Text += $"  v0={v0:F2}, v1={v1:F2}, v2={v2:F2}";
                    DrawAscanRealtime(FMC_DataMatrix, _fs, length_pt);
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

        int step_1 = 1; //采样点


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


     



        

        // 从FMC数据中提取 B 扫所需的一条自发自收 A 扫：Tx=Rx=elementIndex
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
                int totalColumns = FMC_DataMatrix.GetLength(1)/64;
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
            if (comboBoxTransmit != null && comboBoxTransmit.SelectedIndex >= 0)
            {
                return comboBoxTransmit.SelectedIndex;
            }
            return 0;
        }

        // 获取选择的接收阵元（A扫描）
        private int GetSelectedReceiveChannel()
        {
            if (comboBoxReceive != null && comboBoxReceive.SelectedIndex >= 0)
            {
                return comboBoxReceive.SelectedIndex;
            }
            return 0;
        }

        // 获取B扫描选择的发射阵元
        //private int GetSelectedBScanTransmit()
        //{
        //    if (comboBoxBScanTransmit != null && comboBoxBScanTransmit.SelectedIndex >= 0)
        //    {
        //        return comboBoxBScanTransmit.SelectedIndex - 1;
        //    }
        //    return -1;
        //}
        // 更新A扫描标题
        private void UpdateAScanTitle(int transmitIndex, int receiveIndex)
        {
            if (Ascan != null && Ascan.Titles.Count > 0)
            {
                Ascan.Titles[0].Text = $"A扫描 - 发射:{transmitIndex} 接收:{receiveIndex}";
            }
        }

        // 更新B扫描标题

        // B扫描历史管理：每个扫描位置存储一条 Tx=Rx 的自发自收 A 扫
        private List<int[]> _bScanHistory = new List<int[]>();
        private const int MAX_BSCAN_HISTORY = 200;


        private void UpdateBScanImage()
        {
            if (_bScanHistory.Count == 0) return;

            try
            {
                int[,] bscandata = GetBscanDataFromFMC(FMC_DataMatrix);
                int num_samples = bscandata.GetLength(0);
                int num_stations = bscandata.GetLength(1);
                //double T = num_samples * 2e-10 * 1e6;

                //Debug.WriteLine($"B扫描数据: 扫描线={scanLines}, 时间步长={timeSteps}");

                // 行=扫描位置/帧，列=时间/深度
                double[,] displayMatrix = new double[num_stations, num_samples];

                for (int station = 0; station < num_stations; station++)
                {
                    //int[] line = _bScanHistory[lineIndex];
                    //int copyCount = Math.Min(timeSteps, line.Length);
                    for (int sample = 0; sample < num_samples; sample++)
                    {
                        displayMatrix[station, sample] = bscandata[sample, station];
                    }
                }

                Bitmap bmp = CreateBScanBitmap(displayMatrix);

                UpdatePictureBoxWithImage(bmp);

                Debug.WriteLine($"B扫描图像生成完成: 扫描线={displayMatrix.GetLength(0)}, 采样点={displayMatrix.GetLength(1)}");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"更新B扫描图像错误: {ex.Message}");
            }
        }

        // 后台计算B扫图
        private readonly object _bscanImageLock = new object();
        private Bitmap _latestBscanBitmap = null;
        private volatile bool _hasNewBscanBitmap = false;
        private DateTime _lastBscanComputeTime = DateTime.MinValue;

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


        //251022fhf
        private Bitmap CreateBScanBitmap(double[,] displayMatrix)
        {
            int scanLines = displayMatrix.GetLength(0); // 扫描位置/帧 - X轴
            int timeSteps = displayMatrix.GetLength(1); // 时间/深度 - Y轴

            int displayWidth = pictureBoxBscan?.Width > 0 ? pictureBoxBscan.Width : 800;
            int displayHeight = pictureBoxBscan?.Height > 0 ? pictureBoxBscan.Height : 400;

            Bitmap bmp = new Bitmap(displayWidth, displayHeight);

            // 找到数据的动态范围
            double minVal = double.MaxValue;
            double maxVal = double.MinValue;

            for (int line = 0; line < scanLines; line++)
            {
                for (int time = 0; time < timeSteps; time++)
                {
                    double val = Math.Abs(displayMatrix[line, time]);
                    if (val < minVal) minVal = val;
                    if (val > maxVal) maxVal = val;
                }
            }

            Debug.WriteLine($"B扫描数据范围: min={minVal}, max={maxVal}");

            double range = maxVal - minVal;
            if (range < 1e-10) range = 100;

            // X轴：扫描位置/帧；Y轴：时间/深度
            for (int displayY = 0; displayY < displayHeight; displayY++)
            {
                int originalTime = displayY * timeSteps / displayHeight;

                for (int displayX = 0; displayX < displayWidth; displayX++)
                {
                    int originalLine = displayX * scanLines / displayWidth;

                    if (originalLine < scanLines && originalTime < timeSteps)
                    {
                        double value = Math.Abs(displayMatrix[originalLine, originalTime]);
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

            //AddBScanLabels(bmp, scanLines, timeSteps);

            return bmp;
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
                //pictureBoxBscan.Refresh();
                pictureBoxBscan.Invalidate();

                //Debug.WriteLine($"✅ B扫描图像显示: {bmp.Width}x{bmp.Height}");
            }
            catch (Exception ex)
            {
                //Debug.WriteLine($"❌ 更新PictureBox错误: {ex.Message}");
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

        private void DrawAscanRealtime(int[,] FMC_DataMatrix, double fs , int maxPt )
        {
            if (FMC_DataMatrix == null ) return;

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
                t[j] = i ;

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

            return FMC_DataMatrix.Cast<int>().ToArray() ?? new int[0];
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
                case 1: eTrig = csEnumOEMPATrigger.csOEMPAEncoder; eReqIO = csEnumOEMPARequestIO.csOEMPAOnDigitalInputAndCycle; break; // 编码器
                case 2: eTrig = csEnumOEMPATrigger.csOEMPAInternal; eReqIO = csEnumOEMPARequestIO.csOEMPAOnDigitalInputAndCycle; break; // 内触发
                case 3: eTrig = csEnumOEMPATrigger.csOEMPAExternalSequence; eReqIO = csEnumOEMPARequestIO.csOEMPAOnDigitalInputAndCycle; break; // 外触发
                case 4: eTrig = csEnumOEMPATrigger.csOEMPAExternalCycleSequence; eReqIO = csEnumOEMPARequestIO.csOEMPAOnDigitalInputAndCycle; break; // 外部周期信号触发
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



        private void textBoxPort_TextChanged(object sender, EventArgs e)
        {

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

# region 触发方式的事件函数


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



        private static Stopwatch _stopwatch = Stopwatch.StartNew();

        //private CancellationTokenSource _cts = new();  // 取消后台任务
        private CancellationTokenSource _cts = new CancellationTokenSource();  // 取消后台任务
        private System.Windows.Forms.Timer _uiTimer;  // UI 刷新定时器
        #region FMC 数据采集
        //取决于开没开启FMC数据采集
        
        public int[,] FMC_DataMatrix ;
        private int _transmitIndex = 0; // 发射索引
        private int _currentElementIndex = 0; //已采集的阵元数量
        private int numcount = 0;
        private volatile bool _allowFmcCapture = false;
        private volatile int _fmcWrittenColumns = 0;
        private TaskCompletionSource<bool> _fmcDoneTcs;
        private const int FMC_TOTAL_COLUMNS = 64 * 64;
        private int GetSamplesFromTextBoxRange()
        {
            if (!double.TryParse(textBoxWriteRange.Text.Trim(), out double rangeUs))
            {
                MessageBox.Show("Range 输入错误，请输入数字，例如 30");
                return -1;
            }

            if (rangeUs <= 0)
            {
                MessageBox.Show("Range 必须大于 0");
                return -1;
            }

            double fs = 50e6; // 50 MHz
            double rangeSecond = rangeUs * 1e-6;

            int samples = (int)(rangeSecond * fs) + 1;

            return samples;
        }
        
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


            ProcessData( ptr, ascanHeader, acqInfo);
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
       




        //处理IO事件，暂时用不到
        public int AcquisitionIO_0x00010101(Object pAcquisitionParameter, ref csHeaderStream_0x0001 streamHeader, ref csHeaderIO_0x0001 ioHeader)
        {
            return 0;
        }

        #region
        // 在类级别添加这些成员变量
        private bool[] m_gateEnabled = new bool[4] { false, false, false, false };
        private Dictionary<int, List<CscanPoint>> m_gateDataPoints = new Dictionary<int, List<CscanPoint>>();

        // 原有的数组（如果需要保持兼容）
        private Dictionary<int, List<byte>> m_gateAmplitudes = new Dictionary<int, List<byte>>();

        private void comboBoxGates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGates.SelectedIndex >= 0 && comboBoxGates.SelectedIndex < enabledGates.Count)
            {
                selectedGateId = enabledGates[comboBoxGates.SelectedIndex];
                //RefreshCscanDisplay();
            }
        }

        private Dictionary<int, List<double>> m_gatePositionsX = new Dictionary<int, List<double>>();
        private Dictionary<int, List<double>> m_gatePositionsY = new Dictionary<int, List<double>>();
        private Dictionary<int, List<short>> m_gateTofValues = new Dictionary<int, List<short>>();
        private object _lockObj;



        private struct CscanPoint
        {
            public double X;
            public double Y;
            public double Amp;
            public double Tof;
            public int GateId;
        }

        // 边运动采集

        private async Task MoveAxisAsync(ACS.SPiiPlusNET.Api acs, Axis axis, double target, double speed)
        {
            await Task.Run(() =>
            {
                acs.SetVelocity(axis, speed);
                acs.ToPoint(MotionFlags.ACSC_AMF_RELATIVE, axis, target);
                acs.WaitMotionEnd(axis, 10000);
            });
        }
        public class ScanPointData
        {
            public int Index { get; set; }           // 扫描点序号 (1, 2, 3...)
            public double PositionX { get; set; }    // X坐标
            public double PositionY { get; set; }    // Y坐标  
            public double PositionZ { get; set; }    // Z坐标

            public double SpeedX { get; set; }    // X坐标
            public double SpeedY { get; set; }    // Y坐标  
            public double SpeedZ { get; set; }    // Z坐标
            public int[,] FMCData { get; set; }   // FMC数据矩阵 [时间, 通道]
        }
        private List<ScanPointData> _allPointDataList;
      
        private void SaveallScandataToMat() {
            
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "MAT文件 (*.mat)|*.mat";
                dialog.FileName = $"Motion_All_{DateTime.Now:yyyyMMdd_HHmmss}.mat";

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

                    double[,] pos = new double[1, 6]
                    {
                {
                    point.PositionX,
                    point.PositionY,
                    point.PositionZ,
                    point.SpeedX,
                    point.SpeedY,
                    point.SpeedZ
                }
                    };

                    variables.Add(
                        $"{prefix}_FMC",
                        Matrix<double>.Build.DenseOfArray(fmcDouble));

                    variables.Add(
                        $"{prefix}_Motion",
                        Matrix<double>.Build.DenseOfArray(pos));
                }

                MatlabWriter.Write(dialog.FileName, variables);

                MessageBox.Show($"保存完成：{dialog.FileName}");
            }
        }

        
        private bool _isProcessingMotionCapture = false;
        private async void checkBoxMotionCapture_CheckedChanged(object sender, EventArgs e)
        {
            if (_isProcessingMotionCapture) return;
            _isProcessingMotionCapture = true;
            try
            {
                // 加载参数
                hslBtnLoadLine.PerformClick();

                //扫描间隔
                double scanInterval = GetScanIntervalFromUI();

                if (_ACS == null)
                {
                    MessageBox.Show("运动控制器未初始化！");
                    return;
                }

                checkBoxMotionCapture.Enabled = false;
                int totalPoints = _currentMotionMatrix.GetLength(0);


                Axis scanAxis = (Axis)Convert.ToInt16(MyDevice.config.AxisX);
                Axis adjustAxis1 = (Axis)Convert.ToInt16(MyDevice.config.AxisY);
                Axis adjustAxis2 = (Axis)Convert.ToInt16(MyDevice.config.AxisZ);
                double offsetX = MyDevice.PosX.Offset;
                double offsetY = MyDevice.PosY.Offset;
                double offsetZ = MyDevice.PosZ.Offset;
                _allPointDataList = new List<ScanPointData>();
                for (int i = 0; i < totalPoints; i++)
                {
                    // 更新状态显示
                    //MessageBox.Show($"正在运行第 {i + 1}/{totalPoints} 点");
                    textBoxMotionCapture.Text = $"正在运行第 {i + 1}/{totalPoints} 点";
                    Application.DoEvents();

                    // ===== 获取目标位置=====
                    double targetX = _currentMotionMatrix[i, 0] + offsetX;
                    double targetY = _currentMotionMatrix[i, 1] + offsetY;
                    double targetZ = _currentMotionMatrix[i, 2] + offsetZ;
                    double speedX = _currentMotionMatrix[i, 3];
                    double speedY = _currentMotionMatrix[i, 4];
                    double speedZ = _currentMotionMatrix[i, 5];

                    await Task.Run(async () =>
                    {
                        if (Math.Abs(targetY) > 0.001)
                            await MoveAxisAsync(_ACS, adjustAxis1, targetY, speedY);

                        // 移动调整轴 Z
                        if (Math.Abs(targetZ) > 0.001)
                            await MoveAxisAsync(_ACS, adjustAxis2, targetZ, speedZ);

                        // 移动扫描轴 X
                        if (Math.Abs(targetX) > 0.001)
                            await MoveAxisAsync(_ACS, scanAxis, targetX, speedX);
                    });



                    // 等待运动稳定
                    await Task.Delay(TimeSpan.FromMilliseconds(scanInterval));
                    _fmcDoneTcs = new TaskCompletionSource<bool>();
                    // ===== 当前点开始采集 FMC =====
                    _currentElementIndex = 0;

                    FMC_lengthwrite_logic();

                    var swDevice = hwDeviceOEMPA.GetSWDevice();

                    var tcs = new TaskCompletionSource<bool>();
                    int startIndex = _currentElementIndex;
                    //int channels = 4096;
                    //int samples = GetSamplesFromTextBoxRange();



                    //FMC_DataMatrix = new int[samples, channels];
                    int rows = FMC_DataMatrix.GetLength(0);
                    int cols = FMC_DataMatrix.GetLength(1);

                    int[,] fmcCopy = new int[rows, cols];

                    _allowFmcCapture = true;
                    swDevice.EnablePulser(true);
                    //checkBoxPulser.Checked = true;

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

                    Array.Copy(FMC_DataMatrix, fmcCopy, FMC_DataMatrix.Length);

                    _allPointDataList.Add(new ScanPointData
                    {
                        Index = i + 1,
                        PositionX = targetX,
                        PositionY = targetY,
                        PositionZ = targetZ,
                        SpeedX = speedX,
                        SpeedY = speedY,
                        SpeedZ = speedZ,
                        FMCData = fmcCopy
                    });

                    swDevice.EnablePulser(false);
                    _allowFmcCapture = false;
                    checkBoxPulser.Checked = false;


                    //Array.Copy(FMC_DataMatrix, fmcCopy, FMC_DataMatrix.Length);

                    //_allPointDataList.Add(new ScanPointData
                    //{
                    //    Index = i + 1,
                    //    PositionX = targetX,
                    //    PositionY = targetY,
                    //    PositionZ = targetZ,
                    //    SpeedX = speedX,
                    //    SpeedY = speedY,
                    //    SpeedZ = speedZ,
                    //    FMCData = fmcCopy
                    //});


                }

                SaveallScandataToMat();

                MessageBox.Show("运动扫描及保存数据完成");
            }
            finally 
            {
                _allowFmcCapture = false;

                var swDevice = hwDeviceOEMPA?.GetSWDevice();
                swDevice?.EnablePulser(false);

                checkBoxMotionCapture.Enabled = true;

                _isProcessingMotionCapture = true;
                checkBoxMotionCapture.Checked = false;
                _isProcessingMotionCapture = false;

                textBoxMotionCapture.Text = "扫描完成";
            }
           
            
        }

        private readonly object _cscanLock = new object();

        private List<CscanPoint> _cscanPoints = new List<CscanPoint>();

        // C扫采集
        unsafe public int AcquisitionCscan_0x00010X02(
        object pAcquisitionParameter,
        ref csAcqInfoEx acqInfo,
        ref csHeaderStream_0x0001 streamHeader,
        ref csSubStreamCscan_0x0X02 cscanHeader,
        ref csCscanAmp_0x0102[] pBufferAmp,
        ref csCscanAmpTof_0x0202[] pBufferAmpTof)
        {
            try
            {
                double x = acqInfo.dEncoder[0];
                double y = acqInfo.dEncoder[1];

                int gateCount = cscanHeader.count;
                //iGateId = bufferAmp[iDataIndex].gateId;
                //sizeTime = System.Environment.TickCount;

                for (int i = 0; i < gateCount; i++)
                {
                    int gateId = 0;

                    double amp = 0;
                    double tof = 0;

                    // =========================
                    // VERSION 1
                    // AMP ONLY
                    // =========================

                    if (cscanHeader.version == 1)
                    {
                        if (pBufferAmp == null)
                            continue;

                        gateId =
                            pBufferAmp[i].gateId;

                        if (!IsGateEnabled(gateId))
                            continue;

                        if (!pBufferAmp[i]
                            .AmpOverThreshold)
                            continue;

                        amp =
                            pBufferAmp[i].byAmp;

                        tof = 0;
                    }

                    // =========================
                    // VERSION 2
                    // AMP + TOF
                    // =========================

                    else if (cscanHeader.version == 2)
                    {
                        if (pBufferAmpTof == null)
                            continue;



                        gateId =
                            pBufferAmpTof[i].gateId;

                        if (!IsGateEnabled(gateId))
                            continue;

                        amp =
                            pBufferAmpTof[i].byAmp;

                        tof =
                            pBufferAmpTof[i].wTof;
                    }
                    else
                    {
                        continue;
                    }

                    // =========================
                    // SAVE POINT
                    // =========================

                    lock (_cscanLock)
                    {
                        _cscanPoints.Add(
                            new CscanPoint
                            {
                                X = x,
                                Y = y,

                                Amp = amp,
                                Tof = tof,

                                GateId = gateId
                            });
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"C扫回调错误: {ex.Message}");
                return -1;
            }
        }


        private void UpdateGateComboBox(int gateId)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<int>(UpdateGateComboBox), gateId);
                return;
            }

            // 如果ComboBox还没有初始化，先初始化
            if (comboBoxGates.Items.Count == 1 && comboBoxGates.Items[0].ToString() == "正在采集数据...")
            {
                comboBoxGates.Items.Clear();
            }

            // 添加新的闸门选项
            string gateText = $"闸门 {gateId}";
            if (!comboBoxGates.Items.Contains(gateText))
            {
                comboBoxGates.Items.Add(gateText);

                // 如果是第一个闸门，自动选中
                if (comboBoxGates.Items.Count == 1)
                {
                    comboBoxGates.SelectedIndex = 0;
                    comboBoxGates.Enabled = true;
                    selectedGateId = gateId;
                }
            }
        }
        // 在窗体线程中更新数据
        //private void UpdateGateDataInForm(int gateId, byte[,] data)
        //{
        //    if (InvokeRequired)
        //    {
        //        BeginInvoke(new Action<int, byte[,]>(UpdateGateDataInForm), gateId, data);
        //        return;
        //    }

        //    // 存储数据
        //    gateData[gateId] = data;

        //    // 如果是当前选中的闸门，立即刷新显示
        //    if (gateId == selectedGateId)
        //    {
        //        RefreshCscanDisplay();
        //    }

        //    // 更新状态信息
        //    UpdateGateInfoLabel(gateId, data);
        //}

        // 检查闸门是否启用
        private bool IsGateEnabled(int gateId)
        {
            return gateId >= 0 && gateId < 4 && m_gateEnabled[gateId];
        }

        private enum CscanMode
        {
            None,
            SaveOnly,
            Imaging
        }

        private CscanMode _cscanMode = CscanMode.None;

        private void buttonCscan_Click(object sender, EventArgs e)
        {
            try
            {
                // 清空旧数据
                lock (_cscanLock)
                {
                    _cscanPoints.Clear();
                }

                // 默认启用 Gate0
                enabledGates.Clear();
                enabledGates.Add(0);

                // UI提示
                comboBoxGates.Items.Clear();
                comboBoxGates.Items.Add("C扫成像：等待编码器触发...");
                comboBoxGates.SelectedIndex = 0;
                comboBoxGates.Enabled = false;

                // 开启采集
                StartDataAcquisition();

                // 开启实时成像刷新
                StartCscanImageTimer();

                Debug.WriteLine("C扫实时成像已启动");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"启动 C扫失败：{ex.Message}",
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }


        private void StartDataAcquisition()
        {
            var swDevice = hwDeviceOEMPA.GetSWDevice();

            swDevice.EnablePulser(true);
        }

        private System.Windows.Forms.Timer _cscanImageTimer;

        private void StartCscanImageTimer()
        {
            // 先关闭旧 Timer
            if (_cscanImageTimer != null)
            {
                _cscanImageTimer.Stop();
                _cscanImageTimer.Dispose();
            }

            _cscanImageTimer = new System.Windows.Forms.Timer();

            // 200 ms 刷新一次
            _cscanImageTimer.Interval = 200;

            _cscanImageTimer.Tick += (s, e) =>
            {
                try
                {
                    UpdateCscanDisplay();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(
                        $"C扫刷新错误: {ex.Message}");
                }
            };

            _cscanImageTimer.Start();
        }


        private void UpdateCscanDisplay()
        {
            List<CscanPoint> snapshot;

            lock (_cscanLock)
            {
                if (_cscanPoints == null || _cscanPoints.Count == 0)
                    return;

                snapshot = _cscanPoints.ToList();
            }

            int gateId = 0; // 先默认显示 Gate0
            int nx = pictureBoxCscan.Width > 0 ? pictureBoxCscan.Width : 400;
            int ny = pictureBoxCscan.Height > 0 ? pictureBoxCscan.Height : 300;

            double[,] matrix = BuildCscanMatrix(snapshot, gateId, nx, ny);

            if (matrix == null)
                return;

            Bitmap bmp = CreateCscanBitmap(matrix);

            if (bmp == null)
                return;

            var old = pictureBoxCscan.Image;
            pictureBoxCscan.Image = bmp;
            old?.Dispose();

            pictureBoxCscan.Invalidate();
        }


        private double[,] BuildCscanMatrix(List<CscanPoint> points, int gateId, int nx, int ny)
        {
            var gatePoints = points.Where(p => p.GateId == gateId).ToList();

            if (gatePoints.Count == 0)
                return null;

            double xmin = gatePoints.Min(p => p.X);
            double xmax = gatePoints.Max(p => p.X);
            double ymin = gatePoints.Min(p => p.Y);
            double ymax = gatePoints.Max(p => p.Y);

            double[,] matrix = new double[ny, nx];

            foreach (var p in gatePoints)
            {
                int ix = (int)((p.X - xmin) / (xmax - xmin + 1e-12) * (nx - 1));
                int iy = (int)((p.Y - ymin) / (ymax - ymin + 1e-12) * (ny - 1));

                if (ix >= 0 && ix < nx && iy >= 0 && iy < ny)
                {
                    matrix[ny - 1 - iy, ix] = Math.Max(matrix[ny - 1 - iy, ix], p.Amp);
                }
            }

            return matrix;
        }

        private Bitmap CreateCscanBitmap(double[,] matrix)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);

            Bitmap bmp = new Bitmap(width, height);

            double maxVal = 1;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (matrix[y, x] > maxVal)
                        maxVal = matrix[y, x];
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double normalized = matrix[y, x] / maxVal;
                    int c = Math.Max(0, Math.Min(255, (int)(normalized * 255)));

                    bmp.SetPixel(x, y, Color.FromArgb(c, c, c));
                }
            }

            return bmp;
        }












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




