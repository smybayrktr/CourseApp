using System;
using AutoMapper;
using CourseApp.DataTransferObject.Requests;
using CourseApp.DataTransferObject.Responses;
using CourseApp.Entities;

namespace CourseApp.Services.Mappings
{
	public class MapProfile: Profile
	{
		public MapProfile()
		{
			CreateMap<Course, CourseDisplayResponse>();
            CreateMap<Category, CategoryDisplayResponse>();
            CreateMap<CreateNewCourseRequest, Course>();

        }
    }
}

