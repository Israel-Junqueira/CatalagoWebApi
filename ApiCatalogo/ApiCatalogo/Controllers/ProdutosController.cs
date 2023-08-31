using ApiCatalogo.Data;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext Data)
        {
            this._context = Data;
        }
        #region Get Produtos
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {//IEnumerable é por ser uma interface somente leitura
            //não tenho toda coleção na memoria
            //
            List<Produto> produtos = _context.produto.ToList();
            if (produtos == null)
            {
                return NotFound();
            }
            return produtos;
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> GetId(int id)
        {
            Produto produto = _context.produto.FirstOrDefault(c => c.ProdutoId == id);
            if (produto is null)
            {
                return NotFound();
            }
            return produto;
        }
        [HttpGet("{nome}")]
        public ActionResult<Produto> Getnome(string nome)
        {
            Produto produto = _context.produto.FirstOrDefault(c => c.Nome == nome);
            if (produto is null)
            {
                return NotFound();
            }
            return produto;
        }
        #endregion

        #region Criar Produtos

        [HttpPost]
        public ActionResult Add(Produto produto)
        {
            if (produto is null)
            {
                return BadRequest();
            }
            _context.Add(produto);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }

        #endregion
        [HttpPut("{id}")]
        public ActionResult Edit(int id, Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest();
            }
            
            _context.Entry(produto).State=EntityState.Modified;
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            Produto DeletarProduto = _context.produto.FirstOrDefault(c=> c.ProdutoId == id);
            if(DeletarProduto is null)
            { 
                return NotFound("Produto não localizado");
            }



            _context.produto.Remove(DeletarProduto);
            _context.SaveChanges();
            return Ok("Produto Excluido");
        }


    }
}
