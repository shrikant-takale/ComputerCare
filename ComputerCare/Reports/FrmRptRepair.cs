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
    public partial class FrmRptRepair : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        string Repair = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptRepair.rpt");
        string RepairGST = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptRepairwithoutGST.rpt");
        DataTable dt = new DataTable();
        int billno,count;

        public FrmRptRepair()
        {
            InitializeComponent();
        }
           public FrmRptRepair(int billNo)
        {
            InitializeComponent();
            billno = billNo;
          //  user = utype;
           RepairReport();
        }

        private void FrmRptRepair_Load(object sender, EventArgs e)
        {

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

        private void RepairReport()
        {
            try
            {
                getCompanyName();
                 con = c.openConnection();
                 query = "select count(rid) s from tblrepair where invoiceno=@invoiceno and year='"+lblYear.Text+"' and oid='"+lblid.Text+"' and flag=1 and status=0";
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
                    re.Load(RepairGST);

                    query = "select * from tblrepair r, tblrepairitem ritem, tblowner o, tblcustomer c, tblrepairproduct p where r.invoiceno=@invoiceno and r.invoiceno=ritem.invoiceno and r.cid=c.cid and p.rpid=ritem.rpid and r.oid='" + lblid.Text + "' and r.oid=c.oid  and  ritem.oid=r.oid and  r.oid=p.oid and r.year='"+lblYear.Text+"' and r.year=ritem.year and status=0";

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
                    re.Load(Repair);

                    query = "select * from tblrepair r, tblrepairitem ritem, tblowner o, tblcustomer c, tblrepairproduct p where r.invoiceno=@invoiceno and r.invoiceno=ritem.invoiceno and r.cid=c.cid and p.rpid=ritem.rpid and r.oid='" + lblid.Text + "' and r.oid=c.oid  and  ritem.oid=r.oid and  r.oid=p.oid and r.year='" + lblYear.Text + "' and r.year=ritem.year and status=0";

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
                //MessageBox.Show(ee.Message);
            }
            finally
            {
                //con.Close();
                dt.Clear();
                dt.Dispose();
            }

        }

        private void FrmRptRepair_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmRptRepair_KeyDown_1(object sender, KeyEventArgs e)
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
