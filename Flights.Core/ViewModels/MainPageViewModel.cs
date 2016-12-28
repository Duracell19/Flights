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

        public MainPageViewModel(IJsonConverter jsonConverter,
            IFileStore fileStore,
            ICitiesService citiesService,
            IIataService iataService)
        {
            _mainPageEntryViewModel = new MainPageEntryViewModel(jsonConverter, fileStore, citiesService, iataService);
            _mainPageFavoritesViewModel = new MainPageFavoritesViewModel();
        }
    }
}