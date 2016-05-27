using System.Linq;

namespace NugetNotify.Core.Helpers.Implementations
{
    public class StringHelper : IStringHelper
    {
        public string Clean(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim().ToLower();
        }

        public string CleanExtreme(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : Clean(value).Where(char.IsLetter).ToString();
        }
    }
}