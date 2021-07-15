using System;
using FluentNHibernate.Mapping;
using TaskManager.Data.Entities;

namespace TaskManager.Data.SqlServer
{
	/// <summary>
	/// This classes purpose is to prevent the uploading of dirty records to the database.
	/// To be used by all classes that implement the ClassMap implementation
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class VersionedClassMap<T> : ClassMap<T> where T : IVersionedEntity
	{
		protected VersionedClassMap()
		{
			Version(x => x.Version)
				.Column("ts")
				.CustomSqlType("Rowversion")
				.Generated.Always()
				.UnsavedValue("null");
		}

	}
}
