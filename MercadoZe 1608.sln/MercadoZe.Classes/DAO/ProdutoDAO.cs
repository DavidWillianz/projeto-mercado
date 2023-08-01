using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MercadoZe.Classes;

namespace MercadoZe.Classes.DAO
{
    public class ProdutoDAO
    {
        private string _connectionString = @"server=.\SQLexpress;initial catalog=MERCADO_ZE_DB;integrated security=true;";
        public ProdutoDAO()
        {
        }

        public void AdicionarProduto(Produto novoProduto)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"INSERT PRODUTO VALUES (@NOME, @DESCRICAO, @DATA_VENCIMENTO, 
                    @PRECO_UNITARIO, @UNIDADE, @QUANTIDADE_ESTOQUE);";

                    ConverterObjetoParaSql(novoProduto, comando);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Produto> BuscaTodos()
        {
            var listaProduto = new List<Produto>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT ID_PRODUTO, NOME, UNIDADE, DATA_VENCIMENTO, 
                    PRECO_UNITARIO, DESCRICAO, QUANTIDADE_ESTOQUE FROM PRODUTO;";

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        Produto produtoBuscado = ConverterSqlParaObjeto(leitor);

                        listaProduto.Add(produtoBuscado);
                    }
                }
            }

            return listaProduto;
        }

        public void DeletarProduto(Produto produtoBuscado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"DELETE FROM PRODUTO WHERE ID_PRODUTO = @ID;";

                    comando.Parameters.AddWithValue("@ID", produtoBuscado.Id);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarProduto(Produto produtoBuscado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE PRODUTO SET            
                                        NOME = @NOME,
                                        DESCRICAO = @DESCRICAO,
                                        DATA_VENCIMENTO = @DATA_VENCIMENTO,
                                        PRECO_UNITARIO = @PRECO_UNITARIO,
                                        UNIDADE = @UNIDADE
                                 WHERE ID_PRODUTO = @ID;";

                    ConverterObjetoParaSql(produtoBuscado, comando);
                    comando.Parameters.AddWithValue("@ID", produtoBuscado.Id);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void EntradaESaidaEstoque(Produto produtoBuscado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE PRODUTO SET QUANTIDADE_ESTOQUE = @QUANTIDADE_ESTOQUE WHERE ID_PRODUTO = @ID;";

                    ConverterObjetoParaSql(produtoBuscado, comando);
                    comando.Parameters.AddWithValue("@ID", produtoBuscado.Id);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Produto> BuscaPorTexto(string nome)
        {
            var listaProduto = new List<Produto>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT ID_PRODUTO, NOME, UNIDADE, DATA_VENCIMENTO, 
                    PRECO_UNITARIO, DESCRICAO, QUANTIDADE_ESTOQUE FROM PRODUTO WHERE DESCRICAO LIKE @TEXTO;";

                    comando.Parameters.AddWithValue("@TEXTO", $"%{nome}%");

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var produtoBuscado = ConverterSqlParaObjeto(leitor);

                        listaProduto.Add(produtoBuscado);
                    }
                }
            }

            return listaProduto;
        }

        public Produto BuscarPorId(int idProduto)
        {
            var produtoBuscado = new Produto();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT ID_PRODUTO, NOME, UNIDADE, DATA_VENCIMENTO, 
                    PRECO_UNITARIO, DESCRICAO, QUANTIDADE_ESTOQUE FROM PRODUTO WHERE ID_PRODUTO = @ID;";

                    comando.Parameters.AddWithValue("@ID", idProduto);

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        produtoBuscado = ConverterSqlParaObjeto(leitor);
                    }
                }
            }

            return produtoBuscado;
        }

        private Produto ConverterSqlParaObjeto(SqlDataReader leitor)
        {
            var produto = new Produto();
            produto.Id = int.Parse(leitor["ID_PRODUTO"].ToString());
            produto.Nome = leitor["NOME"].ToString();
            produto.DataVencimento = Convert.ToDateTime(leitor["DATA_VENCIMENTO"].ToString());
            produto.PrecoUnitario = double.Parse(leitor["PRECO_UNITARIO"].ToString());
            produto.Unidade = leitor["UNIDADE"].ToString();
            produto.Descricao = leitor["DESCRICAO"].ToString();
            produto.QuantidadeEstoque = int.Parse(leitor["QUANTIDADE_ESTOQUE"].ToString());

            return produto;
        }

        private void ConverterObjetoParaSql(Produto produto, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("@ID_PRODUTO", produto.Id);
            comando.Parameters.AddWithValue("@NOME", produto.Nome);
            comando.Parameters.AddWithValue("@DESCRICAO", produto.Descricao);
            comando.Parameters.AddWithValue("@DATA_VENCIMENTO", produto.DataVencimento);
            comando.Parameters.AddWithValue("@PRECO_UNITARIO", produto.PrecoUnitario);
            comando.Parameters.AddWithValue("@UNIDADE", produto.Unidade);
            comando.Parameters.AddWithValue("@QUANTIDADE_ESTOQUE", produto.QuantidadeEstoque);
        }
    }
}