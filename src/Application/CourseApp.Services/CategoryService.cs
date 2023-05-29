using System;
using AutoMapper;
using CourseApp.DataTransferObject.Responses;
using CourseApp.Infrastructure.Repositories;
using CourseApp.Services.Extensions;

namespace CourseApp.Services
{
	public class CategoryService: ICategoryService
	{
		private readonly IMapper _mapper;
		private readonly ICategoryRepository _categoryRepository;

		public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
		{
			_mapper = mapper;
			_categoryRepository = categoryRepository;
		}
		public IEnumerable<CategoryDisplayResponse> GetCategoriesForList()
		{
			var items = _categoryRepository.GetAll();
			var responses = items.ConvertToDto(_mapper);
			return responses;
		}

    }
}

