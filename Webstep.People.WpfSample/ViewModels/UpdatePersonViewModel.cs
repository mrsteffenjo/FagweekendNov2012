using GalaSoft.MvvmLight;
using Webstep.People.Domain;

namespace Webstep.People.WpfSample.ViewModels
{
    public class UpdatePersonViewModel : ViewModelBase
    {
        public UpdatePersonViewModel(Person person)
        {
            Person = person;
        }

        public Person Person { get; set; }
    }
}
