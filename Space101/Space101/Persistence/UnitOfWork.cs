using System.Data.Entity;
using Space101.DAL;

namespace Space101.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Complete()
        {
            context.SaveChanges();
        }

        public void Modify(object obj)
        {
            context.Entry(obj).State = EntityState.Modified;
        }
    }
}