using System;
namespace Bloggie.Web.Models.Domain
{
	public class Tag
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string DisplayName { get; set; }

        // create an Icollection/navigation property - tells the entity framework core that this tag can have multiple blog posts.

        public ICollection<BlogPost> BlogPosts { get; set; }

	}
}

