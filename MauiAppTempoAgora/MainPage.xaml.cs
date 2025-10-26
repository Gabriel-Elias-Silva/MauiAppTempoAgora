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

        private async void Button_Clicked_Previsão(object sender, EventArgs e)
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

        private async void Button_Clicked_Localização(object sender, EventArgs e)
        {
            try
            {
                GeolocationRequest geolocationRequest = new GeolocationRequest
                                    (GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                Location? local = await Geolocation.Default.GetLocationAsync(geolocationRequest);
                if (local != null)
                {
                    string local_disp = $"Latitude: {local.Latitude} \n"
                            + $"Longitude {local.Longitude}";
                    lbl_coords.Text = local_disp;
                }
                else
                {
                    lbl_coords.Text = "Nenhuma Localização";
                }

            }

            catch (FeatureNotSupportedException fnex)
            {
                await DisplayAlert("Erro: Dispositivo não suporta", fnex.Message, "OK");
            }
            catch (FeatureNotEnabledException fnex)
            {
                await DisplayAlert("Erro: Localização Desabilitada", fnex.Message, "OK");

            }
            catch (PermissionException pex)
            {
                await DisplayAlert("Erro: Permissão da Localização", pex.Message, "OK");


            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");

            }
        }
private async void GetCidade(double lat, double lon)
        {
        IEnumerable<Placemark> places = await    Geocoding.Default.GetPlacemarksAsync(lat, lon);
            Placemark? place = places.FirstOrDefault();
            if (place != null)
            {
                txt_cidade.Text = place.Locality; 


            }

        } 
    } 
    } 
