using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishiBot
{
    class Program
    {
		private DiscordSocketClient _client;
		private IServiceProvider _services;
		private CommandService _commands;
		static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
		public async Task MainAsync()
		{
			_client = new DiscordSocketClient();
			_commands = new CommandService();

			_client.Log += Log;

			// Remember to keep token private or to read it from an 
			// external source! In this case, we are reading the token 
			// from an environment variable. If you do not know how to set-up
			// environment variables, you may find more information on the 
			// Internet or by using other methods such as reading from 
			// a configuration.
			await _client.LoginAsync(TokenType.Bot,
				Environment.GetEnvironmentVariable("koishiBotToken", EnvironmentVariableTarget.User));
			await _client.StartAsync();


			_services = new ServiceCollection()
				.AddSingleton(_client)
				.AddSingleton(_commands)
				.AddSingleton<InteractiveService>()
				.BuildServiceProvider();

			await new CommandHandler(_client, _commands, _services).InstallCommandsAsync();

			// Block this task until the program is closed.
			await Task.Delay(-1);
		}

		private Task Log(LogMessage message)
		{
			Console.WriteLine(message.ToString());
			return Task.CompletedTask;
		}

	}
}
