using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace DataAccess.MongoDataAccess
{
    public class MongoDataAccess : IMongoDataAccess
    {
        private readonly IMongoDatabase _database;
        private IGridFSBucket _gridFsBucket;

        public MongoDataAccess(string databaseName, string databaseConnection)
        {
            try
            {
                if (string.IsNullOrEmpty(databaseName) || string.IsNullOrEmpty(databaseConnection))
                    throw new ArgumentException("Login error: Database connection values cannot be empty or null");

                var client = new MongoClient(databaseConnection);
                _database = client.GetDatabase(databaseName);
                _gridFsBucket = new GridFSBucket(_database);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertRecord<TEntity>(TEntity entity, string table = null)
        {
            try
            {
                var nameTable = string.IsNullOrEmpty(table) ? entity.GetType().Name : table;
                var collection = _database.GetCollection<TEntity>(nameTable);
                await collection.InsertOneAsync(entity);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TEntity> LoadRecords<TEntity>()
        {
            try
            {
                var collection = _database.GetCollection<TEntity>(typeof(TEntity).Name);
                return collection.Find(new BsonDocument()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TEntity> LoadRecordsByField<TEntity>(string field, object value)
        {
            try
            {
                var collection = _database.GetCollection<TEntity>(typeof(TEntity).Name);
                var filter = Builders<TEntity>.Filter.Eq(field, value);
                return collection.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateRecord<TEntity>(TEntity entity, Guid id, string table = null)
        {
            try
            {
                var nameTable = string.IsNullOrEmpty(table) ? entity.GetType().Name : table;
                var collection = _database.GetCollection<TEntity>(nameTable);
                var result = await collection.ReplaceOneAsync(
                            new BsonDocument("_id", id),
                            entity,
                            new ReplaceOptions { IsUpsert = false });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteRecord<TEntity>(Guid id)
        {
            try
            {
                var collection = _database.GetCollection<TEntity>(typeof(TEntity).Name);
                var filter = Builders<TEntity>.Filter.Eq("Id", id);
                var result = await collection.DeleteOneAsync(filter);

                if (result.DeletedCount <= 0)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ObjectId> UploadFile(Stream stream, string fileName, string contentType)
        {
            try
            {
                var options = new GridFSUploadOptions
                {
                    Metadata = new BsonDocument
                {
                    { "FileName", fileName },
                    { "ContentType", contentType }
                }
                };

                return await _gridFsBucket.UploadFromStreamAsync(fileName, stream, options);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Stream> DownloadFile(ObjectId fileId)
        {
            try
            {
                return await _gridFsBucket.OpenDownloadStreamAsync(fileId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteFile(ObjectId fileId)
        {
            try
            {
                await _gridFsBucket.DeleteAsync(fileId);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
