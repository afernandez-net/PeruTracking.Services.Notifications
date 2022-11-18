using Microsoft.AspNetCore.Mvc;
using PeruTracking.Services.Notifications.Builders;
using PeruTracking.Services.Notifications.Services;
using static PeruTracking.Services.Notifications.Constants;

namespace PeruTracking.Services.Notifications.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly MailKitOptions options;
        private readonly IMessagesService messagesService;

        public HomeController(MailKitOptions options,
            IMessagesService messagesService)
        {
            this.options = options;
            this.messagesService = messagesService;
        }
        [HttpGet]
        public IActionResult Get() => Ok("PeruShop Notifications Service");

        [HttpGet("Send")]
        public async Task<IActionResult> Send()
        {
            //Bandeja de correo https://ethereal.email/messages

            var message = MessageBuilder
                .Create()
                .WithReceiver("Juan Perez", "jermaine.leuschke18@ethereal.email")
                .WithSender(options.Name,options.Email)
                .WithSubject("Registro de Usuario - Confirmar Cuenta")
                .WithBody(Template.CustomerCreated,
                          new { 
                              name = "Juan Perez", 
                              url = "https://blog.christian-schou.dk/send-emails-with-asp-net-core-with-mailkit/" 
                          })
                .Build();

            await messagesService.SendAsync(message);

            return Ok("Correo Enviado");
        }
    }
}
