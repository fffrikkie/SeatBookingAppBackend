using AutoMapper;

namespace SeatBookingApp.Application.Common.Mappings
{
    interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}
