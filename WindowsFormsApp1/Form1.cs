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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // 本地LocalDB连接字符串，直接写在代码内
        private readonly string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string acount = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(acount) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("账号和密码不能为空！");
                return;
            }

            try
            {
                string sql = "SELECT COUNT(1) FROM UserLogin WHERE Account=@acc AND Pwd=@pwd";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@acc", acount);
                    cmd.Parameters.AddWithValue("@pwd", password);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());


                    if (count > 0)
                    {
                        MessageBox.Show("登录成功");
                        MainForm main = new MainForm();
                        main.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("账号或密码错误！");
                        textBox2.Clear();
                        textBox2.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接异常：" + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}