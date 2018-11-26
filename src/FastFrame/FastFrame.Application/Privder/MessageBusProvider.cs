﻿using CSRedis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using System;
using System.Threading.Tasks;

namespace FastFrame.Application.Privder
{
    public class MessageBusProvider : IMessageBus
    {
        private readonly CSRedisClient redisClient;

        public MessageBusProvider(CSRedisClient redisClient)
        {
            Console.WriteLine("MessageBusProvider Init");
            this.redisClient = redisClient;
        }
        public async Task PubLishAsync(Message message)
        {
            await redisClient.PublishAsync("message.publish", message.ToJson());
        }
    }
}