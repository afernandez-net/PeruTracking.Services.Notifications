namespace PeruTracking.Services.Notifications.Services
{
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using MimeKit;

    public class MessagesService : IMessagesService
    {
        private readonly MailKitOptions options;

        public MessagesService(MailKitOptions options) => 
            this.options = options;

        public async Task SendAsync(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.Connect(options.SmtpHost, options.Port, SecureSocketOptions.StartTls);
                client.Authenticate(options.Username, options.Password);

                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }
    }
}
