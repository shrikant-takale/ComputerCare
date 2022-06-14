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
    public partial class FrmRptTransaction : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        string TrialBalance = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptTransaction.rpt");
        DataTable dt = new DataTable();
   

        public FrmRptTransaction()
        {
            InitializeComponent();
        }

        private void FrmRptTransaction_Load(object sender, EventArgs e)
        {
            getCompanyName();
            rbSales.Checked = true;
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

        private void FrmRptTransaction_KeyDown(object sender, KeyEventArgs e)
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

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {

                if (rbSales.Checked == true)
                {
                    crystalReportViewer1.Visible = false;
                    ReportDocument re = new ReportDocument();
                    re.Load(TrialBalance);
                    con = c.openConnection();
                    query = "select * from tbltransaction s,tblowner o where date between @date1 and @date2 and s.oid=o.oid and o.oid='" + lblid.Text + "'  and s.year='" + lblYear.Text + "' and type='Sales Account'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@date1", dtFrom.Value.ToString("dd-MM-yyyy"));
                    cmd.Parameters.AddWithValue("@date2", dtTo.Value.ToString("dd-MM-yyyy"));
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
                    re.SetParameterValue("fromdate", dtFrom.Text);
                    re.SetParameterValue("todate", dtTo.Text);
                    crystalReportViewer1.ReportSource = re;
                }
                else if (rbRepair.Checked == true)
                {
                    crystalReportViewer1.Visible = false;
                    ReportDocument re = new ReportDocument();
                    re.Load(TrialBalance);
                    con = c.openConnection();
                    query = "select * from tbltransaction s,tblowner o where date between @date1 and @date2 and s.oid=o.oid and o.oid='" + lblid.Text + "'  and s.year='" + lblYear.Text + "' and type='Repair Account'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@date1", dtFrom.Value.ToString("dd-MM-yyyy"));
                    cmd.Parameters.AddWithValue("@date2", dtTo.Value.ToString("dd-MM-yyyy"));
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
                    re.SetParameterValue("fromdate", dtFrom.Text);
                    re.SetParameterValue("todate", dtTo.Text);
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

    }
}
