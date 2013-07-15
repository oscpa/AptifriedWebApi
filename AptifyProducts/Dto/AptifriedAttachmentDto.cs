#region using

using System;

#endregion

namespace AptifyWebApi.Models.Dto
{
    public class AptifriedAttachmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public AptifriedAttachmentCategoryDto Category { get; set; }
        public int EntityId { get; set; }
        public int RecordId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
    }
}