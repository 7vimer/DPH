using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using System.Threading;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace DPH
{
    class Program
    {
        static string token = "5334748900:AAG87z7-tp0jPGJs3MM1H9avS315Au5AOLU";
        static ITelegramBotClient bot = new TelegramBotClient(token);

        static Random rnd = new Random();
        static List<action> allAnsw = new List<action>();
        static InlineKeyboardMarkup keyboard;
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == UpdateType.InlineQuery)
            {
                var message = update.InlineQuery;
                if (message.Query != null)
                {
	                message.Query = message.Query.ToLower();
                    await botClient.AnswerInlineQueryAsync(message.Id, search(message.Query),0);
                    return;
                }
                else
                {
                    return;   
                }
            }
        }

        class action
        {
	        public string id, title, answ, desc;
        }

        static List<InlineQueryResult> search(string query)
        {
            var toReturn = new List<InlineQueryResult>();
            foreach (var x in allAnsw)
            {
	            if (x.title.Contains(query) && query != "")
	            {
		            int randInt = rnd.Next(1, 101);
		            if (x.id == "1")
		            {
			            InlineQueryResultArticle article = new InlineQueryResultArticle(x.id, x.title,
				            new InputTextMessageContent(x.answ + randInt + " 🎱"));
			            article.Description = x.desc;
			            article.ReplyMarkup = keyboard;
			            toReturn.Add(article);
		            }
		            else if (x.id == "2")
		            {
			            if (randInt < 50)
			            {
				            InlineQueryResultArticle article = new InlineQueryResultArticle(x.id, x.title,
					            new InputTextMessageContent("Выпала Решка 🪙"));
				            article.Description = x.desc;
				            article.ReplyMarkup = keyboard;
				            toReturn.Add(article);
			            }
			            else
			            {
				            InlineQueryResultArticle article = new InlineQueryResultArticle(x.id, x.title,
					            new InputTextMessageContent("Выпал Орел 🦅"));
				            article.Description = x.desc;
				            article.ReplyMarkup = keyboard;
				            toReturn.Add(article);
			            }
		            }
		            else if (x.id == "3")
		            {
			            InlineQueryResultArticle article = new InlineQueryResultArticle(x.id, x.title,
				            new InputTextMessageContent("Укажите игроков через пробел"));
			            article.Description = x.desc;
			            article.ReplyMarkup = keyboard;
			            toReturn.Add(article);
					}
	            }
				else if (query.Contains("/cl"))
	            {
		            var players = query.Split(' ').ToList();
		            players.Remove(players[0]);
		            if (players.Count % 2 == 0)
		            {
			            var countP = players.Count / 2;
			            var team1 = new string[countP];
			            var team2 = new string[countP];
			            var stringt1 = "";
			            var stringt2 = "";
			            for (int i = 0; i < countP; i++)
			            {
				            team1[i] = players[rnd.Next(0, players.Count - 1)];
				            players.Remove(team1[i]);
				            stringt1 += $"\n{team1[i]}";
			            }

			            for (int i = 0; i < countP; i++)
			            {
				            team2[i] = players[rnd.Next(0, players.Count - 1)];
				            players.Remove(team2[i]);
				            stringt2 += $"\n{team2[i]}";
			            }

			            InlineQueryResultArticle article = new InlineQueryResultArticle(x.id, x.title,
				            new InputTextMessageContent("1️⃣ Команда 1:" + stringt1 + "\n2️⃣ Команда 2:" + stringt2));
			            article.Description = x.desc;
			            article.ReplyMarkup = keyboard;
			            toReturn.Add(article);
		            }
		            else
		            {
			            InlineQueryResultArticle article = new InlineQueryResultArticle(x.id, x.title,
				            new InputTextMessageContent("Укажите четное кол-во игроков"));
			            article.Description = x.desc;
			            article.ReplyMarkup = keyboard;
			            toReturn.Add(article);
					}
	            }
	            else if (query == "")
	            {
		            int randInt = rnd.Next(1, 101);
		            if (x.id == "1")
		            {
			            InlineQueryResultArticle article = new InlineQueryResultArticle(x.id, x.title,
				            new InputTextMessageContent(x.answ + randInt + " 🎱"));
			            article.Description = x.desc;
			            article.ReplyMarkup = keyboard;
			            toReturn.Add(article);
		            }
		            else if (x.id == "2")
		            {
			            if (randInt < 50)
			            {
				            InlineQueryResultArticle article = new InlineQueryResultArticle(x.id, x.title,
					            new InputTextMessageContent("Выпала Решка 🪙"));
				            article.Description = x.desc;
				            article.ReplyMarkup = keyboard;
				            toReturn.Add(article);
			            }
			            else
			            {
				            InlineQueryResultArticle article = new InlineQueryResultArticle(x.id, x.title,
					            new InputTextMessageContent("Выпал Орел 🦅"));
				            article.Description = x.desc;
				            article.ReplyMarkup = keyboard;
				            toReturn.Add(article);
			            }
		            }
		            else if (x.id == "3")
		            {
			            InlineQueryResultArticle article = new InlineQueryResultArticle(x.id, x.title,
					        new InputTextMessageContent("Укажите игроков через пробел"));
				        article.Description = x.desc;
				        article.ReplyMarkup = keyboard;
				        toReturn.Add(article);
		            }
	            }
            }
            return toReturn;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Запуск...");
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            allAnsw.Add(new action{id = "1", title = "/roll", answ = "Выпало число - ", desc = "Случайное число от 1 до 100"});
            allAnsw.Add(new action { id = "2", title = "/flip", answ = "", desc = "Орел или Решка"});
            allAnsw.Add(new action { id = "3", title = "/cl", answ = "", desc = "Распределение игроков по командам(указать через пробел четное количество и выбрать /cl)" });

            keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton("Try it too😉") { SwitchInlineQueryCurrentChat = ""});

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

            while (true)
            {
	            Console.ReadLine();
            }
            
        }
    }
}
