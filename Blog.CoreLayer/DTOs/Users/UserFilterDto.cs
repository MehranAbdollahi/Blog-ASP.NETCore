using System.Collections.Generic;
using Blog.CoreLayer.Utilities;

namespace Blog.CoreLayer.DTOs.Users
{
    public class UserFilterDto:BasePagination
    {
        public List<UserDto> Users { get; set; }
    }

}