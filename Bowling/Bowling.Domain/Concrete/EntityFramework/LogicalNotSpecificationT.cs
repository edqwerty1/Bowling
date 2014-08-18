﻿namespace Bowling.Domain.Concrete.EntityFramework
{
    using Bowling.Domain.Abstract;
    using System;
    using System.Diagnostics.Contracts; 

    /// <summary>
    /// Represents a specification that models a logical compliment to another specification <seealso cref="Specification{T}"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type">type</see> of item to evaluate.</typeparam>
    public class LogicalNotSpecification<T> : SpecificationBase<T>
    {
        private readonly ISpecification<T> complement;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicalNotSpecification{T}"/> class.
        /// </summary>
        /// <param name="complement">The <see cref="ISpecification{T}">specification</see> representing the logical compliment the specification.</param>
        public LogicalNotSpecification( ISpecification<T> complement )
        {
            Contract.Requires( complement != null, "complement" );
            this.complement = complement;
        }

        /// <summary>
        /// Gets the compliment of the specification.
        /// </summary>
        /// <value>An <see cref="ISpecification{T}">specification</see> object.</value>
        protected ISpecification<T> Complement
        {
            get
            {
                Contract.Ensures( Contract.Result<ISpecification<T>>() != null );
                return this.complement;
            }
        }

        /// <summary>
        /// Determines whether the specified item satisfies the specification.
        /// </summary>
        /// <param name="item">The item of <typeparamref name="T"/> to evaluate.</param>
        /// <returns>True if <paramref name="item"/> satisfies the specification; otherwise, false.</returns>
        public override bool IsSatisfiedBy( T item )
        {
            return !this.Complement.IsSatisfiedBy( item );
        }
    }
}
