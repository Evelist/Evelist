using System.Diagnostics;
using System.Linq;
using Windows.UI.Core;
using EveList8._1.Common;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону "Приложение с Pivot" см. по адресу http://go.microsoft.com/fwlink/?LinkID=391641
using EveList8._1.DataModel;
using EveList8._1.View;

namespace EveList8._1
{
    /// <summary>
    /// Обеспечивает зависящее от конкретного приложения поведение, дополняющее класс Application по умолчанию.
    /// </summary>
    public sealed partial class App : Application
    {
        private TransitionCollection _transitions;

        /// <summary>
        /// Инициализирует одноэлементный объект приложения. Это первая выполняемая строка разрабатываемого
        /// кода; поэтому она является логическим эквивалентом main() или WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        /// <summary>
        /// Вызывается при обычном запуске приложения пользователем.  Будут использоваться другие точки входа,
        /// если приложение запускается для открытия конкретного файла, отображения
        /// результатов поиска и т. д.
        /// </summary>
        /// <param name="e">Сведения о запросе и обработке запуска.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (Debugger.IsAttached)
            {
                DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            var rootFrame = Window.Current.Content as Frame;

            // Не повторяйте инициализацию приложения, если в окне уже имеется содержимое,
            // только обеспечьте активность окна.
            if (rootFrame == null)
            {
                // Создание фрейма, который станет контекстом навигации, и переход к первой странице.
                rootFrame = new Frame();

                // Связывание фрейма с ключом SuspensionManager.
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

                // TODO: Измените это значение на размер кэша, подходящий для вашего приложения.
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Восстановление сохраненного состояния сеанса только тогда, когда это необходимо.
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        // Возникли ошибки при восстановлении состояния.
                        // Предполагаем, что состояние отсутствует, и продолжаем.
                    }
                }

                // Размещение фрейма в текущем окне.
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // Удаляет турникетную навигацию для запуска.
                if (rootFrame.ContentTransitions != null)
                {
                    _transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        _transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += RootFrame_FirstNavigated;

                // Если стек навигации не восстанавливается для перехода к первой странице,
                // настройка новой страницы путем передачи необходимой информации в качестве параметра
                // навигации.
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                if (localSettings.Values.ContainsKey("session"))
                {
                    var api = new EvelistApi.EvelistApiClient();
                    var dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
                    api.GetProfileInfo((string) localSettings.Values["session"]).ContinueWith(r =>
                    {
                        dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                        {
                            if (r.Result.message == "OK")
                            {
                                var tr = r.Result.info;
                                Session.GetInstance().CurrentSession = (string)localSettings.Values["session"];
                                Session.GetInstance().CurrentUser = new Person(tr.firstname, tr.surname, tr.avatar, tr.sex);

                                if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
                                {
                                    throw new Exception("Failed to create initial page");
                                }
                            }
                            else
                            {
                                if (!rootFrame.Navigate(typeof(AuthPage), e.Arguments))
                                {
                                    throw new Exception("Failed to create initial page");
                                }
                            }
                        });
                    });
                }

                //if (!rootFrame.Navigate(typeof(AuthPage), e.Arguments))
                //{
                //    throw new Exception("Failed to create initial page");
                //}
            }

            // Обеспечение активности текущего окна.
            Window.Current.Activate();
        }

        /// <summary>
        /// Восстанавливает переходы содержимого после запуска приложения.
        /// </summary>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = _transitions ?? new TransitionCollection { new NavigationThemeTransition() };
            rootFrame.Navigated -= RootFrame_FirstNavigated;
        }

        /// <summary>
        /// Вызывается при приостановке выполнения приложения.  Состояние приложения сохраняется
        /// без учета информации о том, будет ли оно завершено или возобновлено с неизменным
        /// содержимым памяти.
        /// </summary>
        /// <param name="sender">Источник запроса приостановки.</param>
        /// <param name="e">Сведения о запросе приостановки.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }
    }
}
