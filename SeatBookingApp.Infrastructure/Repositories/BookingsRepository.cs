using SeatBookingApp.Domain.Entities;
using SeatBookingApp.Domain.Repositories;
using SeatBookingApp.Infrastructure.Persistance;

namespace SeatBookingApp.Infrastructure.Repositories
{
    internal class BookingsRepository : IBookingsRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingsRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Booking>> GetBookings(CancellationToken cancellationToken = default)
        {
            return _context.Bookings.ToList();
        }

        public async Task<Booking> GetBookingById(string bookingId, CancellationToken cancellationToken = default)
        {
            return _context.Bookings.Where(booking => booking.Id == bookingId).FirstOrDefault();
        }

        public async Task<List<Booking>> GetTripBookings(string tripId, CancellationToken cancellationToken = default)
        {
            return _context.Bookings.Where(booking => booking.TripId == tripId).ToList();
        }

        public async Task<string> RegisterTripBooking(Booking booking, CancellationToken cancellationToken = default)
        {
            _context.Bookings.Add(booking);
            return booking.Id;
        }

        public async Task<string> DeleteBooking(Booking booking, CancellationToken cancellationToken = default)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync(cancellationToken);
            return booking.Id;
        }

        public async Task<string> UpdateBooking(Booking booking, CancellationToken cancellationToken = default)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync(cancellationToken);
            return booking.Id;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
