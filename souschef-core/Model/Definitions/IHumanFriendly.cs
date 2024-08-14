namespace souschef_core.Model;

/// <summary>
/// Defines that an object can be structured as a human-readable JSON object.
/// </summary>
public interface IHumanFriendly<T>
{
    /// <summary>
    /// Restructures this object into a Record that can be serialized to human-readable JSON.
    /// </summary>
    /// <returns>Human-readable type for this object.</returns>
    public T ToHumanReadable();
}