namespace MercadoZe.Classes
{
    public struct Endereco
    {
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }

        public override string ToString()
        {
            return $" - Endereço - \n Rua: {Rua}\n Número: {Numero}\n Bairro: {Bairro}\n Cep: {Cep}\n Complemento: {Complemento}";
        }
    }
}