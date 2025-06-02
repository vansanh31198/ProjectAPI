using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Library.Extensions;
public static class LoggerExtensions
{
    //private static readonly ILogger? _logger;

    public static void DebugNew(this ILogger logger, string message)
    {
        logger.LogDebug(message);
    }
}
