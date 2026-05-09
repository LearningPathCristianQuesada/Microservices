var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("ProductService", client =>
{
    client.BaseAddress = new Uri("http://localhost:5001");
});

var app = builder.Build();

var orders = new List<Order>();

app.MapGet("/orders", () =>
{
    return Results.Ok(orders);
});

app.MapPost("/orders", async (
    CreateOrderRequest request,
    IHttpClientFactory httpClientFactory) =>
{
    if (request.Quantity <= 0)
        return Results.BadRequest(new { message = "Quantity must be greater than 0" });

    var client = httpClientFactory.CreateClient("ProductService");

    var product = await client.GetFromJsonAsync<ProductDto>(
        $"/products/{request.ProductId}");

    if (product is null)
        return Results.NotFound(new { message = "Product does not exist" });

    var newId = orders.Any() ? orders.Max(o => o.Id) + 1 : 1;

    var order = new Order(
        newId,
        request.ProductId,
        request.Quantity,
        product.Price * request.Quantity
    );

    orders.Add(order);

    return Results.Created($"/orders/{order.Id}", order);
});

app.Run();

record Order(
    int Id,
    int ProductId,
    int Quantity,
    decimal TotalPrice
);

record CreateOrderRequest(
    int ProductId,
    int Quantity
);

record ProductDto(
    int Id,
    string Name,
    decimal Price
);