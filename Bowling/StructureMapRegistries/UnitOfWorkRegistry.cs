using Bowling.Domain.Abstract;
using Bowling.Domain.Concrete.EntityFramework;
using StructureMap.Configuration.DSL;

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
       //  For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>();
        // For(typeof(IUnitOfWork<>)).Use();
      }
   }
}
