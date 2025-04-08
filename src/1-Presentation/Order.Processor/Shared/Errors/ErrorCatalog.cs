using ErrorOr;

namespace Orders.Worker.Shared.Errors
{
    public static class ErrorCatalog
    {
        public static Error ResaleAlready => Error.Validation("CODE-1", "Resale is already.");
        public static Error ResaleNotFound => Error.Validation("CODE-2", "Resale not found.");
        public static Error MinimumQuantityNotReached => Error.Validation("CODE-3", "Minimum quantity not reached.");
        public static Error OrderAlready => Error.Validation("CODE-4", "Order is already.");
        public static Error OrderNotFound => Error.Validation("CODE-5", "Order not found.");

    }
}

