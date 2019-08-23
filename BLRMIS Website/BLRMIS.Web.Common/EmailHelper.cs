using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLRMIS.Web.Common
{
    public class EmailHelper:IEmailHelper
    {
        private AppSettings AppSettings;
        public EmailHelper(IOptions<AppSettings> settings)
        {
            AppSettings = settings.Value;
        }

        public async Task SendEmailAsync(Dictionary<string, string> placeholders, string EmailTemplate, string ToEmail, string Subject)
        {
            //string templateName = string.Empty;
            //switch (EmailTemplate)
            //{
            //    case EmailTemplates.VerificationCodeTemplate:
            //        templateName = Constants.VerificationCode;
            //        break;
            //}
            var EmailBody = await createEmailBodyAsync(placeholders, EmailTemplate);
            await SendHtmlFormattedEmailAsync(ToEmail, Subject, EmailBody);
        }

        private async Task<string> createEmailBodyAsync(Dictionary<string, string> placeholders, string TemplateName)
        {
            string body = string.Empty;
            string path = Path.Combine(AppSettings.EmailTemplatePath, TemplateName + ".html");
            using (StreamReader reader = new StreamReader(path))
            {
                body = await reader.ReadToEndAsync();
            }

            var matches = Regex.Matches(body, @"{.*?}");
            var uniques = matches.Cast<Match>().Select(match => match.Value).ToList().Distinct();

            foreach (var unique in uniques)
            {
                body = body.Replace(unique, placeholders[unique]);
            }
            return body;
        }

        public async Task SendHtmlFormattedEmailAsync(string toAddress, string subject, string body)
        {

            
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(AppSettings.EmailUserName);

                mailMessage.Subject = subject;

                mailMessage.Body = body;

                mailMessage.IsBodyHtml = true;                

                mailMessage.To.Add(new MailAddress(toAddress));

                SmtpClient smtp = new SmtpClient();

                smtp.Host = AppSettings.EmailHost;

                smtp.EnableSsl = Convert.ToBoolean(AppSettings.EmailEnableSsl);

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                NetworkCred.UserName = AppSettings.EmailUserName; //reading from web.config  

                NetworkCred.Password = AppSettings.EmailPassword;

                smtp.UseDefaultCredentials = true;

                smtp.Credentials = NetworkCred;

                smtp.Port = int.Parse(AppSettings.EmailPort); //reading from web.config  

                await smtp.SendMailAsync(mailMessage);
            }
           
        }

        public void SendEmail(Dictionary<string, string> placeholders, string EmailTemplate, string ToEmail, string Subject)
        {
            //string templateName = string.Empty;
            //switch (EmailTemplate)
            //{
            //    case EmailTemplates.VerificationCodeTemplate:
            //        templateName = Constants.VerificationCode;
            //        break;
            //}
            var EmailBody = createEmailBody(placeholders, EmailTemplate);
            SendHtmlFormattedEmail(ToEmail, Subject, EmailBody);
        }

        private string createEmailBody(Dictionary<string, string> placeholders, string TemplateName)
        {
            string body = string.Empty;
            string path = Path.Combine(AppSettings.EmailTemplatePath, TemplateName + ".html");
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }

            var matches = Regex.Matches(body, @"{.*?}");
            var uniques = matches.Cast<Match>().Select(match => match.Value).ToList().Distinct();

            foreach (var unique in uniques)
            {
                if(placeholders.Keys.Contains(unique))
                    body = body.Replace(unique, placeholders[unique]);
            }
            return body;
        }

        public void SendHtmlFormattedEmail(string toAddress, string subject, string body)
        {
            string ImagePath = Path.Combine(AppSettings.EmailTemplatePath,Convert.ToString(EmailTemplates.Images) + "\\");
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(AppSettings.EmailUserName);

                mailMessage.Subject = subject;

             //   mailMessage.Body = body;

                mailMessage.IsBodyHtml = true;

                LinkedResource logo = new LinkedResource(ImagePath + EmailTemplates.Logo.ToString() +".png");
                logo.ContentId = "LogoImg";
                logo.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
                LinkedResource bitmapImage = new LinkedResource(ImagePath + EmailTemplates.Bitmap.ToString() + ".jpg", new ContentType(MediaTypeNames.Image.Jpeg));
                bitmapImage.ContentId = "BitmapImg";
                bitmapImage.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
                LinkedResource fbIcon = new LinkedResource(ImagePath + EmailTemplates.FbIcon.ToString() + ".png", new ContentType(MediaTypeNames.Image.Jpeg));
                fbIcon.ContentId = "FbIcon";
                LinkedResource googlePlayIcon = new LinkedResource(ImagePath + EmailTemplates.Googleplay.ToString() + ".png", new ContentType(MediaTypeNames.Image.Jpeg));
                googlePlayIcon.ContentId = "GooglePlay";
                LinkedResource twitterIcon = new LinkedResource(ImagePath + EmailTemplates.TwitterIcon.ToString() + ".png", new ContentType(MediaTypeNames.Image.Jpeg));
                twitterIcon.ContentId = "TwitterIcon";
                LinkedResource youTubeIcon = new LinkedResource(ImagePath + EmailTemplates.YouTubeIcon.ToString() + ".png", new ContentType(MediaTypeNames.Image.Jpeg));
                youTubeIcon.ContentId = "YouTubeIcon";
                LinkedResource linkedInIcon = new LinkedResource(ImagePath + EmailTemplates.LinkedinIcon.ToString() + ".png", new ContentType(MediaTypeNames.Image.Jpeg));
                linkedInIcon.ContentId = "LinkedInIcon";
                // done HTML formatting in the next line to display my logo
                AlternateView av1 = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                av1.LinkedResources.Add(logo);
                av1.LinkedResources.Add(bitmapImage);
                av1.LinkedResources.Add(fbIcon);
                av1.LinkedResources.Add(googlePlayIcon);
                av1.LinkedResources.Add(twitterIcon);
                av1.LinkedResources.Add(youTubeIcon);
                av1.LinkedResources.Add(linkedInIcon);
                mailMessage.AlternateViews.Add(av1);



                mailMessage.To.Add(new MailAddress(toAddress));

                SmtpClient smtp = new SmtpClient();

                smtp.Host = AppSettings.EmailHost;

                smtp.EnableSsl = Convert.ToBoolean(AppSettings.EmailEnableSsl);

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                NetworkCred.UserName = AppSettings.EmailUserName; //reading from web.config  

                NetworkCred.Password = AppSettings.EmailPassword;

                smtp.UseDefaultCredentials = true;

                smtp.Credentials = NetworkCred;

                smtp.Port = int.Parse(AppSettings.EmailPort); //reading from web.config  

                smtp.Send(mailMessage);
            }
        }
    }
}
