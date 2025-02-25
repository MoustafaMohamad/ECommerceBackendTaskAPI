using ECommerceBackendTaskAPI.Common;

namespace ECommerceBackendTaskAPI.Features.Common.Customers.Queries
{
    public record IsCustomerExists(int CustomerId) : IRequest<ResultDto<bool>>;

    public class IsCustomerExistsQueryHandler : BaseRequestHandler<Customer, IsCustomerExists, ResultDto<bool>>
    {
        public IsCustomerExistsQueryHandler(RequestParameters<Customer> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<bool>> Handle(IsCustomerExists request, CancellationToken cancellationToken)
        {
            var customer = await _repository.FirstOrDefaultAsync(c => c.ID == request.CustomerId);
            if (customer is null)
            {
                return ResultDto<bool>.Faliure(ErrorCode.NotFound, "This customer Not Found");
            }
            return ResultDto<bool>.Sucess(true);
        }
    }
}
