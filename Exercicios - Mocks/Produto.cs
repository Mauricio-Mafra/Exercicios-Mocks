namespace Exercicios___Mocks
{
    public class Produto
    {
        public Produto(int id, string nome, double preco) {
            Id = id;
            Nome = nome;
            Preco = preco;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
    }
}
