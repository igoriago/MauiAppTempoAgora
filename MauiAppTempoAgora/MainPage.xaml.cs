using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if(!String.IsNullOrEmpty (txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);
                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.Lat}  \n" +
                                         $"Longitude: {t.Lon}  \n" +
                                         $"Nascer do Sol: {t.Sunrise}  \n" +
                                         $"Por do Sol: {t.Sunset}  \n" +
                                         $"Temperatura Máx: {t.Temp_Max}  \n" +
                                         $"Temperatura Min: {t.Temp_Min}  \n";




                        lbl_res.Text = dados_previsao;


                    } else
                    {
                        lbl_res.Text = "Sem dados de Previsão.";
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a Cidade.";
                }
                
            } catch(Exception ex) 
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }

}


//Continuar na próxima semana
