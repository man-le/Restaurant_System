﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.SeedWorks;
using User.Domains.Aggregate.UserAggregate;

namespace User.Infrastructure.MediatorExtension
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, UserContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<UserAccount>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
