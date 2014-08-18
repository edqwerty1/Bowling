﻿namespace Bowling.Domain.Concrete.EntityFramework
{
    using Bowling.Domain.Abstract;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts; 

    /// <summary>
    /// Represents an object that can locate and provide unit of work factories.
    /// </summary>
    public sealed class UnitOfWorkFactoryProvider : IUnitOfWorkFactoryProvider
    {
        private readonly Func<IEnumerable<IUnitOfWorkFactory>> factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkFactoryProvider"/> class.
        /// </summary>
        /// <param name="providerFactory">The provider <see cref="Func{T}">factory method</see> used to locate factories.</param>
        [SuppressMessage( "Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generic support." )]
        public UnitOfWorkFactoryProvider( Func<IEnumerable<IUnitOfWorkFactory>> providerFactory )
        {
            Contract.Requires( providerFactory != null, "providerFactory" );
            this.factory = providerFactory;
        }

        /// <summary>
        /// Gets a sequence of unit of work factories supported by the provider.
        /// </summary>
        /// <value>A <see cref="IEnumerable{T}">sequence</see> of <see cref="IUnitOfWorkFactory">unit of work factories</see>.</value>
        public IEnumerable<IUnitOfWorkFactory> Factories
        {
            get
            {
                return this.factory();
            }
        }
    }
}
