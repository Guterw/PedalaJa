using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace PedalaJa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CityBikeService _cityBikeService;

        public HomeController(ApplicationDbContext context, CityBikeService cityBikeService)
        {
            _context = context;
            _cityBikeService = cityBikeService;
        }

        // Método para exibir as redes e estações
        public async Task<IActionResult> Index()
        {
            // Verifique se você precisa atualizar os dados
            await _cityBikeService.FetchAndSaveNetworksAsync();

            // Obtenha as redes e as estações do banco de dados
            var networks = await _context.Networks.Include(n => n.Stations).ToListAsync();

            return View(networks); // Passe os dados para a View
        }

        // Método para retornar as estações em formato JSON
        [HttpGet]
        public IActionResult GetStations()
        {
            var stations = _context.Stations
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.Latitude,
                    s.Longitude,
                    s.FreeBikes,
                    s.EmptySlots,
                    Company = s.Network.Company,
                    // Calculando a média das avaliações
                    AverageRating = _context.StationRatings
                                            .Where(r => r.StationId == s.Id)
                                            .Average(r => (double?)r.Rating) ?? 0  // Retorna 0 se não houver avaliações
                })
                .ToList();

            return Json(stations);
        }

        // Método para retornar a estação mais próxima
        [HttpGet]
        public JsonResult GetNearestStation(double lat, double lon)
        {
            var stations = _context.Stations.ToList();
            var nearestStation = stations
                .OrderBy(station => GetDistance(lat, lon, station.Latitude, station.Longitude))
                .FirstOrDefault();

            return Json(nearestStation != null ? new[] { nearestStation } : null);
        }

        // Método para salvar uma nova avaliação
        [HttpPost]
        public async Task<IActionResult> RateStation(int stationId, int rating)
        {
            // Verifica se a estação existe
            var station = await _context.Stations.FindAsync(stationId);
            if (station == null)
            {
                return Json(new { success = false, message = "Estação não encontrada." });
            }

            // Verifica se o rating está dentro do intervalo permitido (1 a 5)
            if (rating < 1 || rating > 5)
            {
                return Json(new { success = false, message = "A avaliação deve ser entre 1 e 5." });
            }

            // Cria um novo registro de avaliação
            var stationRating = new StationRating
            {
                StationId = stationId,
                Rating = rating,
                Date = DateTime.Now
            };

            // Adiciona o registro de avaliação no banco de dados
            _context.StationRatings.Add(stationRating);

            // Calcula a média de avaliações de segurança para a estação
            var securityRating = _context.StationRatings
                                        .Where(r => r.StationId == stationId)
                                        .Average(r => (double?)r.Rating) ?? 0;

            station.SecurityRating = securityRating; // Atualiza a média de segurança na estação

            // Salva as mudanças no banco de dados
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Avaliação salva com sucesso!" });
        }


        // Função para calcular a distância entre duas coordenadas (em km)
        private double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var rlat1 = Math.PI * lat1 / 180;
            var rlat2 = Math.PI * lat2 / 180;
            var theta = lon1 - lon2;
            var rtheta = Math.PI * theta / 180;
            var dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            dist = dist * 1.609344; // Conversão para km
            return dist;
        }
        public IActionResult Suporte()
        {
            return View("Suporte"); // Certifique-se de que o arquivo 'Suporte.cshtml' esteja em Views/Home/
        }
    }
}
