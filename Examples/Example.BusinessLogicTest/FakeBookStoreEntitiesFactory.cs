using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Data;

namespace Example.BusinessLogicTest
{
	/// <summary>
	/// This class is simply used by the test to setup fake data.
	/// </summary>
	class FakeBookStoreEntitiesFactory : IFactory<IBookStoreEntities>
	{
		/// <summary>
		/// Creates an instance of the FakeDatabase and fills it with data.
		/// The FakeDatabase uses a static hashtable, so it maintains data 
		/// unless each InMemoryDbSet is cleared.
		/// </summary>
		/// <returns></returns>
		public IBookStoreEntities Create()
		{
			// Create the fake database.
			IBookStoreEntities entities = new FakeDatabase();
			
			// Populate it with fake data.
			DataInitialisation initialiser = new DataInitialisation();
			initialiser.Initialise(entities);

			return entities;
		}
	}
}
