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
    public partial class FrmProblem : Form
    {
        connection c = new connection();
        SqlConnection con;
        string query, cname;
        int i, id;
        public FrmProblem()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmProblem_Load(object sender, EventArgs e)
        {
            try
            {
                getCompanyName();
                getProblemInfo();
                btnUpdate.Enabled = false;
                btnSave.Enabled = true;
                txtProblem.Focus();
                
            }
            catch (Exception ee)
            {
            }
        }

        private void FrmProblem_KeyDown(object sender, KeyEventArgs e)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmProblem_Load(sender,e);
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

        private void getProblemInfo()
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


                listView1.Columns.Add("Problem ID", 0);
                listView1.Columns.Add("Problem Name", 800);
           

                int i;

                con = c.openConnection();

                DataTable dt = new DataTable();
                string[] arr = new string[14];
                ListViewItem itm;
                query = "select * from tblproblem  where oid='" + lblid.Text + "'";
                SqlCommand cmd1 = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(dt);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    arr[0] = Convert.ToString(dt.Rows[i]["pid"]);
                    arr[1] = Convert.ToString(dt.Rows[i]["problem"]);
                   

                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
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
                if (txtProblem.Text == "")
                {
                    MessageBox.Show("Please Enter Problem", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProblem.Focus();
                }
               

                else if (checkProblem())
                {
                    insertProblem();
                    FrmProblem_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Problem Name already Exist! Please Enter another Problem!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FrmProblem_Load(sender, e);
                    txtProblem.Focus();
                }
            }
            catch (Exception ee)
            {
            }
        }


        private bool checkProblem()
        {
            try
            {
                int count = 0;
                con = c.openConnection();
                query = "select count(pid) from tblproblem where problem='" + txtProblem.Text + "'";
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

        private void insertProblem()
        {
            try
            {
                con = c.openConnection();
                query = "insert into tblproblem (oid,problem) values (@oid,@problem)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@oid", lblid.Text);
                cmd.Parameters.AddWithValue("@problem", txtProblem.Text);
                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Problem Information Inserted Successfully!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearTexts();
                }
            }
            catch (Exception ee)
            {
            }
        }

        private void clearTexts()
        {
            txtProblem.Text = "";
            txtProblem.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                txtProblem.Focus();
                con = c.openConnection();

                if (listView1.SelectedItems.Count == 1)
                {
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                    id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                    query = "select * from tblproblem where pid='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {

                        txtProblem.Text = Convert.ToString(sdr.GetValue(1));

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
                    query = "delete from tblproblem where pid='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Problem  Information Deleted Successfully!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmProblem_Load(sender, e);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProblem.Text == "")
                {
                    MessageBox.Show("Please Enter Problem Name", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProblem.Focus();
                }       

                else
                {
                    con = c.openConnection();
                    id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                    query = "update tblproblem set problem=@problem where pid='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@problem", txtProblem.Text);
                    
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Problem  Information Updated Successfully!!!", "" + cname + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearTexts();
                        FrmProblem_Load(sender, e);
                    }
                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message);
            }
        }


        
    }
}
