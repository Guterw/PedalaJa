namespace PedalaJa.Models
{

    public class Network
    {
        public int Id { get; set; } // Certifique-se de que a classe Network tenha um Id
        public string Name { get; set; }

        // Alterado para uma string que armazena as companhias como uma lista separada por vírgulas
        public string Company { get; set; }

        public string City { get; set; }
        public string Country { get; set; }

        // Propriedade de navegação para estações
        public ICollection<Station> Stations { get; set; } = new List<Station>();
    }
}
