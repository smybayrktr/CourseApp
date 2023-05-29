using System;
using AutoMapper;
using CourseApp.DataTransferObject.Responses;
using CourseApp.Entities;

namespace CourseApp.Services.Extensions
{
	public static class MappingExtensions
	{
		public static T ConvertToDto<T>(this IEnumerable<Course> courses, IMapper mapper) {
			return mapper.Map<T>(courses);
		}

        //2.Yol
        public static IEnumerable<CourseDisplayResponse> ConvertToDisplayResponses(this IEnumerable<Course> courses, IMapper mapper)
		{
			return mapper.Map<IEnumerable<CourseDisplayResponse>>(courses);
		}

		public static IEnumerable<CategoryDisplayResponse> ConvertToDto(this IEnumerable<Category> categories, IMapper mapper)
		{
			return mapper.Map<IEnumerable<CategoryDisplayResponse>>(categories);
		}
    }

}

