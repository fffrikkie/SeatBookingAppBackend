using MediatR;
using SeatBookingApp.Domain.Entities;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Bookings.GetBookings
{
    public class GetBookingsCommandHandler : IRequestHandler<GetBookingsCommand, List<Booking>>
    {
        private readonly IBookingsRepository _bookingsRepository;
        public GetBookingsCommandHandler(IBookingsRepository bookingsRepository)
        {
            _bookingsRepository = bookingsRepository ?? throw new ArgumentNullException(nameof(bookingsRepository));
        }

        public async Task<List<Booking>> Handle(GetBookingsCommand command, CancellationToken cancellationToken)
        {
            return await _bookingsRepository.GetBookings(cancellationToken);
        }
    }
}
