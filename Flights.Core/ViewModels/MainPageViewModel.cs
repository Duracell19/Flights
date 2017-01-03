using Flights.Infrastructure.Interfaces;
using MvvmCross.Core.ViewModels;

namespace Flights.Core.ViewModels
{
    public class MainPageViewModel : MvxViewModel
    {  
        private MainPageEntryViewModel _mainPageEntryViewModel;
        private MainPageFavoritesViewModel _mainPageFavoritesViewModel;
        /// <summary>
        /// Properties witch allow to get access to MainPageEntryViewModel and MainPageFavoritesViewModel
        /// </summary>
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
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpService">Parameter to work with http service</param>
        /// <param name="jsonConverter">Parameter to work with json converter</param>
        /// <param name="fileStore">Parameter to work with files</param>
        public MainPageViewModel(IHttpService httpService,
            IJsonConverter jsonConverter,
            IFileStore fileStore)
        {
            _mainPageEntryViewModel = new MainPageEntryViewModel(httpService, jsonConverter, fileStore);
            _mainPageFavoritesViewModel = new MainPageFavoritesViewModel(jsonConverter, fileStore);
        }
    }
}