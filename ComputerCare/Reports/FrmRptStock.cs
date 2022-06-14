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
    public partial class FrmRptStock : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        int cust;
        string Stock = Path.Combine(System.Windows.Forms.Application.StartupPath, "RptStock.rpt");
        DataTable dt = new DataTable();
        

        public FrmRptStock()
        {
            InitializeComponent();
        }

        private void FrmRptStock_Load(object sender, EventArgs e)
        {
            getCompanyName();
            getSellProduct();
        }

        private void getCompanyName()
        {
            try
            {
                con = c.openConnection();
                query = "select  name from tblsession";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    lblCompany.Text = sdr.GetValue(0).ToString();
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


        private void getSellProduct()
        {
            try
            {
                con = c.openConnection();
                cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbProduct.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbProduct.DataBindings.Clear();

                query = "select spid,sproduct from tblsellproduct where oid='" + lblid.Text + " ' ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("Product");
                da.Fill(ds, "Product");
                cmbProduct.DataSource = ds;
                cmbProduct.ValueMember = "Product.spid";
                cmbProduct.DisplayMember = "Product.sproduct";
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                crystalReportViewer1.Visible = false;
                ReportDocument re = new ReportDocument();
                re.Load(Stock);
                con = c.openConnection();
                query = "select * from tblsellproduct s,tblowner o where o.oid=s.oid and s.spid=@productid and o.oid='" + lblid.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@productid", cmbProduct.SelectedValue);
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

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                crystalReportViewer1.Visible = false;
                ReportDocument re = new ReportDocument();
                re.Load(Stock);
                con = c.openConnection();
                query = "select * from tblsellproduct s,tblowner o where o.oid=s.oid and o.oid='" + lblid.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
             //   cmd.Parameters.AddWithValue("@productid", cmbProduct.SelectedValue);
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

        private void FrmRptStock_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                }
                if (e.KeyCode == Keys.Escape)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you  want to Close this window ?", ""+cname+"", MessageBoxButtons.YesNo);
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

        private void cmbProduct_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                query = "select count(spid) from tblsellproduct where oid='" + lblid.Text + "' and sproduct='" + cmbProduct.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cust = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (cust == 0)
                {
                    MessageBox.Show("Product does not Exist. Please Register Product !!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmRptStock_Load(sender, e);
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
