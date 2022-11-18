namespace PeruTracking.Services.Notifications.Handlers.Customers
{
    using MediatR;
    using PeruTracking.Services.Notifications.Builders;
    using PeruTracking.Services.Notifications.Messages.Events;
    using PeruTracking.Services.Notifications.Services;
    using System.Threading;
    using System.Threading.Tasks;

    public class CustomerCreatedHandler : AsyncRequestHandler<CustomerCreated>
    {
        private readonly MailKitOptions options;
        private readonly IMessagesService messagesService;

        public CustomerCreatedHandler(MailKitOptions options,
            IMessagesService messagesService)
        {
            this.options = options;
            this.messagesService = messagesService;
        }

        protected override Task Handle(CustomerCreated request, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var message = MessageBuilder
                .Create()
                .WithReceiver(request.FirstName, request.Email)
                .WithSender(options.Name, options.Email)
                .WithSubject("Registro de Usuario - Confirmar Cuenta")
                .WithBody("CustomerCreated",
                        new
                        {
                            name = request.FirstName,
                            url = "https://blog.christian-schou.dk/send-emails-with-asp-net-core-with-mailkit/"
                        })
                .Build();

                await messagesService.SendAsync(message);

            }, cancellationToken);
        }
    }
}
