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


namespace ComputerCare.Masters
{
    public partial class FrmDealer : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        int i, id;
        public FrmDealer()
        {
            InitializeComponent();
        }

        private void FrmDealer_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmDealer_Load(object sender, EventArgs e)
        {
            try
            {
                getCompanyName();
                getDealerInfo();
                btnUpdate.Enabled = false;
                btnSave.Enabled = true;
                txtDealerName.Focus();
                // FrmDealer d = new FrmDealer();
                // d.Text = cname;
            }
            catch (Exception ee)
            {
            }
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

        private void getDealerInfo()
        {
            try
            {
                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.Update(); // In case there is databinding
                listView1.Refresh(); // Redraw items

                listView1.View = View.Details;
                listView1.GridLines = true;
                listView1.FullRowSelect = true;


                listView1.Columns.Add("Dealer ID", 0);
                listView1.Columns.Add("Dealer Name", 200);
                listView1.Columns.Add("Contact No.", 100);
                listView1.Columns.Add("Address", 200);
                listView1.Columns.Add("Opening Credit", 150);


                int i;

                con = c.openConnection();

                DataTable dt = new DataTable();
                string[] arr = new string[14];
                ListViewItem itm;
                query = "select * from tbldealer  where oid='" + lblid.Text + "'";
                SqlCommand cmd1 = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(dt);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    arr[0] = Convert.ToString(dt.Rows[i]["did"]);
                    arr[1] = Convert.ToString(dt.Rows[i]["dname"]);
                    arr[2] = Convert.ToString(dt.Rows[i]["dcontact"]);
                    arr[3] = Convert.ToString(dt.Rows[i]["daddress"]);
                    arr[4] = Convert.ToString(dt.Rows[i]["dopcredit"]);


                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                }
            }
            catch (Exception ee)
            {

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDealerName.Text == "")
                {
                    MessageBox.Show("Please Enter Dealer Name", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDealerName.Focus();
                }
                else if (txtContactNo.Text == "")
                {
                    MessageBox.Show("Please Enter Contact No.", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContactNo.Focus();
                }
                else if (txtAddress.Text == "")
                {
                    MessageBox.Show("Please Enter Address", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAddress.Focus();
                }
                else if (txtCredit.Text == "")
                {
                    MessageBox.Show("Please Enter Opening Credit", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCredit.Focus();
                }

                else if (checkDealer())
                {
                    insertDealer();
                    FrmDealer_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Dealer Name already Exist! Please Enter another Dealer!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FrmDealer_Load(sender, e);
                    txtDealerName.Focus();
                }
            }
            catch (Exception ee)
            {
            }
        }

        private bool checkDealer()
        {
            try
            {
                int count = 0;
                con = c.openConnection();
                query = "select count(did) from tbldealer where dname='" + txtDealerName.Text + "'";
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

        private void insertDealer()
        {
            try
            {
                con = c.openConnection();
                query = "insert into tbldealer (oid,dname,dcontact,daddress,dopcredit) values (@oid,@dname,@dcontact,@daddress,@dopcredit)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@oid", lblid.Text);
                cmd.Parameters.AddWithValue("@dname", txtDealerName.Text);
                cmd.Parameters.AddWithValue("@dcontact", txtContactNo.Text);
                cmd.Parameters.AddWithValue("@daddress", txtAddress.Text);
                cmd.Parameters.AddWithValue("@dopcredit", txtCredit.Text);


                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Dealer Information Inserted Successfully!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearTexts();
                }
            }
            catch (Exception ee)
            {
            }
        }

        private void clearTexts()
        {
            txtContactNo.Text = "";
            txtAddress.Text = "";
            txtCredit.Text = "";
            txtDealerName.Text = "";
            txtDealerName.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmDealer_Load(sender,e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                txtDealerName.Focus();
                con = c.openConnection();

                if (listView1.SelectedItems.Count == 1)
                {
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                    id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                    query = "select * from tbldealer where did='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {

                        txtDealerName.Text = Convert.ToString(sdr.GetValue(1));
                        txtContactNo.Text = sdr.GetValue(2).ToString();
                        txtAddress.Text = sdr.GetValue(3).ToString();
                        txtCredit.Text = sdr.GetValue(4).ToString();


                    }
                    sdr.Close();
                }
                else
                {
                    MessageBox.Show("Please select atleast one record to Update", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ee)
            {
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDealerName.Text == "")
                {
                    MessageBox.Show("Please Enter Dealer Name", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDealerName.Focus();
                }
                else if (txtContactNo.Text == "")
                {
                    MessageBox.Show("Please Enter Contact No.", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContactNo.Focus();
                }
                else if (txtAddress.Text == "")
                {
                    MessageBox.Show("Please Enter Address", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAddress.Focus();
                }
                else if (txtCredit.Text == "")
                {
                    MessageBox.Show("Please Enter Opening Credit", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCredit.Focus();
                }
                else
                {
                    con = c.openConnection();
                    id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                    query = "update tbldealer set dname=@dname, dcontact=@dcontact,daddress=@daddress,dopcredit=@credit where did='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@dname", txtDealerName.Text);
                    cmd.Parameters.AddWithValue("@dcontact", txtContactNo.Text);
                    cmd.Parameters.AddWithValue("@daddress", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@credit", txtCredit.Text);

                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Dealer Information Updated Successfully!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearTexts();
                        FrmDealer_Load(sender, e);
                    }
                }
            }
            catch (Exception ee)
            {
              //  MessageBox.Show(ee.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();

                if (listView1.SelectedItems.Count == 1)
                {
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                    id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                    query = "delete from tbldealer where did='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Dealer Information Deleted Successfully!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmDealer_Load(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Please select atleast one record to delete", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ee)
            {
            }
        }
    }
}
