﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPark.Web.Models.OrderService
{
    public class OrderSummaryDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string OrderNo { get; set; }
        public DateTime DatePlaced { get; set; }
        public DateTime? DatePaid { get; set; }
        public decimal OrderTotal { get; set; }
        public int? ShippingType { get; set; }
        public int OrderStatus { get; set; }
    }
}
