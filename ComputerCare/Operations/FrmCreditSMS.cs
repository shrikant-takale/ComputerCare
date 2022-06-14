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

namespace ComputerCare.Operations
{
    public partial class FrmCreditSMS : Form
    {
        connection c = new connection();
        SqlConnection con = null;
        string query, cname, msg;
        int i, billno = 1, j, k, m, l, maxid, n, o, count;
           string  contact; double credit;
        System.Net.WebRequest WebRequest = null;
        System.Net.WebResponse WebResonse = null;
        public FrmCreditSMS()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCreditSMS_Load(object sender, EventArgs e)
        {
            getCompanyName();
            //getCustomer();
            //getDescription();
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

    

     
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                con = c.openConnection();
                query = "select ccontact, copcredit from tblcustomer where oid='"+lblid.Text+"' and copcredit>0";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    contact= Convert.ToString(sdr.GetValue(0));
                    credit = Convert.ToDouble(sdr.GetValue(1));
                    msg = "Dear Customer, Your Credit Balance is " + credit + " Rs. Please pay your Due Payment, Computer Care, Baramati ";
                    string WebResponseString = "";
                     string URL = "http://mysms.ynorme.com:8080/sendsms/bulksms?username=yno-scoregraph&password=97306242&type=0&dlr=1&destination=" + contact + "&source=COMPCR&message=" + msg + "";
                    //string URL = "http://mysms.ynorme.com:8080/sendsms/bulksms?username=yno-nabagade&password=63542012&type=0&dlr=1&destination=" + contact + "&source=YNORME&message=" + msg + "";
                    WebRequest = System.Net.HttpWebRequest.Create(URL);
                    WebRequest.Timeout = 25000;
                    WebResonse = WebRequest.GetResponse();
                    System.IO.StreamReader reader = new System.IO.StreamReader(WebResonse.GetResponseStream());
                    WebResponseString = reader.ReadToEnd();
                    WebResonse.Close();
                   
                }
                sdr.Close();
             
                MessageBox.Show("Credit SMS Send Successfully", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                

                FrmCreditSMS_Load(sender, e);

            }
            catch(Exception ex)
            {
                MessageBox.Show("Please Check Internet Connectivity..!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmCreditSMS_KeyDown(object sender, KeyEventArgs e)
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
