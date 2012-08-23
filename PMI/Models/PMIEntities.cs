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
    using System.Data.Entity.Validation;

    public partial class pmiEntities : DbContext
    {
        public override int SaveChanges()
        {
            SavePostCreatedTime();
            SavePostUpdatedTime();
            return base.SaveChanges();
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var result = new DbEntityValidationResult(entityEntry, new List<DbValidationError>());

            if (entityEntry.Entity is Post && entityEntry.State == EntityState.Modified)
            {
                Post post = entityEntry.Entity as Post;
                Guid writer = Posts.Where(p => p.writer == post.writer && p.id == post.id).Select(p => p.writer).SingleOrDefault();
                if (writer != post.writer)
                    result.ValidationErrors.Add(new DbValidationError("global", "Penulis post harus sama dengan editor post."));
            }

            if (result.ValidationErrors.Count > 0)
            {
                return result;
            }
            else
            {
                return base.ValidateEntity(entityEntry, items);
            }
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