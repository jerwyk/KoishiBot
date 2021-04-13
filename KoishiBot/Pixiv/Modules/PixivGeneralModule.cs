using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixeeSharp;
using System.IO;
using Discord;
using KoishiBot.Pixiv.Extensions;

namespace KoishiBot.Pixiv.Models
{
    [Group("pixiv")]
    public class PixivGeneralModule : ModuleBase<SocketCommandContext>
    {
        [Command("search")]
        public async Task SearchPixivAsync(string search)
        {
            try
            {
                PixeeSharpAppApi client = new PixeeSharpAppApi();
                await client.Login("jerwyk@126.com", "Jerwyk0526");
                Random rnd = new Random();
                var illusts = await client.SearchIllustration(search, offset: rnd.Next(300));
                var illust = illusts.Result[rnd.Next(illusts.Result.Count)];
                Stream imgStream = await illust.GetImage(PixeeSharp.Enums.ImageSize.Large);

                await Context.Channel.SendFileAsync(imgStream, "image.jpeg", embed: illust.CreateEmbed());
            }
            catch(Exception ex)
            {

            }
        }
    }
}
