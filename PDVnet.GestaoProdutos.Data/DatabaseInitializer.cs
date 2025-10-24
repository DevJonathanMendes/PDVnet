using System;
using System.Configuration;
using System.Data.SqlClient;

namespace PDVnet.GestaoProdutos.Data
{
    public static class DatabaseInitializer
    {
        public static void EnsureDatabase()
        {
            string connStr = ConfigurationManager.ConnectionStrings["PDVnetDB"].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connStr);
            string dbName = builder.InitialCatalog;
            builder.InitialCatalog = "master";

            using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand($"IF DB_ID('{dbName}') IS NULL CREATE DATABASE [{dbName}]", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Produto' AND xtype='U')
                CREATE TABLE Produto (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    Nome NVARCHAR(100) NOT NULL,
                    Descricao NVARCHAR(255) NULL,
                    Preco DECIMAL(10,2) NOT NULL,
                    Quantidade INT NOT NULL,
                    DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
                )";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
