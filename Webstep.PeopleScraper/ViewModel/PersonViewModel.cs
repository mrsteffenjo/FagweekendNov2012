using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Webstep.People.Domain;

namespace Webstep.PeopleScraper.ViewModel
{
    public class PersonViewModel: ViewModelBase
    {
        public Person Person { get; set; }

        public PersonViewModel(Person person)
        {
            Person = person;
        }


        private string _info;
        public string Info
        {
            get
            {
                return (_info);
            }
            set
            {
                if (_info == value) return;
                _info = value;
                RaisePropertyChanged(() => Info);
            }
        }

    }
}
