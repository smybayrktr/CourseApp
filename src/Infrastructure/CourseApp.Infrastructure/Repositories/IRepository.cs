using System;
using System.Linq.Expressions;
using CourseApp.Entities;

namespace CourseApp.Infrastructure.Repositories
{
	public interface IRepository<T> where T : class, IEntity, new()
    {
		T Get(int id);
		Task<T?> GetAsync(int id);

		IList<T?> GetAll();
		Task<IList<T?>> GetAllAsync();

		
	}
}

