//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Bowling.Domain.Abstract;
//using StructureMap;

//namespace Bowling.Domain.Concrete.EntityFramework
//{
//   public class EFUnitOfWorkFactory : IUnitOfWorkFactory
//   {
//      public IUnitOfWork Create()
//      {
//         Func<DataContext> dataContextFunc = () => ObjectFactory.GetInstance<DataContext>();


//         return new EFUnitOfWork(dataContextFunc);
//      }
//   }
//}
