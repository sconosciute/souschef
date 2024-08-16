using System.ComponentModel.DataAnnotations;
using souschef_core.Exceptions;

namespace souschef_core.Services;

public sealed class Validation
{
    
    private readonly List<Exception> _exceptions = [];
    
    public ICollection<Exception> Exceptions => _exceptions;

    public Validation AddException(Exception e)
    {
        _exceptions.Add(e);
        return this;
    }

}

public static class Validate
{
    private const string OneError = "A validation error occured";
    private const string MultiError = "Multiple validation errors occured";
    public static Validation? Begin()
    {
        return null;
    }

    public static Validation? IsNotNull<T>(this Validation? v, T? obj, string paramName) where T : class
    {
        return obj is null
            ? (v ?? new Validation()).AddException(new ArgumentNullException(paramName))
            : v;
    }

    public static Validation? Check(this Validation? v)
    {
        if (v is null)
        {
            return v;
        }

        if (v.Exceptions.Count == 1)
        {
            throw new ValidationException(OneError, v.Exceptions.First());
        }
        else
        {
            throw new ValidationException(MultiError, new MultiException(v.Exceptions));
        }
    }
}