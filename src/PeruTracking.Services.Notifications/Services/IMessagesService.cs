namespace PeruTracking.Services.Notifications.Services
{
    using MimeKit;

    public interface IMessagesService
    {
        Task SendAsync(MimeMessage message);
    }
}
