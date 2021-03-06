using System;
using System.Collections.Generic;

#nullable disable

namespace BookLibraryApi.Models
{
    public partial class Book
    {
        public Book()
        {
            AuthorBookLinks = new HashSet<AuthorBookLink>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AuthorBookLink> AuthorBookLinks { get; set; }
    }
}
