using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace RoverLinkManager.Domain.Enum.Error;

public enum ErrorCode
{
    Unknown = 0,
    BadRequest = 400,
    MalformedId = 401,
    AccessDenied = 502,
    FeedNotFound = 503
}

// This is a later target for improvement
public static class Localizer
{
    private static Dictionary<ErrorCode, string> messages = new()
    {
        { ErrorCode.Unknown, "Unknown error" },
        { ErrorCode.BadRequest, "Unable to make sense of this request" },
        { ErrorCode.MalformedId, "The provided Id is improperly formatted" },
        { ErrorCode.AccessDenied, "You do not have adequate permission to complete this operation." },
        { ErrorCode.FeedNotFound, "The feed specified by the feed slug and feed id does not exist." }
    };

    public static ResponseStatus ToResponseStatus(this ErrorCode code, string culture = "en")
    {
        if (messages.ContainsKey(code))
        {
            return ResponseStatusUtils.CreateResponseStatus(((int)code).ToString(), messages[code]);
        }

        return ResponseStatusUtils.CreateResponseStatus("0", messages[ErrorCode.Unknown]);
    }
}
