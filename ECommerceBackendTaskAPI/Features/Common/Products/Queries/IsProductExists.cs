
namespace ECommerceBackendTaskAPI.Features.Common.Products.Queries
{
    public record IsProductExists(int ProductId) :IRequest<ResultDto<bool>>;

    public class IsProductExistsQueryHandler : BaseRequestHandler<Product, IsProductExists, ResultDto<bool>>
    {
        public IsProductExistsQueryHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<bool>> Handle(IsProductExists request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId);

            if (product is null) {
                ResultDto<bool>.Faliure(ErrorCode.NotFound, "This product Not Found"); 
            }

            return ResultDto<bool>.Sucess(true);
        }
    }
}
