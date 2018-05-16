using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private IFriendDetailViewModel _friendDetailViewModel;
        private Func<IFriendDetailViewModel> _friendDetailViewModelCreator;
        private IMessageDialogService _messageDialogService;

        public MainViewModel(INavigationViewModel navigationViewModel, Func<IFriendDetailViewModel> friendDetailViewModelCreator,
            IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {

            _eventAggregator = eventAggregator;
            _friendDetailViewModelCreator = friendDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
                .Subscribe(OnOpenFriendDetailView);

            NavigationViewModel = navigationViewModel;
        }

        public INavigationViewModel NavigationViewModel { get; }

        public IFriendDetailViewModel FriendDetailViewModel
        {
            get { return _friendDetailViewModel; }
            private set
            {
                _friendDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private async void OnOpenFriendDetailView(int friendId)
        {
            if (FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You have made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            FriendDetailViewModel = _friendDetailViewModelCreator();
            await FriendDetailViewModel.LoadAsync(friendId);
        }
    }
}
