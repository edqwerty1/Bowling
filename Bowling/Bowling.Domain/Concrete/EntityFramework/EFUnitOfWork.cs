using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling.Domain.Abstract;

namespace Bowling.Domain.Concrete.EntityFramework
{
   /// <summary>
   /// Entity framework unit of work.  A set of changes to be committed at one time
   /// </summary>
   public sealed class EFUnitOfWork : IUnitOfWork, IDisposable
   {
      /// <summary>
      /// The context being used
      /// </summary>
      /// <returns>context</returns>
      public IDbContext Context { get; private set; }

      private Func<IDbContext> dataContextFunc;
      private IDbContext dataContext = null;
      /// <summary>
      /// The database context being used
      /// </summary>
      /// <returns>Data context</returns>
      public IDbContext DataContext
      {
         get
         {
            if (dataContext == null)
            {
               dataContext = dataContextFunc();
            }
            return dataContext;
         }
      }

      private List<KeyValuePair<Type, int>> lockTable;
      /// <summary>
      /// Constructor creating the unit of work from functions defining the
      /// various contexts
      /// </summary>
      /// <param name="dataContextFunc">Function returning the data context</param>
      public EFUnitOfWork(Func<IDbContext> dataContextFunc)
      {
         this.dataContextFunc = dataContextFunc;
         lockTable = new List<KeyValuePair<Type, int>>();
      }

      /// <summary>
      /// Return the correct context for the given type parameter
      /// </summary>
      /// <typeparam name="T">Type</typeparam>
      /// <returns>Context</returns>
      public IDbContext ContextFor<T>()
      {
         string typeNamespace = typeof(T).Namespace;

         IDbContext context = null;

         switch (typeNamespace)
         {
            case "Bowling.Domain.Entities":
               context = DataContext;
               break;
            default:
               break;
         }
         return context;
      }

      /// <summary>
      /// Commit the unit of work to the database
      /// </summary>
      public void Commit()
      {
         try
         {

            dataContext.SaveChanges();
            lockTable = new List<KeyValuePair<Type, int>>();
            //}
         }
         catch (DbUpdateException e)
         {
            //We want to be able to catch database exceptions in the services without having to refer to entity framework assemblies
            throw new Exception("See InnerException for details", e);
         }
      }


      /// <summary>
      /// Commit the unit of work to the database
      /// </summary>
      public Task<int> CommitAsync()
      {
          try
          {
              return dataContext.SaveChangesAsync();
          }
          catch (DbUpdateException e)
          {
              //We want to be able to catch database exceptions in the services without having to refer to entity framework assemblies
              throw new Exception("See InnerException for details", e);
          }
      }


      /// <summary>
      /// Dispose the unit of work and associated contexts
      /// </summary>
      public void Dispose()
      {
         if (dataContext != null)
         {
            dataContext.Dispose();
            dataContext = null;
         }

         GC.SuppressFinalize(this);
         lockTable = null;
      }

      /// <summary>
      /// Check if there is a lock inthe lock table for the Type, id pair
      /// </summary>
      /// <param name="entity">Type</param>
      /// <param name="i">Id</param>
      /// <returns>Lock found?</returns>
      public bool ContainsLockKey(Type entity, int i)
      {
         KeyValuePair<Type, int> newKVP = new KeyValuePair<Type, int>(entity, i);
         if (lockTable.Contains(newKVP) == true)
         {
            return true;
         }
         else
         {
            return false;
         }
      }

      /// <summary>
      /// Add the given type id pair to the lock table
      /// </summary>
      /// <param name="entity">Type</param>
      /// <param name="i">Id</param>
      public void AddToLockTable(Type entity, int i)
      {
         KeyValuePair<Type, int> newKVP = new KeyValuePair<Type, int>(entity, i);
         lockTable.Add(newKVP);
      }
   }
}
