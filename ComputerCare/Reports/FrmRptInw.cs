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
    public partial class FrmRptInw : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        string Inword1 = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptInword.rpt");
        DataTable dt = new DataTable();
        int billno;
        public FrmRptInw()
        {
            InitializeComponent();
        }

        private void FrmRptInw_Load(object sender, EventArgs e)
        {

        }

         public FrmRptInw(int billNo)
        {
            InitializeComponent();
            billno = billNo;
          //  user = utype;
           Inword();

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

         private void Inword()
         {
             try
             {
                 getCompanyName();
                 crystalReportViewer1.Visible = false;
                 ReportDocument re = new ReportDocument();
                 re.Load(Inword1);
                 con = c.openConnection();
                 query = "select * from tblInword i, tblinworditem item, tblowner o, tblcustomer c, tblrepairproduct r, tblproblem p   where i.inwordno=@inwordno and i.inwordno=item.inwordno and item.pid=p.pid  and i.cid=c.cid  and item.rpid=r.rpid and o.oid='" + lblid.Text + "' and i.oid=o.oid and item.oid=i.oid and item.year='"+lblYear.Text+"' and item.year=i.year";
                 
                 SqlCommand cmd = new SqlCommand(query, con);
                 cmd.Parameters.AddWithValue("@inwordno", billno.ToString());
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

         private void FrmRptInw_KeyDown(object sender, KeyEventArgs e)
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
