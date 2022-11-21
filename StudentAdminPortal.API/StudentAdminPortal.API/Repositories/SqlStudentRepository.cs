using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;
using System.Linq;

namespace StudentAdminPortal.API.Repositories
{
    public class SqlStudentRepository : IStudentRepository 
    {
        private readonly StudentAdminContext context;

        public SqlStudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }

        public StudentAdminContext Context { get; }

        public async Task<List<Student>> getStudentsAsync()
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }
    }
}
