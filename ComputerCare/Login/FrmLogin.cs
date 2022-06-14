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
using ComputerCare.Masters;


namespace ComputerCare.Login
{
    public partial class FrmLogin : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query;
        int count = 0, cnt = 0;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            getCompany();
            getC();
            cmbYear.Focus();
            cmbYear.SelectedIndex = 0;
            cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cmbYear.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbYear.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cmbCompany.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbCompany.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void lnkNewCompany_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FrmCompany cmp = new FrmCompany();
            cmp.ShowDialog();
        }

        private void getC()
        {
            try
            {
                con = c.openConnection();
                query = "select count(oid) o from tblowner";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    count = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (count > 0)
                {
                    lnkNewCompany.Visible = false;
                }
                else
                {
                    lnkNewCompany.Visible = true;
                }
            }
            catch (Exception ee)
            {
            }
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text == "")
                {
                    MessageBox.Show("Please Enter Password!!!", "" + cmbCompany.Text + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    con = c.openConnection();
                    query = "select count(userid) from tbllogin where username='" + txtUsername.Text + "' and password='" + txtPassword.Text + "' and oid='" + cmbCompany.SelectedValue + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        count = Convert.ToInt32(sdr.GetValue(0));
                    }
                    sdr.Close();
                    if (count > 0)
                    {
                        query = "select count(sessid) from tblsession";
                        SqlCommand cmd1 = new SqlCommand(query, con);
                        SqlDataReader sdr1 = cmd1.ExecuteReader();
                        if (sdr1.Read())
                        {
                            cnt = Convert.ToInt32(sdr1.GetValue(0));
                        }
                        sdr1.Close();
                        if (cnt > 0)
                        {
                            query = "update tblsession set oid='" + cmbCompany.SelectedValue + "',name='" + cmbCompany.Text + "', year='" + cmbYear.Text + "'";
                            SqlCommand cmd2 = new SqlCommand(query, con);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("You are Logged in successfully!!!", "" + cmbCompany.Text + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            ComputerCare comp = new ComputerCare();
                            comp.ShowDialog();
                        }
                        else
                        {
                            query = "insert into tblsession (oid,name,year) values ('" + cmbCompany.SelectedValue + "','" + cmbCompany.Text + "','" + cmbYear.Text + "')";
                            SqlCommand cmd2 = new SqlCommand(query, con);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("You are Logged in successfully!!!", "" + cmbCompany.Text + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            ComputerCare comp = new ComputerCare();
                            comp.ShowDialog();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Invalid Login Details", "" + cmbCompany.Text + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPassword.Focus();
                    }
                }
            }
            catch (Exception ee)
            {
            }
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                }
                if (e.KeyCode == Keys.Escape)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you  want to Close this window ?", "" + cmbCompany.Text + "", MessageBoxButtons.YesNo);
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

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }




    }
}
