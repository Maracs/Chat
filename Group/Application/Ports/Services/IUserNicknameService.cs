using Grpc.Dtos;
using System.ServiceModel;

namespace Grpc.Interfaces
{
    [ServiceContract]
    public interface IUserNicknameService
    {
        [OperationContract]
        Task<UserNicknameDto> GetUserNicknameAsync(UserIdDto id);
    }
}
