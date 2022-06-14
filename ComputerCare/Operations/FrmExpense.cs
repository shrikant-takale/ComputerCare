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

namespace ComputerCare.Operations
{
    public partial class FrmExpense : Form
    {

        connection c = new connection();
        SqlConnection con;
        string query, cname;
        int billno = 1000, i, j,k;

        public FrmExpense()
        {
            InitializeComponent();
        }

        private void FrmExpense_Load(object sender, EventArgs e)
        {
            getCompanyName();
            getID();

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
                    lblid1.Text = sdr1.GetValue(0).ToString();
                }
                sdr1.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void getID()
        {
            try
            {
                int cc = 0;
                con = c.openConnection();
                query = "select count(expid) from tblexp where oid='"+lblid1.Text+"' and year='"+lblYear.Text+"'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cc = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (cc == 0)
                {
                    lblID.Text = Convert.ToString(billno);
                }
                else
                {
                    query = "select max(expno) from tblexp where oid='" + lblid1.Text + "' and year='" + lblYear.Text + "'";
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    SqlDataReader sdr1 = cmd1.ExecuteReader();
                    if (sdr1.Read())
                    {
                        billno = 1 + Convert.ToInt32(sdr1.GetValue(0));
                        lblID.Text = Convert.ToString(billno);
                    }
                    sdr1.Close();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtExpenceFor.Text == "")
                {
                    MessageBox.Show("Please Enter Expense", ""+cname+"", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtExpenceFor.Focus();
                }
                else if (txtAmount.Text == "" || txtAmount.Text=="0")
                {
                    MessageBox.Show("Please Enter Amount", ""+cname+"", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAmount.Focus();
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


                    listView1.Columns.Add("Expense For", 200);
                    listView1.Columns.Add("Amount", 150);

                    string[] arr = new string[20];
                    ListViewItem itm;


                    arr[0] = txtExpenceFor.Text;
                    arr[1] = txtAmount.Text;


                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);

                    double t = 0.0;
                    int i;
                    int p = listView1.Items.Count;
                    for (i = 0; i < p; i++)
                    {
                        t = t + Convert.ToDouble(listView1.Items[i].SubItems[1].Text);
                    }

                    lblTotal.Text = Convert.ToString(Math.Round(t));

                    txtExpenceFor.Text = "";
                    txtAmount.Text = "0";
                    txtExpenceFor.Focus();
                }
            }
            catch (Exception ee)
            { }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                double t = 0.0;

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
                            t = t + Convert.ToDouble(listView1.Items[i].SubItems[1].Text);
                        }

                        lblTotal.Text = Convert.ToString(t);
                    }
                }
                else
                {
                    MessageBox.Show("Please select atleast one record to remove", ""+cname+"", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ee)
            {
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.Items.Count == 0)
                {
                    MessageBox.Show("Please Add Atleast One Product", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtExpenceFor.Focus();
                }
                else
                {
                    con = c.openConnection();
                    query = "insert into tblexp (expno,date,total,oid,year) values (@expno,@date,@total,@oid,@year)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@expno", lblID.Text);
                    cmd.Parameters.AddWithValue("@year", lblYear.Text);
                    cmd.Parameters.AddWithValue("@date", dtExpence.Value.ToString("dd-MM-yyyy"));
                    cmd.Parameters.AddWithValue("@total", lblTotal.Text);
                    cmd.Parameters.AddWithValue("@oid", lblid1.Text);
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        for (i = 0; i < listView1.Items.Count; i++)
                        {
                            query = "insert into tblexpitem (expno,expense,amount,oid,year) values (@expno,@expensefor,@amount,@oid,@year)";
                            SqlCommand cmd1 = new SqlCommand(query, con);
                            cmd1.Parameters.AddWithValue("@expno", lblID.Text);
                            cmd1.Parameters.AddWithValue("@year", lblYear.Text);
                            cmd1.Parameters.AddWithValue("@oid", lblid1.Text);
                            cmd1.Parameters.AddWithValue("@expensefor", listView1.Items[i].SubItems[0].Text);
                            cmd1.Parameters.AddWithValue("@amount", listView1.Items[i].SubItems[1].Text);

                            j = cmd1.ExecuteNonQuery();
                        }
                        if (j > 0)
                        {
                            for (int ii = 0; ii < listView1.Items.Count; ii++)
                            {
                                query = "insert into tblbalancesheet (oid,date,type,typeid,description,credit,debit,year) values (@oid,@date,@type,@typeid,@description,@credit,@debit,@year)";
                                SqlCommand cmd8 = new SqlCommand(query, con);
                                cmd8.Parameters.AddWithValue("@oid", lblid1.Text);
                                cmd8.Parameters.AddWithValue("@date", dtExpence.Value.ToString("dd-MM-yyyy"));
                                cmd8.Parameters.AddWithValue("@type", "Expense Account");
                                cmd8.Parameters.AddWithValue("@typeid", "");
                                cmd8.Parameters.AddWithValue("@description", " Expense Payment To " + listView1.Items[ii].SubItems[0].Text + " Against Receipt No. " + lblID.Text + ".");
                                cmd8.Parameters.AddWithValue("@credit", "0.00");
                                cmd8.Parameters.AddWithValue("@debit", listView1.Items[ii].SubItems[1].Text);
                                cmd8.Parameters.AddWithValue("@year", lblYear.Text);
                                k= cmd8.ExecuteNonQuery();
                            }
                            if (k > 0)
                            {
                                MessageBox.Show("Expense  Entry Successfully Submitted", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                listView1.Items.Clear();
                                lblTotal.Text = "0";
                                FrmExpense_Load(sender,e);
                            }

                        }
                    }
                }
            }
            catch (Exception ee)
            {
            }

        }

        private void FrmExpense_KeyDown(object sender, KeyEventArgs e)
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
    }
}
