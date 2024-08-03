using souschef_be.models;

namespace souschef_be.Services;

internal interface IMessageSvc
{
    Message? GetMessage(int id);
    List<Message> GetAllMessages();
    Message SendMessage(Message msg);
    bool DeleteMessage(int id);
    void Commit();
}

internal interface IMeasurementSvc
{
    List<Measurement> GetAllMeasures();
}