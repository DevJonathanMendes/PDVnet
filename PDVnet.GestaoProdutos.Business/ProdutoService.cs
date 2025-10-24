using PDVnet.GestaoProdutos.Data;
using PDVnet.GestaoProdutos.Model;
using PDVnet.GestaoProdutos.Business.Validators;
using System.Collections.Generic;

namespace PDVnet.GestaoProdutos.Business
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _repository = new ProdutoRepository();

        public List<Produto> Listar() => _repository.GetAll();

        public void Adicionar(Produto produto)
        {
            ProdutoValidator.Validate(produto);
            _repository.Add(produto);
        }

        public void Atualizar(Produto produto)
        {
            ProdutoValidator.Validate(produto);
            _repository.Update(produto);
        }

        public void Excluir(int id) => _repository.Delete(id);
    }
}
