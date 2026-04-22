using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities
{
    public class Produto
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public Categoria Categoria { get; private set; }
        public decimal Preco { get; private set; }

        public Produto(int id, string nome, Categoria categoria, decimal preco)
        {
            Id = id;
            Nome = nome;
            Categoria = categoria;
            Preco = preco;
        }

        private Produto() { }
    }
}