namespace FlatUI_TestPlatform.Forms.FrmTestUserControl
{
    partial class UCurveBoard
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCurveBoard));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.axTChart4 = new AxTeeChart.AxTChart();
            this.axTChart3 = new AxTeeChart.AxTChart();
            this.axTChart2 = new AxTeeChart.AxTChart();
            this.axTChart1 = new AxTeeChart.AxTChart();
            this.timerCurveBoard = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTChart4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTChart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTChart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.axTChart4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.axTChart3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.axTChart2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.axTChart1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1028, 678);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // axTChart4
            // 
            this.axTChart4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTChart4.Enabled = true;
            this.axTChart4.Location = new System.Drawing.Point(515, 1);
            this.axTChart4.Margin = new System.Windows.Forms.Padding(1);
            this.axTChart4.Name = "axTChart4";
            this.axTChart4.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTChart4.OcxState")));
            this.axTChart4.Size = new System.Drawing.Size(512, 337);
            this.axTChart4.TabIndex = 4;
            // 
            // axTChart3
            // 
            this.axTChart3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTChart3.Enabled = true;
            this.axTChart3.Location = new System.Drawing.Point(1, 340);
            this.axTChart3.Margin = new System.Windows.Forms.Padding(1);
            this.axTChart3.Name = "axTChart3";
            this.axTChart3.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTChart3.OcxState")));
            this.axTChart3.Size = new System.Drawing.Size(512, 337);
            this.axTChart3.TabIndex = 3;
            // 
            // axTChart2
            // 
            this.axTChart2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTChart2.Enabled = true;
            this.axTChart2.Location = new System.Drawing.Point(515, 340);
            this.axTChart2.Margin = new System.Windows.Forms.Padding(1);
            this.axTChart2.Name = "axTChart2";
            this.axTChart2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTChart2.OcxState")));
            this.axTChart2.Size = new System.Drawing.Size(512, 337);
            this.axTChart2.TabIndex = 2;
            // 
            // axTChart1
            // 
            this.axTChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTChart1.Enabled = true;
            this.axTChart1.Location = new System.Drawing.Point(1, 1);
            this.axTChart1.Margin = new System.Windows.Forms.Padding(1);
            this.axTChart1.Name = "axTChart1";
            this.axTChart1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTChart1.OcxState")));
            this.axTChart1.Size = new System.Drawing.Size(512, 337);
            this.axTChart1.TabIndex = 1;
            // 
            // timerCurveBoard
            // 
            this.timerCurveBoard.Tick += new System.EventHandler(this.timerCurveBoard_Tick);
            // 
            // UCurveBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCurveBoard";
            this.Size = new System.Drawing.Size(1028, 678);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTChart4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTChart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTChart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTChart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private AxTeeChart.AxTChart axTChart4;
        private AxTeeChart.AxTChart axTChart3;
        private AxTeeChart.AxTChart axTChart2;
        private AxTeeChart.AxTChart axTChart1;
        private System.Windows.Forms.Timer timerCurveBoard;
    }
}
