using Bsky.Net.Model;

namespace Bsky.Net.Helper.AtProto.Commands.Server
{
    public record RefreshToken(string AccessJwt, string RefreshJwt, string Handle, Did Did) { }
}
