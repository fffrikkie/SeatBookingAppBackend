using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SeatBookingApp.Domain.Entities
{
    public class Trip
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? VehicleId { get; set; }
        public string? StartDestination{ get; set; }
        public string? EndDestination { get; set; }
        public int TotalSeats { get; set; }
        public int TotalAvailableSeats { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTimeOffset DepartureDateTime { get; set; }
    }
}
