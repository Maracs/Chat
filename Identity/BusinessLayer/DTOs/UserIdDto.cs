using System.Runtime.Serialization;

namespace Grpc.DTOs
{
    [DataContract]
    public class UserIdDto
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
    }
}
