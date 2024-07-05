using AutoMapper;
using SeatBookingApp.Application.Common.Mappings;
using SeatBookingApp.Domain.Entities;

namespace SeatBookingApp.Application.Bookings
{
    public class TripBookingsDTO : IMapFrom<Booking>
    {
        public TripBookingsDTO() { }

        public static TripBookingsDTO Create(
            Vehicle vehicleInfo,
            Trip tripInfo,
            List<Booking> bookings
            )
        { 
            return new TripBookingsDTO
            {
                Bookings = bookings,
                VehicleInfo = vehicleInfo,
                TripInfo = tripInfo
            };
        }
        public Vehicle VehicleInfo { get; set; }
        public Trip TripInfo { get; set; }
        public List<Booking> Bookings { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Booking, TripBookingsDTO>();
        }
    }
}
