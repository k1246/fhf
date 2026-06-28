using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HslCommunication.LogNet;// 日志记录
using HslControls.Charts;//Chart部件
using System.Speech.Synthesis;//第一步，引用
using FlatUI_TestPlatform.PubCls;
using Seagull.BarTender.Print;
using HslCommunication;
using thinger.DataConvertLib;//数据转换库
using Newtonsoft.Json;
namespace FlatUI_TestPlatform.Forms
{
    public partial class FrmDebug : Form
    {
        public FrmDebug()
        {
            InitializeComponent();
            LineMonitorChartInit();

        }

        private void FrmDebug_Load(object sender, EventArgs e)
        {
            #region
            comboBox1.DataSource = HslCommunication.BasicFramework.SoftBasic.GetEnumValues<HslMessageDegree>();
            comboBox1.SelectedItem = HslMessageDegree.DEBUG;
            ComboBox2.DataSource = HslCommunication.BasicFramework.SoftBasic.GetEnumValues<HslMessageDegree>();
            ComboBox2.SelectedItem = HslMessageDegree.DEBUG;
            ComboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
            if (MyDevice.config.SpeechEnable)
            {
                gbxSpeech.Enabled = true;
            }
            else
            {
                gbxSpeech.Enabled = false;
            }

            #endregion
        }
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            HslMessageDegree degree = (HslMessageDegree)ComboBox2.SelectedItem;
            PubCls.MyLogNetFile.logNet.SetMessageDegree(degree);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PubCls.MyLogNetFile.logNet.WriteDebug("this is test!", "调试信息");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PubCls.MyLogNetFile.logNet.WriteNewLine();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PubCls.MyLogNetFile.logNet.WriteDescrition("这是一条注释");
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                int j = 100 / i;
            }
            catch (Exception ex)
            {
                PubCls.MyLogNetFile.logNet.WriteException(textBox1.Text, ex);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 100 / i;
        }


        #region 动态曲线
        private ChartPointCollection points;

        private Timer timer;

        private double x = 0d;

        private void LineMonitorChartInit()
        {
            // 可以这样直接获取Series对象的数据点引用
            points = lineMonitorChart.Series[0].Points;
            timer = new Timer() { Interval = 100 };
            timer.Tick += (s, e) =>
            {
                // 下面是动态应用的数据添加代码，只需要向Series对象的Points属性种添加ChartPoint对象就可以动态添加数据。
                double y = Math.Sin(x / 180f * Math.PI) * 10d;
                points.Add(new ChartPoint(x, y));
                x += 100.53;
            };
        }
        private void btnTimerSwitch_Click(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
            btnTimerSwitch.Text = timer.Enabled ? "Stop" : "Start";

        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
                btnTimerSwitch_Click(null, EventArgs.Empty);
            x = 0d;
            // 清空Series对象的数据点
            points.Clear();
        }


        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            // 将控件内容绘制成指定尺寸的位图
            Bitmap bmp = new Bitmap(1080, 720);
            lineMonitorChart.DrawToBitmap(bmp);
            bmp.Save(Application.StartupPath + string.Format(@"\LineMonitor.bmp"));
        }
        #endregion

        private void btnSetZoom_Click(object sender, EventArgs e)
        {
            lineMonitorChart.OffsetX=0;
            lineMonitorChart.OffsetY=0;
            lineMonitorChart.SetZoomTimes(0);

        }
        private Point pt;
        private void lineChart_MouseUp(object sender, MouseEventArgs e)
        {
            pt = e.Location;
        }

        #region Table
        private int count = 0;
        private Random random = new Random();

        private void button2_Click(object sender, EventArgs e)
        {
            // 新增顶部数据
            count++;
            hslTable1.AddRowTop(new string[] { count.ToString(), "A1-1-1", random.Next(1000, 10000).ToString(), "80 %", "应到 16 人，实到 15 人" });

        }

        private void button8_Click(object sender, EventArgs e)
        {
            // 新增底部数据
            count++;
            hslTable1.AddRowDown(new string[] { count.ToString(), "A1-1-1", random.Next(1000, 10000).ToString(), "80 %", "应到 16 人，实到 15 人" });

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 清楚数据
            hslTable1.SetTableValue(new List<string[]>());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 设定数据
            try
            {
                int row = int.Parse(textBox5.Text);
                int col = int.Parse(textBox4.Text);
                hslTable1.SetTableValue(row, col, textBox3.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置数据失败！" + ex.Message);
            }

        }
        private void button10_Click(object sender, EventArgs e)
        {
            hslTable1.SetTableValue(new List<string[]>()
            {
                new string[] { "φ30", "1000", "A类型", "800", "" },
                new string[] { "φ100", "1000", "A类型", "800", "" },
                new string[] { "φ500", "3000", "A类型", "1000", "" },
            });

        }

        #endregion

        private void hslButton1_Click(object sender, EventArgs e)
        {
            PubCls.MyIniFile.WriteData("DEBUG","结果",textBox_WriteData.Text,PubCls.MyIniFile.FilePath);
        }

        private void hslButton2_Click(object sender, EventArgs e)
        {
            textBox_ReadData.Text=PubCls.MyIniFile.ReadData("DEBUG", "结果", "ABC", PubCls.MyIniFile.FilePath);
        }

        readonly double[] Values = new double[100_000];

        private void button11_Click(object sender, EventArgs e)
        {
            //MyDevice.speech.Rate = /*rate*/;
            MyDevice.speech.Volume = 100;
            MyDevice.speech.SpeakAsync(textBox6.Text);
        }

        private void hslButton3_Click(object sender, EventArgs e)
        {
            PubCls.MyBtPrinter.btFormat.SubStrings["机型代码"].Value = txt_tagSerial.Text;
            PubCls.MyBtPrinter.btFormat.SubStrings["年份代码"].Value = "22";
            PubCls.MyBtPrinter.btFormat.SubStrings["月份代码"].Value = "1";
            PubCls.MyBtPrinter.btFormat.SubStrings["填隙片规格"].Value = "1.01";
            Result result =PubCls.MyBtPrinter.btFormat.Print();//Result是一个enum类型，Success=0，Timeout=1，Failure=2
        }

        private void hslButton4_Click(object sender, EventArgs e)
        {
            if (ClsFX3U.Connect("COM6", 9600, 7))
            {
                ClsFX3U.ItTable_Ini();
                MessageBox.Show("连接成功");
                timer1.Enabled = true;
                //timer1.Enabled = true;
            }
        }

        private void hslButton5_Click(object sender, EventArgs e)
        {
            // 断开连接
            timer1.Enabled = false;
            ClsFX3U.Dispose();
        }

        private void hslButton6_Click(object sender, EventArgs e)
        {
            //byte[] Data = ClsFX3U.MutiRead("D100", 20);
            //richTextBox1.Text = Data.ToArrayString();
            HslCommunication.OperateResult<byte[]> read = ClsFX3U.Socket.Read("D100", 20);
            richTextBox1.Text =read.Content.ToArrayString();
        }

        private  void hslButton7_Click(object sender, EventArgs e)
        {
            //ClsFX3U.Write("系统控制", 100);
            //ClsFX3U.Write("系统错误", 111);
            //await ClsFX3U.Socket.WriteAsync("D100", 3);
            //ClsFX3U.Socket.Write("D102", 2);
            HslCommunication.OperateResult write =  ClsFX3U.Socket.Write("D33", 3);


        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            HslCommunication.OperateResult<byte[]> read1 =await ClsFX3U.Socket.ReadAsync("D33", 5);
            HslCommunication.OperateResult<byte[]> read2 = await ClsFX3U.Socket.ReadAsync("D38", 5);
            HslCommunication.OperateResult<byte[]> read3 = await ClsFX3U.Socket.ReadAsync("D43", 5);
            HslCommunication.OperateResult<byte[]> read4 = await ClsFX3U.Socket.ReadAsync("D48", 5);
            richTextBox1.Text = read1.Content.ToArrayString();
        }

        #region MES 采用JSON格式进行，下面例子是结构体实例与JSON格式转换例子
        private void btnJsonConvertDeserialize_Click(object sender, EventArgs e)
        {
            MyMES.MesInfo = JsonConvert.DeserializeObject<MyMES.MesInfoModel>(textBox7.Text);
        }

        private void btnJsonConvertSerialize_Click(object sender, EventArgs e)
        {
            MyMES.MesJSON = JsonConvert.SerializeObject(MyMES.MesInfo);
        }
        #endregion
    }
}
