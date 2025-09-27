using Backend.Domains.User.Domain.Models.Dto;
using Backend.Domains.User.Domain.Models.Entities;

namespace Backend.Domains.User.Application.Mapping;

public class UserMapper : IUserMapper
{
    public UserGetDto MapUserToGetDto(UserEntity userEntity)
    {
        return new UserGetDto
        {
            Id = userEntity.Id,
            FirstName = userEntity.FirstName,
            LastName = userEntity.LastName,
            UserName = userEntity.UserName,
            IsActive = userEntity.IsActive,
            IsInitialUser = userEntity.IsInitialUser,
        };
    }
}
