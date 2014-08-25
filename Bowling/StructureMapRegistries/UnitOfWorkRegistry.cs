using Bowling.Domain.Abstract;
using Bowling.Domain.Concrete.EntityFramework;
using Bowling.Domain.Entities;
using StructureMap;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
namespace StructureMapRegistries
{
   /// <summary>
   /// A StructureMap registry that is used to configure the named and default mappings between the IBusinessTransaction
   /// interface and the appropriate business transaction classes.
   /// </summary>
   public class UnitOfWorkRegistry : Registry
   {
      /// <summary>
      /// Constructor to create the registry
      /// </summary>
      public UnitOfWorkRegistry()
      {
          For<IUnitOfWorkFactory>().Singleton().Use<EFUnitOfWorkFactory>();

          //For<IUnitOfWorkFactoryProvider>().Use<UnitOfWorkFactoryProvider>()
          //                  .Ctor<Func<IEnumerable<IUnitOfWorkFactory>>>("providerFactory")
          //                  .Is(() => new IUnitOfWorkFactory[] { ObjectFactory.GetInstance<IUnitOfWorkFactory>() });


          For<IUnitOfWork>().Use<EFUnitOfWork>();
      }
   }
}
