namespace PeruTracking.Services.Notifications.Handlers.Customers
{
    using MediatR;
    using PeruTracking.Services.Notifications.Builders;
    using PeruTracking.Services.Notifications.Messages.Events;
    using PeruTracking.Services.Notifications.Services;
    using System.Threading;
    using System.Threading.Tasks;
    using static PeruTracking.Services.Notifications.Constants;

    public class CustomerCreatedHandler : AsyncRequestHandler<CustomerCreated>
    {
        private readonly MailKitOptions options;
        private readonly IMessagesService messagesService;

        public CustomerCreatedHandler(MailKitOptions options, IMessagesService messagesService)
        {
            this.options = options;
            this.messagesService = messagesService;
        }

        protected override async Task Handle(CustomerCreated request, CancellationToken cancellationToken)
        {
            var message = MessageBuilder
                .Create()
                .WithReceiver($"{request.LastName}, {request.FirstName}", request.Email)
                .WithSender(options.Name, options.Email)
                .WithSubject(Subject.CustomerCreated)
                .WithBody(Template.CustomerCreated,
                        new
                        {
                            name = request.FirstName,
                            url = "https://blog.christian-schou.dk/send-emails-with-asp-net-core-with-mailkit/"
                        })
                .Build();

            await messagesService.SendAsync(message);
        }
    }
}
