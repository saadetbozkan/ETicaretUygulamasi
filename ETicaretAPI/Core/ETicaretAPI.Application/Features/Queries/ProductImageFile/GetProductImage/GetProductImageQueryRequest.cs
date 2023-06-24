using MediatR;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImage
{
    public class GetProductImageQueryRequest : IRequest<List<GetProductImageQueryResponse>>
    {
        public string Id { get; set; }
    }
}
