using System;
using CourseApp.DataTransferObject.Responses;
using CourseApp.Entities;

namespace CourseApp.Mvc.Models
{
	public class CourseCollection
	{
        public List<CourseItem> CourseItems { get; set; } = new List<CourseItem>();

        public void ClearAll() => CourseItems.Clear();
        public decimal TotalCoursePrice() => CourseItems.Sum(item => (decimal)item.Course.Price * item.Quantity); //Toplam fiyatı hesaplama

        public int TotalCoursesCount() => CourseItems.Sum(p => p.Quantity); //Sepette toplam kaç adet ürün var

        public void AddNewCourse(CourseItem courseItem)
        {
            var exists = CourseItems.FirstOrDefault(c => c.Course.Id == courseItem.Course.Id);
            if (exists != null)//Sepete daha önce eklenmiş mi onu kontrol ediyoruz.
            {
                //var existingCourse = CourseItems.FirstOrDefault(c => c.Course.Id == courseItem.Course.Id);
                exists.Quantity += courseItem.Quantity; //Eğer daha önce sepette varsa onun adetini arttırıyoruz
            }
            else
            {
                CourseItems.Add(courseItem);
            }

        }
    }

	public class CourseItem
	{
		public CourseDisplayResponse Course { get; set; }

		public int Quantity { get; set; }

		public bool? Coupon { get; set; }
	}
}

