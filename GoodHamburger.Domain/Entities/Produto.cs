using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities
{
    public class Produto
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public Categoria Categoria { get; private set; }
        public decimal Preco { get; private set; }

        public Produto(string nome, Categoria categoria, decimal preco)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Categoria = categoria;
            Preco = preco;
        }
    }
}