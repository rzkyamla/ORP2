using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Repositories.Interfaces
{
    public interface IRepository<Entity, Id> where Entity : class
    {
        IEnumerable<Entity> Get();
        Entity Get(Id id);
        int Create(Entity entity);
        int Update(Entity entity);
        int Delete(Id id);
    }
}
