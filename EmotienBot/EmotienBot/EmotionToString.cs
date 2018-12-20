using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotienBot
{
    public class EmotionToString
    {
        public string ToStringEm(string emotion)
        {
            if (emotion == "Anger")
            {
                return "Ваше сострояние сейчас-это злость. Попробуйте выпить травянного чая с жасмином";
            }

            if (emotion == "Contempt")
            {
                return "Ваше лицо выражает сейчас презрение. Что тебе в жизни не хватает, почитай книжку мизантроп";
            }

            if (emotion == "Disgust")
            {
                return "Ваше лицо выражает сейчас отвращение.";
            }

            if (emotion == "Fear")
            {
                return "Ваше лицо выражает страх.";
            }

            if (emotion == "Happiness")
            {
                return "Ваше лицо выражает счастье.";
            }

            if (emotion == "Neutral")
            {
                return "Ваше лицо выражает...эм, ничего. Ну улыбнись хоть, а то с покерфэйсом сидишь";
            }

            if (emotion == "Sadness")
            {
                return "Ваше лицо выражает грусть. Оооо, посмотри комедию, что ли";
            }

            if (emotion == "Surprise")
            {
                return "Ваше лицо выражает удивление. Ничего, сессия скоро пройдет";
            }

            else
            {
                return "Что с тобой? Тебя расплющило? К сожанению мы не смогли определить твое состояние, попробуй сфотографировать чуть-чуть поближе, либо сделай качественную фотку йоу";
            }

        }
    }
}
