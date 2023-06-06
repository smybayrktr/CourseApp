﻿using System;
using CourseApp.DataTransferObject.Responses;

namespace CourseApp.Mvc.CacheTools
{
	public class CacheDataInfo
	{
		public IEnumerable<CourseDisplayResponse> Courses { get; set; }

		public DateTime CacheTime { get; set; }
	}
}

