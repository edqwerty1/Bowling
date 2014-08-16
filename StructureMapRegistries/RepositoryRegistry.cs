using System;
using System.Configuration;
using Bowling.Domain.Abstract;
using Bowling.Domain.Concrete.EntityFramework;
using StructureMap;
using StructureMap.Configuration.DSL;


namespace StructureMapRegistries
{
   /// <summary>
   /// A StructureMap registry that is used to configure the named and default mappings between the IRepository
   /// interface and the appropriate repository classes.
   /// </summary>
   public class RepositoryRegistry : Registry
   {
      /// <summary>
      /// Constructor to create the registry
      /// </summary>
      public RepositoryRegistry()
      {
         For(typeof(IRepository<>)).Use(typeof(EFRepository<>));

      }
   }
}
