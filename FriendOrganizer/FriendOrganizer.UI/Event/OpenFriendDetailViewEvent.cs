using System;
using Prism.Events;

namespace FriendOrganizer.UI.Event
{
    public class OpenFriendDetailViewEvent : PubSubEvent<int>
    {
        internal void Subscribe(object onOpenFriendDetailView)
        {
            throw new NotImplementedException();
        }
    }
}
