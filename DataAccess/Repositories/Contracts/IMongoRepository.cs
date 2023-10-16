using MongoDB.Bson;

namespace DataAccess.Repositories.Contracts
{
    public interface IMongoRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// inserta un registro
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        Task<bool> InsertRecord<TEntity>(TEntity entity, string table = null);

        /// <summary>
        /// lista registros
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        List<TEntity> LoadRecords<TEntity>();

        /// <summary>
        /// lista registros por campo
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        List<TEntity> LoadRecordsByField<TEntity>(string field, object value);

        /// <summary>
        /// actualiza registro
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        Task<bool> UpdateRecord<TEntity>(TEntity entity, Guid id, string table = null);

        /// <summary>
        /// elimina registro
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteRecord<TEntity>(Guid id);

        /// <summary>
        /// registra un archivo
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        Task<ObjectId> UploadFile(Stream stream, string fileName, string contentType);
    }
}
