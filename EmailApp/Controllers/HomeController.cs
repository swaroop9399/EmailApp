using EmailApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace EmailApp.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration Configuration;
        private int i;

        public object Files { get; private set; }

        public HomeController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MailPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMail()
        {
            string from = "poojavishal6699@gmail.com";
            MailMessage mail = new MailMessage(from, Request.Form["to"].ToString());
            mail.Subject = "Test Mail";
            var files = Request.Form.Files.GetFiles("attachment");
            mail.Body = Request.Form["body"].ToString();
            
            for (int i = 0; i < files.Count; i++)
            {
                var bytes = getByteArrayFromFile(files[i]);

                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), files[i].FileName));
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential networkCredential = new NetworkCredential(from, "levqgzyrgghipkbh"); //For gmail user Add App password it generate after 2 step verification
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = networkCredential;
            smtp.Port = 587;
            smtp.Send(mail);
            //Console.WriteLine(Request.Form["attachment"]);


            return new JsonResult("Done");
        }

      

        private byte[] getByteArrayFromFile(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();

            };

        }

    }
}