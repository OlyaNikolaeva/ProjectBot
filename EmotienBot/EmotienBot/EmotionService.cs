using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotienBot
{
    class EmotionService<T> : IDataService<T> where T : Emotion, new()
    {
        const string connString = "Host=localhost;Port=5432;Username=postgres;Password=5432;Database=postgres";
        private string ConnectionString { get; set; }

        public void Save(T file)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO emotion(contempt,disgust,anger,fear,happiness,neutral,sadness,surprise,photo_id,create_date) VALUES (@contempt,@disgust,@anger,@fear,@happiness,@neutral,@sadness,@surprise,@photo_id,@create_date)";
                    cmd.Parameters.AddWithValue("@contempt",file.Contempt);
                    cmd.Parameters.AddWithValue("@disgust", file.Disgust);
                    cmd.Parameters.AddWithValue("@anger", file.Anger);
                    cmd.Parameters.AddWithValue("@fear", file.Fear);
                    cmd.Parameters.AddWithValue("@happiness", file.Happiness);
                    cmd.Parameters.AddWithValue("@neutral", file.Neutral);
                    cmd.Parameters.AddWithValue("@sadness", file.Sadness);
                    cmd.Parameters.AddWithValue("@surprise", file.Surprise);
                    cmd.Parameters.AddWithValue("@photo_id", file.PhotoId);
                    cmd.Parameters.AddWithValue("@create_date", file.DateTimeCreate);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<T> GetAll()
        {
            var result = new List<T>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT contempt,disgust,anger,fear,happiness,neutral,sadness,surprise,photo_id,create_date FROM emotion";
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        T entity = new T
                        {
                            PhotoId = int.Parse(reader[0].ToString()),
                            Contempt = int.Parse(reader[0].ToString()),
                            Disgust= int.Parse(reader[0].ToString()),
                            Anger = int.Parse(reader[0].ToString()),
                            Fear = int.Parse(reader[0].ToString()),
                            Happiness = int.Parse(reader[0].ToString()),
                            Neutral = int.Parse(reader[0].ToString()),
                            Sadness = int.Parse(reader[0].ToString()),
                            Surprise = int.Parse(reader[0].ToString()),
                            DateTimeCreate = Convert.ToDateTime(reader[3].ToString())
                        };
                        result.Add(entity);
                    }
                }

                return result;
            }
        }
    }
}
