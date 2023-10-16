using DataAccess.DbContexts;
using DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class RepositoryGeneric<TEntity> : IRepository<TEntity>
      where TEntity : class
    {
        private readonly DbContext _dbContext;
        public RepositoryGeneric(MyBankSQLDBContext dBContext) {
            _dbContext = dBContext;
        }

        public async Task<bool> Create(TEntity entity)
        {

            try
            {
                if (_dbContext != null && entity != null)
                {
                    _dbContext.Set<TEntity>().Add(entity);
                    await this.SaveDbChangesAsync();
                    return true;
                }
                else
                    throw new ArgumentException("Error. Context DB or entity cannot be null.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> CreateOnTransac(TEntity entity)
        {
            try
            {
                if (_dbContext != null && entity != null)
                {
                    _dbContext.Set<TEntity>().Add(entity);
                    return true;
                }
                else
                    throw new ArgumentException("Error. Context DB or entity cannot be null.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(TEntity entity)
        {
            try
            {
                if (_dbContext != null && entity != null)
                {
                    _dbContext.Set<TEntity>().Remove(entity);
                    await this.SaveDbChangesAsync();
                    return true;
                }
                else
                {
                    throw new ArgumentException("Error. Context DB or entity cannot be null.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteOnTransac(TEntity entity)
        {
            try
            {
                if (_dbContext != null && entity != null)
                {
                    _dbContext.Set<TEntity>().Remove(entity);
                    return true;
                }
                else
                {
                    throw new ArgumentException("Error. Context DB or entity cannot be null.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> ExecuteSP(DbCommand cmd)
        {
            int result = -1;
            try
            {
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                    var resp = await cmd.ExecuteScalarAsync();
                    result = (resp != null) ? Convert.ToInt32(resp) : -1;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return result;
        }

        public async Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                if (_dbContext != null)
                    return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
                else
                    throw new ArgumentException("Error. Context DB cannot be null.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TEntity> Get(object id)
        {
            try
            {
                if (_dbContext != null)
                    return await _dbContext.FindAsync<TEntity>(id);
                else
                    throw new ArgumentException("Error. Context DB cannot be null.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DbCommand GetDbCommand()
        {
            return this._dbContext.Database.GetDbConnection().CreateCommand();
        }

        public async Task<List<TEntity>> List()
        {
            try
            {
                if (_dbContext != null)
                    return await _dbContext.Set<TEntity>().ToListAsync();
                else
                    throw new ArgumentException("Error. Context DB cannot be null.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SaveDbChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                if (_dbContext != null && entity != null)
                {
                    _dbContext.Set<TEntity>().Update(entity);
                    await this.SaveDbChangesAsync();
                    return entity;
                }
                else
                {
                    throw new ArgumentException("Error. Context DB or entity cannot be null.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TEntity> UpdateOnTransac(TEntity entity)
        {
            try
            {
                if (_dbContext != null && entity != null)
                {
                    _dbContext.Set<TEntity>().Update(entity);
                    return entity;
                }
                else
                {
                    throw new ArgumentException("Error. Context DB or entity cannot be null.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
