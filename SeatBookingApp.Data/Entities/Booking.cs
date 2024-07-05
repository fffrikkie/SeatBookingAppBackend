using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SeatBookingApp.Domain.Entities
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TripId { get; set; }
        public string? TravellerName { get; set; }
        public int NumberOfSeatsBooked { get; set; }
    }
}
