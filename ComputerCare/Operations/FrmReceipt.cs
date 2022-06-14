using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ComputerCare.Connections;
using ComputerCare.Reports;

namespace ComputerCare.Operations
{
    public partial class FrmReceipt : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        int i, maxid, j, k,billno=1000,count,cust;

        public FrmReceipt()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmReceipt_Load(object sender, EventArgs e)
        {
            getCompanyName();
            getReceiptNo();
            rbCustomer.Checked = true;
            cmbUser.Focus();

        }

        private void getDealer()
        {
            try
            {
                con = c.openConnection();
                cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbUser.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbUser.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbUser.DataBindings.Clear();
                query = "select did,dname from tbldealer where oid='" + lblid.Text + " ' ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("Dealer");
                da.Fill(ds, "Dealer");
                cmbUser.DataSource = ds;
                cmbUser.ValueMember = "Dealer.did";
                cmbUser.DisplayMember = "Dealer.dname";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }


        private void getOutstanding()
        {
            try
            {
                if (rbCustomer.Checked == true)
                {

                    con = c.openConnection();
                    lblOutStanding.Text = "0";
                    int cid = 0;
                    cid = Int32.Parse(cmbUser.SelectedValue.ToString());
                    query = "select copcredit from tblcustomer where cid='" + cid + "' and oid='" + lblid.Text + "'";
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    SqlDataReader sdr1 = cmd1.ExecuteReader();
                    while (sdr1.Read())
                    {
                        count = Convert.ToInt32(sdr1.GetValue(0));
                    }
                    sdr1.Close();
                    if (count > 0)
                    {

                        query = "select copcredit from tblcustomer where cid='" + cid + "' and oid='" + lblid.Text + "'";
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            lblOutStanding.Text = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();
                    }
                    else
                    {
                        lblOutStanding.Text = "0";
                    }
                }
                else if (rbDealer.Checked == true)
                {
                    con = c.openConnection();
                    lblOutStanding.Text = "0";
                    int did = 0;
                    did = Int32.Parse(cmbUser.SelectedValue.ToString());
                    query = "select dopcredit from tbldealer where did='" + did + "'and oid='" + lblid.Text + "'";
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    SqlDataReader sdr1 = cmd1.ExecuteReader();
                    while (sdr1.Read())
                    {
                        count = Convert.ToInt32(sdr1.GetValue(0));
                    }
                    sdr1.Close();
                    if (count > 0)
                    {
                        //int did = 0;
                        //did = Int32.Parse(cmbUser.SelectedValue.ToString());
                        query = "select dopcredit from tbldealer where did='" + did + "'and oid='" + lblid.Text + "'";
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            lblOutStanding.Text = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();
                    }
                    else
                    {
                        lblOutStanding.Text = " 0";
                    }
                }
            }
            catch (Exception ee)
            {
               // MessageBox.Show(ee.Message);
            }
        }

        private void getCustomer()
        {
            try
            {
                con = c.openConnection();
                cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbUser.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbUser.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbUser.DataBindings.Clear();
                query = "select cid,cname from tblcustomer where oid='" + lblid.Text + " '";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("Customer");
                da.Fill(ds, "Customer");
                cmbUser.DataSource = ds;
                cmbUser.ValueMember = "Customer.cid";
                cmbUser.DisplayMember = "Customer.cname";
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

        }

        private void getCompanyName()
        {
            try
            {
                con = c.openConnection();
                query = "select  name,year from tblsession";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    lblCompany.Text = sdr.GetValue(0).ToString();
                    lblYear.Text = sdr.GetValue(1).ToString();
                    cname = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                query = "select oid from tblowner where name='" + cname + "'";
                SqlCommand cmd1 = new SqlCommand(query, con);
                SqlDataReader sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    lblid.Text = sdr1.GetValue(0).ToString();
                }
                sdr1.Close();
            }
            catch (Exception ee)
            {
            }
        }
        private void getReceiptNo()
        {
            try
            {
                int cc = 0;
                con = c.openConnection();
                query = "select count(rpid) from tblreceipt where oid='"+lblid.Text+"' and year='"+lblYear.Text+"'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cc = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (cc == 0)
                {
                    lblReceipt.Text = Convert.ToString(billno);
                }
                else
                {
                    query = "select max(receiptno) from tblreceipt where oid='" + lblid.Text + "' and year='" + lblYear.Text + "' ";
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    SqlDataReader sdr1 = cmd1.ExecuteReader();
                    if (sdr1.Read())
                    {
                        billno = 1 + Convert.ToInt32(sdr1.GetValue(0));
                        lblReceipt.Text = Convert.ToString(billno);
                    }
                    sdr1.Close();
                }
            }
            catch (Exception ee)
            {

            }
        }
        private void receiptBill(int maxid, string usertype)
        {
            FrmRptReceipt frr = new FrmRptReceipt(maxid, usertype);
            this.Hide();
            frr.ShowDialog();
        }


        private void cleareTexts()
        {
            txtPaid.Text = "";
            txtNarration.Text = "";
            lblOutStanding.Text = "0";
            lblRemain.Text = "0";
            date.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                if (txtPaid.Text == "" || txtPaid.Text == "0")
                {
                    MessageBox.Show("Please Enter Paid Amount", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaid.Focus();
                }
                else   if (rbCustomer.Checked == true)
                {
                    string usertype = "Customer Receipt";
                    query = "insert into tblreceipt (oid,receiptno,userid,usertype,outstanding,paid,remain,narration,date,year) values (@oid,@receiptno,@userid,@usertype,@outstanding,@paid,@remain,@narration,@date,@year)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@oid", lblid.Text);
                    cmd.Parameters.AddWithValue("@receiptno", lblReceipt.Text);
                    cmd.Parameters.AddWithValue("@userid", cmbUser.SelectedValue);
                    cmd.Parameters.AddWithValue("@usertype", usertype);
                    cmd.Parameters.AddWithValue("@outstanding", lblOutStanding.Text);
                    cmd.Parameters.AddWithValue("@paid", txtPaid.Text);
                    cmd.Parameters.AddWithValue("@remain", lblRemain.Text);
                    cmd.Parameters.AddWithValue("@narration", txtNarration.Text);
                    cmd.Parameters.AddWithValue("@date", date.Value.ToString("dd-MM-yyyy"));
                    cmd.Parameters.AddWithValue("@year", lblYear.Text);
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        query = "select max(receiptno) from tblreceipt where oid='" + lblid.Text + "' and year='" + lblYear.Text + "'";
                        SqlCommand cmd3 = new SqlCommand(query, con);
                        SqlDataReader sdr = cmd3.ExecuteReader();
                        if (sdr.Read())
                        {
                            maxid = Convert.ToInt32(sdr.GetValue(0));
                        }
                        sdr.Close();

                        query = "update tblcustomer set copcredit=@credit where cid=@customerid and oid='" + lblid.Text + "'";
                        SqlCommand cmd1 = new SqlCommand(query, con);
                        cmd1.Parameters.AddWithValue("@credit", lblRemain.Text);
                        cmd1.Parameters.AddWithValue("@customerid", cmbUser.SelectedValue);
                        j = cmd1.ExecuteNonQuery();
                        if (j > 0)
                        {
                            query = "insert into tblbalancesheet (oid,date,type,typeid,description,credit,debit,year) values (@oid,@date,@type,@typeid,@description,@credit,@debit,@year)";
                            SqlCommand cmd2 = new SqlCommand(query, con);
                            cmd2.Parameters.AddWithValue("@oid", lblid.Text);
                            cmd2.Parameters.AddWithValue("@date", date.Value.ToString("dd-MM-yyyy"));
                            cmd2.Parameters.AddWithValue("@type", "Receipt Account");
                            cmd2.Parameters.AddWithValue("@typeid", cmbUser.SelectedValue);
                            cmd2.Parameters.AddWithValue("@description", " Receipt Payment From Customer  "+" " + cmbUser.Text +""+ " Against Receipt No. "+" " + lblReceipt.Text + ".");
                            cmd2.Parameters.AddWithValue("@credit", txtPaid.Text);
                            cmd2.Parameters.AddWithValue("@debit", "0.00");
                            cmd2.Parameters.AddWithValue("@year", lblYear.Text);
                            k = cmd2.ExecuteNonQuery();
                            if (k > 0)
                            {
                                MessageBox.Show("Receipt  Inserted Successfully", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                receiptBill(maxid, usertype);
                                cleareTexts();
                                FrmReceipt_Load(sender, e);
                            }

                        }
                    }
                }
                else if (rbDealer.Checked == true)
                {
                    string usertype = "Dealer Receipt";
                    query = "insert into tblreceipt (oid,receiptno,userid,usertype,outstanding,paid,remain,narration,date,year) values (@oid,@receiptno,@userid,@usertype,@outstanding,@paid,@remain,@narration,@date,@year)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@oid", lblid.Text);
                    cmd.Parameters.AddWithValue("@receiptno",lblReceipt.Text);
                    cmd.Parameters.AddWithValue("@userid", cmbUser.SelectedValue);
                    cmd.Parameters.AddWithValue("@usertype", usertype);
                    cmd.Parameters.AddWithValue("@outstanding", lblOutStanding.Text);
                    cmd.Parameters.AddWithValue("@paid", txtPaid.Text);
                    cmd.Parameters.AddWithValue("@remain", lblRemain.Text);
                    cmd.Parameters.AddWithValue("@narration", txtNarration.Text);
                    cmd.Parameters.AddWithValue("@date", date.Value.ToString("dd-MM-yyyy"));
                    cmd.Parameters.AddWithValue("@year", lblYear.Text);

                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        query = "select max(receiptno) from tblreceipt where oid='" + lblid.Text + "' and year='" + lblYear.Text + "'";
                        SqlCommand cmd3 = new SqlCommand(query, con);
                        SqlDataReader sdr = cmd3.ExecuteReader();
                        if (sdr.Read())
                        {
                            maxid = Convert.ToInt32(sdr.GetValue(0));
                        }
                        sdr.Close();

                        query = "update tbldealer set dopcredit=@credit where did=@dealerid and oid='" + lblid.Text + "'";
                        SqlCommand cmd1 = new SqlCommand(query, con);
                        cmd1.Parameters.AddWithValue("@credit", lblRemain.Text);
                        cmd1.Parameters.AddWithValue("@dealerid", cmbUser.SelectedValue);
                        j = cmd1.ExecuteNonQuery();
                        if (j > 0)
                        {
                            query = "insert into tblbalancesheet (oid,date,type,typeid,description,credit,debit,year) values (@oid,@date,@type,@typeid,@description,@credit,@debit,@year)";
                            SqlCommand cmd2 = new SqlCommand(query, con);
                            cmd2.Parameters.AddWithValue("@oid", lblid.Text);
                            cmd2.Parameters.AddWithValue("@date", date.Value.ToString("dd-MM-yyyy"));
                            cmd2.Parameters.AddWithValue("@type", "Receipt Account");
                            cmd2.Parameters.AddWithValue("@typeid", cmbUser.SelectedValue);
                            cmd2.Parameters.AddWithValue("@description", " Give Receipt Payment To Dealer " + cmbUser.Text + " Against Receipt No." + maxid + ".");
                            cmd2.Parameters.AddWithValue("@credit", "0.00");
                            cmd2.Parameters.AddWithValue("@debit", txtPaid.Text);
                            cmd2.Parameters.AddWithValue("@year", lblYear.Text);
                            k = cmd2.ExecuteNonQuery();
                            if (k > 0)
                            {

                                MessageBox.Show("Receipt Inserted Successfully", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                receiptBill(maxid, usertype);
                                cleareTexts();
                                FrmReceipt_Load(sender, e);
                            }

                        }
                    }
                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message);
            }
        }

        private void rbCustomer_CheckedChanged(object sender, EventArgs e)
        {
            getCustomer();
            txtPaid.Text = "0";
            lblRemain.Text = "0";
            getOutstanding();
        }

        private void rbDealer_CheckedChanged(object sender, EventArgs e)
        {
            getDealer();
            txtPaid.Text = "0";
            lblRemain.Text = "0";
            // lblOutStanding.Text = "0";
            getOutstanding();
        }

        private void txtPaid_Leave(object sender, EventArgs e)
        {
            try
            {
                double paid = Convert.ToDouble(txtPaid.Text);
                double outst = Convert.ToDouble(lblOutStanding.Text);
                double remain = outst - paid;
                lblRemain.Text = remain.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void cmbUser_SelectedValueChanged(object sender, EventArgs e)
        {
            getOutstanding();
        }

        private void FrmReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                }
                if (e.KeyCode == Keys.Escape)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you  want to Close this window ?", "" + cname + "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ee)
            {
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbUser_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                if (rbCustomer.Checked == true)
                {
                    query = "select count(cid) from tblcustomer where oid='" + lblid.Text + "' and cname='" + cmbUser.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        cust = Convert.ToInt32(sdr.GetValue(0));
                    }
                    sdr.Close();
                    if (cust == 0)
                    {
                        MessageBox.Show("Customer does not Exist. Please Register Customer !!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmReceipt_Load(sender, e);
                        getCustomer();
                    }
                    else
                    {

                    }
                }
                else if (rbDealer.Checked == true)
                {

                    query = "select count(did) from tbldealer where oid='" + lblid.Text + "' and dname='" + cmbUser.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        cust = Convert.ToInt32(sdr.GetValue(0));
                    }
                    sdr.Close();
                    if (cust == 0)
                    {
                        MessageBox.Show("Dealer does not Exist. Please Register Customer !!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmReceipt_Load(sender, e);
                        getCustomer();
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


    }
}
