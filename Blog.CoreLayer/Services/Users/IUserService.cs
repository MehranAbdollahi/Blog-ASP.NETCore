using Blog.CoreLayer.DTOs.Users;
using Blog.CoreLayer.Utilities;
using Blog.DataLayer.Entities;

namespace Blog.CoreLayer.Services.Users
{
    public interface IUserService
    {
        OperationResult EditUser(EditUserDto command);
        OperationResult RegisterUser(UserRegisterDto registerDto);
        UserDto LoginUser(LoginUserDto loginDto);
        UserDto GetUserById(int userId);
        UserFilterDto GetUsersByFilter(int pageId, int take);
    }
}