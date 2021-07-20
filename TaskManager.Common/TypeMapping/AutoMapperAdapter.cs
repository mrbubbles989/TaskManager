using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace TaskManager.Common.TypeMapping
{
	public class AutoMapperAdapter : IAutoMapper
	{
		public T Map<T>(object objectToMap)
		{
			return Mapper.Map<T>(objectToMap);
		}
	}
}
