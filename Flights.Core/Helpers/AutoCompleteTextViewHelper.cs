using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Flights.Core.Helpers
{
    /// <summary>
    /// This class helps you work with AutoCompleteTextView controls
    /// </summary>
    public class AutoCompleteTextViewHelper : INotifyPropertyChanged
    {
        /// <summary>
        /// List of items to show
        /// </summary>
        public List<string> AutoCompleteList { get; set; }

        public AutoCompleteTextViewHelper()
        {
            AutoCompleteList = new List<string>();
        }
        /// <summary>
        /// Event if property was changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// Current text in control
        /// </summary>
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
        /// <summary>
        /// Current text in control with change
        /// </summary>
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
        /// <summary>
        /// List of items
        /// </summary>
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
        /// <summary>
        /// If text in control was changed 
        /// </summary>
        /// <param name="value">Current text</param>
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
