using Newtonsoft.Json;
using System.Collections.Generic;

// Model para a resposta da API que contém a lista de redes
public class CityBikeApiResponse
{
    [JsonProperty("networks")]
    public List<NetworkResponse> Networks { get; set; }
}

// Representa cada rede
public class NetworkResponse
{
    [JsonProperty("company")]
    public List<string> Company { get; set; } // Agora uma lista de strings

    [JsonProperty("href")]
    public string Href { get; set; }

    [JsonProperty("location")]
    public Location Location { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }
}

// Representa a localização da rede
public class Location
{
    [JsonProperty("latitude")]
    public double Latitude { get; set; }

    [JsonProperty("longitude")]
    public double Longitude { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }
}

// Modelo para a resposta da API que contém detalhes de uma rede específica
public class NetworkDetailsResponse
{
    [JsonProperty("network")]
    public NetworkDetail Network { get; set; }
}

// Detalhes da rede, incluindo as estações
public class NetworkDetail
{
    [JsonProperty("company")]
    public List<string> Company { get; set; } // Agora uma lista de strings

    [JsonProperty("href")]
    public string Href { get; set; }

    [JsonProperty("location")]
    public Location Location { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("stations")]
    public List<StationResponse> Stations { get; set; }
}

// Representa cada estação
public class StationResponse
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("timestamp")]
    public string Timestamp { get; set; }

    [JsonProperty("latitude")]
    public double Latitude { get; set; }

    [JsonProperty("longitude")]
    public double Longitude { get; set; }

    [JsonProperty("free_bikes")]
    public int? FreeBikes { get; set; }

    [JsonProperty("empty_slots")]
    public int? EmptySlots { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }
}
