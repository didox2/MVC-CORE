using System;
using System.Collections.Generic;
using System.Linq;

namespace IoTSensorPortal.Infrastructure.Data.Contracts
{
    /// <summary>
    /// Abstraction of repository access methods
    /// </summary>
    /// <typeparam name="T">Repository type / db table</typeparam>
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// All records in a table
        /// </summary>
        /// <returns>Queryable expression tree</returns>
        IQueryable<T> All();

        /// <summary>
        /// Gets specific record from database by primary key
        /// </summary>
        /// <param name="id">record identificator</param>
        /// <returns>Single record</returns>
        T GetById(object id);

        /// <summary>
        /// Adds entity to the database
        /// </summary>
        /// <param name="entity">Entity to add</param>
        void Add(T entity);

        /// <summary>
        /// Ads collection of entities to the database
        /// </summary>
        /// <param name="entities">Enumerable list of entities</param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// Updates a record in database
        /// </summary>
        /// <param name="entity">Entity for record to be updated</param>
        void Update(T entity);

        /// <summary>
        /// Updates set of records in the database
        /// </summary>
        /// <param name="entities">Enumerable collection of entities to be updated</param>
        void UpdateRange(IEnumerable<T> entities);

        /// <summary>
        /// Deletes a record from database
        /// </summary>
        /// <param name="id">Identificator of record to be deleted</param>
        void Delete(object id);

        /// <summary>
        /// Deletes a record from database
        /// </summary>
        /// <param name="entity">Entity representing record to be deleted</param>
        void Delete(T entity);
    }
}
