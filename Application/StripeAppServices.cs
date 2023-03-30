using Stripe;
using Stripe_Integration.Contracts;
using Stripe_Integration.Models.Stripe;

namespace Stripe_Integration.Application
{
    public class StripeAppServices : IStripeAppService
    {
        private readonly CustomerService _customerservices;
        private readonly ChargeService _chargeservices;     
        private readonly TokenService _tokenservices;

        public StripeAppServices(ChargeService chargeservices, CustomerService customerservices, TokenService tokenservices)
        {
            _chargeservices = chargeservices;
            _customerservices = customerservices;
            _tokenservices = tokenservices;
        }

        public async Task<StripeCustomer> AddStripeCustomerAsyncs(AddStripeCustomer customer, CancellationToken ct)
        {
            // Set Stripe Token options based on customer data
            TokenCreateOptions tokenoption = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Name = customer.Name,
                    Number = customer.CreditCard.CardNumber,
                    ExpYear =customer.CreditCard.ExpirationYear,
                    ExpMonth= customer.CreditCard.ExpirationMonth,
                    Cvc= customer.CreditCard.Cvc

                }

            };

            //Create new stripe token
            Token stripetoken=await _tokenservices.CreateAsync(tokenoption,null,ct);

            // Set customer option using
            CustomerCreateOptions customeroption = new CustomerCreateOptions
            {
                Name = customer.Name,
                Email = customer.Email,
                Source = stripetoken.Id
            };

            //Create customer at stripe
            Customer createcustomer = await _customerservices.CreateAsync(customeroption, null, ct);

            return new StripeCustomer(createcustomer.Name, createcustomer.Email, createcustomer.Id);
        }

        public async Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment, CancellationToken ct)
        {
            // Set the options for the payment we would like to create at Stripe

            ChargeCreateOptions chargeoption = new ChargeCreateOptions
            {
                Customer = payment.CustomerId,
                ReceiptEmail = payment.ReciptEmail,
                Description = payment.Description,
                Currency = payment.Currency,
                Amount = payment.Amount
            };


            //create payment
            var createdPayment = await _chargeservices.CreateAsync(chargeoption, null, ct);

            // Return the payment to requesting method
            return new StripePayment(
              createdPayment.CustomerId,
              createdPayment.ReceiptEmail,
              createdPayment.Description,
              createdPayment.Currency,
              createdPayment.Amount,
              createdPayment.Id);


        }
    }
}
