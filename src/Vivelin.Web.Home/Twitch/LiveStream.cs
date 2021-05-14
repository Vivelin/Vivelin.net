using System;
using System.Collections.Generic;

namespace Vivelin.Web.Home.Twitch
{
    public class LiveStream
    {
        public string GameId { get; set; }

        public string GameName { get; set; }

        public string Id { get; set; }

        public string Language { get; set; }

        public DateTimeOffset StartedAt { get; set; }

        public List<string> TagIds { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public int ViewerCount { get; set; }
    }
}
