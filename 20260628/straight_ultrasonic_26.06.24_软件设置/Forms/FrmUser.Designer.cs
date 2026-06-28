namespace FlatUI_TestPlatform.Forms
{
    partial class FrmUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgrdViewUser = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUserQuery = new FontAwesome.Sharp.IconButton();
            this.btnUserDel = new FontAwesome.Sharp.IconButton();
            this.btnUserLoad = new FontAwesome.Sharp.IconButton();
            this.btnUserUpdate = new FontAwesome.Sharp.IconButton();
            this.btnUserAdd = new FontAwesome.Sharp.IconButton();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtUserLevel = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserEmployeeID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdViewUser)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgrdViewUser
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrdViewUser.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgrdViewUser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgrdViewUser.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgrdViewUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgrdViewUser.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrdViewUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdViewUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrdViewUser.Location = new System.Drawing.Point(0, 200);
            this.dgrdViewUser.Name = "dgrdViewUser";
            this.dgrdViewUser.ReadOnly = true;
            this.dgrdViewUser.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrdViewUser.RowHeadersWidth = 51;
            this.dgrdViewUser.RowTemplate.Height = 27;
            this.dgrdViewUser.Size = new System.Drawing.Size(1702, 778);
            this.dgrdViewUser.TabIndex = 3;
            this.dgrdViewUser.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdView_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.btnUserQuery);
            this.groupBox1.Controls.Add(this.btnUserDel);
            this.groupBox1.Controls.Add(this.btnUserLoad);
            this.groupBox1.Controls.Add(this.btnUserUpdate);
            this.groupBox1.Controls.Add(this.btnUserAdd);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Controls.Add(this.txtUserLevel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtUserPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtUserEmployeeID);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1702, 200);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // btnUserQuery
            // 
            this.btnUserQuery.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnUserQuery.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnUserQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserQuery.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUserQuery.ForeColor = System.Drawing.Color.Black;
            this.btnUserQuery.IconChar = FontAwesome.Sharp.IconChar.UserTag;
            this.btnUserQuery.IconColor = System.Drawing.Color.DarkOrchid;
            this.btnUserQuery.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnUserQuery.IconSize = 32;
            this.btnUserQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserQuery.Location = new System.Drawing.Point(760, 145);
            this.btnUserQuery.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUserQuery.Name = "btnUserQuery";
            this.btnUserQuery.Padding = new System.Windows.Forms.Padding(9, 0, 18, 0);
            this.btnUserQuery.Size = new System.Drawing.Size(124, 40);
            this.btnUserQuery.TabIndex = 5;
            this.btnUserQuery.Text = "查询";
            this.btnUserQuery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUserQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUserQuery.UseVisualStyleBackColor = false;
            this.btnUserQuery.Click += new System.EventHandler(this.btnUserQuery_Click);
            // 
            // btnUserDel
            // 
            this.btnUserDel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnUserDel.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnUserDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserDel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUserDel.ForeColor = System.Drawing.Color.Black;
            this.btnUserDel.IconChar = FontAwesome.Sharp.IconChar.UserTimes;
            this.btnUserDel.IconColor = System.Drawing.Color.DarkOrchid;
            this.btnUserDel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnUserDel.IconSize = 32;
            this.btnUserDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserDel.Location = new System.Drawing.Point(587, 145);
            this.btnUserDel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUserDel.Name = "btnUserDel";
            this.btnUserDel.Padding = new System.Windows.Forms.Padding(9, 0, 18, 0);
            this.btnUserDel.Size = new System.Drawing.Size(124, 40);
            this.btnUserDel.TabIndex = 5;
            this.btnUserDel.Text = "删除";
            this.btnUserDel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUserDel.UseVisualStyleBackColor = false;
            this.btnUserDel.Click += new System.EventHandler(this.btnUserDel_Click);
            // 
            // btnUserLoad
            // 
            this.btnUserLoad.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnUserLoad.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnUserLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserLoad.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUserLoad.ForeColor = System.Drawing.Color.Black;
            this.btnUserLoad.IconChar = FontAwesome.Sharp.IconChar.UserFriends;
            this.btnUserLoad.IconColor = System.Drawing.Color.DarkOrchid;
            this.btnUserLoad.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnUserLoad.IconSize = 32;
            this.btnUserLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserLoad.Location = new System.Drawing.Point(68, 145);
            this.btnUserLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUserLoad.Name = "btnUserLoad";
            this.btnUserLoad.Padding = new System.Windows.Forms.Padding(9, 0, 18, 0);
            this.btnUserLoad.Size = new System.Drawing.Size(124, 40);
            this.btnUserLoad.TabIndex = 5;
            this.btnUserLoad.Text = "加载";
            this.btnUserLoad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUserLoad.UseVisualStyleBackColor = false;
            this.btnUserLoad.Click += new System.EventHandler(this.btnUserLoad_Click);
            // 
            // btnUserUpdate
            // 
            this.btnUserUpdate.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnUserUpdate.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnUserUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserUpdate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUserUpdate.ForeColor = System.Drawing.Color.Black;
            this.btnUserUpdate.IconChar = FontAwesome.Sharp.IconChar.UserEdit;
            this.btnUserUpdate.IconColor = System.Drawing.Color.DarkOrchid;
            this.btnUserUpdate.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnUserUpdate.IconSize = 32;
            this.btnUserUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserUpdate.Location = new System.Drawing.Point(241, 145);
            this.btnUserUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUserUpdate.Name = "btnUserUpdate";
            this.btnUserUpdate.Padding = new System.Windows.Forms.Padding(9, 0, 18, 0);
            this.btnUserUpdate.Size = new System.Drawing.Size(124, 40);
            this.btnUserUpdate.TabIndex = 5;
            this.btnUserUpdate.Text = "修改";
            this.btnUserUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUserUpdate.UseVisualStyleBackColor = false;
            this.btnUserUpdate.Click += new System.EventHandler(this.btnUserUpdate_Click);
            // 
            // btnUserAdd
            // 
            this.btnUserAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnUserAdd.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnUserAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserAdd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUserAdd.ForeColor = System.Drawing.Color.Black;
            this.btnUserAdd.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            this.btnUserAdd.IconColor = System.Drawing.Color.DarkOrchid;
            this.btnUserAdd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnUserAdd.IconSize = 32;
            this.btnUserAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserAdd.Location = new System.Drawing.Point(414, 145);
            this.btnUserAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUserAdd.Name = "btnUserAdd";
            this.btnUserAdd.Padding = new System.Windows.Forms.Padding(9, 0, 18, 0);
            this.btnUserAdd.Size = new System.Drawing.Size(124, 40);
            this.btnUserAdd.TabIndex = 5;
            this.btnUserAdd.Text = "添加";
            this.btnUserAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUserAdd.UseVisualStyleBackColor = false;
            this.btnUserAdd.Click += new System.EventHandler(this.btnUserAdd_Click);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(70, 30);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(100, 29);
            this.txtID.TabIndex = 4;
            this.txtID.Text = "0";
            // 
            // txtUserLevel
            // 
            this.txtUserLevel.Location = new System.Drawing.Point(512, 86);
            this.txtUserLevel.Name = "txtUserLevel";
            this.txtUserLevel.Size = new System.Drawing.Size(100, 29);
            this.txtUserLevel.TabIndex = 1;
            this.txtUserLevel.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(439, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "权限：";
            // 
            // txtUserPassword
            // 
            this.txtUserPassword.Location = new System.Drawing.Point(307, 86);
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.Size = new System.Drawing.Size(100, 29);
            this.txtUserPassword.TabIndex = 1;
            this.txtUserPassword.Text = "45";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "密码：";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(512, 30);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(100, 29);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.Text = "李维";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(439, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "姓名：";
            // 
            // txtUserEmployeeID
            // 
            this.txtUserEmployeeID.Location = new System.Drawing.Point(307, 30);
            this.txtUserEmployeeID.Name = "txtUserEmployeeID";
            this.txtUserEmployeeID.Size = new System.Drawing.Size(100, 29);
            this.txtUserEmployeeID.TabIndex = 1;
            this.txtUserEmployeeID.Text = "001";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "ID：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "员工编号：";
            // 
            // FrmUser
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1702, 978);
            this.Controls.Add(this.dgrdViewUser);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmUser";
            this.Text = "用户";
            ((System.ComponentModel.ISupportInitialize)(this.dgrdViewUser)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgrdViewUser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtUserLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserEmployeeID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btnUserAdd;
        private FontAwesome.Sharp.IconButton btnUserDel;
        private FontAwesome.Sharp.IconButton btnUserUpdate;
        private FontAwesome.Sharp.IconButton btnUserQuery;
        private FontAwesome.Sharp.IconButton btnUserLoad;
    }
}