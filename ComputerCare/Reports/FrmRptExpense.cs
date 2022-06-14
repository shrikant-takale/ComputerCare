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
    public partial class FrmRptExpense : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        string Expense = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptExpense.rpt");
        DataTable dt = new DataTable();


        public FrmRptExpense()
        {
            InitializeComponent();
        }

        private void FrmRptExpense_Load(object sender, EventArgs e)
        {
            getCompanyName();
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

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                crystalReportViewer1.Visible = false;
                ReportDocument re = new ReportDocument();
                re.Load(Expense);
                con = c.openConnection();
                query = "select * from tblexp exp,tblexpitem eitem, tblowner o where exp.expno=eitem.expno and exp.date between @date1 and @date2 and exp.oid=eitem.oid and  o.oid='" + lblid.Text + "' and o.oid=eitem.oid and eitem.year='"+lblYear.Text+"' and eitem.year=exp.year ";
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

        private void FrmRptExpense_KeyDown(object sender, KeyEventArgs e)
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
    }
}
