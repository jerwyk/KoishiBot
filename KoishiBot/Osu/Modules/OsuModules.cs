using Discord.Commands;
using KoishiBot.Osu.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoishiBot.Osu.Extensions;
using Discord;

namespace KoishiBot.Osu.Models
{
    public class OsuModules : ModuleBase<SocketCommandContext>
    {
        [Command("osu")]
        public async Task OsuAsync(string username = "jerwyk")
        {
            try
            {
                var osuClient = new OsuClient();
                var embed = await osuClient.CreateUserEmbed(username, Color.Green);
                await ReplyAsync(embed: embed);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
