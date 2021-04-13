using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishiBot.Models
{
    public class TestModel : ModuleBase<SocketCommandContext>
    {
        [Command("test")]
        public Task TestAsync()
        {
            return ReplyAsync("test");
        }
    }
}