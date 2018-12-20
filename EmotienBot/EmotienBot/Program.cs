using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace EmotienBot
{
    class Program
    {
        static ITelegramBotClient botClient;
        public static Dictionary<int, UserInfo> UserInfos;
        public static UserService<Human> Service;
        public static FileService<Photo> PhotoService;

        public static void Main()
        {
            botClient = new TelegramBotClient("637745165:AAF0CMMeHQIuOHk_4w44n17uM2q6mEj5Vt8");

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
                $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            Service = new UserService<Human>();
            PhotoService = new FileService<Photo>();

            UserInfos = new Dictionary<int, UserInfo>();

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        public static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var dateCurrent = DateTime.Today;
            var listPeople = Service.GetAll();
            var senderId = e.Message.From.Id;
            UserInfos.TryGetValue(senderId, out var userInfo);

            if (userInfo == null)
            {
                await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: "Привет, меня зовут Гриша-Голубь и я являюсь ботом, который распознает твои эмоции по фотографии, которую ты мне отправищь)"
                );

                userInfo = new UserInfo
                {
                    Emotion = new Emotion(),
                    Human = new Human(),
                    Step = 0,
                    Photo = new Photo()
                };

                UserInfos.Add(senderId, userInfo);
                userInfo.Human.SenderId = senderId;
                userInfo.Human.Date = dateCurrent;

                //var listPeople = Service.GetAll();
                foreach (var i in listPeople)
                {
                    if (userInfo.Human.SenderId == i.SenderId)
                    {
                        userInfo.Step = 4;
                        await botClient.SendTextMessageAsync(
                                chatId: e.Message.Chat,
                                text: "Вы уже записаны в базе.Рады вас снова видеть) "

                            );
                        return;
                    }
                    else
                    {
                        Service.Save(userInfo.Human);
                        userInfo.Step = 0;
                        await botClient.SendTextMessageAsync(
                                chatId: e.Message.Chat,
                                text: "Рады с вами познакомится) Позвольте записать вас в базу"
                            );
                        return;
                    }
                }
            }

            if (userInfo.Step == 0)
            {
                userInfo.Step++;
                await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "Начнем?"
                    );
                return;
            }

            if (userInfo.Step == 1)
            {
                if (e.Message.Text == "да" || e.Message.Text == "Да" || e.Message.Text == "Yes")

                {
                    userInfo.Step++;
                    await botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "Отлично, как тебя зовут?"
                        );
                    return;
                }

                if (e.Message.Text == "нет" || e.Message.Text == "Нет" || e.Message.Text == "No")

                {
                    userInfo.Step++;
                    await botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "Так, кто бы ты ни был, ты ОФИГЕЛ!!! Я тут значит работаю, твои эмоции пытаюсь просканировать, а ты со мной разговаривать значит не хочешь??? У тя просто ннет выбора) Как тебя зовут незнакомец?"
                        );
                    return;
                }

                else

                {
                    userInfo.Step++;
                    await botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "Я не бэлмэс тебяя, но сочту твой ответ за Да) Как тебя зовут, незнакомец?"
                        );
                    return;
                }
            }

            if (userInfo.Step == 2)
            {
                var name = e.Message.Text;
                userInfo.Human.Name = name;

                userInfo.Step++;

                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Отлично, а какая у вас фамилия, ну или ник???"
                );

                return;
            }

            if (userInfo.Step == 3)
            {
                var lastName = e.Message.Text;
                userInfo.Human.LastName = lastName;

                userInfo.Step++;
                Service.Save(userInfo.Human);
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Отлично, у меня есть необходимые данные, чтобы внести вас в базу"
                );
                return;
            }

            if (userInfo.Step == 4)
            {
                userInfo.Step++;
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Пришлите мне вашу фотку"
                );
                return;
            }

            if (userInfo.Step == 5)
            {
                if (e.Message.Photo != null)
                {
                    var photos = e.Message.Photo;
                    var photo = photos[photos.Length - 1];
                    var fileId = photo.FileId;
                    var photoIdentifier = Guid.NewGuid();
                    using (var fileStream = System.IO.File.OpenWrite($"files\\{photoIdentifier}.jpg"))
                    {
                        var fileInfo = await botClient.GetInfoAndDownloadFileAsync(
                          fileId: fileId,
                          destination: fileStream
                        );
                    }

                    userInfo.Photo.Path = $"files\\{photoIdentifier}.jpg";
                    userInfo.Photo.DateCreate = DateTime.Today;

                    userInfo.Photo.UserId = 1;
                    PhotoService.Save(userInfo.Photo);

                }
                userInfo.Step++;
            }

            if (userInfo.Step == 6)
            {
                var emotionGuy = new StartEmotionsAPI();

                var currentEmotion = await emotionGuy.Start(userInfo.Photo.Path);
                var emotion =Max(currentEmotion).ToString();
                
                var ty = new EmotionToString();
                var type = ty.ToStringEm(emotion);
                userInfo.Step++;
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: $"Ваше настроение сейчас "
                );
                return;
            }

            if (userInfo.Step == 7)
            {
                userInfo.Step++;
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Что ты хочешь сделать дальше?",
                    replyMarkup: new ReplyKeyboardMarkup
                    {
                        Keyboard = new[]
                        {
                                                new []
                                                {
                                                    new KeyboardButton("Хочу еще раз отправить фото"),
                                                    new KeyboardButton("Устал"),
                     
                                                },
                                                new []
                                                {
                                                    new KeyboardButton("Что то другое"),
                                                    new KeyboardButton("Хочу кофе")

                                                },

                        }

                    }
                );

                return;
            }

            if(userInfo.Step==7)
            {
                //if (e.Message.Text== "Хочу получить фото обратно")
                //{
                //    await botClient.SendPhotoAsync(
                //    chatId: e.Message.Chat,
                //    System.IO.File.OpenRead($"files\\{e.Message.Chat.Id}.jpg")
                //     );
                //}

                if (e.Message.Text == "Устал")
                {

                }

                if (e.Message.Text == "Что то другое")
                {

                }

                if (e.Message.Text == "Хочу кофе")
                {
                    userInfo.Step++;
                    await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "Советую сходить в Coffe Bean,говорят там скидки)"
                    );
                    return;
                }
            }

            if (userInfo.Step == 8)
            {
                userInfo.Step=0;
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: $"Теперь я устал, если хочешь еще поболтать, напиши мне попозже"
                );
                return;
            }
        }

        public static Emotion Max(Emotion emotion)
        {
            long max = 0;
            Emotion b = null;
            foreach (long i in emotion)
            {
                if (i > max)
                {
                    max = i;
                  
                }
            }
            return b ;
        }

    }
}
