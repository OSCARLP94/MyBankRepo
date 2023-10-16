using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq.Expressions;

namespace DataAccess.Repositories.Contracts
{
    public interface IRepository<TEntity>
      where TEntity : class
    {
        /// <summary>
        /// Devuelve el objeto que corresponde a un identificador o valor clave específico.
        /// </summary>
        /// <param name="id">Valor clave identificador del objeto.</param>
        /// <returns>Objeto que corresponde al identificador especificado.</returns>
        Task<TEntity> Get(object id);

        /// <summary>
        /// Utilizando funcion lambda, trata de hallar lista coincidente
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Devuelve una colección con todos los elementos de la entidad que se encuentran almacenados en el origen de datos. 
        /// </summary>
        /// <returns>Conjunto de elementos de la entidad existentes en el origen de datos.</returns>
        Task<List<TEntity>> List();


        /// <summary>
        /// Registra un elemento del tipo entidad en el origen de datos. 
        /// </summary>
        /// <returns>Resultado bool del registro en el origen de datos.</returns>
        Task<bool> Create(TEntity entity);

        /// <summary>
        /// Registra (en transaccion) un elemento del tipo entidad en el origen de datos. 
        /// </summary>
        /// <returns>Resultado bool del registro en el origen de datos.</returns>
        Task<bool> CreateOnTransac(TEntity entity);

        /// <summary>
        /// Actualiza un elemento del tipo entidad en el origen de datos. 
        /// </summary>
        /// <returns>Entidad actualizada en el origen de datos.</returns>
        Task<TEntity> Update(TEntity entity);

        /// <summary>
        /// Actualiza (en transaccion) un elemento del tipo entidad en el origen de datos. 
        /// </summary>
        /// <returns>Entidad actualizada en el origen de datos.</returns>
        Task<TEntity> UpdateOnTransac(TEntity entity);

        /// <summary>
        /// Elimina un elemento del tipo entidad del origen de datos. 
        /// </summary>
        /// <returns>Resultado bool del borrado del registro en el origen de datos.</returns>
        Task<bool> Delete(TEntity entity);

        /// <summary>
        /// Elimina (en transac) un elemento del tipo entidad del origen de datos. 
        /// </summary>
        /// <returns>Resultado bool del borrado del registro en el origen de datos.</returns>
        Task<bool> DeleteOnTransac(TEntity entity);

        /// <summary>
        /// Ejecuta un Stored Procedure en BD
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        Task<int> ExecuteSP(DbCommand cmd);

        /// <summary>
        /// Registra cambios en BD y transacciones
        /// </summary>
        /// <returns></returns>
        Task SaveDbChangesAsync();

        DbCommand GetDbCommand();
    }
}
