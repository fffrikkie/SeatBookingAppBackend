
using SeatBookingApp.Domain.Entities;

namespace SeatBookingApp.Domain.Repositories
{
    public interface IBookingsRepository
    {
        Task<List<Booking>> GetBookings(CancellationToken cancellationToken = default);
        Task<Booking> GetBookingById(string bookingId, CancellationToken cancellationToken = default);
        Task<List<Booking>> GetTripBookings(string tripId, CancellationToken cancellationToken = default);
        Task<string> RegisterTripBooking(Booking booking, CancellationToken cancellationToken = default);
        Task<string> DeleteBooking(Booking booking, CancellationToken cancellationToken = default);
        Task<string> UpdateBooking(Booking booking, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
