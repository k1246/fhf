using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlatUI_TestPlatform.PubCls;
using Seagull.BarTender.Print;

namespace FlatUI_TestPlatform.Forms
{
    public partial class FrmOption : Form
    {
        public FrmOption()
        {
            InitializeComponent();
        }

        private void FrmOption_Load(object sender, EventArgs e)
        {
            MyLogNetFile.logNet.WriteInfo("打开设置界面");

            if (MyDevice.config.PrinterEnable)
            {

                string PrinterName = "";
                PrinterName = MyDevice.option.Printername;
                Option_txtFolderPath.Text = MyDevice.option.FolderPath;
                Printers printers = new Printers();
                foreach (Printer printer in printers)
                {
                    Option_cboPrinters.Items.Add(printer.PrinterName);
                }
                if (printers.Count > 0)
                {
                    Option_cboPrinters.SelectedItem = PrinterName;
                }
                Option_cboPrinters.Text = PubCls.MyIniFile.ReadData("Option", "PrinterName", "", PubCls.MyIniFile.FilePath);
                Option_txtFolderPath.Text = PubCls.MyIniFile.ReadData("Option", "FolderPath", "", PubCls.MyIniFile.FilePath);
                Option_gbxPrinter.Enabled = true;
            }
            else
            {
                Option_gbxPrinter.Enabled = false;
            }

            txtOption1.Text = PubCls.MyIniFile.ReadData("Option", "PhotoPath", "", PubCls.MyIniFile.FilePath);
            txtOption2.Text = PubCls.MyIniFile.ReadData("Option", "CutPicPath", "", PubCls.MyIniFile.FilePath);
            txtOption3.Text = PubCls.MyIniFile.ReadData("Option", "OptionText3", "", PubCls.MyIniFile.FilePath);
            txtOption4.Text = PubCls.MyIniFile.ReadData("Option", "OptionText4", "", PubCls.MyIniFile.FilePath);
            txtOption5.Text = PubCls.MyIniFile.ReadData("Option", "OptionText5", "", PubCls.MyIniFile.FilePath);
            txtOption6.Text = PubCls.MyIniFile.ReadData("Option", "OptionText6", "", PubCls.MyIniFile.FilePath);
            txtOption7.Text = PubCls.MyIniFile.ReadData("Option", "OptionText7", "", PubCls.MyIniFile.FilePath);
            txtOption8.Text = PubCls.MyIniFile.ReadData("Option", "OptionText8", "", PubCls.MyIniFile.FilePath);
            txtOption9.Text = PubCls.MyIniFile.ReadData("Option", "OptionText9", "", PubCls.MyIniFile.FilePath);
            txtOption10.Text = PubCls.MyIniFile.ReadData("Option", "OptionText10", "", PubCls.MyIniFile.FilePath);

        }

        private void Option_Print_btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.btw)|*.btw";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Option_txtFolderPath.Text = fileDialog.FileName;
            }
        }

        private void FrmOption_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (File.Exists(PubCls.MyIniFile.FilePath))                                          //判断是否存在该INI文件
                {
                    if (MyDevice.config.PrinterEnable)
                    {
                        PubCls.MyIniFile.WriteData("Option", "PrinterName", Option_cboPrinters.SelectedItem.ToString(), PubCls.MyIniFile.FilePath);
                        PubCls.MyIniFile.WriteData("Option", "FolderPath", Option_txtFolderPath.Text, PubCls.MyIniFile.FilePath);
                    }
                    else
                    {

                    }
                    PubCls.MyIniFile.WriteData("Option", "PhotoPath", txtOption1.Text, PubCls.MyIniFile.FilePath);
                    PubCls.MyIniFile.WriteData("Option", "CutPicPath", txtOption2.Text, PubCls.MyIniFile.FilePath);
                    PubCls.MyIniFile.WriteData("Option", "OptionText3", txtOption3.Text, PubCls.MyIniFile.FilePath);
                    PubCls.MyIniFile.WriteData("Option", "OptionText4", txtOption4.Text, PubCls.MyIniFile.FilePath);
                    PubCls.MyIniFile.WriteData("Option", "OptionText5", txtOption5.Text, PubCls.MyIniFile.FilePath);
                    PubCls.MyIniFile.WriteData("Option", "OptionText6", txtOption6.Text, PubCls.MyIniFile.FilePath);
                    PubCls.MyIniFile.WriteData("Option", "OptionText7", txtOption7.Text, PubCls.MyIniFile.FilePath);
                    PubCls.MyIniFile.WriteData("Option", "OptionText8", txtOption8.Text, PubCls.MyIniFile.FilePath);
                    PubCls.MyIniFile.WriteData("Option", "OptionText9", txtOption9.Text, PubCls.MyIniFile.FilePath);
                    PubCls.MyIniFile.WriteData("Option", "OptionText10", txtOption10.Text, PubCls.MyIniFile.FilePath);

                    //更新实时系统设置
                    if (MyDevice.config.PrinterEnable)
                    {
                        MyDevice.option.Printername = Option_cboPrinters.SelectedItem.ToString();
                        MyDevice.option.FolderPath = Option_txtFolderPath.Text;
                    }
                    else
                    {

                    }
                    MyDevice.option.PhotoPath = txtOption1.Text;
                    MyDevice.option.CutPicPath = txtOption2.Text;
                    MyDevice.option.Reserve03 = txtOption3.Text;
                    MyDevice.option.Reserve04 = txtOption4.Text;
                    MyDevice.option.Reserve05 = txtOption5.Text;
                    MyDevice.option.Reserve06 = txtOption6.Text;
                    MyDevice.option.Reserve07 = txtOption7.Text;
                    MyDevice.option.Reserve08 = txtOption8.Text;
                    MyDevice.option.Reserve09 = txtOption9.Text;
                    MyDevice.option.Reserve10 = txtOption10.Text;

                    MyLogNetFile.logNet.WriteInfo("保存设置成功");
                }
                else
                {
                    MyLogNetFile.logNet.WriteWarn("配置保存时异常");
                    MessageBox.Show("你所要修改的设置文件不存在，请确认后再进行修改操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MyLogNetFile.logNet.WriteException("配置保存时异常", ex);
            }
        }
    }
}
