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
    public partial class FrmRptDealerPurchase : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        int cust;
        string Dealer = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptPurchase.rpt");
        string Dealer1 = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptDealerPurchase.rpt");
        DataTable dt = new DataTable();


        public FrmRptDealerPurchase()
        {
            InitializeComponent();
        }

        private void FrmRptDealerPurchase_Load(object sender, EventArgs e)
        {
            getCompanyName();
            getDealer();
            getDealer1();
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

        private void getDealer()
        {
            try
            {
                con = c.openConnection();
                cmbDealer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbDealer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbDealer.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbDealer.DataBindings.Clear();
                query = "select did,dname from tbldealer where oid='" + lblid.Text + " ' ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("Dealer");
                da.Fill(ds, "Dealer");
                cmbDealer.DataSource = ds;
                cmbDealer.ValueMember = "Dealer.did";
                cmbDealer.DisplayMember = "Dealer.dname";
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

        }

        private void getDealer1()
        {
            try
            {
                con = c.openConnection();
                cmbDeal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbDeal.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbDeal.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbDeal.DataBindings.Clear();
                query = "select did,dname from tbldealer where oid='" + lblid.Text + " ' ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("Dealer");
                da.Fill(ds, "Dealer");
                cmbDeal.DataSource = ds;
                cmbDeal.ValueMember = "Dealer.did";
                cmbDeal.DisplayMember = "Dealer.dname";
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
                query = "select DISTINCT invoiceno from tblpurchase where oid='" + lblid.Text + " ' and did='" + cmbDealer.SelectedValue + "' and year='"+lblYear.Text+"' ";
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

        private void cmbDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbInvoice.DataSource = null;
            getInvoiceNo();
        }

        private void btnShowHistory_Click(object sender, EventArgs e)
        {
            try
            {
                crystalReportViewer1.Visible = false;
                ReportDocument re = new ReportDocument();
                re.Load(Dealer);
                con = c.openConnection();
                query = "select * from tblpurchase p, tblpurchaseitem pitem, tblowner o, tbldealer d, tblsellproduct s  where p.invoiceno=@invoiceno and p.invoiceno=pitem.invoiceno  and p.did=@dealerid and  p.did=d.did  and pitem.spid=s.spid and o.oid='" + lblid.Text + "' and p.oid=o.oid and pitem.oid=p.oid and pitem.year='"+lblYear.Text+"' and pitem.year=p.year";
             //   query = "select * from tblsell s, tblsellitem sitem, tblowner o, tblcustomer c, tblsellproduct sp  where s.invoiceno=@invoiceno and s.invoiceno=sitem.invoiceno  and s.cid=c.cid  and s.cid=@customerid and sitem.spid=sp.spid and o.oid='" + lblid.Text + "' and s.oid=o.oid and sitem.oid=s.oid ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@dealerid", cmbDealer.SelectedValue);
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
                re.Load(Dealer1);
                con = c.openConnection();
                query = "select * from tblpurchase p, tblpurchaseitem pitem, tblowner o, tbldealer d, tblsellproduct s  where p.date between @date1 and @date2  and p.invoiceno=pitem.invoiceno  and p.did=@dealerid and  p.did=d.did  and pitem.spid=s.spid and o.oid='" + lblid.Text + "' and p.oid=o.oid and pitem.oid=p.oid and pitem.year='" + lblYear.Text + "' and pitem.year=p.year ";
             //   query = "select * from tblsell s, tblsellitem sitem, tblowner o, tblcustomer c, tblsellproduct sp  where s.date between @date1 and @date2 and   s.invoiceno=sitem.invoiceno  and s.cid=c.cid  and s.cid=@customerid and sitem.spid=sp.spid and o.oid='" + lblid.Text + "' and s.oid=o.oid and sitem.oid=s.oid ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@date1", datefrom.Value.ToString("dd-MM-yyyy"));
                cmd.Parameters.AddWithValue("@date2", dateto.Value.ToString("dd-MM-yyyy"));
                cmd.Parameters.AddWithValue("@dealerid", cmbDeal.SelectedValue);
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
               // MessageBox.Show(ee.Message);
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
                re.Load(Dealer1);
                con = c.openConnection();
                query = "select * from tblpurchase p, tblpurchaseitem pitem, tblowner o, tbldealer d, tblsellproduct s  where p.date between @date1 and @date2 and p.invoiceno=pitem.invoiceno   and  p.did=d.did  and pitem.spid=s.spid and o.oid='" + lblid.Text + "' and p.oid=o.oid and pitem.oid=p.oid and pitem.year='" + lblYear.Text + "' and pitem.year=p.year";
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

        private void FrmRptDealerPurchase_KeyDown(object sender, KeyEventArgs e)
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

        private void cmbDealer_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                query = "select count(did) from tbldealer where oid='" + lblid.Text + "' and dname='" + cmbDealer.Text + "'";
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
                    FrmRptDealerPurchase_Load(sender, e);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void cmbDeal_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                query = "select count(did) from tbldealer where oid='" + lblid.Text + "' and dname='" + cmbDeal.Text + "'";
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
                    FrmRptDealerPurchase_Load(sender, e);
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
                query = "select count(prid) from tblpurchase where oid='" + lblid.Text + "' and invoiceno='" + cmbInvoice.Text + "'";
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
                    FrmRptDealerPurchase_Load(sender, e);
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
