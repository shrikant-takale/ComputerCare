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
    public partial class FrmRepair : Form
    {
        connection c = new connection();
        SqlConnection con;
        string cname, query,msg;
        int billno = 1000,  res, res1,res2, k, maxid,count,count1,n;
        System.Net.WebRequest WebRequest = null;
        System.Net.WebResponse WebResonse = null;

        public FrmRepair()
        {
            InitializeComponent();
        }

        private void FrmRepair_Load(object sender, EventArgs e)
        {
            getCompanyName();
            getInvoiceNo();
            cmbPayType.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            txtInward.Focus();
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
                query = "select count(rid) from tblrepair where oid='"+lblid.Text+"' and year='"+lblYear.Text+"'";
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
                    query = "select max(invoiceno) from tblrepair where oid='" + lblid.Text + "' and year='" + lblYear.Text + "'";
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

        private void txtInward_Leave(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                if (txtInward.Text == "")
                {
                }
                else
                {
                    query = "select count(inwid) from tblinworditem item, tblinword i where i.oid='" + lblid.Text + "' and i.inwordno='" + txtInward.Text + "'  and i.inwordno=item.inwordno and i.oid=item.oid and i.year='" + lblYear.Text + "' and i.year=item.year";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        count = Convert.ToInt32(sdr.GetValue(0));
                    }
                    sdr.Close();
                    query = "select  count(rid) from tblrepair r where r.oid='" + lblid.Text + "' and r.inwardno='" + txtInward.Text + "'  and  r.year='" + lblYear.Text + "' ";
                    SqlCommand cmd9 = new SqlCommand(query, con);
                    SqlDataReader sdr9 = cmd9.ExecuteReader();
                    while (sdr9.Read())
                    {
                        count1 = Convert.ToInt32(sdr9.GetValue(0));
                    }
                    sdr9.Close();
                    if (count == 0)
                    {
                        MessageBox.Show("Inward No. does not exist. Please Enter Correct Inward No.", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInward.Text = "";
                        txtInward.Focus();
                    }
                    else if (count1 > 0)
                    {
                        MessageBox.Show("Product with Inward no. " + "" + txtInward.Text + "" + " already delivered", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInward.Text = "";
                        txtInward.Focus();
                    
                    }

                    else
                    {
                        listView1.Columns.Clear();
                        listView1.Items.Clear();
                        listView1.Update(); // In case there is databinding
                        listView1.Refresh(); // Redraw items

                        listView1.View = View.Details;
                        listView1.GridLines = true;
                        listView1.FullRowSelect = true;


                        listView1.Columns.Add("rpid", 0);
                        listView1.Columns.Add("Product", 180);
                        listView1.Columns.Add("Description", 300);
                        listView1.Columns.Add("PID", 0);
                        listView1.Columns.Add("Problem", 250);
                        listView1.Columns.Add("Qty.", 60);
                        // listView1.Columns.Add("Service Tag No.", 160);

                        int i;

                        con = c.openConnection();

                        DataTable dt = new DataTable();
                        string[] arr = new string[14];
                        ListViewItem itm;

                        query = "select * from tblinworditem item, tblinword i where i.oid='" + lblid.Text + "' and i.inwordno='" + txtInward.Text + "'  and i.inwordno=item.inwordno and i.oid=item.oid and i.year='" + lblYear.Text + "' and i.year=item.year";
                        SqlCommand cmd1 = new SqlCommand(query, con);

                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        da.Fill(dt);

                        for (i = 0; i < dt.Rows.Count; i++)
                        {

                            arr[0] = Convert.ToString(dt.Rows[i]["rpid"]);
                            arr[1] = Convert.ToString(dt.Rows[i]["rproduct"]);
                            arr[2] = Convert.ToString(dt.Rows[i]["description"]);
                            arr[3] = Convert.ToString(dt.Rows[i]["pid"]);
                            arr[4] = Convert.ToString(dt.Rows[i]["problem"]);
                            arr[5] = Convert.ToString(dt.Rows[i]["qty"]);
                            txtServiceTagNo.Text = Convert.ToString(dt.Rows[i]["servicetagno"]);
                            lblCustomer.Text = Convert.ToString(dt.Rows[i]["customer"]);
                            lblCid.Text = Convert.ToString(dt.Rows[i]["cid"]);
                            lblContact.Text = Convert.ToString(dt.Rows[i]["contact"]);
                            lblCustGST.Text = Convert.ToString(dt.Rows[i]["gstno"]);
                            itm = new ListViewItem(arr);
                            listView1.Items.Add(itm);
                        }
                    }



                }
               
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
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
                double amount=Convert.ToDouble(txtAmount.Text);
                double gst=Convert.ToDouble(txtGST.Text);
                double gstamt=amount*gst/100;
                lblGST.Text=gstamt.ToString();
                double total=amount+gstamt;
                lblTAmt.Text=Convert.ToString(Math.Round(total));
            }
            catch(Exception ex)
            {

            }
        }

      
     

        private void txtPaid_Leave(object sender, EventArgs e)
        {
            try
            {
                double tamt = Convert.ToDouble(lblTAmt.Text);
                double paid = Convert.ToDouble(txtPaid.Text);
                double remain = tamt - paid;
                lblRemain.Text = remain.ToString();
            }
            catch(Exception ex)
            {
            }
        }
        private void RepairReport(int maxid)
        {
            FrmRptRepair q = new FrmRptRepair(maxid);
            this.Hide();
            q.ShowDialog();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                if (txtInward.Text == "" )
                {
                    MessageBox.Show("Please Enter Inward No.!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  
                    txtInward.Focus();
                }
                else if (lblCustomer.Text == "")
                {
                    MessageBox.Show("Please Enter Customer!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    lblCustomer.Focus();
                }
                else if (txtServiceTagNo.Text == "")
                {
                    MessageBox.Show("Please Enter Service Tag No.!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtServiceTagNo.Focus();
                }
                else if (txtAmount.Text == "")
                {
                    MessageBox.Show("Please Enter Repair Amount!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtAmount.Focus();
                }
                else if (txtPaid.Text == "" )
                {
                    MessageBox.Show("Please Enter Paid Amount!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaid.Text = "0";
                    txtPaid.Focus();
                }
                else
                {
                    if(cmbStatus.Text=="Repaired")
                    {
                    query = "insert into tblrepair (date,invoiceno,inwardno,cid,servicetagno,amt,gst,gstamt,total,paytype,paid,remain,oid,year,flag) values(@date,@invoiceno,@inwardno,@cid,@servicetagno,@amt,@gst,@gstamt,@total,@paytype,@paid,@remain,@oid,@year,@flag)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@date", dtRepair.Value.ToString("dd-MM-yyyy"));
                    cmd.Parameters.AddWithValue("@invoiceno", lblInvoice.Text);
                    cmd.Parameters.AddWithValue("@year", lblYear.Text);
                    cmd.Parameters.AddWithValue("@inwardno", txtInward.Text);
                 cmd.Parameters.AddWithValue("@cid", lblCid.Text);
                  cmd.Parameters.AddWithValue("@servicetagno", txtServiceTagNo.Text);
                    cmd.Parameters.AddWithValue("@amt", txtAmount.Text);
                    cmd.Parameters.AddWithValue("@gst", txtGST.Text);
                    cmd.Parameters.AddWithValue("@gstamt", lblGST.Text);
                    cmd.Parameters.AddWithValue("@total", lblTAmt.Text);
                    cmd.Parameters.AddWithValue("@paytype", cmbPayType.Text);
                    cmd.Parameters.AddWithValue("@paid", txtPaid.Text);
                    cmd.Parameters.AddWithValue("@remain", lblRemain.Text);
                    cmd.Parameters.AddWithValue("@oid", lblid.Text);
                    if (chkGST.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@flag", 1);
                    }
                    else if (chkGST.Checked == false)
                    {
                        cmd.Parameters.AddWithValue("@flag", 0);
                    }
                    res1 = cmd.ExecuteNonQuery();
                    if(res1>0)
                    {
                        for (int jj = 0; jj < listView1.Items.Count; jj++)
                        {
                            query = "insert into tblrepairitem (invoiceno,rpid,description,pid, qty,oid,year) values(@invoiceno,@rpid,@description,@pid, @qty,@oid,@year)";
                            SqlCommand cmd2 = new SqlCommand(query, con);
                            //cmd.Parameters.AddWithValue("@date", dtRepair.Value.ToString("dd-MM-yyyy"));
                            cmd2.Parameters.AddWithValue("@invoiceno", lblInvoice.Text);
                            cmd2.Parameters.AddWithValue("@year", lblYear.Text);
                            cmd2.Parameters.AddWithValue("@oid", lblid.Text);
                          //  cmd.Parameters.AddWithValue("@cid", lblCid.Text);
                            cmd2.Parameters.AddWithValue("@rpid", listView1.Items[jj].SubItems[0].Text);
                            cmd2.Parameters.AddWithValue("@description", listView1.Items[jj].SubItems[2].Text);
                            cmd2.Parameters.AddWithValue("@pid", listView1.Items[jj].SubItems[3].Text);
                            cmd2.Parameters.AddWithValue("@qty", listView1.Items[jj].SubItems[5].Text);
                           // cmd.Parameters.AddWithValue("@servicetagno", listView1.Items[jj].SubItems[6].Text);
                            res = cmd2.ExecuteNonQuery();
                        }

                        if (res > 0)
                        {


                            query = "update tblcustomer set copcredit=copcredit+'" + lblRemain.Text + "' where cid='" + lblCid.Text + "'";
                            SqlCommand cmd1 = new SqlCommand(query, con);
                            res2 = cmd1.ExecuteNonQuery();

                            if (res2 > 0)
                            {
                               
                                    query = "insert into tblbalancesheet (oid,date,type,typeid,description,credit,debit,year) values (@oid,@date,@type,@typeid,@description,@credit,@debit,@year)";
                                    SqlCommand cmd8 = new SqlCommand(query, con);
                                    cmd8.Parameters.AddWithValue("@oid", lblid.Text);
                                    cmd8.Parameters.AddWithValue("@date", dtRepair.Value.ToString("dd-MM-yyyy"));
                                    cmd8.Parameters.AddWithValue("@type", "Repair Account");
                                    cmd8.Parameters.AddWithValue("@typeid", lblCid.Text);
                                    cmd8.Parameters.AddWithValue("@description", "Repair Payment From Customer " + " " + lblCustomer.Text + " " + " Against Invoice No. " + "" + lblInvoice.Text + ".");
                                    cmd8.Parameters.AddWithValue("@credit", txtPaid.Text);
                                    cmd8.Parameters.AddWithValue("@debit", "0.00");
                                    cmd8.Parameters.AddWithValue("@year", lblYear.Text);
                                    k = cmd8.ExecuteNonQuery();
                                    if (k > 0)
                                    {
                                        query = "insert into tbltransaction (oid,date,type,cid,cname,gstno,invoiceno,gst,amount,year) values (@oid,@date,@type,@cid,@cname,@gstno,@invoiceno,@gst,@amount,@year)";
                                        SqlCommand cmd9 = new SqlCommand(query, con);
                                        cmd9.Parameters.AddWithValue("@oid", lblid.Text);
                                        cmd9.Parameters.AddWithValue("@date", dtRepair.Value.ToString("dd-MM-yyyy"));
                                        cmd9.Parameters.AddWithValue("@cid", lblCid.Text);
                                        cmd9.Parameters.AddWithValue("@cname", lblCustomer.Text);
                                        cmd9.Parameters.AddWithValue("@gstno", lblCustGST.Text);
                                        cmd9.Parameters.AddWithValue("@invoiceno", lblInvoice.Text);
                                        cmd9.Parameters.AddWithValue("@gst", lblGST.Text);
                                        cmd9.Parameters.AddWithValue("@amount", lblTAmt.Text);
                                        cmd9.Parameters.AddWithValue("@year", lblYear.Text);
                                        cmd9.Parameters.AddWithValue("@type", "Repair Account");
                                        n = cmd9.ExecuteNonQuery();
                                        
                                        if (k > 0)
                                        {
                                            query = "select max(invoiceno) from tblrepair where oid='" + lblid.Text + "' and year='" + lblYear.Text + "'";
                                            SqlCommand cmd3 = new SqlCommand(query, con);
                                            SqlDataReader sdr = cmd3.ExecuteReader();
                                            if (sdr.Read())
                                            {
                                                maxid = Convert.ToInt32(sdr.GetValue(0));
                                            }
                                            sdr.Close();
                                            try
                                            {
                                                msg = "Dear Customer, Your Product with Inward No. " + txtInward.Text + " is Ready and Your Bill Amount is " + lblTAmt.Text + " Rs. Computer Care, Baramati ";
                                                string WebResponseString = "";
                                                string URL = "http://mysms.ynorme.com:8080/sendsms/bulksms?username=yno-scoregraph&password=97306242&type=0&dlr=1&destination=" + lblContact.Text + "&source=COMPCR&message=" + msg + "";
                                                WebRequest = System.Net.HttpWebRequest.Create(URL);
                                                WebRequest.Timeout = 25000;
                                                WebResonse = WebRequest.GetResponse();
                                                System.IO.StreamReader reader = new System.IO.StreamReader(WebResonse.GetResponseStream());
                                                WebResponseString = reader.ReadToEnd();
                                                WebResonse.Close();
                                                MessageBox.Show("Message Sent Successfully", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                MessageBox.Show(" Repair Invoice  Inserted Successfully!!! Please Take Print!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                RepairReport(maxid);
                                                txtAmount.Text = "0";
                                                lblGST.Text = "0";
                                                txtGST.Text = "0";
                                                lblCustomer.Text = "";
                                                lblTAmt.Text = "0";
                                                txtPaid.Text = "0";
                                                lblRemain.Text = "0";
                                                FrmRepair_Load(sender, e);
                                            }
                                            catch (System.Net.WebException)
                                            {
                                                MessageBox.Show("Unable to Send Message due to Internet Connection", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                MessageBox.Show(" Repair Invoice Generated Successfully!!! Please Take Print!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                RepairReport(maxid);
                                                txtAmount.Text = "0";
                                                lblGST.Text = "0";
                                                txtGST.Text = "0";
                                                txtInward.Text = "";
                                                lblCustomer.Text = "";
                                                txtServiceTagNo.Text = "";
                                                lblTAmt.Text = "0";
                                                txtPaid.Text = "0";
                                                lblRemain.Text = "0";
                                                FrmRepair_Load(sender, e);
                                            }
                                        }
                                    }
                                    }
                              
                            }
                        }
                    }
                    else if (cmbStatus.Text == "Not Repaired")
                    {
                        query = "insert into tblrepair (date,invoiceno,inwardno,cid,servicetagno,amt,gst,gstamt,total,paytype,paid,remain,oid,year,flag,status) values(@date,@invoiceno,@inwardno,@cid,@servicetagno,@amt,@gst,@gstamt,@total,@paytype,@paid,@remain,@oid,@year,@flag,@status)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@date", dtRepair.Value.ToString("dd-MM-yyyy"));
                        cmd.Parameters.AddWithValue("@invoiceno", lblInvoice.Text);
                        cmd.Parameters.AddWithValue("@year", lblYear.Text);
                        cmd.Parameters.AddWithValue("@inwardno", txtInward.Text);
                        cmd.Parameters.AddWithValue("@cid", lblCid.Text);
                        cmd.Parameters.AddWithValue("@servicetagno", txtServiceTagNo.Text);
                        cmd.Parameters.AddWithValue("@amt", txtAmount.Text);
                        cmd.Parameters.AddWithValue("@gst", txtGST.Text);
                        cmd.Parameters.AddWithValue("@gstamt", lblGST.Text);
                        cmd.Parameters.AddWithValue("@total", lblTAmt.Text);
                        cmd.Parameters.AddWithValue("@paytype", cmbPayType.Text);
                        cmd.Parameters.AddWithValue("@paid", txtPaid.Text);
                        cmd.Parameters.AddWithValue("@remain", lblRemain.Text);
                        cmd.Parameters.AddWithValue("@oid", lblid.Text);
                        cmd.Parameters.AddWithValue("@status", 1);
                        if (chkGST.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@flag", 1);
                        }
                        else if (chkGST.Checked == false)
                        {
                            cmd.Parameters.AddWithValue("@flag", 0);
                        }

                        res1 = cmd.ExecuteNonQuery();
                        if (res1 > 0)
                        {
                            for (int jj = 0; jj < listView1.Items.Count; jj++)
                            {
                                query = "insert into tblrepairitem (invoiceno,rpid,description,pid, qty,oid,year) values(@invoiceno,@rpid,@description,@pid, @qty,@oid,@year)";
                                SqlCommand cmd2 = new SqlCommand(query, con);
                                //cmd.Parameters.AddWithValue("@date", dtRepair.Value.ToString("dd-MM-yyyy"));
                                cmd2.Parameters.AddWithValue("@invoiceno", lblInvoice.Text);
                                cmd2.Parameters.AddWithValue("@year", lblYear.Text);
                                cmd2.Parameters.AddWithValue("@oid", lblid.Text);
                                //  cmd.Parameters.AddWithValue("@cid", lblCid.Text);
                                cmd2.Parameters.AddWithValue("@rpid", listView1.Items[jj].SubItems[0].Text);
                                cmd2.Parameters.AddWithValue("@description", listView1.Items[jj].SubItems[2].Text);
                                cmd2.Parameters.AddWithValue("@pid", listView1.Items[jj].SubItems[3].Text);
                                cmd2.Parameters.AddWithValue("@qty", listView1.Items[jj].SubItems[5].Text);
                                // cmd.Parameters.AddWithValue("@servicetagno", listView1.Items[jj].SubItems[6].Text);
                                res = cmd2.ExecuteNonQuery();
                            }

                            if (res > 0)
                            {
                                MessageBox.Show("Product information saved Succeessfully", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtAmount.Text = "0";
                                txtInward.Text = "";
                                lblGST.Text = "0";
                                txtGST.Text = "0";
                                lblCustomer.Text = "";
                                lblTAmt.Text = "0";
                                txtPaid.Text = "0";
                                lblRemain.Text = "0";
                                txtServiceTagNo.Text = "";
                                listView1.Clear();
                                FrmRepair_Load(sender,e);
                            }
                        }
                    }
                 
                }
            }
            catch (Exception ee)
            {
              //  MessageBox.Show(ee.Message);
            }
        }

        private void lblTAmt_TextChanged(object sender, EventArgs e)
        {
            lblRemain.Text = lblTAmt.Text;
        }

        private void FrmRepair_KeyDown(object sender, KeyEventArgs e)
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

        private void chkGST_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGST.Checked == true)
            {
                txtGST.Enabled = false;
                lblGST.Enabled = false;
             //   lblTAmt.Text = txtAmount.Text;
            }
            else if (chkGST.Checked == false)
            {
                txtGST.Enabled = true;
                lblGST.Enabled = true;
            }
         
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                if (chkGST.Checked == true)
                {
                    txtGST.Enabled = false;
                    lblGST.Enabled = false;
                    lblTAmt.Text = txtAmount.Text;
                }
                else if(chkGST.Checked==false)
                {
                    txtGST.Enabled = true;
                    lblGST.Enabled = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbStatus.Text == "Not Repaired")
                {
                    chkGST.Enabled = false;
                    txtAmount.Enabled = false;
                    txtGST.Enabled = false;
                    lblGST.Enabled = false;
                    lblTAmt.Enabled = false;
                    cmbPayType.Enabled = false;
                    txtPaid.Enabled = false;
                    lblRemain.Enabled = false;
                }
                else if (cmbStatus.Text == "Repaired")
                {
                    chkGST.Enabled = true;
                    txtAmount.Enabled = true;
                    txtGST.Enabled = true;
                    lblGST.Enabled = true;
                    lblTAmt.Enabled = true;
                    cmbPayType.Enabled = true;
                    txtPaid.Enabled = true;
                    lblRemain.Enabled = true;
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
