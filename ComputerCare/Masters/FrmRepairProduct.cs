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
    public partial class FrmRepairProduct : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        int i, id;

        public FrmRepairProduct()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmRepairProduct_Load(sender,e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRepairProduct_Load(object sender, EventArgs e)
        {
            try
            {
                getCompanyName();
                getRepairProductInfo();
                btnUpdate.Enabled = false;
                btnSave.Enabled = true;
              txtProduct.Focus();
                // FrmDealer d = new FrmDealer();
                // d.Text = cname;
            }
            catch (Exception ee)
            {
            }
        }

        private void FrmRepairProduct_KeyDown(object sender, KeyEventArgs e)
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

       

        private void getRepairProductInfo()
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


                listView1.Columns.Add("Repair Product id", 0);
                listView1.Columns.Add("Product Name", 300);
                listView1.Columns.Add("Description", 300);

                int i;

                con = c.openConnection();

                DataTable dt = new DataTable();
                string[] arr = new string[14];
                ListViewItem itm;
                query = "select * from tblrepairproduct  where oid='" + lblid.Text + "'";
                SqlCommand cmd1 = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(dt);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    arr[0] = Convert.ToString(dt.Rows[i]["rpid"]);
                    arr[1] = Convert.ToString(dt.Rows[i]["rproduct"]);
                    arr[2] = Convert.ToString(dt.Rows[i]["description"]);

                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                }
            }
            catch (Exception ee)
            {
               // MessageBox.Show(ee.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProduct.Text == "")
                {
                    MessageBox.Show("Please Enter Product Name", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProduct.Focus();
                }
                 
                else  if (checkRepairProduct())
                {
                    insertRepairProduct();
                    FrmRepairProduct_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Product Name  already Exist! Please Enter another Product!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FrmRepairProduct_Load(sender, e);
                    txtProduct.Focus();
                }
            }
            catch (Exception ee)
            {
            }
        }
        private bool checkRepairProduct()
        {
            try
            {
                int count = 0;
                con = c.openConnection();
                query = "select count(rpid) from tblrepairproduct where rproduct='" + txtProduct.Text + "'";
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

        private void insertRepairProduct()
        {
            try
            {
                con = c.openConnection();
                query = "insert into tblrepairproduct (oid,rproduct,description) values (@oid,@rproduct,@description)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@oid", lblid.Text);
                cmd.Parameters.AddWithValue("@rproduct", txtProduct.Text);
                cmd.Parameters.AddWithValue("@description", txtDescription.Text);


                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Repair Product Information Inserted Successfully!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearTexts();
                }
            }
            catch (Exception ee)
            {
            }
        }

        private void clearTexts()
        {
        
            txtProduct.Text = "";
            txtDescription.Text = "";
            txtProduct.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
               txtProduct.Focus();
                con = c.openConnection();

                if (listView1.SelectedItems.Count == 1)
                {
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                    id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                    query = "select * from tblrepairproduct where rpid='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {

                        txtProduct.Text = Convert.ToString(sdr.GetValue(1));
                        txtDescription.Text = sdr.GetValue(2).ToString();


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
                    query = "delete from tblrepairproduct where rpid='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Repair Product Information Deleted Successfully!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmRepairProduct_Load(sender, e);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtProduct.Text == "")
                {
                    MessageBox.Show("Please Enter Product Name", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProduct.Focus();
                }
               
                else
                {

                    con = c.openConnection();
                    id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                    query = "update tblrepairproduct set rproduct=@rproduct,description=@description  where rpid='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@rproduct", txtProduct.Text);
                    cmd.Parameters.AddWithValue("@description", txtDescription.Text);



                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Repair Product Information Updated Successfully!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearTexts();
                        FrmRepairProduct_Load(sender, e);
                    }
                }
                }
        
            catch (Exception ee)
            {
              //  MessageBox.Show(ee.Message);
            }

        }



    }
}
