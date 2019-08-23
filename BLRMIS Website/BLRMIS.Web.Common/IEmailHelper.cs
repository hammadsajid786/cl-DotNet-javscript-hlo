using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Common
{
    public interface IEmailHelper
    {
        void SendEmail(Dictionary<string, string> placeholders, string EmailTemplate,string toEmail, string Subject);
        void SendHtmlFormattedEmail(string toAddress, string subject, string body);
    }
}
