using NugetNotify.Core.Models;

namespace NugetNotify.Core.Services
{
    public interface IPackageService
    {
        bool Exists(string name);

        IPackage Create(string name);

        IPackage Get(string name);
    }
}