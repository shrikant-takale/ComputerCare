using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComputerCare.Login;
using ComputerCare.Connections;
using ComputerCare.Masters;
using ComputerCare.Utilities;
using ComputerCare.Reports;
using ComputerCare.Operations;
using System.Data.SqlClient;

namespace ComputerCare
{
    public partial class ComputerCare : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        //int i, id;
        public ComputerCare()
        {
            InitializeComponent();
        }

        private void ComputerCare_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmLogin l = new FrmLogin();
            l.Show();
            this.Hide();
        }

        private void ComputerCare_Load(object sender, EventArgs e)
        {
          getCompanyName();
          Customer.Focus();
        }
        private void getCompanyName()
        {
            try
            {
                con = c.openConnection();
                query = "select name, year from tblsession";
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

        private void ComputerCare_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                }
                if (e.KeyCode == Keys.Escape)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you  want to Close this window ?", "Company", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
                if (e.KeyCode == Keys.C)
                {
                    FrmCustomer c= new FrmCustomer();
                    c.ShowDialog();
                }
                if (e.KeyCode == Keys.D)
                {
                     FrmDealer d = new FrmDealer();
                     d.ShowDialog();
                }
                if (e.KeyCode == Keys.S)
                {
                    FrmSellProduct sl = new FrmSellProduct();
                    sl.ShowDialog();
                }
                if (e.KeyCode == Keys.R)
                {
                    FrmRepairProduct r = new FrmRepairProduct();
                   r.ShowDialog();
                }

                if (e.KeyCode == Keys.P)
                {
                    FrmProblem p= new FrmProblem();
                    p.ShowDialog();
                }


                if (e.KeyCode == Keys.I)
                {
                    FrmInword c = new FrmInword();
                    c.ShowDialog();
                }
                if (e.KeyCode == Keys.Q)
                {
                    FrmQuotation q = new FrmQuotation();
                    q.ShowDialog();
                }
                if (e.KeyCode == Keys.U)
                {
                    FrmPurchase p = new FrmPurchase();
                    p.ShowDialog();
                }
                if (e.KeyCode == Keys.L)
                {
                    FrmSell s = new FrmSell();
                    s.ShowDialog();
                }

                if (e.KeyCode == Keys.A)
                {
                    FrmRepair a = new FrmRepair();
                    a.ShowDialog();
                }
                if (e.KeyCode == Keys.E)
                {
                    FrmExpense exp = new FrmExpense();
                    exp.ShowDialog();
                }

                if (e.KeyCode == Keys.T)
                {
                    FrmReceipt t= new FrmReceipt();
                    t.ShowDialog();
                }

                if (e.KeyCode == Keys.M)
                {
                    FrmCreditSMS cr = new FrmCreditSMS();
                    cr.ShowDialog();
                }

                if (e.KeyCode == Keys.G)
                {
                    FrmGroupSMS grp = new FrmGroupSMS();
                    grp.ShowDialog();
                }
                if (e.KeyCode == Keys.F2)
                {
                    FrmRptTransaction c = new FrmRptTransaction();
                    c.ShowDialog();
                }
                if (e.KeyCode == Keys.F3)
                {
                    FrmRptCustomerSell c = new FrmRptCustomerSell();
                    c.ShowDialog();
                }
                if (e.KeyCode == Keys.F4)
                {
                    FrmRptDealerPurchase d = new FrmRptDealerPurchase();
                    d.ShowDialog();
                }
                if (e.KeyCode == Keys.F5)
                {
                    FrmRptCredit sl = new FrmRptCredit();
                    sl.ShowDialog();
                }
                if (e.KeyCode == Keys.F6)
                {
                    FrmRptStock r = new FrmRptStock();
                    r.ShowDialog();
                }

                if (e.KeyCode == Keys.F7)
                {
                    FrmRptInward p = new FrmRptInward();
                    p.ShowDialog();
                }


                if (e.KeyCode == Keys.F8)
                {
                    FrmRptQuotation c = new FrmRptQuotation();
                    c.ShowDialog();
                }
                if (e.KeyCode == Keys.F9)
                {
                    FrmRptRepairReport q = new FrmRptRepairReport();
                    q.ShowDialog();
                }
                if (e.KeyCode == Keys.F10)
                {
                    FrmRptExpense p = new FrmRptExpense();
                    p.ShowDialog();
                }
                if (e.KeyCode == Keys.F11)
                {
                    FrmRptReceiptReport s = new FrmRptReceiptReport();
                    s.ShowDialog();
                }

                if (e.KeyCode == Keys.F12)
                {
                    FrmRptTrialBalance a = new FrmRptTrialBalance();
                    a.ShowDialog();
                }
            }
            catch(Exception ex)
            {
            }
        }

        private void Customer_Click(object sender, EventArgs e)
        {

            FrmCustomer c = new FrmCustomer();
            c.ShowDialog();
        }

        private void Dealer_Click(object sender, EventArgs e)
        {
            FrmDealer d = new FrmDealer();
            d.ShowDialog();
        }

        private void SellProduct_Click(object sender, EventArgs e)
        {
            FrmSellProduct sl = new FrmSellProduct();
            sl.ShowDialog();
        }

        private void RepairProduct_Click(object sender, EventArgs e)
        {
            FrmRepairProduct rp = new FrmRepairProduct();
            rp.ShowDialog();
        }

        private void Problem_Click(object sender, EventArgs e)
        {
            FrmProblem p = new FrmProblem();
            p.ShowDialog();
        }

        private void Inword_Click(object sender, EventArgs e)
        {
            FrmInword iw = new FrmInword();
            iw.ShowDialog();
        }

        private void Quotation_Click(object sender, EventArgs e)
        {
            FrmQuotation q = new FrmQuotation();
            q.ShowDialog();
        }

        private void Purchase_Click(object sender, EventArgs e)
        {
            FrmPurchase p = new FrmPurchase();
            p.ShowDialog();
        }

        private void Sell_Click(object sender, EventArgs e)
        {
            FrmSell s = new FrmSell();
            s.ShowDialog();
        }

        private void Expense_Click(object sender, EventArgs e)
        {
            FrmExpense exp = new FrmExpense();
            exp.ShowDialog();
        }

        private void Receipt_Click(object sender, EventArgs e)
        {
            FrmReceipt r = new FrmReceipt();
            r.ShowDialog();
        }

        private void BackupRestore_Click(object sender, EventArgs e)
        {
            FrmBackupRestore bs = new FrmBackupRestore();
            bs.ShowDialog();
        }

        private void PasswordChange_Click(object sender, EventArgs e)
        {
            FrmChangePassword cp = new FrmChangePassword();
            cp.ShowDialog();
        }

        private void EditCompany_Click(object sender, EventArgs e)
        {
            FrmUpdateCompany uc = new FrmUpdateCompany();
            uc.ShowDialog();
        }

        private void Repair_Click(object sender, EventArgs e)
        {
            FrmRepair r = new FrmRepair();
            r.ShowDialog();
        }

        private void CustomerRpt_Click(object sender, EventArgs e)
        {
            FrmRptCustomerSell s = new FrmRptCustomerSell();
            s.ShowDialog();
        }

        private void PurchaseRpt_Click(object sender, EventArgs e)
        {
            FrmRptDealerPurchase d = new FrmRptDealerPurchase();
            d.ShowDialog();
        }

        private void CreditRpt_Click(object sender, EventArgs e)
        {
            FrmRptCredit c = new FrmRptCredit();
            c.ShowDialog();
        }

        private void Stock_Click(object sender, EventArgs e)
        {
            FrmRptStock s = new FrmRptStock();
            s.ShowDialog();
        }

        private void QuotationReport_Click(object sender, EventArgs e)
        {
            FrmRptQuotation q = new FrmRptQuotation();
            q.ShowDialog();
        }

        private void Inward_Click(object sender, EventArgs e)
        {
            FrmRptInward i = new FrmRptInward();
            i.ShowDialog();
        }

        private void RepairReport_Click(object sender, EventArgs e)
        {
            FrmRptRepairReport r = new FrmRptRepairReport();
            r.ShowDialog();
        }

        private void ExpenseReport_Click(object sender, EventArgs e)
        {
            FrmRptExpense exp = new FrmRptExpense();
            exp.ShowDialog();
        }

        private void ReceiptReport_Click(object sender, EventArgs e)
        {
            FrmRptReceiptReport r = new FrmRptReceiptReport();
            r.ShowDialog();
        }

        private void TrialBalance_Click(object sender, EventArgs e)
        {
            FrmRptTrialBalance t = new FrmRptTrialBalance();
            t.ShowDialog();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void CreditSMS_Click(object sender, EventArgs e)
        {
            FrmCreditSMS cr = new FrmCreditSMS();
            cr.ShowDialog();
        }

        private void GroupSMS_Click(object sender, EventArgs e)
        {
            FrmGroupSMS grp = new FrmGroupSMS();
            grp.ShowDialog();
        }

        private void Transaction_Click(object sender, EventArgs e)
        {
            FrmRptTransaction tr = new FrmRptTransaction();
            tr.ShowDialog();
        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

  

      

       
    }
}
