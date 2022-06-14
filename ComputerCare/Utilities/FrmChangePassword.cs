using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ComputerCare.Connections;
using ComputerCare.Login;

namespace ComputerCare.Utilities
{
    public partial class FrmChangePassword : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query;
        int i = 0;

        public FrmChangePassword()
        {
            InitializeComponent();
        }

        private void FrmChangePassword_Load(object sender, EventArgs e)
        {
            getCompany();
            txtPassword.Focus();
            
        }
        private void getCompany()
        {
            try
            {
                con = c.openConnection();
                cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbCompany.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbCompany.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbCompany.DataBindings.Clear();
                query = "select oid,name from tblowner";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet("Company");

                da.Fill(ds, "Company");
                cmbCompany.DataSource = ds;
                cmbCompany.ValueMember = "Company.oid";
                cmbCompany.DisplayMember = "Company.name";

            }
            catch (Exception ee)
            {

            }
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            getUsername();
        }

        private void getUsername()
        {
            try
            {
                con = c.openConnection();
                query = "select username from tblowner where oid=@oid";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@oid", cmbCompany.SelectedValue);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    txtUsername.Text = sdr.GetValue(0).ToString();
                }
                sdr.Close();
            }
            catch (Exception ee)
            {
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "";
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text == "")
                {
                    MessageBox.Show("Please Enter Password!!!. ", "ERP System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                }
                else
                {

                    con = c.openConnection();
                    query = "update tbllogin set password='" + txtPassword.Text + "' where oid='" + cmbCompany.SelectedValue + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Password updated Successfully!!!! Your Session was expire Restart Your Product", "Computer Care", MessageBoxButtons.OK, MessageBoxIcon.Information);

                       
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Please Enter Correct Company Name!!!!", "Computer Care", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ee)
            {
            }
        }

        private void FrmChangePassword_KeyDown(object sender, KeyEventArgs e)
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
