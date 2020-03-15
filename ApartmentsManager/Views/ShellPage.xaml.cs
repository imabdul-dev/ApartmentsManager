using ApartmentsManager.ViewModels;

namespace ApartmentsManager.Views
{
    public sealed partial class ShellPage
    {
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
        }
    }
}
