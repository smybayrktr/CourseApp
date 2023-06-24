using System;
using CourseApp.DataTransferObject.Requests;
using CourseApp.DataTransferObject.Responses;
using CourseApp.Entities;

namespace CourseApp.Services
{
    public interface ICourseService
    {
        IEnumerable<CourseDisplayResponse> GetCourseDisplayResponses();

        IEnumerable<CourseDisplayResponse> GetCoursesByCategory(int categoryId);

        CourseDisplayResponse GetCourse(int id);

        Task CreateCourseAsync(CreateNewCourseRequest createNewCourseRequest);

        Task UpdateCourse(UpdateCourseRequest updateCourseRequest);
        Task<bool> CourseIsExists(int courseId);
        Task DeleteAsync(int id);

        Task<UpdateCourseRequest> GetCourseForUpdate(int id);
        Task<IEnumerable<CourseDisplayResponse>> SearchByName(string name);


        Task<int> CreateCourseAndReturnIdAsync(CreateNewCourseRequest createNewCourseRequest);
    }

}