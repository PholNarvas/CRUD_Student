public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<IEnumerable<Student>> GetAllStudentAsync()
    {
        return await _studentRepository.GetAllStudentAsync();
    }

    public async Task<bool> CreateStudentAsync(Student student)
    {
        return await _studentRepository.CreateStudentAsync(student);
    }

    public async Task<bool> UpdateStudentAsync(Student student)
    {
        return await _studentRepository.UpdateStudentAsync(student);
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        return await _studentRepository.GetStudentByIdAsync(id);
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        return await _studentRepository.DeleteStudentAsync(id);
    }

    public async Task<IEnumerable<Student>> SearchStudentAsync(string search, string value)
    {
    return await _studentRepository.SearchStudentAsync(search, value);
    }
    
 }

