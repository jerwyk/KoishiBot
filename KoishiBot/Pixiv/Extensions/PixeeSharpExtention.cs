using Discord;
using PixeeSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishiBot.Pixiv.Extensions
{
    public static class PixeeSharpExtention
    {

        public static Embed CreateEmbed(this PixivIllustration illust, string imageName = "image.jpeg")
        {
            return new EmbedBuilder()
            {
                Title = $"{illust.Title} ({illust.Id})",
                Url = $"https://www.pixiv.net/artworks/{illust.Id}",
                Description = $"Author: {illust.User.Name}",
                ImageUrl = $"attachment://{imageName}",
                Footer = new EmbedFooterBuilder() { Text = "By Koishi Bot" }
            }.WithCurrentTimestamp().Build();
        }

    }
}
