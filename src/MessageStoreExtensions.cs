using System;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using AlbedoTeam.Sdk.EventStore.Configuration;
using AlbedoTeam.Sdk.EventStore.Db;
using AlbedoTeam.Sdk.EventStore.Mappers;
using MassTransit.Audit;
using Microsoft.Extensions.DependencyInjection;

namespace AlbedoTeam.Sdk.EventStore
{
    public static class MessageStoreExtensions
    {
        private static IServiceCollection AddEventStore(
            this IServiceCollection services)
        {
            services.AddScoped<IMessageAuditStore, MongoMessageAuditStore>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IMessageMapper, MessageMapper>();

            var provider = services.BuildServiceProvider();

            var dbSettings = provider.GetService<IDbSettings>();
            if (dbSettings == null)
                throw new InvalidOperationException("Please add Data Access Layer to start Event Store");

            // todo espetar no hub do mass transit
            // services.AddMassTransit(configure =>
            // {
            //     // ...
            //     configure.UsingRabbitMq((context, cfg) =>
            //     {
            //         // ...
            //         cfg.ConnectSendAuditObservers(auditStore);
            //     });
            //     // ...
            //     consumerRegistration.Add<EventRedeliveryRequestConsumer>();
            //     // ...
            //     requestClientRegistration.Add<EventRedeliveryRequest>();
            // });

            return services;
        }
    }
}