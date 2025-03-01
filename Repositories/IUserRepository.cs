using APBD_Cw1_s30673.Models;

namespace APBD_Cw1_s30673.Repositories;

public interface IUserRepository
{
    void Add(User user);
    List<User> GetAll();
    User? GetById(string id);
}
