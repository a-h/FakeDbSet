using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections;

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
        HashSet<T> Data { get; set; }

		readonly static HashSet<T> _data = new HashSet<T>();
		readonly IQueryable _query = _data.AsQueryable();

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
            this.Data = data;
        }

        /// <summary>
        /// Creates an instance of the InMemoryDbSet using the default static backing store.This means
        /// that data persists between test runs, like it would do with a database unless you
        /// cleared it down.
        /// </summary>
        /// <param name="clearDownExistingData"></param>
        public InMemoryDbSet(bool clearDownExistingData)
        {
            if (clearDownExistingData)
            {
                Clear();
            }
        }

        public void Clear()
        {
            if (this.IsStaticMode)
            {
                _data.Clear();
            }
            else
            {
                this.Data.Clear();
            }
        }

		public T Add(T entity)
		{
            if (this.IsStaticMode)
            {
                _data.Add(entity);
            }
            else
            {
                this.Data.Add(entity);
            }

			return entity;
		}

		public T Attach(T entity)
		{
            if (this.IsStaticMode)
            {
                _data.Add(entity);
            }
            else
            {
                this.Data.Add(entity);
            }

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

		public System.Collections.ObjectModel.ObservableCollection<T> Local
		{
			get { return new System.Collections.ObjectModel.ObservableCollection<T>(_data); }
		}

		public T Remove(T entity)
		{
            if (this.IsStaticMode)
            {
                _data.Remove(entity);
            }
            else
            {
                this.Data.Remove(entity);
            }

			return entity;
		}

		public IEnumerator<T> GetEnumerator()
		{
            if (this.IsStaticMode)
            {
                return _data.GetEnumerator();
            }
            else
            {
                return this.Data.GetEnumerator();
            }
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
            if (this.IsStaticMode)
            {
                return _data.GetEnumerator();
            }
            else
            {
                return this.Data.GetEnumerator();
            }
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
