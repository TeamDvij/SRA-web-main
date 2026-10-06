using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
namespace SRACMSApplication.Captcha
{
    public class CaptchaImage : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            var captchaCode = GenerateCaptchaCode();
            context.HttpContext.Session["CaptchaCode"] = captchaCode;

            using (var bitmap = new Bitmap(150, 50))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
                var font = new Font("Arial", 24, FontStyle.Bold);
                var brush = new SolidBrush(Color.Black);
                graphics.DrawString(captchaCode, font, brush, 10, 10);

                context.HttpContext.Response.ContentType = "image/png";
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    context.HttpContext.Response.OutputStream.Write(ms.ToArray(), 0, (int)ms.Length);
                }
            }
        }

        private string GenerateCaptchaCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var captcha = new char[5];
            for (int i = 0; i < captcha.Length; i++)
                captcha[i] = chars[random.Next(chars.Length)];
            return new string(captcha);
        }
    }
}