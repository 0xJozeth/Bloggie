using System;
namespace Bloggie.Web.Models.Domain
{
	public class BlogPost
	{
		public Guid Id { get; set; }

		public string Heading { get; set; }

        public string PageTitle { get; set; }

        public string Content { get; set; }

        public string ShortDescription { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }

        public bool Visible { get; set; }

        // create an Icollection/navigation property - tells the entity framework core that this blog post can have multiple tags.

        public ICollection<Tag> Tags { get; set; }

    }
}

