using Bsky.Net.Model;
using System.Reflection.Emit;

namespace Bsky.Net.Model
{
    public record Profile(string Did,
       string Handle,
       string DisplayName,
       string Description,
       string Avatar,
       string Banner,
       int FollowsCount,
       int FollowersCount,
       int PostsCount,
       DateTime IndexedAt,
       Viewer Viewer,
       IReadOnlyList<Label> Labels
   );
}