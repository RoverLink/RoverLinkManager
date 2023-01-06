using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Logging;

namespace RoverLinkManager.Infrastructure;

public class ServiceBase<T>
{
    private ILog? _logger;

    protected ILog Log => _logger ??= LogManager.GetLogger(typeof(T));

    protected void LogAndThrowFatalException(string message)
    {
        Log.Fatal(message);
        throw new Exception(message);
    }
}
