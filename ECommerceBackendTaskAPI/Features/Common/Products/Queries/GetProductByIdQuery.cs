
namespace ECommerceBackendTaskAPI.Features.Common.Products.Queries
{
    public record GetProductByIdQuery(int ProductId) :IRequest<ResultDto<Product>>;

    public class GetProductByIdQueryHandler : BaseRequestHandler<Product, GetProductByIdQuery, ResultDto<Product>>
    {
        public GetProductByIdQueryHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId);
            if (product is null) { 
                return  ResultDto<Product>.Faliure(ErrorCode.NotFound, "This product Not Found");
            }

            return ResultDto<Product>.Sucess(product);
        }
    }
}
