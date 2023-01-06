using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverLinkManager.Infrastructure.Common.IdGenerator.Models;
public class SnowflakeId
{
    public long Id { get; set; }
    public string ShortId { get; set; } = string.Empty;

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        var other = obj as SnowflakeId;

        return this.Id == other.Id && this.ShortId == other.ShortId;
    }
}
