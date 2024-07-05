using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SeatBookingApp.Domain.Entities
{
    public class Vehicle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? OwnerName { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? Variant { get; set; }
        public int? RegistrationYear { get; set; }
        public int? NumberOfSeats { get; set; }
    }
}
