using Microsoft.EntityFrameworkCore;
using ORP_API.Context;
using ORP_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Repositories
{
    public class GeneralRepository<Entity, Context, Id> : IRepository<Entity, Id>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private DbSet<Entity> entities;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        public int Create(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                entities.Add(entity);
                var result = myContext.SaveChanges();
                return result;
            }
        }

        public int Delete(Id id)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                Entity entity = entities.Find(id);
                entities.Remove(entity);
                var result = myContext.SaveChanges();
                return result;
            }
        }
        public IEnumerable<Entity> Get()
        {
            return entities.AsEnumerable();
        }

        public Entity Get(Id id)
        {
            return entities.Find(id);
        }

        public int Update(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                try
                {
                    myContext.Entry(entity).State = EntityState.Modified;
                    var result = myContext.SaveChanges();
                    return result;
                }

                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
