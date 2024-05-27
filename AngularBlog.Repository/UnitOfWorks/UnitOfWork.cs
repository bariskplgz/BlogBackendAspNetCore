using AngularBlog.API;
using AngularBlog.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularBlog.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AngularBlogDbContext _context;

        public UnitOfWork(AngularBlogDbContext context)
        {
            _context = context;
        }
        public void Commit() 
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync() 
        {
            await _context.SaveChangesAsync();
        }
    }
}
