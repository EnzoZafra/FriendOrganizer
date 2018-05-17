namespace FriendOrganizer.DataAccess.Migrations
{
    using FriendOrganizer.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizer.DataAccess.FriendOrganizerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendOrganizer.DataAccess.FriendOrganizerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Friends.AddOrUpdate(
                f => f.FirstName,
                    new Friend { FirstName = "Lorenzo", LastName = "Zafra" },
                    new Friend { FirstName = "Andreas", LastName = "Huber" },
                    new Friend { FirstName = "Julia", LastName = "Boehler" },
                    new Friend { FirstName = "Thomas", LastName = "Huber" }
                );

            context.ProgrammingLanguages.AddOrUpdate(
                pl => pl.Name,
                new ProgrammingLanguage { Name = "C#" },
                new ProgrammingLanguage { Name = "Java" },
                new ProgrammingLanguage { Name = "Python" },
                new ProgrammingLanguage { Name = "C" },
                new ProgrammingLanguage { Name = "Swift" }
                );

            context.SaveChanges();

            context.FriendPhoneNumbers.AddOrUpdate(pn => pn.Number,
                new FriendPhoneNumber { Number = "780-554-4226", FriendId = context.Friends.FirstOrDefault().Id });

            context.Meetings.AddOrUpdate(m => m.Title,
                new Meeting
                {
                    Title = "Watching Soccer",
                    DateFrom = new DateTime(2018, 5, 26),
                    DateTo = new DateTime(2018, 5, 26),
                    Friends = new List<Friend>
                    {
                        context.Friends.Single(f => f.FirstName == "Enzo" && f.LastName == "Zafra"),
                        context.Friends.Single(f => f.FirstName == "Thomas" && f.LastName == "Huber")
                    }
                });
        }
    }
}
