using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutoGW.Application.Interfaces;
using ProdutoGW.Application.Services;
using ProdutoGW.Domain.Entities;
using ProdutoGW.Domain.Requests.Produtos;
using ProdutoGW.Domain.Responses.Produtos;

namespace ProdutoGW.API.Controllers
{
    [Authorize]
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoService produtoService, IMapper mapper)
        {
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProdutoCreateRequest produtoRequest)
        {
            try
            {
                var produto = _mapper.Map<Produto>(produtoRequest);

                var createdProduto = await _produtoService.CreateAsync(produto);

                var response = _mapper.Map<ProdutoResponse>(createdProduto);

                return CreatedAtAction(nameof(Created), new { response.ProdutoGuid }, response);
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
            if (produtos == null || !produtos.Any())
                return NotFound();

            var response = _mapper.Map<IEnumerable<ProdutoResponse>>(produtos);

            return Ok(response);
        }

        [HttpGet("{produtoGuid}")]
        public async Task<IActionResult> GetByGuid(Guid produtoGuid)
        {
            var produto = await _produtoService.GetByGuidAsync(produtoGuid);
            if (produto == null)
                return NotFound();

            var response = _mapper.Map<ProdutoResponse>(produto);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProdutoCreateRequest produtoRequest)
        {
            try
            {
                var produto = _mapper.Map<Produto>(produtoRequest);

                var updatedProduto = await _produtoService.UpdateAsync(produto);

                var response = _mapper.Map<ProdutoResponse>(updatedProduto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{produtoGuid}")]
        public async Task<IActionResult> Delete(Guid produtoGuid)
        {
            await _produtoService.DeleteAsync(produtoGuid);
            return NoContent();
        }

    }
}
