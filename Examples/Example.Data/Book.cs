using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Data
{
	/// <summary>
	/// An example EF4 code first entity.
	/// </summary>
	public class Book
	{
		public int BookId { get; set; }
		public string Title { get; set; }
		public virtual Author Author { get; set; }
		public int YearPublished { get; set; }
	}
}
