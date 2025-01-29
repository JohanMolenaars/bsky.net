using Bsky.Net.Model;

namespace Bsky.Net.Model
{
    public class FollowersResponse
	{
		public List<Profile> Followers { get; set; }
		public string Cursor { get; set; }
	}
}