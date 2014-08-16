using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling.Domain.Abstract;
using StructureMap.Configuration.DSL;
using StructureMap.Web;
namespace Bowling.Domain.Concrete.EntityFramework
{
   /// <summary>
   /// StructureMap registry that configures StructureMap with implementations provided by the Entity Framework Repository project.
   /// </summary>
   public class EFRepositoryRegistry : Registry
   {
      /// <summary>
      /// Constructor creating the registry using the function passed in to get the connection string
      /// </summary>
      /// <param name="getConnectionString">Function returning the connection string</param>
      public EFRepositoryRegistry(Func<string> getConnectionString)
      {

         For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>();
         For(typeof(IRepository<>)).Use(typeof(Repository<>));
         //For(typeof(IUnitOfWork)).Use(typeof(UnitOfWork));

         //Pass the InsightDB connection string text in rather than using letting EntityFramework used the named version 
         //as it overrides the caching provider with the System.Data.SqlClient provider
         For<DataContext>().HybridHttpOrThreadLocalScoped().Use<DataContext>()
            .Ctor<string>("connectionString").Is(getConnectionString());

         //For<DataContext>().HybridHttpOrThreadLocalScoped().Use(o => new DataContext("InsightDB", false));
      }
   }
}
