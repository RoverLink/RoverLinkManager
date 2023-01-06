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
}
