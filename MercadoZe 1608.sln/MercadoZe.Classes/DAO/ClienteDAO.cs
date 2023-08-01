using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using MercadoZe.Classes;

namespace MercadoZe.Classes.DAO
{
    public class ClienteDAO
    {
        private string _connectionString = @"server=.\SQLexpress;initial catalog=MERCADO_ZE_DB;integrated security=true;";

        public ClienteDAO()
        {

        }
        public void AdicionarCliente(Cliente novoCliente)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"INSERT CLIENTE VALUES (@CPF, @NOME, @DATA_NASCIMENTO, 
                    @PONTOS_FIDELIDADE, @RUA, @NUMERO, @BAIRRO, @CEP, @COMPLEMENTO);";

                    ConverterObjetoParaSql(novoCliente, comando);

                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Cliente> BuscaTodos()
        {
            var listaCliente = new List<Cliente>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT CPF, NOME, DATA_NASCIMENTO, 
                                     PONTOS_FIDELIDADE, RUA, NUMERO, 
                                     BAIRRO, CEP, COMPLEMENTO FROM CLIENTE;";

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        Cliente clienteBuscado = ConverterSqlParaObjeto(leitor);

                        listaCliente.Add(clienteBuscado);
                    }
                }
            }

            return listaCliente;
        }

        public void DeletarCliente(Cliente clienteBuscado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"DELETE FROM PROFESSOR WHERE CPF = @CPF_CLIENTE;";

                    comando.Parameters.AddWithValue("@CPF_CLIENTE", clienteBuscado.CPF);

                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarCliente(Cliente clienteBuscado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE CLIENTE SET            
                                        NOME = @NOME,
                                        DATA_NASCIMENTO = @DATA_NASCIMENTO,
                                        PONTOS_FIDELIDADE = @PONTOS_FIDELIDADE,
                                        RUA = @RUA,
                                        NUMERO = @NUMERO,
                                        BAIRRO = @BAIRRO,
                                        CEP = @CEP,
                                        COMPLEMENTO = @COMPLEMENTO
                                 WHERE CPF = @CPF_CLIENTE;";

                    ConverterObjetoParaSql(clienteBuscado, comando);
                    comando.Parameters.AddWithValue("@CPF_CLIENTE", clienteBuscado.CPF);

                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Cliente> BuscaPorTexto(string nome)
        {
            var listaCliente = new List<Cliente>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT CPF, NOME, DATA_NASCIMENTO, 
                                     PONTOS_FIDELIDADE, RUA, NUMERO, 
                                     BAIRRO, CEP, COMPLEMENTO 
                                  FROM CLIENTE WHERE NOME LIKE @TEXTO;";

                    comando.Parameters.AddWithValue("@TEXTO", $"%{nome}%");

                    comando.CommandText = sql;
                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var clienteBuscado = ConverterSqlParaObjeto(leitor);
                        listaCliente.Add(clienteBuscado);
                    }
                }
            }

            return listaCliente;
        }
        public Cliente BuscarPorCpf(long cpf)
        {
            var clienteBuscado = new Cliente();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT CPF, NOME, DATA_NASCIMENTO, 
                                     PONTOS_FIDELIDADE, RUA, NUMERO, 
                                     BAIRRO, CEP, COMPLEMENTO 
                                  FROM CLIENTE WHERE CPF = @CPF_CLIENTE;";

                    comando.Parameters.AddWithValue("@CPF_CLIENTE", cpf);

                    comando.CommandText = sql;
                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        clienteBuscado = ConverterSqlParaObjeto(leitor);
                    }
                }
            }

            return clienteBuscado;
        }

        private Cliente ConverterSqlParaObjeto(SqlDataReader leitor)
        {
            var cliente = new Cliente();
            cliente.CPF = long.Parse(leitor["CPF"].ToString());
            cliente.Nome = leitor["NOME"].ToString();
            cliente.DataNascimento = Convert.ToDateTime(leitor["DATA_NASCIMENTO"].ToString());
            cliente.PontosFidelidade = int.Parse(leitor["PONTOS_FIDELIDADE"].ToString());
            cliente.Endereco = new Endereco
            {
                Rua = leitor["RUA"].ToString(),
                Numero = int.Parse(leitor["NUMERO"].ToString()),
                Bairro = leitor["BAIRRO"].ToString(),
                Cep = leitor["CEP"].ToString(),
                Complemento = leitor["COMPLEMENTO"].ToString(),
            };

            return cliente;
        }

        private void ConverterObjetoParaSql(Cliente cliente, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("@CPF", cliente.CPF);
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.DataNascimento);
            comando.Parameters.AddWithValue("@PONTOS_FIDELIDADE", cliente.PontosFidelidade);
            comando.Parameters.AddWithValue("@RUA", cliente.Endereco.Rua);
            comando.Parameters.AddWithValue("@NUMERO", cliente.Endereco.Numero);
            comando.Parameters.AddWithValue("@BAIRRO", cliente.Endereco.Bairro);
            comando.Parameters.AddWithValue("@CEP", cliente.Endereco.Cep);
            comando.Parameters.AddWithValue("@COMPLEMENTO", cliente.Endereco.Complemento);
        }
    }
}