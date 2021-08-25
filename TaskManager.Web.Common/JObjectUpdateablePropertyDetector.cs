using System;
using System.Collections.Generic;
///NTS: Uninstall then reinstall it's being mean again. Don't forget to restart VS to. 
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace TaskManager.Web.Common
{
	public class JObjectUpdateablePropertyDetector : IUpdateablePropertyDetector
	{
		public IEnumerable<string> GetNamesOfPropertiesToUpdate<TTargetType>(object objectContainingUpdatedData)
		{
			var objectDataAsJObject = (JObject)objectContainingUpdatedData;

			var propertyInfos = typeof(TTargetType).GetProperties();

			var modifiablePropertyInfos = propertyInfos.Where(x =>
			{
				var editableAttribute = x.GetCustomAttributes(typeof(EditableAttribute)).FirstOrDefault() as EditableAttribute;

				return editableAttribute != null && editableAttribute.AllowEdit;
			});

			var namesOfSuppliedProperties = objectDataAsJObject.Properties().Select(x => x.Name);
			
			return modifiablePropertyInfos.Select(x => x.Name).Where(x => namesOfSuppliedProperties.Contains(x, StringComparer.InvariantCultureIgnoreCase));
		}
	}
}
