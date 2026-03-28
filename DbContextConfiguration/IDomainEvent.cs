namespace DbContextConfiguration;

public interface IDomainEvent { }

public interface IHasDomainEvents
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
