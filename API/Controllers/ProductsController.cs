using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using core.Interfaes;
using core.Specifications;
using AutoMapper;
using API.Dtos;
using API.Errors;


namespace API.Controllers
{
   
    public class ProductsController:BaseApiController
    {
        private readonly IGenericInterface<ProductBrand> _productBrandRepo;
        private readonly IGenericInterface<ProductType> _productTypeRepo;
        private readonly IGenericInterface<Product> _productsRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericInterface<Product> productsRepo,
            IGenericInterface<ProductType> productTypeRepo, 
            IGenericInterface<ProductBrand> productBrandRepo ,IMapper mapper)
        {
            _mapper = mapper;
            _productsRepo = productsRepo;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
        }


        [HttpGet]
          
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productsRepo.ListAsync(spec);
          
           return Ok(_mapper.Map<IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
             var spec = new ProductsWithTypesAndBrandsSpecification(id);
           var product= await _productsRepo.GetEntityWithSpec(spec);
             if (product == null) return NotFound(new ApiResponse(404));
             return _mapper.Map<Product,ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
        
    }
}