using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        readonly PersonService _personService = new PersonService();

        public MainViewModel()
        {
            AddPersonCommand = new RelayCommand(AddPerson);
            LoadPeopleCommand = new RelayCommand(() =>
                {
                    People.Clear();
                    _personService.GetPeople();
                });
            DeletePersonCommand = new RelayCommand<Person>(DeletePerson);

            Messenger.Default.Register<CommunicationFailedEvent>(this, (e) => MessageBox.Show(e.Message));
            Messenger.Default.Register<PersonCreatedEvent>(this, (e) => _people.Add(e.Person));
            Messenger.Default.Register<PersonDeletedEvent>(this, (e) =>
                {
                    if (People.Any(p => p.Id == e.Person.Id))
                    {
                        var person = People.First(p => p.Id == e.Person.Id);
                        _people.Remove(person);
                    }
                });
            Messenger.Default.Register<PeopleRetrievedEvent>(this, (e) =>
            {
                foreach (var person in e.People)
                {
                    People.Add(person);
                }
            });
        }

        private void DeletePerson(Person person)
        {
            _personService.Delete(person);
        }

        private void AddPerson()
        {
            var person = new Person
                {
                    FirstName = "Hans",
                    LastName = "Eriksen",
                    Email = "hans.eriksen@webstep.no"
                };
            _personService.AddPerson(person);
        }

        public ICommand LoadPeopleCommand { get; set; }
        public ICommand AddPersonCommand { get; set; }
        public RelayCommand<Person> DeletePersonCommand { get; set; }

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
