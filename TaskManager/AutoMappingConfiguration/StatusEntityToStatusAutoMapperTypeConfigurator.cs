using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskManager.Common.TypeMapping;
using TaskManager.Data.Entities;

namespace TaskManager.AutoMappingConfiguration
{
	public class StatusEntityToStatusAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
	{
		public void Configure()
		{
			Mapper.CreateMap<Status, Models.Status>();
		}
	}
}