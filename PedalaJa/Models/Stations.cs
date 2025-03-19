namespace PedalaJa.Models
{
    public class Station
    {
        public int Id { get; set; } // Id para a estação
        public string Name { get; set; }
        public int? FreeBikes { get; set; }
        public int? EmptySlots { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Chave estrangeira para a rede
        public int NetworkId { get; set; }
        public Network Network { get; set; } // Navegação de volta para a rede

        public double SecurityRating { get; set; }  // Média das notas de segurança
    }
}
