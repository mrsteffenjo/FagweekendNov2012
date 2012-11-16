using System.Windows;
using Webstep.People.WpfSample.ViewModels;

namespace Webstep.People.WpfSample.Views
{
    /// <summary>
    /// Interaction logic for CreateView.xaml
    /// </summary>
    public partial class CreateView : Window
    {
        public CreateView()
        {
            InitializeComponent();
            Loaded += (sender, args) => DataContext = new CreatePersonViewModel();
        }

        private void OKClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelClik(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
