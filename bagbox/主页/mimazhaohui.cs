using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bagbox.前端窗口;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Reflection.Emit;

namespace bagbox
{
    public partial class mimazhaohui : Form
    {

        public static string a = string.Empty;

        public mimazhaohui()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {

        }

        private void mimazhaohui_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
        }

        public bool SendMail(string mailFrom, string mailTo, string token, string subject, string body)
        {
            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient();
            // 指定电子邮件发送方式
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            // 指定SMTP服务器
            smtpClient.Host = "smtp.qq.com";
            // 使用安全加密连接（是否启用SSL）
            smtpClient.EnableSsl = true;
            // 不和请求一块发送。
            smtpClient.UseDefaultCredentials = false;
            // 用户名和密码
            smtpClient.Credentials = new NetworkCredential(mailFrom, token);

            // 发送人和收件人
            MailMessage mailMessage = new MailMessage(mailFrom, mailTo);
            // 主题
            mailMessage.Subject = subject;
            // 内容
            mailMessage.Body = body;
            // 正文编码
            mailMessage.BodyEncoding = Encoding.UTF8;
            // 设置为HTML格式
            mailMessage.IsBodyHtml = true;
            // 优先级
            mailMessage.Priority = MailPriority.Low;
            try
            {
                // 发送邮件
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (SmtpException ex)
            {
                //打印错误
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public string GenerateRandomCode(int length)
        {
            const string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private Dictionary<string, string> verificationCodes = new Dictionary<string, string>();


        public void button1_Click(object sender, EventArgs e)
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";

            // 验证输入的字符是否与正则表达式模式匹配
            if (!Regex.IsMatch(txtPwd.Text, pattern))
            {
                // 如果不匹配，则清除文本框内容
                
                txtPwd.Clear();
            }
            if (txtName.Text == "" || txtPwd.Text == "")
            {
                label4.Visible = true;
                
                //MessageBox.Show("请输入账号密码");
            }
            else
            {
                label4.Visible = false;
                
                string query = "SELECT * FROM customer_Table WHERE Name = @Name AND Email = @Email";

                // 创建 SqlCommand 对象
                SqlCommand cmd = new SqlCommand(query, DB.GetCn());

                // 添加参数
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Email", txtPwd.Text);

                // 使用 SqlCommand 对象执行查询
                DataTable dt = DB.GetDataSet(cmd);



                if (dt.Rows.Count > 0)
                {
                    DB.GetCn();
                    // 检查上次发送验证码的时间
                    object lastCaptchaSentTimeObj = dt.Rows[0]["last_captcha_sent_time"];
                    DateTime lastCaptchaSentTime;
                    if (lastCaptchaSentTimeObj != DBNull.Value)
                    {
                        lastCaptchaSentTime = Convert.ToDateTime(lastCaptchaSentTimeObj);
                        if (DateTime.Now.Subtract(lastCaptchaSentTime).TotalHours > 99999)
                        //if (DateTime.Now.Subtract(lastCaptchaSentTime).TotalHours < 1)
                        {
                            MessageBox.Show("1 小时内只能发送一次验证码，请稍后重试。");
                            return;
                        }
                    }

                    // 生成验证码
                    string verificationCode = GenerateRandomCode(6);

                    // 更新数据库中的验证码字段和上次发送时间
                    string updateQuery = $"UPDATE customer_Table SET captcha = '{verificationCode}', last_captcha_sent_time = GETDATE() WHERE Name = '{txtName.Text}'";
                    bool success = DB.sqlEx(updateQuery);

                    if (success)
                    {
                        // 发送邮件，包括验证码
                        string token = ConfigurationManager.AppSettings["token"];
                        // 获取用户名
                        string userName = txtName.Text;

                        // 获取收件人邮箱地址
                        string mailTo = DB.GetRecipientEmailFromDatabase(userName);
                        //string mailTo = ConfigurationManager.AppSettings["mailTo"];
                        string mailFrom = ConfigurationManager.AppSettings["mailFrom"];
                        SendMail(mailFrom, mailTo, token, "验证码", $"您的验证码是：{verificationCode},5分钟后失效");
                        a = txtName.Text;
                        MessageBox.Show("验证码已发送，请检查您的邮箱。");
                        //MessageBox.Show(a);
                        yanzhengma t1 = new yanzhengma();
                        t1.ShowDialog();
                        
                    }
                    else
                    {
                        MessageBox.Show("发送验证码失败");
                    }
                }
                else
                {
                    MessageBox.Show("用户名或者邮箱错误");
                }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
