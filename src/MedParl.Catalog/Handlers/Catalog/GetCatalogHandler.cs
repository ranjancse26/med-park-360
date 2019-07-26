﻿using MedPark.Catalog.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedPark.Common;
using MedPark.Catalog.Queries;
using MedPark.Common.Handlers;
using MedPark.Catalog.Domain;
using AutoMapper;
using MedPark.Common.Types;

namespace MedPark.Catalog.Handlers.Catalog
{
    public class GetCatalogHandler : IQueryHandler<CatalogQuery, CatalogDetailDto>
    {
        private IMedParkRepository<Product> _productsRepo { get; }
        private IMedParkRepository<ProductCatalog> _catalogRepo { get; }
        private IMedParkRepository<Category> _categoryRepo { get; }

        private IMapper _mapper { get; }

        public GetCatalogHandler(IMedParkRepository<Product> productsRepo, IMapper mapper, IMedParkRepository<ProductCatalog> catalogRepo, IMedParkRepository<Category> categoryRepo)
        {
            _productsRepo = productsRepo;
            _mapper = mapper;
            _catalogRepo = catalogRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<CatalogDetailDto> HandleAsync(CatalogQuery query)
        {
            IEnumerable<Category> categories = new List<Category>();
            List<Product> products = new List<Product>();

            if(query.ParentCategoryId is null)
            {
                //Get the entire catalog
                categories = await _categoryRepo.FindAsync(x => x.ParentCategory != null);
            }
            else
            {
                //Get the entire catalog for a parent category

                bool subCatExists = await _categoryRepo.ExistsAsync(x => x.Id == query.ParentCategoryId && x.ParentCategory.HasValue);

                if (!subCatExists)
                    throw new MedParkException("sub_category_does_not_exist", $"Sub category {query.ParentCategoryId} does not exist.");

                categories = await _categoryRepo.FindAsync(x => x.ParentCategory == query.ParentCategoryId);
            }

            products = (from p in await _productsRepo.GetAllAsync()
                        join pc in await _catalogRepo.GetAllAsync() on p.Id equals pc.ProductId
                        where categories.Select(x => x.Id).Contains(pc.CategoryId)
                        select p).ToList();

            return new CatalogDetailDto
            {
                Categories = _mapper.Map<List<CategoryDto>>(categories),
                Products = _mapper.Map<List<ProductDetailDto>>(products)
            };
        }
    }
}
