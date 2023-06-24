using System;
using AutoMapper;
using CourseApp.DataTransferObject.Requests;
using CourseApp.DataTransferObject.Responses;
using CourseApp.Entities;
using CourseApp.Infrastructure.Repositories;
using CourseApp.Services.Extensions;

namespace CourseApp.Services
{
	public class CourseService: ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public IEnumerable<CourseDisplayResponse> GetCourseDisplayResponses()
        {
            var courses = _courseRepository.GetAll();
            /*
             * Entityden dto ya dönüştürcez.
             * Automapper ile mapleme yapcaz
             */
            var response = courses.ConvertToDto<IEnumerable<CourseDisplayResponse>>(_mapper);
            //2.Yol
            //var response = courses.ConvertToDisplayResponses(_mapper);
            return response;
        }

        public IEnumerable<CourseDisplayResponse> GetCoursesByCategory(int categoryId)
        {
            var courses = _courseRepository.GetCoursesByCategory(categoryId);
            var response = courses.ConvertToDto<IEnumerable<CourseDisplayResponse>>(_mapper);
            return response;
        }

        public CourseDisplayResponse GetCourse(int id)
        {
            var course = _courseRepository.Get(id);
            return _mapper.Map<CourseDisplayResponse>(course);
        }

        public async Task CreateCourseAsync(CreateNewCourseRequest createNewCourseRequest)
        {
            var course = _mapper.Map<Course>(createNewCourseRequest);
            await _courseRepository.CreateAsync(course);
        }

        public async Task UpdateCourse(UpdateCourseRequest updateCourseRequest)
        {
            var course = _mapper.Map<Course>(updateCourseRequest);
            await _courseRepository.UpdateAsync(course);

        }

        public async Task<bool> CourseIsExists(int courseId)
        {
            return await _courseRepository.IsExitsAsync(courseId);

        }

        public async Task<UpdateCourseRequest> GetCourseForUpdate(int id)
        {
            var course = await _courseRepository.GetAsync(id);
            return _mapper.Map<UpdateCourseRequest>(course);

        }

        public Task<UpdateCourseRequest> GetCourseForUpdateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            await _courseRepository.DeleteAsync(id);

        }
        public async Task<IEnumerable<CourseDisplayResponse>> SearchByName(string name)
        {
            var courses = await _courseRepository.GetCoursesByName(name);
            return courses.ConvertToDisplayResponses(_mapper);
        }
        public async Task<int> CreateCourseAndReturnIdAsync(CreateNewCourseRequest createNewCourseRequest)
        {
            var course = _mapper.Map<Course>(createNewCourseRequest);
            await _courseRepository.CreateAsync(course);
            return course.Id;
        }
    }
}

