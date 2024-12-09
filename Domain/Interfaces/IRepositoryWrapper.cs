using Domain.Interfaces.Repository;


namespace Domain.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        ITicketRepository Ticket { get; }
        ISpecialRepository Special { get; }
        IReviewRepository Review { get; }
        IRoleRepository Role { get; }
        IPassengerRepository Passenger { get; }
        INotificationRepository Notification { get; }
        IFlightRepository Flight { get; }
        ICharterRepository Charter { get; }
        IChangerRepository Changer { get; }
        IBookingRepository Booking { get; }

        IBaggageRepository Baggage { get; }
        IAirportRepository Airport { get; }


        Task Save();
    }
}
