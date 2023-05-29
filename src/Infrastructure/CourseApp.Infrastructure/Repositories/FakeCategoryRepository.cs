using System;
using CourseApp.Entities;

namespace CourseApp.Infrastructure.Repositories
{
    public class FakeCategoryRepository : ICategoryRepository
    {
        private List<Category> _categories;

        public FakeCategoryRepository()
        {
            _categories = new() {
            new() {
                Id =1, Name=".Net"
            },
              new() {
                Id =2, Name=".Net Core"
            },
                new() {
                Id =3, Name="Java"
            }

            };
        }

        public Category Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Category?> GetAll()
        {
            return _categories;
        }

        public Task<IList<Category?>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category?> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

