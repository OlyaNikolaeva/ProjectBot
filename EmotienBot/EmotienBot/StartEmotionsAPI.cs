using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmotienBot
{
    class StartEmotionsAPI
    {
        public async Task<Emotion> Start(string imageFilePath)
        {
            var makeAnalys = new MakeAnalyst();

            if (!File.Exists(imageFilePath))
            {
                throw new Exception();
            }

            try
            {
                var g = await makeAnalys.MakeAnalysisRequest(imageFilePath);
                return g;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
