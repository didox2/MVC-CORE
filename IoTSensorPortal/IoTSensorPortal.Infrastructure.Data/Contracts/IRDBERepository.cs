using System.Linq;
using System.Threading.Tasks;

namespace IoTSensorPortal.Infrastructure.Data.Contracts
{
    /// <summary>
    /// Interface for RDBE Repository
    /// Relational Database Engine
    /// </summary>
    /// <typeparam name="T">Type of the data table to which 
    /// current reposity is attached</typeparam>
    public interface IRDBERepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// The result collection won't be tracked by the context
        /// </summary>
        /// <returns>Expression tree</returns>
        IQueryable<T> AllReadonly();

        /// <summary>
        /// Detaches given entity from the context
        /// </summary>
        /// <param name="entity">Entity to be detached</param>
        void Detach(T entity);

        /// <summary>
        /// Saves all made changes in trasaction
        /// </summary>
        /// <returns>Error code</returns>
        int SaveChanges();
    }
}
