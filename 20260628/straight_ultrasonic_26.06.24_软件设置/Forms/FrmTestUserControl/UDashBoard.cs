using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlatUI_TestPlatform.PubCls;
namespace FlatUI_TestPlatform.Forms.FrmTestUserControl
{
    public partial class UDashBoard : UserControl
    {
        Random rnd = new Random();
        public UDashBoard()
        {
            InitializeComponent();
        }

        private void timerDashBoard_Tick(object sender, EventArgs e)
        {
            hslLabelCombo1.TextValue = String.Format("{0:0.0000}", MyDevice.PosX.DisplayVal);
            hslLabelCombo2.TextValue = String.Format("{0:0.0000}", MyDevice.PosY.DisplayVal);
            hslLabelCombo3.TextValue = String.Format("{0:0.0000}", MyDevice.PosZ.DisplayVal);
            hslLabelCombo4.TextValue = String.Format("{0:0.0000}", MyDevice.Load3X.DisplayVal);
            hslLabelCombo5.TextValue = String.Format("{0:0.0000}", MyDevice.Load3Z.DisplayVal);
            hslLabelCombo6.TextValue = String.Format("{0:0.0000}", MyDevice.Load3Y.DisplayVal);
            hslLabelCombo7.TextValue = String.Format("{0:0.0000}", MyDevice.LoadY.DisplayVal);
            hslLabelCombo8.TextValue = String.Format("{0:0.0000}", MyDevice.LoadP1.DisplayVal);
            hslLabelCombo9.TextValue = String.Format("{0:0.0000}", MyDevice.LoadP2.DisplayVal);
            hslLabelCombo10.TextValue = String.Format("{0:0.0000}", MyDevice.LoadP3.DisplayVal);
            hslLabelCombo11.TextValue = String.Format("{0:0.0000}", MyDevice.LoadP4.DisplayVal);
            hslLabelCombo12.TextValue = String.Format("{0:0.0000}", rnd.NextDouble());
            //hslLabelCombo13.TextValue = String.Format("{0:0.0000}", MyDevice.PosX.DisplayVal);
            //hslLabelCombo14.TextValue = String.Format("{0:0.0000}", MyDevice.PosX.DisplayVal);
            //hslLabelCombo15.TextValue = String.Format("{0:0.0000}", MyDevice.PosX.DisplayVal);
            //hslLabelCombo16.TextValue = String.Format("{0:0.0000}", MyDevice.PosX.DisplayVal);
            //hslLabelCombo17.TextValue = String.Format("{0:0.0000}", MyDevice.PosX.DisplayVal);
            //hslLabelCombo18.TextValue = String.Format("{0:0.0000}", MyDevice.PosX.DisplayVal);

        }
    }
}
