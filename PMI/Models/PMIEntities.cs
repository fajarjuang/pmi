using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMI.Models
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class pmiEntities : DbContext
    {
        public override int SaveChanges()
        {
            SavePostCreatedTime();
            SavePostUpdatedTime();
            return base.SaveChanges();
        }

        private void SavePostCreatedTime()
        {
            DateTime now = DateTime.Now;
            foreach (var entity in ChangeTracker.Entries<Post>().Where(e => e.State == EntityState.Added).Select(e => e.Entity))
            {
                entity.created = now;
                entity.updated = now;
            }
        }

        private void SavePostUpdatedTime()
        {
            DateTime now = DateTime.Now;
            foreach (var entity in ChangeTracker.Entries<Post>().Where(e => e.State == EntityState.Modified).Select(e => e.Entity))
            {
                entity.updated = now;
            }
        }
    }
}