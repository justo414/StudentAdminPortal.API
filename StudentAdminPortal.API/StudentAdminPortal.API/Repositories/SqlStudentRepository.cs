﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Student>> getStudentsAsync()
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<Student> getStudentAsync(Guid studentId)
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(x => x.Id == studentId);
        }


    }
}
