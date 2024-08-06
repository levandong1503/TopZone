using Application;
using Application.Interface;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopZone.Dtos;

namespace TopZone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productServices;
        private readonly ITypeProductService _typeProductServices;
        private readonly IMapper _mapper;

        public ProductController(IProductService services, IMapper mapper, ITypeProductService typeProductService)
        {
            _productServices = services;
            _typeProductServices = typeProductService;
            _mapper = mapper;
        }

        [HttpGet("GetById/{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            return Ok(_productServices.GetById(id));
        }

        [HttpPost]
        public ActionResult Create(ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            var newProduct = _productServices.Add(_mapper.Map<Product>(productDto));
            _typeProductServices.get

            
            return Ok();
        }

        [HttpGet("GetProductsOfTypes")]
        public ActionResult GetProducsOfType([FromQuery] int idType, int productTaking = 5)
        {
            var producsOfTypes = _productServices.GetHotProductsOfType(idType,productTaking);
            return Ok(producsOfTypes);
        }

        [HttpGet("GetProductsByTypeName")]
        public ActionResult GetProductsByTypeName([FromQuery] string typeName)
        {
            return Ok(_productServices.GetProductsByNameType(typeName));
        }

        [HttpGet("GetDetailProduct/{id}")]
        public ActionResult GetDetailProduct([FromRoute] int id)
        {
            return Ok(_productServices.GetDetailProduct(id));
        }
    }
}
