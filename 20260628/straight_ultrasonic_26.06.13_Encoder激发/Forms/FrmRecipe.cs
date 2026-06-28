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
namespace FlatUI_TestPlatform.Forms
{
    public partial class FrmRecipe : Form
    {
        #region  定义变量
        List<MyDB.RecipeModel> myQueryData = new List<MyDB.RecipeModel>();//定义查询获得的列表，用于表格显示
        #endregion

        #region 公共子函数
        private void WireUpRecipeList(DataGridView dgrdView)
        {
            dgrdView.DataSource = null;
            dgrdView.DataSource = myQueryData;
        }
        private void LoadRecipeList(int page)
        {
            switch(page)
            {
                case 1:
                    myQueryData = MyDB.MutiQueryRecipe(MyDB.sqlCommand.Reserve01);
                    WireUpRecipeList(dgrdViewRecipe1);
                    break;
                case 2:
                    myQueryData = MyDB.MutiQueryRecipe(MyDB.sqlCommand.Reserve02);
                    WireUpRecipeList(dgrdViewRecipe2);
                    break;

                case 3:
                    myQueryData = MyDB.MutiQueryRecipe(MyDB.sqlCommand.Reserve03);
                    WireUpRecipeList(dgrdViewRecipe3);
                    break;

                //case 4:
                //    myQueryData = MyDB.MutiQueryRecipe(MyDB.sqlCommand.Reserve04);
                //    WireUpRecipeList(dgrdViewRecipe4);
                //    break;

                //case 5:
                //    myQueryData = MyDB.MutiQueryRecipe(MyDB.sqlCommand.Reserve05);
                //    WireUpRecipeList(dgrdViewRecipe5);
                //    break;

                //case 6:
                //    myQueryData = MyDB.MutiQueryRecipe(MyDB.sqlCommand.Reserve06);
                //    WireUpRecipeList(dgrdViewRecipe6);
                //    break;

                //case 7:
                //    myQueryData = MyDB.MutiQueryRecipe(MyDB.sqlCommand.Reserve07);
                //    WireUpRecipeList(dgrdViewRecipe7);
                //    break;

                //case 8:
                //    myQueryData = MyDB.MutiQueryRecipe(MyDB.sqlCommand.Reserve08);
                //    WireUpRecipeList(dgrdViewRecipe8);
                //    break;

                //case 9:
                //    myQueryData = MyDB.MutiQueryRecipe(MyDB.sqlCommand.Reserve09);
                //    WireUpRecipeList(dgrdViewRecipe9);
                //    break;

                //case 10:
                //    myQueryData = MyDB.MutiQueryRecipe(MyDB.sqlCommand.Reserve10);
                //    WireUpRecipeList(dgrdViewRecipe10);
                //    break;

                default:
                    myQueryData = MyDB.MutiQueryRecipe(MyDB.sqlCommand.Reserve01);
                    WireUpRecipeList(dgrdViewRecipe1);
                    break;
            }
        }
        #endregion
        public FrmRecipe()
        {
            InitializeComponent();
        }

        private void btnRecipeLoad1_Click(object sender, EventArgs e)
        {
            LoadRecipeList(1);
        }
        private void btnRecipeLoad2_Click(object sender, EventArgs e)
        {
            LoadRecipeList(2);
        }

        private void btnRecipeLoad3_Click(object sender, EventArgs e)
        {
            LoadRecipeList(3);
        }

        //private void LoadRecipeList()
        //{
        //    myQueryData =DB.MutiQueryRecipe();
        //    WireUpRecipeList();
        //}

        private void btnRecipeUpdate_Click(object sender, EventArgs e)
        {
            MyDB.RecipeModel p = new MyDB.RecipeModel();
            p.ID = Convert.ToInt32(txtRecipeID.Text);
            p.配方名称 = txtRecipeName.Text;
            p.机型 = txtRecipeModel.Text;
            p.匹配尺寸 = txtRecipe1.Text;
            p.公差上限 = txtRecipe2.Text;
            p.公差下限 = txtRecipe3.Text;
            MyDB.UpdateRecipeData(p);
            LoadRecipeList(1);
        }

        private void btnRecipeAdd_Click(object sender, EventArgs e)
        {
            MyDB.RecipeModel p = new MyDB.RecipeModel();
            p.配方名称 = txtRecipeName.Text;
            p.机型 = txtRecipeModel.Text;
            p.匹配尺寸 = txtRecipe1.Text;
            p.公差上限 = txtRecipe2.Text;
            p.公差下限 = txtRecipe3.Text;
            MyDB.InsertRecipe(p);
            txtRecipeModel.Text = "";
            txtRecipe1.Text = "";
            txtRecipe2.Text = "";
            txtRecipe3.Text = "";
            txtRecipeName.Text = "";
            txtRecipeID.Text = "0";
            LoadRecipeList(1);
        }

        private void btnRecipeDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgrdViewRecipe1.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                    int x = int.Parse(dgrdViewRecipe1.SelectedRows[0].Cells[0].Value.ToString());
                    MyDB.DeleteRecipe(x);
                    LoadRecipeList(1);
                }
                else
                {
                    MessageBox.Show("请选择需要删除的行", "提示", MessageBoxButtons.OK);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(this, x.Message, "Error", MessageBoxButtons.OK);
                MyLogNetFile.logNet.WriteException("删除配方时异常", x);
            }
        }

        private void dgrdViewRecipe1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgrdViewRecipe1.Rows[e.RowIndex];
            txtRecipeID.Text = row.Cells[0].Value.ToString();
            txtRecipeName.Text = row.Cells[1].Value.ToString();
            txtRecipeModel.Text = row.Cells[2].Value.ToString();
            txtRecipe1.Text = row.Cells[3].Value.ToString();
            txtRecipe2.Text = row.Cells[4].Value.ToString();
            txtRecipe3.Text = row.Cells[5].Value.ToString();
        }

        private void btnRecipeQuery_Click(object sender, EventArgs e)
        {
            MyDB.RecipeModel p = new MyDB.RecipeModel();
            p.ID = Convert.ToInt32(txtRecipeID.Text);
            p.配方名称 = txtRecipeName.Text;
            p.机型 = txtRecipeModel.Text;
            p.匹配尺寸 = txtRecipe1.Text;
            p.公差上限 = txtRecipe2.Text;
            p.公差下限 = txtRecipe3.Text;
            dgrdViewRecipe1.DataSource =MyDB.QueryRecipe(p);
        }

        //选择配方，配方参数初始化，此功能可以放到主界面，由配方名称来选择
        private void btnRecipeSel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgrdViewRecipe1.SelectedRows.Count > 0)
                {
                    MyDevice.recipe1.Reserve01 = dgrdViewRecipe1.SelectedRows[0].Cells[1].Value.ToString();
                    MyDevice.recipe1.Reserve02 = dgrdViewRecipe1.SelectedRows[0].Cells[2].Value.ToString();
                    MyDevice.recipe1.Reserve03 = dgrdViewRecipe1.SelectedRows[0].Cells[3].Value.ToString();
                    MyDevice.recipe1.Reserve04 = dgrdViewRecipe1.SelectedRows[0].Cells[4].Value.ToString();
                    MyDevice.recipe1.Reserve05 = dgrdViewRecipe1.SelectedRows[0].Cells[5].Value.ToString();
                    //MyDevice.recipe.Reserve06 = dgrdViewRecipe.SelectedRows[0].Cells[6].Value.ToString();
                    //MyDevice.recipe.Reserve07 = dgrdViewRecipe.SelectedRows[0].Cells[7].Value.ToString();
                    //MyDevice.recipe.Reserve08 = dgrdViewRecipe.SelectedRows[0].Cells[8].Value.ToString();
                    //MyDevice.recipe.Reserve09 = dgrdViewRecipe.SelectedRows[0].Cells[9].Value.ToString();
                    //MyDevice.recipe.Reserve10 = dgrdViewRecipe.SelectedRows[0].Cells[10].Value.ToString();
                }
                else
                {
                    MessageBox.Show("请先查询配方后再选择对应配方的行！", "提示", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                MyLogNetFile.logNet.WriteException("选择配方异常", ex);
            }
        }


    }
}
