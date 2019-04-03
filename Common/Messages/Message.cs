using Common.Enum;
using NServiceBus;
using System;

namespace Common.Messages
{
    public class Message : IMessage
    {
        public Guid Id { get; set; }
        public RequestType RequestType { get; set; }
        public string Content { get; set; }
        public bool IsCreated { get; set; }
        public bool IsSucceded { get; set; }
    }
}
