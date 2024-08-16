namespace souschef_core.Services;

using souschef_core.Model.DTO;

public interface IMetricSvc
{
    public Task<UserMetrics> GetMetrics(long userId);
}