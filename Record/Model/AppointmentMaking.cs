using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bots;
using Telegram.Bots.Types;
using ParseMode = Telegram.Bot.Types.Enums.ParseMode;
using Update = Telegram.Bot.Types.Update;
using UpdateType = Telegram.Bot.Types.Enums.UpdateType;

namespace Record.Model;

public class AppointmentMaking
{
    private readonly ITelegramBotClient _client;
    private Dictionary<long, AppointmentState> _operations = new();
    
    public enum State
    {
        WaitingForServiceSelection,
        WaitingForTimeSelection
    }

    public enum Service
    {
        Педик,
        Маник
    }
    
    public class AppointmentState
    {
        public State State = State.WaitingForServiceSelection;
        public Service Service;
    }

    public AppointmentMaking(ITelegramBotClient client)
    {
        _client = client;
    }
    
    public async Task NotifyUpdate(Update update)
    {
        var message = update.Message;
        
        if (!_operations.TryGetValue(message?.Chat.Id ?? 0, out AppointmentState? appointmentState))
        {
            var chatId = update.Message.Chat.Id;
            if (message.Text == "/record")
            {
                var appointment = new AppointmentState();
                _operations.Add(chatId, appointment);
                await _client.SendTextMessageAsync(update.Message.Chat.Id, "Выберите услугу:", replyMarkup: new ReplyKeyboardMarkup(
                    new []
                    {
                        new KeyboardButton("Маникюр"), 
                        new KeyboardButton("Педикюр")
                    }));
            }
        }
        else if (appointmentState.State == State.WaitingForServiceSelection)
        {
            if (update.Message != null)
            {
                if (update.Message.Text == "Маникюр")
                {
                    appointmentState.Service = Service.Маник;
                }
                else if (update.Message.Text == "Педикюр")
                {
                    appointmentState.Service = Service.Педик;
                }
                var url = @"https://hermer29.github.io/record-date/";
                var webAppInfo = new Telegram.Bot.Types.WebAppInfo() {Url = url};
                var response = await _client.SendTextMessageAsync(update.Message.Chat.Id, "Выберите время",
                    replyMarkup: new ReplyKeyboardMarkup(KeyboardButton.WithWebApp("Перейти к выбору", webAppInfo)),
                    allowSendingWithoutReply: false, cancellationToken: new CancellationToken());
                Console.WriteLine(JsonConvert.SerializeObject(response.WebAppData?.Data));
                appointmentState.State = State.WaitingForTimeSelection;
            }

            //await _client.SendTextMessageAsync(update.Message.Chat.Id, "<input id=\"datetime\" type=\"datetime-local\" />", parseMode: ParseMode.Html);
        }
        else if (appointmentState.State == State.WaitingForTimeSelection)
        {
            // if (update.Type == UpdateType.)
            // {
            //     Console.WriteLine(JsonConvert.SerializeObject(update.CallbackQuery));
            // }
        }
    }
}