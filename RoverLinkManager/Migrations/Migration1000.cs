using System.ComponentModel;
using RoverLinkManager.Domain.Entities;
using ServiceStack.Auth;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace RoverLinkManager.Migrations;

/// <summary>
/// See https://docs.servicestack.net/ormlite/apis/schema.html#modify-custom-schema for documentation on migration schema
/// Use the Test Explorer and run MigrationTasks -> Migrate to update to the latest migration
/// </summary>

#pragma warning disable CS8618
public class Migration1000 : MigrationBase
{
    public class Building
    {
        [PrimaryKey]
        [AutoIncrement]
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public List<string> GeoCoordinates { get; set; } = new();
        public string PhoneNumber { get; set; } = string.Empty;
        [Reference]
        public Building? ParentBuilding { get; set; }
        [ForeignKey(typeof(Building))]
        public long? ParentId { get; set; }
    }

    public class UserAuth : IUserAuth
    {
        [AutoIncrement]
        public virtual int Id { get; set; }

        [Index]
        public virtual string UserName { get; set; }
        [Index]
        public virtual string Email { get; set; }

        public virtual string PrimaryEmail { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Company { get; set; }
        public virtual DateTime? BirthDate { get; set; }
        public virtual string BirthDateRaw { get; set; }
        public virtual string Address { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Country { get; set; }
        public virtual string Culture { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Gender { get; set; }
        public virtual string Language { get; set; }
        public virtual string MailAddress { get; set; }
        public virtual string Nickname { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual string Salt { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string DigestHa1Hash { get; set; }
        public virtual List<string> Roles { get; set; } = new List<string>();
        public virtual List<string> Permissions { get; set; } = new List<string>();
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual int InvalidLoginAttempts { get; set; }
        public virtual DateTime? LastLoginAttempt { get; set; }
        public virtual DateTime? LockedDate { get; set; }
        public virtual string RecoveryToken { get; set; }

        //Custom Reference Data
        public virtual int? RefId { get; set; }
        public virtual string RefIdStr { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
    }

    public class UserAuthDetails : IUserAuthDetails
    {
        [AutoIncrement]
        public virtual int Id { get; set; }

        public virtual int UserAuthId { get; set; }
        public virtual string Provider { get; set; }
        public virtual string UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string FullName { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Company { get; set; }
        public virtual string Email { get; set; }
        public virtual string PhoneNumber { get; set; }

        public virtual DateTime? BirthDate { get; set; }
        public virtual string BirthDateRaw { get; set; }
        public virtual string Address { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Country { get; set; }
        public virtual string Culture { get; set; }
        public virtual string Gender { get; set; }
        public virtual string Language { get; set; }
        public virtual string MailAddress { get; set; }
        public virtual string Nickname { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string TimeZone { get; set; }

        public virtual string RefreshToken { get; set; }
        public virtual DateTime? RefreshTokenExpiry { get; set; }
        public virtual string RequestToken { get; set; }
        public virtual string RequestTokenSecret { get; set; }
        public virtual Dictionary<string, string> Items { get; set; } = new Dictionary<string, string>();
        public virtual string AccessToken { get; set; }
        public virtual string AccessTokenSecret { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }

        //Custom Reference Data
        public virtual int? RefId { get; set; }
        public virtual string RefIdStr { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
    }

    public class UserAuthRole : IMeta
    {
        [AutoIncrement]
        public virtual int Id { get; set; }

        public virtual int UserAuthId { get; set; }

        public virtual string Role { get; set; }

        public virtual string Permission { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual DateTime ModifiedDate { get; set; }

        //Custom Reference Data
        public virtual int? RefId { get; set; }
        public virtual string RefIdStr { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
    }

    class AppUser : UserAuth
    {
        [Index]
        public string FirebaseUid { get; set; } = string.Empty;
        public bool IsOrganizationMember { get; set; } = false;
        [ForeignKey(typeof(Building))]
        public long? BuildingId { get; set; }
        [Reference]
        public Building Building { get; set; } = new();
        public string Occupation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Default(0)]
        public int FollowersCount { get; set; }
        [Default(0)]
        public int FriendsCount { get; set; }
        public string HomeTown { get; set; } = string.Empty;
        public bool Notifications { get; set; }
        public string? ProfilePhotoUrl { get; set; }
        public string? ProfileCoverUrl { get; set; }
        public string? LastLoginIp { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool Verified { get; set; }
    }

    public class Link
    {
        public long Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string ShortId { get; set; } = string.Empty;
        [Default(0)]
        public long VisitCount { get; set; }
        [Default(OrmLiteVariables.SystemUtc)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }

    public override void Up()
    {
        Db.CreateTable<Building>();
        Db.CreateTable<UserAuthRole>();
        Db.CreateTable<UserAuthDetails>();
        Db.CreateTable<AppUser>();
        Db.CreateTable<Link>();

        Db.Insert<Link>(new Link
        {
            Id = 4203172058923008L,
            ShortId = "4vm31og520811",
            Url = "https://scholarships.eastonsd.org/",
            VisitCount = 0,
            CreatedDate = DateTime.UtcNow
        });
    }

    public override void Down()
    {
        Db.DropTable<Link>();
        Db.DropTable<AppUser>();
        Db.DropTable<UserAuthDetails>();
        Db.DropTable<UserAuthRole>();
        Db.DropTable<Building>();
    }
}
#pragma warning restore CS8618
