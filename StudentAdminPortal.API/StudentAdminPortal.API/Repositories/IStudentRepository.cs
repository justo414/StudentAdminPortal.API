using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DomainModels;
using System.Collections.Generic;
using Gender = StudentAdminPortal.API.DataModels.Gender;
using Student = StudentAdminPortal.API.DataModels.Student;

namespace StudentAdminPortal.API.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> getStudentsAsync();

        Task<Student> getStudentAsync(Guid studentId);

        Task<List<Gender>> getGenderAsync();

        Task<bool> Exists(Guid studentId);

        Task<Student> UpdateStudent(Guid studentId, Student request);

        Task<Student> DeleteStudent(Guid studentId);
    }
}
