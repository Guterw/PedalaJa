using PedalaJa.Models;

public class StationRating
{
    public int Id { get; set; }
    public int StationId { get; set; }  // Referência à estação
    public int Rating { get; set; }  // A nota da avaliação
    public DateTime Date { get; set; }  // Data da avaliação

    public Station Station { get; set; }  // Navegação para a estação
}
