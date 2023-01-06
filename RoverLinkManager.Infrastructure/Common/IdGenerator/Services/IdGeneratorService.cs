using IdGen;
using ServiceStack.Configuration;

namespace RoverLinkManager.Infrastructure.Common.IdGenerator.Services;

public class IdGeneratorService
{
	private static readonly DateTime Epoch = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	private static readonly IdStructure IdStructure = new IdStructure(40, 8, 15);
	
	private IdGen.IdGenerator Generator { get; set; } 

	public IdGeneratorService(IAppSettings appSettings)
    {
		// TODO: Get machine id from appSettings
		Generator = new IdGen.IdGenerator(0, new IdGeneratorOptions(IdStructure, new DefaultTimeSource(Epoch)));
    }
}
