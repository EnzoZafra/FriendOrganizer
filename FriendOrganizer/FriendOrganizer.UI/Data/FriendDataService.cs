using FriendOrganizer.Model;
using System.Collections.Generic;

namespace FriendOrganizer.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        public IEnumerable<Friend> GetAll()
        {
            // Todo: Load data from db
            yield return new Friend { FirstName = "Lorenzo", LastName = "Zafra" };
            yield return new Friend { FirstName = "Andreas", LastName = "Huber" };
            yield return new Friend { FirstName = "Julia", LastName = "Boehler" };
            yield return new Friend { FirstName = "Thomas", LastName = "Huber" };
        }
    }
}
