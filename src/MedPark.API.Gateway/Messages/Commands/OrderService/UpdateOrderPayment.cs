﻿using MedPark.Common.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPark.API.Gateway.Messages.Commands.OrderService
{
    [MessageNamespace("order-service")]
    public class UpdateOrderPayment : ICommand
    {
        public Guid OrderId { get; set; }
        public Guid PaymentMethodId { get; set; }

        [JsonConstructor]
        public UpdateOrderPayment(Guid orderId, Guid paymentMethodId)
        {
            OrderId = orderId;
            PaymentMethodId = paymentMethodId;
        }
    }
}
