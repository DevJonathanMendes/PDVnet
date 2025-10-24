using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using PDVnet.GestaoProdutos.Model;

namespace PDVnet.GestaoProdutos.Data
{
    public class ProdutoRepository
    {
        public List<Produto> GetAll()
        {
            List<Produto> produtos = new List<Produto>();

            using (SqlConnection conn = ConnectionHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Produto ORDER BY Id DESC", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    produtos.Add(new Produto
                    {
                        Id = (int)reader["Id"],
                        Nome = reader["Nome"].ToString(),
                        Descricao = reader["Descricao"].ToString(),
                        Preco = (decimal)reader["Preco"],
                        Quantidade = (int)reader["Quantidade"],
                        DataCadastro = (DateTime)reader["DataCadastro"]
                    });
                }
            }

            return produtos;
        }

        public void Add(Produto produto)
        {
            using (SqlConnection conn = ConnectionHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Produto (Nome, Descricao, Preco, Quantidade, DataCadastro) " +
                    "VALUES (@Nome, @Descricao, @Preco, @Quantidade, GETDATE())", conn);

                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Produto produto)
        {
            using (SqlConnection conn = ConnectionHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Produto SET Nome=@Nome, Descricao=@Descricao, Preco=@Preco, Quantidade=@Quantidade WHERE Id=@Id", conn);

                cmd.Parameters.AddWithValue("@Id", produto.Id);
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = ConnectionHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Produto WHERE Id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
