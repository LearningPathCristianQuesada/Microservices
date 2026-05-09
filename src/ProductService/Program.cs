var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var products = new List<Product>
{
    new(1, "Laptop", 2500),
    new(2, "Mouse", 80),
    new(3, "Keyboard", 150)
};

app.MapGet("/products", () =>
{
    return Results.Ok(products);
});

app.MapGet("/products/{id:int}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);

    return product is null
        ? Results.NotFound(new { message = "Product not found" })
        : Results.Ok(product);
});

app.MapPost("/products", (CreateProductRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Name))
        return Results.BadRequest(new { message = "Product name is required" });

    if (request.Price <= 0)
        return Results.BadRequest(new { message = "Product price must be greater than 0" });

    var newId = products.Any() ? products.Max(p => p.Id) + 1 : 1;

    var product = new Product(newId, request.Name, request.Price);
    products.Add(product);

    return Results.Created($"/products/{product.Id}", product);
});

app.Run();

record Product(int Id, string Name, decimal Price);

record CreateProductRequest(string Name, decimal Price);