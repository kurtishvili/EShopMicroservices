namespace Shopping.Web.Pages
{
    public class ProductDetailModel(IBasketService basketService, ICatalogService catalogService, ILogger<ProductDetailModel> logger) : PageModel
    {
        public ProductModel Product { get; set; } = default!;

        [BindProperty]
        public string Color { get; set; } = default!;

        [BindProperty]
        public int Quantity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid productId)
        {
            var response = await catalogService.GetProduct(productId);
            Product = response.Product;

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
                Quantity = Quantity,
                Price = productResponse.Product.Price,
                Color = Color,
            });

            var result = await basketService.StoreBasket(new StoreBasketRequest(basket));

            return RedirectToPage("Cart");
        }
    }
}
