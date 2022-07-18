using Newtonsoft.Json;

namespace CounterConsumer;

public class FlyWithUsClient
{
    private readonly HttpClient _httpClient;

    public FlyWithUsClient(Uri baseUri)
    {
        _httpClient = new HttpClient{BaseAddress = baseUri};
    }

    public async Task<Booking> GetBookingByFirstName(string pnr, string firstname)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"booking/{pnr}?firstname={firstname}");
        var response = await _httpClient.SendAsync(request);
        
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<Booking>(content)??throw new Exception("Can not parse json content");
    }

    public partial class Booking
    {
        public string PNR { get; set; }
    }
}