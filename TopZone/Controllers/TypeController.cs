using Application.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopZone.Dtos;
using Type = Domain.Entities.Type;

namespace TopZone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _services;
        private readonly IMapper _mapper;

        public TypeController(ITypeService services, IMapper mapper) 
        {
            _services = services;
            _mapper = mapper;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            return Ok(_services.GetById(id));
        }

        [HttpPost]
        public IActionResult Create(TypeDto type)
        {
            if (type == null)
            {
                return BadRequest();
            }

            _services.Add(_mapper.Map<Type>(type));

            return Ok();

        }

        //[HttpGet]
        //public IActionResult GetTopProductForEveryType(TypeOfPrroduct typeOfPrroduct)
        //{
               
        //}

    }
}
