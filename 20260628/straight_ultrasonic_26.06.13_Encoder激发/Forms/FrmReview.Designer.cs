namespace FlatUI_TestPlatform.Forms
{
    partial class FrmReview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReview));
            this.btnReviewQuery = new FontAwesome.Sharp.IconButton();
            this.btnReviewDel = new FontAwesome.Sharp.IconButton();
            this.btnReviewLoad = new FontAwesome.Sharp.IconButton();
            this.btnReviewUpdate = new FontAwesome.Sharp.IconButton();
            this.btnReviewAdd = new FontAwesome.Sharp.IconButton();
            this.txtReviewID = new System.Windows.Forms.TextBox();
            this.txtReview3 = new System.Windows.Forms.TextBox();
            this.txtReview2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReview1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReviewModel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReviewName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgrdViewReview = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReviewToWord = new FontAwesome.Sharp.IconButton();
            this.btnReviewToExcel = new FontAwesome.Sharp.IconButton();
            this.txtReview5 = new System.Windows.Forms.TextBox();
            this.txtReview4 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtExcelTitle = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnReviewToCurve = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.axTChart1 = new AxTeeChart.AxTChart();
            this.panel2 = new System.Windows.Forms.Panel();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdViewReview)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTChart1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReviewQuery
            // 
            this.btnReviewQuery.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnReviewQuery.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnReviewQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReviewQuery.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReviewQuery.ForeColor = System.Drawing.Color.Black;
            this.btnReviewQuery.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnReviewQuery.IconColor = System.Drawing.Color.DarkOrchid;
            this.btnReviewQuery.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReviewQuery.IconSize = 32;
            this.btnReviewQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReviewQuery.Location = new System.Drawing.Point(728, 145);
            this.btnReviewQuery.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReviewQuery.Name = "btnReviewQuery";
            this.btnReviewQuery.Padding = new System.Windows.Forms.Padding(9, 0, 18, 0);
            this.btnReviewQuery.Size = new System.Drawing.Size(124, 40);
            this.btnReviewQuery.TabIndex = 5;
            this.btnReviewQuery.Text = "查询";
            this.btnReviewQuery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReviewQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReviewQuery.UseVisualStyleBackColor = false;
            this.btnReviewQuery.Click += new System.EventHandler(this.btnReviewQuery_Click);
            // 
            // btnReviewDel
            // 
            this.btnReviewDel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnReviewDel.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnReviewDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReviewDel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReviewDel.ForeColor = System.Drawing.Color.Black;
            this.btnReviewDel.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            this.btnReviewDel.IconColor = System.Drawing.Color.DarkOrchid;
            this.btnReviewDel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReviewDel.IconSize = 32;
            this.btnReviewDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReviewDel.Location = new System.Drawing.Point(563, 145);
            this.btnReviewDel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReviewDel.Name = "btnReviewDel";
            this.btnReviewDel.Padding = new System.Windows.Forms.Padding(9, 0, 18, 0);
            this.btnReviewDel.Size = new System.Drawing.Size(124, 40);
            this.btnReviewDel.TabIndex = 5;
            this.btnReviewDel.Text = "删除";
            this.btnReviewDel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReviewDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReviewDel.UseVisualStyleBackColor = false;
            this.btnReviewDel.Click += new System.EventHandler(this.btnReviewDel_Click);
            // 
            // btnReviewLoad
            // 
            this.btnReviewLoad.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnReviewLoad.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnReviewLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReviewLoad.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReviewLoad.ForeColor = System.Drawing.Color.Black;
            this.btnReviewLoad.IconChar = FontAwesome.Sharp.IconChar.ListUl;
            this.btnReviewLoad.IconColor = System.Drawing.Color.DarkOrchid;
            this.btnReviewLoad.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReviewLoad.IconSize = 32;
            this.btnReviewLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReviewLoad.Location = new System.Drawing.Point(68, 145);
            this.btnReviewLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReviewLoad.Name = "btnReviewLoad";
            this.btnReviewLoad.Padding = new System.Windows.Forms.Padding(9, 0, 18, 0);
            this.btnReviewLoad.Size = new System.Drawing.Size(124, 40);
            this.btnReviewLoad.TabIndex = 5;
            this.btnReviewLoad.Text = "加载";
            this.btnReviewLoad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReviewLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReviewLoad.UseVisualStyleBackColor = false;
            this.btnReviewLoad.Click += new System.EventHandler(this.btnReviewLoad_Click);
            // 
            // btnReviewUpdate
            // 
            this.btnReviewUpdate.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnReviewUpdate.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnReviewUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReviewUpdate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReviewUpdate.ForeColor = System.Drawing.Color.Black;
            this.btnReviewUpdate.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            this.btnReviewUpdate.IconColor = System.Drawing.Color.DarkOrchid;
            this.btnReviewUpdate.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReviewUpdate.IconSize = 32;
            this.btnReviewUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReviewUpdate.Location = new System.Drawing.Point(233, 145);
            this.btnReviewUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReviewUpdate.Name = "btnReviewUpdate";
            this.btnReviewUpdate.Padding = new System.Windows.Forms.Padding(9, 0, 18, 0);
            this.btnReviewUpdate.Size = new System.Drawing.Size(124, 40);
            this.btnReviewUpdate.TabIndex = 5;
            this.btnReviewUpdate.Text = "修改";
            this.btnReviewUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReviewUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReviewUpdate.UseVisualStyleBackColor = false;
            this.btnReviewUpdate.Click += new System.EventHandler(this.btnReviewUpdate_Click);
            // 
            // btnReviewAdd
            // 
            this.btnReviewAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnReviewAdd.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnReviewAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReviewAdd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReviewAdd.ForeColor = System.Drawing.Color.Black;
            this.btnReviewAdd.IconChar = FontAwesome.Sharp.IconChar.PlusSquare;
            this.btnReviewAdd.IconColor = System.Drawing.Color.DarkOrchid;
            this.btnReviewAdd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReviewAdd.IconSize = 32;
            this.btnReviewAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReviewAdd.Location = new System.Drawing.Point(398, 145);
            this.btnReviewAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReviewAdd.Name = "btnReviewAdd";
            this.btnReviewAdd.Padding = new System.Windows.Forms.Padding(9, 0, 18, 0);
            this.btnReviewAdd.Size = new System.Drawing.Size(124, 40);
            this.btnReviewAdd.TabIndex = 5;
            this.btnReviewAdd.Text = "添加";
            this.btnReviewAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReviewAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReviewAdd.UseVisualStyleBackColor = false;
            this.btnReviewAdd.Click += new System.EventHandler(this.btnReviewAdd_Click);
            // 
            // txtReviewID
            // 
            this.txtReviewID.Location = new System.Drawing.Point(70, 30);
            this.txtReviewID.Name = "txtReviewID";
            this.txtReviewID.ReadOnly = true;
            this.txtReviewID.Size = new System.Drawing.Size(100, 34);
            this.txtReviewID.TabIndex = 4;
            this.txtReviewID.Text = "0";
            // 
            // txtReview3
            // 
            this.txtReview3.Location = new System.Drawing.Point(307, 76);
            this.txtReview3.Name = "txtReview3";
            this.txtReview3.Size = new System.Drawing.Size(58, 34);
            this.txtReview3.TabIndex = 1;
            this.txtReview3.Text = "1";
            // 
            // txtReview2
            // 
            this.txtReview2.Location = new System.Drawing.Point(1303, 31);
            this.txtReview2.Name = "txtReview2";
            this.txtReview2.Size = new System.Drawing.Size(70, 34);
            this.txtReview2.TabIndex = 1;
            this.txtReview2.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(190, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 27);
            this.label6.TabIndex = 0;
            this.label6.Text = "结果值：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1189, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 27);
            this.label4.TabIndex = 0;
            this.label4.Text = "操作者：";
            // 
            // txtReview1
            // 
            this.txtReview1.Location = new System.Drawing.Point(1057, 31);
            this.txtReview1.Name = "txtReview1";
            this.txtReview1.Size = new System.Drawing.Size(58, 34);
            this.txtReview1.TabIndex = 1;
            this.txtReview1.Text = "45";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(948, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 27);
            this.label3.TabIndex = 0;
            this.label3.Text = "流水号：";
            // 
            // txtReviewModel
            // 
            this.txtReviewModel.Location = new System.Drawing.Point(553, 30);
            this.txtReviewModel.Name = "txtReviewModel";
            this.txtReviewModel.Size = new System.Drawing.Size(309, 34);
            this.txtReviewModel.TabIndex = 1;
            this.txtReviewModel.Text = "SCI-106-89";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(479, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "机型：";
            // 
            // txtReviewName
            // 
            this.txtReviewName.Location = new System.Drawing.Point(307, 30);
            this.txtReviewName.Name = "txtReviewName";
            this.txtReviewName.Size = new System.Drawing.Size(100, 34);
            this.txtReviewName.TabIndex = 1;
            this.txtReviewName.Text = "001";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 27);
            this.label5.TabIndex = 0;
            this.label5.Text = "ID：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "产品名称：";
            // 
            // dgrdViewReview
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrdViewReview.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgrdViewReview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgrdViewReview.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgrdViewReview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgrdViewReview.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrdViewReview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdViewReview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrdViewReview.Location = new System.Drawing.Point(0, 0);
            this.dgrdViewReview.Name = "dgrdViewReview";
            this.dgrdViewReview.ReadOnly = true;
            this.dgrdViewReview.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrdViewReview.RowHeadersWidth = 51;
            this.dgrdViewReview.RowTemplate.Height = 27;
            this.dgrdViewReview.Size = new System.Drawing.Size(1411, 131);
            this.dgrdViewReview.TabIndex = 5;
            this.dgrdViewReview.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdViewReview_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.btnReviewToCurve);
            this.groupBox1.Controls.Add(this.btnReviewToWord);
            this.groupBox1.Controls.Add(this.btnReviewToExcel);
            this.groupBox1.Controls.Add(this.btnReviewQuery);
            this.groupBox1.Controls.Add(this.btnReviewDel);
            this.groupBox1.Controls.Add(this.iconButton1);
            this.groupBox1.Controls.Add(this.btnReviewLoad);
            this.groupBox1.Controls.Add(this.btnReviewUpdate);
            this.groupBox1.Controls.Add(this.btnReviewAdd);
            this.groupBox1.Controls.Add(this.txtReviewID);
            this.groupBox1.Controls.Add(this.txtReview5);
            this.groupBox1.Controls.Add(this.txtReview3);
            this.groupBox1.Controls.Add(this.txtReview4);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtReview2);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtReview1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtExcelTitle);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtReviewModel);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtReviewName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1411, 200);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // btnReviewToWord
            // 
            this.btnReviewToWord.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnReviewToWord.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnReviewToWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReviewToWord.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReviewToWord.ForeColor = System.Drawing.Color.Black;
            this.btnReviewToWord.IconChar = FontAwesome.Sharp.IconChar.FileWord;
            this.btnReviewToWord.IconColor = System.Drawing.Color.DarkOrchid;
            this.btnReviewToWord.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReviewToWord.IconSize = 32;
            this.btnReviewToWord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReviewToWord.Location = new System.Drawing.Point(1097, 145);
            this.btnReviewToWord.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReviewToWord.Name = "btnReviewToWord";
            this.btnReviewToWord.Padding = new System.Windows.Forms.Padding(9, 0, 18, 0);
            this.btnReviewToWord.Size = new System.Drawing.Size(155, 40);
            this.btnReviewToWord.TabIndex = 30;
            this.btnReviewToWord.Text = "导出Word";
            this.btnReviewToWord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReviewToWord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReviewToWord.UseVisualStyleBackColor = false;
            this.btnReviewToWord.Click += new System.EventHandler(this.btnReviewToWord_Click);
            // 
            // btnReviewToExcel
            // 
            this.btnReviewToExcel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnReviewToExcel.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnReviewToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReviewToExcel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReviewToExcel.ForeColor = System.Drawing.Color.Black;
            this.btnReviewToExcel.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.btnReviewToExcel.IconColor = System.Drawing.Color.DarkOrchid;
            this.btnReviewToExcel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReviewToExcel.IconSize = 32;
            this.btnReviewToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReviewToExcel.Location = new System.Drawing.Point(893, 145);
            this.btnReviewToExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReviewToExcel.Name = "btnReviewToExcel";
            this.btnReviewToExcel.Padding = new System.Windows.Forms.Padding(9, 0, 18, 0);
            this.btnReviewToExcel.Size = new System.Drawing.Size(163, 40);
            this.btnReviewToExcel.TabIndex = 29;
            this.btnReviewToExcel.Text = "导出EXCEL";
            this.btnReviewToExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReviewToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReviewToExcel.UseVisualStyleBackColor = false;
            this.btnReviewToExcel.Click += new System.EventHandler(this.btnReviewToExcel_Click);
            // 
            // txtReview5
            // 
            this.txtReview5.Location = new System.Drawing.Point(804, 75);
            this.txtReview5.Name = "txtReview5";
            this.txtReview5.Size = new System.Drawing.Size(58, 34);
            this.txtReview5.TabIndex = 1;
            this.txtReview5.Text = "1";
            // 
            // txtReview4
            // 
            this.txtReview4.Location = new System.Drawing.Point(555, 75);
            this.txtReview4.Name = "txtReview4";
            this.txtReview4.Size = new System.Drawing.Size(70, 34);
            this.txtReview4.TabIndex = 1;
            this.txtReview4.Text = "1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(687, 79);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(112, 27);
            this.label13.TabIndex = 0;
            this.label13.Text = "日期时间：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(441, 79);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(132, 27);
            this.label12.TabIndex = 0;
            this.label12.Text = "填隙片规格：";
            // 
            // txtExcelTitle
            // 
            this.txtExcelTitle.Location = new System.Drawing.Point(1147, 77);
            this.txtExcelTitle.Name = "txtExcelTitle";
            this.txtExcelTitle.Size = new System.Drawing.Size(226, 34);
            this.txtExcelTitle.TabIndex = 1;
            this.txtExcelTitle.Text = "SCI-106-89";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(948, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 27);
            this.label7.TabIndex = 0;
            this.label7.Text = "WORD标题：";
            // 
            // btnReviewToCurve
            // 
            this.btnReviewToCurve.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnReviewToCurve.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnReviewToCurve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReviewToCurve.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReviewToCurve.ForeColor = System.Drawing.Color.Black;
            this.btnReviewToCurve.IconChar = FontAwesome.Sharp.IconChar.FileWord;
            this.btnReviewToCurve.IconColor = System.Drawing.Color.DarkOrchid;
            this.btnReviewToCurve.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReviewToCurve.IconSize = 32;
            this.btnReviewToCurve.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReviewToCurve.Location = new System.Drawing.Point(1269, 145);
            this.btnReviewToCurve.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReviewToCurve.Name = "btnReviewToCurve";
            this.btnReviewToCurve.Padding = new System.Windows.Forms.Padding(9, 0, 18, 0);
            this.btnReviewToCurve.Size = new System.Drawing.Size(130, 40);
            this.btnReviewToCurve.TabIndex = 30;
            this.btnReviewToCurve.Text = "曲线";
            this.btnReviewToCurve.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReviewToCurve.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReviewToCurve.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1411, 406);
            this.panel1.TabIndex = 6;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgrdViewReview);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.axTChart1);
            this.splitContainer1.Size = new System.Drawing.Size(1411, 406);
            this.splitContainer1.SplitterDistance = 131;
            this.splitContainer1.TabIndex = 6;
            // 
            // axTChart1
            // 
            this.axTChart1.Dock = System.Windows.Forms.DockStyle.Left;
            this.axTChart1.Enabled = true;
            this.axTChart1.Location = new System.Drawing.Point(0, 0);
            this.axTChart1.Margin = new System.Windows.Forms.Padding(1);
            this.axTChart1.Name = "axTChart1";
            this.axTChart1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTChart1.OcxState")));
            this.axTChart1.Size = new System.Drawing.Size(494, 271);
            this.axTChart1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.iconButton2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(494, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(917, 271);
            this.panel2.TabIndex = 3;
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.iconButton1.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iconButton1.ForeColor = System.Drawing.Color.Black;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.ListUl;
            this.iconButton1.IconColor = System.Drawing.Color.DarkOrchid;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.IconSize = 32;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.Location = new System.Drawing.Point(68, 145);
            this.iconButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Padding = new System.Windows.Forms.Padding(9, 0, 18, 0);
            this.iconButton1.Size = new System.Drawing.Size(124, 40);
            this.iconButton1.TabIndex = 5;
            this.iconButton1.Text = "加载";
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Click += new System.EventHandler(this.btnReviewLoad_Click);
            // 
            // iconButton2
            // 
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconButton2.IconColor = System.Drawing.Color.Black;
            this.iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton2.Location = new System.Drawing.Point(27, 38);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Size = new System.Drawing.Size(125, 44);
            this.iconButton2.TabIndex = 0;
            this.iconButton2.Text = "iconButton2";
            this.iconButton2.UseVisualStyleBackColor = true;
            // 
            // FrmReview
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1411, 606);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmReview";
            this.Text = "浏览";
            ((System.ComponentModel.ISupportInitialize)(this.dgrdViewReview)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTChart1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private FontAwesome.Sharp.IconButton btnReviewQuery;
        private FontAwesome.Sharp.IconButton btnReviewDel;
        private FontAwesome.Sharp.IconButton btnReviewLoad;
        private FontAwesome.Sharp.IconButton btnReviewUpdate;
        private FontAwesome.Sharp.IconButton btnReviewAdd;
        private System.Windows.Forms.TextBox txtReviewID;
        private System.Windows.Forms.TextBox txtReview3;
        private System.Windows.Forms.TextBox txtReview2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtReview1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReviewModel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReviewName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgrdViewReview;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtReview5;
        private System.Windows.Forms.TextBox txtReview4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtExcelTitle;
        private System.Windows.Forms.Label label7;
        private FontAwesome.Sharp.IconButton btnReviewToWord;
        private FontAwesome.Sharp.IconButton btnReviewToExcel;
        private FontAwesome.Sharp.IconButton btnReviewToCurve;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton iconButton2;
        private AxTeeChart.AxTChart axTChart1;
    }
}