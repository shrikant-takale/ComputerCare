namespace ComputerCare.Operations
{
    partial class FrmRepair
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRepair));
            this.lblCompany = new System.Windows.Forms.TextBox();
            this.lblid = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtServiceTagNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblContact = new System.Windows.Forms.Label();
            this.lblCid = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtInward = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInvoice = new System.Windows.Forms.Label();
            this.lblinvoiceno = new System.Windows.Forms.Label();
            this.dtRepair = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.txtGST = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblGST = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTAmt = new System.Windows.Forms.Label();
            this.cmbPayType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPaid = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.lblRemain = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.chkGST = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblCustGST = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCompany
            // 
            this.lblCompany.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblCompany.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblCompany.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.ForeColor = System.Drawing.Color.Maroon;
            this.lblCompany.Location = new System.Drawing.Point(9, 12);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.ReadOnly = true;
            this.lblCompany.Size = new System.Drawing.Size(1069, 25);
            this.lblCompany.TabIndex = 12;
            this.lblCompany.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblid
            // 
            this.lblid.AutoSize = true;
            this.lblid.Location = new System.Drawing.Point(879, 40);
            this.lblid.Name = "lblid";
            this.lblid.Size = new System.Drawing.Size(47, 15);
            this.lblid.TabIndex = 14;
            this.lblid.Text = "label2";
            this.lblid.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblCustGST);
            this.groupBox2.Controls.Add(this.cmbStatus);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtServiceTagNo);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lblContact);
            this.groupBox2.Controls.Add(this.lblCid);
            this.groupBox2.Controls.Add(this.lblCustomer);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtInward);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblInvoice);
            this.groupBox2.Controls.Add(this.lblinvoiceno);
            this.groupBox2.Controls.Add(this.dtRepair);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(9, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(798, 108);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // txtServiceTagNo
            // 
            this.txtServiceTagNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtServiceTagNo.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceTagNo.Location = new System.Drawing.Point(407, 69);
            this.txtServiceTagNo.Name = "txtServiceTagNo";
            this.txtServiceTagNo.ReadOnly = true;
            this.txtServiceTagNo.Size = new System.Drawing.Size(176, 23);
            this.txtServiceTagNo.TabIndex = 67;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(292, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 15);
            this.label7.TabIndex = 68;
            this.label7.Text = "Service Tag No.";
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.Location = new System.Drawing.Point(741, 35);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(14, 14);
            this.lblContact.TabIndex = 66;
            this.lblContact.Text = "0";
            this.lblContact.Visible = false;
            // 
            // lblCid
            // 
            this.lblCid.AutoSize = true;
            this.lblCid.Location = new System.Drawing.Point(712, 34);
            this.lblCid.Name = "lblCid";
            this.lblCid.Size = new System.Drawing.Size(14, 14);
            this.lblCid.TabIndex = 2;
            this.lblCid.Text = "0";
            this.lblCid.Visible = false;
            // 
            // lblCustomer
            // 
            this.lblCustomer.BackColor = System.Drawing.SystemColors.Window;
            this.lblCustomer.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.Location = new System.Drawing.Point(85, 69);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.ReadOnly = true;
            this.lblCustomer.Size = new System.Drawing.Size(196, 23);
            this.lblCustomer.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 15);
            this.label5.TabIndex = 65;
            this.label5.Text = "Customer";
            // 
            // txtInward
            // 
            this.txtInward.Location = new System.Drawing.Point(558, 31);
            this.txtInward.Name = "txtInward";
            this.txtInward.Size = new System.Drawing.Size(135, 22);
            this.txtInward.TabIndex = 0;
            this.txtInward.Leave += new System.EventHandler(this.txtInward_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(436, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 15);
            this.label1.TabIndex = 64;
            this.label1.Text = "Enter Inward No.";
            // 
            // lblInvoice
            // 
            this.lblInvoice.AutoSize = true;
            this.lblInvoice.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoice.Location = new System.Drawing.Point(348, 34);
            this.lblInvoice.Name = "lblInvoice";
            this.lblInvoice.Size = new System.Drawing.Size(15, 15);
            this.lblInvoice.TabIndex = 4;
            this.lblInvoice.Text = "0";
            // 
            // lblinvoiceno
            // 
            this.lblinvoiceno.AutoSize = true;
            this.lblinvoiceno.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblinvoiceno.Location = new System.Drawing.Point(263, 34);
            this.lblinvoiceno.Name = "lblinvoiceno";
            this.lblinvoiceno.Size = new System.Drawing.Size(79, 15);
            this.lblinvoiceno.TabIndex = 62;
            this.lblinvoiceno.Text = "Invoice No.";
            // 
            // dtRepair
            // 
            this.dtRepair.CustomFormat = "dd-MM-yyyy";
            this.dtRepair.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtRepair.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtRepair.Location = new System.Drawing.Point(117, 31);
            this.dtRepair.Name = "dtRepair";
            this.dtRepair.Size = new System.Drawing.Size(114, 23);
            this.dtRepair.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(23, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Invoice Date";
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Window;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(9, 173);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(798, 269);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // txtGST
            // 
            this.txtGST.Location = new System.Drawing.Point(929, 167);
            this.txtGST.Name = "txtGST";
            this.txtGST.Size = new System.Drawing.Size(59, 23);
            this.txtGST.TabIndex = 4;
            this.txtGST.Text = "0";
            this.txtGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGST.Leave += new System.EventHandler(this.txtGST_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(868, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 170;
            this.label2.Text = "GST(%)";
            // 
            // lblGST
            // 
            this.lblGST.AutoSize = true;
            this.lblGST.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGST.Location = new System.Drawing.Point(1004, 170);
            this.lblGST.Name = "lblGST";
            this.lblGST.Size = new System.Drawing.Size(15, 15);
            this.lblGST.TabIndex = 5;
            this.lblGST.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(879, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 15);
            this.label6.TabIndex = 177;
            this.label6.Text = "Total";
            // 
            // lblTAmt
            // 
            this.lblTAmt.AutoSize = true;
            this.lblTAmt.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTAmt.Location = new System.Drawing.Point(925, 205);
            this.lblTAmt.Name = "lblTAmt";
            this.lblTAmt.Size = new System.Drawing.Size(15, 15);
            this.lblTAmt.TabIndex = 6;
            this.lblTAmt.Text = "0";
            this.lblTAmt.TextChanged += new System.EventHandler(this.lblTAmt_TextChanged);
            // 
            // cmbPayType
            // 
            this.cmbPayType.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPayType.FormattingEnabled = true;
            this.cmbPayType.Items.AddRange(new object[] {
            "Cash",
            "Credit Card",
            "Debit Card",
            "Online Transfer"});
            this.cmbPayType.Location = new System.Drawing.Point(934, 235);
            this.cmbPayType.Name = "cmbPayType";
            this.cmbPayType.Size = new System.Drawing.Size(110, 23);
            this.cmbPayType.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(818, 238);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 15);
            this.label10.TabIndex = 180;
            this.label10.Text = "Select Pay Type";
            // 
            // txtPaid
            // 
            this.txtPaid.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaid.Location = new System.Drawing.Point(934, 269);
            this.txtPaid.Name = "txtPaid";
            this.txtPaid.Size = new System.Drawing.Size(144, 23);
            this.txtPaid.TabIndex = 8;
            this.txtPaid.Text = "0";
            this.txtPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPaid.Leave += new System.EventHandler(this.txtPaid_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(839, 272);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 15);
            this.label14.TabIndex = 182;
            this.label14.Text = "Paid Amount";
            // 
            // lblRemain
            // 
            this.lblRemain.AutoSize = true;
            this.lblRemain.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemain.Location = new System.Drawing.Point(938, 306);
            this.lblRemain.Name = "lblRemain";
            this.lblRemain.Size = new System.Drawing.Size(15, 15);
            this.lblRemain.TabIndex = 9;
            this.lblRemain.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(828, 306);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(109, 15);
            this.label16.TabIndex = 184;
            this.label16.Text = "Remain Amount";
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(832, 372);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 37);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save && Print";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(950, 372);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(105, 37);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(929, 138);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(157, 23);
            this.txtAmount.TabIndex = 3;
            this.txtAmount.Text = "0";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(820, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 15);
            this.label4.TabIndex = 188;
            this.label4.Text = "Repair Amount";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(488, 40);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(37, 15);
            this.lblYear.TabIndex = 13;
            this.lblYear.Text = "Year";
            // 
            // chkGST
            // 
            this.chkGST.AutoSize = true;
            this.chkGST.Location = new System.Drawing.Point(899, 92);
            this.chkGST.Name = "chkGST";
            this.chkGST.Size = new System.Drawing.Size(106, 19);
            this.chkGST.TabIndex = 2;
            this.chkGST.Text = "Without GST";
            this.chkGST.UseVisualStyleBackColor = true;
            this.chkGST.CheckedChanged += new System.EventHandler(this.chkGST_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(603, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 15);
            this.label8.TabIndex = 69;
            this.label8.Text = "Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Repaired",
            "Not Repaired"});
            this.cmbStatus.Location = new System.Drawing.Point(658, 69);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(121, 22);
            this.cmbStatus.TabIndex = 70;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // lblCustGST
            // 
            this.lblCustGST.AutoSize = true;
            this.lblCustGST.Location = new System.Drawing.Point(765, 34);
            this.lblCustGST.Name = "lblCustGST";
            this.lblCustGST.Size = new System.Drawing.Size(14, 14);
            this.lblCustGST.TabIndex = 71;
            this.lblCustGST.Text = "0";
            this.lblCustGST.Visible = false;
            // 
            // FrmRepair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1098, 445);
            this.Controls.Add(this.chkGST);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblRemain);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtPaid);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmbPayType);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblTAmt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblGST);
            this.Controls.Add(this.txtGST);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.lblid);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Maroon;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FrmRepair";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Repair";
            this.Load += new System.EventHandler(this.FrmRepair_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmRepair_KeyDown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox lblCompany;
        private System.Windows.Forms.Label lblid;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblInvoice;
        private System.Windows.Forms.Label lblinvoiceno;
        private System.Windows.Forms.DateTimePicker dtRepair;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInward;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox txtGST;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblGST;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTAmt;
        private System.Windows.Forms.ComboBox cmbPayType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPaid;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblRemain;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox lblCustomer;
        private System.Windows.Forms.Label lblCid;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.CheckBox chkGST;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.TextBox txtServiceTagNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblCustGST;
    }
}