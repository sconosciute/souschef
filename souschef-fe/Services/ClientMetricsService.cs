using souschef_core.Services;

namespace souschef_fe.Services;

using souschef_core.Model.DTO;

public class ClientMetricsService(HttpClient api) : IMetricSvc
{
    private const string Uri = "http://localhost:5293/usermetrics";
    
    public async Task<UserMetrics> GetMetrics(long userId)
    {
        return await api.GetFromJsonAsync<UserMetrics>($"{Uri}/{userId}");
    }
}