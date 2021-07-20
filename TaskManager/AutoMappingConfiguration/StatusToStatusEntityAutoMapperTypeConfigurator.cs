using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskManager.Common.TypeMapping;
using TaskManager.Web.Api.Models;

namespace TaskManager.AutoMappingConfiguration
{
	public class StatusToStatusEntityAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
	{
		public void Configure()
		{
			Mapper.CreateMap<Status, Data.Entities.Status>()
				.ForMember(opt => opt.Version, x => x.Ignore());
		}
	}
}