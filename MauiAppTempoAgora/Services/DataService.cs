using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;


namespace MauiAppTempoAgora.Services
{
    public  class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;

            string chave = "3fb32cacaee56c7feeb40185e7e924dc";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&appid={chave}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();


                    var rascunho = JObject.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    t = new()
                    {
                        Lon = (double)rascunho["coord"]["lon"],
                        Lat = (double)rascunho["coord"]["lat"],
                        Description = (string)rascunho["weather"][0]["description"],
                        Main = (string)rascunho["weather"][0]["main"],
                        Temp_Min = (double)rascunho["main"]["temp_Min"],
                        Temp_Max = (double)rascunho["main"]["temp_Max"],
                        Speed = (double)rascunho["wind"]["speed"],
                        Visibility = (int)rascunho["visibility"],
                        Sunrise = sunrise.ToString(),
                        Sunset = sunset.ToString(),
                    }; //Fecha obj do Tempo.
                } //Fecha if se o status do servidor foi de sucesso
            } // fecha laço using
            return t;
        }
    }
}
