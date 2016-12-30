using Flights.Models;
using MvvmCross.Core.ViewModels;

namespace Flights.Core.ViewModels
{
    public class FlightsInfoViewModel : MvxViewModel
    {
        private FlyInfoShow infoFlyList;

        public FlyInfoShow InfoFlyList
        {
            get { return infoFlyList; }
        }

        public void Init(FlyInfoShow flightsItem)
        {
            infoFlyList = flightsItem;
        }
    }
}
