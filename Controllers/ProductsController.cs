using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAdvance.DAL.EfCore;
using WebApiAdvance.DAL.Repositories.Abstracts;
using WebApiAdvance.DAL.UnitOfWork.Abstracts;
using WebApiAdvance.Entities;
using WebApiAdvance.Entities.Dtos.Products;

namespace WebApiAdvance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(AppDbContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetProducts()
        {
            if (await _unitOfWork.ProductRepository.GetAllAsync() == null)
            {
                return NotFound();
            }
            var result = await _unitOfWork.ProductRepository.GetAllAsync(includes:"Brand");
            List<GetProductDto> getProductDtos = _mapper.Map<List<GetProductDto>>(result);
            return getProductDtos;
        }
        
        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> GetProduct(int id)
        {
            if (await _unitOfWork.ProductRepository.GetAllAsync() == null)
            {
                return NotFound();
            }
            var product = await _unitOfWork.ProductRepository.GetAsync(x=>x.Id == id,"Brand");
            if (product == null)
            {
                return NotFound();
            }
            GetProductDto getProductDto = _mapper.Map<GetProductDto>(product);
            return getProductDto;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UpdateProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }
            Product product = _mapper.Map<Product>(productDto);
            product.ProductCode = product.Name.Substring(0, 2);
            _unitOfWork.ProductRepository.Update(product);

            try
            {
                await _unitOfWork.SaveChangeAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.ProductRepository.ExistAsync(x=>x.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<GetProductDto>> PostProduct([FromBody]CreateProductDto productDto)
        {
            if (productDto is null) return BadRequest();
            Product product = _mapper.Map<Product>(productDto);
            product.Created = DateTime.UtcNow;
            product.ProductCode = product.Name.Substring(0, 2);
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveChangeAsync();

            GetProductDto getProductDto = _mapper.Map<GetProductDto>(product);
            return (getProductDto);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_unitOfWork.ProductRepository.GetAllAsync() == null)
            {
                return NotFound();
            }
            var product = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.ProductRepository.Delete(product);
            await _unitOfWork.SaveChangeAsync();

            return NoContent();
        }
    }
}
