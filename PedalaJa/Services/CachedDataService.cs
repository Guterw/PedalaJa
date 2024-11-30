using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PedalaJa.Models;

public class CachedDataService
{
    private readonly CityBikeService _cityBikeService;
    private DateTime _lastFetchTime;
    private List<Network> _cachedNetworks;

    public CachedDataService(CityBikeService cityBikeService)
    {
        _cityBikeService = cityBikeService;
        _lastFetchTime = DateTime.MinValue;
    }

    public async Task<List<Network>> GetNetworksAsync()
    {
        // Verifica se a última requisição foi feita há mais de 1 hora
        if (_cachedNetworks == null || DateTime.UtcNow - _lastFetchTime > TimeSpan.FromHours(1))
        {
            await _cityBikeService.FetchAndSaveNetworksAsync(); // Não armazena no _cachedNetworks
            _cachedNetworks = await _cityBikeService.GetAllNetworksAndStationsAsync(); // Método que retorna todas as redes salvas no banco
            _lastFetchTime = DateTime.UtcNow;
        }

        return _cachedNetworks;
    }

}
