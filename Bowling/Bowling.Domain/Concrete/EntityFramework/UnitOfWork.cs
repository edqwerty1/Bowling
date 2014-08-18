﻿namespace Bowling.Domain.Concrete.EntityFramework
{
    using Bowling.Domain.Abstract;
    using StructureMap;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a unit of work factory.
    /// </summary>
    public static class UnitOfWork
    {
        /// <summary>
        /// Represents an empty unit of work factory to provider used to satisfy the Null Object pattern.
        /// </summary>
        private sealed class EmptyFactoryProvider : IUnitOfWorkFactoryProvider
        {
            public IEnumerable<IUnitOfWorkFactory> Factories
            {
                get
                {
                    return new IUnitOfWorkFactory[0];
                }
            }
        }

        /// <summary>
        /// Represents a unit of work factory that creates uncommittable units of work for any type.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type">type</see> to create a unit of work for.</typeparam>
        private sealed class UncommittableUnitOfWorkFactory<T> : UnitOfWorkFactory where T : class
        {
            private readonly ISpecification<Type> specification = new Specification<Type>( t => true );

            internal UncommittableUnitOfWorkFactory()
            {
                this.RegisterFactoryMethod( () => new UncommittableUnitOfWork<T>() );
            }

            public override ISpecification<Type> Specification
            {
                get
                {
                    return this.specification;
                }
            }
        }

        /// <summary>
        /// Represents an uncommittable unit of work.
        /// </summary>
        /// <typeparam name="T">The type of objects contained in the unit of work.</typeparam>
        /// <remarks>This class is internally used to satisfy the Null Object pattern. This allows unit testing and
        /// other scenarios without requiring a unit of work to be registered.  When an attempt is made to commit
        /// work against this class is performed, an <see cref="InvalidOperationException"/> is thrown.</remarks>
        private sealed class UncommittableUnitOfWork<T> : UnitOfWork<T> where T : class
        {
            protected override bool IsNew( T item )
            {
                // treat all items as new items
                return true;
            }

            public override Task CommitAsync( CancellationToken cancellationToken )
            {
                // no work can ever be committed by this class
                var format = "No factory has been registered for a unit of work of type '{0}'.";
                var message = string.Format( CultureInfo.CurrentCulture, format, typeof( T ) );
                throw new InvalidOperationException( message );
            }
        }

        private static IUnitOfWorkFactoryProvider provider = new EmptyFactoryProvider();

        /// <summary>
        /// Gets or sets the <see cref="IUnitOfWorkFactoryProvider">unit of work factory provider</see> used for all units of work.
        /// </summary>
        /// <value>A <see cref="IUnitOfWorkFactoryProvider">unit of work factory provider</see>.</value>
        public static IUnitOfWorkFactoryProvider Provider
        {
            get
            {
                if (provider == null)
                    provider = ObjectFactory.GetInstance<IUnitOfWorkFactoryProvider>();

                Contract.Ensures( Contract.Result<IUnitOfWorkFactoryProvider>() != null );
                return provider;
            }
            set
            {
                Contract.Requires( value != null, "value" );
                provider = value;
            }
        }

        private static IUnitOfWorkFactory GetFactory<TItem>() where TItem : class
        {
            Contract.Ensures( Contract.Result<IUnitOfWorkFactory>() != null );

            var type = typeof( TItem );
            IUnitOfWorkFactory factory = null;

            try
            {
                // resolve matching factory
                factory = Provider.Factories.SingleOrDefault( f => f.Specification.IsSatisfiedBy( type ) );
            }
            catch ( InvalidOperationException )
            {
                // more than one factory is registered
                var format = "Multiple factories have been registered for a unit of work of type '{0}'.";
                var message = string.Format( CultureInfo.CurrentCulture, format, type );
                throw new InvalidOperationException( message );
            }

            // if no factory is found, use an uncommittable unit of work factory (null object pattern)
            return factory ?? new UncommittableUnitOfWorkFactory<TItem>();
        }

        /// <summary>
        /// Creates a unit of work for a given type.
        /// </summary>
        /// <typeparam name="TItem">The <see cref="Type">type</see> of item to create a <see cref="IUnitOfWork{T}">unit of work</see> for.</typeparam>
        /// <returns>A <see cref="IUnitOfWork{T}">unit of work</see>.</returns>
        public static IUnitOfWork<TItem> Create<TItem>() where TItem : class
        {
            Contract.Ensures( Contract.Result<IUnitOfWork<TItem>>() != null );
            var factory = GetFactory<TItem>();
            return factory.Create<TItem>();
        }

        /// <summary>
        /// Gets the current unit of work for a given type.
        /// </summary>
        /// <typeparam name="TItem">The <see cref="Type">type</see> of item to retrieve a <see cref="IUnitOfWork{T}">unit of work</see> for.</typeparam>
        /// <returns>A <see cref="IUnitOfWork{T}">unit of work</see>.</returns>
        public static IUnitOfWork<TItem> GetCurrent<TItem>() where TItem : class
        {
            Contract.Ensures( Contract.Result<IUnitOfWork<TItem>>() != null );
            var factory = GetFactory<TItem>();
            return factory.GetCurrent<TItem>();
        }

        /// <summary>
        /// Sets the current unit of work for a given type.
        /// </summary>
        /// <typeparam name="TItem">The <see cref="Type">type</see> of item to set a <see cref="IUnitOfWork{T}">unit of work</see> for.</typeparam>
        /// <param name="unitOfWork">The current <see cref="IUnitOfWork{T}">unit of work</see>.</param>
        public static void SetCurrent<TItem>( IUnitOfWork<TItem> unitOfWork ) where TItem : class
        {
            Contract.Requires( unitOfWork != null, "unitOfWork" );
            var factory = GetFactory<TItem>();
            factory.SetCurrent( unitOfWork );
        }

        /// <summary>
        /// Sets and returns a new unit of work for a given type.
        /// </summary>
        /// <typeparam name="TItem">The <see cref="Type">type</see> of item to create a new a <see cref="IUnitOfWork{T}">unit of work</see> for.</typeparam>
        /// <returns>The new, current <see cref="IUnitOfWork{T}">unit of work</see> for the given <typeparamref name="TItem">item</typeparamref>.</returns>
        public static IUnitOfWork<TItem> NewCurrent<TItem>() where TItem : class
        {
            Contract.Ensures( Contract.Result<IUnitOfWork<TItem>>() != null );
            
            var factory = GetFactory<TItem>();
            var unitOfWork = factory.Create<TItem>();

            factory.SetCurrent( unitOfWork );

            return unitOfWork;
        }
    }
}
