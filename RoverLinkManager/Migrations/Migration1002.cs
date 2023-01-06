using System.ComponentModel;
using ServiceStack.Auth;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace RoverLinkManager.Migrations;

/// <summary>
/// See https://docs.servicestack.net/ormlite/apis/schema.html#modify-custom-schema for documentation on migration schema
/// Use the Test Explorer and run MigrationTasks -> Migrate to update to the latest migration
/// </summary>
public class Migration1002 : MigrationBase
{
    public class Link
    {
        [DefaultValue(0)]
        public long Visits { get; set; }
    }

    public override void Up()
    {
        Db.Migrate<Link>();
    }

    public override void Down()
    {
        Db.Revert<Link>();
    }
}
