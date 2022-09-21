namespace Pichincha.Infrastructure.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using Pichincha.Infrastructure.DataAccess.Interfaces;
    using Pichincha.Infrastructure.Interfaces;
    using Pichincha.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The SQL data access.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly ISqlDataContext dataContext;

        /// <summary>
        /// The data set.
        /// </summary>
        private readonly DbSet<TEntity> dataset;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}" /> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="businessContext">The business context.</param>
        public Repository(ISqlDataContext dataContext)
        {
            ArgumentValidators.ThrowIfNull(dataContext, nameof(dataContext));

            this.dataContext = dataContext;
            this.dataset = this.dataContext.Set<TEntity>();
        }

        /// <inheritdoc/>
        public DbSet<TEntity> EntitySet()
        {
            return this.dataset;
        }

        /// <inheritdoc/>
        public DbSet<T> Set<T>()
            where T : class, IEntity
        {
            return this.dataContext.Set<T>();
        }

        /// <inheritdoc/>
        public TEntity Insert(TEntity entity)
        {
            ArgumentValidators.ThrowIfNull(entity, nameof(entity));
            return this.dataset.Add(entity).Entity;
        }

        /// <inheritdoc/>
        public void InsertAll(IEnumerable<TEntity> entities)
        {
            ArgumentValidators.ThrowIfNull(entities, nameof(entities));
            this.dataset.AddRange(entities);
        }

        /// <inheritdoc/>
        public void UpdateAll(IEnumerable<TEntity> entities)
        {
            ArgumentValidators.ThrowIfNull(entities, nameof(entities));
            this.dataset.UpdateRange(entities);
        }

        /// <inheritdoc/>
        public void Update(TEntity entity)
        {
            ArgumentValidators.ThrowIfNull(entity, nameof(entity));
            this.dataset.Attach(entity);
            this.dataContext.Entry(entity).State = EntityState.Modified;
        }

        /// <inheritdoc/>
        public void Delete(TEntity entity)
        {
            ArgumentValidators.ThrowIfNull(entity, nameof(entity));
            this.dataset.Remove(entity);
        }

        /// <inheritdoc/>
        public void DeleteAll(IEnumerable<TEntity> entities)
        {
            ArgumentValidators.ThrowIfNull(entities, nameof(entities));
            this.dataset.RemoveRange(entities);
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await this.dataset.FindAsync(id);
        }

        /// <inheritdoc/>
        public Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate == null ? this.dataset.LongCountAsync() : this.dataset.LongCountAsync(predicate);
        }

        /// <inheritdoc/>
        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate != null
                ? dataset.FirstOrDefaultAsync(predicate)
                : dataset.FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.Select(selector).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate != null
                ? this.dataset.SingleOrDefaultAsync(predicate)
                : this.dataset.SingleOrDefaultAsync();
        }

        /// <inheritdoc/>
        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.SingleOrDefaultAsync();
        }

        /// <inheritdoc/>
        public Task<TResult> SingleOrDefaultAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.Select(selector).SingleOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.dataset.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
                query = query.Where(predicate);

            if (includes != null)
                includes.ToList().ForEach(include =>
                {
                    if (include != null)
                        query = query.Include(include);
                });

            return await query.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = this.dataset.AsQueryable();
            if (includes != null)
                includes.ToList().ForEach(include =>
                {
                    if (include != null)
                        query = query.Include(include);
                });
            return await query.ToListAsync();
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TResult, bool>> predicate, params string[] includeProperties)
            where TResult : class, IEntity
        {
            IQueryable<TResult> query = this.Set<TResult>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.Select(selector).ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> OrderByAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, int? take)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> OrderByDescendingAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, int? take)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = query.OrderByDescending(orderBy);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<IQueryable<TEntity>> QueryAllAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return Task.FromResult(query);
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await this.dataContext.SaveAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public Task ExecuteAsync(object args, IDictionary<string, object> data)
        {
            ArgumentValidators.ThrowIfNull(args, nameof(args));
            ArgumentValidators.ThrowIfNull(data, nameof(data));

            var sql = BuildSql(args.ToString(), data.Keys);
            var parameters = data.Select(BuildParameters);

            return null;//this.dataContext.Database.ExecuteSqlCommandAsync(new RawSqlString(sql), parameters);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TEntity>> ExecuteQueryAsync(object args, IDictionary<string, object> data)
        {
            ArgumentValidators.ThrowIfNull(args, nameof(args));
            ArgumentValidators.ThrowIfNull(data, nameof(data));

            var sql = BuildSql(args.ToString(), data.Keys);
            var parameters = data.Select(BuildParameters);

            return null;// await this.dataContext.Query<TEntity>().FromSql(new RawSqlString(sql), parameters.ToArray<object>()).ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> ExecuteViewAsync()
        {
            return Task.FromResult(this.dataContext.Query<TEntity>().AsQueryable());
        }

        private static string BuildSql(string name, IEnumerable<string> keys)
        {
            var sb = new StringBuilder();

            // build input parameters
            var input = keys.Where(k => !k.StartsWith("@out_", StringComparison.OrdinalIgnoreCase));
            sb.Append(name).Append(' ').Append(string.Join(",", input));

            // build output parameters
            var output = keys.Where(k => k.StartsWith("@out_", StringComparison.OrdinalIgnoreCase)).ToList();
            output.ForEach(o =>
            {
                sb.Append(',').Append(o).Append(' ').Append("OUTPUT");
            });

            return sb.ToString();
        }

        private static SqlParameter BuildParameters(KeyValuePair<string, object> parameter)
        {
            var direction = parameter.Key.StartsWith("@out_", StringComparison.OrdinalIgnoreCase) ? ParameterDirection.Output : ParameterDirection.Input;
            var p = new SqlParameter
            {
                ParameterName = parameter.Key,
                Direction = direction,
            };

            // Output parameters of int type are only supported as of now
            if (direction == ParameterDirection.Output)
            {
                p.SqlDbType = SqlDbType.Int;
            }

            if (direction == ParameterDirection.Input)
            {
                p.Value = parameter.Value ?? DBNull.Value;
            }

            if (parameter.Value is not DataTable dt)
            {
                return p;
            }

            p.TypeName = dt.TableName;
            p.SqlDbType = SqlDbType.Structured;

            return p;
        }
    }
}