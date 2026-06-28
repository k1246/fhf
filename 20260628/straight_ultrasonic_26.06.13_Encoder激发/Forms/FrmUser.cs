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
    public partial class FrmUser : Form
    {
        #region  定义变量
        List<MyDB.UserModel> myQueryData = new List<MyDB.UserModel>();//定义查询获得的列表，用于表格显示
        #endregion

        #region 公共子函数
        private void WireUpPeopleList()
        {
            dgrdViewUser.DataSource = null;
            dgrdViewUser.DataSource = myQueryData;
        }
        private void LoadPeopleList()
        {
            myQueryData = MyDB.MutiQueryUser();
            WireUpPeopleList();
        }
        #endregion

        public FrmUser()
        {
            InitializeComponent();
        }

        private void btnUserLoad_Click(object sender, EventArgs e)
        {
            LoadPeopleList();
        }

        private void btnUserUpdate_Click(object sender, EventArgs e)
        {
            MyDB.UserModel p=new MyDB.UserModel();
            p.ID = Convert.ToInt32(txtID.Text);
            p.员工编号 = txtUserEmployeeID.Text;
            p.姓名 = txtUserName.Text;
            p.密码 = txtUserPassword.Text;
            p.权限 = txtUserLevel.Text;
            MyDB.UpdateUserData(p);
            LoadPeopleList();
        }

        private void btnUserAdd_Click(object sender, EventArgs e)
        {
            MyDB.UserModel p = new MyDB.UserModel();
            p.员工编号 = txtUserEmployeeID.Text;
            p.姓名 = txtUserName.Text;
            p.密码 = txtUserPassword.Text;
            p.权限 = txtUserLevel.Text;
            MyDB.InsertUser(p);
            txtUserName.Text = "";
            txtUserPassword.Text = "";
            txtUserLevel.Text = "";
            txtUserEmployeeID.Text = "";
            txtID.Text = "0";
            LoadPeopleList();
        }

        private void btnUserDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgrdViewUser.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                    int x = int.Parse(dgrdViewUser.SelectedRows[0].Cells[0].Value.ToString());
                    MyDB.DeleteUser(x);
                    LoadPeopleList();
                }
                else
                {
                    MessageBox.Show("请选择需要删除的行", "提示", MessageBoxButtons.OK);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(this, x.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void dgrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgrdViewUser.Rows[e.RowIndex];
            txtID.Text = row.Cells[0].Value.ToString();
            txtUserEmployeeID.Text = row.Cells[1].Value.ToString();
            txtUserName.Text = row.Cells[2].Value.ToString();
            txtUserPassword.Text = row.Cells[3].Value.ToString();
            txtUserLevel.Text = row.Cells[4].Value.ToString();
        }

        private void btnUserQuery_Click(object sender, EventArgs e)
        {
            MyDB.UserModel p = new MyDB.UserModel();
            p.ID = Convert.ToInt32(txtID.Text);
            p.员工编号 = txtUserEmployeeID.Text;
            p.姓名 = txtUserName.Text;
            p.密码 = txtUserPassword.Text;
            p.权限 = txtUserLevel.Text;

            dgrdViewUser.DataSource =MyDB.QueryUser(p);
        }
    }
}
