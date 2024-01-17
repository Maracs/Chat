using System.Runtime.Serialization;

namespace Grpc.DTOs
{
    [DataContract]
    public class UserNicknameDto
    {
        [DataMember(Order = 1)]
        public string AccountName { get; set; }

        [DataMember(Order = 2)]
        public string NickName { get; set; }
    }
}
