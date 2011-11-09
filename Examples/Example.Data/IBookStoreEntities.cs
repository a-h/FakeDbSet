using System;
using System.Data.Entity;

namespace Example.Data
{
	/// <summary>
	/// The interface which data access code works against (data access code uses 
	/// IBookStoreEntities rather than BookStoreEntities).
	/// </summary>
	public interface IBookStoreEntities : IDisposable
	{
		IDbSet<Author> Authors { get; set; }
		IDbSet<Book> Books { get; set; }

		int SaveChanges();
	}
}
