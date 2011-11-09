using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Data
{
	/// <summary>
	/// An example EF4 code first entity.
	/// </summary>
	public class Author
	{
		public int AuthorId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
	}
}
