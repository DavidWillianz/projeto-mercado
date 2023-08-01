using System;

namespace MercadoZe.Classes
{
    public class Cliente
    {
        public long CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int PontosFidelidade { get; set; }
        public Endereco Endereco { get; set; }
     
     public Cliente()
     {
        Endereco = new Endereco();
     }
        public override string ToString()
        {
            return $" - Cliente - \n CPF:{CPF}\n Nome: {Nome}\n Pontos: {PontosFidelidade}\n Data nascimento: {DataNascimento.ToShortDateString()}\n {Endereco}";           
        }
    }
}