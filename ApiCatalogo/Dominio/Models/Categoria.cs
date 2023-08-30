using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCatalogo.Models
{
    public class Categoria
    {
        public int? CategoriaId { get; set; }
        public string? Nome { get; set; }
        public string?  ImagemUrl { get; set; }

        public IEnumerable<Produto>? produtos { get; set; }
    }
}
