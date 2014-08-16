using StructureMap.Configuration.DSL;
using StructureMap.Web;
using Bowling.Services;
using Bowling.Services.Implementation;

namespace StructureMapRegistries
{
   /// <summary>
   /// A StructureMap registry that is used to configure the named and default mappings between the IBusinessTransaction
   /// interface and the appropriate business transaction classes.
   /// </summary>
   public class ServiceRegistry : Registry
   {
      /// <summary>
      /// Constructor to create the registry
      /// </summary>
      public ServiceRegistry()
      {
         For<IBowlerService>().HybridHttpOrThreadLocalScoped().Use<BowlerService>();
      }
   }
}
