using System.Reflection;

namespace Ez.UrlShortener.Domain
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
