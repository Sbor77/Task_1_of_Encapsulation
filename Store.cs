using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

class Good
{
    public string Name { get; private set; }

    public Good (string name)
    {
        if (name.Length > 100)
            throw new ArgumentOutOfRangeException(nameof(name));
        
        Name = name;
    }
}

class Cell
{
    public Good Good { get; private set; }
    public int Count { get; private set; }

    public Cell(Good good, int count)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        Good = good;
        Count = count;
    }

    public void DecrementCount
}

class Warehouse
{
    public List<Cell> Cells;

    public Warehouse()
    {
        Cells = new List<Cell>();        
    }

    private List<Cell> GetRequiredGoods(Good good, int count)
    {
        List<Cells> searchResults = Cells.FindAll(cell => cell.Good == good && cell.Count >= count);
      
    }

    private bool IsGoodInStock(Good good, int count)
    {
        if (Cells.Exists(cell => cell.Good == good && cell.Count >= count))        
            return true;        
        else        
            return false;        
    }

    private void RemoveGoods (Good good, int count)
    {
        if (IsGoodInStock(good,count))
        {
            int cellIndex = Cells.IndexOf(good.Name);
            Cells[cellIndex].Count = -count;            
        }        
    }

    public void Deliver(Good good, int count)
    {
        if (IsGoodInStock(good,count))
        {

        }
        else
        {
            Console.Write("Required good is out of stock");
        }
    }
}

class Shop
{
    public List<Warehouse> Warehouses;

    public Shop(Warehouse warehouse)
    {
        Warehouses = new List<Warehouse>();
    }
}

class Cart
{
    private Shop _shop;
    private Good _good;
    private int _count;

    public Cart(Shop shop)
    {
        _shop = shop;
        _good = null;
        _count = 0;
    }

    public void Add(Good good, int count)
    {

    }

    public void Order()
    {

    }

}
    

    




Good iPhone12 = new Good("IPhone 12");
Good iPhone11 = new Good("IPhone 11");

Warehouse warehouse = new Warehouse();

Shop shop = new Shop(warehouse);

warehouse.Delive(iPhone12, 10);
warehouse.Delive(iPhone11, 1);

//Вывод всех товаров на складе с их остатком

Cart cart = shop.Cart();
cart.Add(iPhone12, 4);
cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

//Вывод всех товаров в корзине

Console.WriteLine(cart.Order().Paylink);

cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары