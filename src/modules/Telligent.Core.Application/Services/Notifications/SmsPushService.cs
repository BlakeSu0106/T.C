using System.Xml.Serialization;
using Serilog;
using Telligent.Core.Domain.Notifications;
using Telligent.Core.Infrastructure.Database;
using Telligent.Core.Infrastructure.Extensions;
using Telligent.Core.Infrastructure.Services;

namespace Telligent.Core.Application.Services.Notifications;

public class SmsPushService : IAppService
{
    private const int Retry = 3;
    private readonly BaseDbContext _dbContext;

    public SmsPushService(BaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PushResult> PushAsync(string groupId, string mobile, string message)
    {
        using var client = new SMS_APIClient();

        var retry = 0;
        var errorMessage = "";

        do
        {
            try
            {
                var xml = await client.Send_SMSForCodeAsync(groupId, mobile, message);

                using var reader = new StringReader(xml);

                var serializer = new XmlSerializer(typeof(SmsPushResult));

                if (serializer.Deserialize(reader) is SmsPushResult smsPushResult && smsPushResult.Result.ToLower().Equals("success"))
                {
                    break;
                }
            }
            catch (Exception ex)
            {
                errorMessage += $"retry times: {retry}, {ex.Message} {Environment.NewLine}";

                Log.Error(ex, 
                    "{groupId} {mobile} {message}",
                    groupId,
                    mobile,
                    message);
            }
            finally
            {
                retry += 1;
            }
        } while (retry < Retry);

        var result = new PushResult
        {
            Message = string.IsNullOrEmpty(errorMessage) ? "SUCCESS" : "FAIL",
            ResultData = new PushResultMessage
            {
                ErrorCode = string.IsNullOrEmpty(errorMessage) ? "" : "0000",
                ErrorMessage = errorMessage
            }
        };

        await _dbContext.NotificationLogs.AddAsync(new NotificationLog
        {
            PushTime = DateTime.UtcNow.ToUtc8DateTime(),
            PushType = PushType.Sms,
            PushResult = result.Message,
            ErrorCode = result.ResultData.ErrorCode,
            ErrorMessage = result.ResultData.ErrorMessage,
            Subject = "",
            Content = message,
            From = "",
            To = mobile
        });

        await _dbContext.SaveChangesAsync();

        return result;
    }
}