namespace AptifyWebApi.Models
{
    public class AptifriedProductAttachmentWithThumbnail
    {
        public virtual int Id { get; set; }
        public virtual int ProductId { get; set; }
        public virtual int Sequence { get; set; }
        public virtual string ThumbnailName { get; set; }
        public virtual string FileName { get; set; }
    }
}