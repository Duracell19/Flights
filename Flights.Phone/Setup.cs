using MvvmCross.Core.ViewModels;
using MvvmCross.WindowsCommon.Platform;
using Windows.UI.Xaml.Controls;

namespace Flights.Phone
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
    }
}
