using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TaskManager.Common
{
	public static class PrimitiveTypeParser
	{
		public static T Parse<T>(string valueAsString)
		{
			var converter = TypeDescriptor.GetConverter(typeof(T));
			var result = converter.ConvertFromString(valueAsString);
			return (T)result;
		}
	}
}
