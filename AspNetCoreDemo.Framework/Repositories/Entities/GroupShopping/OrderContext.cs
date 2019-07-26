using Microsoft.EntityFrameworkCore;

namespace AspNetCoreDemo.Framework.Repositories.Entities.GroupShopping
{
    public partial class OrderContext : DbContext
    {
        private readonly string _conn;
        public OrderContext()
        {
        }
        public OrderContext(string conn)
        {
            _conn = conn;
        }

        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GroupShoppingFound> GroupShoppingFound { get; set; }
        public virtual DbSet<Log> Log { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable 1030
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
#pragma warning restore 1030
                optionsBuilder.UseMySQL(_conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupShoppingFound>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.ToTable("group_shopping_found", "order");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ActualPrice)
                    .HasColumnName("actual_price")
                    .HasColumnType("decimal(20,3)");

                entity.Property(e => e.DtBegin).HasColumnName("dt_begin");

                entity.Property(e => e.DtEnd).HasColumnName("dt_end");

                entity.Property(e => e.DtInserted)
                    .HasColumnName("dt_inserted")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DtLastUpdated)
                    .HasColumnName("dt_last_updated")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.GroupStatus)
                    .HasColumnName("group_status")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.HeadId)
                    .HasColumnName("head_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.HeadStatus)
                    .HasColumnName("head_status")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.InsertedBy)
                    .IsRequired()
                    .HasColumnName("inserted_by")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("Online");

                entity.Property(e => e.LastUpdatedBy)
                    .IsRequired()
                    .HasColumnName("last_updated_by")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("Online");

                entity.Property(e => e.Sku)
                    .HasColumnName("sku")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("log", "order");

                entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Application)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Callsite)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Exception)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Level)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Logger)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Message).IsUnicode(false);
            });
        }
    }
}
