using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Data;
using FakeDbSet;
using System.Data.Entity;

namespace Example.BusinessLogicTest
{
	/// <summary>
	/// This is an example of how we'd create a fake database by implementing the 
	/// same interface that the BookeStoreEntities class implements.
	/// </summary>
	public class FakeDatabase : IBookStoreEntities
	{
		/// <summary>
		/// Sets up the fake database.
		/// </summary>
		public FakeDatabase()
		{
			// We're setting our DbSets to be InMemoryDbSets rather than using SQL Server.
			this.Authors = new InMemoryDbSet<Author>();
			this.Books = new InMemoryDbSet<Book>();
		}

		public IDbSet<Author> Authors { get; set; }
		public IDbSet<Book> Books { get; set; }

		public int SaveChanges()
		{
			// Pretend that each entity gets a database id when we hit save.
			int changes = 0;
			changes += DbSetHelper.IncrementPrimaryKey<Author>(x => x.AuthorId, this.Authors);
			changes += DbSetHelper.IncrementPrimaryKey<Book>(x => x.BookId, this.Books);

			return changes;
		}

		public void Dispose()
		{
			// Do nothing!
		}
	}
}
