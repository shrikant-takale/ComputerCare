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
using ComputerCare.Reports;

namespace ComputerCare.Operations
{
    public partial class FrmPurchase : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname,maxid;
        int i, billno = 1, j, k, m, l,n,count,cust;

        public FrmPurchase()
        {
            InitializeComponent();
        }

        private void FrmPurchase_Load(object sender, EventArgs e)
        {
            getCompanyName();
          //  getInvoiceNo();
            getDealer();
            getSellProduct();
            getDescription();
             lblInvoice.Focus();
            cmbPayType.SelectedIndex = 0;
        }

        private void getCompanyName()
        {
            try
            {
                con = c.openConnection();
                query = "select  name,year  from tblsession";
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
                query = "select count(prid) from tblpurchase where oid='"+lblid.Text+"' and year='"+lblYear.Text+"'";
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
                    query = "select max(invoiceno) from tblpurchase where oid='"+lblid.Text+"' and year='"+lblYear.Text+"'";
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
                query = "select description,stock,gst,purchaserate from tblsellproduct where spid='" + spid + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {

                    //  lblHSN.Text = sdr.GetValue(0).ToString();
                    txtDescription.Text = sdr.GetValue(0).ToString();
                    lblStock.Text = sdr.GetValue(1).ToString();
                    txtGST.Text = sdr.GetValue(2).ToString();
                    lblPrate.Text = sdr.GetValue(3).ToString();

                }
                sdr.Close();
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
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

        private void getDealer()
        {
            try
            {
                con = c.openConnection();
                cmbDealer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cmbDealer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbDealer.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbDealer.DataBindings.Clear();

                query = "select did,dname from tbldealer where oid='" + lblid.Text + " ' ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("Dealer");
                da.Fill(ds, "Dealer");
                cmbDealer.DataSource = ds;
                cmbDealer.ValueMember = "Dealer.did";
                cmbDealer.DisplayMember = "Dealer.dname";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            getDescription();
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            try
            {
                double qty = Convert.ToDouble(txtQuantity.Text);
                double price = Convert.ToDouble(txtPrice.Text);
                double total = qty * price;
                txtTotal.Text = total.ToString();
            }
            catch(Exception ex)
            {
            }
        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
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

        

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gst = Convert.ToDouble(txtGST.Text);
                double total = Convert.ToDouble(txtTotal.Text);
                double tamt = total * gst /100 ;
                lblGST.Text = Convert.ToString(Math.Round(tamt));
                double gst1 = Convert.ToDouble(lblGST.Text);
                double tamt1 = total+gst1;
                lblTAmt.Text = tamt1.ToString();
              
            }
            catch (Exception ex)
            {
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtGST_Leave(object sender, EventArgs e)
        {
            try
            {
                double gst = Convert.ToDouble(txtGST.Text);
                double total = Convert.ToDouble(txtTotal.Text);
                double tamt = total * gst / (100);
                lblGST.Text = Convert.ToString(Math.Round(tamt));
                double gst1=Convert.ToDouble(lblGST.Text);
                double tamt1 = total+ gst1;
                lblTAmt.Text = tamt1.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblInvoice.Text == "")
                {
                    MessageBox.Show("Please Enter Invoice No.", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   lblInvoice.Focus();
                  // getDescription();
                }

                else  if (txtQuantity.Text == ""||txtQuantity.Text=="0")
                {
                    MessageBox.Show("Please Enter Quantity", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQuantity.Focus();
                }
                else if (txtPrice.Text == ""||txtPrice.Text=="0")
                {
                    MessageBox.Show("Please Enter Purchase  Rate", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                }
                else if (txtGST.Text == "")
                {
                    MessageBox.Show("Please Enter GST", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGST.Focus();
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
                    listView1.Columns.Add("Product", 100);
                    listView1.Columns.Add("Description", 200);
                    listView1.Columns.Add("Qty.", 80);
                    listView1.Columns.Add("Rate", 80);
                    listView1.Columns.Add("Total", 0);
                    listView1.Columns.Add("GST%", 60);
                    listView1.Columns.Add("GST(Rs.)", 80);
                    listView1.Columns.Add("Total Amount", 120);

                    string[] arr = new string[20];
                    ListViewItem itm;

                    arr[0] = cmbProduct.SelectedValue.ToString();
                    arr[1] = cmbProduct.Text;
                    arr[2] = txtDescription.Text;
                    arr[3] = txtQuantity.Text;
                    arr[4] = txtPrice.Text;
                    arr[5] = txtTotal.Text;
                    arr[6] = txtGST.Text;
                    arr[7] = lblGST.Text;
                    arr[8] = lblTAmt.Text;

                  

                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm); 
                    double t = 0.0, q = 0.00;
                    int i;
                    int p = listView1.Items.Count;
                    for (i = 0; i < p; i++)
                    {
                        q = q + Convert.ToDouble(listView1.Items[i].SubItems[3].Text);
                        t = t + Convert.ToDouble(listView1.Items[i].SubItems[8].Text);
                    }

                    lblTotalQty.Text = Convert.ToString(Math.Round(q));
                    lblFinalTotal.Text = Convert.ToString(Math.Round(t));

                    txtQuantity.Text = "0";
                    txtPrice.Text = "0";
                    txtTotal.Text = "0";
                    txtGST.Text = "0";
                    lblGST.Text = "0";
                    lblTAmt.Text = "0";
                    cmbProduct.Focus();
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
                            t = t + Convert.ToDouble(listView1.Items[i].SubItems[8].Text);
                            q = q + Convert.ToDouble(listView1.Items[i].SubItems[3].Text);

                        }

                        lblTotalQty.Text = Convert.ToString(t);
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
        private void PurchaseReport(string maxid)
        {
            FrmRptPurchase p = new FrmRptPurchase(maxid);
            this.Hide();
            p.ShowDialog();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.Items.Count == 0)
                {
                    MessageBox.Show("Please Add Atleast One Product", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbDealer.Focus();
                }
                else if (lblInvoice.Text=="")
                {
                    MessageBox.Show("PleaseEnter Invoice No.", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblInvoice.Focus();
                }

                else  if (txtPaid.Text == "" )
                {
                    MessageBox.Show("Please Enter Paid Amount!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaid.Text = "0";
                    txtPaid.Focus();
                }

                else
                {
                    con = c.openConnection();

                    query = "insert into tblpurchase (date,invoiceno,did,totalqty,totalamt,paytype,paid,remain,oid,year) values (@date,@invoiceno,@did,@totalqty,@totalamt,@paytype,@paid,@remain,@oid,@year)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@oid", lblid.Text);
                    cmd.Parameters.AddWithValue("@year", lblYear.Text);
                    cmd.Parameters.AddWithValue("@date", dtPurchase.Value.ToString("dd-MM-yyyy"));
                    cmd.Parameters.AddWithValue("@totalqty", lblTotalQty.Text);
                    cmd.Parameters.AddWithValue("@did", cmbDealer.SelectedValue);
                    cmd.Parameters.AddWithValue("@invoiceno", lblInvoice.Text);
                    cmd.Parameters.AddWithValue("@totalamt", lblFinalTotal.Text);
                    cmd.Parameters.AddWithValue("@paytype", cmbPayType.Text);
                    cmd.Parameters.AddWithValue("@paid",txtPaid.Text);
                    cmd.Parameters.AddWithValue("@remain", lblRemain.Text);
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        for (i = 0; i < listView1.Items.Count; i++)
                        {

                            query = "insert into tblpurchaseitem (invoiceno,spid,description,qty,rate,total,gst,gstamt,totalamount,oid,year) values (@invoiceno,@spid,@description,@qty,@rate,@total,@gst,@gstamt,@totalamount,@oid,@year)";
                            SqlCommand cmd1 = new SqlCommand(query, con);
                            cmd1.Parameters.AddWithValue("@qno", lblInvoice.Text);
                            cmd1.Parameters.AddWithValue("@oid", lblid.Text);
                            cmd1.Parameters.AddWithValue("@year", lblYear.Text);
                            cmd1.Parameters.AddWithValue("@invoiceno", lblInvoice.Text);
                            cmd1.Parameters.AddWithValue("@spid", listView1.Items[i].SubItems[0].Text);
                            cmd1.Parameters.AddWithValue("@description", listView1.Items[i].SubItems[2].Text);
                            cmd1.Parameters.AddWithValue("@qty", listView1.Items[i].SubItems[3].Text);
                            cmd1.Parameters.AddWithValue("@rate", listView1.Items[i].SubItems[4].Text);
                            cmd1.Parameters.AddWithValue("@total", listView1.Items[i].SubItems[5].Text);
                            cmd1.Parameters.AddWithValue("@gst", listView1.Items[i].SubItems[6].Text);
                            cmd1.Parameters.AddWithValue("@gstamt", listView1.Items[i].SubItems[7].Text);
                            cmd1.Parameters.AddWithValue("@totalamount", listView1.Items[i].SubItems[8].Text);
                            m= cmd1.ExecuteNonQuery();

                        }

                        if(m>0)
                        {

                            for (int jj = 0; jj < listView1.Items.Count; jj++)
                            {

                                query = "update tblsellproduct set stock=stock+@qty where  oid=@oid and spid=@pid";
                                SqlCommand cmd4 = new SqlCommand(query, con);
                                cmd4.Parameters.AddWithValue("@oid", lblid.Text);
                                cmd4.Parameters.AddWithValue("@pid", listView1.Items[jj].SubItems[0].Text);
                                cmd4.Parameters.AddWithValue("@qty", listView1.Items[jj].SubItems[3].Text);
                                k = cmd4.ExecuteNonQuery();


                            }
                            if (k > 0)
                            {
                                query = "update tbldealer set dopcredit=dopcredit+@remain where did='" + cmbDealer.SelectedValue + "' and oid='" + lblid.Text + "' ";
                                SqlCommand cmd2 = new SqlCommand(query, con);
                                cmd2.Parameters.AddWithValue("@remain", lblRemain.Text);
                                j = cmd2.ExecuteNonQuery();
                                if(j>0)
                                {
                                    if (cmbPayType.Text == "Cash")
                                    {
                                        query = "insert into tblbalancesheet (oid,date,type,typeid,description,credit,debit,year) values (@oid,@date,@type,@typeid,@description,@credit,@debit,@year)";
                                        SqlCommand cmd8 = new SqlCommand(query, con);
                                        cmd8.Parameters.AddWithValue("@oid", lblid.Text);
                                        cmd8.Parameters.AddWithValue("@date", dtPurchase.Value.ToString("dd-MM-yyyy"));
                                        cmd8.Parameters.AddWithValue("@type", "Purchase Account");
                                        cmd8.Parameters.AddWithValue("@typeid", cmbDealer.SelectedValue);
                                        cmd8.Parameters.AddWithValue("@description", " Give Payment To Dealer " + " " + cmbDealer.Text + " " + "Against Invoice No." + " " + lblInvoice.Text + ".");
                                        cmd8.Parameters.AddWithValue("@credit", "0.00");
                                        cmd8.Parameters.AddWithValue("@debit", txtPaid.Text);
                                        cmd8.Parameters.AddWithValue("@year", lblYear.Text);
                                        n = cmd8.ExecuteNonQuery();

                                        if (n > 0)
                                        {
                                            query = "select invoiceno  from tblpurchase  where oid='" + lblid.Text + "' and year='" + lblYear.Text + "' order by prid desc";
                                            SqlCommand cmd3 = new SqlCommand(query, con);
                                            SqlDataReader sdr = cmd3.ExecuteReader();
                                            if (sdr.Read())
                                            {
                                                maxid = Convert.ToString(sdr.GetValue(0));
                                            }
                                            sdr.Close();

                                            MessageBox.Show("Purchase Invoice Successfully Submitted", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            PurchaseReport(maxid);
                                            listView1.Items.Clear();
                                            lblFinalTotal.Text = "0";
                                            lblTotalQty.Text = "0";
                                            lblRemain.Text = "0";
                                            txtPaid.Text = "0";

                                            FrmPurchase_Load(sender, e);
                                        }
                                        

                            }
                                    else
                                    {
                                        query = "select invoiceno  from tblpurchase  where oid='" + lblid.Text + "' and year='" + lblYear.Text + "' order by prid desc";
                                        SqlCommand cmd3 = new SqlCommand(query, con);
                                        SqlDataReader sdr = cmd3.ExecuteReader();
                                        if (sdr.Read())
                                        {
                                            maxid = Convert.ToString(sdr.GetValue(0));
                                        }
                                        sdr.Close();

                                        MessageBox.Show("Purchase Invoice Successfully Submitted", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        PurchaseReport(maxid);
                                        listView1.Items.Clear();
                                        lblFinalTotal.Text = "0";
                                        lblTotalQty.Text = "0";
                                        lblRemain.Text = "0";
                                        txtPaid.Text = "0";

                                        FrmPurchase_Load(sender, e);
                                    }
                                }
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }

        private void lblFinalTotal_TextChanged(object sender, EventArgs e)
        {
            lblRemain.Text = lblFinalTotal.Text;
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

        private void FrmPurchase_KeyDown(object sender, KeyEventArgs e)
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

        private void lblInvoice_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblInvoice_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                query = "select count(prid) o from tblpurchase where invoiceno='"+lblInvoice.Text+"'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    count = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (count > 0)
                {
                    MessageBox.Show("Invoice No. Already Exist, Please Enter Another Invoice No.!!!", "" +cname+ "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblInvoice.Text = "";
                    lblInvoice.Focus();
                }

            }
            catch(Exception ex)
            {
            }
        }

        private void lblPrate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gst = Convert.ToDouble(txtGST.Text);
                double prate = Convert.ToDouble(lblPrate.Text);
                double total = Math.Round(prate-prate*(gst/(gst+100)));
                txtPrice.Text = total.ToString();

            }
            catch(Exception ex)
            {
            }
        }

        private void cmbDealer_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                query = "select count(did) from tbldealer where oid='" + lblid.Text + "' and dname='" + cmbDealer.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cust = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (cust == 0)
                {
                    MessageBox.Show("Dealer does not Exist. Please Register Dealer !!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmPurchase_Load(sender, e);
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
                    FrmPurchase_Load(sender, e);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            getDescription();
        }

       

    }
}
