using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Net;

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
                if (!string.IsNullOrWhiteSpace(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = $"Latitude: {t.lat}\n" +
                                                $"Longitude: {t.lon}\n" +
                                                $"Nascer do Sol: {t.sunrise}\n" +
                                                $"Pôr do Sol: {t.sunset}\n" +
                                                $"Temp Máx: {t.temp_max}°C\n" +
                                                $"Temp Mín: {t.temp_min}°C\n" +
                                                $"Clima: {t.description}\n" +
                                                $"Vento: {t.speed} m/s\n" +
                                                $"Visibilidade: {t.visibility} metros";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        await DisplayAlert("Cidade não encontrada", "Não foi possível localizar a cidade informada.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Campo vazio", "Por favor, digite o nome de uma cidade.", "OK");
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Erro de conexão", "Verifique sua internet e tente novamente.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro inesperado", ex.Message, "OK");
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}
