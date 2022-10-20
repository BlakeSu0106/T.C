using Telligent.Core.Infrastructure.Extensions;

namespace Telligent.Core.Infrastructure.Generators
{
    public static class HistoryNoGenerator
    {
        public static string NewHistoryNo()
        {
            return DateTime.UtcNow.ToUtc8DateTime().ToString("yyMMddHHmmssfff");
        }
    }
}
