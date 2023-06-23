using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public class UserDomain : IUserDomain
{
    private IUserInfrastructure _userInfrastructure;

    public UserDomain(IUserInfrastructure userInfrastructure)
    {
        _userInfrastructure = userInfrastructure;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _userInfrastructure.GetAllAsync();
    }

    public User GetById(int id)
    {
        ExistsById(id);
        return _userInfrastructure.GetById(id);
    }

    public int Login(User user)
    {
        try
        {
            if (_userInfrastructure.ExistsByEmailAndPassword(user.Email, user.Password))
                return _userInfrastructure.GetUserIdByEmailAndPassword(user);
        }
        catch (Exception e)
        {
            throw new Exception("No se pudo realizar el pedido");
        }

        throw new Exception("El usuario o la contraseña esta mal");
    }


    public bool save(User value)
    {
        if (ExistsByEmailValidation(value.Email)) throw new Exception("A user already exists with this email");
        IsValidSave(value);
        value.DateCreated = DateTime.Now;
        return _userInfrastructure.save(value);
    }

    public bool update(int id, UserDto value)
    {
        if (!ExistsByIdValidation(id)) throw new Exception("The user doesnt exist");
        IsValidUpdate(value);
        if (!AllowedEmailUpdate(id, value)) throw new Exception("A user already exists with this email");
        return _userInfrastructure.update(id, value);
    }

    public bool delete(int id)
    {
        ExistsById(id);
        return _userInfrastructure.delete(id);
    }

    public async Task<User> GetByUsername(string username)
    {
        return await _userInfrastructure.GetByUsername(username);
    }

    private static void IsValidSave(User user)
    {
        if (user.Name.Length == 0) throw new Exception("Name is required");
        if (user.Email.Length == 0) throw new Exception("Email is required");
        if (user.Password.Length == 0) throw new Exception("Password is required");
        if (user.BirthDate.ToString().Length == 0) throw new Exception("BirthDate is required");
        if (user.Name.Length > 50) throw new Exception("Name has to be less than 50 characters");
        if (user.Email.Length > 50) throw new Exception("Email has to be less than 50 characters");
        if (user.Phone.Length > 15) throw new Exception("Phone has to be less than 15 characters");
        if (user.BirthDate > DateTime.Now.AddYears(-15)) throw new Exception("User has to be at least 15 years old");
    }

    private bool AllowedEmailUpdate(int id, UserDto user)
    {
        if (_userInfrastructure.ExistsByIdAndEmail(id, user.Email)) return true;
        if (ExistsByEmailValidation(user.Email)) throw new Exception("A user already exists with this email");
        return true;
    }


    private bool ExistsByEmailValidation(string email)
    {
        return _userInfrastructure.ExistsByEmail(email);
    }


    private bool ExistsByIdValidation(int id)
    {
        return _userInfrastructure.ExistsById(id);
    }

    private static void IsValidUpdate(UserDto user)
    {
        if (user.Name.Length > 50) throw new Exception("Name has to be less than 50 characters");
        if (user.Email.Length > 50) throw new Exception("Email has to be less than 50 characters");
        if (user.Phone.Length > 15) throw new Exception("Phone has to be less than 15 characters");
    }

    private bool ExistsById(int id)
    {
        try
        {
            var user = _userInfrastructure.GetById(id);
            return true;
        }
        catch (Exception e)
        {
            throw new Exception("No existe usuario con el id de" + id);
        }
    }
}