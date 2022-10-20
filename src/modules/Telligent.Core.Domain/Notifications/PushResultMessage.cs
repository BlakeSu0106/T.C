using Newtonsoft.Json;

namespace Telligent.Core.Domain.Notifications
{
    public class PushResultMessage
    {
        /// <summary>
        /// 錯誤碼（0000 為系統內部錯誤）
        /// </summary>
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; } = "0000";

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        [JsonProperty("error_msg")]
        public string ErrorMessage { get; set; }
    }
}
