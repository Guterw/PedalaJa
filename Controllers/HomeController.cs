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
    }
}
