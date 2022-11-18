namespace PeruTracking.Services.Notifications.Messages.Events
{
    using MediatR;
    using Newtonsoft.Json;
    using PeruTracking.Common.EventBus.Messaging;
    using System;

    public class CustomerCreated : IEvent, IRequest
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }

        [JsonConstructor]
        public CustomerCreated(Guid id, string firstName, string lastName, string email)
        {
            Id = id;
            FirstName = firstName; 
            LastName = lastName;
            Email = email;
        }
    }
}
