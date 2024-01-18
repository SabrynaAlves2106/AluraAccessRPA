namespace AluraAccessRPA.Infrastructure.Context;

public class AluraDbContext : DbContext
{
    public AluraDbContext(DbContextOptions<AluraDbContext> dbContextOption) : base(dbContextOption)
    {
        //Deleta os bancos de dados caso eles existam
        Database.EnsureDeleted();
        //Cria o banco de dados 
        Database.EnsureCreated();

    }
    public DbSet<CourseInformation> Courses { get; set; }
}
