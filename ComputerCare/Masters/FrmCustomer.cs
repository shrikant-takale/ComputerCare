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
    public partial class FrmCustomer : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        int i, id;

        public FrmCustomer()
        {
            InitializeComponent();
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                getCompanyName();
                getCustomerInfo();
                btnUpdate.Enabled = false;
                btnSave.Enabled = true;
                txtCustomerName.Focus();
                // FrmDealer d = new FrmDealer();
                // d.Text = cname;
            }
            catch (Exception ee)
            {
            }
        }

        private void FrmCustomer_KeyDown(object sender, KeyEventArgs e)
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

        private void getCustomerInfo()
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


                listView1.Columns.Add("Customer id", 0);
                listView1.Columns.Add("Customer Name", 180);
                listView1.Columns.Add("Contact No.", 100);
                listView1.Columns.Add("Address",200);
                listView1.Columns.Add("Opening Credit",150);
                listView1.Columns.Add("GST No.", 100);
               

                int i;

                con = c.openConnection();

                DataTable dt = new DataTable();
                string[] arr = new string[14];
                ListViewItem itm;
                query = "select * from tblcustomer  where oid='" + lblid.Text + "'";
                SqlCommand cmd1 = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(dt);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    arr[0] = Convert.ToString(dt.Rows[i]["cid"]);
                    arr[1] = Convert.ToString(dt.Rows[i]["cname"]);
                    arr[2] = Convert.ToString(dt.Rows[i]["ccontact"]);
                    arr[3] = Convert.ToString(dt.Rows[i]["caddress"]);
                    arr[4] = Convert.ToString(dt.Rows[i]["copcredit"]);
                    arr[5] = Convert.ToString(dt.Rows[i]["gstno"]);

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
                if (txtCustomerName.Text == "")
                {
                    MessageBox.Show("Please Enter Customer Name", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCustomerName.Focus();
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
                else if (checkCustomer())
                {
                    insertCustomer();
                    FrmCustomer_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Customer Name already Exist! Please Enter another Customer!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FrmCustomer_Load(sender, e);
                    txtCustomerName.Focus();
                }
            }
            catch (Exception ee)
            {
            }
        }

        private bool checkCustomer()
        {
            try
            {
                int count = 0;
                con = c.openConnection();
                query = "select count(cid) from tblcustomer where cname='" + txtCustomerName.Text + "'";
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

        private void insertCustomer()
        {
            try
            {
                con = c.openConnection();
                query = "insert into tblcustomer (oid,cname,ccontact,caddress,copcredit,gstno) values (@oid,@cname,@ccontact,@caddress,@copcredit,@gstno)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@oid", lblid.Text);
                cmd.Parameters.AddWithValue("@cname", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@ccontact", txtContactNo.Text);
                cmd.Parameters.AddWithValue("@caddress", txtAddress.Text);
                cmd.Parameters.AddWithValue("@copcredit", txtCredit.Text);
                cmd.Parameters.AddWithValue("@gstno", txtGST.Text);

                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Customer Information Inserted Successfully!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtCredit.Text = "0";
            txtCustomerName.Text = "";
            txtGST.Text = "";
            txtCustomerName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                txtCustomerName.Focus();
                con = c.openConnection();

                if (listView1.SelectedItems.Count == 1)
                {
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                    id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                    query = "select * from tblcustomer where cid='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                       
                        txtCustomerName.Text = Convert.ToString(sdr.GetValue(1));
                        txtContactNo.Text = sdr.GetValue(2).ToString();
                        txtAddress.Text = sdr.GetValue(3).ToString();
                        txtCredit.Text = sdr.GetValue(4).ToString();
                        txtGST.Text = sdr.GetValue(5).ToString();

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
                if (txtCustomerName.Text == "")
                {
                    MessageBox.Show("Please Enter Customer Name", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCustomerName.Focus();
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
                    query = "update tblcustomer set cname=@cname, ccontact=@ccontact,caddress=@caddress,copcredit=@credit, gstno=@gst  where cid='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                   
                    cmd.Parameters.AddWithValue("@cname", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@ccontact", txtContactNo.Text);
                    cmd.Parameters.AddWithValue("@caddress", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@credit", txtCredit.Text);
                    cmd.Parameters.AddWithValue("@gst", txtGST.Text);
                    
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Customer Information Updated Successfully!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearTexts();
                        FrmCustomer_Load(sender, e);
                    }
                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message);
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
                    query = "delete from tblcustomer where cid='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Customer Information Deleted Successfully!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmCustomer_Load(sender, e);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmCustomer_Load(sender,e);
        }


    }
}
