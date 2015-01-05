// Документацию по шаблону элемента пустой страницы см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using EveList8._1.Common;

namespace EveList8._1.View
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
            Messenger<NavigationMessage>.Register(NavigateTo);
        }

        private void NavigateTo(NavigationMessage navigationMessage)
        {
            if(navigationMessage.PageName == "Main")
                Frame.Navigate(typeof (MainPage), navigationMessage.ParametrQuery);
        }

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }
    }
}
