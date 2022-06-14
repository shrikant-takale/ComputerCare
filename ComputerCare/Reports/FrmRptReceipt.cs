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
    public partial class FrmRptReceipt : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        string Customer = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptCustomerReceipt.rpt");
        string Dealer = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptDealerReceipt.rpt");
        DataTable dt = new DataTable();
        int billno;
        string user;

        public FrmRptReceipt()
        {
            InitializeComponent();
        }
          public FrmRptReceipt(int billNo, string utype)
        {
            InitializeComponent();
            billno = billNo;
            user = utype;
            invoice();
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
          private void invoice()
          {
              try
              {
                  getCompanyName();
                  if (user == "Customer Receipt")
                  {
                      crystalReportViewer1.Visible = false;
                      ReportDocument re = new ReportDocument();
                      re.Load(Customer);
                      con = c.openConnection();
                      query = "select * from tblreceipt r,tblcustomer c,tblowner o where r.userid=c.cid and r.oid=o.oid and r.receiptno=@sellno and o.oid='" + lblid.Text + "' and r.year='"+lblYear.Text+"'";
                      SqlCommand cmd = new SqlCommand(query, con);
                      cmd.Parameters.AddWithValue("@sellno", billno.ToString());
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
                  else if (user == "Dealer Receipt")
                  {
                      crystalReportViewer1.Visible = false;
                      ReportDocument re = new ReportDocument();
                      re.Load(Dealer);
                      con = c.openConnection();
                      query = "select * from tblreceipt r,tbldealer d,tblowner o where r.userid=d.did and r.receiptno=@sellno and o.oid=r.oid and o.oid='" + lblid.Text + "' and r.year='" + lblYear.Text + "'";
                      SqlCommand cmd = new SqlCommand(query, con);
                      cmd.Parameters.AddWithValue("@sellno", billno.ToString());
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


        private void FrmRptReceipt_Load(object sender, EventArgs e)
        {

        }

        private void FrmRptReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                }
                if (e.KeyCode == Keys.Escape)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you  want to Close this window ?", "ERP System", MessageBoxButtons.YesNo);
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
