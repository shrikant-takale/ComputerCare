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
    public partial class FrmRptPurchase : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        string Purchase = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptPurchase.rpt");
        DataTable dt = new DataTable();
      string billno;
        public FrmRptPurchase()
        {
            InitializeComponent();
        }
           public FrmRptPurchase(string billNo)
        {
            InitializeComponent();
            billno = billNo;
          //  user = utype;
         PurchaseReport();
        }


        private void FrmRptPurchase_Load(object sender, EventArgs e)
        {

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

        private void PurchaseReport()
        {
            try
            {
                getCompanyName();
                crystalReportViewer1.Visible = false;
                ReportDocument re = new ReportDocument();
                re.Load(Purchase);
                con = c.openConnection();
                query = "select * from tblpurchase p, tblpurchaseitem pitem, tblowner o, tbldealer d, tblsellproduct s  where p.invoiceno=@invoiceno and p.invoiceno=pitem.invoiceno  and p.did=d.did  and pitem.spid=s.spid and o.oid='" + lblid.Text + "' and p.oid=o.oid and pitem.oid=p.oid  and p.year='" + lblYear.Text + "' and p.year=pitem.year ";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@invoiceno", billno.ToString());
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
               // MessageBox.Show(ee.Message);
            }
            finally
            {
                //con.Close();
                dt.Clear();
                dt.Dispose();
            }

        }

        private void FrmRptPurchase_KeyDown(object sender, KeyEventArgs e)
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
