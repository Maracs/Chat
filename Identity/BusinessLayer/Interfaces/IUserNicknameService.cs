using BusinessLayer.DTOs;
using System.ServiceModel;

namespace BusinessLayer.Interfaces
{
    [ServiceContract]
    public interface IUserNicknameService
    {
        [OperationContract]
        Task<UserNicknameDto> GetUserNicknameAsync(UserIdDto id);
    }
}
