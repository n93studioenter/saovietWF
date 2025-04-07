namespace SaovietWF
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbselect = new System.Windows.Forms.ComboBox();
            this.btnLoadFromDisk = new System.Windows.Forms.Button();
            this.btnDownloadFromCQT = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.cbbTo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbFrom = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCapnhat = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnKHCapnhat = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKHPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKHUsername = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtGhichu = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtTKThue = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTkCo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTKNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNoidung = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.grvDinhdanh = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvDinhdanh)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đường dẫn";
            // 
            // cbbselect
            // 
            this.cbbselect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbselect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbselect.FormattingEnabled = true;
            this.cbbselect.Location = new System.Drawing.Point(16, 105);
            this.cbbselect.Name = "cbbselect";
            this.cbbselect.Size = new System.Drawing.Size(342, 28);
            this.cbbselect.TabIndex = 11;
            this.cbbselect.SelectedIndexChanged += new System.EventHandler(this.cbbselect_SelectedIndexChanged);
            // 
            // btnLoadFromDisk
            // 
            this.btnLoadFromDisk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadFromDisk.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLoadFromDisk.BackColor = System.Drawing.Color.White;
            this.btnLoadFromDisk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadFromDisk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadFromDisk.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadFromDisk.Image")));
            this.btnLoadFromDisk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadFromDisk.Location = new System.Drawing.Point(16, 141);
            this.btnLoadFromDisk.Name = "btnLoadFromDisk";
            this.btnLoadFromDisk.Size = new System.Drawing.Size(342, 56);
            this.btnLoadFromDisk.TabIndex = 10;
            this.btnLoadFromDisk.Text = "Tải từ máy tính";
            this.btnLoadFromDisk.UseVisualStyleBackColor = false;
            this.btnLoadFromDisk.Click += new System.EventHandler(this.btnLoadFromDisk_Click);
            // 
            // btnDownloadFromCQT
            // 
            this.btnDownloadFromCQT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownloadFromCQT.BackColor = System.Drawing.Color.White;
            this.btnDownloadFromCQT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadFromCQT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadFromCQT.Image = ((System.Drawing.Image)(resources.GetObject("btnDownloadFromCQT.Image")));
            this.btnDownloadFromCQT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDownloadFromCQT.Location = new System.Drawing.Point(16, 37);
            this.btnDownloadFromCQT.Name = "btnDownloadFromCQT";
            this.btnDownloadFromCQT.Size = new System.Drawing.Size(342, 57);
            this.btnDownloadFromCQT.TabIndex = 9;
            this.btnDownloadFromCQT.Text = "Tải dữ liệu từ cơ quan thuế";
            this.btnDownloadFromCQT.UseVisualStyleBackColor = false;
            this.btnDownloadFromCQT.Click += new System.EventHandler(this.btnDownloadFromCQT_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImport.Location = new System.Drawing.Point(1073, 163);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(198, 38);
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnLoc
            // 
            this.btnLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoc.Image = ((System.Drawing.Image)(resources.GetObject("btnLoc.Image")));
            this.btnLoc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoc.Location = new System.Drawing.Point(1073, 104);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(198, 40);
            this.btnLoc.TabIndex = 7;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // cbbTo
            // 
            this.cbbTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbbTo.FormattingEnabled = true;
            this.cbbTo.Location = new System.Drawing.Point(709, 112);
            this.cbbTo.Name = "cbbTo";
            this.cbbTo.Size = new System.Drawing.Size(343, 28);
            this.cbbTo.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(605, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Đến tháng";
            // 
            // cbbFrom
            // 
            this.cbbFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFrom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbbFrom.FormattingEnabled = true;
            this.cbbFrom.ItemHeight = 20;
            this.cbbFrom.Location = new System.Drawing.Point(153, 109);
            this.cbbFrom.Name = "cbbFrom";
            this.cbbFrom.Size = new System.Drawing.Size(414, 28);
            this.cbbFrom.TabIndex = 4;
            this.cbbFrom.SelectedIndexChanged += new System.EventHandler(this.cbbFrom_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Từ tháng";
            // 
            // btnCapnhat
            // 
            this.btnCapnhat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapnhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapnhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapnhat.Image = ((System.Drawing.Image)(resources.GetObject("btnCapnhat.Image")));
            this.btnCapnhat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCapnhat.Location = new System.Drawing.Point(1073, 44);
            this.btnCapnhat.Name = "btnCapnhat";
            this.btnCapnhat.Size = new System.Drawing.Size(198, 34);
            this.btnCapnhat.TabIndex = 2;
            this.btnCapnhat.Text = "Cập nhật";
            this.btnCapnhat.UseVisualStyleBackColor = true;
            this.btnCapnhat.Click += new System.EventHandler(this.btnCapnhat_Click);
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(152, 49);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(900, 26);
            this.txtPath.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("VNI-Times", 7.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 235);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("VNI-Times", 7.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("VK Sans Display DemiBold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1678, 383);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1718, 668);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1710, 635);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Import Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnImport);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnLoc);
            this.groupBox2.Controls.Add(this.txtPath);
            this.groupBox2.Controls.Add(this.cbbTo);
            this.groupBox2.Controls.Add(this.btnCapnhat);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbbFrom);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(20, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1288, 212);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lọc dữ liệu";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbbselect);
            this.groupBox1.Controls.Add(this.btnDownloadFromCQT);
            this.groupBox1.Controls.Add(this.btnLoadFromDisk);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(1324, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 208);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tải dữ liệu";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1712, 637);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Thông tin tài khoản";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.btnKHCapnhat);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtKHPassword);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtKHUsername);
            this.panel2.Location = new System.Drawing.Point(6, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(525, 625);
            this.panel2.TabIndex = 0;
            // 
            // btnKHCapnhat
            // 
            this.btnKHCapnhat.BackColor = System.Drawing.Color.DimGray;
            this.btnKHCapnhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKHCapnhat.ForeColor = System.Drawing.Color.White;
            this.btnKHCapnhat.Location = new System.Drawing.Point(107, 125);
            this.btnKHCapnhat.Name = "btnKHCapnhat";
            this.btnKHCapnhat.Size = new System.Drawing.Size(394, 42);
            this.btnKHCapnhat.TabIndex = 4;
            this.btnKHCapnhat.Text = "Cập nhật";
            this.btnKHCapnhat.UseVisualStyleBackColor = false;
            this.btnKHCapnhat.Click += new System.EventHandler(this.btnKHCapnhat_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(23, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "Mật khẩu";
            // 
            // txtKHPassword
            // 
            this.txtKHPassword.Location = new System.Drawing.Point(107, 78);
            this.txtKHPassword.Name = "txtKHPassword";
            this.txtKHPassword.Size = new System.Drawing.Size(394, 26);
            this.txtKHPassword.TabIndex = 2;
            this.txtKHPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(23, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Tài khoản";
            // 
            // txtKHUsername
            // 
            this.txtKHUsername.Location = new System.Drawing.Point(107, 28);
            this.txtKHUsername.Name = "txtKHUsername";
            this.txtKHUsername.Size = new System.Drawing.Size(394, 26);
            this.txtKHUsername.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Controls.Add(this.grvDinhdanh);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1712, 637);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Mật định tài khoản cho từ khóa thường gặp.";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.txtGhichu);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.btnThem);
            this.panel3.Controls.Add(this.txtTKThue);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.txtTkCo);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtTKNo);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.txtNoidung);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(15, 17);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1533, 71);
            this.panel3.TabIndex = 1;
            // 
            // txtGhichu
            // 
            this.txtGhichu.Location = new System.Drawing.Point(522, 25);
            this.txtGhichu.Name = "txtGhichu";
            this.txtGhichu.Size = new System.Drawing.Size(255, 26);
            this.txtGhichu.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(444, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Nội dung";
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(1423, 19);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(98, 39);
            this.btnThem.TabIndex = 10;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtTKThue
            // 
            this.txtTKThue.Location = new System.Drawing.Point(1278, 25);
            this.txtTKThue.Name = "txtTKThue";
            this.txtTKThue.Size = new System.Drawing.Size(116, 26);
            this.txtTKThue.TabIndex = 9;
            this.txtTKThue.Text = "1331";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1208, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "TK thuế";
            // 
            // txtTkCo
            // 
            this.txtTkCo.Location = new System.Drawing.Point(1049, 25);
            this.txtTkCo.Name = "txtTkCo";
            this.txtTkCo.Size = new System.Drawing.Size(116, 26);
            this.txtTkCo.TabIndex = 7;
            this.txtTkCo.Text = "1111";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(993, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 20);
            this.label9.TabIndex = 6;
            this.label9.Text = "TK có";
            // 
            // txtTKNo
            // 
            this.txtTKNo.Location = new System.Drawing.Point(839, 24);
            this.txtTKNo.Name = "txtTKNo";
            this.txtTKNo.Size = new System.Drawing.Size(116, 26);
            this.txtTKNo.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(783, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "TK nợ";
            // 
            // txtNoidung
            // 
            this.txtNoidung.Location = new System.Drawing.Point(114, 25);
            this.txtNoidung.Name = "txtNoidung";
            this.txtNoidung.Size = new System.Drawing.Size(289, 26);
            this.txtNoidung.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Nội dung";
            // 
            // grvDinhdanh
            // 
            this.grvDinhdanh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grvDinhdanh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvDinhdanh.Location = new System.Drawing.Point(15, 94);
            this.grvDinhdanh.Name = "grvDinhdanh";
            this.grvDinhdanh.RowHeadersWidth = 62;
            this.grvDinhdanh.RowTemplate.Height = 28;
            this.grvDinhdanh.Size = new System.Drawing.Size(1533, 542);
            this.grvDinhdanh.TabIndex = 0;
            this.grvDinhdanh.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvDinhdanh_CellContentClick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1742, 692);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvDinhdanh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.ComboBox cbbTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCapnhat;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnDownloadFromCQT;
        private System.Windows.Forms.Button btnLoadFromDisk;
        private System.Windows.Forms.ComboBox cbbselect;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtKHPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtKHUsername;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnKHCapnhat;
        private System.Windows.Forms.DataGridView grvDinhdanh;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTKNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNoidung;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TextBox txtTKThue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTkCo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtGhichu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

