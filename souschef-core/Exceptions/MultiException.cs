using System.Runtime.Intrinsics.Arm;
using System.Runtime.Serialization;
using System.Text;

namespace souschef_core.Exceptions;

[Serializable]
public sealed class MultiException : Exception
{
    private Exception[] _innerExceptions = [];

    public IEnumerable<Exception> InnerExceptions => _innerExceptions;
    
    public MultiException() : base() { }
    
    public MultiException(string message) : base(message) { }

    public MultiException(string message, Exception innerException) : base(message, innerException)
    {
        _innerExceptions = new[] { innerException };
    }

    public MultiException(ICollection<Exception> innerExceptions) : this("Multiple Exceptions Occured", innerExceptions) { }

    public MultiException(string message, in ICollection<Exception> innerExceptions)
        : base(message, innerExceptions.FirstOrDefault())
    {
        _innerExceptions = innerExceptions.ToArray();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var e in _innerExceptions)
        {
            sb.AppendLine(e.ToString());
        }

        return sb.ToString();
    }
}