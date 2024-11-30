using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<IActionResult> Index()
        {
            // Verifique se você precisa atualizar os dados
            await _cityBikeService.FetchAndSaveNetworksAsync();

            // Obtenha as redes e as estações do banco de dados
            var networks = await _context.Networks.Include(n => n.Stations).ToListAsync();

            return View(networks); // Passe os dados para a View
        }
        [HttpGet]
        public IActionResult GetStations()
        {
            // Busca todas as estações do banco de dados
            var stations = _context.Stations
                .Select(s => new
                {
                    s.Name,
                    s.Latitude,
                    s.Longitude,
                    s.FreeBikes,
                    s.EmptySlots,
                    Company = s.Network.Company // Incluindo o nome da companhia associada
                })
                .ToList();

            // Retorna as estações em formato JSON
            return Json(stations);
        }

        [HttpGet]
        public JsonResult GetNearestStation(double lat, double lon)
        {
            // Carrega todas as estações na memória (usando ToList para evitar tradução no LINQ)
            var stations = _context.Stations.ToList();

            // Encontra a estação mais próxima com base na distância
            var nearestStation = stations
                .OrderBy(station => GetDistance(lat, lon, station.Latitude, station.Longitude))
                .FirstOrDefault();

            if (nearestStation != null)
            {
                return Json(new[] { nearestStation });
            }

            return Json(null); // Caso não encontre nenhuma estação
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
            dist = dist * 60 * 1.1515; // Conversão para milhas
            dist = dist * 1.609344; // Conversão para km
            return dist;
        }
    }
}

