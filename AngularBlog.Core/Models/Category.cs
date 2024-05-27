using System;
using System.Collections.Generic;

namespace AngularBlog.API
{
    public partial class Category
    {
        public Category()
        {
            Articles = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Article> Articles { get; set; }
    }
}
