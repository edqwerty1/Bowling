using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bowling.Domain.Abstract
{
   /// <summary>
   /// Interface for all repository iplementations
   /// </summary>
   /// <typeparam name="T">Type of the entities in the repository</typeparam>
   public interface IRepository<T> where T : class
   {
      /// <summary>
      /// Get all items from the repository  
      /// </summary>
      /// <returns>Queryable containing all items in the repository</returns>
      IQueryable<T> GetAll();

      ICollection<T> GetAllLocal();

      /// <summary>
      /// Retrieves all items in the repository satisfied by the specified query asynchronously.
      /// </summary>
      /// <param name="queryShaper">The <see cref="Func{T,TResult}">function</see> that shapes the <see cref="IQueryable{T}">query</see> to execute.</param>
      /// <param name="cancellationToken">The <see cref="CancellationToken">cancellation token</see> that can be used to cancel the operation.</param>
      /// <returns>A <see cref="Task{T}">task</see> containing the retrieved <see cref="IEnumerable{T}">sequence</see>
      /// of <typeparamref name="T">items</typeparamref>.</returns>
      /// <example>The following example demonstrates how to paginate the items in a repository using an example query.
      /// <code><![CDATA[
      /// using Microsoft.DesignPatterns.Examples;
      /// using System;
      /// using System.Collections.Generic;
      /// using System.Linq;
      /// using System.Threading;
      /// using System.Threading.Tasks;
      /// 
      /// public async static void Main()
      /// {
      ///     var index = 0;
      ///     var repository = new MyRepository();
      ///     var cancellationToken = new CancellationToken();
      ///     
      ///     foreach ( var item in await repository.GetAsync( q => q.Where( i => i.FirstName.StartsWith( "Jo" ) ).OrderBy( i => i.LastName ), cancellationToken );
      ///         Console.WriteLine( i => i.ToString() );
      /// }
      /// ]]>
      /// </code></example>
      [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
      Task<IEnumerable<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>> queryShaper, CancellationToken cancellationToken);

      /// <summary>
      /// Retrieves a query result asynchronously.
      /// </summary>
      /// <typeparam name="TResult">The <see cref="Type">type</see> of result to retrieve.</typeparam>
      /// <param name="queryShaper">The <see cref="Func{T,TResult}">function</see> that shapes the <see cref="IQueryable{T}">query</see> to execute.</param>
      /// <param name="cancellationToken">The <see cref="CancellationToken">cancellation token</see> that can be used to cancel the operation.</param>
      /// <returns>A <see cref="Task{T}">task</see> containing the <typeparamref name="TResult">result</typeparamref> of the operation.</returns>
      [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
      Task<TResult> GetAsync<TResult>(Func<IQueryable<T>, TResult> queryShaper, CancellationToken cancellationToken);

      /// <summary>
      /// Basic search method that allows filtering on the repository based on
      /// a boolean predicate.
      /// </summary>
      /// <param name="where">Where clause for the search</param>
      /// <returns>Queryable of the search results</returns>
      IQueryable<T> Search(Expression<Func<T, bool>> where);

      /// <summary>
      /// Search and return a single entity using a where clause
      /// </summary>
      /// <param name="where">Where clause</param>
      /// <returns>Result entity</returns>
      T Single(Expression<Func<T, bool>> where);

      /// <summary>
      /// Search and return a single entity using a query function
      /// </summary>
      /// <param name="query">Query function</param>
      /// <returns>Result entity</returns>
      T Single(Func<IQueryable<T>, IQueryable<T>> query);

      /// <summary>
      /// Get entity by the id
      /// </summary>
      /// <param name="id">Guid of the entity</param>
      /// <returns>Result entity</returns>
      T GetById(int id);

      /// <summary>
      /// Search and return the first entity that matches the given where clause
      /// </summary>
      /// <param name="where">Where clause</param>
      /// <returns>Result entity</returns>
      T First(Expression<Func<T, bool>> where);

      /// <summary>
      /// Search and return the first entity matching the given query
      /// </summary>
      /// <param name="query">Query</param>
      /// <returns>Result entity</returns>
      T First(Func<IQueryable<T>, IQueryable<T>> query);

      /// <summary>
      /// Delete the given entity from the repository
      /// </summary>
      /// <param name="entity">Entity to delete</param>
      void Delete(T entity);

      /// <summary>
      /// Add the given entity to the repository
      /// </summary>
      /// <param name="entity">Entity to add</param>
      void Add(T entity);

      /// <summary>
      /// Add a collection of new entities to the repository. This can be more efficient
      /// than adding the entities one by one.
      /// </summary>
      /// <param name="entities">Range of entities to add</param>
      void AddRange(IEnumerable<T> entities);

      /// <summary>
      /// Attach the gievn entity
      /// </summary>
      /// <param name="entity">Entity to attach</param>
      void Attach(T entity);

      /// <summary>
      /// Obtain a lock on a specific entity where possible.
      /// Also refreshes the entity after obtaining the lock
      /// </summary>
      /// <param name="entity">Entity to lock</param>
      void Lock(T entity);

      /// <summary>
      /// Lock the specific entity where possible.  Find the entity via the given id,
      /// table and type
      /// </summary>
      /// <param name="id">Id of the entity being locked</param>
      /// <param name="table">Table that the entity being locked belongs to</param>
      /// <param name="entType">Type of the entity being locked</param>
      void Lock(int id, string table, Type entType);

      /// <summary>
      /// Refresh the given entity
      /// </summary>
      /// <param name="entity">Entity to refresh</param>
      void Refresh(T entity);
   }
}
