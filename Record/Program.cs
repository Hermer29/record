using Newtonsoft.Json;
using Record.Controllers.CommandHandlers;
using Record.DataAccess;
using Record.Model;
using Record.Model.Sessions;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using MenuButton = Telegram.Bots.Types.MenuButton;
using MenuButtonType = Telegram.Bots.Types.MenuButtonType;

namespace TelegramBotExperiments
{

    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("6490963988:AAEgOVkCzEmFwED3kJ1uSI2M8KwkpF63cII");
        private static Session _session = new Session(new MessagesDataAccess(), "");

        private List<Chat> _chats = new List<Chat>();
        private static AppointmentMaking _making = new AppointmentMaking(bot);
        
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if(update.Type == UpdateType.Message)
            {
                var message = update.Message;
                
                if (message == null)
                    return;
                if (message.Text == null)
                    return;
                
                _making.NotifyUpdate(update);

                var router = new CommandRouter(bot, _session);
                router.Route(message);
                await Task.Yield();
                //await botClient.SendTextMessageAsync(message.Chat, "<span class=\"tg-quote\" style=\"color: rgb(255, 0, 0);\"> myQuote</span>", parseMode: ParseMode.Html);
                var inputMessagesController = new InputMessagesController(_session);
                inputMessagesController.ReceiveMessage(message.Text);
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }

        static void Main(string[] args)
        {
            new RestoreSchema().Execute();
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            CancellationToken cancellationToken = cts.Token;
           
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                new ReceiverOptions
                {
                    AllowedUpdates = {}
                },
                cancellationToken
            );
            bot.OnApiResponseReceived += BotOnOnApiResponseReceived;
            Console.ReadLine();
        }

        private static ValueTask BotOnOnApiResponseReceived(ITelegramBotClient botclient, ApiResponseEventArgs args, CancellationToken cancellationtoken)
        {
            //Console.WriteLine(JsonConvert.SerializeObject(args, Formatting.Indented));
            return ValueTask.CompletedTask;
        }
    }
}