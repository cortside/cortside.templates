namespace Acme.WebApiStarter.Data {
    public interface IRepository<T> { //where T : IAggregateRoot {
        IUnitOfWork UnitOfWork { get; }
    }
}
