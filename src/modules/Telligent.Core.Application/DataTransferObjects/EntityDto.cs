using System.ComponentModel.DataAnnotations;

namespace Telligent.Core.Application.DataTransferObjects
{
    public class EntityDto : BaseEntityDto
    {
        [Key]
        public Guid Id { get; set; }
    }
}
