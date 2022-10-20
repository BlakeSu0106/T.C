using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Serilog;
using Telligent.Core.Domain.Notifications;
using Telligent.Core.Infrastructure.Database;
using Telligent.Core.Infrastructure.Extensions;
using Telligent.Core.Infrastructure.Services;

namespace Telligent.Core.Application.Services.Notifications;

public class EmailPushService : IAppService
{
    private readonly IConfiguration _configuration;
    private readonly BaseDbContext _dbContext;

    public EmailPushService(IConfiguration configuration, BaseDbContext dbContext)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }

    /// <summary>
    /// 派送 email 通知
    /// </summary>
    /// <param name="toName">收件者名稱</param>
    /// <param name="toAddress">收件者郵件信箱</param>
    /// <param name="templateId">樣板識別碼</param>
    /// <param name="args">參數</param>
    /// <returns>push result</returns>
    public async Task<PushResult> PushAsync(
        string toName,
        string toAddress,
        Guid templateId,
        Dictionary<string, string> args)
    {
        var template = await _dbContext.NotificationTemplates
            .FirstAsync(t => t.Id.Equals(templateId) && t.EntityStatus);

        return await PushAsync(toName, toAddress, template.Subject, template.Content.Replace(args));
    }

    /// <summary>
    /// 派送 email 通知
    /// </summary>
    /// <param name="toName">收件者名稱</param>
    /// <param name="toAddress">收件者郵件信箱</param>
    /// <param name="subject">標題</param>
    /// <param name="content">內容</param>
    /// <returns>push result</returns>
    public async Task<PushResult> PushAsync(string toName, string toAddress, string subject,
        string content)
    {
        var section = _configuration.GetSection("SmtpClient");
        var host = section.GetValue<string>("Host");
        var port = section.GetValue<int>("Port");
        var userName = section.GetValue<string>("UserName");
        var password = section.GetValue<string>("Password");
        var from = section.GetValue<string>("From");

        var errorMessage = "";

        try
        {
            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(userName, password);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from, from));
            message.To.Add(new MailboxAddress(toName, toAddress));

            message.Subject = subject;
            message.Body = new BodyBuilder
            {
                HtmlBody = content
            }.ToMessageBody();

            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);

            Log.Information("{toName} {toAddress} {content}",
                toName,
                toAddress,
                content);
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;

            Log.Error(ex,
                "{toName} {toAddress} {content}",
                toName,
                toAddress,
                content);
        }

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
            PushType = PushType.Email,
            PushResult = result.Message,
            ErrorCode = result.ResultData.ErrorCode,
            ErrorMessage = result.ResultData.ErrorMessage,
            Subject = subject,
            Content = content,
            From = from,
            To = toAddress
        });

        await _dbContext.SaveChangesAsync();

        return result;
    }
}