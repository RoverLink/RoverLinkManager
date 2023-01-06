using IdGen;

namespace RoverLinkManager.Infrastructure.Common.IdGenerator.Services;

public class IdGeneratorService
{
	private static readonly DateTime Epoch = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	private static readonly IdStructure IdStructure = new IdStructure(45, 8, 10);
	
	public IdGeneratorService()
    {

    }
}
