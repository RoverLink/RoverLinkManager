using HashidsNet;
using IdGen;
using Microsoft.Extensions.Logging;
using RoverLinkManager.Domain.Entities.Settings;
using RoverLinkManager.Infrastructure.Common.IdGenerator.Models;
using ServiceStack.Configuration;

namespace RoverLinkManager.Infrastructure.Common.IdGenerator.Services;

public class IdGeneratorService : ServiceBase<IdGeneratorService>
{
    private const int MaxGenerationAttempts = 1000;

	private static readonly DateTime Epoch = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	private static readonly IdStructure IdStructure = new IdStructure(40, 8, 15);
    
    // Only a single generator should exist no matter how this service is registered (ideally singleton)
	private static IdGen.IdGenerator? _generatorInstance { get; set; } 
    private static Hashids? _hashidsInstance { get; set; }

    private IdGen.IdGenerator Generator { get; }
    private Hashids Hasher { get; }

    public IdGeneratorService(IAppSettings appSettings, ApplicationSettings settings)
    {
        // Get generator id from app settings
        int generatorId = appSettings.Get<int>("GeneratorId");

        // Make sure we also have a salt configured for HashId
        if (String.IsNullOrEmpty(settings.HashIdSalt))
        {
            LogAndThrowFatalException("HashId salt is missing from secrets configuration");
        }
        
        // Get the generator and hashId hasher
		Generator ??= _generatorInstance = new IdGen.IdGenerator(generatorId, new IdGeneratorOptions(IdStructure, new DefaultTimeSource(Epoch)));
        Hasher ??= _hashidsInstance = new Hashids(settings.HashIdSalt, alphabet: "abcdefghijklmnopqrstuvwxyz1234567890");
    }

    /// <summary>
    /// Creates a new Snowflake Id
    /// </summary>
    /// <returns></returns>
    /// <exception cref="SequenceOverflowException">Thrown when there aren't enough sequence numbers after MaxGenerationAttempts tries.</exception>
    private long CreateSnowflakeId()
    {
        int attempts = 0;
        long id = -1;

        // Snowflake Ids are limited in quantity that can be generated within the same millisecond
        // Generating too many within the same millisecond timestamp will overflow the sequence and fail
        // Therefore, it is necessary to sometimes wait to start the next sequence before continuing to
        // generate Id numbers
        while (attempts < MaxGenerationAttempts)
        {
            try
            {
                id = Generator!.CreateId();

                return id;
            }
            catch (SequenceOverflowException)
            {
                // We've overflowed the sequence, so wait a millisecond and try again
                Task.Delay(1).Wait();
            }

            attempts++;
        }

        throw new SequenceOverflowException();
    }

    /// <summary>
    /// Create a new Snowflake Id
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public SnowflakeId CreateId()
    {
        try
        {
            long id = CreateSnowflakeId();
            string hash = Hasher.EncodeLong(id);

            return new SnowflakeId
            {
                Id = id,
                ShortId = hash
            };
        }
        catch (Exception ex)
        {
            Log.Fatal("Unable to generate snowflake Id", ex);
        }

        throw new Exception("Unable to generate snowflake Id");
    }

    /// <summary>
    /// Attempts to decode a short id and then returns the corresponding Snowflake Id
    /// </summary>
    /// <param name="shortId"></param>
    /// <returns></returns>
    public SnowflakeId? ToSnowflakeId(string shortId)
    {
        long id = -1;

        bool success = Hasher.TryDecodeSingleLong(shortId, out id);

        if (!success)
            return null;

        return new SnowflakeId
        {
            Id = id,
            ShortId = shortId
        };
    }
}
