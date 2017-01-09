using Flights.Core.Helpers;
using Flights.Infrastructure.Interfaces;
using Flights.Models;
using Flights.Services.Helpers;
using MvvmCross.Core.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Flights.Core.ViewModels
{
    public class MainPageEntryViewModel : MvxViewModel
    {
        private readonly IHttpService _httpService;
        private readonly IJsonConverter _jsonConverter;
        private readonly IFileStore _fileStore;
        private AutoCompleteTextViewHelper _itemsCountriesFrom;
        private AutoCompleteTextViewHelper _itemsCountriesTo;
        private AutoCompleteTextViewHelper _itemsCitiesFrom;
        private AutoCompleteTextViewHelper _itemsCitiesTo;
        private bool _isAirportFromExist;
        private bool _isAirportToExist;
        private bool _isDataAboutFlightExist;
        private bool _isCheckedOneWay;
        private bool _isCheckedReturn;
        private bool _isEnabledDateReturn;
        private string _selectedCountryFrom;
        private string _selectedCountryTo;
        private string _selectedCityFrom;
        private string _selectedCityTo;
        private DateTime _dateOneWay;
        private DateTime _dateReturn;
        private DataOfFlights _dataOfFlights;
        /// <summary>
        /// Properties to binding View and ViewModel
        /// </summary>
        public bool IsAirportFromExist
        {
            get { return _isAirportFromExist; }
            set
            {
                _isAirportFromExist = value;
                RaisePropertyChanged(() => IsAirportFromExist);
            }
        }

        public bool IsAirportToExist
        {
            get { return _isAirportToExist; }
            set
            {
                _isAirportToExist = value;
                RaisePropertyChanged(() => IsAirportToExist);
            }
        }

        public bool IsDataAboutFlightExist
        {
            get { return _isDataAboutFlightExist; }
            set
            {
                _isDataAboutFlightExist = value;
                RaisePropertyChanged(() => IsDataAboutFlightExist);
            }
        }

        public bool IsCheckedOneWay
        {
            get { return _isCheckedOneWay; }
            set
            {
                _isCheckedOneWay = value;
                RaisePropertyChanged(() => IsCheckedOneWay);
            }
        }

        public bool IsCheckedReturn
        {
            get { return _isCheckedReturn; }
            set
            {
                _isCheckedReturn = value;
                RaisePropertyChanged(() => IsCheckedReturn);
            }
        }

        public bool IsEnabledDateReturn
        {
            get { return _isEnabledDateReturn; }
            set
            {
                _isEnabledDateReturn = value;
                RaisePropertyChanged(() => IsEnabledDateReturn);
            }
        }

        public string SelectedCountryFrom
        {
            get { return _selectedCountryFrom; }
            set
            {
                _selectedCountryFrom = value;
                SetCitiesFromAsync();
                RaisePropertyChanged(() => SelectedCountryFrom);
            }
        }

        public string SelectedCountryTo
        {
            get { return _selectedCountryTo; }
            set
            {
                _selectedCountryTo = value;
                SetCitiesToAsync();
                RaisePropertyChanged(() => SelectedCountryTo);
            }
        }

        public string SelectedCityFrom
        {
            get { return _selectedCityFrom; }
            set
            {
                _selectedCityFrom = value;
                SetIatasFromAsync();
                RaisePropertyChanged(() => SelectedCityFrom);
            }
        }

        public string SelectedCityTo
        {
            get { return _selectedCityTo; }
            set
            {
                _selectedCityTo = value;
                SetIatasToAsync();
                RaisePropertyChanged(() => SelectedCityTo);
            }
        }

        public AutoCompleteTextViewHelper ItemsCountriesFrom
        {
            get { return _itemsCountriesFrom; }
            set
            {
                _itemsCountriesFrom = value;
                RaisePropertyChanged(() => ItemsCountriesFrom);
            }
        }

        public AutoCompleteTextViewHelper ItemsCountriesTo
        {
            get { return _itemsCountriesTo; }
            set
            {
                _itemsCountriesTo = value;
                RaisePropertyChanged(() => ItemsCountriesTo);
            }
        }

        public AutoCompleteTextViewHelper ItemsCitiesFrom
        {
            get { return _itemsCitiesFrom; }
            set
            {
                _itemsCitiesFrom = value;
                RaisePropertyChanged(() => ItemsCitiesFrom);
            }
        }

        public AutoCompleteTextViewHelper ItemsCitiesTo
        {
            get { return _itemsCitiesTo; }
            set
            {
                _itemsCitiesTo = value;
                RaisePropertyChanged(() => ItemsCitiesTo);
            }
        }

        public DateTime DateOneWay
        {
            get { return _dateOneWay; }
            set
            {
                _dateOneWay = value;
                RaisePropertyChanged(() => DateOneWay);
            }
        }

        public DateTime DateReturn
        {
            get { return _dateReturn; }
            set
            {
                _dateReturn = value;
                RaisePropertyChanged(() => DateReturn);
            }
        }
        /// <summary>
        /// Initialization of commands
        /// </summary>
        public ICommand FindFlightsCommand { get; set; }
        public ICommand SetOneWayCommand { get; set; }
        public ICommand SetReturnCommand { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpService">Parameter to work with http service</param>
        /// <param name="jsonConverter">Parameter to work with json converter</param>
        /// <param name="fileStore">Parameter to work with files</param>
        public MainPageEntryViewModel(IHttpService httpService,
            IJsonConverter jsonConverter, 
            IFileStore fileStore)
        {
            _httpService = httpService;
            _jsonConverter = jsonConverter;
            _fileStore = fileStore;
            _dataOfFlights = new DataOfFlights();
            _itemsCountriesFrom = new AutoCompleteTextViewHelper();
            _itemsCountriesTo = new AutoCompleteTextViewHelper();
            _itemsCitiesFrom = new AutoCompleteTextViewHelper();
            _itemsCitiesTo = new AutoCompleteTextViewHelper();

            FindFlightsCommand = new MvxCommand(FindFlights);
            SetOneWayCommand = new MvxCommand(SetOneWay);
            SetReturnCommand = new MvxCommand(SetReturn);
            
            Initialization();
        }
        /// <summary>
        /// Initialization of ViewModel if some parameters are come
        /// </summary>
        /// <param name="arg"></param>
        public void Init(Favorite arg)
        {
                         
        }
        /// <summary>
        /// Initialization
        /// </summary>
        public void Initialization()
        {
            var countries = new Countries();
            ItemsCountriesFrom.AutoCompleteList = countries.GetCountries();
            ItemsCountriesTo.AutoCompleteList = countries.GetCountries();
            IsAirportFromExist = false;
            IsAirportToExist = false;
            IsDataAboutFlightExist = false;
            IsCheckedOneWay = true;
            IsEnabledDateReturn = false;
            DateOneWay = DateTime.Now;
            DateReturn = DateTime.Now;
        }
        /// <summary>
        /// Implementation of commands
        /// </summary>
        private void FindFlights()
        {
            _dataOfFlights.DateOneWay = DateOneWay.ToString("yyyy-MM-dd");
            _dataOfFlights.DateReturn = DateReturn.ToString("yyyy-MM-dd");
            var param = _jsonConverter.Serialize(_dataOfFlights);
            ShowViewModel<FlightsListViewModel>(new { param });
        }

        private void SetOneWay()
        {
            IsCheckedOneWay = true;
            IsCheckedReturn = false;
            IsEnabledDateReturn = false;
            _dataOfFlights.ReturnWay = false;
        }

        private void SetReturn()
        {
            IsCheckedOneWay = false;
            IsCheckedReturn = true;
            IsEnabledDateReturn = true;
            _dataOfFlights.ReturnWay = true;
        }
        /// <summary>
        /// The methods witch allow to get cities by specific country
        /// </summary>
        /// <returns>Asynchronous result</returns>
        private async Task SetCitiesFromAsync()
        {
            var citiesService = new CitiesService(_httpService, _jsonConverter);
            _dataOfFlights.CountryFrom = SelectedCountryFrom;
            var citiesFrom = await citiesService.GetCitiesAsync(SelectedCountryFrom);
            if (citiesFrom != null)
            {
                ItemsCitiesFrom.AutoCompleteList.Clear();
                ItemsCitiesFrom.AutoCompleteList = citiesFrom;
                IsAirportFromExist = true;
                _dataOfFlights.CitiesFrom = citiesFrom;
                RaisePropertyChanged(() => ItemsCitiesFrom);
            }
            else
            {
                IsAirportFromExist = false;
                _dataOfFlights.CitiesFrom = null;
            }
        }

        private async Task SetCitiesToAsync()
        {
            var citiesService = new CitiesService(_httpService, _jsonConverter);
            _dataOfFlights.CountryTo = SelectedCountryTo;
            var citiesTo = await citiesService.GetCitiesAsync(SelectedCountryTo);
            if (citiesTo != null)
            {
                ItemsCitiesTo.AutoCompleteList.Clear();
                ItemsCitiesTo.AutoCompleteList = citiesTo;
                IsAirportToExist = true;
                _dataOfFlights.CitiesTo = citiesTo;
                RaisePropertyChanged(() => ItemsCitiesTo);
            }
            else
            {
                IsAirportToExist = false;
                _dataOfFlights.CitiesTo = null;
            }
        }
        /// <summary>
        /// The methods witch allow to get iatas by specific country
        /// </summary>
        /// <returns>Asynchronous result</returns>
        private async Task SetIatasFromAsync()
        {
            var iataService = new IataService(_httpService, _jsonConverter);
            _dataOfFlights.CityFrom = SelectedCityFrom;
            var iatasFrom = await iataService.GetIataAsync(SelectedCityFrom);
            _dataOfFlights.IatasFrom = iatasFrom;
            IsDataAboutFlightExist = (IsDataAboutFlightExistCheck()) ? true : false;
        }

        private async Task SetIatasToAsync()
        {
            var iataService = new IataService(_httpService, _jsonConverter);
            _dataOfFlights.CityTo = SelectedCityTo;
            var iatasTo = await iataService.GetIataAsync(SelectedCityTo);
            _dataOfFlights.IatasTo = iatasTo;
            IsDataAboutFlightExist = (IsDataAboutFlightExistCheck()) ? true : false;
        }
        /// <summary>
        /// Check if data about flight exist
        /// </summary>
        /// <returns>Exist or not exist</returns>
        private bool IsDataAboutFlightExistCheck()
        {
            return IsAirportFromExist == true && IsAirportToExist == true &&
                _dataOfFlights.IatasTo != null && _dataOfFlights.IatasFrom != null;
        }
    }
}
