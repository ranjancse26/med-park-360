﻿using AutoMapper;
using MedPark.Bookings.Domain;
using MedPark.Bookings.Dto;
using MedPark.Catalog.Domain;
using MedPark.Catalog.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPark.Bookings.Mappings
{
    public class CatalogServiceMappings : Profile
    {
        public CatalogServiceMappings()
        {
            CreateMap<Product, ProductDetailDto>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
