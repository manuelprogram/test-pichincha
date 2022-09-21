namespace Pichincha.Infrastructure.DataAccess.Interfaces
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The data context for data store.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IDataContext : IDisposable
    {

        /// <summary>
        /// Saves the context asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Number of rows effected.
        /// </returns>
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}