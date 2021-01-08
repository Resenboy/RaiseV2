using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Raise.Model.Models;

namespace Raise.MobileAppService.Models
{
    public partial class RaisedbContext : DbContext
    {
        public static IConfiguration Configuration;

        public RaisedbContext()
        {
        }

        public RaisedbContext(DbContextOptions<RaisedbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Feed> Feed { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Configuration.GetConnectionString("RaiseDbConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountIdenti);

                entity.ToTable("TCO_ACCOUN", "core");

                entity.HasIndex(e => e.AccountIdenti)
                    .HasName("ACC_IDENTI_UK")
                    .IsUnique();

                entity.Property(e => e.AccountIdenti)
                    .HasColumnName("ACC_IDENTI")
                    .HasDefaultValueSql("nextval('core.\"TCO_ACCOUN_ACC_IDENTI_SEQ\"'::regclass)");

                entity.Property(e => e.Birthday).HasColumnName("ACC_BTHDAY");

                entity.Property(e => e.City)
                    .HasColumnName("ACC_CITY")
                    .HasColumnType("character varying");

                entity.Property(e => e.CreateDate).HasColumnName("ACC_CREDAT");

                entity.Property(e => e.GamerTag).HasColumnName("ACC_GAMER");

                entity.Property(e => e.GamerTag)
                    .HasColumnName("ACC_GAMTAG")
                    .HasColumnType("character varying");

                entity.Property(e => e.UrlProfileImage)
                    .IsRequired()
                    .HasColumnName("ACC_IMAGE")
                    .HasColumnType("character varying");

                entity.Property(e => e.StartingDate).HasColumnName("ACC_INIDAT");

                entity.Property(e => e.State)
                    .HasColumnName("ACC_STATE")
                    .HasColumnType("character varying");

                entity.Property(e => e.UpdateDate).HasColumnName("ACC_UPDDAT");

                entity.Property(e => e.User.UserIdenti).HasColumnName("ACC_USR_IDENTI");

                entity.HasOne(d => d.User)
                    .WithOne()
                    .HasForeignKey<User>(d => d.UserIdenti)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ACC_USR_FK");
            });

            modelBuilder.Entity<Feed>(entity =>
            {
                entity.HasKey(e => e.FeedIdenti);

                entity.ToTable("TCO_FOLLOW", "core");

                entity.HasIndex(e => e.FeedIdenti)
                    .HasName("FLW_IDENTI_UK")
                    .IsUnique();

                entity.Property(e => e.FeedIdenti)
                    .HasColumnName("FLW_IDENTI")
                    .HasDefaultValueSql("nextval('core.\"TCO_FOLLOW_FLW_IDENTI_SEQ\"'::regclass)");

                entity.Property(e => e.CreateDate).HasColumnName("FLW_CREDAT");

                entity.Property(e => e.User.UserIdenti).HasColumnName("FLW_USR_IDENTI");

                entity.Property(e => e.FollowerUserPost.UserIdenti).HasColumnName("FLW_USR_USR_IDENTI");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feed)
                    .HasForeignKey(d => d.FollowerUserPost.UserIdenti)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FLW_USR_USR_FK");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.PostIdenti);

                entity.ToTable("TCO_POST", "core");

                entity.HasIndex(e => e.PostIdenti)
                    .HasName("POS_IDENTI_UK")
                    .IsUnique();

                entity.Property(e => e.PostIdenti)
                    .HasColumnName("POS_IDENTI")
                    .HasDefaultValueSql("nextval('core.\"TCO_POST_POS_IDENTI_SEQ\"'::regclass)");

                entity.Property(e => e.CreateDate).HasColumnName("POS_CREDAT");

                entity.Property(e => e.Date).HasColumnName("POS_DATE");

                entity.Property(e => e.Description)
                    .HasColumnName("POS_DESCRI")
                    .HasColumnType("character varying");

                entity.Property(e => e.UrlPostImage)
                    .HasColumnName("POS_IMAGE")
                    .HasColumnType("character varying");

                entity.Property(e => e.UpdateDate).HasColumnName("POS_UPDDAT");

                entity.Property(e => e.User.UserIdenti).HasColumnName("POS_USR_IDENTI");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.User.UserIdenti)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("POS_USR_FK");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserIdenti);

                entity.ToTable("TCO_USER", "core");

                entity.HasIndex(e => e.UserIdenti)
                    .HasName("USR_IDENTI_UK")
                    .IsUnique();

                entity.Property(e => e.UserIdenti)
                    .HasColumnName("USR_IDENTI")
                    .HasDefaultValueSql("nextval('core.\"TCO_USER_USR_IDENTI_SEQ\"'::regclass)");

                entity.Property(e => e.AcceptedTerms).HasColumnName("USR_ACPTER");

                entity.Property(e => e.CreateDate).HasColumnName("USR_CREDAT");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("USR_EMAIL")
                    .HasColumnType("character varying(65)");

                entity.Property(e => e.IsFacebookUser).HasColumnName("USR_FCBUSR");

                entity.Property(e => e.GuidKey).HasColumnName("USR_GUID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("USR_NAME")
                    .HasColumnType("character varying(65)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("USR_PASWRD")
                    .HasColumnType("character(65)");

                entity.Property(e => e.RememberMe).HasColumnName("USR_REMEMB");

                entity.Property(e => e.UpdateDate).HasColumnName("USR_UPDDAT");
            });

            modelBuilder.HasSequence("TCO_ACCOUN_ACC_IDENTI_SEQ");

            modelBuilder.HasSequence("TCO_FOLLOW_FLW_IDENTI_SEQ");

            modelBuilder.HasSequence("TCO_POST_POS_IDENTI_SEQ");

            modelBuilder.HasSequence("TCO_USER_USR_IDENTI_SEQ");
        }
    }
}
