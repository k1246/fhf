using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACS.SPiiPlusNET;
using FlatUI_TestPlatform.PubCls;

namespace FlatUI_TestPlatform.Forms
{
    public partial class FrmConfig : Form
    {
        public FrmConfig()
        {
            InitializeComponent();
        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            MyLogNetFile.logNet.WriteInfo("进入配置界面");

            txtBaseConfig1.Text = PubCls.MyIniFile.ReadData("Config", "AppName", "", PubCls.MyIniFile.FilePath);
            txtBaseConfig2.Text = PubCls.MyIniFile.ReadData("Config", "AppVer", "", PubCls.MyIniFile.FilePath);
            txtBaseConfig3.Text = PubCls.MyIniFile.ReadData("Config", "CustomerLogoPath", "", PubCls.MyIniFile.FilePath);
            txtBaseConfig4.Text = PubCls.MyIniFile.ReadData("Config", "CustomerName", "", PubCls.MyIniFile.FilePath);
            txtBaseConfig5.Text = PubCls.MyIniFile.ReadData("Config", "Manufacturer", "", PubCls.MyIniFile.FilePath);
            txtBaseConfig6.Text = PubCls.MyIniFile.ReadData("Config", "Abbreviation", "", PubCls.MyIniFile.FilePath);

            txtConfig1.Text = PubCls.MyIniFile.ReadData("Config", "AxisX", "", PubCls.MyIniFile.FilePath);
            txtConfig2.Text = PubCls.MyIniFile.ReadData("Config", "AxisY", "", PubCls.MyIniFile.FilePath);
            txtConfig3.Text = PubCls.MyIniFile.ReadData("Config", "AxisZ", "", PubCls.MyIniFile.FilePath);
            txtConfig4.Text = PubCls.MyIniFile.ReadData("Config", "AxisX_Dir", "", PubCls.MyIniFile.FilePath);
            txtConfig5.Text = PubCls.MyIniFile.ReadData("Config", "AxisY_Dir", "", PubCls.MyIniFile.FilePath);
            txtConfig6.Text = PubCls.MyIniFile.ReadData("Config", "AxisZ_Dir", "", PubCls.MyIniFile.FilePath);
            txtConfig7.Text = PubCls.MyIniFile.ReadData("Config", "TotalAxis", "", PubCls.MyIniFile.FilePath);
            txtConfig8.Text = PubCls.MyIniFile.ReadData("Config", "ConfigText8", "", PubCls.MyIniFile.FilePath);
            txtConfig9.Text = PubCls.MyIniFile.ReadData("Config", "ConfigText9", "", PubCls.MyIniFile.FilePath);
            txtConfig10.Text = PubCls.MyIniFile.ReadData("Config", "ConfigText10", "", PubCls.MyIniFile.FilePath);

            chkPrinterEnable.Checked = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "PrinterEnable", "", PubCls.MyIniFile.FilePath));
            chkSpeechEnable.Checked = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "SpeechEnable", "", PubCls.MyIniFile.FilePath));

            chkLoginEnable.Checked = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "LoginEnable", "", PubCls.MyIniFile.FilePath));
            chkRecipeEnable.Checked = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "RecipeEnable", "", PubCls.MyIniFile.FilePath));
            chkReviewEnable.Checked = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "ReviewEnable", "", PubCls.MyIniFile.FilePath));
            chkOptionEnable.Checked = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "OptionEnable", "", PubCls.MyIniFile.FilePath));
            chkMaintainEnable.Checked = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "MaintainEnable", "", PubCls.MyIniFile.FilePath));
            chkToolsEnable.Checked = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "ToolsEnable", "", PubCls.MyIniFile.FilePath));
            chkUserEnable.Checked = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "UserEnable", "", PubCls.MyIniFile.FilePath));
            chkConfigEnable.Checked = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "ConfigEnable", "", PubCls.MyIniFile.FilePath));
            chkDebugEnable.Checked = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "DebugEnable", "", PubCls.MyIniFile.FilePath));
            chkDemoEnable.Checked = Convert.ToBoolean(PubCls.MyIniFile.ReadData("Config", "DemoEnable", "", PubCls.MyIniFile.FilePath));
        }

        private void FrmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                PubCls.MyIniFile.WriteData("Config", "AppName", txtBaseConfig1.Text, PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "AppVer", txtBaseConfig2.Text, PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "CustomerLogoPath", txtBaseConfig3.Text, PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "CustomerName", txtBaseConfig4.Text, PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "Manufacturer", txtBaseConfig5.Text, PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "Abbreviation", txtBaseConfig6.Text, PubCls.MyIniFile.FilePath);

                PubCls.MyIniFile.WriteData("Config", "AxisX", txtConfig1.Text, PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "AxisY", txtConfig2.Text, PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "AxisZ", txtConfig3.Text, PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "AxisX_Dir", txtConfig4.Text, PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "AxisY_Dir", txtConfig5.Text, PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "AxisZ_Dir", txtConfig6.Text, PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "TotalAxis", txtConfig7.Text, PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "ConfigText8", txtConfig8.Text, PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "ConfigText9", txtConfig9.Text, PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "ConfigText10", txtConfig10.Text, PubCls.MyIniFile.FilePath);

                //功能选择
                PubCls.MyIniFile.WriteData("Config", "PrinterEnable", chkPrinterEnable.Checked.ToString(), PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "SpeechEnable", chkSpeechEnable.Checked.ToString(), PubCls.MyIniFile.FilePath);

                PubCls.MyIniFile.WriteData("Config", "LoginEnable", chkLoginEnable.Checked.ToString(), PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "RecipeEnable", chkRecipeEnable.Checked.ToString(), PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "ReviewEnable", chkReviewEnable.Checked.ToString(), PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "OptionEnable", chkOptionEnable.Checked.ToString(), PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "MaintainEnable", chkMaintainEnable.Checked.ToString(), PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "ToolsEnable", chkToolsEnable.Checked.ToString(), PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "UserEnable", chkUserEnable.Checked.ToString(), PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "ConfigEnable", chkConfigEnable.Checked.ToString(), PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "DebugEnable", chkDebugEnable.Checked.ToString(), PubCls.MyIniFile.FilePath);
                PubCls.MyIniFile.WriteData("Config", "DemoEnable", chkDemoEnable.Checked.ToString(), PubCls.MyIniFile.FilePath);
                //PubCls.ClsIniFile.WriteData("Config", "Spare2Enable", chkSpare2Enable.Checked.ToString(), PubCls.ClsIniFile.FilePath);
                //PubCls.ClsIniFile.WriteData("Config", "Spare3Enable", chkSpare3Enable.Checked.ToString(), PubCls.ClsIniFile.FilePath);

                //更新实时系统设置
                MyDevice.config.AppName = txtBaseConfig1.Text;
                MyDevice.config.AppVer = txtBaseConfig2.Text;
                MyDevice.config.CustomerLogoPath = txtBaseConfig3.Text;
                MyDevice.config.CustomerName = txtBaseConfig4.Text;
                MyDevice.config.Manufacturer = txtBaseConfig5.Text;
                MyDevice.config.Abbreviation = txtBaseConfig6.Text;

                MyDevice.config.AxisX = (Axis) Convert.ToInt16(txtConfig1.Text);
                MyDevice.config.AxisY = (Axis)Convert.ToInt16(txtConfig2.Text);
                MyDevice.config.AxisZ = (Axis)Convert.ToInt16(txtConfig3.Text);
                MyDevice.config.AxisX_Dir = Convert.ToInt16(txtConfig4.Text);
                MyDevice.config.AxisY_Dir = Convert.ToInt16(txtConfig5.Text);
                MyDevice.config.AxisZ_Dir = Convert.ToInt16(txtConfig6.Text);
                MyDevice.config.TotalAxis = Convert.ToInt16(txtConfig7.Text);
                MyDevice.config.Reserve08 = txtConfig8.Text;
                MyDevice.config.Reserve09 = txtConfig9.Text;
                MyDevice.config.Reserve10 = txtConfig10.Text;

                MyDevice.config.PrinterEnable = chkPrinterEnable.Checked;
                MyDevice.config.SpeechEnable = chkSpeechEnable.Checked;

                MyDevice.config.LoginEnable = chkLoginEnable.Checked;
                MyDevice.config.RecipeEnable = chkRecipeEnable.Checked;
                MyDevice.config.ReviewEnable = chkReviewEnable.Checked;
                MyDevice.config.OptionEnable = chkOptionEnable.Checked;
                MyDevice.config.MaintainEnable = chkMaintainEnable.Checked;
                MyDevice.config.ToolsEnable = chkToolsEnable.Checked;
                MyDevice.config.UserEnable = chkUserEnable.Checked;
                MyDevice.config.ConfigEnable = chkConfigEnable.Checked;
                MyDevice.config.DebugEnable = chkDebugEnable.Checked;
                MyDevice.config.DemoEnable= chkDemoEnable.Checked;
                MyLogNetFile.logNet.WriteInfo("保存并退出配置界面");
            }
            catch (Exception ex)
            {
                MyLogNetFile.logNet.WriteException("执行配置保存时异常", ex);
            }
        }
    }
}
