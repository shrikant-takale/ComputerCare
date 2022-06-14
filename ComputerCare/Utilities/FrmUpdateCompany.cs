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

namespace ComputerCare.Utilities
{
    public partial class FrmUpdateCompany : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        int i;

        public FrmUpdateCompany()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmUpdateCompany_Load(object sender, EventArgs e)
        {
            getID();
            getInfo();
            txtCompany.Focus();
        }

        private void getID()
        {
            try
            {
                con = c.openConnection();
                query = "select  name from tblsession";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
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

        private void getInfo()
        {
            try
            {
                con = c.openConnection();
                query = "select * from tblowner where oid='" + lblid.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    txtCompany.Text = sdr.GetValue(1).ToString();
                    txtAddress.Text = sdr.GetValue(2).ToString();
                    txtOwnerName.Text = sdr.GetValue(3).ToString();
                    txtContactNo.Text = sdr.GetValue(4).ToString();
                    txtEmail.Text = sdr.GetValue(5).ToString();
                    txtLicense.Text = sdr.GetValue(6).ToString();
                    txtWeb.Text = sdr.GetValue(7).ToString();
                 

                }
                sdr.Close();
            }
            catch (Exception ee)
            {
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCompany.Text == "")
                {
                    MessageBox.Show("Please Enter Society Name. ", "Computer Care", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCompany.Focus();
                }
                else if (txtAddress.Text == "")
                {
                    MessageBox.Show("Please Enter Address", "Computer Care", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAddress.Focus();
                }
                else if (txtContactNo.Text == "")
                {
                    MessageBox.Show("Please Enter Contact No.", "Computer Care", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContactNo.Focus();
                }
                else if (txtOwnerName.Text == "")
                {
                    MessageBox.Show("Please Enter Owner Name", "Computer Care", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAddress.Focus();
                }
                else if (txtEmail.Text == "")
                {
                    MessageBox.Show("Please Enter Email", "Computer Care", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Focus();
                }
               
                else
                {
                    con = c.openConnection();
                    query = "update tblowner set name=@name,addr=@addr,oname=@oname,contact=@contact,email=@email,ogst=@license,website=@website,ocontact=@ocontact where oid='" + lblid.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@name", txtCompany.Text);
                    cmd.Parameters.AddWithValue("@addr", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@oname", txtOwnerName.Text);
                    cmd.Parameters.AddWithValue("@contact", txtContactNo.Text);
                    cmd.Parameters.AddWithValue("@ocontact", txtContactNo.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@license", txtLicense.Text);
                    cmd.Parameters.AddWithValue("@website", txtWeb.Text);
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Your registered Company updated Successfully. Your Session was expire Restart Your Product", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void FrmUpdateCompany_KeyDown(object sender, KeyEventArgs e)
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
