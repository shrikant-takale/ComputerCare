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
    public partial class FrmSellProduct : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        int i, id;

        public FrmSellProduct()
        {
            InitializeComponent();
        }

        

        private void FrmSellProduct_Load(object sender, EventArgs e)
        {
            try
            {
                getCompanyName();
                getSellProductInfo();
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

        private void FrmSellProduct_KeyDown(object sender, KeyEventArgs e)
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

        private void getSellProductInfo()
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


                listView1.Columns.Add("Sell Product id", 0);
                listView1.Columns.Add(" Sell Product", 100);
                listView1.Columns.Add("Description", 200);
                listView1.Columns.Add("HSN No.", 100);
                listView1.Columns.Add("Stock", 100);
                listView1.Columns.Add("Purchase Rate", 100);
                listView1.Columns.Add("Sell Rate", 100);
                listView1.Columns.Add("GST", 80);
                listView1.Columns.Add("SGST",80);
                listView1.Columns.Add("CGST", 80);
             


                int i;

                con = c.openConnection();

                DataTable dt = new DataTable();
                string[] arr = new string[14];
                ListViewItem itm;
                query = "select * from tblsellproduct  where oid='" + lblid.Text + "'";
                SqlCommand cmd1 = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(dt);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    arr[0] = Convert.ToString(dt.Rows[i]["spid"]);
                    arr[1] = Convert.ToString(dt.Rows[i]["sproduct"]);
                    arr[2] = Convert.ToString(dt.Rows[i]["description"]);
                    arr[3] = Convert.ToString(dt.Rows[i]["hsn"]);
                    arr[4] = Convert.ToString(dt.Rows[i]["stock"]);
                    arr[5] = Convert.ToString(dt.Rows[i]["purchaserate"]);
                    arr[6] = Convert.ToString(dt.Rows[i]["salerate"]);
                    arr[7] = Convert.ToString(dt.Rows[i]["gst"]);
                    arr[8] = Convert.ToString(dt.Rows[i]["sgst"]);
                    arr[9] = Convert.ToString(dt.Rows[i]["cgst"]);
                   
                    


                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message);
            }
        }

     

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmSellProduct_Load(sender,e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProduct.Text == "")
                {
                    MessageBox.Show("Please Enter Product", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProduct.Focus();
                }
                else if (txtDescription.Text == "")
                {
                    MessageBox.Show("Please Enter Description", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDescription.Focus();
                }
              
                if (txtUnit.Text == "")
                {
                    MessageBox.Show("Please Enter Opening Stock", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUnit.Focus();
                }
                else if (txtPurchaseRate.Text == "")
                {
                    MessageBox.Show("Please Enter Purchase Rate", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPurchaseRate.Focus();
                }
                else if (txtSaleRate.Text == "")
                {
                    MessageBox.Show("Please Enter Sale Rate", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSaleRate.Focus();
                }
                else if (txtGST.Text == "")
                {
                    MessageBox.Show("Please Enter GST", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGST.Focus();
                }
                else if (txtCGST.Text == "")
                {
                    MessageBox.Show("Please Enter CGST", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCGST.Focus();
                }
                else if (txtSGST.Text == "")
                {
                    MessageBox.Show("Please Enter SGST", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSGST.Focus();
                }
                else if (checkSellProduct())
                {
                    insertSellProduct();
                    FrmSellProduct_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Product Name already Exist! Please Enter another Product Name!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FrmSellProduct_Load(sender, e);
                    txtProduct.Focus();
                }
            }
            catch (Exception ee)
            {
            }
        }

        private bool checkSellProduct()
        {
            try
            {
                int count = 0;
                con = c.openConnection();
                query = "select count(spid) from tblsellproduct where sproduct='" + txtProduct .Text+ "'";
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

        private void insertSellProduct()
        {
            try
            {
                con = c.openConnection();
                query = "insert into tblsellproduct (oid,sproduct,description,hsn,stock,purchaserate,salerate,gst,sgst,cgst) values (@oid,@sproduct,@description,@hsn,@stock,@purchaserate,@sellrate,@gst,@sgst,@cgst)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@oid", lblid.Text);
                cmd.Parameters.AddWithValue("@sproduct", txtProduct.Text);
                cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@hsn", txtHsnCode.Text);
                cmd.Parameters.AddWithValue("@stock", txtUnit.Text);
                cmd.Parameters.AddWithValue("@purchaserate", txtPurchaseRate.Text);
                cmd.Parameters.AddWithValue("@sellrate", txtSaleRate.Text);
                cmd.Parameters.AddWithValue("@gst", txtGST.Text);
                cmd.Parameters.AddWithValue("@sgst", txtSGST.Text);
                cmd.Parameters.AddWithValue("@cgst", txtCGST.Text);


                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Sales Product Information Inserted Successfully!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearTexts();
                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message);
            }
        }

        private void clearTexts()
        {
            txtProduct.Text = "";
            txtDescription.Text = "";
            txtHsnCode.Text = "";
            txtUnit.Text = "0";
            txtPurchaseRate.Text = "0";
            txtSaleRate.Text = "0";
            txtGST.Text = "0";
            txtSGST.Text = "0";
            txtCGST.Text = "0";
            txtProduct.Text = "";
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
                    query = "select * from tblsellproduct where spid='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {

                        txtProduct.Text = Convert.ToString(sdr.GetValue(1));
                        txtDescription.Text = sdr.GetValue(2).ToString();
                        txtHsnCode.Text = sdr.GetValue(3).ToString();
                        txtUnit.Text = sdr.GetValue(4).ToString();
                        txtPurchaseRate.Text = sdr.GetValue(5).ToString();
                        txtSaleRate.Text = sdr.GetValue(6).ToString();
                        txtGST.Text = sdr.GetValue(7).ToString();
                        txtSGST.Text = sdr.GetValue(8).ToString();
                        txtCGST.Text = sdr.GetValue(9).ToString();
                     


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

                if (txtProduct.Text == "")
                {
                    MessageBox.Show("Please Enter Product", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProduct.Focus();
                }
                else if (txtDescription.Text == "")
                {
                    MessageBox.Show("Please Enter Description", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDescription.Focus();
                }

                if (txtUnit.Text == "")
                {
                    MessageBox.Show("Please Enter Opening Stock", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUnit.Focus();
                }
                else if (txtPurchaseRate.Text == "")
                {
                    MessageBox.Show("Please Enter Purchase Rate", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPurchaseRate.Focus();
                }
                else if (txtSaleRate.Text == "")
                {
                    MessageBox.Show("Please Enter Sales Rate", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSaleRate.Focus();
                }
                else if (txtGST.Text == "")
                {
                    MessageBox.Show("Please Enter GST", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGST.Focus();
                }
                else if (txtCGST.Text == "")
                {
                    MessageBox.Show("Please Enter CGST", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCGST.Focus();
                }
                else if (txtSGST.Text == "")
                {
                    MessageBox.Show("Please Enter SGST", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSGST.Focus();
                }
                else
                {
                    con = c.openConnection();
                    id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                    query = "update tblsellproduct set sproduct=@sproduct,description=@description,hsn=@hsn,stock=@stock,purchaserate=@purchaserate,salerate=@sellrate,gst=@gst,sgst=@sgst,cgst=@cgst where spid='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@sproduct", txtProduct.Text);
                    cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@hsn", txtHsnCode.Text);
                    cmd.Parameters.AddWithValue("@stock", txtUnit.Text);
                    cmd.Parameters.AddWithValue("@purchaserate", txtPurchaseRate.Text);
                    cmd.Parameters.AddWithValue("@sellrate", txtSaleRate.Text);
                    cmd.Parameters.AddWithValue("@gst", txtGST.Text);
                    cmd.Parameters.AddWithValue("@sgst", txtSGST.Text);
                    cmd.Parameters.AddWithValue("@cgst", txtCGST.Text);
                   

                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Sales Product Information Updated Successfully!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearTexts();
                        FrmSellProduct_Load(sender, e);
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
                    query = "delete from tblsellproduct where spid='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Sales Product Information Deleted Successfully!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmSellProduct_Load(sender, e);
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

        private void txtGST_Leave(object sender, EventArgs e)
        {
            try
            {
                double gst = Convert.ToDouble(txtGST.Text);
                double val = gst / 2;
                txtSGST.Text = val.ToString();
                txtCGST.Text = val.ToString();
              
            }
            catch(Exception ex)
            {
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtGST_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
