using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoverLinkManager.Domain.Entities.Settings;
using Stream;

namespace RoverLinkManager.Infrastructure.GetStream.Services;

public class StreamConnectionFactory
{
    private readonly ApplicationSettings _settings;

    public StreamConnectionFactory(ApplicationSettings settings)
    {
        _settings = settings;
    }

    public StreamClient Open()
    {
        return new StreamClient(_settings.Stream.StreamKey, _settings.Stream.StreamSecret, StreamClientOptions.Default);
    }
}
