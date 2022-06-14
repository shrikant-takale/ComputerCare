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
    public partial class FrmSell : Form
    {
        connection c = new connection();
        SqlConnection con=null;
        string query, cname,msg;
        int i, billno = 1000, j, k, m, l, maxid,n,o,count,cust,y,x;
        System.Net.WebRequest WebRequest = null;
        System.Net.WebResponse WebResonse = null;
        public FrmSell()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSell_Load(object sender, EventArgs e)
        {
            getCompanyName();
            getInvoiceNo();
            getCustomer();
            getSellProduct();
            getGST();
            getDescription();
         
           cmbCustomer.Focus();
            cmbPayType.SelectedIndex = 0;
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

        private void getInvoiceNo()
        {
            try
            {
                int cc = 0;
                con = c.openConnection();
                query = "select count(sid) from tblsell where oid='"+lblid.Text+"' and year='" +lblYear.Text+"'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cc = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (cc == 0)
                {
                    lblInvoice.Text = Convert.ToString(billno);
                }
                else
                {
                    query = "select max(invoiceno) from tblsell where oid='" + lblid.Text + "' and year='" + lblYear.Text + "'";
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    SqlDataReader sdr1 = cmd1.ExecuteReader();
                    if (sdr1.Read())
                    {
                        billno = 1 + Convert.ToInt32(sdr1.GetValue(0));
                        lblInvoice.Text = Convert.ToString(billno);
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
                int spid = 0;
                spid = Int32.Parse(cmbProduct.SelectedValue.ToString());
                query = "select description,stock,salerate from tblsellproduct where spid='" + spid + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {

                    //  lblHSN.Text = sdr.GetValue(0).ToString();
                    txtDescription.Text = sdr.GetValue(0).ToString();
                    lblStock.Text = sdr.GetValue(1).ToString();            
                    lblPrate.Text = sdr.GetValue(2).ToString();
                 
                }
              
                sdr.Close();
                }
          
            catch (Exception ex)
            {
            }
        }

        private void getGST()
        {
            try
            {
                if (chkGST.Checked == true)
                {
                    lblGST.Text = "0";
                    txtCGST.Text = "0";
                    txtSGST.Text = "0";
                }
                else
                {


                    con = c.openConnection();
                    int spid = 0;
                    spid = Int32.Parse(cmbProduct.SelectedValue.ToString());
                    query = "select gst,sgst,cgst from tblsellproduct where spid='" + spid + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {

                        //  lblHSN.Text = sdr.GetValue(0).ToString();
                       
                        lblGST.Text = sdr.GetValue(0).ToString();
                        txtSGST.Text = sdr.GetValue(1).ToString();
                        txtCGST.Text = sdr.GetValue(2).ToString();
                    }

                    sdr.Close();
                }
            }
            catch (Exception ex)
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

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            int stock = Convert.ToInt32(lblStock.Text);
            int quantity = Convert.ToInt32(txtQuantity.Text);
            if (stock < quantity)
            {
                MessageBox.Show("Stock is not available..", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQuantity.Text = "0";
                txtQuantity.Focus();
            }
            else
            {
                double qty = Convert.ToDouble(txtQuantity.Text);
                double price = Convert.ToDouble(txtPrice.Text);
                double total = qty * price;
                txtTotal.Text = total.ToString();

            }
        }

        private void FrmSell_KeyDown(object sender, KeyEventArgs e)
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

        private void txtQuotation_Leave(object sender, EventArgs e)
        {
           
        }

        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                 if (txtServicetagno.Text == ""||txtServicetagno.Text=="0")
                {
                    MessageBox.Show("Please Enter Service Tag No.", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtServicetagno.Focus();
                }
                else if (txtQuantity.Text == ""||txtQuantity.Text=="0")
                {
                    MessageBox.Show("Please Enter Quantity", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQuantity.Focus();
                }
               
                else if (txtPrice.Text == "")
                {
                    MessageBox.Show("Please Enter Sales Rate", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                
                  // txtPrice.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
                   // txtPrice.BackColor = Color.Black;
                }
                else if (txtSGST.Text == "")
                {
                    MessageBox.Show("Please Enter SGST", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSGST.Focus();
                }
                else if (txtCGST.Text == "")
                {
                    MessageBox.Show("Please Enter CGST", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCGST.Focus();
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
                    listView1.Columns.Add("Service Tag No.", 0);
                    listView1.Columns.Add("PID", 0);
                    listView1.Columns.Add("Product", 100);
                    listView1.Columns.Add("Description", 200);
                    listView1.Columns.Add("GST%", 60);
                    listView1.Columns.Add("Qty.", 80);
                    listView1.Columns.Add("Rate", 80);
                    listView1.Columns.Add("Total", 0);
                    listView1.Columns.Add("SGST%", 60);
                    listView1.Columns.Add("SGST(Rs.)", 80);
                    listView1.Columns.Add("CGST%", 60);
                    listView1.Columns.Add("CGST(Rs.)", 80);
                    listView1.Columns.Add("Total Amount", 120);

                    string[] arr = new string[20];
                    ListViewItem itm;
                    arr[0] = txtServicetagno.Text;
                    arr[1] = cmbProduct.SelectedValue.ToString();
                    arr[2] = cmbProduct.Text;
                    arr[3] = txtDescription.Text;
                    arr[4] = lblGST.Text;
                    arr[5] = txtQuantity.Text;
                    arr[6] = txtPrice.Text;
                    arr[7] = txtTotal.Text;
                    arr[8] = txtSGST.Text;
                    arr[9] = lblSGST.Text;
                    arr[10] = txtCGST.Text;
                    arr[11] = lblCGST.Text;
                    arr[12] = lblTAmt.Text;

                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm); double t = 0.0, q = 0.00;
                    int i;
                    int p = listView1.Items.Count;
                    for (i = 0; i < p; i++)
                    {
                        q = q + Convert.ToDouble(listView1.Items[i].SubItems[5].Text);
                        t = t + Convert.ToDouble(listView1.Items[i].SubItems[12].Text);
                    }

                    lblTotalQty.Text = Convert.ToString(Math.Round(q));
                    lblFinalTotal.Text = Convert.ToString(Math.Round(t));

                    txtQuantity.Text = "0";
                    lblTAmt.Text = "0";
                    txtTotal.Text = "0";
                    txtCGST.Text = "0";
                    txtSGST.Text = "0";
                    lblCGST.Text = "0";
                    lblSGST.Text = "0";
                    lblTAmt.Text = "0";
                    txtServicetagno.Text = "";
                    cmbProduct.Focus();
                   
                    chkGST.Enabled = false;
                }

                }
            
            catch (Exception ee)
            {
               // MessageBox.Show(ee.Message);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                double t = 0.0, q = 0.00;

                int i;
                if (listView1.SelectedItems.Count == 1)
                {
                    for (i = 0; i < listView1.Items.Count; i++)
                    {
                        listView1.SelectedItems[i].Remove();
                        i--;
                        int p = listView1.Items.Count;
                        for (i = 0; i < p; i++)
                        {
                            t = t + Convert.ToDouble(listView1.Items[i].SubItems[12].Text);
                            q = q + Convert.ToDouble(listView1.Items[i].SubItems[5].Text);

                        }

                        lblTotalQty.Text = Convert.ToString(q);
                        lblFinalTotal.Text = Convert.ToString(t);

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

        private void SellReport(int maxid)
        {
            FrmRptSell q = new FrmRptSell(maxid);
            this.Hide();
            q.ShowDialog();
        }


    

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQuantity.Text = "0";
            txtTotal.Text = "0";
            getGST();
            getDescription();
   
        }
       

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkGST.Checked == true)
                {
                    lblTAmt.Text = txtTotal.Text;
                }
                if (chkGST.Checked == false)
                {
                    double total = Convert.ToDouble(txtTotal.Text);
                    double gst = Convert.ToDouble(lblGST.Text);
                    double cgst = Convert.ToDouble(txtCGST.Text);
                    double sgst = Convert.ToDouble(txtSGST.Text);
                    double tgst = Math.Round(total * gst / (100));
                    double tcgst =tgst / 2;
                    double tsgst =tgst / 2;
                    lblCGST.Text = tcgst.ToString();
                    lblSGST.Text = tsgst.ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void txtSGST_Leave(object sender, EventArgs e)
        {
            try
            {
              //  double total = Convert.ToDouble(txtTotal.Text);
              //  double gst = Convert.ToDouble(lblGST.Text);
              // // double cgst = Convert.ToDouble(txtCGST.Text);
              //  double sgst = Convert.ToDouble(txtSGST.Text);
              //  double tgst = Math.Round(total * gst / (100));
              // // double tcgst = tgst / 2;
              //  double tsgst = Math.Round(tgst / 2);
              ////  lblCGST.Text = tcgst.ToString();
              //  lblSGST.Text = tsgst.ToString();

            }
            catch (Exception ex)
            {
            }
        }

        private void txtCGST_Leave(object sender, EventArgs e)
        {
            try
            {
              //  double total = Convert.ToDouble(txtTotal.Text);
              //  double gst = Convert.ToDouble(lblGST.Text);
              //  double cgst = Convert.ToDouble(txtCGST.Text);
              ////  double sgst = Convert.ToDouble(txtSGST.Text);
              //  double tgst = Math.Round(total * gst / (100));
              //  double tcgst =Math.Round( tgst / 2);
              // // double tsgst = tgst / 2;
              //  lblCGST.Text = tcgst.ToString();
              ////  lblSGST.Text = tsgst.ToString();
              
            }
            catch (Exception ex)
            {
            }
        }

        private void lblSGST_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double total = Convert.ToDouble(txtTotal.Text);
                double cgst = Convert.ToDouble(lblCGST.Text);
                double sgst = Convert.ToDouble(lblSGST.Text);
                double grandtotal = total+(cgst + sgst);
                lblTAmt.Text = grandtotal.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void lblCGST_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double total = Convert.ToDouble(txtTotal.Text);
                double cgst = Convert.ToDouble(lblCGST.Text);
                double sgst = Convert.ToDouble(lblSGST.Text);
                double grandtotal = total+( cgst + sgst);
                lblTAmt.Text = grandtotal.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void lblFinalTotal_TextChanged(object sender, EventArgs e)
        {
            lblRemain.Text = lblFinalTotal.Text;
        }

        private void cmbPayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbPayType.Text == "Credit")
            //{
            //    lblRemain.Text = lblFinalTotal.Text;
            //    txtPaid.Enabled = false;
            //}
            //else
            //{
            //    txtPaid.Text = "0";
            //    txtPaid.Enabled = true;

            //}
        }

        private void txtPaid_Leave(object sender, EventArgs e)
        {
            try
            {
                double paid = Convert.ToDouble(txtPaid.Text);
                double final = Convert.ToDouble(lblFinalTotal.Text);
                double remain = final - paid;
                lblRemain.Text = remain.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getContact();

            }
            catch(Exception ex)
            {
            }
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
                    lblcustGST.Text = sdr.GetValue(1).ToString();

                }
                sdr.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void chkGST_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGST.Checked == true)
            {
                lblGST.Text = "0";
                txtCGST.Text = "0";
                txtSGST.Text = "0";
                lblCGST.Text = "0";
                lblSGST.Text = "0";
                txtQuantity.Text = "0";
                txtTotal.Text = "0";
                lblGST.Enabled = false;
                lblSGST.Enabled = false;
                lblCGST.Enabled = false;
                txtSGST.Enabled = false;
                txtCGST.Enabled = false;

            }
            else if(chkGST.Checked==false)
            {
                txtQuantity.Text = "0";
                txtTotal.Text = "0";
                lblGST.Enabled = true;
                lblSGST.Enabled = true;
                lblCGST.Enabled = true;
                txtSGST.Enabled = true;
                txtCGST.Enabled = true;
                getGST();
            }
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
                else if (txtPaid.Text == "")
                {
                    MessageBox.Show("Please Enter Paid Amount!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaid.Text = "0";
                    txtPaid.Focus();
                }
                else
                {
                    con = c.openConnection();
                   
                        query = "insert into tblsell (date,invoiceno,cid,totalqty,totalamt,paytype,paid,remain,oid,year,flag) values (@date,@invoiceno,@cid,@tqty,@totalamt,@paytype,@paid,@remain,@oid,@year,@flag)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@oid", lblid.Text);
                        cmd.Parameters.AddWithValue("@year", lblYear.Text);
                        cmd.Parameters.AddWithValue("@date", dtSell.Value.ToString("dd-MM-yyyy"));
                        cmd.Parameters.AddWithValue("@tqty", lblTotalQty.Text);
                        cmd.Parameters.AddWithValue("@cid", cmbCustomer.SelectedValue);
                       // cmd.Parameters.AddWithValue("@servicetagno", txtServicetagno.Text);
                        cmd.Parameters.AddWithValue("@invoiceno", lblInvoice.Text);
                        cmd.Parameters.AddWithValue("@totalamt", lblFinalTotal.Text);
                        cmd.Parameters.AddWithValue("@paytype", cmbPayType.Text);
                        cmd.Parameters.AddWithValue("@paid", txtPaid.Text);
                        cmd.Parameters.AddWithValue("@remain", lblRemain.Text);
                        if (chkGST.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@flag", 1);
                        }
                        else  if (chkGST.Checked == false)
                        {
                            cmd.Parameters.AddWithValue("@flag", 0);
                        }
                        i = cmd.ExecuteNonQuery();
                 
                    if (i > 0)
                    {
                        for (i = 0; i < listView1.Items.Count; i++)
                        {

                            query = "insert into tblsellitem (invoiceno,servicetagno,spid,description,gst,qty,rate,total,sgst,sgstamt,cgst,cgstamt,totalamount,oid,year) values (@invoiceno,@servicetagno,@spid,@description,@gst,@qty,@rate,@total,@sgst,@sgstamt,@cgst,@cgstamt,@totalamount,@oid,@year)";
                            SqlCommand cmd1 = new SqlCommand(query, con);
                            cmd1.Parameters.AddWithValue("@invoiceno", lblInvoice.Text);
                            cmd1.Parameters.AddWithValue("@oid", lblid.Text);
                            cmd1.Parameters.AddWithValue("@year", lblYear.Text);
                            cmd1.Parameters.AddWithValue("@servicetagno", listView1.Items[i].SubItems[0].Text);
                            cmd1.Parameters.AddWithValue("@spid", listView1.Items[i].SubItems[1].Text);
                            cmd1.Parameters.AddWithValue("@description", listView1.Items[i].SubItems[3].Text);
                            cmd1.Parameters.AddWithValue("@gst", listView1.Items[i].SubItems[4].Text);
                            cmd1.Parameters.AddWithValue("@qty", listView1.Items[i].SubItems[5].Text);
                            cmd1.Parameters.AddWithValue("@rate", listView1.Items[i].SubItems[6].Text);
                            cmd1.Parameters.AddWithValue("@total", listView1.Items[i].SubItems[7].Text);
                            cmd1.Parameters.AddWithValue("@sgst", listView1.Items[i].SubItems[8].Text);
                            cmd1.Parameters.AddWithValue("@sgstamt", listView1.Items[i].SubItems[9].Text);
                            cmd1.Parameters.AddWithValue("@cgst", listView1.Items[i].SubItems[10].Text);
                            cmd1.Parameters.AddWithValue("@cgstamt", listView1.Items[i].SubItems[11].Text);
                            cmd1.Parameters.AddWithValue("@totalamount", listView1.Items[i].SubItems[12].Text);
                            m = cmd1.ExecuteNonQuery();

                        }
                        if (m > 0)
                        {


                            for (int jj = 0; jj < listView1.Items.Count; jj++)
                            {

                                query = "update tblsellproduct set stock=stock-@qty where  oid=@oid and spid=@spid";
                                SqlCommand cmd4 = new SqlCommand(query, con);
                                cmd4.Parameters.AddWithValue("@oid", lblid.Text);
                                cmd4.Parameters.AddWithValue("@spid", listView1.Items[jj].SubItems[1].Text);
                                cmd4.Parameters.AddWithValue("@qty", listView1.Items[jj].SubItems[5].Text);
                                k = cmd4.ExecuteNonQuery();


                            }
                            if (k > 0)
                            {
                               
                                    query = "update tblcustomer set copcredit=copcredit+@remain where cid='" + cmbCustomer.SelectedValue + "' and oid='" + lblid.Text + "' ";
                                    SqlCommand cmd2 = new SqlCommand(query, con);
                                    cmd2.Parameters.AddWithValue("@remain", lblRemain.Text);
                                    j = cmd2.ExecuteNonQuery();
                              
                                if (j > 0)
                                {
                                   
                                        query = "insert into tblbalancesheet (oid,date,type,typeid,description,credit,debit,year) values (@oid,@date,@type,@typeid,@description,@credit,@debit,@year)";
                                        SqlCommand cmd8 = new SqlCommand(query, con);
                                        cmd8.Parameters.AddWithValue("@oid", lblid.Text);
                                        cmd8.Parameters.AddWithValue("@date", dtSell.Value.ToString("dd-MM-yyyy"));
                                        cmd8.Parameters.AddWithValue("@type", "Sales Account");
                                        cmd8.Parameters.AddWithValue("@typeid", cmbCustomer.SelectedValue);
                                        cmd8.Parameters.AddWithValue("@description", "Sales Payment From Customer " + cmbCustomer.Text + " Against Invoice No. " + lblInvoice.Text + ".");
                                        cmd8.Parameters.AddWithValue("@credit", txtPaid.Text);
                                        cmd8.Parameters.AddWithValue("@debit", "0.00");
                                        cmd8.Parameters.AddWithValue("@year", lblYear.Text);
                                        y = cmd8.ExecuteNonQuery();


                                        if (y > 0)
                                        {
                                            query = "insert into tbltransaction (oid,date,type,cid,cname,gstno,invoiceno,gst,amount,year) values (@oid,@date,@type,@cid,@cname,@gstno,@invoiceno,@gst,@amount,@year)";
                                            SqlCommand cmd9 = new SqlCommand(query, con);
                                            cmd9.Parameters.AddWithValue("@oid", lblid.Text);
                                            cmd9.Parameters.AddWithValue("@date", dtSell.Value.ToString("dd-MM-yyyy"));
                                            cmd9.Parameters.AddWithValue("@cid", cmbCustomer.SelectedValue.ToString());
                                            cmd9.Parameters.AddWithValue("@cname", cmbCustomer.Text);
                                            cmd9.Parameters.AddWithValue("@gstno", lblcustGST.Text);
                                            cmd9.Parameters.AddWithValue("@invoiceno", lblInvoice.Text);
                                            cmd9.Parameters.AddWithValue("@gst", lblGST.Text);
                                            cmd9.Parameters.AddWithValue("@amount", lblFinalTotal.Text);
                                            cmd9.Parameters.AddWithValue("@year", lblYear.Text);
                                            cmd9.Parameters.AddWithValue("@type", "Sales Account");
                                          n = cmd9.ExecuteNonQuery();
                                            if (n > 0)
                                            {

                                                query = "select max(invoiceno) from tblsell where oid='" + lblid.Text + "' and year='" + lblYear.Text + "'";
                                                SqlCommand cmd3 = new SqlCommand(query, con);
                                                SqlDataReader sdr = cmd3.ExecuteReader();
                                                if (sdr.Read())
                                                {
                                                    maxid = Convert.ToInt32(sdr.GetValue(0));
                                                }
                                                sdr.Close();
                                                try
                                                {
                                                    msg = "Dear Customer, Thank you for visiting Computer Care, Baramati and Your Bill Amount is " + lblFinalTotal.Text + " Rs.";
                                                    string WebResponseString = "";
                                                    string URL = "http://mysms.ynorme.com:8080/sendsms/bulksms?username=yno-scoregraph&password=97306242&type=0&dlr=1&destination=" + lblContact.Text + "&source=COMPCR&message=" + msg + "";
                                                    //string URL = "http://mysms.ynorme.com:8080/sendsms/bulksms?username=yno-nabagade&password=63542012&type=0&dlr=1&destination=" + lblContact.Text + "&source=YNORME&message=" + msg + "";
                                                    WebRequest = System.Net.HttpWebRequest.Create(URL);
                                                    WebRequest.Timeout = 25000;
                                                    WebResonse = WebRequest.GetResponse();

                                                    System.IO.StreamReader reader = new System.IO.StreamReader(WebResonse.GetResponseStream());
                                                    WebResponseString = reader.ReadToEnd();
                                                    WebResonse.Close();
                                                    MessageBox.Show("Message Sent Successfully", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    MessageBox.Show("Sales Invoice Generated Successfully", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    SellReport(maxid);
                                                    listView1.Items.Clear();
                                                    lblFinalTotal.Text = "0";
                                                    lblTotalQty.Text = "0";

                                                    FrmSell_Load(sender, e);
                                                }
                                                catch (System.Net.WebException)
                                                {
                                                    MessageBox.Show("Unable to Send Message due to Internet Connection", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    MessageBox.Show("Sales Invoice Generated Successfully ", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    SellReport(maxid);
                                                    listView1.Items.Clear();
                                                    lblFinalTotal.Text = "0";
                                                    lblTotalQty.Text = "0";
                                                    FrmSell_Load(sender, e);

                                                }
                                            }
                                        }                                   
                                }
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

     

        private void lblTAmt_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblPrate_TextChanged(object sender, EventArgs e)
        {
            try
            {
              
                    double gst = Convert.ToDouble(lblGST.Text);
                    double prate = Convert.ToDouble(lblPrate.Text);
                   

                    double price = Math.Round(prate-prate * gst / (gst+100));
                    txtPrice.Text = price.ToString();
             
            }
            catch(Exception ex)
            {
            }
        }

        private void lblGST_TextChanged(object sender, EventArgs e)
        {
            try
            {

                double gst = Convert.ToDouble(lblGST.Text);
                double prate = Convert.ToDouble(lblPrate.Text);


                double price = Math.Round(prate+prate * gst / (100));
                txtPrice.Text = price.ToString();

            }
            catch (Exception ex)
            {
            }
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            getDescription();
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
                    FrmSell_Load(sender, e);
                }
                else
                {

                }
            }
            catch (Exception ex)
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
                    MessageBox.Show("Product does not Exist. Please Add Product !!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmSell_Load(sender, e);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                double qty = Convert.ToDouble(txtQuantity.Text);
                double price = Convert.ToDouble(lblTAmt.Text);
                double total = qty * price;
                txtTotal.Text = total.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void txtPrice_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                double qty = Convert.ToDouble(txtQuantity.Text);
                double price = Convert.ToDouble(txtPrice.Text);
                double total = qty * price;
                txtTotal.Text = total.ToString();
            }
            catch (Exception ex)
            {
            }
        }

       

      

       

      

    }
}
