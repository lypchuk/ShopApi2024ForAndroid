using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi2024.DTOs;
using ShopApi2024.Entities;
using ShopApi2024.Interfaces;
using ShopApi2024.Services;

namespace ShopApi2024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController (ShopApi2024DbContext context, IProductService productService,IImageWorker imageWorker, IMapper mapper) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            //return Ok(await productService.GetAll());
            var model = await context.Products.ProjectTo<ProductDto>(mapper.ConfigurationProvider).ToListAsync();
            return Ok(model);
        }

        [HttpGet("getbyid/{id:int}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        //public async Task<IActionResult> GetById(int id)
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

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromForm] CreateProductDto model)
        {
            
            string[] imagesName = new string[] { };
            if (model.Image == null)
            {
                Array.Resize(ref imagesName, 1);
                imagesName[0] = imageWorker.Save("https://picsum.photos/1200/800?category").Result;
            }
            else
            {
                Array.Resize(ref imagesName, model.Image.Length);

                for (int i = 0; i < imagesName.Length; i++)
                {
                    imagesName[i] = imageWorker.Save(model.Image[i]).Result;

                }
            }

            var entity = mapper.Map<Product>(model);
            //entity.Image = imageName;
            context.Products.Add(entity);
            await context.SaveChangesAsync();
            //context.SaveChanges();          

            for (int i = 0; i < imagesName.Length; i++)
            {

                ProductImage productImage = new ProductImage
                {
                    Image = imagesName[i],
                    Priority = i+1,
                    ProductId = entity.Id,
                };
                context.ProductImages.Add(productImage);
                await context.SaveChangesAsync();
            }
            return Ok(entity.Id);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            context.Entry(product).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;            
            }
            return NoContent();
        }

        // DELETE: api/Category/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var items = context.ProductImages.Where(x => x.ProductId == id);
            foreach (var item in items)
            {
                imageWorker.Delete(item.Image);
                context.ProductImages.Remove(item);
                await context.SaveChangesAsync();
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return NoContent();
        }


    }
}
