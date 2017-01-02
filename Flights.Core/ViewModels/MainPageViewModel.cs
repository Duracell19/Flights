using Flights.Infrastructure.Interfaces;
using MvvmCross.Core.ViewModels;

namespace Flights.Core.ViewModels
{
    public class MainPageViewModel : MvxViewModel
    {  
        private MainPageEntryViewModel _mainPageEntryViewModel;
        private MainPageFavoritesViewModel _mainPageFavoritesViewModel;

        public MainPageEntryViewModel MainPageEntryViewModel
        {
            get { return _mainPageEntryViewModel; }
            set
            {
                _mainPageEntryViewModel = value;
                RaisePropertyChanged(() => MainPageEntryViewModel);
            }
        }
        public MainPageFavoritesViewModel MainPageFavoritesViewModel
        {
            get { return _mainPageFavoritesViewModel; }
            set
            {
                _mainPageFavoritesViewModel = value;
                RaisePropertyChanged(() => MainPageFavoritesViewModel);
            }
        }

        public MainPageViewModel(IHttpService httpService,
            IJsonConverter jsonConverter,
            IFileStore fileStore)
        {
            _mainPageEntryViewModel = new MainPageEntryViewModel(httpService, jsonConverter, fileStore);
            _mainPageFavoritesViewModel = new MainPageFavoritesViewModel(jsonConverter, fileStore);
        }
    }
}