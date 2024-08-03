namespace souschef_core.Exceptions;

/// <summary>
/// A necessary resource URI was missing from the specified location.
/// </summary>
public class MissingUriException(string caller) : Exception(BaseErr + caller)
{
    private const string BaseErr = "Missing required URI in ";
}