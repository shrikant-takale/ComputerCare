using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ComputerCare.Connections;
namespace ComputerCare.Reports
{
    public partial class FrmRptReceiptReport : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        int cust;
        string Customer = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptCustomerReceipt.rpt");
        string Dealer = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptDealerReceipt.rpt");
        DataTable dt = new DataTable();
        public FrmRptReceiptReport()
        {
            InitializeComponent();
        }

        private void FrmRptReceiptReport_Load(object sender, EventArgs e)
        {
            getCompanyName();
          
            rbCustomer.Checked = true;
          //  rbCust.Checked = true;
            getReceiptNo();
            cmbUser.Focus();

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

        //private void getDealer1()
        //{
        //    try
        //    {
        //        con = c.openConnection();
        //        cmbUse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
        //        cmbUse.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        cmbUse.AutoCompleteSource = AutoCompleteSource.ListItems;
        //        cmbUse.DataBindings.Clear();
        //        query = "select did,dname from tbldealer where oid='" + lblid.Text + " ' ";
        //        SqlCommand cmd = new SqlCommand(query, con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet("Dealer");
        //        da.Fill(ds, "Dealer");
        //        cmbUse.DataSource = ds;
        //        cmbUse.ValueMember = "Dealer.did";
        //        cmbUse.DisplayMember = "Dealer.dname";
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message);
        //    }

        //}
        //private void getCustomer1()
        //{
        //    try
        //    {
        //        con = c.openConnection();
        //        cmbUse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
        //        cmbUse.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        cmbUse.AutoCompleteSource = AutoCompleteSource.ListItems;
        //        cmbUse.DataBindings.Clear();
        //        query = "select cid,cname from tblcustomer where oid='" + lblid.Text + " '";
        //        SqlCommand cmd = new SqlCommand(query, con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet("Customer");
        //        da.Fill(ds, "Customer");
        //        cmbUse.DataSource = ds;
        //        cmbUse.ValueMember = "Customer.cid";
        //        cmbUse.DisplayMember = "Customer.cname";
        //    }
        //    catch (Exception ex)
        //    {
        //        // MessageBox.Show(ex.Message);
        //    }

        //}


        private void getReceiptNo()
        {
            try
            {
                con = c.openConnection();
                if (rbCustomer.Checked == true)
                {
                //query = "select count(rpid) from tblreceipt where oid='" + lblid.Text + "' and userid='" + cmbUser.SelectedValue + "' and usertype=@utype and year='" + lblYear.Text + "' ";
                //SqlCommand cmd = new SqlCommand(query, con);
                //cmd.Parameters.AddWithValue("@utype", "Customer Receipt");
                //SqlDataReader sdr = cmd.ExecuteReader();
                //while (sdr.Read())
                //{
                //    count = Convert.ToInt32(sdr.GetValue(0));
                //}
                //sdr.Close();
                //if (count > 0)
                //{
                    cmbReceipt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cmbReceipt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmbReceipt.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmbReceipt.DataBindings.Clear();
                    //string usertype = "Customer Receipt";
                    query = "select receiptno from tblreceipt where oid='" + lblid.Text + "' and userid='" + cmbUser.SelectedValue + "' and usertype=@utype and year='" + lblYear.Text + "' ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@utype", "Customer Receipt");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet("Customer");
                    da.Fill(ds, "Customer");
                    cmbReceipt.DataSource = ds;
                    cmbReceipt.ValueMember = "Customer.receiptno";
                    cmbReceipt.DisplayMember = "Customer.receiptno";
                //}
                //else
                //{
                //    cmbReceipt.Text = "";
                //}
                }
                else if (rbDealer.Checked == true)
                {
                    cmbReceipt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cmbReceipt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmbReceipt.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmbReceipt.DataBindings.Clear();
                  //  string usertype = "Dealer Receipt";
                    query = "select receiptno from tblreceipt where oid='" + lblid.Text + "' and userid='" + cmbUser.SelectedValue + "' and usertype=@utype  and year='" + lblYear.Text + "' ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@utype","Dealer Receipt");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet("Customer");
                    da.Fill(ds, "Customer");
                    cmbReceipt.DataSource = ds;
                    cmbReceipt.ValueMember = "Customer.receiptno";
                    cmbReceipt.DisplayMember = "Customer.receiptno";
                }
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void rbCustomer_CheckedChanged(object sender, EventArgs e)
        {
            getCustomer();
        }

        private void rbDealer_CheckedChanged(object sender, EventArgs e)
        {
            getDealer();
        }

        private void rbCust_CheckedChanged(object sender, EventArgs e)
        {
          //  getCustomer1();
        }

        private void rbDeal_CheckedChanged(object sender, EventArgs e)
        {
           // getDealer1();
        }

        private void cmbUser_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbReceipt.DataSource = null;
            getReceiptNo();
        }

        private void btnShowHistory_Click(object sender, EventArgs e)
        {
            try
            {
                getCompanyName();
                if (rbCustomer.Checked==true)
                {
                    crystalReportViewer1.Visible = false;
                    ReportDocument re = new ReportDocument();
                    re.Load(Customer);
                    con = c.openConnection();
                    query = "select * from tblreceipt r,tblcustomer c,tblowner o where r.userid=c.cid and r.userid=@uid  and  r.receiptno=@rid and r.oid=o.oid  and o.oid='" + lblid.Text + "'  and r.year='" + lblYear.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@rid",cmbReceipt.Text);
                    cmd.Parameters.AddWithValue("@uid",cmbUser.SelectedValue);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    ad.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        crystalReportViewer1.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Sorry ,This Data NOT Found");
                    }

                    re.SetDataSource(dt);
                    crystalReportViewer1.ReportSource = re;
                }
                else if (rbDealer.Checked==true)
                {
                    crystalReportViewer1.Visible = false;
                    ReportDocument re = new ReportDocument();
                    re.Load(Dealer);
                    con = c.openConnection();
                    query = "select * from tblreceipt r,tbldealer d,tblowner o where r.userid=d.did  and  r.userid=@uid and r.receiptno=@rid  and o.oid=r.oid and o.oid='" + lblid.Text + "' and r.year='" + lblYear.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                   cmd.Parameters.AddWithValue("@uid", cmbUser.SelectedValue);
                    cmd.Parameters.AddWithValue("@rid", cmbReceipt.Text);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    ad.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        crystalReportViewer1.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Sorry ,This Data NOT Found");
                    }

                    re.SetDataSource(dt);
                    crystalReportViewer1.ReportSource = re;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            finally
            {
                //con.Close();
                dt.Clear();
                dt.Dispose();
            }
        }

        private void lblid_Click(object sender, EventArgs e)
        {

        }

        private void FrmRptReceiptReport_KeyDown(object sender, KeyEventArgs e)
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

        private void label3_Click(object sender, EventArgs e)
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
                        FrmRptReceiptReport_Load(sender, e);
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
                        MessageBox.Show("Dealer does not Exist. Please Register Dealer !!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmRptReceiptReport_Load(sender, e);
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

        private void cmbReceipt_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                query = "select count(rpid) from tblreceipt where oid='" + lblid.Text + "' and receiptno='" + cmbReceipt.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cust = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (cust == 0)
                {
                    MessageBox.Show("Receipt does not Exist. Please Select Receipt No. !!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmRptReceiptReport_Load(sender, e);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
            }
        }

       
    }
}
