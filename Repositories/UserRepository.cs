using APBD_Cw1_s30673.Models;

namespace APBD_Cw1_s30673.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _userList = new();

    public void Add(User user)
    {
        _userList.Add(user);
    }

    public List<User> GetAll()
    {
        return _userList;
    }

    public User? GetById(string id)
    {
        return _userList.FirstOrDefault(x => x.Id == id);
    }
}
