using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.ObjectModel;

namespace FakeDbSet
{
    /// <summary>
    /// The in-memory database set, taken from Microsoft's online example (http://msdn.microsoft.com/en-us/ff714955.aspx) 
    /// and modified to be based on DbSet instead of ObjectSet.
    /// </summary>
    /// <typeparam name="T">The type of DbSet.</typeparam>
    public class InMemoryDbSet<T> : IDbSet<T> where T : class
    {

        private Func<IEnumerable<T>, object[], T> _findFunction;

        private readonly HashSet<T> _nonStaticData;
        /// <summary>
        /// The non static backing store data for the InMemoryDbSet.
        /// </summary>
        private HashSet<T> Data
        {
            get
            {
                if (_nonStaticData == null)
                {
                    return _staticData;
                }
                else
                {
                    return _nonStaticData;
                }
            }
        }

        private static readonly HashSet<T> _staticData = new HashSet<T>();

        public Func<IEnumerable<T>, object[], T> FindFunction
        {
            get { return _findFunction; }
            set { _findFunction = value; }
        }

        /// <summary>
        /// Creates an instance of the InMemoryDbSet using the default static backing store.This means
        /// that data persists between test runs, like it would do with a database unless you
        /// cleared it down.
        /// </summary>
        public InMemoryDbSet()
            : this(true)
        {
        }

        /// <summary>
        /// This constructor allows you to pass in your own data store, instead of using
        /// the static backing store.
        /// </summary>
        /// <param name="data">A place to store data.</param>
        public InMemoryDbSet(HashSet<T> data)
        {
            _nonStaticData = data;
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
            Data.Clear();
        }

        public T Add(T entity)
        {
            Data.Add(entity);

            return entity;
        }

        public T Attach(T entity)
        {
            Data.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public virtual T Find(params object[] keyValues)
        {
            if (FindFunction == null)
            {
                return FindFunction(Data, keyValues);
            }
            else
            {
                throw new NotImplementedException("Derive from InMemoryDbSet and override Find, or provide a FindFunction.");
            }
        }

        public ObservableCollection<T> Local
        {
            get { return new ObservableCollection<T>(Data); }
        }

        public T Remove(T entity)
        {
            Data.Remove(entity);

            return entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        private IEnumerator GetEnumerator1()
        {
            return Data.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }

        public Type ElementType
        {
            get { return Data.AsQueryable().ElementType; }
        }

        public Expression Expression
        {
            get { return Data.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return Data.AsQueryable().Provider; }
        }
    }
}
