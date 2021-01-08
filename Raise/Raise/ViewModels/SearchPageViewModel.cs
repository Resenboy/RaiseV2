using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Raise.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        public ICommand EffetiveSearchCommand { get; set; }
        public ICommand PartialSearchCommand { get; set; }

        public SearchPageViewModel()
        {
            EffetiveSearchCommand = new Command<string>(async (filter) => await EffetiveSearch(filter));
            PartialSearchCommand = new Command<string>(async (partialFilter) => await PartialSearch(partialFilter));
        }

        private async Task EffetiveSearch(string filter)
        {

        }

        private async Task PartialSearch(string partialFilter)
        {

        }
    }
}
