namespace PeruTracking.Services.Notifications.Builders
{
    using MimeKit;

    public interface IMessageBuilder
    {
        IMessageBuilder WithSender(string name, string senderEmail);
        IMessageBuilder WithReceiver(string name, string receiverEmail);
        IMessageBuilder WithSubject(string subject);
        IMessageBuilder WithSubject(string template, params object[] @params);
        IMessageBuilder WithBody(string body);
        IMessageBuilder WithBody(string template, object @params);
        MimeMessage Build();
    }
}
