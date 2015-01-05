using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using EveList8._1.Common;

// Документацию по шаблону "Приложение с Pivot" см. по адресу http://go.microsoft.com/fwlink/?LinkID=391641
using EveList8._1.ViewModel;

namespace EveList8._1.View
{
    public sealed partial class MainPage : Page
    {
        private readonly NavigationHelper     _navigationHelper;

        public MainPage()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;

            _navigationHelper = new NavigationHelper(this);
            _navigationHelper.LoadState += NavigationHelper_LoadState;
            _navigationHelper.SaveState += NavigationHelper_SaveState;
        }

        /// <summary>
        /// Получает объект <see cref="NavigationHelper"/>, связанный с данным объектом <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        /// <summary>
        /// Заполняет страницу содержимым, передаваемым в процессе навигации. Также предоставляется (при наличии) сохраненное состояние
        /// при повторном создании страницы из предыдущего сеанса.
        /// </summary>
        /// <param name="sender">
        /// Источник события; как правило, <see cref="NavigationHelper"/>.
        /// </param>
        /// <param name="e">Данные события, предоставляющие параметр навигации, который передается
        /// <see cref="Frame.Navigate(Type, Object)"/> при первоначальном запросе этой страницы и
        /// словарь состояний, сохраненных этой страницей в ходе предыдущего
        /// сеанса. Состояние будет равно значению NULL при первом посещении страницы.</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            ((MainViewModel)DataContext).GetList();
            Messenger<NavigationMessage>.Register(message => Frame.Navigate(typeof(ItemPage), message.ParametrQuery));
            list.SelectedItem = null;
        }

        /// <summary>
        /// Сохраняет состояние, связанное с данной страницей, в случае приостановки приложения или
        /// удаления страницы из кэша навигации. Значения должны соответствовать требованиям сериализации
        /// <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">Источник события; как правило, <see cref="NavigationHelper"/>.</param>
        /// <param name="e">Данные события, которые предоставляют пустой словарь для заполнения
        /// сериализуемым состоянием.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            // TODO: Сохраните здесь уникальное состояние страницы.
        }


        #region Регистрация NavigationHelper

        /// <summary>
        /// Методы, предоставленные в этом разделе, используются исключительно для того, чтобы
        /// NavigationHelper для отклика на методы навигации страницы.
        /// <para>
        /// Логика страницы должна быть размещена в обработчиках событий для 
        /// <see cref="NavigationHelper.LoadState"/>
        /// и <see cref="NavigationHelper.SaveState"/>.
        /// Параметр навигации доступен в методе LoadState 
        /// в дополнение к состоянию страницы, сохраненному в ходе предыдущего сеанса.
        /// </para>
        /// </summary>
        /// <param name="e">Предоставляет данные для методов навигации и обработчики
        /// событий, которые не могут отменить запрос навигации.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //((MainViewModel)DataContext).NavigateOnEvent((EventViewModel)e.ClickedItem);
            Frame.Navigate(typeof (ItemPage), e.ClickedItem);
        }

        private void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pivot.SelectedIndex == 1)
            {
                SearchButton.Visibility = 
                    RefreshButton.Visibility = 
                    Visibility.Collapsed;
                ExitButton.Visibility = Visibility.Visible;
            }
            else
            {
                SearchButton.Visibility = RefreshButton.Visibility = Visibility.Visible;
                ExitButton.Visibility = Visibility.Collapsed;
            }
        }

    }
}
