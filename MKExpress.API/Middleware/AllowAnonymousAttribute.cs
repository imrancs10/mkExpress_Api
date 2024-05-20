namespace MKExpress.API.Middleware
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AllowAnonymousAttribute : Attribute
    {
    }

}
