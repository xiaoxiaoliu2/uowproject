using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using uowpublic.Models;
using uowpublic.Data;

namespace uowpublic.Services
{
    public interface ICourseService
    {
        Task CreateAsync(Course newCourse);
        Task<List<Course>> GetAsync();
        Task<Course?> GetAsync(int id);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, Course updatedCourse);
    }

    public class CourseService : ICourseService
    {
        private readonly DatabaseContext _context;

        public CourseService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Course newCourse)
        {
            _context.Course.Add(newCourse);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Course>> GetAsync()
        {
            return await _context.Course.ToListAsync();
        }

        public async Task<Course?> GetAsync(int id)
        {
            return await _context.Course.FindAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course != null)
            {
                _context.Course.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, Course updatedCourse)
        {
            var course = await _context.Course.FindAsync(id);
            if (course != null)
            {
                course.Name = updatedCourse.Name;
                course.Dept = updatedCourse.Dept;
                course.Description = updatedCourse.Description;
                course.IsDeleted = updatedCourse.IsDeleted;
                await _context.SaveChangesAsync();
            }
        }
    }
}
