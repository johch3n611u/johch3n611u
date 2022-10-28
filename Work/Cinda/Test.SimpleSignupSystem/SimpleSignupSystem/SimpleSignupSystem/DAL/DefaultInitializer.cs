namespace SimpleSignupSystem.DAL
{
    using SimpleSignupSystem.Models.Entity;
    using System.Collections.Generic;

    public class DefaultInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SignupDB>
    {
        protected override void Seed(SignupDB context)
        {
            var Default_tblActiveItem = new List<tblActiveItem>
            {
               new tblActiveItem{ cItemName = "排球", cActiveDt = "AM 10:00~AM 11:00" },
               new tblActiveItem{ cItemName = "羽球", cActiveDt = "AM 11:00~PM 12:00" },
               new tblActiveItem{ cItemName = "自行車", cActiveDt = "PM 15:00~PM 16:00" },
            };

            Default_tblActiveItem.ForEach(s => context.tblActiveItem.Add(s));
            context.SaveChanges();
        }

    }
}