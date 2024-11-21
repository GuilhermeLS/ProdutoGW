using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutoGW.Application.Interfaces;
using ProdutoGW.Application.Services;
using ProdutoGW.Domain.Entities;

namespace ProdutoGW.API.Controllers
{
    [Authorize]
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Produto produto)
        {
            try
            {
                var createdProduto = await _produtoService.CreateAsync(produto);
                return CreatedAtAction(nameof(GetById), new { id = createdProduto.Id }, createdProduto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await _produtoService.GetAllAsync();
            if (produtos == null)
                return NotFound();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _produtoService.GetByIdAsync(id);
            if (produto == null)
                return NotFound();
            return Ok(produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Produto produto)
        {
            if (id != produto.Id)
                return BadRequest("ID no caminho e corpo não correspondem.");

            try
            {
                var updatedProduto = await _produtoService.UpdateAsync(produto);
                return Ok(updatedProduto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _produtoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
