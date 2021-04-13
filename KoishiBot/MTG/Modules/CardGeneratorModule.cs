using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixeeSharp;
using System.IO;
using Discord;
using KoishiBot.MTG.Services;
using Discord.Addons.Interactive;

namespace KoishiBot.MTG.Modules
{
    [Group("mtg")]
    public class PixivGeneralModule : InteractiveBase
    {
        [Command("tts")]
        public async Task CreateCardAsync(string name)
        {
            try
            {
                var client = new ScryfallClient();

                var card = await client.NameCardSimple(name);

                var tts = new ttsClient();

                string json = tts.CreateSimpleCard(card);

                string path = @"\" + card.Name + ".json";
                System.IO.File.WriteAllText(path, json);

                var embed = new EmbedBuilder()
                {
                    Author = new EmbedAuthorBuilder()
                    {
                        Name = card.Name,
                        Url = card.CardUrl,
                    },
                    ImageUrl = card.ImageUrl,
                    Footer = new EmbedFooterBuilder() { Text = "By Koishi Bot" }
                }.WithCurrentTimestamp().Build();

                await ReplyAsync(embed: embed);
                await Context.Channel.SendFileAsync(path);

                File.Delete(path);

            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }

        [Command]
        public async Task NameCardAsync(string name)
        {
            try
            {
                var client = new ScryfallClient();

                var card = await client.NameCard(name);

                var embed = new EmbedBuilder()
                {
                    Author = new EmbedAuthorBuilder()
                    {
                        Name = Convert.ToString(card.name),
                        Url = Convert.ToString(card.scryfall_uri),
                    },
                    ImageUrl = Convert.ToString(card.image_uris.normal),
                    Footer = new EmbedFooterBuilder() { Text = "By Koishi Bot" }
                }.WithCurrentTimestamp().Build();

                await ReplyAsync(embed: embed);
            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }

        [Command("search")]
        public async Task SearchCardAsync(string query)
        {
            try
            {
                var client = new ScryfallClient();

                dynamic result;

                string resultList = "";
                int queryPage = 1;
                List<string> resultPages = new List<string>();
                bool multiPage = false;
                //create search result
                do
                {
                    result = await client.SearchCard(query, queryPage++);

                    foreach (dynamic card in result.data)
                    {
                        if (resultList.Length < 1800)
                        {
                            if (card.ContainsKey("printed_name"))
                            {
                                resultList += $"[{Convert.ToString(card.printed_name)}]({Convert.ToString(card.scryfall_uri)})\n";
                            }
                            else
                            {
                                resultList += $"[{Convert.ToString(card.name)}]({Convert.ToString(card.scryfall_uri)})\n";
                            }
                        }
                        else
                        {
                            multiPage = true;
                            resultPages.Add(resultList);
                            resultList = "";
                        }
                    }
                } while (Convert.ToBoolean(result.has_more));

                if (!multiPage)
                {
                    var embed = new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            Name = $"Search results for \"{query}\"",
                        },
                        Description = resultList,
                        Footer = new EmbedFooterBuilder() { Text = "By Koishi Bot" }
                    }.WithCurrentTimestamp().Build();

                    await ReplyAsync(embed: embed);
                }
                else
                {
                    resultPages.Add(resultList);
                    var pageMsg = new PaginatedMessage();
                    pageMsg.Author = new EmbedAuthorBuilder()
                    {
                        Name = $"Search results for \"{query}\"",
                    };
                    pageMsg.Pages = resultPages;
                    pageMsg.Options = new PaginatedAppearanceOptions()
                    {
                        DisplayInformationIcon = false,
                        JumpDisplayOptions = resultPages.Count > 5 ? JumpDisplayOptions.WithManageMessages : JumpDisplayOptions.Never,
                    };
                    await PagedReplyAsync(pageMsg);
                }
            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }

    }
}
