﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bowling.Domain.Abstract;
using Bowling.Domain.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
namespace Bowling.Domain.Concrete.EntityFramework
{
   /// <summary>
   /// Base generic repository implementation
   /// </summary>
   /// <typeparam name="T">Type of the repository</typeparam>
   public class EFRepository<T> : IRepository<T> where T : BaseEntity
   {
      private IDbContext context;
      private DbSet<T> dbSet;

      /// <summary>
      /// Costructor
      /// </summary>
      public EFRepository()
      {

      }

      /// <summary>
      /// The context on wich the repository is based.
      /// </summary>
      /// <returns>Context</returns>
      protected IDbContext Context
      {
         get
         {
            if (context == null)
            {
               context = GetCurrentUnitOfWork<EFUnitOfWork>().ContextFor<T>();
            }

            return context;
         }
      }

      /// <summary>
      /// The dbSet object retrieved from the context
      /// </summary>
      /// <returns>DbSet</returns>
      protected DbSet<T> DbSet
      {
         get
         {
            if (dbSet == null)
            {
               dbSet = this.Context.Set<T>();
            }
            return dbSet;
         }
      }

      /// <summary>
      /// Get the current Unit of Work
      /// </summary>
      /// <typeparam name="TUnitOfWork">The type of the unit of work</typeparam>
      /// <returns>Current unit of work</returns>
      public TUnitOfWork GetCurrentUnitOfWork<TUnitOfWork>() where TUnitOfWork : IUnitOfWork
      {
         return (TUnitOfWork)UnitOfWork.Current;
      }

      /// <summary>
      /// Get all data from the dbSet.  This takes into account the valid partitions for the user
      /// </summary>
      /// <returns>IQueriable containing all data</returns>
      public IQueryable<T> GetAll()
      {
         return this.DbSet;
      }

      /// <summary>
      /// Get all data from the dbSet.  This takes into account the valid partitions for the user
      /// </summary>
      /// <returns>IQueriable containing all data</returns>
      public ICollection<T> GetAllLocal()
      {
         return this.DbSet.Local;
      }

      /// <summary>
      /// Retrieves all items in the repository satisfied by the specified query asynchronously.
      /// </summary>
      /// <param name="queryShaper">The <see cref="Func{T1,TResult}">function</see> that shapes the <see cref="IQueryable{T}">query</see> to execute.</param>
      /// <param name="cancellationToken">The <see cref="CancellationToken">cancellation token</see> that can be used to cancel the operation.</param>
      /// <returns>A <see cref="Task{T}">task</see> containing the retrieved <see cref="IEnumerable{T}">sequence</see> of <typeparamref name="T">items</typeparamref>.</returns>
      [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
      public async Task<IEnumerable<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>> queryShaper, CancellationToken cancellationToken)
      {
          var query = queryShaper(this.DbSet);
          return await query.ToArrayAsync(cancellationToken);
      }

      /// <summary>
      /// Retrieves a query result asynchronously.
      /// </summary>
      /// <typeparam name="TResult">The <see cref="Type">type</see> of result to retrieve.</typeparam>
      /// <param name="queryShaper">The <see cref="Func{T,TResult}">function</see> that shapes the <see cref="IQueryable{T}">query</see> to execute.</param>
      /// <param name="cancellationToken">The <see cref="CancellationToken">cancellation token</see> that can be used to cancel the operation.</param>
      /// <returns>A <see cref="Task{T}">task</see> containing the <typeparamref name="TResult">result</typeparamref> of the operation.</returns>
      [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
      public async Task<TResult> GetAsync<TResult>(Func<IQueryable<T>, TResult> queryShaper, CancellationToken cancellationToken)
      {
          var set = this.DbSet;
          var query = queryShaper;
          var factory = Task<TResult>.Factory;
          return await factory.StartNew(() => query(set), cancellationToken);
      }


      /// <summary>
      /// Search the dbSet with the given expression.  This takes into account valid partitions
      /// for the user
      /// </summary>
      /// <param name="where">Expression used to search the data</param>
      /// <returns>IQueryable containing result data</returns>
      public IQueryable<T> Search(Expression<Func<T, bool>> where)
      {
         return this.DbSet.Where<T>(where);
      }

      /// <summary>
      /// Get a single entity from the dbSet defined by the given where clause.  This takes
      /// into account valid partitions for the user
      /// </summary>
      /// <param name="where">Expression used to search the data</param>
      /// <returns>IQueryable containing result data</returns>
      public T Single(Expression<Func<T, bool>> where)
      {
         return this.DbSet.SingleOrDefault<T>(where);
      }

      /// <summary>
      /// Get a single entity from the dbSet defined by the given query.  This takes
      /// into account valid partitions for the user
      /// </summary>
      /// <param name="query">Query used to search the data</param>
      /// <returns>IQueryable containing result data</returns>
      public T Single(Func<IQueryable<T>, IQueryable<T>> query)
      {
         return query(this.DbSet).SingleOrDefault();
      }

      /// <summary>
      /// Get entity by the id
      /// </summary>
      /// <param name="id">Guid of the entity</param>
      /// <returns>Result entity</returns>
      public T GetById(int id)
      {
         return this.DbSet.SingleOrDefault(t => t.Id == id);
      }

      /// <summary>
      /// Get the first entity from the dbSet defined by the given where clause.  This takes
      /// into account valid partitions for the user
      /// </summary>
      /// <param name="where">Expression used to search the data</param>
      /// <returns>IQueryable containing result data</returns>
      public T First(Expression<Func<T, bool>> where)
      {
         return this.DbSet.FirstOrDefault(where);
      }

      /// <summary>
      /// Get the first entity from the dbSet defined by the given query.  This takes
      /// into account valid partitions for the user
      /// </summary>
      /// <param name="query">Query used to search the data</param>
      /// <returns>IQueryable containing result data</returns>
      public T First(Func<IQueryable<T>, IQueryable<T>> query)
      {
         return query(this.DbSet).FirstOrDefault();
      }

      /// <summary>
      /// Delete the given entity from the data set
      /// </summary>
      /// <param name="entity">Entity to be deleted</param>
      public virtual void Delete(T entity)
      {
         //entity.SetDataArea();
         //if (entity.DataArea == null)
         //   entity.DataArea = GetDataAreas().First();
         this.DbSet.Remove(entity);
      }

      /// <summary>
      /// Add the given entity to the data set
      /// </summary>
      /// <param name="entity">Entity to be added</param>
      public virtual void Add(T entity)
      {
         //entity.SetDataArea();
         //if (entity.DataArea == null)
         //entity.DataArea = GetDataAreas().First();
         this.DbSet.Add(entity);
      }

      /// <summary>
      /// Add a range of entities to the data set
      /// </summary>
      /// <param name="entities">Range of entities to be added</param>
      public virtual void AddRange(IEnumerable<T> entities)
      {
         //var dataArea = GetDataAreas();
         foreach (var entity in entities)
         {
            //entity.SetDataArea();
            //if (entity.DataArea == null)
            //   entity.DataArea = dataArea.First();
         }
         this.DbSet.AddRange(entities);
      }

      /// <summary>
      /// Attaches the given entity to the context underlying the set. That is, the entity 
      /// is placed into the context in the Unchanged state, just as if it had been read from the database. 
      /// </summary>
      /// <param name="entity">The entity to be attached</param>
      public void Attach(T entity)
      {
         T oldEntity = this.DbSet.Local.FirstOrDefault(e => e.Id == entity.Id);

         if (oldEntity != null)
         {
            this.Context.Entry(oldEntity).CurrentValues.SetValues(entity);
         }
         else
         {
            this.DbSet.Attach(entity);
            this.Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
         }
      }

      /// <summary>
      /// Lock the given entity in the data set
      /// </summary>
      /// <param name="entity">The entity to be locked</param>
      public void Lock(T entity)
      {
         Type enttype = typeof(T);
         int id = (int)enttype.GetProperty("Id").GetValue(entity, null);

         //ToDo Guid will now always be available, so how do we know if the entity has been written or not?
         //if the id is 0, then it is not on the database as of yet and does not have to be locked
         //if (id == 0)
         //{
         //   return;
         //}

         //if the unit of work already locked the value then return - No need to relock.
         if (UnitOfWork.Current.ContainsLockKey(enttype, id) == true)
         {
            return;
         }

         Lock(id, typeof(T).Name, enttype);

         Refresh(entity);
      }

      /// <summary>
      /// This obtains an update lock on a given row by ID, or waits until it can get one (or times out).
      /// </summary>
      /// <param name="id">Id of the entity to be locked</param>
      /// <param name="table">Table in which the entity is stored</param>
      /// <param name="entType">Type of the entity</param>
      public void Lock(int id, string table, Type entType)
      {
         Context.Lock(id, table, entType);
      }

      /// <summary>
      /// This refreshes the contents of a given object from the database.
      /// </summary>
      /// <param name="entity">The entity to be refreshed</param>
      public void Refresh(T entity)
      {
         Context.Refresh(entity);
      }

   }

}
