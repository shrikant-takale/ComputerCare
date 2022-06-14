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

namespace ComputerCare.Utilities
{
    public partial class FrmBackupRestore : Form
    {
        connection c = new connection();
        SqlConnection con;

        public FrmBackupRestore()
        {
            InitializeComponent();
        }

        private void FrmBackupRestore_Load(object sender, EventArgs e)
        {
            tabbuk.Focus();
        }

        private void btnbrowse_Click(object sender, EventArgs e)
        {
            try
            {
                string ManthYear = DateTime.Today.Month + "_" + DateTime.Today.Year;
                string Date = DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year;
                String input = string.Empty;
                String strFileName = "";
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.InitialDirectory = @"D:\DatabaseBackup\" ; dialog.Title = "Select a bak file";

                dialog.Filter = "bak files (*.bak)|*.bak"; //|All files (*.*)|*.*

                if (dialog.ShowDialog() == DialogResult.OK)
                    txtfilename.Text = dialog.FileName;
                strFileName = dialog.FileName;
                if (strFileName == String.Empty)
                    return;

            }

            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                GetBackup();

            }
            catch (SqlException ex)
            {
                //MessageBox.Show("DataBase Backup :" + ex.Message, "CompCare");
            }
        }

        private void GetBackup()
        {
            try
            {
                timer1.Enabled = true;
                con = c.openConnection();
                //  dr = new SqlDataReader();
                //con.connopen();

                DateTime now = DateTime.Now;
                String MontName = now.ToString("MMM");

                string ManthYear = now.ToString("MMM") + "_" + DateTime.Today.Year;
                string Date = DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year;

                string targetPath = @"D:\DatabaseBackup";
                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }

                targetPath = @"D:\DatabaseBackup\Data_Backup_" + Date;
                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }

             

                SqlCommand cmd = new SqlCommand(@"BACKUP DATABASE dbComputerCare TO DISK='" + targetPath + "\\dbComputerCare.bak'", con);
                cmd.ExecuteNonQuery();
                con.Close();


            }
            catch (SqlException ex)
            {
               // MessageBox.Show("DataBase Backup :" + ex.Message, "CompCare");
            }
        }

        private void btnrestore_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtfilename.Text == "")
                {
                    MessageBox.Show("Please Browse Backup File TO RESTORE");
                    return;
                }
                else
                {
                    timer2.Enabled = true;
                    Cursor.Current = Cursors.WaitCursor;

                    //String CNSTR = @"Data Source=.\SQLEXPRESS;Initial Catalog=master ;Integrated Security=True;;";
                    //SqlConnection CN = new SqlConnection(CNSTR);
                    //CN = c.openConnection();                
                    con = c.openConnection();

                    SqlCommand cmd = new SqlCommand("use master", con);
                    cmd.ExecuteNonQuery();
                    String Str = @"RESTORE DATABASE  [dbComputerCare]  FROM DISK = '" + txtfilename.Text + "' WITH REPLACE";

                    cmd = new SqlCommand(Str, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (progressBar2.Value >= 200)
            {
                timer2.Enabled = false;
                MessageBox.Show("Database Restore Successfuly !!!");
                progressBar2.Value = 0;

            }
            else
            {
                progressBar2.Value = progressBar2.Value + 40;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value >= 100)
            {
                timer1.Enabled = false;
                MessageBox.Show("Database Backup Completed Successfuly !!!");
                progressBar1.Value = 0;


            }
            else
            {
                progressBar1.Value = progressBar1.Value + 20;
            }  
        }

        private void FrmBackupRestore_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                }
                if (e.KeyCode == Keys.Escape)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you  want to Close this window ?", "CompCare", MessageBoxButtons.YesNo);
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
