using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Flights.Core.Helpers
{
    public class AutoCompleteTextViewHelper : INotifyPropertyChanged
    {
        public ObservableCollection<string> AutoCompleteList;

        public AutoCompleteTextViewHelper()
        {
            AutoCompleteList = new ObservableCollection<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _currentText;
        public string CurrentText
        {
            get { return _currentText; }
            set
            {
                _currentText = value;
                RaisePropertyChanged("CurrentText");
            }
        }

        private string _currentTextHint;
        public string CurrentTextHint
        {
            get { return _currentTextHint; }
            set { Filter(value); }
        }

        private void SetSuggestionsEmpty()
        {
            AutoCompleteSuggestions = new List<string>();
        }

        private List<string> _autoCompleteSuggestions = new List<string>();
        public List<string> AutoCompleteSuggestions
        {
            get
            {
                if (_autoCompleteSuggestions == null)
                {
                    _autoCompleteSuggestions = new List<string>();
                }
                return _autoCompleteSuggestions;
            }
            set
            {
                _autoCompleteSuggestions = value;
                RaisePropertyChanged("AutoCompleteSuggestions");
            }
        }

        private void Filter(string value)
        {
            if (value == "")
            {
                _currentTextHint = null;
                SetSuggestionsEmpty();
                return;
            }
            else
            {
                _currentTextHint = value;
            }

            if (_currentTextHint.Trim().Length < 2)
            {
                SetSuggestionsEmpty();
                return;
            }

            var list = AutoCompleteList.Where(i => (i ?? "").ToUpper().Contains(_currentTextHint.ToUpper()));
            if (list.Count() > 0)
            {
                AutoCompleteSuggestions = list.ToList();
            }
            else
            {
                SetSuggestionsEmpty();
            }
        }
    }
}
