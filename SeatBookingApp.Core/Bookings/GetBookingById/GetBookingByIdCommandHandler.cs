using MediatR;
using SeatBookingApp.Domain.Entities;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Bookings.GetBookingById
{
    public class GetBookingByIdCommandHandler : IRequestHandler<GetBookingByIdCommand, Booking>
    {
        private readonly IBookingsRepository _bookingsRepository;
        public GetBookingByIdCommandHandler(IBookingsRepository bookingsRepository)
        {
            _bookingsRepository = bookingsRepository ?? throw new ArgumentNullException(nameof(bookingsRepository));
        }

        public async Task<Booking> Handle(GetBookingByIdCommand command, CancellationToken cancellationToken)
        {
            return await _bookingsRepository.GetBookingById(command.Id, cancellationToken);
        }
    }
}
