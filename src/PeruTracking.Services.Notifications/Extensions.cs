namespace PeruTracking.Services.Notifications
{
    using PeruTracking.Services.Notifications.Services;

    public static class Extensions
    {
        public static IServiceCollection AddMailKit(this IServiceCollection services)
        {
            services.AddSingleton(context =>
            {
                var configuration = context.GetService<IConfiguration>();
                var options = configuration.GetSection("mailkit").Get<MailKitOptions>();

                return options;
            });

            services.AddScoped<IMessagesService, MessagesService>();

            return services;
        }
    }
}
