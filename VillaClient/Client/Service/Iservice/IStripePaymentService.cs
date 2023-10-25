using Models;

namespace VillaClient.Client.Service.Iservice
{
    public interface IStripePaymentService
    {
        public Task<SuccessModel> CheckOut(StripePaymentDto stripePaymentDto);
    }
}
