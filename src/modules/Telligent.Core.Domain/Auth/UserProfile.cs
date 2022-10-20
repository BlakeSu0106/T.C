namespace Telligent.Core.Domain.Auth
{
    public class UserProfile
    {
        public Guid TenantId { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
