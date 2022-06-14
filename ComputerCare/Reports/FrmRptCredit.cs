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
    public partial class FrmRptCredit : Form
    {
        connection c = new connection();
        SqlConnection con;
        int cust;
        string query, cname;
        string Customer = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptCustomerCredit.rpt");
        string Dealer = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptDealerCredit.rpt");
        DataTable dt = new DataTable();
    

        public FrmRptCredit()
        {
            InitializeComponent();
        }

        private void FrmRptCredit_Load(object sender, EventArgs e)
        {
            getCompanyName();
            rbCustomer.Checked = true;
            rbCustomer1.Checked = true;
            cmbUser.Focus();
        }
        private void getCompanyName()
        {
            try
            {
                con = c.openConnection();
                query = "select  name from tblsession";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    lblCompany.Text = sdr.GetValue(0).ToString();
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

        private void getCustomer()
        {
            try
            {
                con = c.openConnection();
                cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbUser.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbUser.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbUser.DataBindings.Clear();
                query = "select cid,cname from tblcustomer where oid='" + lblid.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet("Customer");

                da.Fill(ds, "Customer");
                cmbUser.DataSource = ds;
                cmbUser.ValueMember = "Customer.cid";
                cmbUser.DisplayMember = "Customer.cname";

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
                query = "select did,dname from tbldealer where oid='" + lblid.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet("Dealer");

                da.Fill(ds, "Dealer");
                cmbUser.DataSource = ds;
                cmbUser.ValueMember = "Dealer.did";
                cmbUser.DisplayMember = "Dealer.dname";

            }
            catch (Exception ee)
            {

            }
        }

        private void rbCustomer_CheckedChanged(object sender, EventArgs e)
        {
            getCustomer();
        }

        private void rbDealer_CheckedChanged(object sender, EventArgs e)
        {
            getDealer();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (rbCustomer.Checked == true)
            {
                try
                {
                    crystalReportViewer1.Visible = false;
                    ReportDocument re = new ReportDocument();
                    re.Load(Customer);
                    con = c.openConnection();
                    query = "select * from tblcustomer c,tblowner o where cid=@customerid and o.oid=c.oid and o.oid='" + lblid.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@customerid", cmbUser.SelectedValue);
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
            else if (rbDealer.Checked == true)
            {
                try
                {
                    crystalReportViewer1.Visible = false;
                    ReportDocument re = new ReportDocument();
                    re.Load(Dealer);
                    con = c.openConnection();
                    query = "select * from tbldealer d,tblowner o where did=@dealerid and o.oid=d.oid and o.oid='" + lblid.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@dealerid", cmbUser.SelectedValue);
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
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            if (rbCustomer1.Checked == true)
            {
                try
                {
                    crystalReportViewer1.Visible = false;
                    ReportDocument re = new ReportDocument();
                    re.Load(Customer);
                    con = c.openConnection();
                    query = "select * from tblcustomer c,tblowner o where o.oid=c.oid and o.oid='" + lblid.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
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
                catch (Exception ee)
                {
                    //MessageBox.Show(ee.Message);
                }
                finally
                {
                    //con.Close();
                    dt.Clear();
                    dt.Dispose();
                }
            }
            else if (rbDealer1.Checked == true)
            {
                try
                {
                    crystalReportViewer1.Visible = false;
                    ReportDocument re = new ReportDocument();
                    re.Load(Dealer);
                    con = c.openConnection();
                    query = "select * from tbldealer d,tblowner o where o.oid=d.oid and o.oid='" + lblid.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
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
                catch (Exception ee)
                {
                    //MessageBox.Show(ee.Message);
                }
                finally
                {
                    dt.Clear();
                    dt.Dispose();
                }
            }
        }

        private void FrmRptCredit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                }
                if (e.KeyCode == Keys.Escape)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you  want to Close this window ?", ""+cname+"", MessageBoxButtons.YesNo);
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
                        FrmRptCredit_Load(sender, e);
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
                        FrmRptCredit_Load(sender, e);
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
