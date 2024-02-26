using Exercicios___Mocks;
using FluentAssertions;
using Moq;

namespace Exercicios_Mocks.Tests
{
    public class ProdutoServiceTests
    {
        private readonly Mock<IProdutoRepository> _repositoryMock;
        private readonly ProdutoService _produtoService;

        public ProdutoServiceTests() {
            _repositoryMock = new Mock<IProdutoRepository>();
            _produtoService = new(_repositoryMock.Object);
        }


        #region Testes GetProduto
        [Fact(DisplayName = "Busca produto por ID")]
        [Trait("Entrada Válida","")]
        public void GetProduto_ID_Valido()
        {
            _produtoService.GetProduto(1);

            _repositoryMock.Verify(x => x.GetById(1), Times.Once);
        }

        #endregion


        #region Testes SalvarProduto
        [Fact(DisplayName ="Adiciona produto válido")]
        [Trait("Entrada válida", "")]
        public void SalvarProduto_ProdutoValido_Adiciona()
        {
            Produto novoProduto = new(1, "Carro", 60000);

            _produtoService.SalvarProduto(novoProduto);

            _repositoryMock.Verify(x => x.Save(novoProduto), Times.Once);
        }

        [Fact(DisplayName = "Adicionar produto nulo")]
        [Trait("Entrada inválida", "Produto nulo")]
        public void SalvarProduto_ProdutoNulo_Exception()
        {
            Action action = () => _produtoService.SalvarProduto(null);

            action
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact(DisplayName = "Adicionar produto sem nome")]
        [Trait("Entrada inválida","Produto sem nome")]
        public void SalvarProduto_ProdutoNomeNulo_Exception()
        {
            Produto produto = new Produto(1, null, 100);

            Action action = () => _produtoService.SalvarProduto(produto);

            action
                .Should()
                .Throw<ArgumentException>();
        }

        [Fact(DisplayName = "Adicionar produto sem nome")]
        [Trait("Entrada inválida", "Produto sem nome")]
        public void SalvarProduto_ProdutoNomeVazio_Exception()
        {
            Produto produto = new Produto(1, " ", 100);

            Action action = () => _produtoService.SalvarProduto(produto);

            action
                .Should()
                .Throw<ArgumentException>();
        }

        [Fact(DisplayName = "Adicionar produto preço negativo")]
        [Trait("Entrada inválida", "Preço negativo")]
        public void SalvarProduto_PrecoNegativo_Exception()
        {
            Produto produto = new Produto(1, "Carro", -100);

            Action action = () => _produtoService.SalvarProduto(produto);

            action
                .Should()
                .Throw<ArgumentException>();
        }
        #endregion

        #region Testes AtualizaProduto
        [Fact(DisplayName = "Atualiza produto válido")]
        [Trait("Entrada válida", "")]
        public void AtualizaProduto_ProdutoValido_Atualiza()
        {
            Produto produtoAtualizacao = new(1, "Carro", 60000);

            _repositoryMock.Setup(x => x.GetById(1)).Returns(produtoAtualizacao);

            _produtoService.AtualizarProduto(produtoAtualizacao);

            _repositoryMock.Verify(x => x.Update(produtoAtualizacao), Times.Once);
        }

        [Fact(DisplayName = "Atualiza produto nulo")]
        [Trait("Entrada inválida", "Produto nulo")]
        public void AtualizaProduto_ProdutoNulo_Exception()
        {

            Action action = () => _produtoService.AtualizarProduto(null);


            action
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact(DisplayName = "Atualiza produto inexistente na base")]
        [Trait("Entrada válida", "Produto inexistente")]
        public void AtualizaProduto_ProdutoInexistente_Exception()
        {
            Produto produtoAtualizacao = new(1, "Carro", 60000);

            _repositoryMock.Setup(x => x.GetById(produtoAtualizacao.Id)).Returns((Produto) null);

            Action action = () => _produtoService.AtualizarProduto(produtoAtualizacao);


            action
                .Should()
                .Throw<InvalidOperationException>();
        }


        [Fact(DisplayName = "Atualiza produto com nome nulo")]
        [Trait("Entrada inválida", "Produto com nome nulo")]
        public void AtualizaProduto_ProdutoNomeNulo_Exception()
        {
            Produto produtoAtualizacao = new(1, null, 60000);

            _repositoryMock.Setup(x => x.GetById(produtoAtualizacao.Id)).Returns(produtoAtualizacao);

            Action action = () => _produtoService.AtualizarProduto(produtoAtualizacao);


            action
                .Should()
                .Throw<ArgumentException>();
        }

        [Fact(DisplayName = "Atualiza produto com nome vazio")]
        [Trait("Entrada inválida", "Produto com nome vazio")]
        public void AtualizaProduto_ProdutoNomeVazio_Exception()
        {
            Produto produtoAtualizacao = new(1, " ", 60000);

            _repositoryMock.Setup(x => x.GetById(produtoAtualizacao.Id)).Returns(produtoAtualizacao);

            Action action = () => _produtoService.AtualizarProduto(produtoAtualizacao);


            action
                .Should()
                .Throw<ArgumentException>();
        }

        [Fact(DisplayName = "Atualiza produto com preço negativo")]
        [Trait("Entrada inválida", "Produto com preço negativo")]
        public void AtualizaProduto_ProdutoPrecoNegativo_Exception()
        {
            Produto produtoAtualizacao = new(1, "Persona 3 Reloaded", -60000);

            _repositoryMock.Setup(x => x.GetById(produtoAtualizacao.Id)).Returns(produtoAtualizacao);

            Action action = () => _produtoService.AtualizarProduto(produtoAtualizacao);


            action
                .Should()
                .Throw<ArgumentException>();
        }
        #endregion


        #region Testes ExcluirProduto
        [Fact(DisplayName = "Excluir produto válido")]
        [Trait("Entrada válida", "")]
        public void ExcluirProduto_ProdutoValido_Exclui()
        {
            Produto produtoAtualizacao = new(1, "Carro", 60000);

            _repositoryMock.Setup(x => x.GetById(1)).Returns(produtoAtualizacao);

            _produtoService.ExcluirProduto(1);

            _repositoryMock.Verify(x => x.Delete(1), Times.Once);
        }

        [Fact(DisplayName = "Excluir produto inexistente")]
        [Trait("Entrada válida", "Produto inexistente")]
        public void ExcluirProduto_ProdutoInexistente_Exception()
        {
            _repositoryMock.Setup(x => x.GetById(1)).Returns((Produto)null);

            Action action = () => _produtoService.ExcluirProduto(1);

            action
                .Should()
                .Throw<InvalidOperationException>();
        }
        #endregion

        #region Testes GetAll
        [Fact(DisplayName = "Busca todos produtos")]
        public void ObterTodosProdutos_Valido_RetornaLista()
        {
            List<Produto> produtos = new List<Produto>() { new Produto(1, "Final Fantasy VII Rebirth", 200), new Produto(2, "Persona 3 Reload", 200) };
            _repositoryMock.Setup(x => x.GetAll()).Returns(produtos);

            var result = _produtoService.ObterTodosProdutos();

            result
                .Should()
                .HaveCount(2);
            
        }
        #endregion
    }
}