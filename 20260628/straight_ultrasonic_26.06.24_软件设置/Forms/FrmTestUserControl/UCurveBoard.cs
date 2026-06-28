using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlatUI_TestPlatform.Forms.FrmTestUserControl
{
    public partial class UCurveBoard : UserControl
    {
        public UCurveBoard()
        {
            InitializeComponent();
        }

        private void timerCurveBoard_Tick(object sender, EventArgs e)
        {
            //根据曲线设置绘制曲线
            //switch (试验名称或序号)
            //{
            //    case 1:
            //        //绘制曲线
            //        break;
            //    case 2:
            //        //绘制曲线
            //        break;
            //    default:
            //        break;
            //}
            //保存所有曲线数据至文本文件

            //uint col = (uint)ColorTranslator.ToOle(Color.Blue);//Color类型转换OLE_Color类型
            //// 下面是动态应用的数据添加代码，只需要向Series对象的Points属性种添加ChartPoint对象就可以动态添加数据。
            //double y = Math.Sin(x / 180f * Math.PI) * 10d;
            //axTChart1.Series(0).AddXY(x, y, "", col);
            //x += 1.53;
        }
    }
}
