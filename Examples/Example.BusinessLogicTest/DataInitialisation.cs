using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Data;

namespace Example.BusinessLogicTest
{
	/// <summary>
	/// A class which initialises databases (both fake and SQL server).
	/// </summary>
	public class DataInitialisation
	{
		/// <summary>
		/// This method could be used to initialise either a real database
		/// or a fake one.
		/// </summary>
		public void Initialise(IBookStoreEntities database)
		{
			CreateBooks(database);
			database.SaveChanges();
		}

		/// <summary>
		/// Initialises the books table of the database.
		/// </summary>
		/// <param name="database">The entities to update.</param>
		private void CreateBooks(IBookStoreEntities database)
		{
			// Add fake books.
			if (!database.Books.Any())
			{
				var authorOne = new Author()
				{
					DateOfBirth = new DateTime(2010, 1, 1),
					FirstName = "First Name 1",
					LastName = "Last Name 1"
				};

				database.Books.Add(new Book()
				{
					Author = authorOne,
					Title = "Book 1",
					YearPublished = 2010
				});

				database.Authors.Add(authorOne);

				var authorTwo = new Author()
				{
					DateOfBirth = new DateTime(2011, 1, 1),
					FirstName = "First Name 2",
					LastName = "Last Name 2"
				};

				database.Books.Add(new Book()
				{
					Author = authorTwo,
					Title = "Book 2",
					YearPublished = 2011
				});

				database.Authors.Add(authorTwo);

				database.SaveChanges();
			}
		}
	}
}
