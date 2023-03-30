namespace Stripe_Integration.Models.Stripe
{
    public record AddStripePayment
    (
        string CustomerId,
        string ReciptEmail,
        string Description,
        string Currency,
        long Amount

        
        );
}
