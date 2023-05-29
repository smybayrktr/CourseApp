using System;
using CourseApp.DataTransferObject.Responses;
using CourseApp.Entities;

namespace CourseApp.Services
{
	public interface ICourseService
	{
		IEnumerable<CourseDisplayResponse> GetCourseDisplayResponses();

        IEnumerable<CourseDisplayResponse> GetCoursesByCategory(int categoryId);

        CourseDisplayResponse GetCourse(int id);
    }
}

