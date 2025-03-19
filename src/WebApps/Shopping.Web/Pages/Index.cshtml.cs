namespace Shopping.Web.Pages
{
    public class IndexModel(ICatalogService catalogService, IBasketService basketService, ILogger<IndexModel> logger) : PageModel
    {
        public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation("index page visited");

            var result = await catalogService.GetProducts();
            ProductList = result.Products;

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            logger.LogInformation("add to cart button clicked");

            var productResponse = await catalogService.GetProduct(productId);

            var basket = await basketService.LoadUserBasket();

            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productId,
                ProductName = productResponse.Product.Name,
                Quantity = 1,
                Price = productResponse.Product.Price,
                Color = "Black",
            });

            var result = await basketService.StoreBasket(new StoreBasketRequest(basket));

            return RedirectToPage("Cart");
        }
    }
}