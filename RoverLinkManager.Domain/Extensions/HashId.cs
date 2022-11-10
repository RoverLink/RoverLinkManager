using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashidsNet;

namespace RoverLinkManager.Domain.Extensions;

// Hash ids are intended to hide database numeric ids from api users - The frontfacing API services should always interact with
// hashId strings with the idea that they will be converted to a long prior to utilization.
public static class HashId
{
    // Never change this salt or id numbers can't properly be decoded
    private static readonly string Salt = "tdkmvg4dyasxy5q75pxy6t";

    // Create static generator
    private static readonly Hashids Generator = new Hashids(Salt, 5, "abcdefghijklmnopqrstuvwxyz1234567890", "cfhistu");

    public static string Encode(int id) => Generator.EncodeLong(id);
    public static string Encode(long id) => Generator.EncodeLong(id);
    public static long DecodeInt(string id) => Generator.DecodeSingle(id);
    public static long DecodeLong(string id) => Generator.DecodeSingleLong(id);
    public static bool TryDecodeInt(string id, out int result) => Generator.TryDecodeSingle(id, out result);
    public static bool TryDecodeLong(string id, out long result) => Generator.TryDecodeSingleLong(id, out result);
}
