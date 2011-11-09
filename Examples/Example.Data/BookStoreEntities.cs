using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Example.Data
{
	/// <summary>
	/// The real database implementation (as opposed to FakeDatabase).  Note that
	/// we implement the IBookStoreEntities interface here as well as inheriting
	/// from DbContext.
	/// </summary>
	public class BookStoreEntities : DbContext, IBookStoreEntities
	{
		public IDbSet<Book> Books { get; set; }
		public IDbSet<Author> Authors { get; set; }
	}
}
