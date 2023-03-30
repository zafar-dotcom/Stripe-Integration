namespace Stripe_Integration.Models.Stripe
{
    public record AddStripeCustomer
    (
        string Name,
        string Email,
        AddStripeCard CreditCard
        );
    
}
