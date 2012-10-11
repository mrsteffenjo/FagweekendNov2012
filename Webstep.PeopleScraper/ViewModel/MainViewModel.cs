using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using Webstep.People.Domain;
using Webstep.PeopleScraper.Events;

namespace Webstep.PeopleScraper.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        const string Url = "http://www.webstep.no/ansatte/stavanger/";
        readonly PersonService _personService = new PersonService();
        private int _infoCounter;
        private bool _isFinishedScraping;

        public MainViewModel()
        {
            LoadPeopleCommand = new RelayCommand(LoadPeople);
            SaveToFileCommand = new RelayCommand(SaveToFile, () => _isFinishedScraping);
            People = new ObservableCollection<PersonViewModel>();

            Messenger.Default.Register<PeopleRetrievedEvent>(this, (e) =>
                                                                       {

                                                                           foreach (var person in e.People)
                                                                           {
                                                                               People.Add(new PersonViewModel(person));
                                                                               _infoCounter += 1;
                                                                               LoadPersonInfo(person);
                                                                           }
                                                                       });
            Messenger.Default.Register<PersonInfoRetrievedEvent>(this, (e) =>
                                                                           {
                                                                               var personViewModel =
                                                                                   People.First(
                                                                                       p => p.Person.Id == e.Person.Id);
                                                                               personViewModel.Info = e.Info;
                                                                               if (_infoCounter == People.Count)
                                                                               {
                                                                                   _isFinishedScraping = true;
                                                                               }
                                                                           });
        }

        public ICommand LoadPeopleCommand { get; set; }
        public ICommand SaveToFileCommand { get; set; }


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

        private void SaveToFile()
        {
            var persons = People.Select(p => p.Person);
            var dlg = new Microsoft.Win32.SaveFileDialog
                          {FileName = "Webstep-People", DefaultExt = ".json", Filter = "Json object (.json)|*.json"};
            
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                string json = JsonConvert.SerializeObject(persons, Formatting.Indented);
                File.WriteAllText(filename, json);
            }

        }
    }
}
