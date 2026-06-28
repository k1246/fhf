using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using thinger.DataConvertLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using FlatUI_TestPlatform;
//using Demo;

namespace FlatUI_TestPlatform.Forms
{
    public partial class FrmAscan : Form
    {
        private bool isShowing = false;
        private const double XAxisRange = 10D;
        private bool isLoading = false;
        ScanData data = new ScanData();

        //public FrmAscan()
        //{
        //    InitializeComponent();
        //    //area.AxisX.Maximum = XAxisRange;
        //    textChannel.Text = (trackBarChannel.Value + 1).ToString();
        //    //double start_time = double.Parse(FormMain.currentChildForm);
        //}


        //private void FrmShow_Load(object sender, EventArgs e)
        //{
        //    hslBtnRecord.Enabled = false;
        //}

        //private void hslRecord_Click(object sender, EventArgs e)
        //{
        //    if (!isShowing)
        //    {
        //        // 开始记录
        //        var pts = data.GetNewPoints(trackBarChannel.Value);
        //        chartshow.Series[0].Points.DataBindXY(pts[0], pts[1]);
        //    }
        //    else
        //    {
        //        // 结束记录

        //    }
        //    isShowing = !isShowing;

        //    UpdateButtonAppearance();
        //}

        ////公共函数
        //private void UpdateButtonAppearance()
        //{
        //    if (isShowing)
        //    {
        //        hslBtnRecord.Text = "清空";
        //        hslBtnRecord.ForeColor = System.Drawing.Color.Red;

        //    }
        //    else
        //    {
        //        hslBtnRecord.Text = "显示";
        //        hslBtnRecord.ForeColor = SystemColors.ControlText;
        //        chartshow.Series["Series1"].Points.Clear();
        //    }
        //}


        //private void hslBtnSkip_Click(object sender, EventArgs e)
        //{
        //    // 2. 尝试解析
        //    if (int.TryParse(textBoxBar.Text, out int val) && val >= 1 && val <= 64)
        //    {
        //        // 合法：让 TrackBar 跳转（自动限制在 Minimum~Maximum 之间）
        //        trackBarChannel.Value = val - 1;
        //    }
        //    else
        //    {
        //        // 非法：文本框置空，可选提示
        //        textBoxBar.Clear();
        //    }
        //}
        

        //private void trackBarChannel_Change(object sender, EventArgs e)
        //{
        //    textChannel.Text = (trackBarChannel.Value + 1).ToString();
        //    if (isShowing)
        //    {
        //        var pts = data.GetNewPoints(trackBarChannel.Value);
        //        chartshow.Series[0].Points.DataBindXY(pts[0], pts[1]);
        //    }
        //}
        public sealed class ScanData
        {
            private readonly List<double[]> _mat = new List<double[]>(64);
            private double[] _pos = Array.Empty<double>();

            public void ReplaceRow(int index, double[] row)
            {
                if ((uint)index >= 64) throw new ArgumentOutOfRangeException(nameof(index));
                if (row is null) throw new ArgumentNullException(nameof(row));

                while (_mat.Count <= index) _mat.Add(null);
                _mat[index] = row;
            }

            public void ReplaceMatrix(double[,] src)
            {
                if (src is null) throw new ArgumentNullException(nameof(src));
                if (src.GetLength(0) != 64) throw new ArgumentException("");

                int cols = src.GetLength(1);
                for (int r = 0; r < 64; r++)
                {
                    double[] row = new double[cols];
                    for (int c = 0; c < cols; c++) row[c] = src[r, c];
                    ReplaceRow(r, row);
                }
            }

            public void ReplaceMatrix(IReadOnlyList<double[]> src)
            {
                if (src is null) throw new ArgumentNullException(nameof(src));
                if (src.Count != 64) throw new ArgumentException("");

                for (int r = 0; r < 64; r++) ReplaceRow(r, src[r]);
            }

            public double[] GetRow(int index) => _mat[index];

            public double[] this[int r] => GetRow(r);

            public int RowCount => _mat.Count;

            public int ColCount(int r) => _mat[r]?.Length ?? 0;

            public void ReplacePos(double[] newPos) => _pos = newPos ?? throw new ArgumentNullException(nameof(newPos));

            public double[] GetPos() => _pos;

            public double[][] GetNewPoints(int rowIndex)
            {
                if ((uint)rowIndex >= 64) throw new ArgumentOutOfRangeException(nameof(rowIndex));
                double[] y = _mat[rowIndex] ?? throw new InvalidOperationException();
                int n = y.Length;

                double[] x = _pos;
                if (x.Length != n)
                {
                    x = new double[n];
                    Array.Copy(_pos, x, Math.Min(_pos.Length, n));
                    for (int i = _pos.Length; i < n; i++) x[i] = double.NaN;
                }
                return new double[2][] { x, y };
            }
            public double MatMin
            {
                get
                {
                    if (_mat.Count == 0) return double.NaN;
                    double min = double.MaxValue;
                    foreach (var row in _mat)
                    {
                        if (row == null) continue;
                        for (int i = 0; i < row.Length; i++)
                        {
                            double v = row[i];
                            if (v < min) min = v;
                        }
                    }
                    return min;
                }
            }

            public double MatMax
            {
                get
                {
                    if (_mat.Count == 0) return double.NaN;
                    double max = double.MinValue;
                    foreach (var row in _mat)
                    {
                        if (row == null) continue;
                        for (int i = 0; i < row.Length; i++)
                        {
                            double v = row[i];
                            if (v > max) max = v;
                        }
                    }
                    return max;
                }
            }

            public double PosMin => _pos.Length == 0 ? double.NaN : _pos[0];
            public double PosMax => _pos.Length == 0 ? double.NaN : _pos[_pos.Length - 1];
        }

        //private void hslBtnLoading_Click(object sender, EventArgs e)
        //{
        //    if (!isLoading)
        //    {
        //        hslBtnLoading.Text = "重新加载";
        //        isLoading = true;
        //        //UpdateScanData(TestData.MatrixA, TestData.Pos_mm);
        //        hslBtnRecord.Enabled = true;
        //    }
        //    else
        //    {
        //        //UpdateScanData(TestData.MatrixB, TestData.Pos_mm);
        //    }
        //}

        //private void UpdateScanData(double[,] _Matrix, double[] _Pos_mm)
        //{
        //    data.ReplaceMatrix(_Matrix);// 64×128 整块秒导
        //    data.ReplacePos(_Pos_mm);// 位置向量
        //    var area = chartshow.ChartAreas["ChartArea1"];   // 如果设计器没建，代码里 new 也行
        //    area.AxisY.Maximum = Math.Max(Math.Abs(data.MatMax), Math.Abs(data.MatMin));
        //    area.AxisY.Minimum = -area.AxisY.Maximum;
        //    area.AxisX.Maximum = data.PosMax;
        //    area.AxisX.Minimum = data.PosMin;
        //}

        public static IEnumerable<double> GenerateDoubleSequence(double start, double step, double end)
        {
            for (double current = start; current <= end + step * 0.5; current += step)
            {
                yield return current;
            }
        }


    }
}
