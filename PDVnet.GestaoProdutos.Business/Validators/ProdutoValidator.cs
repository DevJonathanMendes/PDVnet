using PDVnet.GestaoProdutos.Model;
using System;

namespace PDVnet.GestaoProdutos.Business.Validators
{
    public static class ProdutoValidator
    {
        public static void Validate(Produto produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Nome))
                throw new Exception("O nome do produto é obrigatório.");

            if (produto.Preco < 0)
                throw new Exception("O preço não pode ser negativo.");

            if (produto.Quantidade < 0)
                throw new Exception("A quantidade não pode ser negativa.");
        }
    }
}
