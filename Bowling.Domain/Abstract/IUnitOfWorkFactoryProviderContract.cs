﻿namespace Bowling.Domain.Abstract
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    [ContractClassFor( typeof( IUnitOfWorkFactoryProvider ) )]
    internal abstract class IUnitOfWorkFactoryProviderContract : IUnitOfWorkFactoryProvider
    {
        IEnumerable<IUnitOfWorkFactory> IUnitOfWorkFactoryProvider.Factories
        {
            get
            {
                Contract.Ensures( Contract.Result<IEnumerable<IUnitOfWorkFactory>>() != null );
                return null;
            }
        }
    }
}
