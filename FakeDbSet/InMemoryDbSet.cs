using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace FakeDbSet
{
	/// <summary>
	/// The in-memory database set, taken from Microsoft's online example (http://msdn.microsoft.com/en-us/ff714955.aspx) 
	/// and modified to be based on DbSet instead of ObjectSet.
	/// </summary>
	/// <typeparam name="T">The type of DbSet.</typeparam>
	public class InMemoryDbSet<T> : IDbSet<T> where T : class
	{
        bool IsStaticMode = false;

	    /// <summary>
	    /// The non static backing store data for the InMemoryDbSet.
	    /// </summary>
	    private HashSet<T> Data
	    {
	        get
	        {
                return IsStaticMode ? _StaticData : _InstanceData;
	        }
	    }

        readonly HashSet<T> _InstanceData = new HashSet<T>(); 
        readonly static HashSet<T> _StaticData = new HashSet<T>();
		readonly IQueryable<T> _query;

        /// <summary>
        /// Creates an instance of the InMemoryDbSet using the default static backing store.This means
        /// that data persists between test runs, like it would do with a database unless you
        /// cleared it down.
        /// </summary>
		public InMemoryDbSet() : this(true)
		{
		}

        /// <summary>
        /// This constructor allows you to pass in your own data store, instead of using
        /// the static backing store.
        /// </summary>
        /// <param name="data">A place to store data.</param>
        public InMemoryDbSet(HashSet<T> data)
        {
            this.IsStaticMode = false;
            this._InstanceData = data;
            _query = Data.AsQueryable();
        }

        /// <summary>
        /// Creates an instance of the InMemoryDbSet using the default static backing store.This means
        /// that data persists between test runs, like it would do with a database unless you
        /// cleared it down.
        /// </summary>
        /// <param name="clearDownExistingData"></param>
        public InMemoryDbSet(bool clearDownExistingData)
        {
			_query = Data.AsQueryable();
            if (clearDownExistingData)
            {
                Clear();
            }
        }

	    public void Clear()
        {
            this.Data.Clear();
        }

	    public T Add(T entity)
		{
            this.Data.Add(entity);

			return entity;
		}

		public T Attach(T entity)
		{
            this.Data.Add(entity);

			return entity;
		}

		public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
		{
			throw new NotImplementedException();
		}

	    public T Create()
		{
			return Activator.CreateInstance<T>();
		}

		public virtual T Find(params object[] keyValues)
		{
			throw new NotImplementedException("Derive from InMemoryDbSet and override Find.");
		}

        public Task<T> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            throw new NotImplementedException("Derive from InMemoryDbSet and override Find.");
        }

        public DbLocalView<T> Local
		{
            get { return new DbLocalView<T>(Data); }
		}

		public T Remove(T entity)
		{
            this.Data.Remove(entity);

			return entity;
		}

		public IEnumerator<T> GetEnumerator()
		{
            return this.Data.GetEnumerator();
		}

	    IEnumerator IEnumerable.GetEnumerator()
	    {
	        return GetEnumerator();
	    }

	    public Type ElementType
		{
			get { return _query.ElementType; }
		}

		public Expression Expression
		{
			get { return _query.Expression; }
		}

		public IQueryProvider Provider
		{
			get { return _query.Provider; }
		}
	}
}
