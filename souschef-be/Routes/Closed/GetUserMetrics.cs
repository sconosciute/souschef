using FastEndpoints;
using souschef_be.Services;
using souschef_core.Model;
using souschef_core.Model.DTO;
using souschef_core.Services;

namespace souschef_be.Routes.Closed;

public class GetUserMetrics(IMetricSvc metricsSvc) : Endpoint<UserMetrics>
{
    public override void Configure()
    {
        Get("/usermetrics/{@user_id}", userId => new{userId.UserId});
        AllowAnonymous();
    }

    public override async Task HandleAsync(UserMetrics req, CancellationToken ct)
    {
        await SendAsync(await metricsSvc.GetMetrics(req.UserId), cancellation: ct);
    }
}