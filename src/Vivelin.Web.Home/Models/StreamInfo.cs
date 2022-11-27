using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vivelin.Web.Home.Twitch;

namespace Vivelin.Web.Home.Models;

public class StreamInfo
{
    public StreamInfo(Twitch.LiveStream stream, Twitch.UserInfo user,
        int thumbnailWidth = 600, int thumbnailHeight = 337)
    {
        if (user.Id != stream.UserId)
            throw new ArgumentException("The user does not match the broadcaster of the stream.");

        Link = $"https://www.twitch.tv/{stream.UserLogin}";
        Title = stream.Title;
        Broadcaster = stream.UserName;
        GameName = stream.GameName;
        ProfileImageUrl = user.ProfileImageUrl;
        ThumbnailUrl = stream.ThumbnailUrl
            .Replace("{width}", thumbnailWidth.ToString())
            .Replace("{height}", thumbnailHeight.ToString());
        ViewerCount = stream.ViewerCount;
        StartedAt = stream.StartedAt;
    }

    public string Link { get; }

    public string Title { get; }

    public string Broadcaster { get; }

    public string GameName { get; }

    public string ProfileImageUrl { get; }

    public string ThumbnailUrl { get; }

    public int ViewerCount { get; }

    public DateTimeOffset StartedAt { get; }

    public TimeSpan Uptime => DateTimeOffset.Now - StartedAt;

    public static List<StreamInfo> BuildList(
        IEnumerable<LiveStream> streams,
        IEnumerable<UserInfo> users)
    {
        var list = new List<StreamInfo>();

        foreach (var stream in streams)
        {
            var user = users.SingleOrDefault(x => x.Id == stream.UserId);
            if (user == null)
                throw new InvalidOperationException($"Could not find user {stream.UserId}.");

            list.Add(new StreamInfo(stream, user));
        }

        return list;
    }
}