using System.Reflection;

namespace CleanArchitecture.Persistance;

public static class AssemblyeReferance
{
    public static readonly Assembly assembly = typeof(Assembly).Assembly;
}
