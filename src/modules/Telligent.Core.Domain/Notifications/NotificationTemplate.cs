using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Core.Domain.Notifications
{
    [Table("sys_notification_template")]
    public class NotificationTemplate : Entity
    {
        /// <summary>
        /// 樣板名稱
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 樣板描述
        /// </summary>
        [Column("description")]
        public string Description { get; set; }

        /// <summary>
        /// 樣板標題
        /// </summary>
        [Column("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// 樣板內容
        /// </summary>
        [Column("content")]
        public string Content { get; set; }
    }
}
