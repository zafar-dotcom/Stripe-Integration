using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe_Integration.Contracts;
using Stripe_Integration.Models.Stripe;

namespace Stripe_Integration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly IStripeAppService _stripe;

        public StripeController(IStripeAppService stripe)
        {
            _stripe = stripe;
        }

        [Route("addcustomer")]
        [HttpPost]
        public async Task<ActionResult<StripeCustomer>> AddStripeCustomers([FromBody] AddStripeCustomer customer ,CancellationToken ct)
        {
            StripeCustomer createcustomer=await _stripe.AddStripeCustomerAsyncs(customer, ct);   
            return StatusCode(StatusCodes.Status200OK, createcustomer);
        }

        [Route("addpayment")]
        [HttpPost]
        public async Task<ActionResult<StripePayment>> AddStripePAyment([FromBody] AddStripePayment payment,CancellationToken ct)
        {
            StripePayment createpayment=await _stripe.AddStripePaymentAsync(payment, ct);
            return StatusCode(StatusCodes.Status200OK, createpayment);
        }
    }
}
