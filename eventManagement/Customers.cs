using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace eventManagement
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            ShowCustomers();
        }
        private void ShowCustomers()
        {
            Con.Open();
            string Query = "Select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Clear()
        {
            CNameTb.Text = "";
            CPhoneTb.Text = "";
          
        }
        private void Customers_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dell\Documents\EventDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CNameTb.Text == '' || CPhoneTb.Text == '')
            {
                MessageBox.Show('Missing Information');
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into CustomerTbl(CName, CPhone)values(@CN, @CP)", Con);
                    cmd.Parameters.AddWithValue("@CN", CNameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CPhoneTb.Text);
                
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer added");
                    Con.Close();
                    ShowVenue();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int key = 0;
        private void editBtn_Click(object sender, EventArgs e)
        {
            if (CNameTb.Text == '' || CPhoneTb.Text == '')
            {
                MessageBox.Show('Missing Information');
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update CustomerTbl set CName=@CN,CPhone=@CP where CustId=@CKey", Con);
                    cmd.Parameters.AddWithValue("@CN", CNameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@Ckey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully");
                    Con.Close();
                    ShowVenue();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show('Select Data');
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from  CustomerTbl where CustId=@Ckey", Con);
                    cmd.Parameters.AddWithValue("@Ckey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted Successfully");
                    Con.Close();
                    ShowCustomers();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { 
            CNameTb.Text = CustomerDGV.SelectedRows[0].Cells[1].ToString();
            CPhoneTb.Text = CustomerDGV.SelectedRows[0].Cells[2].ToString();
         
            if (CNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
          
        }

        private void label3_Click(object sender, EventArgs e)
        {
            VCapacityTb obj = new VCapacityTb();
            obj.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Events obj = new Events();
            obj.Show();
            this.Hide();
        }
    }
}
