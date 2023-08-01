using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using MercadoZe.Classes;

namespace MercadoZe.Classes.DAO
{
    public class PedidoDao
    {

        private string _connectionString = @"server=.\SQLexpress;initial catalog=MERCADO_ZE_DB;integrated security=true;";

        public PedidoDao()
        {

        }

        public void AdicionarPedido(Pedido novoPedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"INSERT PEDIDO VALUES (@DATA_HORA, @QUANTIDADE, @VALOR_TOTAL, @PRODUTO_ID, @CPF_CLIENTE) ";

                    ConverterObjetoParaSql(novoPedido, comando);

                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Pedido> BuscarPedidos()
        {
            var listaPedidos = new List<Pedido>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT * FROM PEDIDOS";

                    comando.CommandText = sql;
                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {

                        Pedido pedidoBuscado = ConverterSqlParaObjeto(leitor);
                        listaPedidos.Add(pedidoBuscado);

                    }
                }
            }

            return listaPedidos;
        }

        private Pedido ConverterSqlParaObjeto(SqlDataReader leitor)
        {
            var pedido = new Pedido();

            pedido.DataHora = Convert.ToDateTime(leitor["DATA_HORA"].ToString());
            pedido.Quantidade = int.Parse(leitor["QUANTIDADE"].ToString());
            pedido.ValorTotal = double.Parse(leitor["VALOR_TOTAL"].ToString());
            pedido.ProdutoId = int.Parse(leitor["PRODUTO_ID"].ToString());
            pedido.CpfCliente = int.Parse(leitor["DESCRICAO"].ToString());

            return pedido;
        }

        private void ConverterObjetoParaSql(Pedido pedido, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("@DATA_HORA", pedido.DataHora);
            comando.Parameters.AddWithValue("@QUANTIDADE", pedido.Quantidade);
            comando.Parameters.AddWithValue("@VALOR_TOTAL", pedido.ValorTotal);
            comando.Parameters.AddWithValue("@PRODUTO_ID", pedido.ProdutoId);
            comando.Parameters.AddWithValue("@CPF_CLIENTE", pedido.CpfCliente);
        }

    }
}