using System.Runtime.Serialization;


namespace Grpc.Dtos
{
    [DataContract]
    public class UserIdDto
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
    }
}
