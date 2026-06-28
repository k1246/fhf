using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;
using FlatUI_TestPlatform.PubCls;

namespace FlatUI_TestPlatform.Forms
{
    public partial class FrmLogin : Form
    {
        #region  定义变量
        List<MyDB.UserModel> myQueryData = new List<MyDB.UserModel>();//定义查询获得的列表，用于表格显示
        #endregion

        public FrmLogin()
        {
            InitializeComponent();
            ReadOut();//初始化默认用户
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                //更改提示状态
                this.label3.Text = "正在登录...";
                this.label3.ForeColor = Color.Blue;
                try
                {
                    //根据员工编号和密码查询
                    MyDB.UserModel tempP = new MyDB.UserModel();
                    tempP.员工编号 =txtName.Text;
                    tempP.密码 = txtPassword.Text;
                    myQueryData = MyDB.QueryUser(tempP);
                    
                    if (myQueryData.Count > 0)
                    {
                        //查询账户名称
                        MyDevice.user.UserNo= myQueryData[0].ID.ToString();
                        MyDevice.user.UserName= myQueryData[0].姓名;
                        MyDevice.user.UserPsssword= myQueryData[0].密码.ToString();
                        MyDevice.user.UserLevel = myQueryData[0].权限.ToString();

                        if (this.cb.Checked)
                        {
                            WriteIn(1, MyDevice.user.UserName);
                        }
                        else
                        {
                            WriteIn(0, MyDevice.user.UserName);
                        }
                        this.Dispose();//释放本窗口
                    }
                    else
                    {
                        MessageBox.Show("账号或密码错误！请检查后登录", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //更改提示状态
                        this.label3.Text = "登录失败";
                        this.label3.ForeColor = Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MyLogNetFile.logNet.WriteException("登录时异常", ex);
                }
            }
        }

        //输入内容检查
        private bool Check()
        {
            if (txtName.Text.Trim().Equals("") || txtName.Text == null)
            {
                MessageBox.Show("请输入用户名！\t", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            else if (txtPassword.Text.Trim().Equals("") || txtPassword.Text == null)
            {
                MessageBox.Show("请输入密码！\t", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        //是否记住密码
        private void cb_CheckedChanged(object sender, EventArgs e)
        {
            //if (Check())
            //{
            //    if (this.cb.Checked) //如果选中了则执行记录账号和密码操作
            //    {
            //        WriteIn(1);
            //    }
            //    else
            //    {
            //        ClearConfig();
            //    }
            //}
        }
        //写入文档
        /// <summary>
        /// 记住账号和密码
        /// </summary>
        /// <param name="mode">0：记住登录用户名 1：记住用户名和密码</param>
        private void WriteIn(int mode,string name)
        {
            string File = Application.StartupPath;
            string path = Application.StartupPath + "\\id_pwd\\PasswordConfig.txt";
            string content = null;
            switch (mode)
            {
                case 0:
                    content = this.txtName.Text.Trim() + "," + "" + ",1,"+name;
                    break;
                case 1:
                    content = this.txtName.Text.Trim() + "," + this.txtPassword.Text.Trim() + ",1,"+name;
                    break;
                default:
                    content = this.txtName.Text.Trim() + "," + "" + ",1,"+name;
                    break;
            }
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(content);  //写入文档
            sw.Close();
            fs.Close();
        }

        //读取文档
        private void ReadOut()
        {
            //先将内容读取出来
            string File = Application.StartupPath;
            string path = Application.StartupPath + "\\id_pwd\\PasswordConfig.txt";
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string content = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            //对内容进行分割处理
            if (content != "")
            {
                string[] pwd_id = content.Split(',');
                this.txtName.Text = pwd_id[0];  //将内容写进文本框中
                this.txtPassword.Text = pwd_id[1];
                if (pwd_id[2] == "1")
                {
                    this.cb.Checked = true;
                }
            }
        }

        //清空文档
        private void ClearConfig()
        {
            string File = Application.StartupPath;
            string path = Application.StartupPath + "\\id_pwd\\PasswordConfig.txt";
            string content = "";
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(content);  //写入文档
            sw.Close();
            fs.Close();
        }
    }
}
