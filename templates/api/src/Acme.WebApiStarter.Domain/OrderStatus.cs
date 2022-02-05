namespace Acme.WebApiStarter.Domain {
    public enum OrderStatus {
        Submitted,
        AwaitingValidation,
        StockConfirmed,
        Paid,
        Shipped,
        Cancelled
    }
}
