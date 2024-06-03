using Application;
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
        private readonly ProductService _services;
        private readonly IMapper _mapper;

        public ProductController(ProductService services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        [HttpGet("GetById/{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            return Ok(_services.GetById(id));
        }

        [HttpPost]
        public ActionResult Create(ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            _services.Add(_mapper.Map<Product>(productDto));

            return Ok();
        }
    }
}
