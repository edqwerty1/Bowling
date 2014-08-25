using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling.Domain.Abstract;
using StructureMap;

namespace Bowling.Domain.Concrete.EntityFramework
{
   public class EFUnitOfWorkFactory : IUnitOfWorkFactory
   {
       public EFUnitOfWorkFactory()
       {

       }
      public IUnitOfWork Create()
      {
         Func<IDbContext> dataContextFunc = () => new DataContext();


         return new EFUnitOfWork(dataContextFunc);
      }
   }
}
