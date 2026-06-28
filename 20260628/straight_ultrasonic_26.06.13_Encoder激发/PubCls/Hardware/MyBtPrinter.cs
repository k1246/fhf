using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Seagull.BarTender.Print;
namespace FlatUI_TestPlatform.PubCls
{
    public static class MyBtPrinter
    {
        public static Engine btEngine; // 定义BarTender打印引擎
        public static LabelFormatDocument btFormat; // 打印模板

        /// <summary>
        /// 打印标签例子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void PrintDemo()
        {
            //修改标签模板内容
            //读取键的值，键的名称在标签设置里定义
            //string addressSubstring = btFormat.SubStrings["地址"].Value;
            //修改，通过键修改对应的内容
            //btFormat.SubStrings["地址"].Value = "东南湖大路1726号";
            //btFormat.SubStrings["操作者"].Value = "John";
            //btFormat.SubStrings["数量"].Value = "10";
            //btFormat.SubStrings["出厂编号"].Value = tagSerial.Text;
            Result result = btFormat.Print();//Result是一个enum类型，Success=0，Timeout=1，Failure=2
        }

        /// <summary>
        /// 打开程序时加载打印功能.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void PrinterIni()
        {
            // 实例化BarTender打印引擎，并启动
            try
            {
                btEngine = new Engine(true);
            }
            catch (PrintEngineException exception)
            {
                MessageBox.Show("实例化BarTender打印引擎失败！");
                MyLogNetFile.logNet.WriteException("实例化BarTender打印引擎失败!", exception);

                return;
            }
            //打印模板的设置
            btFormat =btEngine.Documents.Open(MyDevice.option.FolderPath);
            //打印一系列标签时的设置
            btFormat.PrintSetup.NumberOfSerializedLabels = 1;//规定了一次打印多长序列的标签
            btFormat.PrintSetup.IdenticalCopiesOfLabel = 1;//规定了一次打印标签重复次数（副本数）
            // 获得打印机列表，并显示出来
            btFormat.PrintSetup.PrinterName = MyDevice.option.Printername;
        }

        /// <summary>
        /// 退出程序时关闭打印引擎
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Dispose()
        {
            // 退出打印引擎，并且不保存模板的改变
            if (btEngine != null)
            {
                btEngine.Stop(SaveOptions.DoNotSaveChanges);
                btEngine.Dispose();//释放资源
            }
        }
    }
}
