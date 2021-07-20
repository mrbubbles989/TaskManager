using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskManager.Common.TypeMapping;
using TaskManager.Data.Entities;

namespace TaskManager.AutoMappingConfiguration
{
	public class TaskEntityToTaskAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
	{ 
		public void Configure()
		{
			Mapper.CreateMap<Task, Models.Task>()
				.ForMember(opt => opt.Links, x => x.Ignore())
				.ForMember(opt => opt.Asignees, x => x.ResolveUsing<TaskAssigneesResolver>());
		}
	}
}