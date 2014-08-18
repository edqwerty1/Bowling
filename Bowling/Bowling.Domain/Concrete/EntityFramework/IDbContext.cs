using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Bowling.Domain.Concrete.EntityFramework
{
   /// <summary>
   /// Interface for database contexts
   /// </summary>
   public interface IDbContext : IDisposable
   {
      /// <summary>
      /// Save the changes to all entities within the current context
      /// </summary>
      /// <returns>Error code</returns>
      int SaveChanges();

      /// <summary>
      /// Returns a DbSet for the current entity type
      /// </summary>
      /// <typeparam name="TEntity">The entity type</typeparam>
      /// <returns>DbSet</returns>
      DbSet<TEntity> Set<TEntity>() where TEntity : class;

      /// <summary>
      /// This refreshes the contents of a given object from the database.
      /// </summary>
      /// <param name="entity">Object to be refreshed</param>
      void Refresh(object entity);

      /// <summary>
      /// Gets a System.Data.Entity.Infrastructure.DbEntityEntry object for the given entity
      /// </summary>
      /// <param name="entity">Entity to use</param>
      /// <returns>DbEntityEntry</returns>
      DbEntityEntry Entry(object entity);
   }
}
