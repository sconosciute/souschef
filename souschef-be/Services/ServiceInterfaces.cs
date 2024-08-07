using souschef_core.Model;
using souschef_core.Services;

namespace souschef_be.Services;

public interface IBeMessageSvc : IMessageSvc
{
    Task CommitAsync();
}

public interface IMeasurementSvc
{
    List<Measurement> GetAllMeasures();
}