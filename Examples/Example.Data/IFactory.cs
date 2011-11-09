using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Data
{
	/// <summary>
	/// A basic factory pattern interface.  A factory creates an instance 
	/// of a class when requested.
	/// </summary>
	/// <typeparam name="T">The type of object to create.</typeparam>
	public interface IFactory<T>
	{
		T Create();
	}
}
