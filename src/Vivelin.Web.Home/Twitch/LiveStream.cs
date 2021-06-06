using System;
using System.Collections.Generic;

namespace Vivelin.Web.Home.Twitch
{
    public class LiveStream
    {
        /// <summary>
        /// ID of the game being played on the stream.
        /// </summary>
        public string GameId { get; set; }

        /// <summary>
        /// Name of the game being played.
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        /// Stream ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Stream language. A language value is either the ISO 639-1 two-letter
        /// code for a supported stream language or “other”.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// UTC timestamp.
        /// </summary>
        public DateTimeOffset StartedAt { get; set; }

        /// <summary>
        /// Shows tag IDs that apply to the stream.
        /// </summary>
        public List<string> TagIds { get; set; }

        /// <summary>
        /// Thumbnail URL of the stream. All image URLs have variable width and
        /// height. You can replace {width} and {height} with any values to get
        /// that size image
        /// </summary>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// Stream title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Stream type: "live" or "" (in case of error).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// ID of the user who is streaming.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Login of the user who is streaming.
        /// </summary>
        public string UserLogin { get; set; }

        /// <summary>
        /// Display name corresponding to user_id.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Number of viewers watching the stream at the time of the query.
        /// </summary>
        public int ViewerCount { get; set; }

        public override string ToString()
        {
            return $"{UserName} - {GameName}";
        }
    }
}