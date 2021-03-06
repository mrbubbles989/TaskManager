using AutoMapper;
using TaskManager.Common.TypeMapping;
using TaskManager.Data.Entities;

namespace TaskManager.AutoMappingConfiguration
{
	public class UserEntityToUserAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
	{
		public void Configure()
		{
			Mapper.CreateMap<User, Models.User>().ForMember(opt => opt.Links, x => x.Ignore());
		}
	}
}