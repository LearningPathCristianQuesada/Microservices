// namespace ProductService.Products.GetProducts;
//
// public class GetProductsEndpoint : ICarterModule
// {
//     public void AddRoutes(IEndpointRouteBuilder app)
//     {
//         app.MapGet("/products", async (ISender sender) =>
//         {
//             var result = await sender.Send(new GetProductsQuery());
//             var response = result.Adapt<GetProductsResult>();
//
//             return Results.Ok(response);
//         })
//         .WithName("GetProducts")
//         .Produces<GetProductsResult>(StatusCodes.Status200OK)
//         .ProducesProblem(StatusCodes.Status400BadRequest)
//         .WithSummary("Get Products summary")
//         .WithDescription("Get Products description");
//     }
// }