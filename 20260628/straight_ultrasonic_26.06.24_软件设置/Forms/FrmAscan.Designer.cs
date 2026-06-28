namespace FlatUI_TestPlatform.Forms
{
    partial class FrmAscan
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.comboBoxReceive = new System.Windows.Forms.ComboBox();
            this.Ascan = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pictureBoxBscan = new System.Windows.Forms.PictureBox();
            this.comboBoxBScanTransmit = new System.Windows.Forms.ComboBox();
            this.comboBoxTransmit = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Ascan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBscan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Location = new System.Drawing.Point(1593, 581);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(627, 54);
            this.button5.TabIndex = 109;
            this.button5.Text = "B扫成像";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(1593, 254);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(627, 54);
            this.button4.TabIndex = 108;
            this.button4.Text = "A扫成像";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // comboBoxReceive
            // 
            this.comboBoxReceive.FormattingEnabled = true;
            this.comboBoxReceive.Location = new System.Drawing.Point(2259, 409);
            this.comboBoxReceive.Name = "comboBoxReceive";
            this.comboBoxReceive.Size = new System.Drawing.Size(115, 26);
            this.comboBoxReceive.TabIndex = 105;
            // 
            // Ascan
            // 
            chartArea1.AxisX.Crossing = 0D;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.ScaleView.SmallScrollMinSize = 0D;
            chartArea1.AxisX.ScrollBar.Enabled = false;
            chartArea1.AxisY.Crossing = 0D;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.ScrollBar.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.Ascan.ChartAreas.Add(chartArea1);
            this.Ascan.Location = new System.Drawing.Point(1593, 308);
            this.Ascan.Margin = new System.Windows.Forms.Padding(4);
            this.Ascan.Name = "Ascan";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Name = "Series1";
            this.Ascan.Series.Add(series1);
            this.Ascan.Size = new System.Drawing.Size(628, 265);
            this.Ascan.TabIndex = 107;
            this.Ascan.Text = "chart1";
            // 
            // pictureBoxBscan
            // 
            this.pictureBoxBscan.BackColor = System.Drawing.Color.White;
            this.pictureBoxBscan.Cursor = System.Windows.Forms.Cursors.No;
            this.pictureBoxBscan.Location = new System.Drawing.Point(1593, 635);
            this.pictureBoxBscan.Name = "pictureBoxBscan";
            this.pictureBoxBscan.Size = new System.Drawing.Size(628, 325);
            this.pictureBoxBscan.TabIndex = 106;
            this.pictureBoxBscan.TabStop = false;
            // 
            // comboBoxBScanTransmit
            // 
            this.comboBoxBScanTransmit.FormattingEnabled = true;
            this.comboBoxBScanTransmit.Location = new System.Drawing.Point(2259, 757);
            this.comboBoxBScanTransmit.Name = "comboBoxBScanTransmit";
            this.comboBoxBScanTransmit.Size = new System.Drawing.Size(115, 26);
            this.comboBoxBScanTransmit.TabIndex = 104;
            // 
            // comboBoxTransmit
            // 
            this.comboBoxTransmit.FormattingEnabled = true;
            this.comboBoxTransmit.Location = new System.Drawing.Point(2259, 325);
            this.comboBoxTransmit.Name = "comboBoxTransmit";
            this.comboBoxTransmit.Size = new System.Drawing.Size(115, 26);
            this.comboBoxTransmit.TabIndex = 103;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(315, 102);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(627, 54);
            this.button1.TabIndex = 111;
            this.button1.Text = "TFM成像";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.No;
            this.pictureBox1.Location = new System.Drawing.Point(27, 188);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1218, 830);
            this.pictureBox1.TabIndex = 110;
            this.pictureBox1.TabStop = false;
            // 
            // FrmAscan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2476, 1189);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBoxReceive);
            this.Controls.Add(this.Ascan);
            this.Controls.Add(this.pictureBoxBscan);
            this.Controls.Add(this.comboBoxBScanTransmit);
            this.Controls.Add(this.comboBoxTransmit);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmAscan";
            this.Text = "Form1";
            //this.Load += new System.EventHandler(this.FrmShow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Ascan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBscan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox comboBoxReceive;
        private System.Windows.Forms.DataVisualization.Charting.Chart Ascan;
        private System.Windows.Forms.PictureBox pictureBoxBscan;
        private System.Windows.Forms.ComboBox comboBoxBScanTransmit;
        private System.Windows.Forms.ComboBox comboBoxTransmit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}