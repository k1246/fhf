using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HslCommunication.LogNet;//引用日志记录
/********************************************************************************
** 作者：王永明
** 创始时间：2022-03-23
** 修改时间：2022-03-23
** 描述：实现一个简单强大的日志记录功能，包采用线程安全实现，所有的记录在后台完成，
** 即使您在前台调用100万次方法，耗时也不过1000ms（具体时间依照电脑性能决定），支持
** 日志等级，并提供了一个控件用于分析所有的日志消息。
** http://www.hsltechnology.cn/Doc/HslCommunication
*********************************************************************************/
namespace FlatUI_TestPlatform.PubCls
{
    /// <summary>
    /// 日志记录 LogNet文件读写类
    /// 定义静态的全局变量，整个程序都可以访问
    /// </summary>
    public static class MyLogNetFile
    {
        public static ILogNet logNet;
        public static bool LogIni()
        {
            #region 日志实例化，存储方式有三种
            // 第一种：单日志，指定绝对路径，用的比较少
            //logNet = new LogNetSingle("D:\\log.txt");
            // 单日志，相对路径，与可执行程序所在的路径一致，比较常用
            //logNet = new LogNetSingle(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt"));

            // 第二种：按照文件大小切割日志，默认2M，不限制日志文件数量
            //logNet = new LogNetFileSize(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"));
            // 按照文件大小切割日志，指定10M，不限制日志文件数量
            //logNet = new LogNetFileSize(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"), 10 * 1024 * 1024);
            // 按照文件大小切割日志，指定10M，限制最多10个日志文件数量，总计最多占用 100M 日志
            //logNet = new LogNetFileSize(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"), 10 * 1024 * 1024, 10);

            // 第三种：按照时间来实例化日志，默认按照年存储
            logNet = new LogNetDateTime(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"));
            // 按照时间来实例化日志，指定按照天存储
            //logNet = new LogNetDateTime(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"), GenerateMode.ByEveryDay);
            // 按照时间来实例化日志，指定按照天存储，最多存储30天的日志
            //logNet = new LogNetDateTime(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"), GenerateMode.ByEveryDay, 30);
            // 当指定了存放了日志的文件数量上限后，就不需要担心日志文件爆炸了。但是也就意味着更早的日志信息会丢失。
            #endregion

            #region 设置等级，共5 个等级区分，分别是 Debug, Info, Warn, Error, Fatal
            //Debug 等级是用来存储调试信息，方便我用来调试的，最终部署的时候，需要屏蔽掉调试的信息，例如调用了某个接口，参数是什么？
            //Info 等级用来记录一般操作的信息，例如用户登录了系统，导出了什么数据，方便后续查找记录。
            //Warn 等级用来记录重要的操作信息，例如用户启动了设备，修改了密码，设备提示库存小于警戒，提示温度高过警戒。
            //Error 等级用来记录错误的操作信息，例如启动设备失败，设备因为错误信息导致产品的质量下降，或是造成停机。
            //Fatal 等级用来记录毁灭性的异常信息，设备宕机，损坏，严重影响产品质量，产量，绝对不允许出现的报警。

            // 此处的5个等级有高低之分 debug < info < warn < error < fatal < all
            logNet.SetMessageDegree(HslMessageDegree.DEBUG);//所有等级存储

            // 如果我们需要屏蔽debug等级的话
            //logNet.SetMessageDegree(HslMessageDegree.INFO);

            // 如果我们需要屏蔽debug及info等级的
            //logNet.SetMessageDegree(HslMessageDegree.WARN);
            #endregion

            #region 设置过滤关键字的存储
            //logNet.FiltrateKeyword("123");  // 过滤"123"的存储
            #endregion

            #region 自定义触发事件
            //自定义事件，在所有日志进行存储前会报告事件，可以用于控制台的显示等，注意：如果在事件关联方法中直接访问UI线程，会异常
            logNet.BeforeSaveToFile += LogNet_BeforeSaveToFile;
            #endregion

            return true;
        }

        //自定义的触发事件
        public static void LogNet_BeforeSaveToFile(object sender, HslEventArgs e)
        {
            // 如果需要UI显示，就要取消注释下方的代码

            //if(InvokeRequired)
            //{
            //    Invoke(new Action(() =>{
            //        LogNet_BeforeSaveToFile(sender, e);
            //    }));
            //    return;
            //}

            //控制台显示例子
            if (e.HslMessage.Degree == HslMessageDegree.DEBUG) Console.ForegroundColor = ConsoleColor.Gray;
            else if (e.HslMessage.Degree == HslMessageDegree.INFO) Console.ForegroundColor = ConsoleColor.White;
            else if (e.HslMessage.Degree == HslMessageDegree.WARN) Console.ForegroundColor = ConsoleColor.Yellow;
            else if (e.HslMessage.Degree == HslMessageDegree.ERROR) Console.ForegroundColor = ConsoleColor.Red;
            else if (e.HslMessage.Degree == HslMessageDegree.FATAL) Console.ForegroundColor = ConsoleColor.DarkRed;
            else Console.ForegroundColor = ConsoleColor.White;

            string degree = e.HslMessage.Degree.ToString();//获取等级
            DateTime time = e.HslMessage.Time;//获取时间
            string text = e.HslMessage.Text;//日志文本
            int threadId = e.HslMessage.ThreadId;//记录日志的线程id

            Console.WriteLine(e.HslMessage.ToString());//控制台显示信息
        }

        #region 应用举例
        //一般日志写入
        // 然后我们的代码就可以调用下面的方法来存储日志了，支持下面的5个等级的
        //logNet.WriteDebug( "Debug log test" );
        //logNet.WriteInfo( "Info log test" );
        //logNet.WriteWarn( "Warn log test" );
        //logNet.WriteError( "Error log test" );
        //logNet.WriteFatal( "Fatal log test" );

        // 还有下面的几种额外的情况
        //logNet.WriteNewLine( );                       // 追加一行空行
        //logNet.WriteDescrition( "test" );             // 写入额外的注释的信息
        //logNet.WriteAnyString( "any string" );        // 写任意的数据，不受格式化影响

        //logNet.WriteDebug("调试信息");
        //logNet.WriteInfo("一般信息");
        //logNet.WriteWarn("警告信息");
        //logNet.WriteError("错误信息");
        //logNet.WriteFatal("致命信息");
        //logNet.WriteException(null, new IndexOutOfRangeException());

        // 带有关键字的写入，关键字建议为方法名或是类名，方便分析的时候归类搜索
        // 带关键字的功能
        //logNet.WriteDebug( "A","Debug log test" );
        //logNet.WriteInfo( "B", "Info log test" );
        //logNet.WriteWarn( "C", "Warn log test" );
        //logNet.WriteError( "A", "Error log test" );
        //logNet.WriteFatal( "B", "Fatal log test" );

        //写异常例子
        //try
        //{
        //  int i = 0;
        //  int j = 100 / i;
        //}
        //catch(Exception ex)
        //{
        //  logNet.WriteException(textBox1.Text, ex);
        //}


        //单文件模式，具有两个额外的方法，1.获取该文件日志中所有的内容 2.清空该文件的所有数据
        //举例如下：
        //if (logNet != null)
        //{
        //    string logData = logNet.GetAllSavedLog();//获取所有的日志信息
        //    logNet.ClearLog();//清除所有的日志信息

        //上述功能好像取消了，用如下代码代替
        //显示日志
        //    if (System.IO.File.Exists("log.txt"))
        //    {

        //        using (System.IO.StreamReader sr = new System.IO.StreamReader("log.txt", Encoding.UTF8))
        //        {
        //            textBox3.Text = sr.ReadToEnd();
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("没有文件！");
        //    }

        //清除所有日志
        //System.IO.File.WriteAllBytes( "log.txt", new byte[0] );
        //}

        // 有了关键字之后，我们就可以根据关键字过滤了
        //logNet.FiltrateKeyword( "B" ); // 我们不需要存储B的关键字的日志
        //logNet.RemoveFiltrate( "B" );  // 重新需要B的关键字的日志

        //日志分析器例子
        //private void userButton13_Click(object sender, EventArgs e)
        //{
        //    // 此处演示一个使用HSL自带的日志查看器窗体，可以直接显示
        //    using (HslCommunication.LogNet.FormLogNetView form = new HslCommunication.LogNet.FormLogNetView())
        //    {
        //        form.ShowDialog();
        //    }
        //}

        #endregion
    }
}
