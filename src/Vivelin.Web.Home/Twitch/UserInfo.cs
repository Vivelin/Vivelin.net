using System;

namespace Vivelin.Web.Home.Twitch
{
    public class UserInfo
    {
        public string BroadcasterType { get; set; }
        
        public string Description { get; set; }

        public string DisplayName { get; set; }

        public string Id { get; set; }

        public string Login { get; set; }

        public string OfflineImageUrl { get; set; }

        public string ProfileImageUrl { get; set; }

        public string Type { get; set; }

        public int ViewCount { get; set; }

        public string Email { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
