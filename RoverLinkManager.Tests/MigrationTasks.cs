﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RoverLinkManager.Migrations;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace RoverLinkManager.Tests;
[TestFixture, Explicit, Category(nameof(MigrationTasks))]
public class MigrationTasks
{
    IDbConnectionFactory ResolveDbFactory() => new ConfigureDb().ConfigureAndResolve<IDbConnectionFactory>();
    Migrator CreateMigrator() => new(ResolveDbFactory(), typeof(Migration1000).Assembly);

    [Test]
    public void Migrate()
    {
        var migrator = CreateMigrator();
        var result = migrator.Run();
        Assert.That(result.Succeeded);
    }

    [Test]
    public void Revert_All()
    {
        var migrator = CreateMigrator();
        var result = migrator.Revert(Migrator.All);
        Assert.That(result.Succeeded);
    }

    [Test]
    public void Revert_Last()
    {
        var migrator = CreateMigrator();
        var result = migrator.Revert(Migrator.Last);
        Assert.That(result.Succeeded);
    }

    [Test]
    public void Rerun_Last_Migration()
    {
        Revert_Last();
        Migrate();
    }
}