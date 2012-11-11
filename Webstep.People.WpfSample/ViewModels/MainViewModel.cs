using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Webstep.People.Domain;
using Webstep.People.WpfSample.Model;
using Webstep.People.WpfSample.Model.Events;

namespace Webstep.People.WpfSample.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        PersonService _personService = new PersonService();

        public MainViewModel()
        {
            LoadPeopleCommand = new RelayCommand(() => _personService.GetPeopleRest());
            Messenger.Default.Register<PeopleRetrievedEvent>(this, (e) =>
            {

                foreach (var person in e.People)
                {
                    People.Add(person);
                }
            });
        }

        public ICommand LoadPeopleCommand { get; set; }

        private ObservableCollection<Person> _people = new ObservableCollection<Person>();
        public ObservableCollection<Person> People
        {
            get
            {
                return (_people);
            }
            set
            {
                if (_people == value) return;
                _people = value;
                RaisePropertyChanged(() => People);
            }
        }
    }
}
