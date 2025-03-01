using APBD_Cw1_s30673.Models;
using APBD_Cw1_s30673.Repositories;
using APBD_Cw1_s30673.Utils;

namespace APBD_Cw1_s30673.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IdGenerator _idGenerator;

    public UserService(IUserRepository userRepository, IdGenerator idGenerator)
    {
        _userRepository = userRepository;
        _idGenerator = idGenerator;
    }

    public Student AddStudent(string firstName, string lastName, string studentNumber, string faculty)
    {
        var student = new Student(_idGenerator.NextUserId(), firstName, lastName, studentNumber, faculty);
        _userRepository.Add(student);
        return student;
    }

    public Employee AddEmployee(string firstName, string lastName, string department, string position)
    {
        var employee = new Employee(_idGenerator.NextUserId(), firstName, lastName, department, position);
        _userRepository.Add(employee);
        return employee;
    }

    public List<User> GetAll()
    {
        return _userRepository.GetAll();
    }
}
