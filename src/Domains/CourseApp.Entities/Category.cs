using System;
namespace CourseApp.Entities
{
	public class Category: IEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string? Description { get; set; }

		public ICollection<Course> Courses { get; set; }
	}
}

