using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using System.Reflection;
using bagbox.前端窗口;
using System.Data.Common;
using static bagbox.DB;

namespace bagbox
{
    
    public partial class Login : Form
    {

        public static string StrValue = string.Empty;
        public static string StrValue11 = string.Empty;
        
        public Login()
        {
            InitializeComponent();
            
        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void btnOk_Click(object sender, EventArgs e)
        {
            
            if (txtName.Text == "" || txtPwd.Text == "")
            {
                MessageBox.Show("请输入账号密码");
            }
            else
            {
                // 执行查询并验证用户信息是否匹配
                DB.GetCn();

                if (radioButton1.Checked)
                {
                    string enteredPassword = PasswordHasher.HashPassword(txtPwd.Text); // 对用户输入的密码进行哈希加密

                    string query = "SELECT * FROM customer_Table WHERE Name = @Name AND Password = @Password";

                    // 创建 SqlCommand 对象
                    SqlCommand cmd = new SqlCommand(query, DB.GetCn());

                    // 添加参数
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Password", enteredPassword); // 使用哈希加密后的密码

                    // 使用 SqlCommand 对象执行查询
                    DataTable dt = DB.GetDataSet(cmd);

                    if (dt.Rows.Count > 0)
                    {
                        // 登录成功
                        Name = txtName.Text;
                        this.Visible = false;
                        Front t1 = new Front();
                        StrValue = txtName.Text;
                        
                        t1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("用户名或者密码错误");
                    }


                }

                if (radioButton2.Checked)
                {
                    string query = "SELECT * FROM Employee_Table WHERE Employee_Name = @Employee_Name AND Telephone = @Telephone";

                    // 创建 SqlCommand 对象
                    SqlCommand cmd = new SqlCommand(query, DB.GetCn());

                    // 添加参数
                    cmd.Parameters.AddWithValue("@Employee_Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Telephone", txtPwd.Text);

                    // 使用 SqlCommand 对象执行查询
                    DataTable dt = DB.GetDataSet(cmd);

                    if (dt.Rows.Count > 0)
                    {
                        
                        //MessageBox.Show("登录成功");
                        Name = txtName.Text;
                        Form1.flag = 2;
                        this.Visible = false;
                        Backend t1 = new Backend();
                        StrValue11 = txtName.Text;
                        t1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("用户名或者密码错误");
                    }
                }

                if (radioButton3.Checked)
                {
                    string query = "SELECT * FROM User_Table WHERE Username = @Username AND pwd = @pwd";

                    // 创建 SqlCommand 对象
                    SqlCommand cmd = new SqlCommand(query, DB.GetCn());

                    // 添加参数
                    cmd.Parameters.AddWithValue("@Username", txtName.Text);
                    cmd.Parameters.AddWithValue("@pwd", txtPwd.Text);

                    // 使用 SqlCommand 对象执行查询
                    DataTable dt = DB.GetDataSet(cmd);

                    if (dt.Rows.Count > 0)
                    {
                        
                        //MessageBox.Show("登录成功");
                        Name = txtName.Text;
                        Form1.flag = 3;
                        this.Visible = false;
                        Backend t1 = new Backend();
                        StrValue11 = txtName.Text;
                        t1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("用户名或者密码错误");
                    }
                }
                

            }

            
        }
        

        public void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void btnCance_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void Login_Shown(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mimazhaohui t1 = new mimazhaohui();
            t1.ShowDialog();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnOk;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPwd.PasswordChar = '\0' ;
            }
            else
            {
                txtPwd.PasswordChar = '*';
            }
        }
    }
}
