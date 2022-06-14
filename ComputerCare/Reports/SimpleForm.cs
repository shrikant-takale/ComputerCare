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
    public partial class SimpleForm : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query;
        string productStock = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptProductStock.rpt");
        
        DataTable dt = new DataTable();
        int count;

        public SimpleForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                crystalReportViewer1.Visible = false;
                ReportDocument re = new ReportDocument();
                re.Load(productStock);
                query = "select * from tblsellproduct";

                SqlCommand cmd = new SqlCommand(query, con);
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
