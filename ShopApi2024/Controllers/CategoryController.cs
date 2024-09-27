using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi2024.DTOs;
using ShopApi2024.Interfaces;

namespace ShopApi2024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            
        }


        [HttpGet("all")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.ADULT)]
        public async Task<IActionResult> Get()
        {
            return Ok(await categoryService.GetAll());
        }

        [HttpGet("allWithProducts")]
        public async Task<IActionResult> GetWithProducts()
        {
            return Ok(await categoryService.GetAllWithProducts());
        }

        [HttpGet("get/{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await categoryService.Get(id));
        }

        [HttpDelete("delete/{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            categoryService.Delete(id);
            return Ok();
        }

        [HttpDelete("softDelete/{id:int}")]
        public IActionResult SoftDelete([FromRoute] int id)
        {
            categoryService.SoftDelete(id);
            return Ok();
        }


        [HttpPost("create")]
        public IActionResult Create([FromForm] CreateCategoryDto model)
        {
            categoryService.Create(model);
            return Ok();
        }

        [HttpPut("edit")]
        //public IActionResult Edit([FromBody] CategoryDto model)
        public IActionResult Edit([FromForm] UpdateCategoryDto model)
        {
            categoryService.Edit(model);
            return Ok();
        }

    }
}
