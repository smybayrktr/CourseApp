﻿using System;
using CourseApp.DataTransferObject.Responses;

namespace CourseApp.Mvc.Models
{
    public class PaginationCourseViewModel
    {
        public IEnumerable<CourseDisplayResponse> Courses { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}

