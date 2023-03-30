namespace Stripe_Integration.Models.Stripe
{
    public record StripeCustomer
    (
        string Name,
        string Email,
        string CustomerId
        );
}
