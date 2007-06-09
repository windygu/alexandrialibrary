using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Data;

namespace Alexandria
{
	public interface IStorageEngine
	{
		T Lookup<T>(Guid id) where T: class,IPersistant;
		void Save(IPersistant record);
		void Delete(IPersistant record);
	}
}