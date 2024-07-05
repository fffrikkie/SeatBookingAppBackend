using System.Windows.Input;
using MediatR;

namespace SeatBookingApp.Application.Trips.GetTrips
{
    public class GetTripsCommand : IRequest<List<TripsDTO>>
    {
        // What gets passed in
    }
}
