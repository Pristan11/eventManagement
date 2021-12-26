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
    public partial class VCapacityTb : Form
    {
        public VCapacityTb()
        {
            InitializeComponent();
            ShowVenue();
        }
        private void ShowVenue()
        {
            Con.Open();
            string Query = "Select * from VenueTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            VenueDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Clear()
        {
            VAddressTb.Text = "";
            VNameTb.Text = "";
            VPhoneTb.Text = "";
            VManagerTb.Text = "";
            VCapacityTbf.Text = "";
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Venues_Load(object sender, EventArgs e)
        {

        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dell\Documents\EventDb.mdf;Integrated Security=True;Connect Timeout=30");



        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(VAddressTb.Text == '' || VCapacityTbf.Text == '' || VManagerTb.Text == '' || VPhoneTb.Text == '' || VNameTb.Text == '')
            {
                MessageBox.Show('Missing Information');
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into VenueTble(VName,VAddress,VCapacity,VManager,VPhone)values(@VN,@VA,@VC,@VM,@VP)", Con);
                    cmd.Parameters.AddWithValue("@VN", VNameTb.Text);
                    cmd.Parameters.AddWithValue("@VA", VAddressTb.Text);
                    cmd.Parameters.AddWithValue("@V", VCapacityTbf.Text);
                    cmd.Parameters.AddWithValue("@VM", VManagerTb.Text);
                    cmd.Parameters.AddWithValue("@VP", VPhoneTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("values added");
                    Con.Close();
                    ShowVenue();
                    Clear();
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
int key = 0;
        private void VenueDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            VNameTb.Text = VenueDGV.SelectedRows[0].Cells[1].ToString();
            VAddressTb.Text = VenueDGV.SelectedRows[0].Cells[2].ToString();
            VCapacityTbf.Text = VenueDGV.SelectedRows[0].Cells[3].ToString();
            VManagerTb.Text = VenueDGV.SelectedRows[0].Cells[4].ToString();
            VPhoneTb.Text = VenueDGV.SelectedRows[0].Cells[5].ToString();
            if(VNameTb.Text == "")
            {
                key = 0;
            }else
            {
                key = Convert.ToInt32(VenueDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void VCapacityTbf_TextChanged(object sender, EventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if(key == 0) 
            {
                MessageBox.Show('Select Data');
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from  VenueTble where VId=@Vkey", Con);
                    cmd.Parameters.AddWithValue("@Vkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted Successfully");
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

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (VAddressTb.Text == '' || VCapacityTbf.Text == '' || VManagerTb.Text == '' || VPhoneTb.Text == '' || VNameTb.Text == '')
            {
                MessageBox.Show('Missing Information');
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update VenueTble set VName=@VN,VAddress=@VA,VCapacity=@VC,VManager=@VM,VPhone=@VP where VId=@VKey", Con);
                    cmd.Parameters.AddWithValue("@VN", VNameTb.Text);
                    cmd.Parameters.AddWithValue("@VA", VAddressTb.Text);
                    cmd.Parameters.AddWithValue("@V", VCapacityTbf.Text);
                    cmd.Parameters.AddWithValue("@VM", VManagerTb.Text);
                    cmd.Parameters.AddWithValue("@VP", VPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@Vkey", key);
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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Events obj = new Events();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }
    }
}
