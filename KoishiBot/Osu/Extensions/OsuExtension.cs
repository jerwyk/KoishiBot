using Discord;
using KoishiBot.Osu.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishiBot.Osu.Extensions
{
    public static class OsuExtension
    {
        public static async Task<Embed> CreateUserEmbed(this OsuClient client, string username, Color? color = null)
        {
            dynamic user = (await client.GetUser(username))[0];
            color = color ?? Color.Default;

            return new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder()
                { 
                    Name = $"{user.username} (Lv{Math.Round((double)user.level,2)}) in Osu!Standard",
                    Url = $"https://osu.ppy.sh/users/{user.user_id}",
                    IconUrl = $"https://osu.ppy.sh/images/flags/{user.country}.png"
                },
                Description = $"**Rank:** #{user.pp_rank} ({user.country}#{user.pp_country_rank})\r\n" +
                              $"**PP:** {user.pp_raw}\r\n" +
                              $"**Acc:** {Math.Round((double)user.accuracy, 2)}%\r\n" +
                              $"**Total Plays:** {user.playcount}\r\n",
                //ImageUrl = $"attachment://{imageName}",
                Footer = new EmbedFooterBuilder() { Text = $"Joined on {user.join_date}" },
                ThumbnailUrl = $"https://a.ppy.sh/{user.user_id}",           
                Color = color,
            }.Build();

        }
    }
}
