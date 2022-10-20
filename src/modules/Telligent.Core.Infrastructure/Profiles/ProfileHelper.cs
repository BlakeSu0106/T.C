using System.IdentityModel.Tokens.Jwt;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Telligent.Core.Domain.Auth;

namespace Telligent.Core.Infrastructure.Profiles;

public static class ProfileHelper
{
    /// <summary>
    /// 取得 payload 裡的 user 資訊
    /// </summary>
    /// <param name="context"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static UserProfile GetProfile(HttpContext context, out string token)
    {
        token = "";

        if (context == null) return null;

        var authorization = context.Request.Headers["Authorization"].ToString();
        if (!authorization.Contains("Bearer")) return null;

        token = authorization.Split(' ')[1];

        var securityToken = new JwtSecurityToken(token);
        var payload = new JwtPayload(securityToken.Claims);

        return new UserProfile
        {
            TenantId = Guid.Parse(payload["tenant"].ToString() ?? string.Empty),
            UserId = Guid.Parse(payload["id"].ToString() ?? string.Empty),
            Name = payload[JwtClaimTypes.Name].ToString(),
            Email = payload[JwtClaimTypes.Email].ToString()
        };
    }
}