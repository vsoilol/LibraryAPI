using System.Reflection;

namespace WebLibrary.BusinessLayer.Exceptions;

public class NotSuchValidatorException : Exception
{
    public NotSuchValidatorException(MemberInfo type) : base($"Not such validator for model {type.Name}")
    {
    }
}