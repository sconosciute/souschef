using System.Diagnostics.Metrics;
using FastEndpoints;
using souschef_core.Model;
using souschef_core.Services;

namespace souschef_be.Routes.Open;

public class AddMeasurement(ICrudSvc<Measurement> measurSvc) : Endpoint<Measurement>
{
    public override void Configure()
    {
        Post("/measur");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Measurement req, CancellationToken ct)
    {
        await SendAsync(await measurSvc.AddAsync(req), cancellation: ct);
    }
}

public class GetMeasurement(ICrudSvc<Measurement> measurSvc) : Endpoint<Measurement>
{
    public override void Configure()
    {
        Get("/measur/{@meas_id}", measur => new { measur.MeasId });
        AllowAnonymous();
    }

    public override async Task HandleAsync(Measurement req, CancellationToken ct)
    {
        await SendAsync(await measurSvc.GetAsync(req.MeasId), cancellation: ct);
    }
}

public class GetAllMeasurements(ICrudSvc<Measurement> measurSvc) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/measur");
        AllowAnonymous();
    }

    // ask about this
    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync(await measurSvc.GetAllAsync(), cancellation: ct);
    }
}
public class UpdateMeasurement(ICrudSvc<Measurement> measurSvc) : Endpoint<Measurement>
{
    public override void Configure()
    {
        Put("/measur");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Measurement req, CancellationToken ct)
    {
        await SendAsync(await measurSvc.UpdateAsync(req), cancellation: ct);
    }
}
public class DeleteMeasurement(ICrudSvc<Measurement> measurSvc) : Endpoint<Measurement>
{
    public override void Configure()
    {
        Delete("/measur/{@meas_Id}", measur => new {measur.MeasId});
        AllowAnonymous();
    }

    public override async Task HandleAsync(Measurement req, CancellationToken ct)
    {
        if ( await measurSvc.DeleteAsync(req.MeasId) )
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            await SendErrorsAsync(500, ct);
        }
    }
}