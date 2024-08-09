using FastEndpoints;
using souschef_core.Model;

namespace souschef_be.Routes.Open;

public class Messages : Endpoint<Message>
{
    public override void Configure()
    {
        Get("/msg/{@msgId}", msg => new { msg.MsgId });
    }

    public override async Task HandleAsync(Message req, CancellationToken c)
    {
        throw new NotImplementedException();
    }
}