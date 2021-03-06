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
    public partial class FrmRptSell : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        string Sell = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptSell.rpt");
        string SellGST = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptSellwithoutGST.rpt");
        DataTable dt = new DataTable();
        int billno,count;

        public FrmRptSell()
        {
            InitializeComponent();
        }

         public FrmRptSell(int billNo)
        {
            InitializeComponent();
            billno = billNo;
          //  user = utype;
        SellReport();
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

         private void SellReport()
         {
             try
             {
                 getCompanyName();
                 con = c.openConnection();
                query = "select count(sid) s from tblsell where invoiceno=@invoiceno and year='"+lblYear.Text+"' and oid='"+lblid.Text+"' and flag=1";
                SqlCommand cmd1 = new SqlCommand(query, con);
                 cmd1.Parameters.AddWithValue("@invoiceno", billno.ToString());
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
                    query = "select * from tblsell s, tblsellitem sitem, tblowner o, tblcustomer c, tblsellproduct sp  where s.invoiceno=@invoiceno and s.invoiceno=sitem.invoiceno  and s.cid=c.cid  and sitem.spid=sp.spid and o.oid='" + lblid.Text + "' and s.oid=o.oid and sitem.oid=s.oid and s.year='"+lblYear.Text+"' and s.year=sitem.year";

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
                else
                {
                    crystalReportViewer1.Visible = false;
                    ReportDocument re = new ReportDocument();
                    re.Load(Sell);
                    query = "select * from tblsell s, tblsellitem sitem, tblowner o, tblcustomer c, tblsellproduct sp  where s.invoiceno=@invoiceno and s.invoiceno=sitem.invoiceno  and s.cid=c.cid  and sitem.spid=sp.spid and o.oid='" + lblid.Text + "' and s.oid=o.oid and sitem.oid=s.oid and s.year='" + lblYear.Text + "' and s.year=sitem.year ";

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



        private void FrmRptSell_Load(object sender, EventArgs e)
        {

        }

        private void FrmRptSell_KeyDown(object sender, KeyEventArgs e)
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

        private void lblYear_Click(object sender, EventArgs e)
        {

        }
    }
}
