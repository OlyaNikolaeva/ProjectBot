﻿using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace EmotienBot
{
    static class Program
    {
        static ITelegramBotClient botClient;
        public static Dictionary<int, UserInfo> UserInfos;

        public static void Main()
        {
            botClient = new TelegramBotClient("637745165:AAF0CMMeHQIuOHk_4w44n17uM2q6mEj5Vt8");

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
                $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            UserInfos = new Dictionary<int, UserInfo>();

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        public static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var service = new UserService<Human>();

            var senderId = e.Message.From.Id;
            UserInfos.TryGetValue(senderId, out var userInfo);
            if (userInfo == null)
            {
                userInfo = new UserInfo
                {
                    Human = new Human(),
                    Step = 0
                };

                UserInfos.Add(senderId, userInfo);
            }

            if (userInfo.Step == 0)
            {
                userInfo.Step++;
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Привет, меня зовут Гриша-Голубь и я являюсь ботом, который распознает твои эмоции по фотографии, которую ты мне отправищь) Ну что, начнем?"
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
                userInfo.Human.SenderId = senderId;

                userInfo.Step++;

                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Отлично, у меня есть необходимые данные, чтобы внести вас в базу"
                );
                return;
            }

            if (userInfo.Step == 4)
            {
                foreach (var i in userInfo.ToString())
                {
                    if ()
                    {
                        userInfo.Step++;
                        await botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "Ваше Id уже есть в базе"
                        );
                        return;
                    }
                    else
                    {
                        userInfo.Step++;
                        service.Save(userInfo.Human);
                        await botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "Вы добавлены"
                        );
                        return;
                    }
                }
            }
            
        }
    }
}

//                    userInfo.Step++;
//                    await botClient.SendTextMessageAsync(
//                        chatId: e.Message.Chat,
//                        text: "В какой ты группе?",
//                        replyMarkup: new ReplyKeyboardMarkup
//                        {
//                            Keyboard = new[]
//                            {
//                                new []
//                                {
//                                    new KeyboardButton("709"),
//                                    new KeyboardButton("701"),
//                                    new KeyboardButton("704")
//                                },
//                                new []
//                                {
//                                    new KeyboardButton("704"),
//                                    new KeyboardButton("702"),
//                                },
//                                new []
//                                {
//                                    new KeyboardButton("703"),
//                                    new KeyboardButton("705"),
//                                    new KeyboardButton("я школьник")
//                                },
//                            }

//                        }
//                    );

//                    return;
//                }
//                else
//                {
//                    await botClient.SendTextMessageAsync(
//                        chatId: e.Message.Chat,
//                        text: "Введи возраст"
//                    );

//                    return;
//                }
//            }
//            if (userInfo.Step == 3)
//            {
//                // ReplyKeyboardMarkup.OnTimeKeyboard = true;
//                if (e.Message.Text != "709")

//                {
//                    await botClient.SendTextMessageAsync(
//                            chatId: e.Message.Chat,
//                            text: "Ну, хорошая попытка"
//                        );
//                    return;
//                }
//            }
//            var group_number = e.Message.Text;
//            service.Save(userInfo.Human);
//            //service.Save(group_number); //--
//            // dataBase.DataBaseInfo("group_number", e.Message.Text);
//            await botClient.SendTextMessageAsync(
//                        chatId: e.Message.Chat,
//                        text: "Ты красавчик!!!!"
//                    );
//            return;

//        }
//    }
//}