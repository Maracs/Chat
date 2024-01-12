using System.ServiceModel;

namespace Shared
{
    [ServiceContract]
    public interface IUserNicknameService
    {
        [OperationContract]
        Task<UserNicknameDto> GetUserNicknameAsync(UserIdDto id);
    }
}

