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
    public partial class FrmRptInward : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        int count,cust;
        string Inward = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptInword.rpt");
        string Inward1 = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptInwordReport.rpt");
        DataTable dt = new DataTable();
  

        public FrmRptInward()
        {
            InitializeComponent();
        }

        private void FrmRptInward_Load(object sender, EventArgs e)
        {
            getCompanyName();
            getCustomer();
            getInwardNo();
            cmbCustomer.Focus();
        }

        private void getCompanyName()
        {
            try
            {
                con = c.openConnection();
                query = "select  name,year  from tblsession";
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

     

        private void getInwardNo()
        {
            try
            {
                con = c.openConnection();
                cmbInward.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbInward.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbInward.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbInward.DataBindings.Clear();
                query = "select DISTINCT inwordno from tblinword where oid='" + lblid.Text + " ' and cid='" + cmbCustomer.SelectedValue + "' and year='"+lblYear.Text+"'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("Customer");
                da.Fill(ds, "Customer");
                cmbInward.DataSource = ds;
                cmbInward.ValueMember = "Customer.inwordno";
                cmbInward.DisplayMember = "Customer.inwordno";
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbInward.DataSource=null;
            getInwardNo();
        }

        private void btnShowHistory_Click(object sender, EventArgs e)
        {
            try
            {
                crystalReportViewer1.Visible = false;
                ReportDocument re = new ReportDocument();
                re.Load(Inward);
                con = c.openConnection();
                query = "select * from tblinword i, tblinworditem item, tblowner o, tblcustomer c, tblrepairproduct rp,tblproblem p  where i.inwordno=@inwardno and i.inwordno=item.inwordno  and i.cid=c.cid  and i.cid=@customerid and item.rpid=rp.rpid and item.pid=p.pid and o.oid='" + lblid.Text + "' and i.oid=o.oid and item.oid=i.oid  and i.year='"+lblYear.Text+"' and i.year=item.year";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@customerid", cmbCustomer.SelectedValue);
                cmd.Parameters.AddWithValue("@inwardno", cmbInward.SelectedValue);
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
              //  MessageBox.Show(ee.Message);
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
                re.Load(Inward);
                con = c.openConnection();
                query = "select * from tblinword i, tblinworditem item, tblowner o, tblcustomer c, tblrepairproduct rp,tblproblem p  where item.servicetagno=@service and i.inwordno=item.inwordno and item.pid=p.pid  and i.cid=c.cid  and item.rpid=rp.rpid and o.oid='" + lblid.Text + "' and i.oid=o.oid and item.oid=i.oid and i.year='" + lblYear.Text + "' and i.year=item.year";
                SqlCommand cmd = new SqlCommand(query, con);
               // cmd.Parameters.AddWithValue("@customerid", cmbCustomer.SelectedValue);
                cmd.Parameters.AddWithValue("@service",txtServiceTagNo.Text);
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

        private void FrmRptInward_KeyDown(object sender, KeyEventArgs e)
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

        private void txtServiceTagNo_Leave(object sender, EventArgs e)
        {
            try
            {
                 con = c.openConnection();
                 if (txtServiceTagNo.Text == "")
                 {
                 }
                 else
                 {
                     query = "select * from tblinworditem item  where item.oid='" + lblid.Text + "' and item.servicetagno='" + txtServiceTagNo.Text + "' and year='" + lblYear.Text + "'";
                     SqlCommand cmd = new SqlCommand(query, con);
                     SqlDataReader sdr = cmd.ExecuteReader();
                     while (sdr.Read())
                     {
                         count = Convert.ToInt32(sdr.GetValue(0));
                     }
                     sdr.Close();
                     if (count > 0)
                     {
                     }
                     else
                     {
                         MessageBox.Show("Service Tag  No. does not exist. Please Enter Correct Service Tag  No.", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         txtServiceTagNo.Text = "";
                         txtServiceTagNo.Focus();
                     }
                 }
            }
            catch(Exception ex)
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
                    FrmRptInward_Load(sender, e);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void cmbInward_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                query = "select count(inwid) from tblinword where oid='" + lblid.Text + "' and inwordno='" + cmbInward.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cust = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (cust == 0)
                {
                    MessageBox.Show("Inward does not Exist. Please Select Inward No. !!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmRptInward_Load(sender, e);
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
