using Acr.UserDialogs;
using Flights.Core.ViewModels;
using Flights.Infrastructure.Interfaces;
using Flights.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;

namespace Flights.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterType<IHttpService, HttpService>();
            Mvx.RegisterType<IJsonConverter, JsonConverter>();
            Mvx.RegisterType<IFileStore, FileStore>();
            Mvx.RegisterType<ICitiesService, CitiesService>();
            Mvx.RegisterType<IIataService, IataService>();

            Mvx.RegisterSingleton(() => UserDialogs.Instance);

            RegisterAppStart<MainPageViewModel>();
        }
    }
}
