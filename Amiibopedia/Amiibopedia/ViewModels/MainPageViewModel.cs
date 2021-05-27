using Amiibopedia.Helpers;
using Amiibopedia.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Amiibopedia.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<Amiibo> _amiibos;

        public ObservableCollection<Character> Characters { get; set; }
        public ObservableCollection<Amiibo> Amiibos
        {
            get => _amiibos;
            set
            {
                _amiibos = value;
                OnPropertyChanged();
            }
        }
        public ICommand SearchCommand { get; set; }

        public MainPageViewModel()
        {
            SearchCommand = new Command(async (param) =>
            {
                try
                {
                    IsBusy = true;
                    var character = param as Character;
                    if (character != null)
                    {
                        string url = $"https://www.amiiboapi.com/api/amiibo/?character={character.name}";
                        var service = new HttpHelper<Amiibos>();
                        var amiibos = await service.GetRestServiceDataAsync(url);
                        Amiibos = new ObservableCollection<Amiibo>(amiibos.amiibo);
                    }
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    DisplayAlertHelper displayAlert = new DisplayAlertHelper();
                    await displayAlert.MostrarDisplayAlertAsync($"Error de tipo {ex.GetType().Name}", "A ocurrido un error con la petición al servidor", "Ok");
                }
            });
        }

        /// <summary>
        /// Método asíncrono que consume el servicio REST a través de la clase HttpHelper.
        /// Consulta todos los amiibos
        /// </summary>
        /// <returns></returns>
        public async Task LoadCharacters()
        {
            try
            {
                IsBusy = true;
                var url = "https://www.amiiboapi.com/api/character/";
                var service = new HttpHelper<Characters>();
                var characters = await service.GetRestServiceDataAsync(url);
                Characters = new ObservableCollection<Character>(characters.amiibo);
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                DisplayAlertHelper displayAlert = new DisplayAlertHelper();
                await displayAlert.MostrarDisplayAlertAsync($"Error de tipo {ex.GetType().Name}", "A ocurrido un error con la petición al servidor", "Ok");
            }
        }
    }
}
