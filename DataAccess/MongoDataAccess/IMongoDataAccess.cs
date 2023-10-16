
using MongoDB.Bson;

namespace DataAccess.MongoDataAccess
{
    public interface IMongoDataAccess
    {
        Task<bool> InsertRecord<TEntity>(TEntity entity, string table = null);
        List<TEntity> LoadRecords<TEntity>();

        List<TEntity> LoadRecordsByField<TEntity>(string field, object value);

        Task<bool> UpdateRecord<TEntity>(TEntity entity, Guid id, string table = null);

        Task<bool> DeleteRecord<TEntity>(Guid id);

        Task<ObjectId> UploadFile(Stream stream, string fileName, string contentType);
    }
}
