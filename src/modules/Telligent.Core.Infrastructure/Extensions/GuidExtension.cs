namespace Telligent.Core.Infrastructure.Extensions
{
    public static class GuidExtension
    {
        public static bool IsNullOrEmpty(this Guid guid)
        {
            return guid.Equals(Guid.Empty);
        }
    }
}
