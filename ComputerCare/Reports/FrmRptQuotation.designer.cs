namespace ComputerCare.Reports
{
    partial class FrmRptQuotation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRptQuotation));
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.dateto = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbCust = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnShowSingle = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.datefrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblid = new System.Windows.Forms.Label();
            this.cmbQuotation = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShowHistory = new System.Windows.Forms.Button();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.DisplayGroupTree = false;
            this.crystalReportViewer1.ForeColor = System.Drawing.Color.Maroon;
            this.crystalReportViewer1.Location = new System.Drawing.Point(10, 216);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.SelectionFormula = "";
            this.crystalReportViewer1.Size = new System.Drawing.Size(990, 436);
            this.crystalReportViewer1.TabIndex = 2;
            this.crystalReportViewer1.ViewTimeSelectionFormula = "";
            // 
            // dateto
            // 
            this.dateto.CustomFormat = "dd-MM-yyyy";
            this.dateto.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateto.Location = new System.Drawing.Point(253, 86);
            this.dateto.Name = "dateto";
            this.dateto.Size = new System.Drawing.Size(129, 23);
            this.dateto.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbCust);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnShowSingle);
            this.groupBox2.Controls.Add(this.btnShow);
            this.groupBox2.Controls.Add(this.dateto);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.datefrom);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox2.Location = new System.Drawing.Point(398, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(602, 158);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // cmbCust
            // 
            this.cmbCust.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCust.FormattingEnabled = true;
            this.cmbCust.Location = new System.Drawing.Point(149, 36);
            this.cmbCust.Name = "cmbCust";
            this.cmbCust.Size = new System.Drawing.Size(200, 23);
            this.cmbCust.TabIndex = 0;
            this.cmbCust.Leave += new System.EventHandler(this.cmbCust_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(68, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "Customer";
            // 
            // btnShowSingle
            // 
            this.btnShowSingle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowSingle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowSingle.Location = new System.Drawing.Point(432, 40);
            this.btnShowSingle.Name = "btnShowSingle";
            this.btnShowSingle.Size = new System.Drawing.Size(158, 26);
            this.btnShowSingle.TabIndex = 3;
            this.btnShowSingle.Text = "Show Customer";
            this.btnShowSingle.UseVisualStyleBackColor = true;
            this.btnShowSingle.Click += new System.EventHandler(this.btnShowSingle_Click);
            // 
            // btnShow
            // 
            this.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShow.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.Location = new System.Drawing.Point(432, 81);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(158, 26);
            this.btnShow.TabIndex = 4;
            this.btnShow.Text = "Show All Customer";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(221, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "To";
            // 
            // datefrom
            // 
            this.datefrom.CustomFormat = "dd-MM-yyyy";
            this.datefrom.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefrom.Location = new System.Drawing.Point(86, 86);
            this.datefrom.Name = "datefrom";
            this.datefrom.Size = new System.Drawing.Size(129, 23);
            this.datefrom.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "From";
            // 
            // lblid
            // 
            this.lblid.AutoSize = true;
            this.lblid.Location = new System.Drawing.Point(941, 33);
            this.lblid.Name = "lblid";
            this.lblid.Size = new System.Drawing.Size(47, 15);
            this.lblid.TabIndex = 5;
            this.lblid.Text = "label2";
            this.lblid.Visible = false;
            // 
            // cmbQuotation
            // 
            this.cmbQuotation.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbQuotation.FormattingEnabled = true;
            this.cmbQuotation.Location = new System.Drawing.Point(136, 66);
            this.cmbQuotation.Name = "cmbQuotation";
            this.cmbQuotation.Size = new System.Drawing.Size(91, 23);
            this.cmbQuotation.TabIndex = 1;
            this.cmbQuotation.Leave += new System.EventHandler(this.cmbQuotation_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(55, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 15);
            this.label11.TabIndex = 9;
            this.label11.Text = "Customer";
            // 
            // lblCompany
            // 
            this.lblCompany.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblCompany.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.ForeColor = System.Drawing.Color.Maroon;
            this.lblCompany.Location = new System.Drawing.Point(22, 8);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(966, 22);
            this.lblCompany.TabIndex = 3;
            this.lblCompany.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Quotation No.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbQuotation);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnShowHistory);
            this.groupBox1.Controls.Add(this.cmbCustomer);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox1.Location = new System.Drawing.Point(10, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 158);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnShowHistory
            // 
            this.btnShowHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowHistory.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowHistory.Location = new System.Drawing.Point(136, 101);
            this.btnShowHistory.Name = "btnShowHistory";
            this.btnShowHistory.Size = new System.Drawing.Size(159, 34);
            this.btnShowHistory.TabIndex = 2;
            this.btnShowHistory.Text = "Show History";
            this.btnShowHistory.UseVisualStyleBackColor = true;
            this.btnShowHistory.Click += new System.EventHandler(this.btnShowHistory_Click);
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(136, 35);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(192, 23);
            this.cmbCustomer.TabIndex = 0;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbCustomer_SelectedIndexChanged);
            this.cmbCustomer.Leave += new System.EventHandler(this.cmbCustomer_Leave);
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(480, 31);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(37, 15);
            this.lblYear.TabIndex = 4;
            this.lblYear.Text = "Year";
            // 
            // FrmRptQuotation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1010, 656);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblid);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Maroon;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FrmRptQuotation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quotation Report";
            this.Load += new System.EventHandler(this.FrmRptQuotation_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmRptQuotation_KeyDown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.DateTimePicker dateto;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbCust;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnShowSingle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datefrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblid;
        private System.Windows.Forms.ComboBox cmbQuotation;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox lblCompany;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnShowHistory;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Label lblYear;
    }
}