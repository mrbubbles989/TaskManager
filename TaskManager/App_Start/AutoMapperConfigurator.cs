using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskManager.Common.TypeMapping;

namespace TaskManager.App_Start
{
	public class AutoMapperConfigurator
	{
		public void Configure (IEnumerable<IAutoMapperTypeConfigurator> autoMapperTypeconfigurations)
		{
			autoMapperTypeconfigurations.ToList().ForEach(x => x.Configure());

			Mapper.AssertConfigurationIsValid();
		}
	}
}