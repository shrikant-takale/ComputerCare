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

namespace ComputerCare.Masters
{
    public partial class FrmCompany : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query;
        int i, maxid;
        public FrmCompany()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked == true)
            {
                txtPassowrd.PasswordChar = '\0';
            }
            else
            {
                txtPassowrd.PasswordChar = '*';
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCompany.Text == "")
                {
                    MessageBox.Show("Please Enter Company. ", "Computer Care", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCompany.Focus();
                }
                else if (txtAddress.Text == "")
                {
                    MessageBox.Show("Please Enter Address", "Computer Care", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAddress.Focus();
                }
                else if (txtContactNo.Text == "")
                {
                    MessageBox.Show("Please Enter Contact No..", "Computer Care", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContactNo.Focus();
                }
                else if (txtOwnerName.Text == "")
                {
                    MessageBox.Show("Please Enter Owner Name", "Computer Care", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContactNo.Focus();
                }
                else if (txtUsername.Text == "")
                {
                    MessageBox.Show("Please Enter Username. ", "Computer Care", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Focus();
                }
                else if (txtPassowrd.Text == "")
                {
                    MessageBox.Show("Please Enter Password.", "Computer Care", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassowrd.Focus();
                }
                else if (checkCompnay())
                {
                    insertCompany();
                    deleteCompany();
                }
            }
            catch (Exception ee)
            {
            }
        }

        private void deleteCompany()
        {
            throw new NotImplementedException();
        }

        private void insertCompany()
        {
            try
            {
                con = c.openConnection();
                query = "insert into tblowner (name,addr,oname,contact,email,ogst,username,password,website,ocontact) values (@name,@addr,@oname,@contact,@email,@gst,@username,@password,@website,@ocontact)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", txtCompany.Text);
                cmd.Parameters.AddWithValue("@addr", txtAddress.Text);
                cmd.Parameters.AddWithValue("@oname",txtOwnerName.Text);
                cmd.Parameters.AddWithValue("@contact", txtContactNo.Text);
                cmd.Parameters.AddWithValue("@ocontact", txtContactNo.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@gst", txtGST.Text);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassowrd.Text);
                cmd.Parameters.AddWithValue("@website", txtWeb.Text);
               
                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    query = "select max(oid) from tblowner";
                    SqlCommand cmd3 = new SqlCommand(query, con);
                    SqlDataReader sdr = cmd3.ExecuteReader();
                    if (sdr.Read())
                    {
                        maxid = Convert.ToInt32(sdr.GetValue(0));
                    }
                    sdr.Close();

                    query = "insert into tbllogin (oid,username,password) values (@oid,@username,@password)";
                    SqlCommand cmd2 = new SqlCommand(query, con);
                    cmd2.Parameters.AddWithValue("@oid", maxid);
                    cmd2.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd2.Parameters.AddWithValue("@password", txtPassowrd.Text);
                    i = cmd2.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("You are registered Successfully Your Username is " + txtUsername.Text + " and Your Password is " + txtPassowrd.Text, "Computer Care", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        FrmLogin l = new FrmLogin();
                        l.Show();
                    }
                }
            }
            catch (Exception ee)
            {
            }
        }

        private bool checkCompnay()
        {
            try
            {
                int count = 0;
                con = c.openConnection();
                query = "select count(oid) from tblowner where name='" + txtCompany.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    count = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (count > 0)
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ee)
            {
                return false;
            }
        }

        private void txtGST_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                }
                if (e.KeyCode == Keys.Escape)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you  want to Close this window ?", "Computer Care", MessageBoxButtons.YesNo);
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

        private void FrmCompany_Load(object sender, EventArgs e)
        {

        }

        private void FrmCompany_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                }
                if (e.KeyCode == Keys.Escape)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you  want to Close this window ?", "Computer Care", MessageBoxButtons.YesNo);
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

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
