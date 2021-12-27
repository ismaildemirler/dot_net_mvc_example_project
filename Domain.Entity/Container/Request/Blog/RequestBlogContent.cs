using System;

namespace Domain.Entity.Container.Request.Blog
{
    public class RequestBlogContent
    {
        public Guid? BlogContentId { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string ShortContent { get; set; }
        public int CategoryId { get; set; }
        public byte[] PostImage { get; set; }
    }
}
