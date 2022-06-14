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
    public partial class FrmRptCustomerSell : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        string Customer = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptSell.rpt");
        string SellGST = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptSellwithoutGST.rpt");
        string Customer1 = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptCustomerSell.rpt");
        DataTable dt = new DataTable();
        int billno,count,cust; 

        public FrmRptCustomerSell()
        {
            InitializeComponent();
        }

        private void FrmRptCustomerSell_Load(object sender, EventArgs e)
        {
            getCompanyName();
            getCustomer();
            getCustomer1();
            getInvoiceNo();
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

        private void getCustomer()
        {
            try
            {
                con = c.openConnection();
                cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbCustomer.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbCustomer.DataBindings.Clear();
                query = "select cid,cname from tblcustomer where oid='" + lblid.Text + " ' ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("Customer");
                da.Fill(ds, "Customer");
                cmbCustomer.DataSource = ds;
                cmbCustomer.ValueMember = "Customer.cid";
                cmbCustomer.DisplayMember = "Customer.cname";
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

        }

        private void getCustomer1()
        {
            try
            {
                con = c.openConnection();
                cmbCust.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbCust.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbCust.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbCust.DataBindings.Clear();
                query = "select cid,cname from tblcustomer where oid='" + lblid.Text + " ' ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("Customer");
                da.Fill(ds, "Customer");
                cmbCust.DataSource = ds;
                cmbCust.ValueMember = "Customer.cid";
                cmbCust.DisplayMember = "Customer.cname";
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

        }

        private void getInvoiceNo()
        {
            try
            {
                con = c.openConnection();
                cmbInvoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbInvoice.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbInvoice.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbInvoice.DataBindings.Clear();
                query = "select DISTINCT invoiceno from tblsell where oid='" + lblid.Text + " ' and cid='" + cmbCustomer.SelectedValue + "' and year='"+lblYear.Text+"'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("Customer");
                da.Fill(ds, "Customer");
                cmbInvoice.DataSource = ds;
                cmbInvoice.ValueMember = "Customer.invoiceno";
                cmbInvoice.DisplayMember = "Customer.invoiceno";
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbInvoice.DataSource = null;
            getInvoiceNo();
        }

        private void btnShowHistory_Click(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                 query = "select count(sid) s from tblsell where invoiceno=@invoiceno and year='"+lblYear.Text+"' and oid='"+lblid.Text+"' and flag=1";
                SqlCommand cmd1 = new SqlCommand(query, con);
                cmd1.Parameters.AddWithValue("@invoiceno", cmbInvoice.SelectedValue);
                SqlDataReader sdr1 = cmd1.ExecuteReader();
                while (sdr1.Read())
                {
                    count = Convert.ToInt32(sdr1.GetValue(0));
                }
                sdr1.Close();
                if (count > 0)
                {
                    crystalReportViewer1.Visible = false;
                    ReportDocument re = new ReportDocument();
                    re.Load(SellGST);

                    query = "select * from tblsell s, tblsellitem sitem, tblowner o, tblcustomer c, tblsellproduct sp  where s.invoiceno=@invoiceno and s.invoiceno=sitem.invoiceno  and s.cid=c.cid  and s.cid=@customerid and sitem.spid=sp.spid and o.oid='" + lblid.Text + "' and s.oid=o.oid and sitem.oid=s.oid and sitem.year='" + lblYear.Text + "' and sitem.year=s.year ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@customerid", cmbCustomer.SelectedValue);
                    cmd.Parameters.AddWithValue("@invoiceno", cmbInvoice.SelectedValue);
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
                else
                {
                    crystalReportViewer1.Visible = false;
                    ReportDocument re = new ReportDocument();
                    re.Load(Customer);

                    query = "select * from tblsell s, tblsellitem sitem, tblowner o, tblcustomer c, tblsellproduct sp  where s.invoiceno=@invoiceno and s.invoiceno=sitem.invoiceno  and s.cid=c.cid  and s.cid=@customerid and sitem.spid=sp.spid and o.oid='" + lblid.Text + "' and s.oid=o.oid and sitem.oid=s.oid and sitem.year='" + lblYear.Text + "' and sitem.year=s.year ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@customerid", cmbCustomer.SelectedValue);
                    cmd.Parameters.AddWithValue("@invoiceno", cmbInvoice.SelectedValue);
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
                //MessageBox.Show(ee.Message);
            }
            finally
            {
                //con.Close();
                dt.Clear();
                dt.Dispose();
            }
        }

        private void btnShowSingle_Click(object sender, EventArgs e)
        {
            try
            {
                crystalReportViewer1.Visible = false;
                ReportDocument re = new ReportDocument();
                re.Load(Customer1);
                con = c.openConnection();
                query = "select * from tblsell s, tblsellitem sitem, tblowner o, tblcustomer c, tblsellproduct sp  where s.date between @date1 and @date2 and   s.invoiceno=sitem.invoiceno  and s.cid=c.cid  and s.cid=@customerid and sitem.spid=sp.spid and o.oid='" + lblid.Text + "' and s.oid=o.oid and sitem.oid=s.oid and sitem.year='" + lblYear.Text + "' and sitem.year=s.year ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@date1", datefrom.Value.ToString("dd-MM-yyyy"));
                cmd.Parameters.AddWithValue("@date2", dateto.Value.ToString("dd-MM-yyyy"));
                cmd.Parameters.AddWithValue("@customerid", cmbCust.SelectedValue);
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
                re.SetParameterValue("fromdate", datefrom.Text);
                re.SetParameterValue("todate", dateto.Text);
                crystalReportViewer1.ReportSource = re;
            }
            catch (Exception ee)
            {
              //  MessageBox.Show(ee.Message);
            }
            finally
            {
                //con.Close();
                dt.Clear();
                dt.Dispose();
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                crystalReportViewer1.Visible = false;
                ReportDocument re = new ReportDocument();
                re.Load(Customer1);
                con = c.openConnection();
                query = "select * from tblsell s, tblsellitem sitem, tblowner o, tblcustomer c, tblsellproduct sp  where s.date between @date1 and @date2 and   s.invoiceno=sitem.invoiceno  and s.cid=c.cid and sitem.spid=sp.spid and o.oid='" + lblid.Text + "' and s.oid=o.oid and sitem.oid=s.oid and sitem.year='" + lblYear.Text + "' and sitem.year=s.year ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@date1", datefrom.Value.ToString("dd-MM-yyyy"));
                cmd.Parameters.AddWithValue("@date2", dateto.Value.ToString("dd-MM-yyyy"));
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
                re.SetParameterValue("fromdate", datefrom.Text);
                re.SetParameterValue("todate", dateto.Text);
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

        private void FrmRptCustomerSell_KeyDown(object sender, KeyEventArgs e)
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

        private void cmbCustomer_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                query = "select count(cid) from tblcustomer where oid='" + lblid.Text + "' and cname='" + cmbCustomer.Text + "'";
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
                    FrmRptCustomerSell_Load(sender, e);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void cmbCust_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                query = "select count(cid) from tblcustomer where oid='" + lblid.Text + "' and cname='" + cmbCust.Text + "'";
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
                    FrmRptCustomerSell_Load(sender, e);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void cmbInvoice_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                query = "select count(sid) from tblsell where oid='" + lblid.Text + "' and invoiceno='" + cmbInvoice.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cust = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (cust == 0)
                {
                    MessageBox.Show("Invoice does not Exist. Please Select Invoice No. !!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmRptCustomerSell_Load(sender, e);
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
