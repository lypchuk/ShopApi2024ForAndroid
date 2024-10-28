using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi2024.DTOs;
using ShopApi2024.Entities;
using ShopApi2024.Interfaces;

namespace ShopApi2024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController (ShopApi2024Db context, IProductService productService, IMapper mapper) : ControllerBase
    {
        //private readonly IProductService productService;

        //public ProductController(IProductService productService)
        //{
        //    this.productService = productService;
        //}


        [HttpGet("all")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.ADULT)]
        public async Task<IActionResult> Get()
        {
            //return Ok(await productService.GetAll());
            var model = await context.Products.ProjectTo<ProductDto>(mapper.ConfigurationProvider).ToListAsync();
            return Ok(model);
        }

        [HttpGet("getbyid/{id:int}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.ADULT)]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            //return Ok(await productService.GetAll());
            var model = await context.Products.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }
            var dto = mapper.Map<ProductDto>(model);
            //.ProjectTo<ProductDto>(mapper.ConfigurationProvider);
            return Ok(dto);
        }

    }
}
