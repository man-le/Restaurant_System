﻿using MediatR;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Events
{
    public class MenuRemovedFromRestaurant : INotification
    {
        public string ResName { get; private set; }
        public Address Address { get; private set; }
        public String MenuId { get; private set; }

        public MenuRemovedFromRestaurant(string resName, Address address, string menuId)
        {
            ResName = resName;
            Address = address;
            MenuId = menuId;
        }
    }
}
