using System;
using System.Globalization;

namespace MercadoZe.Classes
{
    public class Pedido
    {
        public int Numero {get; set;}

        public int Quantidade {get; set;}

        public double ValorTotal {get; set;}

        public int ProdutoId {get; set;}

        public int CpfCliente {get; set;}

        public double Pontuacao {get; set;}

        public DateTime DataHora {get; set;}

        public Produto Produto {get; set;}

        public Cliente Cliente {get; set;}

        public Pedido()
        {
            this.Produto = new Produto();
            this.Cliente = new Cliente();
            this.DataHora = DateTime.Now;
        }

        public static double PontosEmDobro()
        {
            Pedido novoPedido = new Pedido();
            return novoPedido.Pontuacao = novoPedido.ValorTotal*2;
        }
    }
}