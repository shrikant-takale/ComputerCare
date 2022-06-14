using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Windows.Forms;
using System.Data.SqlClient;
using ComputerCare.Connections;
using ComputerCare.Reports;

namespace ComputerCare.Operations
{
    public partial class FrmInword : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname,msg;
        int i, billno = 1000, j, k, m, l, maxid,cust;
        System.Net.WebRequest WebRequest = null;
        System.Net.WebResponse WebResonse = null;

        public FrmInword()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtQty.Text == ""||txtQty.Text=="0")
                {
                    MessageBox.Show("Please Enter Quantity", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQty.Focus();
                }
                else if (txtServiceTagNo.Text == ""||txtServiceTagNo.Text=="0")
                {
                    MessageBox.Show("Please Enter Service Tag No.", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtServiceTagNo.Focus();
                }
                else if (listView1.Items.Count == 1)
                {
                    MessageBox.Show("You can add only one product", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FrmInword_Load(sender,e);
                    txtQty.Text = "";
                    txtServiceTagNo.Text = "";
                    cmbCustomer.Focus();
                }
                else
                {
                    listView1.Columns.Clear();
                    // listView1.Items.Clear();
                    listView1.Update(); // In case there is databinding
                    listView1.Refresh(); // Redraw items

                    listView1.View = View.Details;
                    listView1.GridLines = true;
                    listView1.FullRowSelect = true;

                
                    listView1.Columns.Add("PID", 0);
                    listView1.Columns.Add("Product Name", 120);
                    listView1.Columns.Add("Description", 200);
                    listView1.Columns.Add("Pid", 0);
                    listView1.Columns.Add("Problem", 220);
                    listView1.Columns.Add("Qty.", 60);
                    listView1.Columns.Add("Service Tag No.", 120);


                    string[] arr = new string[20];
                    ListViewItem itm;


                 
                    arr[0] = cmbProduct.SelectedValue.ToString();
                    arr[1] = cmbProduct.Text;
                    arr[2] = txtDescription.Text;
                    arr[3] = cmbProblem.SelectedValue.ToString();
                    arr[4] = cmbProblem.Text;
                    arr[5] = txtQty.Text;
                    arr[6] = txtServiceTagNo.Text;
                    

                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm); 
                    //FrmInword_Load(sender,e);
                    txtQty.Text = "0";
                    txtServiceTagNo.Text = "0";
                    cmbProduct.Focus();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void FrmInword_Load(object sender, EventArgs e)
        {
            getCompanyName();
            getInwordNo();
            getCustomer();
            getRepairProduct();
            getDescription();
            getProblem();
            cmbCustomer.Focus();
        }

        private void getCompanyName()
        {
            try
            {
                con = c.openConnection();
                query = "select  name,year from tblsession";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    lblCompany.Text = sdr.GetValue(0).ToString();
                    lblYear.Text = sdr.GetValue(1).ToString();
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

        private void getInwordNo()
        {
            try
            {
                int cc = 0;
                con = c.openConnection();
                query = "select count(inwid) from tblinword where year='"+lblYear.Text+"'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cc = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (cc == 0)
                {
                    lblInword.Text = Convert.ToString(billno);
                }
                else
                {
                    query = "select max(inwordno) from tblinword where year='"+lblYear.Text+"'";
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    SqlDataReader sdr1 = cmd1.ExecuteReader();
                    if (sdr1.Read())
                    {
                        billno = 1 + Convert.ToInt32(sdr1.GetValue(0));
                        lblInword.Text = Convert.ToString(billno);
                    }
                    sdr1.Close();
                }
            }
            catch (Exception ee)
            {

            }
        }


        private void getDescription()
        {
            try
            {
                con = c.openConnection();
                int rpid = 0;
                rpid = Int32.Parse(cmbProduct.SelectedValue.ToString());
                query = "select description from tblrepairproduct where rpid='" + rpid + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {

                    //  lblHSN.Text = sdr.GetValue(0).ToString();
                    txtDescription.Text = sdr.GetValue(0).ToString();
                }
                sdr.Close();
            }
            catch (Exception ex)
            {
            }
        }


        private void getRepairProduct()
        {
            try
            {
                con = c.openConnection();
                cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbProduct.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbProduct.DataBindings.Clear();

                query = "select rpid,rproduct from tblrepairproduct where oid='" + lblid.Text + " ' ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("Product");
                da.Fill(ds, "Product");
                cmbProduct.DataSource = ds;
                cmbProduct.ValueMember = "Product.rpid";
                cmbProduct.DisplayMember = "Product.rproduct";
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }

        }

        private void getCustomer()
        {
            try
            {
                con = c.openConnection();
                cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbCustomer.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbCustomer.DataBindings.Clear();

                query = "select cid,cname from tblcustomer where oid='" + lblid.Text + " ' ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("Customer");
                da.Fill(ds, "Customer");
                cmbCustomer.DataSource = ds;
                cmbCustomer.ValueMember = "Customer.cid";
                cmbCustomer.DisplayMember = "Customer.cname";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }
        private void getProblem()
        {
            try
            {
                con = c.openConnection();
                cmbProblem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbProblem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProblem.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbProblem.DataBindings.Clear();

                query = "select pid,problem from tblproblem where oid='" + lblid.Text + " ' ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("Problem");
                da.Fill(ds, "Problem");
                cmbProblem.DataSource = ds;
                cmbProblem.ValueMember = "Problem.pid";
                cmbProblem.DisplayMember = "Problem.problem";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            getDescription();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
              

                int i;
                if (listView1.SelectedItems.Count == 1)
                {
                    for (i = 0; i < listView1.Items.Count; i++)
                    {
                        listView1.SelectedItems[i].Remove();
                        i--;

                    }
                }
                else
                {
                    MessageBox.Show("Please select atleast one record to remove", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ee)
            {
            }

        }
        private void InwordReport(int maxid)
        {
            FrmRptInw frr = new FrmRptInw(maxid);
            this.Hide();
            frr.ShowDialog();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.Items.Count == 0)
                {
                    MessageBox.Show("Please Add Atleast One Product", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbCustomer.Focus();
                }
                else
                {
                    con = c.openConnection();

                    query = "insert into tblinword (date,inwordno,cid,customer,contact,oid,year,gstno) values (@date,@inwordno,@cid,@customer,@contact,@oid,@year,@GST)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@oid", lblid.Text);
                    cmd.Parameters.AddWithValue("@date", dtInword.Value.ToString("dd-MM-yyyy"));
                    cmd.Parameters.AddWithValue("@cid", cmbCustomer.SelectedValue);
                    cmd.Parameters.AddWithValue("@customer", cmbCustomer.Text);
                    cmd.Parameters.AddWithValue("@inwordno", lblInword.Text);
                    cmd.Parameters.AddWithValue("@contact", lblContact.Text);
                    cmd.Parameters.AddWithValue("@year", lblYear.Text);
                    cmd.Parameters.AddWithValue("@GST", lblCustGST.Text);
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        for (i = 0; i < listView1.Items.Count; i++)
                        {

                            query = "insert into tblinworditem (inwordno,rpid,rproduct,description,qty,pid,problem,servicetagno,oid,year) values (@inwordno,@rpid,@rproduct,@desc,@qty,@pid,@problem,@servicetagno,@oid,@year)";
                            SqlCommand cmd1 = new SqlCommand(query, con);
                            cmd1.Parameters.AddWithValue("@inwordno", lblInword.Text);
                            cmd1.Parameters.AddWithValue("@oid", lblid.Text);
                            cmd1.Parameters.AddWithValue("@year", lblYear.Text);
                            cmd1.Parameters.AddWithValue("@rpid", listView1.Items[i].SubItems[0].Text);
                            cmd1.Parameters.AddWithValue("@rproduct", listView1.Items[i].SubItems[1].Text);
                            cmd1.Parameters.AddWithValue("@desc", listView1.Items[i].SubItems[2].Text);
                            cmd1.Parameters.AddWithValue("@pid", listView1.Items[i].SubItems[3].Text);
                            cmd1.Parameters.AddWithValue("@problem", listView1.Items[i].SubItems[4].Text);
                            cmd1.Parameters.AddWithValue("@qty", listView1.Items[i].SubItems[5].Text);
                            cmd1.Parameters.AddWithValue("@servicetagno", listView1.Items[i].SubItems[6].Text);
                    
                           
                            j = cmd1.ExecuteNonQuery();

                        }

                        if (j > 0)
                        {
                            query = "select max(inwordno) from tblinword where year='"+lblYear.Text+"'";
                            SqlCommand cmd3 = new SqlCommand(query, con);
                            SqlDataReader sdr = cmd3.ExecuteReader();
                            if (sdr.Read())
                            {
                                maxid = Convert.ToInt32(sdr.GetValue(0));
                            }
                            sdr.Close();
                            try
                            {
                                msg = "Dear Customer, Your Product with Inward No. " + lblInword.Text + " is received at Computer Care, Baramati ";
                                string WebResponseString = "";
                                string URL = "http://mysms.ynorme.com:8080/sendsms/bulksms?username=yno-scoregraph&password=97306242&type=0&dlr=1&destination=" + lblContact.Text + "&source=COMPCR&message=" + msg + "";
                                WebRequest = System.Net.HttpWebRequest.Create(URL);
                                WebRequest.Timeout = 25000;
                                WebResonse = WebRequest.GetResponse();
                                System.IO.StreamReader reader = new System.IO.StreamReader(WebResonse.GetResponseStream());
                                WebResponseString = reader.ReadToEnd();
                                WebResonse.Close();
                                MessageBox.Show("Message Sent Successfully", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MessageBox.Show("Inward Generated Successfully ", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                InwordReport(maxid);
                            
                                listView1.Items.Clear();

                                // lblRemain.Text = "0";

                                FrmInword_Load(sender, e);
                            }
                            catch (System.Net.WebException)
                            {
                                MessageBox.Show("Unable to Send Message due to Internet Connection", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MessageBox.Show(" Inward  Generated Successfully!!! Please Take Print!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                InwordReport(maxid);
                               
                                FrmInword_Load(sender, e);
                            }
                              

                                    }
                                }
                            }
                        }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmInword_KeyDown(object sender, KeyEventArgs e)
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

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            getContact();
        }

        private void getContact()
        {
            try
            {
                con = c.openConnection();
                int cid = 0;
                cid = Int32.Parse(cmbCustomer.SelectedValue.ToString());
                query = "select  ccontact,gstno from tblcustomer where oid='" + lblid.Text + "' and cid='" + cid + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    lblContact.Text = sdr.GetValue(0).ToString();
                    lblCustGST.Text = sdr.GetValue(1).ToString();

                }
                sdr.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            getDescription();
        }

        private void cmbProduct_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                query = "select count(rpid) from tblrepairproduct where oid='" + lblid.Text + "' and rproduct='" + cmbProduct.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cust = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (cust == 0)
                {
                    MessageBox.Show("Product does not Exist. Please Add Product !!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmInword_Load(sender, e);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
            }
        }
        
        private void cmbCustomer_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                query = "select count(cid) from tblcustomer where oid='" + lblid.Text + "' and cname='" + cmbCustomer.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cust = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (cust == 0)
                {
                    MessageBox.Show("Customer does not Exist. Please Register Customer !!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmInword_Load(sender, e);
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
            }
        }

        private void cmbProblem_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                query = "select count(pid) from tblproblem where oid='" + lblid.Text + "' and problem='" + cmbProblem.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cust = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (cust == 0)
                {
                    MessageBox.Show("Problem does not Exist. Please Add Problem !!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmInword_Load(sender, e);
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
