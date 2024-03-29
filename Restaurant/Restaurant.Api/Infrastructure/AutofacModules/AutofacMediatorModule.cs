﻿using Autofac;
using FluentValidation;
using MediatR;
using Restaurant.Api.Application.Behaviors;
using Restaurant.Api.Application.Command;
using Restaurant.Api.Application.DomainEventHandler;
using Restaurant.Api.Application.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Restaurant.Api.Infrastructure.AutofacModules
{
    public class AutofacMediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();
            //Register commands
            builder.RegisterAssemblyTypes(typeof(CreateFoodCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            //Register domain events
            builder.RegisterAssemblyTypes(typeof(NewRestaurantCreatedDomainEventHandler).GetTypeInfo().Assembly)
               .AsClosedTypesOf(typeof(INotificationHandler<>));

            //Register Validators
            builder
                .RegisterAssemblyTypes(typeof(CreateFoodCommandValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();


            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });


            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        }
    }
}
