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
using MiniExcelLibs;
using MiniSoftware;

namespace FlatUI_TestPlatform.Forms
{
    public partial class FrmReview : Form
    {
        #region  定义变量
        List<MyDB.ReviewModel> myQueryData = new List<MyDB.ReviewModel>();//定义查询获得的列表，用于表格显示
        #endregion

        #region 公共子函数
        private void WireUpReviewList()
        {
            dgrdViewReview.DataSource = null;
            dgrdViewReview.DataSource = myQueryData;
        }
        private void LoadReviewList()
        {
            myQueryData = MyDB.MutiQueryReview();
            WireUpReviewList();
        }
        #endregion
        public FrmReview()
        {
            InitializeComponent();
        }

        private void btnReviewLoad_Click(object sender, EventArgs e)
        {
            LoadReviewList();
        }

        //private void LoadReviewList()
        //{
        //    myQueryData =DB.MutiQueryReview();
        //    WireUpReviewList();
        //}

        private void btnReviewUpdate_Click(object sender, EventArgs e)
        {
            MyDB.ReviewModel p = new MyDB.ReviewModel();
            p.ID = Convert.ToInt32(txtReviewID.Text);
            p.产品名称 = txtReviewName.Text;
            p.机型 = txtReviewModel.Text;
            p.流水号 = txtReview1.Text;
            p.操作者 = txtReview2.Text;
            p.结果值 = txtReview3.Text;
            p.填隙片规格 = txtReview4.Text; ;
            p.日期时间 = txtReview5.Text; ;
            MyDB.UpdateReviewData(p);
            LoadReviewList();
        }

        private void btnReviewAdd_Click(object sender, EventArgs e)
        {
            MyDB.ReviewModel p = new MyDB.ReviewModel();
            p.ID = Convert.ToInt32(txtReviewID.Text);
            p.产品名称 = txtReviewName.Text;
            p.机型 = txtReviewModel.Text;
            p.流水号 = txtReview1.Text;
            p.操作者 = txtReview2.Text;
            p.结果值 = txtReview3.Text;
            p.填隙片规格 = txtReview4.Text; ;
            p.日期时间 = txtReview5.Text; ;
            MyDB.InsertReview(p);
            txtReviewModel.Text = "";
            txtReview1.Text = "";
            txtReview2.Text = "";
            txtReview3.Text = "";
            txtReview4.Text = "";
            txtReview5.Text = "";
            txtReviewName.Text = "";
            txtReviewID.Text = "0";
            LoadReviewList();
        }

        private void btnReviewDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgrdViewReview.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                    int x = int.Parse(dgrdViewReview.SelectedRows[0].Cells[0].Value.ToString());
                    MyDB.DeleteReview(x);
                    LoadReviewList();
                }
                else
                {
                    MessageBox.Show("请选择需要删除的行", "提示", MessageBoxButtons.OK);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(this, x.Message, "Error", MessageBoxButtons.OK);
                MyLogNetFile.logNet.WriteException("删除结果时异常", x);
            }
        }

        private void dgrdViewReview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgrdViewReview.Rows[e.RowIndex];
            txtReviewID.Text = row.Cells[0].Value.ToString();
            txtReviewName.Text = row.Cells[1].Value.ToString();
            txtReviewModel.Text = row.Cells[2].Value.ToString();
            txtReview1.Text = row.Cells[3].Value.ToString();
            txtReview2.Text = row.Cells[4].Value.ToString();
            txtReview3.Text = row.Cells[5].Value.ToString();
            txtReview4.Text = row.Cells[6].Value.ToString();
            txtReview5.Text = row.Cells[7].Value.ToString();

        }

        private void btnReviewQuery_Click(object sender, EventArgs e)
        {
            MyDB.ReviewModel p = new MyDB.ReviewModel();
            p.ID = Convert.ToInt32(txtReviewID.Text);
            p.产品名称 = txtReviewName.Text;
            p.机型 = txtReviewModel.Text;
            p.流水号 = txtReview1.Text;
            p.操作者 = txtReview2.Text;
            p.结果值 = txtReview3.Text;
            p.填隙片规格= txtReview4.Text; ;
            p.日期时间= txtReview5.Text; ;
            dgrdViewReview.DataSource = MyDB.QueryReview(p);
        }

        private void btnReviewToExcel_Click(object sender, EventArgs e)
        {
            //设置保存文件对话框
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel文件（*.xlsx）|*.xlsx";//过滤文件格式
            saveFileDialog.RestoreDirectory = true;//记住默认路径
            saveFileDialog.FileName = "";//默认文件名
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
             {
                string fileName =saveFileDialog.FileName.ToString();//设置好的要保存的文件名
                //1、DataTable整个表异步导出
                MiniExcel.SaveAsAsync(
                path: fileName,
                dgrdViewReview.DataSource,
                printHeader: true,
                sheetName: "Sheet1",
                excelType: ExcelType.UNKNOWN,
                configuration: null,
                overwriteFile: true);

                //2、DataTable整个表异步模板导出
                var value = new Dictionary<string, object>()
                {
                    ["Title"] = "天亿自动化",
                    ["Managers"] = dgrdViewReview.DataSource,
                };
                MiniExcel.SaveAsByTemplateAsync(fileName,PubCls.MyExcelFile.templatePath, value);

                MessageBox.Show("导出Excel文件成功！");
             }
        }

        private void btnReviewToWord_Click(object sender, EventArgs e)
        {
            //设置保存文件对话框
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Word文件（*.docx）|*.docx";//过滤文件格式
            saveFileDialog.RestoreDirectory = true;//记住默认路径
            saveFileDialog.FileName = "";//默认文件名
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName.ToString();//设置好的要保存的文件名

                var value = new Dictionary<string, object>()
                {
                    ["Logo"] = new MiniWordPicture() { Path = Application.StartupPath + "\\Images\\test\\2.jpg", Width = 160, Height = 90 },
                    ["Unit"] = "吉林省天亿自动化",
                    ["Title"] = "拉伸试验",
                    ["TripHs"] = new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object>
                        {
                            { "sDate",DateTime.Parse("2022-09-08 08:30:00")},
                            { "eDate",DateTime.Parse("2022-09-08 15:00:00")},
                            { "How","Discussion requirement part1"},
                            { "Photo",new MiniWordPicture() { Path =Application.StartupPath+"\\Images\\test\\1.jpg", Width = 160, Height = 90 }},
                         },
                        new Dictionary<string, object>
                        {
                            { "sDate",DateTime.Parse("2022-09-09 08:30:00")},
                            { "eDate",DateTime.Parse("2022-09-09 17:00:00")},
                            { "How","Discussion requirement part2 and development"},
                            { "Photo",new MiniWordPicture() { Path =Application.StartupPath+"\\Images\\test\\2.jpg", Width = 160, Height = 90 }},
                        },
                     }
                };
                MiniSoftware.MiniWord.SaveAsByTemplate(fileName, PubCls.MyWordlFile.templatePath, value);
            }
            MessageBox.Show("导出Word文件成功！");
        }
    }
}
