using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using Webstep.People.WpfSample.Model.Events;
using Webstep.People.WpfSample.ViewModels;

namespace Webstep.People.WpfSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
          
            Loaded += (sender, args) => Init();
       
        }


        private void Init()
        {
            var viewModel = new MainViewModel();
            DataContext = viewModel;

            Messenger.Default.Register<PersonCreatedEvent>(this, (e) =>
            {
                
                // Making sure our inserted person comes into  view in datagrid (appears in bottom)
                PersonGrid.ScrollIntoView(e.Person);
            });
        }
    }
}
