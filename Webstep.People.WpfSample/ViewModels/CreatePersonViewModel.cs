using GalaSoft.MvvmLight;
using Webstep.People.Domain;

namespace Webstep.People.WpfSample.ViewModels
{
    public class CreatePersonViewModel : ViewModelBase
    {
        public CreatePersonViewModel()
        {
            Person = Person.Create();
        }

        public Person Person { get; set; }
    }
}
