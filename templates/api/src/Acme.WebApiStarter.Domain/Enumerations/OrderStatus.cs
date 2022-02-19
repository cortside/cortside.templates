namespace Acme.WebApiStarter.Domain.Enumerations {
    public enum OrderStatus {
        Submitted,
        AwaitingValidation,
        StockConfirmed,
        Paid,
        Shipped,
        Cancelled
    }
}
