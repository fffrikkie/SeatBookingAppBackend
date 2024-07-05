using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SeatBookingApp.Application.Common.Mappings;
using SeatBookingApp.Domain.Entities;

namespace SeatBookingApp.Application.Vehicles
{
    public class VehiclesDTO : IMapFrom<Vehicle>
    {
        public VehiclesDTO() { }

        public static VehiclesDTO Create(
            string id,
            string ownerName,
            string displayName,
            string make,
            string model,
            string variant,
            int registrationYear,
            int numberOfSeats
        )
        { 
            return new VehiclesDTO
            {
                Id = id,
                OwnerName = ownerName,
                DisplayName = displayName,
                Make = make,
                Model = model,
                Variant = variant,
                RegistrationYear = registrationYear,
                NumberOfSeats = numberOfSeats
            };
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? OwnerName { get; set; }
        public string? DisplayName { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? Variant { get; set; }
        public int? RegistrationYear { get; set; }
        public int? NumberOfSeats { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Vehicle, VehiclesDTO>();
        }
    }
}
