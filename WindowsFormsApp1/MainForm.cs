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
    public partial class MainForm : Form
    {
        // 和登录页统一连接字符串
        private readonly string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True";

        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadAllStudent();
        }

        private void LoadAllStudent()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = "SELECT * FROM Student ORDER BY Id ASC";
                    SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取学生数据失败：" + ex.Message);
            }
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = "INSERT INTO Student(StudentNo,StudentName,Grade,Class) VALUES(@SNo,@SName,@Grade,@Class)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@SNo", "2026005");
                    cmd.Parameters.AddWithValue("@SName", "小明");
                    cmd.Parameters.AddWithValue("@Grade", "2026级");
                    cmd.Parameters.AddWithValue("@Class", "2班");
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("学生新增成功！");
                LoadAllStudent(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("新增学生失败：" + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 获取输入
            string sname = txtName.Text.Trim();
            string cls = txtClass.Text.Trim();

            // 非空校验
            if (string.IsNullOrEmpty(sname) || string.IsNullOrEmpty(cls))
            {
                MessageBox.Show("姓名和班级不能为空！");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    // SQL只需要插入姓名、班级，学号/年级/时间数据库自动填充
                    string sql = "INSERT INTO Student(StudentName,Class) VALUES(@SName,@Class)";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // 仅绑定两个参数
                    cmd.Parameters.AddWithValue("@SName", sname);
                    cmd.Parameters.AddWithValue("@Class", cls);

                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("学生添加成功！");
                LoadAllStudent(); // 刷新表格
                                  // 清空输入框
                txtName.Clear();
                txtClass.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("新增失败：" + ex.Message);
            }
        }
    }
}