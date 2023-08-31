using ApiCatalogo.Data;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriasController(AppDbContext data)
        {
            _context= data;
        }

        [HttpGet(Name ="ObterCategoria")]
        public ActionResult<IEnumerable<Categoria>>Get() {
            List<Categoria> categoria = _context.categorias.ToList();
            if (categoria is null)
            {
                return NotFound("Nenhuma categoria encontrada.");
                }
            return categoria; 
        
        }

        [HttpGet("{id}")]
        public ActionResult<Categoria>Getid() { return Ok("Categoria retornada com sucesso"); }
       
        [HttpPost]//adição
        public ActionResult Add(Categoria categoria) {
            
            _context.Add(categoria);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterCategoria",new {id = categoria.CategoriaId },categoria); 
        }

        [HttpDelete]
        public ActionResult Delete(int id) {

            Categoria categoria = _context.categorias.First(i =>i.CategoriaId ==id);
            if(categoria is null)
            {
                return NotFound("Não encontrado!");
            }
            _context.categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok("Produto Excluido");
        }

        [HttpPut("{id}")]
        public ActionResult Editar(int id) { return Ok("Editado com sucesso"); }
    }
}
