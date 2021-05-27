using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Amiibopedia.Helpers
{
    public class DisplayAlertHelper
    {
        public async Task MostrarDisplayAlertAsync(string titulo, string mensaje, string botoncancel)
        {
            await Application.Current.MainPage.DisplayAlert
                (titulo, mensaje, botoncancel, FlowDirection.LeftToRight);
        }
    }
}
