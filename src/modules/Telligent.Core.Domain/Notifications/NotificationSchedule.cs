using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Core.Domain.Notifications
{
    [Table("sys_notification_schedule")]
    public class NotificationSchedule : Entity
    {
        public DateTime PushTime { get; set; }

        public PushType PushType { get; set; }

        public char? IntervalType { get; set; }

        public int? IntervalTimes { get; set; }

        public int? Interval { get; set; }

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
        /// 來源 Client Id
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
