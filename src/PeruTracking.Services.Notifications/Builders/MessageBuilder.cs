namespace PeruTracking.Services.Notifications.Builders
{
    using HandlebarsDotNet;
    using MimeKit;
    using System.Text;

    public class MessageBuilder : IMessageBuilder
    {
        private readonly MimeMessage message;

        private MessageBuilder()
        {
            message = new MimeMessage();
        }

        public static IMessageBuilder Create()
            => new MessageBuilder();

        IMessageBuilder IMessageBuilder.WithSender(string name,string senderEmail)
        {
            message.From.Add(new MailboxAddress(name,senderEmail));
            return this;
        }

        IMessageBuilder IMessageBuilder.WithReceiver(string name, string receiverEmail)
        {
            message.To.Add(new MailboxAddress(name,receiverEmail));
            return this;
        }

        IMessageBuilder IMessageBuilder.WithSubject(string subject)
        {
            message.Subject = subject;
            return this;
        }

        IMessageBuilder IMessageBuilder.WithSubject(string template, params object[] @params)
            => ((IMessageBuilder)this).WithSubject(string.Format(template, @params));

        IMessageBuilder IMessageBuilder.WithBody(string body)
        {
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };
            return this;
        }
        
        IMessageBuilder IMessageBuilder.WithBody(string template, object @params)
        {
            //Templates bootstrap
            //https://bbbootstrap.com/snippets/confirm-account-email-template-17848137

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string templateDir = Path.Combine(baseDir, "Templates");
            string templatePath = Path.Combine(templateDir, $"{template}.html");

            using FileStream fileStream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using StreamReader streamReader = new StreamReader(fileStream, Encoding.Default);
            string html = streamReader.ReadToEnd();

            var compile = Handlebars.Compile(html);
            var body = compile(@params);

            return ((IMessageBuilder)this).WithBody(body);
        }

        MimeMessage IMessageBuilder.Build()
            => message;
    }
}
