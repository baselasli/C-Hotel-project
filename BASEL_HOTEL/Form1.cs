using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BASEL_HOTEL
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
    
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox6.PasswordChar = '*';
            loginPanel.Show();
            RegistrationPanel.Hide();
            textBox11.ReadOnly = true;
            ren();
           
        }
        public void ren()
        {
        }
        Boolean flag = false;
        long ID;
        string sql;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ASUS\source\repos\BASEL_HOTEL\BASEL_HOTEL\Database1.mdf;Integrated Security=True");
        private void button3_Click(object sender, EventArgs e)
        {
           
        }
        public static string name = "";

        private void button3_Click_1(object sender, EventArgs e)
        {
            loginPanel.Hide();
            RegistrationPanel.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sql = "insert into Accounts values (@Username,@Password,@name)";
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Username", textBox1.Text);
            cmd.Parameters.AddWithValue("@Password", textBox2.Text);
            cmd.Parameters.AddWithValue("@name", textBox3.Text);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            loginPanel.Show();
            RegistrationPanel.Hide();
        }
        
        private void button2_Click_1(object sender, EventArgs e)
        {
            sql = "select * from Accounts where username=@un and Password=@up";
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@un", textBox5.Text);
            cmd.Parameters.AddWithValue("@up", textBox6.Text);
            con.Open();
            SqlDataReader sqlread = cmd.ExecuteReader();
            while (sqlread.Read())
            {
                if (textBox5.Text == sqlread.GetValue(1).ToString() && textBox6.Text == sqlread.GetValue(2).ToString()) 
                {
                    name = sqlread.GetValue(3).ToString();
                    tabControl1.Show();
                    loginPanel.Hide();
                    RegistrationPanel.Hide();
                }
            }
            con.Close();
        }
        private void price()
        {
            int total = 0;
            int kids = 0;
            int adults = 0;

            DateTime from = dateTimePicker1.Value;
            DateTime to = dateTimePicker2.Value;
            TimeSpan tspan = to - from;
            double days = tspan.TotalDays;
            int idays;
            idays = (int)days;
            if (days > idays)
            {
                idays = idays + 1;
            }
            kids = (int)comboBox3.SelectedIndex;
            adults = (int)comboBox2.SelectedIndex;
            try
            {
                if (comboBox1.SelectedItem.ToString() == "Standurd room")
                {
                    int kp1 = 100;
                    int ap1 = 200;
                    total = idays * ((kp1 * kids) + (ap1 * adults));
                    if(total>200)
                    {
                        flag = true;
                    }
                    
                }
                else if (comboBox1.SelectedItem.ToString() == "Family room")
                {
                    int kp2 = 150;
                    int ap2 = 300;
                    total = idays * ((kp2 * kids) + (ap2 * adults));
                    if(total>300)
                    {
                        flag = true;
                    }
                }
                else if (comboBox1.SelectedItem.ToString() == "Luxurious suite")
                {
                    int kp3 = 250;
                    int ap3 = 500;
                    total = idays * ((kp3 * kids) + (ap3 * adults));
                    if(total>500)
                    {
                        flag = true;
                    }
                }
                textBox11.Text = total.ToString();
                
            }
            catch
            {
                flag = false;
                MessageBox.Show("Sorry you have not chosen the room tybe");
            }

           

        }
        DataSet rent = new DataSet();
        SqlDataAdapter da;

        private void button8_Click_2(object sender, EventArgs e)
        {
            loginPanel.Hide();
            RegistrationPanel.Hide();

            ////////////////////
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                price();
                if (flag == true)
                {
                    sql = "insert into Rental values (@name,@start,@end,@price)";
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@start", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@end", dateTimePicker2.Text);
                    cmd.Parameters.AddWithValue("@price", textBox11.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Thanks" + " " + name + ". The room was booked successfully");
                }
            }
            catch
            {
                MessageBox.Show("Thanks" + " " + name + ". The room was booked Falid");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            sql = "select * from Rental";
            cmd = new SqlCommand(sql, con);
            da = new SqlDataAdapter(cmd);
            con.Open();
            rent.Clear();
            da.Fill(rent, "rental");
            dataGridView1.DataSource = rent;
            dataGridView1.DataMember = rent.Tables[0].ToString();
            con.Close();
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            
            if(flag==false)
            {
                textBox4.Show();
                label8.Show();
                flag = true;
            }
            else if(flag==true)
            {
                sql = "Delete from Rental where id=@idd";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@idd", textBox4.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted");
                flag = false;

            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem.ToString() == "Standurd room")
            {
                pictureBox1.Image = Properties.Resources.mobile_metula_191432_hotel_pics_3;
            }
            if (comboBox1.SelectedItem.ToString() == "Family room")
            {
                pictureBox1.Image = Properties.Resources.mobile_metula_145654_hotel_pics_27;
            }
            if (comboBox1.SelectedItem.ToString() == "Luxurious suite")
            {
                pictureBox1.Image = Properties.Resources.Best_Business_Hotels_Carlton_option_2;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
