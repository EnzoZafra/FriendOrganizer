using Autofac;
using FriendOrganizer.DataAccess;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.View.Services;
using FriendOrganizer.UI.ViewModel;
using Prism.Events;

namespace FriendOrganizer.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<FriendOrganizerDbContext>().AsSelf();

            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<FriendDetailViewModel>().As<IFriendDetailViewModel>();
            // Register a FriendDataService whenever a IFriendDataService is required
            builder.RegisterType<FriendRepository>().As<IFriendRepository>();
            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();

            return builder.Build();
        }
    }
}
