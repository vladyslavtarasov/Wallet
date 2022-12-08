namespace BLL;

public class NoDefaultCategoryException : ArgumentException
{
    public string ParamName { get;}

    public NoDefaultCategoryException(string message, string paramName) : base(message)
    {
        ParamName = paramName;
    }
}