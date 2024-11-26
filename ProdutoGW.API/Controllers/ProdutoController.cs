using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutoGW.Application.Interfaces;
using ProdutoGW.Domain.Entities;
using ProdutoGW.Domain.Requests.Produtos;
using ProdutoGW.Domain.Responses.Produtos;

namespace ProdutoGW.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoService produtoService, IMapper mapper)
        {
            _produtoService = produtoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Inclusão de um novo produto.
        /// </summary>
        /// <returns>O produto criado.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduto([FromBody] ProdutoCreateRequest produtoRequest)
        {
            try
            {
                var produto = _mapper.Map<Produto>(produtoRequest);

                var createdProduto = await _produtoService.CreateAsync(produto);

                var response = _mapper.Map<ProdutoResponse>(createdProduto);

                return CreatedAtAction(nameof(GetByGuid), new { produtoGuid = createdProduto.Guid }, createdProduto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Consulta todos os produtos.
        /// </summary>
        /// <returns>Uma lista com os produtos existentes.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await _produtoService.GetAllAsync();
            if (produtos == null || !produtos.Any())
                return Ok(new List<Produto>());

            var response = _mapper.Map<IEnumerable<ProdutoResponse>>(produtos);

            return Ok(response);
        }

        /// <summary>
        /// Consulta um produto específico.
        /// </summary>
        /// <returns>Um produtos existente que corresponda ao GUID.</returns>
        [HttpGet("{produtoGuid}")]
        public async Task<IActionResult> GetByGuid(Guid produtoGuid)
        {
            var produto = await _produtoService.GetByGuidAsync(produtoGuid);
            if (produto == null)
                return NotFound();

            var response = _mapper.Map<ProdutoResponse>(produto);

            return Ok(response);
        }

        /// <summary>
        /// Atualiza um produto específico.
        /// </summary>
        /// <returns>O produto atualizado.</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProdutoUpdateRequest produtoRequest)
        {
            try
            {
                var produto = await _produtoService.GetByGuidAsync(produtoRequest.Guid);
                if (produto == null)
                    return NotFound();

                produto = _mapper.Map<Produto>(produtoRequest);

                var updatedProduto = await _produtoService.UpdateAsync(produto);

                var response = _mapper.Map<ProdutoResponse>(updatedProduto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclusão de um produto específico.
        /// </summary>
        [HttpDelete("{produtoGuid}")]
        public async Task<IActionResult> Delete(Guid produtoGuid)
        {
            var produto = await _produtoService.GetByGuidAsync(produtoGuid);
            if (produto == null)
                return NotFound(); 

            await _produtoService.DeleteAsync(produtoGuid);
            return NoContent();
        }

    }
}
