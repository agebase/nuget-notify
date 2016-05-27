namespace NugetNotify.Core.Models
{
    public interface IPackage
    {
        int Id { get; set; }

        string Name { get; set; }
    }
}