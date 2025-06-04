using System.Data;
using Dapper;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class StudentRepository : IStudentRepository
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;

    public StudentRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("CrudConnection");
    }
    public async Task<IEnumerable<Student>> GetAllStudentAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<Student>("sp_AllStudents", commandType: CommandType.StoredProcedure);
        }
    }

        public async Task<bool> CreateStudentAsync(Student student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
            
               var result = await connection.ExecuteAsync("sp_InsertStudent", 
                   new
               {
                   FName = student.FName,
                   MName = student.MName,
                   LName = student.LName,
                   DepartmentID = student.DepartmentID,
                   ClassID = student.ClassID,
                   Email = student.Email

               }, commandType: CommandType.StoredProcedure);
                return result > 0;
            }

        }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            return await connection.QueryFirstOrDefaultAsync<Student>("sp_GetStudentID", new { StudentsID = id }, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<bool> UpdateStudentAsync(Student student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.ExecuteAsync("sp_UpdateStudent",
                 new
                 {   StudentsID = student.StudentsID,
                     FName = student.FName,
                     MName = student.MName,
                     LName = student.LName,
                     DepartmentID = student.DepartmentID,
                     ClassID = student.ClassID,
                     Email = student.Email

                 }, commandType: CommandType.StoredProcedure);
                return result > 0;
             }

        }

     public async Task<bool> DeleteStudentAsync (int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

            var result = await connection.ExecuteAsync(
                "sp_DeleteStudent",
                new
                {
                    StudentsID = id,
                }, commandType: CommandType.StoredProcedure);
           
            return result > 0;
            }
        }
       
      public async Task<IEnumerable<Student>> SearchStudentAsync(string search, string value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("SearchField", search);
                parameters.Add("SearchValue", value);

            return await connection.QueryAsync<Student>("sp_Search", parameters, commandType: CommandType.StoredProcedure);
         }
            
        }
        

    }

