namespace FlatUI_TestPlatform.Forms.FrmTestUserControl
{
    partial class UCameraBoard
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.hslButton3 = new HslControls.HslButton();
            this.hslButton5 = new HslControls.HslButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.hslButton3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.hslButton5, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(240, 240);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // hslButton3
            // 
            this.hslButton3.CornerRadius = 3;
            this.hslButton3.CustomerInformation = null;
            this.hslButton3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hslButton3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hslButton3.ForeColor = System.Drawing.Color.Black;
            this.hslButton3.Location = new System.Drawing.Point(3, 213);
            this.hslButton3.Name = "hslButton3";
            this.hslButton3.Size = new System.Drawing.Size(114, 24);
            this.hslButton3.TabIndex = 39;
            this.hslButton3.Text = "拍照";
            // 
            // hslButton5
            // 
            this.hslButton5.CornerRadius = 3;
            this.hslButton5.CustomerInformation = null;
            this.hslButton5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hslButton5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hslButton5.ForeColor = System.Drawing.Color.Black;
            this.hslButton5.Location = new System.Drawing.Point(123, 213);
            this.hslButton5.Name = "hslButton5";
            this.hslButton5.Size = new System.Drawing.Size(114, 24);
            this.hslButton5.TabIndex = 38;
            this.hslButton5.Text = "保存";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(234, 204);
            this.panel1.TabIndex = 0;
            // 
            // FrmCameraBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmCameraBoard";
            this.Size = new System.Drawing.Size(240, 240);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private HslControls.HslButton hslButton3;
        private HslControls.HslButton hslButton5;
    }
}
