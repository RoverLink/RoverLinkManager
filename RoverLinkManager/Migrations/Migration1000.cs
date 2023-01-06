using RoverLinkManager.Domain.Entities;
using ServiceStack.Auth;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace RoverLinkManager.Migrations;

/// <summary>
/// See https://docs.servicestack.net/ormlite/apis/schema.html#modify-custom-schema for documentation on migration schema
/// Use the Test Explorer and run MigrationTasks -> Migrate to update to the latest migration
/// </summary>
public class Migration1000 : MigrationBase
{
    /*
    public class MyTable
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    */
    class AppUser 
    {
	    [Default(0)] 
	    public int Followers { get; set; }
    }

    public override void Up()
    {
        //Db.CreateTable<MyTable>();
        Db.Migrate<AppUser>();
    }

    public override void Down()
    {
        //Db.DropTable<MyTable>();
        Db.Revert<AppUser>();
    }
}
