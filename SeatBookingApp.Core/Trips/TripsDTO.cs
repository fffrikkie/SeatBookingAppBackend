using AutoMapper;
using SeatBookingApp.Application.Common.Mappings;
using SeatBookingApp.Domain.Entities;

namespace SeatBookingApp.Application.Trips
{
    public class TripsDTO : IMapFrom<Trip>
    {
        public TripsDTO() { }

        public static TripsDTO Create(
            string id,
            string vehicleId,
            string startDestination,
            string endDestination,
            int totalSeats,
            int totalAvailableSeats,
            DateTimeOffset departureDateTime
            )
        { 
            return new TripsDTO
            {
                Id = id,
                VehicleId = vehicleId,
                StartDestination = startDestination,
                EndDestination = endDestination,
                TotalSeats = totalSeats,
                TotalAvailableSeats = totalAvailableSeats,
                DepartureDateTime = departureDateTime
            };
        }
        public string? Id { get; set; }
        public string? VehicleId { get; set; }
        public string? StartDestination { get; set; }
        public string? EndDestination { get; set; }
        public int TotalSeats { get; set; }
        public int TotalAvailableSeats { get; set; }
        public DateTimeOffset DepartureDateTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Trip, TripsDTO>();
        }
    }
}
