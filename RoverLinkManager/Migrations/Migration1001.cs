using ServiceStack.Auth;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace RoverLinkManager.Migrations;

/// <summary>
/// See https://docs.servicestack.net/ormlite/apis/schema.html#modify-custom-schema for documentation on migration schema
/// Use the Test Explorer and run MigrationTasks -> Migrate to update to the latest migration
/// </summary>
public class Migration1001 : MigrationBase
{
    public class Link
    {
        public long Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string ShortId { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }

    public override void Up()
    {
        Db.CreateTable<Link>();
    }

    public override void Down()
    {
        Db.DropTable<Link>();
    }
}
