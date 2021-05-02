using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BookLibraryApi.Models;

#nullable disable

namespace BookLibraryApi
{
    public partial class BookLibraryContext : DbContext
    {
        public BookLibraryContext()
        {
        }

        public BookLibraryContext(DbContextOptions<BookLibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<AuthorBookLink> AuthorBookLinks { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BookLibrary;Username=postgres;Password=7753191");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");
            });

            modelBuilder.Entity<AuthorBookLink>(entity =>
            {
                entity.ToTable("AuthorBookLink");

                entity.HasIndex(e => new { e.BookId, e.AuthorId }, "AuthorBookUniq")
                    .IsUnique();

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorBookLinks)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("AuthorBookLink_AuthorId_fkey");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.AuthorBookLinks)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("AuthorBookLink_BookId_fkey");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
