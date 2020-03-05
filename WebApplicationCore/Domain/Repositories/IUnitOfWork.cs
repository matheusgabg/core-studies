using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCore.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }

}
