using System;
using System.Linq.Expressions;
using CourseApp.Entities;

namespace CourseApp.Infrastructure.Repositories
{
	public interface ICourseRepository: IRepository<Course>
	{
        IEnumerable<Course> GetCoursesByCategory(int categoryId);
        IEnumerable<Course> GetCoursesByName(string name);


    }
}

