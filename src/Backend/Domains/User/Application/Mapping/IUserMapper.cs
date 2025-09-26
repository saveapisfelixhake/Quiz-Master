using Backend.Domains.User.Domain.Models.Dto;
using Backend.Domains.User.Domain.Models.Entities;

namespace Backend.Domains.User.Application.Mapping;

public interface IUserMapper
{
    UserGetDto MapUserToGetDto(UserEntity userEntity);
}
