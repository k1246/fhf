using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FontAwesome.Sharp;//需要安装FontAwesome字体
using FlatUI_TestPlatform.Forms;
using FlatUI_TestPlatform.Demo;
using FlatUI_TestPlatform.PubCls;
using System.Threading;
using Seagull.BarTender.Print;//可能需要安装BarTender软件引擎
using PrecisionTiming;//引用高精度定时器,不可操作界面
using ACS.SPiiPlusNET;

namespace FlatUI_TestPlatform
{
    public partial class FormMain : Form
    {
        #region 声明区
        private IconButton currentBtn;//当前激活的菜单按钮
        private Panel leftBorderBtn;
        private Form currentChildForm;//当前打开的子窗体
        private bool leftMenuIsCollapsed;//左侧菜单是否已折叠
        private int leftMenuWidth;//左侧菜单原始宽度，用来展开恢复宽度

        //高精度定时器
        /*注意：定时器执行的方法里，如果执行时间大于定时器周期，就会发送任务堆积，解决办法就是开线程。*/
        PrecisionTimer TimePro1ms = new PrecisionTimer();//超高速AD
        PrecisionTimer TimePro10ms = new PrecisionTimer();//TCP/UDP/USB
        PrecisionTimer TimePro200ms = new PrecisionTimer();//232/48/PLC
        #endregion

        #region 引用功能函数，不必修改
        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        #endregion

        public FormMain()
        {
            InitializeComponent();
            leftMenuIsCollapsed = false;//界面初始时，左侧菜单不折叠
            leftMenuWidth = panelMenu.Width;//左侧菜单宽度值
            //菜单左侧焦点条
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, iconButton1.Height);//按钮左侧焦点条，高度要与按钮高度一致
            panelMenu.Controls.Add(leftBorderBtn);
            //Form
            this.Text = String.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            #region 初始化变量
            MyDevice.Work_control.running = false;//工作运行中设置为False
            #endregion

            #region 初始化文件类
            txtInfo.AppendText("正在初始化文件类..." + "\r\n");
            PubCls.MyLogNetFile.LogIni();//初始化日志功能
            PubCls.MyIniFile.FilePath = Application.StartupPath + "\\config.ini";//日志存储路径和文件名
            PubCls.MyExcelFile.FilePath = Application.StartupPath + "\\Excel\\a.xlsx";
            PubCls.MyExcelFile.templatePath = Application.StartupPath + "\\Excel\\templateA.xlsx";//Excel模板文件
            PubCls.MyWordlFile.templatePath = Application.StartupPath + "\\Word\\templateA.docx";//Word模板文件
#endregion

            #region 初始化配置config
            txtInfo.AppendText("正在初始化配置..." + "\r\n");
            MyDevice.config.AppName = PubCls.MyIniFile.ReadData("Config", "AppName", "", PubCls.MyIniFile.FilePath);
            MyDevice.config.AppVer = PubCls.MyIniFile.ReadData("Config", "AppVer", "", PubCls.MyIniFile.FilePath);
            MyDevice.config.CustomerLogoPath = PubCls.MyIniFile.ReadData("Config", "CustomerLogoPath", "", PubCls.MyIniFile.FilePath);
            MyDevice.config.CustomerName = PubCls.MyIniFile.ReadData("Config", "CustomerName", "", PubCls.MyIniFile.FilePath);
            MyDevice.config.Manufacturer = PubCls.MyIniFile.ReadData("Config", "Manufacturer", "", PubCls.MyIniFile.FilePath);
            MyDevice.config.Abbreviation = PubCls.MyIniFile.ReadData("Config", "Abbreviation", "", PubCls.MyIniFile.FilePath);

            MyDevice.config.AxisX = (Axis)Convert.ToInt16(PubCls.MyIniFile.ReadData("Config", "AxisX", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.AxisY = (Axis)Convert.ToInt16(PubCls.MyIniFile.ReadData("Config", "AxisY", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.AxisZ = (Axis)Convert.ToInt16(PubCls.MyIniFile.ReadData("Config", "AxisZ", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.AxisX_Dir = Convert.ToInt16(PubCls.MyIniFile.ReadData("Config", "AxisX_Dir", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.AxisY_Dir = Convert.ToInt16(PubCls.MyIniFile.ReadData("Config", "AxisY_Dir", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.AxisZ_Dir = Convert.ToInt16(PubCls.MyIniFile.ReadData("Config", "AxisZ_Dir", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.TotalAxis = Convert.ToInt16(PubCls.MyIniFile.ReadData("Config", "TotalAxis", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.Reserve08 = PubCls.MyIniFile.ReadData("Config", "ConfigText8", "", PubCls.MyIniFile.FilePath);
            MyDevice.config.Reserve09 = PubCls.MyIniFile.ReadData("Config", "ConfigText9", "", PubCls.MyIniFile.FilePath);
            MyDevice.config.Reserve10 = PubCls.MyIniFile.ReadData("Config", "ConfigText10", "", PubCls.MyIniFile.FilePath);

            MyDevice.config.PrinterEnable = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "PrinterEnable", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.SpeechEnable = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "SpeechEnable", "", PubCls.MyIniFile.FilePath));

            MyDevice.config.LoginEnable = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "LoginEnable", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.RecipeEnable = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "RecipeEnable", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.ReviewEnable = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "ReviewEnable", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.OptionEnable = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "OptionEnable", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.MaintainEnable = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "MaintainEnable", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.ToolsEnable = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "ToolsEnable", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.UserEnable = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "UserEnable", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.ConfigEnable = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "ConfigEnable", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.DebugEnable = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "DebugEnable", "", PubCls.MyIniFile.FilePath));
            MyDevice.config.DemoEnable = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "DemoEnable", "", PubCls.MyIniFile.FilePath));
            #endregion

            #region 初始化设置option
            txtInfo.AppendText("正在初始化设置..." + "\r\n");
            MyDevice.option.Printername = PubCls.MyIniFile.ReadData("option", "Printername", "", PubCls.MyIniFile.FilePath);
            MyDevice.option.FolderPath = PubCls.MyIniFile.ReadData("option", "FolderPath", "", PubCls.MyIniFile.FilePath);

            MyDevice.option.PhotoPath = PubCls.MyIniFile.ReadData("option", "PhotoPath", "", PubCls.MyIniFile.FilePath);
            MyDevice.option.CutPicPath = PubCls.MyIniFile.ReadData("option", "CutPicPath", "", PubCls.MyIniFile.FilePath);
            MyDevice.option.Reserve03 = PubCls.MyIniFile.ReadData("option", "OptionText3", "", PubCls.MyIniFile.FilePath);
            MyDevice.option.Reserve04 = PubCls.MyIniFile.ReadData("option", "OptionText4", "", PubCls.MyIniFile.FilePath);
            MyDevice.option.Reserve05 = PubCls.MyIniFile.ReadData("option", "OptionText5", "", PubCls.MyIniFile.FilePath);
            MyDevice.option.Reserve06 = PubCls.MyIniFile.ReadData("option", "OptionText6", "", PubCls.MyIniFile.FilePath);
            MyDevice.option.Reserve07 = PubCls.MyIniFile.ReadData("option", "OptionText7", "", PubCls.MyIniFile.FilePath);
            MyDevice.option.Reserve08 = PubCls.MyIniFile.ReadData("option", "OptionText8", "", PubCls.MyIniFile.FilePath);
            MyDevice.option.Reserve09 = PubCls.MyIniFile.ReadData("option", "OptionText9", "", PubCls.MyIniFile.FilePath);
            MyDevice.option.Reserve10 = PubCls.MyIniFile.ReadData("option", "OptionText10", "", PubCls.MyIniFile.FilePath);
            #endregion

            #region 初始化界面
            txtInfo.AppendText("正在初始化界面..." + "\r\n");
            lblTitleChildForm.Text = MyDevice.config.AppName;
            lblTitleChildForm.Left = (panelTitleBar.Width - lblTitleChildForm.Width) / 2;//文字居中
            Title.Text = MyDevice.config.Abbreviation;
            lblDeviceName.Text = MyDevice.config.AppName;
            hslBadgeVer.Text = "Ver";
            hslBadgeVer.RightText = MyDevice.config.AppVer;
            lblDeviceName.Left = (panelDesktop.Width - lblDeviceName.Width) / 2;
            hslBadgeVer.Left = (panelDesktop.Width - hslBadgeVer.Width) / 2;

            iconButton2.Visible = MyDevice.config.RecipeEnable;
            iconButton3.Visible = MyDevice.config.ReviewEnable;
            iconButton4.Visible = MyDevice.config.OptionEnable;
            iconButton5.Visible = MyDevice.config.MaintainEnable;
            iconButton6.Visible = MyDevice.config.ToolsEnable;
            iconButton7.Visible = MyDevice.config.UserEnable;
            iconButton8.Visible = MyDevice.config.ConfigEnable;
            iconButton9.Visible = MyDevice.config.DebugEnable;
            iconButton10.Visible = MyDevice.config.LoginEnable;
            iconButton11.Visible = MyDevice.config.DemoEnable;
            #endregion

            #region 初始化用户和常用SQL语句
            txtInfo.AppendText("正在初始化用户和常用字符串..." + "\r\n");
            MyDevice.user.UserID = "0";//数据库中ID号
            MyDevice.user.UserNo = "0";//员工编号
            MyDevice.user.UserName = "登录";////操作者登录姓名,未登录时显示"登录"
            MyDevice.user.UserPsssword = "0";//操作者登录密码
            MyDevice.user.UserLevel = "1";//权限 0：开发者 1：系统管理员 2：管理员 3：操作员 4：无权限

            MyDB.sqlCommand.Reserve01 = "select * from 配方1";
            MyDB.sqlCommand.Reserve02 = "select * from 配方2";
            MyDB.sqlCommand.Reserve03 = "select * from 配方3";
            MyDB.sqlCommand.Reserve04 = "select * from 配方4";
            MyDB.sqlCommand.Reserve05 = "select * from 配方5";
            MyDB.sqlCommand.Reserve06 = "select * from 配方6";
            MyDB.sqlCommand.Reserve07 = "select * from 配方7";
            MyDB.sqlCommand.Reserve08 = "select * from 配方8";
            MyDB.sqlCommand.Reserve09 = "select * from 配方9";
            MyDB.sqlCommand.Reserve10 = "select * from 配方10";
            #endregion

            #region 初始化硬件
            #region 定时器线程初始化
            txtInfo.AppendText("正在初始化线程..." + "\r\n");
            TimePro1ms.SetAutoResetMode(true);//设置为自动循环
            TimePro1ms.SetPeriod(1);//设置时间间隔
            TimePro1ms.SetAction(TimePro1ms_Tick);//设置要执行的事件
            TimePro10ms.SetAutoResetMode(true);//设置为自动循环
            TimePro10ms.SetPeriod(10);//设置时间间隔
            TimePro10ms.SetAction(TimePro10ms_Tick);//设置要执行的事件
            TimePro200ms.SetAutoResetMode(true);//设置为自动循环
            TimePro200ms.SetPeriod(200);//设置时间间隔
            TimePro200ms.SetAction(TimePro200ms_Tick);//设置要执行的事件
            #endregion

            #region 打印机初始化
            txtInfo.AppendText("正在加载打印机..." + "\r\n");
            if (MyDevice.config.PrinterEnable)
            {
                PubCls.MyBtPrinter.PrinterIni();//打印机
            }
            else
            {
            }
            #endregion

            #region 设置语音输出设备
            txtInfo.AppendText("正在加载语音输出设备..." + "\r\n");
            if (MyDevice.config.SpeechEnable)
            {
                MyDevice.speech.SetOutputToDefaultAudioDevice();//第三步：设置语音输出设备
            }
            else
            {
            }
            #endregion
            #endregion

            #region 其它
            //加载后默认折叠菜单
            //iconButton_Collapse_Click(null, null);
            panelMenu.Width = 55;
            Title.Text = String.Empty;
            iconButton1.Text = String.Empty;
            iconButton2.Text = String.Empty;
            iconButton3.Text = String.Empty;
            iconButton4.Text = String.Empty;
            iconButton5.Text = String.Empty;
            iconButton6.Text = String.Empty;
            iconButton7.Text = String.Empty;
            iconButton8.Text = String.Empty;
            iconButton9.Text = String.Empty;
            iconButton10.Text = String.Empty;
            iconButton11.Text = String.Empty;
            leftMenuIsCollapsed = true;
            this.Refresh();
            //如果登录使能，则默认执行登录按钮事件
            if (!MyDevice.config.LoginEnable)
            {
                //需要的话，可以默认点击按钮，进入子界面
                iconButton1.PerformClick();
            }
            else
            {
            }

            //清除加载信息
            txtInfo.Clear();
            #endregion
        }

        #region  菜单Click事件，不必修改
        private void iconButton1_Click(object sender, EventArgs e)
        {
            //试验中不可以按按钮
            if (MyDevice.Work_control.running)
            {
                DialogResult dr = MessageBox.Show("试验进行中，禁止此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dr != DialogResult.OK)
                { }
            }
            else
            {
                ActivateButton(sender, MySys.RGBColors.color1);
                OpenChildForm(new FrmTestTool(null));
            }
        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
            //试验中不可以按按钮
            if (MyDevice.Work_control.running)
            {
                DialogResult dr = MessageBox.Show("试验进行中，禁止此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dr != DialogResult.OK)
                { }
            }
            else
            {
                ActivateButton(sender, MySys.RGBColors.color2);
                OpenChildForm(new FrmRecipe());
            }
        }
        private void iconButton3_Click(object sender, EventArgs e)
        {
            //试验中不可以按按钮
            if (MyDevice.Work_control.running)
            {
                DialogResult dr = MessageBox.Show("试验进行中，禁止此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dr != DialogResult.OK)
                { }
            }
            else
            {
                ActivateButton(sender, MySys.RGBColors.color3);
                OpenChildForm(new FrmReview());
            }
        }
        private void iconButton4_Click(object sender, EventArgs e)
        {
            //试验中不可以按按钮
            if (MyDevice.Work_control.running)
            {
                DialogResult dr = MessageBox.Show("试验进行中，禁止此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dr != DialogResult.OK)
                { }
            }
            else
            {
                ActivateButton(sender, MySys.RGBColors.color4);
                OpenChildForm(new FrmOption());
            }
        }
        private void iconButton5_Click(object sender, EventArgs e)
        {
            //试验中不可以按按钮
            if (MyDevice.Work_control.running)
            {
                DialogResult dr = MessageBox.Show("试验进行中，禁止此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dr != DialogResult.OK)
                { }
            }
            else
            {
                ActivateButton(sender, MySys.RGBColors.color5);
                OpenChildForm(new FrmMaintain());
            }
        }
        private void iconButton6_Click(object sender, EventArgs e)
        {
            //试验中不可以按按钮
            if (MyDevice.Work_control.running)
            {
                DialogResult dr = MessageBox.Show("试验进行中，禁止此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dr != DialogResult.OK)
                { }
            }
            else
            {
                ActivateButton(sender, MySys.RGBColors.color6);
                OpenChildForm(new FrmTools());
            }
        }
        private void iconButton7_Click(object sender, EventArgs e)
        {
            //试验中不可以按按钮
            if (MyDevice.Work_control.running)
            {
                DialogResult dr = MessageBox.Show("试验进行中，禁止此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dr != DialogResult.OK)
                { }
            }
            else
            {
                ActivateButton(sender, MySys.RGBColors.color7);
                OpenChildForm(new FrmUser());
            }
        }
        private void iconButton8_Click(object sender, EventArgs e)
        {
            //试验中不可以按按钮
            if (MyDevice.Work_control.running)
            {
                DialogResult dr = MessageBox.Show("试验进行中，禁止此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dr != DialogResult.OK)
                { }
            }
            else
            {
                ActivateButton(sender, MySys.RGBColors.color8);
                OpenChildForm(new FrmConfig());
            }
        }
        private void iconButton9_Click(object sender, EventArgs e)
        {
            //试验中不可以按按钮
            if (MyDevice.Work_control.running)
            {
                DialogResult dr = MessageBox.Show("试验进行中，禁止此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dr != DialogResult.OK)
                { }
            }
            else
            {
                ActivateButton(sender, MySys.RGBColors.color9);
                OpenChildForm(new FrmDebug());
            }
        }
        private void iconButton10_Click(object sender, EventArgs e)
        {
            //试验中不可以按按钮
            if (MyDevice.Work_control.running)
            {
                DialogResult dr = MessageBox.Show("试验进行中，禁止此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dr != DialogResult.OK)
                { }
            }
            else
            {
                ActivateButton(sender, MySys.RGBColors.color10);
                OpenChildForm(new FrmLogin());
            }
        }
        private void iconButton11_Click(object sender, EventArgs e)
        {
            //试验中不可以按按钮
            if (MyDevice.Work_control.running)
            {
                DialogResult dr = MessageBox.Show("试验进行中，禁止此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dr != DialogResult.OK)
                { }
            }
            else
            {
                ActivateButton(sender, MySys.RGBColors.color11);
                OpenChildForm(new FrmDemo());
            }

        }
        private void iconButtonExit_Click(object sender, EventArgs e)
        {
            //试验中不可以按按钮
            if (MyDevice.Work_control.running)
            {
                DialogResult dr = MessageBox.Show("试验进行中，禁止此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dr != DialogResult.OK)
                { }
            }
            else
            {
                SoftExit();
            }
        }
        private void iconButton_Collapse_Click(object sender, EventArgs e)
        {
            //if (leftMenuIsCollapsed)
            //{
            //    panelMenu.Width = leftMenuWidth;
            //    Title.Text = MyDevice.config.Abbreviation; ;
            //    iconButton1.Text = "主界面";
            //    iconButton2.Text = "配方";
            //    iconButton3.Text = "浏览";
            //    iconButton4.Text = "设置";
            //    iconButton5.Text = "维护";
            //    iconButton6.Text = "工具";
            //    iconButton7.Text = "用户";
            //    iconButton8.Text = "配置";
            //    iconButton9.Text = "调试";
            //    iconButton10.Text = "登录";
            //    iconButton11.Text = "例程";
            //    leftMenuIsCollapsed = false;
            //    this.Refresh();
            //}
            //else
            //{
            //    panelMenu.Width = 55;
            //    Title.Text = String.Empty;
            //    iconButton1.Text = String.Empty;
            //    iconButton2.Text = String.Empty;
            //    iconButton3.Text = String.Empty;
            //    iconButton4.Text = String.Empty;
            //    iconButton5.Text = String.Empty;
            //    iconButton6.Text = String.Empty;
            //    iconButton7.Text = String.Empty;
            //    iconButton8.Text = String.Empty;
            //    iconButton9.Text = String.Empty;
            //    iconButton10.Text = String.Empty;
            //    iconButton11.Text = String.Empty;
            //    leftMenuIsCollapsed = true;
            //    this.Refresh();
            //}
        }
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        #region 定时器事件，修改区
        /// <summary>
        /// 1s定时事件，主要用于界面显示等主线程定时事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1s_Tick(object sender, EventArgs e)
        {
            //程序简称更新
            if (leftMenuIsCollapsed)
            {
                Title.Text = String.Empty;
                //权限控制
                iconButton10.Text = String.Empty;//显示操作者姓名
            }
            else
            {
                Title.Text = MyDevice.config.Abbreviation;
                //权限控制
                iconButton10.Text = MyDevice.user.UserName;//显示操作者姓名
            }

            //时钟显示
            lblTime.Text = System.DateTime.Now.ToLongTimeString();

            //switch (MyDevice.user.UserLevel)
            //{
            //    case "0"://开发者
            //        iconButton1.Visible = true;//试验
            //        iconButton2.Visible = true;//配方
            //        iconButton3.Visible = true;//浏览
            //        iconButton4.Visible = true;//设置
            //        iconButton5.Visible = true;//维护
            //        iconButton6.Visible = true;//工具
            //        iconButton7.Visible = true;//用户
            //        iconButton8.Visible = true;//配置
            //        iconButton9.Visible = true;//调试
            //        iconButton10.Visible = true;//登录
            //        iconButton11.Visible = true;//例程
            //        break;
            //    case "1"://系统管理员
            //        iconButton1.Visible = true;
            //        iconButton2.Visible = true;
            //        iconButton3.Visible = true;
            //        iconButton4.Visible = true;
            //        iconButton5.Visible = true;
            //        iconButton6.Visible = true;
            //        iconButton7.Visible = true;
            //        iconButton8.Visible = true;
            //        iconButton9.Visible = true;
            //        iconButton10.Visible = true;
            //        iconButton11.Visible = false;
            //        break;
            //    case "2"://管理员
            //        iconButton1.Visible = true;
            //        iconButton2.Visible = true;
            //        iconButton3.Visible = true;
            //        iconButton4.Visible = true;
            //        iconButton5.Visible = true;
            //        iconButton6.Visible = false;
            //        iconButton7.Visible = false;
            //        iconButton8.Visible = false;
            //        iconButton9.Visible = false;
            //        iconButton10.Visible = true;
            //        iconButton11.Visible = false;
            //        break;
            //    case "3"://操作员
            //        iconButton1.Visible = true;
            //        iconButton2.Visible = false;
            //        iconButton3.Visible = false;
            //        iconButton4.Visible = false;
            //        iconButton5.Visible = false;
            //        iconButton6.Visible = false;
            //        iconButton7.Visible = false;
            //        iconButton8.Visible = false;
            //        iconButton9.Visible = false;
            //        iconButton10.Visible = true;
            //        iconButton11.Visible = false;
            //        break;
            //    default://未登录
            //        iconButton1.Visible = false;
            //        iconButton2.Visible = false;
            //        iconButton3.Visible = false;
            //        iconButton4.Visible = false;
            //        iconButton5.Visible = false;
            //        iconButton6.Visible = false;
            //        iconButton7.Visible = false;
            //        iconButton8.Visible = false;
            //        iconButton9.Visible = false;
            //        iconButton10.Visible = true;
            //        iconButton11.Visible = false;
            //        break;
            //}
        }

        /// <summary>
        /// 1ms高精度定时器回调函数，不可跨线程操作界面
        /// 主要用于数据采集和处理
        /// </summary>
        public static void TimePro1ms_Tick()
        {
            //Console.WriteLine("方法1在" + DateTime.Now.ToString("G") + "执行。");
        }

        /// <summary>
        /// 10ms高精度定时器回调函数，不可跨线程操作界面
        /// 主要用于TCP/UDP/USB数据采集和处理
        /// </summary>
        public static void TimePro10ms_Tick()
        {
            //Console.WriteLine("方法1在" + DateTime.Now.ToString("G") + "执行。");
        }

        /// <summary>
        /// 200ms高精度定时器回调函数，不可跨线程操作界面
        /// 主要用于数据采集,232/485、plc通讯和处理
        /// </summary>
        public static void TimePro200ms_Tick()
        {
            //Console.WriteLine("方法1在" + DateTime.Now.ToString("G") + "执行。");
        }

        #endregion

        #region 公共函数，可修改区
        //Methods
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                //currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                //currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                //currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                //Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                //Icon Current Child Form
                panelShadow1.BackColor = color;
                //panelTitleBar.BackColor = color;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.DarkSlateBlue;

                currentBtn.ForeColor = Color.White;
                //currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.White;
                //currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                //currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            if (currentChildForm != null)
            {
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                panelDesktop.Controls.Add(childForm);
                panelDesktop.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();
            }
        }

        public void SoftExit()
        {
            #region 关闭所有定时器
            timer1s.Enabled = false;
            TimePro1ms.Dispose();
            TimePro10ms.Dispose();
            TimePro200ms.Dispose();
            #endregion

            #region 释放硬件
            //释放打印机
            if (MyDevice.config.PrinterEnable)
            {
                PubCls.MyBtPrinter.Dispose();//释放打印机引擎
            }
            else
            {
            }
            //释放语音输出设备
            if (MyDevice.config.SpeechEnable)
            {
                MyDevice.speech.Dispose();
            }
            else
            {
            }
            #endregion
            OpenChildForm(null);//确保在配置界面时退出软件，来保证保存当时的配置，即要执行一次FormClosing()事件
            System.Environment.Exit(0);//这是最彻底的退出方式，不管什么线程都被强制退出，把程序结束的很干净。 参数0：代表程序正常退出；参数1：代表程序非正常退出
        }
        #endregion

        private void iconButton12_Click(object sender, EventArgs e)
        {
            //if(this.WindowState==FormWindowState.Normal)
            //{
            //    this.WindowState = FormWindowState.Maximized;
            //}
            //else
            //{
            //    this.WindowState=FormWindowState.Normal;
            //}
        }
    }
}