using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private IFriendRepository _friendRepository;
        private IEventAggregator _eventAggregator;
        private FriendWrapper _friend;
        private bool _hasChanges;

        public FriendDetailViewModel(IFriendRepository friendRepository, IEventAggregator eventAggregator)
        {
            _friendRepository = friendRepository;
            _eventAggregator = eventAggregator;
            // Subscribe to event, pass in event handler

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        public async Task LoadAsync(int friendId)
        {
            var friend = await _friendRepository.GetByIdAsync(friendId);

            Friend = new FriendWrapper(friend);

            Friend.PropertyChanged += Friend_PropertyChanged;
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void Friend_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _friendRepository.HasChanges();
            }

            if(e.PropertyName == nameof(Friend.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public FriendWrapper Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }


        public ICommand SaveCommand { get; }

        private async void OnSaveExecute()
        {
            await _friendRepository.SaveAsync();
            HasChanges = _friendRepository.HasChanges();
            _eventAggregator.GetEvent<AfterFriendSavedEvent>().Publish(
                new AfterFriendSavedEventArgs
                {
                    Id = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                });
        }

        private bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && HasChanges;
        }
    }
}
