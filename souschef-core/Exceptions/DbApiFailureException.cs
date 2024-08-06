namespace souschef_core.Exceptions;

/// <summary>
/// A call to the database API service failed to execute as expected
/// </summary>
public class DbApiFailureException(string detail) : Exception(BaseErr + detail)
{
    private const string BaseErr = "DB API Error:\n";
}