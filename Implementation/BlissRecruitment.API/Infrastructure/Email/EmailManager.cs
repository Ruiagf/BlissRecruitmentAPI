namespace BlissRecruitment.Infrastructure.Email
{
    using BlissRecruitment.BusinessLogic.Abstract;
    using log4net;
    using System;
    using System.Net;
    using System.Net.Configuration;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using System.Web.Configuration;

    public class EmailProvider : IEmailProvider
    {
        private readonly ILog logger;
        private readonly SmtpClient smtpClient;
        private readonly string from;

        public EmailProvider(ILog logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var path = "/";

            var configuration = WebConfigurationManager.OpenWebConfiguration(path);

            var mailSettings = (MailSettingsSectionGroup)configuration.GetSectionGroup("system.net/mailSettings");

            if (mailSettings == null)
            {
                this.logger.Error("The e-mail configuration is missing in the configuration file.");
                return;
            }

            this.from = mailSettings.Smtp.From;

            this.smtpClient = new SmtpClient(mailSettings.Smtp.Network.Host)
            {
                Credentials = new NetworkCredential(mailSettings.Smtp.Network.UserName, mailSettings.Smtp.Network.Password),
                Port = mailSettings.Smtp.Network.Port
            };
        }

        public async Task<bool> SendMailAsync(string from, string fromDescription, string to, string toDescription, string subject, string body)
        {
            try
            {
                using (var email = new MailMessage(new MailAddress(!string.IsNullOrWhiteSpace(from) ? from : this.from, fromDescription), new MailAddress(to, toDescription)))
                {
                    email.Subject = subject;
                    email.Body = body;
                    email.IsBodyHtml = true;

                    await this.smtpClient.SendMailAsync(email);

                    return true;
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                return false;
            }
        }
    }
}