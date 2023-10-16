using DataAccess.MongoDataAccess;
using DataAccess.Repositories.Contracts;
using MongoDB.Bson;

namespace DataAccess.Repositories
{
    public class MongoGenericRepository<TEntity> : IMongoRepository<TEntity>
        where TEntity : class
    {
        private readonly IMongoDataAccess _dataAccess;
        public MongoGenericRepository(IMongoDataAccess dataAccess) { 
            _dataAccess = dataAccess;
        }
        public async Task<bool> DeleteRecord<TEntity>(Guid id)
        {
            return await _dataAccess.DeleteRecord<TEntity>(id);
        }

        public async Task<bool> InsertRecord<TEntity>(TEntity entity, string table = null)
        {
            return await _dataAccess.InsertRecord<TEntity>(entity, table);
        }

        public List<TEntity> LoadRecords<TEntity>()
        {
            return _dataAccess.LoadRecords<TEntity>();
        }

        public List<TEntity> LoadRecordsByField<TEntity>(string field, object value)
        {
            return _dataAccess.LoadRecordsByField<TEntity>(field, value);
        }

        public async Task<bool> UpdateRecord<TEntity>(TEntity entity, Guid id, string table = null)
        {
            return await _dataAccess.UpdateRecord<TEntity>(entity, id, table);
        }

        public async Task<ObjectId> UploadFile(Stream stream, string fileName, string contentType)
        {
            return await _dataAccess.UploadFile(stream, fileName, contentType);
        }
    }
}
