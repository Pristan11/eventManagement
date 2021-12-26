using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eventManagement
{
    public partial class Events : Form
    {
        public Events()
        {
            InitializeComponent();
            ShowEvents();
            GetVenue();
            GetCustomer();
        }
        private void ShowEvents()
        {
            Con.Open();
            string Query = "Select * from EventTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EventGDV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Clear()
        {
            ENameTb.Text = "";
            VNameTb.Text = "";
            CNameTb.Text = "";
            EStatusCb.SelectedIndex = -1;
            EDurationCb.Text = "";
        }
        
        private void GetVenue()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select VId from VenueTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("VId", typeof(int));
            dt.Load(Rdr);
            VidCb.ValueMember = "VId";
            VidCb.DataSource = dt;
            Con.Close();

        }
        private void GetVenueName()
        {
            Con.Open();
            string Query = "Select * from VenueTbl where VId=" + VidCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                VNameTb.Text = dr["VName"].ToString();
            }
            Con.Close();
        }

        private void GetCustomer()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CustId from CustomerTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
            dt.Load(Rdr);
            VidCb.ValueMember = "CustId";
            VidCb.DataSource = dt;
            Con.Close();

        }
        private void GetCustomerName()
        {
            Con.Open();
            string Query = "Select * from CustomerTbl where CustId=" + CustIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CNameTb.Text = dr["CName"].ToString();
            }
            Con.Close();
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Events_Load(object sender, EventArgs e)
        {

        }

        private void Events_Load_1(object sender, EventArgs e)
        {

        }

        private void Events_Load_2(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dell\Documents\EventDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ENameTb.Text == "" || VNameTb.Text == "" || CNameTb.Text == "" || EStatusCb.SelectedIndex == -1 || EDurationCb.Text == "")
            {
                MessageBox.Show('Missing Information');
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into EventTbl(EName,EDate,VId, VName, EDuration,CustId,CustName,EStatus)values(@EN,@ED,@VI,@VN,@ED,@CI,@CN,@ES)", Con);
                    cmd.Parameters.AddWithValue("@EN", ENameTb.Text);
                    cmd.Parameters.AddWithValue("@ED", EDatePr.Value.Date);
                    cmd.Parameters.AddWithValue("@VI", VidCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@VN", VNameTb.Text);
                    cmd.Parameters.AddWithValue("@ED", EDurationCb.Text);
                    cmd.Parameters.AddWithValue("@CI", CustIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CN", CNameTb.Text);
                    cmd.Parameters.AddWithValue("@ES", EStatusCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Event added");
                    Con.Close();
                    ShowEvents();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void VidCb_SelectedValueChanged(object sender, EventArgs e)
        {
            GetVenueName();
        }

        private void CustIdCb_SelectedValueChanged(object sender, EventArgs e)
        {
            GetCustomerName();
        }
        int key = 0;
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ENameTb.Text == "" || VNameTb.Text == "" || CNameTb.Text == "" || EStatusCb.SelectedIndex == -1 || EDurationCb.Text == "")
            {
                MessageBox.Show('Missing Information');
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update EventTbl set EName=@EN,EDate=@ED,VId=@VI, VName=@VN, EDuration=@ED,CustId=@CI,CustName=@CN,EStatus=@ES where Eid=@Ekey", Con);
                    cmd.Parameters.AddWithValue("@EN", ENameTb.Text);
                    cmd.Parameters.AddWithValue("@ED", EDatePr.Value.Date);
                    cmd.Parameters.AddWithValue("@VI", VidCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@VN", VNameTb.Text);
                    cmd.Parameters.AddWithValue("@ED", EDurationCb.Text);
                    cmd.Parameters.AddWithValue("@CI", CustIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CN", CNameTb.Text);
                    cmd.Parameters.AddWithValue("@ES", EStatusCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Ekey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Event Updated");
                    Con.Close();
                    ShowEvents();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EventGDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ENameTb.Text = EventGDV.SelectedRows[0].Cells[1].ToString();
            EDatePr.Text = EventGDV.SelectedRows[0].Cells[2].ToString();
            VidCb.SelectedValue = EventGDV.SelectedRows[0].Cells[3].ToString();
            VNameTb.Text = EventGDV.SelectedRows[0].Cells[4].ToString();
            EDurationCb.Text = EventGDV.SelectedRows[0].Cells[5].ToString();
            CustIdCb.SelectedValue = EventGDV.SelectedRows[0].Cells[6].ToString();
            CNameTb.Text = EventGDV.SelectedRows[0].Cells[7].ToString();
            EStatusCb.SelectedItem = EventGDV.SelectedRows[0].Cells[8].ToString();
            if (VNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(EventGDV.SelectedRows[0].Cells[0].Value.ToString());
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
                    SqlCommand cmd = new SqlCommand("Delete from  EventTbl where EId=@Ekey", Con);
                    cmd.Parameters.AddWithValue("@Ekey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted Successfully");
                    Con.Close();
                    ShowEvents();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            VCapacityTb obj = new VCapacityTb();
            obj.Show();
            this.Hide();
        }
    }
}
