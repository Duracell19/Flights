using Flights.Models;
using MvvmCross.Core.ViewModels;

namespace Flights.Core.ViewModels
{
    public class FlightsInfoViewModel : MvxViewModel
    {
        private FlyInfoShow _infoFlyList;

        public FlyInfoShow InfoFlyList
        {
            get { return _infoFlyList; }
        }

        public void Init(FlyInfoShow flightsItem)
        {
            _infoFlyList = flightsItem;
        }
    }
}
