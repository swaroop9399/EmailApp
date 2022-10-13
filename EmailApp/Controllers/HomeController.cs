using EmailApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace EmailApp.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration Configuration;
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
            //Console.WriteLine(Request.Form["to"]);
            //Console.WriteLine(Request.Form["body"]);
            var attachmentFile = Request.Form.Files.GetFile("attachment");
            var byteArray = getByteArrayFromFile(attachmentFile);
            string from = "poojavishal6699@gmail.com";
            MailMessage mail = new MailMessage(from, Request.Form["to"].ToString());
            mail.Subject = "Test Mail";
            mail.Body = Request.Form["body"].ToString();
            mail.Attachments.Add(new Attachment(new MemoryStream(byteArray), attachmentFile.FileName));

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential networkCredential = new NetworkCredential(from, "levqgzyrgghipkbh"); //For gmail user Add App password it generate after 2 step verification
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = networkCredential;
            smtp.Port = 587;
            smtp.Send(mail);
            Console.WriteLine(Request.Form["attachment"]);


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