using System.ComponentModel.DataAnnotations.Schema;

namespace Telligent.Core.Domain.Notifications
{
    [Table("sys_notification_log")]
    public class NotificationLog
    {
        /// <summary>
        /// 通知紀錄識別碼
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 派送時間
        /// </summary>
        [Column("push_time")]
        public DateTime PushTime { get; set; }

        /// <summary>
        /// 派送類型（Email, SMS）
        /// </summary>
        [Column("push_type")]
        public PushType PushType { get; set; }

        /// <summary>
        /// 通知結果
        /// </summary>
        [Column("push_result")]
        public string PushResult { get; set; }

        /// <summary>
        /// 通知錯誤代碼（比照友盟提供），若為內部系統則為 0000
        /// </summary>
        [Column("error_code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 通知錯誤訊息
        /// </summary>
        [Column("error_message")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        [Column("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [Column("content")]
        public string Content { get; set; }

        /// <summary>
        /// 來源對象
        /// </summary>
        [Column("from_user")]
        public string From { get; set; }

        /// <summary>
        /// 派送對象
        /// </summary>
        [Column("to_user")]
        public string To { get; set; }
    }
}
