using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Data;

namespace Example.BusinessLogic
{
	/// <summary>
	/// An example logic layer which accesses the "database".
	/// </summary>
	public class DataAccess
	{
		private IFactory<IBookStoreEntities> factory;

		/// <summary>
		/// Creates an instance of the Data Access class, passing in a factory.  The
		/// factory creates an instance of IBookStoreEntities.  You might want to use 
		/// Ninject and create your own Ninject module rather than writing your own 
		/// factory implementations.
		/// </summary>
		/// <param name="factory">The factory which will create book store entities.</param>
		public DataAccess(IFactory<IBookStoreEntities> factory)
		{
			this.factory = factory;
		}

		/// <summary>
		/// Provides a list of books created by an author.  It will use the IBookStoreEntities
		/// created by the factory to list the books.  The IBookeStoreEntities created by
		/// the factory could be a fake database, or a real SQL server database, this code doesn't
		/// care.
		/// </summary>
		/// <param name="authorLastName">The last name of the author.</param>
		/// <returns>A list of books created by an author with the specified last name.</returns>
		public List<Book> ListBooksCreatedBy(string authorLastName)
		{
			using (IBookStoreEntities entities = factory.Create())
			{
				return entities.Books
					.Where(b => 
						b.Author.LastName == authorLastName)
					.ToList();
			}
		}
	}
}
