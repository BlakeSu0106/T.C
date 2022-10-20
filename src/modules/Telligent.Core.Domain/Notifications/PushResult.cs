using Newtonsoft.Json;

namespace Telligent.Core.Domain.Notifications
{
    /// <summary>
    /// 派送結果（沿用友盟的派送結果）
    /// </summary>
    public class PushResult
    {
        /// <summary>
        /// 派送結果（SUCCESS = 成功，FAIL = 失敗）
        /// </summary>
        [JsonProperty("ret")]
        public string Message { get; set; }

        /// <summary>
        /// 派送結果說明
        /// </summary>
        [JsonProperty("data")]
        public PushResultMessage ResultData { get; set; }
    }
}
