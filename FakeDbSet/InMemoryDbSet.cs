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
		readonly static HashSet<T> _data = new HashSet<T>();
		readonly IQueryable _query = _data.AsQueryable();

		public InMemoryDbSet() : this(false)
		{
		}

        public InMemoryDbSet(bool clearDownExistingData)
        {
            if (clearDownExistingData)
            {
                Clear();
            }
        }

        public void Clear()
        {
            _data.Clear();
        }

		public T Add(T entity)
		{
			_data.Add(entity);
			return entity;
		}

		public T Attach(T entity)
		{
			_data.Add(entity);
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
			_data.Remove(entity);
			return entity;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _data.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _data.GetEnumerator();
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
