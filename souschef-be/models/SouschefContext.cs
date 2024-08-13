using Microsoft.EntityFrameworkCore;
using Npgsql;
using souschef_core.Model;

namespace souschef_be.models;

public partial class SouschefContext : DbContext
{
    private readonly string? _pgConn = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__PG");
    private NpgsqlDataSource Source { get; }

    public SouschefContext()
    {
        Source = InitSource();
    }

    public SouschefContext(DbContextOptions<SouschefContext> options)
        : base(options)
    {
        Source = InitSource();
    }

    private NpgsqlDataSource InitSource()
    {
        var sourceBuilder = new NpgsqlDataSourceBuilder(_pgConn);
        sourceBuilder.MapEnum<MeasureType>();
        return sourceBuilder.Build();
    }


    public virtual DbSet<Access> Accesses { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<IngrRecipe> IngrRecipes { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Measurement> Measurements { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(Source);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("measure_type", new[] { "volume", "weight", "unit" })
            .HasPostgresExtension("citext");

        modelBuilder.Entity<Access>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RecipeId }).HasName("access_pkey");

            entity.ToTable("access");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.Comment)
                .HasDefaultValue(true)
                .HasColumnName("comment");
            entity.Property(e => e.Edit)
                .HasDefaultValue(false)
                .HasColumnName("edit");
            entity.Property(e => e.View)
                .HasDefaultValue(true)
                .HasColumnName("view");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Accesses)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("access_recipe_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Accesses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("access_user_id_fkey");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("comments");

            entity.Property(e => e.Comment1).HasColumnName("comment");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Recipe).WithMany()
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("comments_recipe_id_fkey");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("comments_user_id_fkey");
        });

        modelBuilder.Entity<IngrRecipe>(entity =>
        {
            entity.HasKey(e => new { e.RecipeId, e.IngrId }).HasName("ingr_recipe_pkey");

            entity.ToTable("ingr_recipe");

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.IngrId).HasColumnName("ingr_id");
            entity.Property(e => e.Measurement).HasColumnName("measurement");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Section).HasColumnName("section");

            entity.HasOne(d => d.Ingr).WithMany(p => p.IngrRecipes)
                .HasForeignKey(d => d.IngrId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ingr_recipe_ingr_id_fkey");

            entity.HasOne(d => d.MeasurementNavigation).WithMany(p => p.IngrRecipes)
                .HasForeignKey(d => d.Measurement)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ingr_recipe_measurement_fkey");

            entity.HasOne(d => d.Recipe).WithMany(p => p.IngrRecipes)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("ingr_recipe_recipe_id_fkey");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngrId).HasName("ingredients_pkey");

            entity.ToTable("ingredients");

            entity.HasIndex(e => e.Name, "ingredients_name_key").IsUnique();

            entity.Property(e => e.IngrId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ingr_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasColumnType("citext")
                .HasColumnName("name");
            entity.Property(e => e.Tags).HasColumnName("tags");
        });

        modelBuilder.Entity<Measurement>(entity =>
        {
            entity.HasKey(e => e.MeasId).HasName("measurements_pkey");

            entity.ToTable("measurements");

            entity.Property(e => e.MeasId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("meas_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UnitMeasure).HasColumnName("unit_measure");
            entity.Property((e => e.Type)).HasColumnName("type");
        });

        modelBuilder.Entity<Message>(entity => { entity.HasKey(e => e.MsgId); });

        modelBuilder.Entity<Note>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("notes");

            entity.Property(e => e.Note1).HasColumnName("note");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Recipe).WithMany()
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("notes_recipe_id_fkey");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("notes_user_id_fkey");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RecipeId }).HasName("ratings_pkey");

            entity.ToTable("ratings");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.Rating1).HasColumnName("rating");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("ratings_recipe_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("ratings_user_id_fkey");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("recipes_pkey");

            entity.ToTable("recipes");

            entity.Property(e => e.RecipeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("recipe_id");
            entity.Property(e => e.Author).HasColumnName("author");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Directions).HasColumnName("directions");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Public).HasColumnName("public");
            entity.Property(e => e.Tags).HasColumnName("tags");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.Author)
                .HasConstraintName("recipes_author_fkey");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("tags_pkey");

            entity.ToTable("tags");

            entity.Property(e => e.TagId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("tag_id");
            entity.Property(e => e.TagName).HasColumnName("tag_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.DisplayName, "users_display_name_key").IsUnique();

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("user_id");
            entity.Property(e => e.DisplayName).HasColumnName("display_name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Firstname).HasColumnName("firstname");
            entity.Property(e => e.Lastname).HasColumnName("lastname");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName).HasColumnName("role_name");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.UserId }).HasName("user_role_pkey");

            entity.ToTable("user_role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(e => e.User).WithMany(d => d.Roles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_role_fkey");

            entity.HasOne(e => e.Role).WithMany(d => d.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("role_user_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}