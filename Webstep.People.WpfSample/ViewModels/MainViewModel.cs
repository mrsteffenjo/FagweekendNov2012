using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Webstep.People.Domain;
using Webstep.People.WpfSample.Model;
using Webstep.People.WpfSample.Model.Events;
using Webstep.People.WpfSample.Views;

namespace Webstep.People.WpfSample.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        readonly PersonService _personService = new PersonService();

        public MainViewModel()
        {
            // Wire up commands / Button clicks
            AddPersonCommand = new RelayCommand(AddPerson);
            LoadPeopleCommand = new RelayCommand(() =>
                {
                    People.Clear();
                    _personService.GetPeople();
                });
            DeletePersonCommand = new RelayCommand<Person>(DeletePerson);
            UpdatePersonCommand = new RelayCommand<Person>(UpdatePerson, person =>
                {
                    return SelectedPerson != null;
                });

            // Events handlers
            Messenger.Default.Register<CommunicationFailedEvent>(this, (e) => MessageBox.Show(e.Message));
            Messenger.Default.Register<PersonUpdatedEvent>(this, (e) => MessageBox.Show("Person was updated on server"));
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

        public ICommand LoadPeopleCommand { get; set; }
        public ICommand AddPersonCommand { get; set; }
        public RelayCommand<Person> DeletePersonCommand { get; set; }
        public RelayCommand<Person> UpdatePersonCommand { get; set; }


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


        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get
            {
                return (_selectedPerson);
            }
            set
            {
                if (_selectedPerson == value) return;
                _selectedPerson = value;
                RaisePropertyChanged(() => SelectedPerson);
            }
        }
        

        private void DeletePerson(Person person)
        {
            if (MessageBox.Show("Do you want to delete this person?", "Cofirmation of delete", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                _personService.Delete(person);
            }
           
        }

        private void AddPerson()
        {
            
            var createWindow = new CreateView();

            if (createWindow.ShowDialog() == true)
            {
                var viewModel = (CreatePersonViewModel)createWindow.DataContext;
                _personService.AddPerson(viewModel.Person);
            }
           
        }

        private void UpdatePerson(Person person)
        {
            var updateWindow = new UpdateView();
            updateWindow.DataContext = new UpdatePersonViewModel(person);
          
            if (updateWindow.ShowDialog() == true)
            {
               
                _personService.UpdatePerson(person);
            }

        }

        
    }
}
