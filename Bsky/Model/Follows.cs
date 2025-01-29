using System.Text.Json.Serialization;

namespace Bsky.Net.Model
{
    public class Following
    {
        [JsonConstructor]
        public Following(
            List<Follow> follows,
            Subject1 subject,
            string cursor
        )
        {
            Follows = follows;
            Subject = subject;
            Cursor = cursor;
        }

        public List<Follow> Follows { get; }

        public Subject1 Subject { get; }

        public string Cursor { get; set; }

        public class Associated
        {
            public Chat chat { get; set; }
        }

        public class Chat
        {
            public string allowIncoming { get; set; }
        }

        public class Follow
        {
            public string did { get; set; }
            public string handle { get; set; }
            public string displayName { get; set; }
            public string avatar { get; set; }
            public Viewer viewer { get; set; }
            public List<object> labels { get; set; }
            public DateTime createdAt { get; set; }
            public DateTime indexedAt { get; set; }
            public string description { get; set; }
            public Associated associated { get; set; }
        }



        public class Subject1
        {
            public string did { get; set; }
            public string handle { get; set; }
            public string displayName { get; set; }
            public string avatar { get; set; }
            public Viewer viewer { get; set; }
            public List<object> labels { get; set; }
            public DateTime createdAt { get; set; }
            public string description { get; set; }
            public DateTime indexedAt { get; set; }
        }

        public class Viewer
        {
            public bool muted { get; set; }
            public bool blockedBy { get; set; }
            public string following { get; set; }
            public string followedBy { get; set; }
        }
    }
}
