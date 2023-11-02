public class Product
{
    public Product(string name, int price, string description, string category)
    {
        Name = name;
        Price = price;
        Description = description;
        Category = category;
    }

    public string Name { get; }
    public int Price { get; }
    public string Description { get; }
    public string Category { get; }
}

public class User
{
    public User(string login, string password)
    {
        Login = login;
        Password = password;
    }

    public string Login { get; }
    public string Password { get; }
}

public class Order
{
    public Order(IEnumerable<Product> products, int quantity, int totalPrice, OrderStatus status)
    {
        Products = products;
        Quantity = quantity;
        TotalPrice = totalPrice;
        Status = status;
    }

    public IEnumerable<Product> Products { get; }
    public int Quantity { get; }
    public int TotalPrice { get; }
    public OrderStatus Status { get; }
}
public interface ISearchable
{
    bool IsMatch(Product product) const;
}
public class Store
{
    private readonly List<Product> _products = new List<Product>();
    private readonly Dictionary<string, User> _users = new Dictionary<string, User>();
    private readonly List<Order> _orders = new List<Order>();

    public Store()
    {
        _products.Add(new Product("iPhone 13 Pro", 1000, "The best iPhone yet", "Smartphones"));
        _products.Add(new Product("MacBook Pro", 2000, "The most powerful MacBook", "Laptops"));
        _products.Add(new Product("AirPods Pro", 300, "The best wireless earbuds", "Headphones"));
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public void RemoveProduct(Product product)
    {
        _products.Remove(product);
    }

    public void AddUser(User user)
    {
        _users[user.Login] = user;
    }

    public void RemoveUser(User user)
    {
        _users.Remove(user.Login);
    }

    public void CreateOrder(User user, IEnumerable<Product> products, int quantity)
    {
        _orders.Add(new Order(products, quantity, 0, OrderStatus.New));
    }

    public void UpdateOrder(Order order)
    {
        _orders.Find(o => o.Id == order.Id).Value = order;
    }

    public void CompleteOrder(Order order)
    {
        _orders.Find(o => o.Id == order.Id).Value.Status = OrderStatus.Completed;
    }

    public bool IsLoggedIn(string login, string password)
    {
        return _users.ContainsKey(login) && _users[login].Password == password;
    }

    public IEnumerable<Product> Search(ISearchable searchable)
    {
        return _products.Where(searchable.IsMatch);
    }
}
