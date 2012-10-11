using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Webstep.People.Domain;
using Webstep.PeopleScraper.Events;

namespace Webstep.PeopleScraper.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        const string Url = "http://www.webstep.no/ansatte/stavanger/";
        readonly PersonService _personService = new PersonService();

        public MainViewModel()
        {
            LoadPeopleCommand = new RelayCommand(LoadPeople);
            People = new ObservableCollection<PersonViewModel>();

            Messenger.Default.Register<PeopleRetrievedEvent>(this, (e) =>
                                                                       {

                                                                           foreach (var person in e.People)
                                                                           {
                                                                               People.Add(new PersonViewModel(person));
                                                                               LoadPersonInfo(person);
                                                                           }
                                                                       });
            Messenger.Default.Register<PersonInfoRetrievedEvent>(this, (e) =>
                                                                           {
                                                                               var personViewModel =
                                                                                   People.First(
                                                                                       p => p.Person.Id == e.Person.Id);
                                                                               personViewModel.Info = e.Info;
                                                                           });
        }

        public ICommand LoadPeopleCommand { get; set; }

        private ObservableCollection<PersonViewModel> _people;
        public ObservableCollection<PersonViewModel> People
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

        private void LoadPeople()
        {
            _personService.DownloadPeople(Url);
        }

        private void LoadPersonInfo(Person person)
        {
            _personService.DownloadPersonInfo(person);
        }
    }
}
