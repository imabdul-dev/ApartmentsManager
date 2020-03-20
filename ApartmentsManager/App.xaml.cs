using System;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using ApartmentsManager.Services;

namespace ApartmentsManager
{
    public sealed partial class App
    {
        private readonly Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();

            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(typeof(Views.ContentGridPage), new Lazy<UIElement>(CreateShell));
        }

        private static UIElement CreateShell()
        {
            return new Views.ShellPage();
        }
    }
}
