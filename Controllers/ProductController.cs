using AutoMapper;
using Ecommerce.DAL.Entities;
using Ecommerce.DTO;
using Ecommerce.Errors;
using Ecommerce.Helper;
using Ecoomerce.BLL.Interfaces;
using Ecoomerce.BLL.Specifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    
    public class ProductController : BaseController
    {
        //private readonly IGenericRepository<Product> _genericRepo;
        //private readonly IGenericRepository<ProductBrand> _brandRepo;
        //private readonly IGenericRepository<ProductType> _typeRepo
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [CacheResponse(600)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Pagination<ProductDto>>> GetProductsWithSpec([FromQuery] ProductSpecificationParameters param)
        {
          
            ProductWithBrandAndTypeSpecification spec = new ProductWithBrandAndTypeSpecification(param);
            var products = await _unitOfWork.Repository<Product>().GetAllSpec(spec);
            if (products.Count == 0)
                return NotFound(new ApiResponse(404));
            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            var specForFilter = new ProductSpecificationWithFilterFourCount(param);
            var count = await _unitOfWork.Repository<Product>().GetCountAsync(spec);
            return Ok(new Pagination<ProductDto>(Data , param.PageIndex ,param.PageSize , count));
        }
       
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductByIdWithSpec(int id )
        {

            ProductWithBrandAndTypeSpecification spec = new ProductWithBrandAndTypeSpecification(p => p.Id == id);
            var product = await _unitOfWork.Repository<Product>().GetByIdSpec(spec);
            
            if (product == null)
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product,ProductDto>(product));
        }


        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            if (brands.Count == 0)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
            return Ok(brands);

        }



        [HttpGet("types")]
       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            if (types.Count == 0)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
            return Ok(types);

        }
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Product>> GetProductById(int id)
        //{
        //    return Ok(await _genericRepo.GetByIdAsync(id));
        //}

    }
}
