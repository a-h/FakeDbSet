using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Example.BusinessLogic;
using Example.Data;

namespace Example.BusinessLogicTest
{
	/// <summary>
	/// An example which shows how we can test EF code by creating our own fake
	/// database entities.
	/// </summary>
	[TestFixture]
	public class DataAccessTest
	{
		/// <summary>
		/// Tests the ListBooksCreatedBy method. 
		/// </summary>
		[Test]
		public void ListBooksCreatedByReturnsAppropriateResults()
		{
			// Arrange.
			// Create an instance of the DataAccess class, passing in a factory
			// which will create test data for the DataAccess class to use.
			var data = new DataAccess(new FakeBookStoreEntitiesFactory());

			// Act.
			// Attempt to call the method.
			List<Book> books = data.ListBooksCreatedBy("Last Name 2");

			// Assert.
			// Check that the expected behaviour is seen.
			Assert.That(books.Count, Is.EqualTo(1));
			Assert.That(books.First().Author.LastName, Is.EqualTo("Last Name 2"));
		}
	}
}
