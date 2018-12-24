using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotienBot
{
    public class MaxEmotionKek
    {
        public string Max(Emotion emotion)
        {
            var anger = emotion.Anger;
            var contempt = emotion.Contempt;
            var disgust = emotion.Disgust;
            var fear = emotion.Fear;
            var happyness = emotion.Happiness;
            var neutral = emotion.Neutral;
            var sadness = emotion.Sadness;
            var surprise = emotion.Surprise;
            var res = anger;
            long max = anger;
            var array = new[] { anger, contempt, disgust, fear, happyness, neutral, sadness, surprise };
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    res = i;
                }

            }
            var t = Result(res);
            return t;
        }

        public string Result(long res)
        {
            var str = "";
            if (res == 0)
            {
                str = "Anger";
            }

            if (res == 1)
            {
                str = "Contempt";
            }
            if (res == 2)
            {
                str = "Disgust";
            }
            if (res == 3)
            {
                str = "Fear";
            }
            if (res == 4)
            {
                str = "Happyness";
            }
            if (res == 5)
            {
                str = "Neutral";
            }
            if (res == 6)
            {
                str = "Sadness";
            }
            if (res == 7)
            {
                str = "Surprise";
            }
            return str;
        }
    }
}
