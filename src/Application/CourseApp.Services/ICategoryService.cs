using System;
using CourseApp.DataTransferObject.Responses;

namespace CourseApp.Services
{
	public interface ICategoryService
	{
        IEnumerable<CategoryDisplayResponse> GetCategoriesForList();

    }
}

