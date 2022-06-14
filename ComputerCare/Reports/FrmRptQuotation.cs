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
    public partial class FrmRptQuotation : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        int count,cust;
        string Quot = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptQuotn.rpt");
        string QuotGST = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptQuotnWithoutGST.rpt");
        string Quotation = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptQuotation.rpt");
        DataTable dt = new DataTable();
        public FrmRptQuotation()
        {
            InitializeComponent();
        }

        private void FrmRptQuotation_Load(object sender, EventArgs e)
        {
            getCompanyName();
            getCustomer();
            getCustomer1();
            getQno();
            cmbCustomer.Focus();
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

        private void getQno()
        {
            try
            {
                con = c.openConnection();
                cmbQuotation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbQuotation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbQuotation.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbQuotation.DataBindings.Clear();
                query = "select DISTINCT qno from tblquotation where oid='" + lblid.Text + " ' and cid='" + cmbCustomer.SelectedValue + "' and year='"+lblYear.Text+"'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("Customer");
                da.Fill(ds, "Customer");
                cmbQuotation.DataSource = ds;
                cmbQuotation.ValueMember = "Customer.qno";
                cmbQuotation.DisplayMember = "Customer.qno";
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbQuotation.DataSource = null;
            getQno();
        }

        private void btnShowHistory_Click(object sender, EventArgs e)
        {
            try
            {
                //getCompanyName();
                con = c.openConnection();
                   query = "select count(qid) q from tblquotation where qno=@qno and year='"+lblYear.Text+"' and oid='"+lblid.Text+"' and flag=1";
                SqlCommand cmd1 = new SqlCommand(query, con);
                 cmd1.Parameters.AddWithValue("@qno", cmbQuotation.Text);
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
                    re.Load(QuotGST);

                    query = "select * from tblquotation q, tblquotationitem qitem, tblowner o, tblcustomer c, tblsellproduct s  where q.qno=@qno and q.qno=qitem.qno  and q.cid=@Cid and q.cid=c.cid  and qitem.spid=s.spid and o.oid='" + lblid.Text + "' and q.oid=o.oid and qitem.oid=q.oid and qitem.year='" + lblYear.Text + "' and qitem.year=q.year ";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Cid", cmbCustomer.SelectedValue);
                    cmd.Parameters.AddWithValue("@qno", cmbQuotation.SelectedValue);
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
                    re.Load(Quot);

                    query = "select * from tblquotation q, tblquotationitem qitem, tblowner o, tblcustomer c, tblsellproduct s  where q.qno=@qno and q.qno=qitem.qno  and q.cid=@Cid and q.cid=c.cid  and qitem.spid=s.spid and o.oid='" + lblid.Text + "' and q.oid=o.oid and qitem.oid=q.oid and qitem.year='" + lblYear.Text + "' and qitem.year=q.year ";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Cid", cmbCustomer.SelectedValue);
                    cmd.Parameters.AddWithValue("@qno", cmbQuotation.SelectedValue);
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
                re.Load(Quotation);
                con = c.openConnection();
                query = "select * from tblquotation q, tblquotationitem qitem, tblowner o, tblcustomer c, tblsellproduct s  where q.date between @date1 and @date2  and q.qno=qitem.qno  and q.cid=@Cid and q.cid=c.cid  and qitem.spid=s.spid and o.oid='" + lblid.Text + "' and q.oid=o.oid and qitem.oid=q.oid ";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Cid", cmbCust.SelectedValue);
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

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {

                crystalReportViewer1.Visible = false;
                ReportDocument re = new ReportDocument();
                re.Load(Quotation);
                con = c.openConnection();
                query = "select * from tblquotation q, tblquotationitem qitem, tblowner o, tblcustomer c, tblsellproduct s  where q.date between @date1 and @date2  and q.qno=qitem.qno  and q.cid=c.cid  and qitem.spid=s.spid and o.oid='" + lblid.Text + "' and q.oid=o.oid and qitem.oid=q.oid ";

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

        private void FrmRptQuotation_KeyDown(object sender, KeyEventArgs e)
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
                    FrmRptQuotation_Load(sender, e);
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
                    FrmRptQuotation_Load(sender, e);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void cmbQuotation_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                query = "select count(qid) from tblquotation where oid='" + lblid.Text + "' and qno='" + cmbQuotation.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cust = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (cust == 0)
                {
                    MessageBox.Show("Quotation does not Exist. Please Select Quotation No. !!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmRptQuotation_Load(sender, e);
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
