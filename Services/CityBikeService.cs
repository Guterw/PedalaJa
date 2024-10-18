using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using PedalaJa.Models;
using Microsoft.EntityFrameworkCore;

public class CityBikeService
{
    private readonly HttpClient _httpClient;
    private readonly ApplicationDbContext _context;

    public CityBikeService(HttpClient httpClient, ApplicationDbContext context)
    {
        _httpClient = httpClient;
        _context = context;
    }

    public async Task FetchAndSaveNetworksAsync()
    {
        var response = await _httpClient.GetAsync("http://api.citybik.es/v2/networks");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<CityBikeApiResponse>(content);


        foreach (var network in data.Networks)
        {
            var existingNetwork = await _context.Networks
                    .FirstOrDefaultAsync(n => n.Name == network.Name);

                // Verifica se a cidade da network é Sorocaba
                if (network.Location.City?.ToLower() == "sorocaba" && existingNetwork == null)
            {
                var newNetwork = new Network
                {
                    Name = network.Name,
                    Company = string.Join(", ", network.Company),
                    City = network.Location.City,
                    Country = network.Location.Country
                };

                _context.Networks.Add(newNetwork);
                await _context.SaveChangesAsync();

                // Usar o endpoint específico para buscar as estações de cada rede
                await FetchAndSaveStationsAsync(network.Id, newNetwork.Id);
            }
        }
    }

    private async Task FetchAndSaveStationsAsync(string networkId, int newNetworkId)
    {
        var response = await _httpClient.GetAsync($"http://api.citybik.es/v2/networks/{networkId}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var networkDetails = JsonConvert.DeserializeObject<NetworkDetailsResponse>(content);

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {

            try
            {
                foreach (var station in networkDetails.Network.Stations)
                {
                    var existingStation = await _context.Stations
                        .FirstOrDefaultAsync(s => s.Name == station.Name && s.NetworkId == newNetworkId);

                        // Verifica se a cidade da estação é Sorocaba
                        if (networkDetails.Network.Location.City?.ToLower() == "sorocaba" && !string.IsNullOrWhiteSpace(station.Name) && existingStation == null)
                        
                    {
                        var newStation = new Station
                        {
                            Name = station.Name,
                            FreeBikes = station?.FreeBikes,
                            EmptySlots = station?.EmptySlots,
                            Latitude = station.Latitude,
                            Longitude = station.Longitude,
                            NetworkId = newNetworkId
                        };

                        _context.Stations.Add(newStation);
                    }
                    else
                    {
                        // Opcional: logar ou tratar o caso em que a estação não possui um nome
                        Console.WriteLine($"Estação sem nome detectada para a rede {networkId}. Ignorando.");
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public async Task<List<Network>> GetAllNetworksAndStationsAsync()
    {
        return await _context.Networks
            .Include(n => n.Stations)
            .ToListAsync();
    }

}
