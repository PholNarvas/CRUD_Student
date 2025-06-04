
    public interface IStudentService
    {
    Task<IEnumerable<Student>> GetAllStudentAsync();
    Task<bool> CreateStudentAsync(Student student);
    Task<bool> UpdateStudentAsync(Student student);
    Task<Student> GetStudentByIdAsync(int id);
    Task<bool> DeleteStudentAsync(int id);
    Task<IEnumerable<Student>> SearchStudentAsync(string search, string value);
    }

