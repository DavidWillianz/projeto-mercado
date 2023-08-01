using System;

namespace MercadoZe.Classes
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double PrecoUnitario { get; set; }
        public string Unidade { get; set; }
        public DateTime DataVencimento { get; set; }
        public int QuantidadeEstoque { get; set; }
     
        public override string ToString()
        {
            return $" Id:{Id}\n Nome: {Nome}\n Descrição: {Descricao}\n Preço unitário:{PrecoUnitario}\n"
                  +$" Unidade: {Unidade}\n Data Vencimento: {DataVencimento.ToShortDateString()}\n Quantidade no estoque: {QuantidadeEstoque}";
        }

        public void EntradaEstoque(int quantidade)
        {
            QuantidadeEstoque += quantidade;
        }

        public void SaidaEstoque(int quantidade)
        {
            QuantidadeEstoque -= quantidade;
        }
    }
}