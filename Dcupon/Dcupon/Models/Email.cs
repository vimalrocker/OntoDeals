using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Linq;
namespace Dcupon.Models
{
    public class Email
    {

        private string emailaddress { get; set; }
        private string password { get; set; }
        private string subject { get; set; }
        private string host { get; set; }
        private int port { get; set; }
        public string Body { get; set; }

        public List<string> Toaddress { get; set; }


        /// <summary>
        ///  Send Email 
        /// </summary>
        public void SendErrorEmail()
        {
            try
            {
                var json = "";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(@"~/Configuration/Config.json")))
                {
                    json = reader.ReadToEnd();
                }
                JToken token = JToken.Parse(json);
                emailaddress = Convert.ToString(token.Root["IMAPDetails"].First["FromAddress"]);
                password = Helper.Decrypt(Convert.ToString(token.Root["IMAPDetails"].First["Password"]), true);
                subject = Convert.ToString(token.Root["IMAPDetails"].First["Subject"]);
                host = Convert.ToString(token.Root["IMAPDetails"].First["Host"]);
                port = Convert.ToInt32(token.Root["IMAPDetails"].First["Port"]);
                SmtpClient smtp = new SmtpClient();
                MailMessage mailMessage = new MailMessage();
                object usertoken = "Test";
                mailMessage.From = new MailAddress(emailaddress);
                mailMessage.To.Add("joseph.vimal.tom@gmail.com");
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = Body;
                NetworkCredential networkCredential = new NetworkCredential(emailaddress, password);
                smtp.Credentials = networkCredential;
                smtp.Host = host;
                smtp.Port = port;
                smtp.EnableSsl = true;
                smtp.Send(mailMessage);
            }

            catch (Exception ex)
            {



            }
        }

        public void PasswordResetEmail(string Email , string passtoken)
        {
            try
            {
                var json = "";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(@"~/Configuration/Config.json")))
                {
                    json = reader.ReadToEnd();
                }
                JToken token = JToken.Parse(json);
                emailaddress = Convert.ToString(token.Root["NOReply"].First["FromAddress"]);
                password = Convert.ToString(token.Root["NOReply"].First["Password"]);
                subject = Convert.ToString(token.Root["NOReply"].First["Subject"]);
                host = Convert.ToString(token.Root["NOReply"].First["Host"]);
                port = Convert.ToInt32(token.Root["NOReply"].First["Port"]);
                SmtpClient smtp = new SmtpClient();
                MailMessage mailMessage = new MailMessage();
                object usertoken = "Test";
                mailMessage.From = new MailAddress(emailaddress);
                mailMessage.To.Add(Email);
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = Convert.ToString(token.Root["NOReply"].First["body"]).Replace("'@abcde'", passtoken);
                mailMessage.Body = mailMessage.Body.Replace("'1234'", Email);
                NetworkCredential networkCredential = new NetworkCredential(emailaddress, password);
                smtp.Credentials = networkCredential;
                smtp.Host = host;
                smtp.Port = port;
                smtp.EnableSsl = false;
                //smtp.UseDefaultCredentials = false;
                smtp.Send(mailMessage);
            }

            catch (Exception ex)
            {
                


            }
        }
        public void AdminPasswordResetEmail(string Email, string passtoken)
        {
            try
            {
                var json = "";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(@"~/Configuration/Config.json")))
                {
                    json = reader.ReadToEnd();
                }
                JToken token = JToken.Parse(json);
                emailaddress = Convert.ToString(token.Root["NOReply"].First["FromAddress"]);
                password = Convert.ToString(token.Root["NOReply"].First["Password"]);
                subject = Convert.ToString(token.Root["NOReply"].First["Subject"]);
                host = Convert.ToString(token.Root["NOReply"].First["Host"]);
                port = Convert.ToInt32(token.Root["NOReply"].First["Port"]);
                SmtpClient smtp = new SmtpClient();
                MailMessage mailMessage = new MailMessage();
                object usertoken = "Test";
                mailMessage.From = new MailAddress(emailaddress);
                mailMessage.To.Add(Email);
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = Convert.ToString(token.Root["NOReply"].First["Adminbody"]).Replace("'@abcde'", passtoken);
                mailMessage.Body = mailMessage.Body.Replace("'1234'", Email);
                NetworkCredential networkCredential = new NetworkCredential(emailaddress, password);
                smtp.Credentials = networkCredential;
                smtp.Host = host;
                smtp.Port = port;
                smtp.EnableSsl = false;
                //smtp.UseDefaultCredentials = false;
                smtp.Send(mailMessage);
            }

            catch (Exception ex)
            {



            }
        }

        public  string  GenerateToken()
        {
            Random random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789@23";
            return new string(chars.Select(c => chars[random.Next(chars.Length)]).Take(8).ToArray());

           
        }
    }
}