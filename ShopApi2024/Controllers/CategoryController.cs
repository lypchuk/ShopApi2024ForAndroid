﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi2024.DTOs;
using ShopApi2024.Entities;
using ShopApi2024.Interfaces;

namespace ShopApi2024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CategoryController(ICategoryService categoryService/*,ShopApi2024Db _context, IMapper mapper*//*, IConfiguration configuration*/) : ControllerBase
    {
        //private readonly ICategoryService categoryService;
        //public CategoryController(ICategoryService categoryService)
        //{
        //    this.categoryService = categoryService;
        //}


        [HttpGet("all")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.ADULT)]
        public async Task<IActionResult> Get()
        {
            //return Ok(await categoryService.GetAllTeacher()); //not wort in this project .... to work with dbcontext

            return Ok(await categoryService.GetAll());

            //var model = await _context.Categories.ProjectTo<Category>(mapper.ConfigurationProvider).ToListAsync();
            //return Ok(model);
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
