using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace RoverLinkManager.Domain.Entities;
public class Building
{
	[PrimaryKey]
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
	public long? ParentBuildingId { get; set; }
}
