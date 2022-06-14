using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Windows.Forms;
using System.Data.SqlClient;
using ComputerCare.Connections;



namespace ComputerCare.Operations
{
    public partial class FrmGroupSMS : Form
    {
        connection c = new connection();
        SqlConnection con = null;
        string query, cname, msg;
        int i, billno = 1, j, k, m, l, maxid, n, o, count;
        System.Net.WebRequest WebRequest = null;
        System.Net.WebResponse WebResonse = null;

        public FrmGroupSMS()
        {
            InitializeComponent();
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

        private void getContacts()
        {
            try
            {
                con = c.openConnection();
                 DataTable dt = new DataTable();
                string[] arr = new string[14];
                
                query = "select ccontact from tblcustomer where oid='" + lblid.Text + "'";
                SqlCommand cmd1 = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(dt);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    if (i ==0)
                    {
                        arr[0] = Convert.ToString(dt.Rows[i]["ccontact"]);
                        txtContact.Text =  arr[0];
                    }
                    else
                    {
                        arr[0] = Convert.ToString(dt.Rows[i]["ccontact"]);
                        txtContact.Text = txtContact.Text + "," + arr[0];
                    }
                }
                string ReturnStr = txtContact.Text;
                string temp = "";
                ReturnStr.Split(',').Distinct().ToList().ForEach(k => temp += k + ",");
                ReturnStr = temp.Trim(',');
                txtContact.Text = ReturnStr;

              
      
            }
            catch(Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }

        private long Join(string p, IEnumerable<string> iEnumerable)
        {
            throw new NotImplementedException();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void FrmGroupSMS_Load(object sender, EventArgs e)
        {
            getCompanyName();
            getContacts();
            txtSMS.Focus();
        }

        private void FrmGroupSMS_KeyDown(object sender, KeyEventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSMS.Text == "" )
                {
                    MessageBox.Show("Please Enter SMS", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSMS.Focus();
                }
                else if (txtContact.Text == "")
                {
                    MessageBox.Show("Please Enter Contact Nos.", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContact.Focus();
                }
                else
                {

                    string strcontact = txtContact.Text;
                    string[] contact = strcontact.Split(',');
                    for (int i = 0; i < contact.Length; ++i)
                    {
                        msg = txtSMS.Text;
                        string WebResponseString = "";
                        string URL = "http://mysms.ynorme.com:8080/sendsms/bulksms?username=yno-scoregraph&password=97306242&type=0&dlr=1&destination=" + txtContact.Text + "&source=COMPCR&message=" + msg + "";
                        //string URL = "http://mysms.ynorme.com:8080/sendsms/bulksms?username=yno-nabagade&password=63542012&type=0&dlr=1&destination=" + contact[i].ToString() + "&source=YNORME&message=" + msg + "";
                        WebRequest = System.Net.HttpWebRequest.Create(URL);
                        WebRequest.Timeout = 25000;
                        WebResonse = WebRequest.GetResponse();
                        System.IO.StreamReader reader = new System.IO.StreamReader(WebResonse.GetResponseStream());
                        WebResponseString = reader.ReadToEnd();
                        WebResonse.Close();
                    }
                    
                    MessageBox.Show("Your SMS Send Successfully to all Customers", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtContact.Text = "";
                    txtSMS.Text = "";
                    FrmGroupSMS_Load(sender, e);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please Check Internet Connectivity..!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void txtContact_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtContact.Text.EndsWith(","))
                {
                    MessageBox.Show("You Cannot end with ',' ");
                    string text = txtContact.Text;
                    txtContact.Text=text.TrimEnd(',');
                    txtContact.Focus();

                  
                }
                else  if (txtContact.Text.EndsWith(";"))
                {
                    MessageBox.Show("You Cannot end with ';' ");
                    string text = txtContact.Text;
                    txtContact.Text = text.TrimEnd(';');
                    txtContact.Focus();


                }
            }
            catch(Exception ex)
            {
            }
        }
    }
}
