using Common;
using Microsoft.AspNetCore.Mvc;
using Models;
using NLog;
using Stripe.Checkout;

namespace LibertyVilla_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StripePaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();
        public StripePaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Create(StripePaymentDto stripePaymentdto)
        {
              try
              {
                logger.Info($"Sarting payment gateway");
                var domain = SD.Local_Liberty_Client_Url;
                  var options = new SessionCreateOptions
                  {
                      PaymentMethodTypes = new List<string>
                      {
                          "card"
                      },
                      LineItems = new List<SessionLineItemOptions>
                      {
                          new SessionLineItemOptions
                          {
                              PriceData = new SessionLineItemPriceDataOptions
                              {
                                  UnitAmount = stripePaymentdto.Amount, //convert to cent
                                  Currency = "usd",
                                  ProductData = new SessionLineItemPriceDataProductDataOptions
                                  {
                                      Name = stripePaymentdto.ProductName
                                  }
                              },
                              Quantity = 1
                          }
                      },
                      Mode = "payment",
                      SuccessUrl = domain + "transactionSuccessful?session_id={{CHECKOUT_SESSION_ID}}",
                      CancelUrl = domain + stripePaymentdto.ReturnUrl
                  };
                  var service = new SessionService();
                  Session  session = await service.CreateAsync(options);
                logger.Info($"Payment with session id {session.Id} is successfully initialized");

                return Ok(new SuccessModel()
                  {
                      Data = session.Id,
                  });
              }
              catch (Exception ex) 
              {
                logger.Error($"Exception Type {ex.GetType()} and the exception message says {ex.Message}. An error occured while using payment gatway");
                return BadRequest(new ErrorModel { ErrorMessage = ex.Message });
              }
          //  return Ok();
        }
    }
}
