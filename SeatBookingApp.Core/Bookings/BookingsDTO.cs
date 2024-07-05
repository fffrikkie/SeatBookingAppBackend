using AutoMapper;
using SeatBookingApp.Application.Common.Mappings;
using SeatBookingApp.Domain.Entities;

namespace SeatBookingApp.Application.Bookings
{
    public class BookingsDTO : IMapFrom<Booking>
    {
        public BookingsDTO() { }

        public static BookingsDTO Create(
            int id,
            int tripId,
            string travellerName,
            int numberOfSeatsBooked
            )
        { 
            return new BookingsDTO
            {
                Id = id,
                TripId = tripId,
                TravellerName = travellerName,
                NumberOfSeatsBooked = numberOfSeatsBooked
            };
        }
        public int Id { get; set; }
        public int TripId { get; set; }
        public string? TravellerName { get; set; }
        public int NumberOfSeatsBooked { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Booking, BookingsDTO>();
        }
    }
}
