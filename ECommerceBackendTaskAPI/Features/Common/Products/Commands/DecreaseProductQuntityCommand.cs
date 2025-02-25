
namespace ECommerceBackendTaskAPI.Features.Common.Products.Commands
{
    public record DecreaseProductQuntityCommand(int ProductId , int DecreasingQuantity) : IRequest<ResultDto<bool>>;

    public class DecreaseProductQuntityCommandHandler : BaseRequestHandler<Product, DecreaseProductQuntityCommand, ResultDto<bool>>
    {
        public DecreaseProductQuntityCommandHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<bool>> Handle(DecreaseProductQuntityCommand request, CancellationToken cancellationToken)
        {
            var productResult = await _mediator.Send(new GetProductByIdQuery(request.ProductId));

            if (productResult.IsSuccess is false)
            {
                return ResultDto<bool>.Faliure(productResult.ErrorCode, productResult.Message);
            }
            var product = productResult.Data;
            var updatedProduct = new Product() 
            { ID = product.ID, StockQuantity = product.StockQuantity - request.DecreasingQuantity }; 

            _repository.UpdateIncluded(updatedProduct, nameof(Product.StockQuantity));

            return ResultDto<bool>.Sucess(true);
        }
    }
}
