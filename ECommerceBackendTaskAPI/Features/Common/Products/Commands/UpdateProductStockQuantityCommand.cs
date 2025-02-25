namespace ECommerceBackendTaskAPI.Features.Common.Products.Commands
{
    public record UpdateProductStockQuantityCommand(int ProductId, int Quantity) : IRequest<ResultDto<bool>>;

    public class UpdateProductStockQuantityCommandHandler : BaseRequestHandler<Product, UpdateProductStockQuantityCommand, ResultDto<bool>>
    {
        public UpdateProductStockQuantityCommandHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {

         }

        public override async Task<ResultDto<bool>> Handle(UpdateProductStockQuantityCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId);
            if (product is null)
            {
                return ResultDto<bool>.Faliure(ErrorCode.NotFound, "Product not found");
            }

            product.StockQuantity += request.Quantity;
            _repository.UpdateIncluded(product, nameof(Product.StockQuantity));

            return ResultDto<bool>.Sucess(true, "Stock updated successfully");
        }
    }
}
