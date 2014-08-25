namespace Bowling.Domain.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides extension methods for the <see cref="IRepository{T}"/> and <see cref="IRepository{T}"/> interfaces.
    /// </summary>
    public static class IRepositoryExtensions
    {
        /// <summary>
        /// Retrieves all items in the repository satisfied by the specified query asynchronously.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type">type</see> of item in the repository.</typeparam>
        /// <param name="repository">The extended <see cref="IRepository{T}">repository</see>.</param>
        /// <param name="queryShaper">The <see cref="Func{T,TResult}">function</see> that shapes the <see cref="IQueryable{T}">query</see> to execute.</param>
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
        ///     
        ///     foreach ( var item in await repository.GetAsync( q => q.Where( i => i.FirstName.StartsWith( "Jo" ) ).OrderBy( i => i.LastName ) );
        ///         Console.WriteLine( i => i.ToString() );
        /// }
        /// ]]>
        /// </code></example>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated by a code contract.")]
        public static Task<IEnumerable<T>> GetAsync<T>(this IRepository<T> repository, Func<IQueryable<T>, IQueryable<T>> queryShaper) where T : class
        {
            Contract.Requires(repository != null);
            Contract.Requires(queryShaper != null);
            Contract.Ensures(Contract.Result<Task<IEnumerable<T>>>() != null);
            return repository.GetAsync(queryShaper, CancellationToken.None);
        }

        /// <summary>
        /// Retrieves a query result asynchronously.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type">type</see> of item in the repository.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type">type</see> of result to retrieve.</typeparam>
        /// <param name="repository">The extended <see cref="IRepository{T}">repository</see>.</param>
        /// <param name="queryShaper">The <see cref="Func{T,TResult}">function</see> that shapes the <see cref="IQueryable{T}">query</see> to execute.</param>
        /// <returns>A <see cref="Task{T}">task</see> containing the <typeparamref name="TResult">result</typeparamref> of the operation.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated by a code contract.")]
        public static Task<TResult> GetAsync<T, TResult>(this IRepository<T> repository, Func<IQueryable<T>, TResult> queryShaper) where T : class
        {
            Contract.Requires(repository != null);
            Contract.Requires(queryShaper != null);
            Contract.Ensures(Contract.Result<Task<TResult>>() != null);
            return repository.GetAsync(queryShaper, CancellationToken.None);
        }

        /// <summary>
        /// Retrieves all items in the repository asynchronously.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type">type</see> of item in the repository.</typeparam>
        /// <param name="repository">The extended <see cref="IRepository{T}">repository</see>.</param>
        /// <returns>A <see cref="Task{T}">task</see> containing the <see cref="IEnumerable{T}">sequence</see>
        /// of all <typeparamref name="T">items</typeparamref> in the repository.</returns>
        /// <example>The following example demonstrates how to retrieve all items in a repository.
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
        ///     var repository = new MyRepository();
        ///     
        ///     foreach ( var item in await repository.GetAllAsync( i => i.LastName == "Doe" ) )
        ///         Console.WriteLine( item.ToString() );
        /// }
        /// ]]>
        /// </code></example>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated by a code contract.")]
        public static Task<IEnumerable<T>> GetAllAsync<T>(this IRepository<T> repository) where T : class
        {
            Contract.Requires(repository != null);
            Contract.Ensures(Contract.Result<Task<IEnumerable<T>>>() != null);
            return repository.GetAsync(q => q, CancellationToken.None);
        }

        /// <summary>
        /// Retrieves all items in the repository asynchronously.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type">type</see> of item in the repository.</typeparam>
        /// <param name="repository">The extended <see cref="IRepository{T}">repository</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken">cancellation token</see> that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Task{T}">task</see> containing the <see cref="IEnumerable{T}">sequence</see>
        /// of all <typeparamref name="T">items</typeparamref> in the repository.</returns>
        /// <example>The following example demonstrates how to retrieve all items in a repository.
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
        ///     var repository = new MyRepository();
        ///     var cancellationToken = new CancellationToken();
        ///     
        ///     foreach ( var item in await repository.GetAllAsync( i => i.LastName == "Doe", cancellationToken ) )
        ///         Console.WriteLine( item.ToString() );
        /// }
        /// ]]>
        /// </code></example>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated by a code contract.")]
        public static Task<IEnumerable<T>> GetAllAsync<T>(this IRepository<T> repository, CancellationToken cancellationToken) where T : class
        {
            Contract.Requires(repository != null);
            Contract.Ensures(Contract.Result<Task<IEnumerable<T>>>() != null);
            return repository.GetAsync(q => q, cancellationToken);
        }

        /// <summary>
        /// Searches for items in the repository that match the specified predicate asynchronously.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type">type</see> of item in the repository.</typeparam>
        /// <param name="repository">The extended <see cref="IRepository{T}">repository</see>.</param>
        /// <param name="predicate">The <see cref="Expression{T}">expression</see> representing the predicate used to
        /// match the requested <typeparamref name="T">items</typeparamref>.</param>
        /// <returns>A <see cref="Task{T}">task</see> containing the matched <see cref="IEnumerable{T}">sequence</see>
        /// of <typeparamref name="T">items</typeparamref>.</returns>
        /// <example>The following example demonstrates how to find items in a repository.
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
        ///     var repository = new MyRepository();
        ///     var items = await repository.FindByAsync( i => i.LastName == "Doe" );
        ///     items.ForEach( i => Console.WriteLine( i.ToString() ) );
        /// }
        /// ]]>
        /// </code></example>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated by a code contract.")]
        public static Task<IEnumerable<T>> FindByAsync<T>(this IRepository<T> repository, Expression<Func<T, bool>> predicate) where T : class
        {
            Contract.Requires(repository != null);
            Contract.Requires(predicate != null);
            Contract.Ensures(Contract.Result<Task<IEnumerable<T>>>() != null);
            return repository.GetAsync(q => q.Where(predicate), CancellationToken.None);
        }

        /// <summary>
        /// Searches for items in the repository that match the specified predicate asynchronously.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type">type</see> of item in the repository.</typeparam>
        /// <param name="repository">The extended <see cref="IRepository{T}">repository</see>.</param>
        /// <param name="predicate">The <see cref="Expression{T}">expression</see> representing the predicate used to
        /// match the requested <typeparamref name="T">items</typeparamref>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken">cancellation token</see> that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Task{T}">task</see> containing the matched <see cref="IEnumerable{T}">sequence</see>
        /// of <typeparamref name="T">items</typeparamref>.</returns>
        /// <example>The following example demonstrates how to find items in a repository.
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
        ///     var repository = new MyRepository();
        ///     var cancellationToken = new CancellationToken();
        ///     var items = await repository.FindByAsync( i => i.LastName == "Doe", cancellationToken );
        ///     items.ForEach( i => Console.WriteLine( i.ToString() ) );
        /// }
        /// ]]>
        /// </code></example>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated by a code contract.")]
        public static Task<IEnumerable<T>> FindByAsync<T>(this IRepository<T> repository, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) where T : class
        {
            Contract.Requires(repository != null);
            Contract.Requires(predicate != null);
            Contract.Ensures(Contract.Result<Task<IEnumerable<T>>>() != null);
            return repository.GetAsync(q => q.Where(predicate), cancellationToken);
        }

        /// <summary>
        /// Retrieves a single item in the repository matching the specified predicate asynchronously.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type">type</see> of item in the repository.</typeparam>
        /// <param name="repository">The extended <see cref="IRepository{T}">repository</see>.</param>
        /// <param name="predicate">The <see cref="Expression{T}">expression</see> representing the predicate used to
        /// match the requested <typeparamref name="T">item</typeparamref>.</param>
        /// <returns>A <see cref="Task{T}">task</see> containing the matched <typeparamref name="T">item</typeparamref>
        /// or null if no match was found.</returns>
        /// <example>The following example demonstrates how to retrieve a single item from a repository.
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
        ///     var repository = new MyRepository();
        ///     var item = await repository.GetSingleAsync( i => i.Id == 1 );
        ///     Console.WriteLine( item.ToString() );
        /// }
        /// ]]>
        /// </code></example>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated by a code contract.")]
        public static Task<T> GetSingleAsync<T>(this IRepository<T> repository, Expression<Func<T, bool>> predicate) where T : class
        {
            Contract.Requires(repository != null);
            Contract.Requires(predicate != null);
            Contract.Ensures(Contract.Result<Task<T>>() != null);
            return repository.GetSingleAsync(predicate, CancellationToken.None);
        }

        /// <summary>
        /// Retrieves a single item in the repository matching the specified predicate asynchronously.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type">type</see> of item in the repository.</typeparam>
        /// <param name="repository">The extended <see cref="IRepository{T}">repository</see>.</param>
        /// <param name="predicate">The <see cref="Expression{T}">expression</see> representing the predicate used to
        /// match the requested <typeparamref name="T">item</typeparamref>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken">cancellation token</see> that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Task{T}">task</see> containing the matched <typeparamref name="T">item</typeparamref>
        /// or null if no match was found.</returns>
        /// <example>The following example demonstrates how to retrieve a single item from a repository.
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
        ///     var repository = new MyRepository();
        ///     var item = await repository.GetSingleAsync( i => i.Id == 1 );
        ///     Console.WriteLine( item.ToString() );
        /// }
        /// ]]>
        /// </code></example>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated by a code contract.")]
        public static async Task<T> GetSingleAsync<T>(this IRepository<T> repository, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) where T : class
        {
            Contract.Requires(repository != null);
            Contract.Requires(predicate != null);
            Contract.Ensures(Contract.Result<Task<T>>() != null);
            var items = await repository.GetAsync(q => q.Where(predicate), cancellationToken);
            return items.SingleOrDefault();
        }
    }
}
