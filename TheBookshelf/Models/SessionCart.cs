using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using TheBookshelf.Infrastructure;

namespace TheBookshelf.Models
{
    public class SessionCart:Cart
    {

        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
            SessionCart? cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();

            cart.Session=session;
            return cart;
        }

        [JsonIgnore]
        public ISession? Session { get; set; }

        public override void AddItem(Book book, int quantity)
        {
            base.AddItem(book, quantity);
            Session?.SetJson("Cart", this);
        }

        public override void RemoveLine(Book book)
        {
            base.RemoveLine(book);                                  
            Session?.SetJson("Cart",this);
        }

        public override void Clear()
        {
            base.Clear();
            Session?.Remove("Cart");
        }
    }
}
