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

class Cell : IReadOnlyCell
{
    public Good Good { get; private set; }
    public int Count { get; set; }

    public Cell(Good good, int count)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        Good = good;
        Count = count;
    }  
}

interface IReadOnlyCell
{
    public Good Good { get; }

    public int Count { get; }
}

class Warehouse
{
    private readonly List<Cell> _cells;    

    public Warehouse()
    {
        _cells = new List<Cell>();        
    }

    public IReadOnlyList<IReadOnlyCell> Cells => _cells;    

    private List<Cell> GetRequiredGoods(Good good, int count)
    {
        List<Cell> searchResults = _cells.FindAll(cell => cell.Good == good && cell.Count >= count);

        return searchResults;        
    }

    private void RemoveRequiredGoods (Good good, int count)
    {
        if (GetRequiredGoods(good, count) != null)
        {
            int cellIndex = _cells.IndexOf(good.Name);
            _cells[cellIndex].Count = -count;            
        }        
    }

    public List<Cell> Deliver(Good good, int count)
    {
        List<Cell> cellsToDeliver = new List<Cell>();
        cellsToDeliver = GetRequiredGoods(good, count);

        if (cellsToDeliver != null)
        {
            RemoveRequiredGoods(good, int);
            return cellsToDeliver;
        }
        else
        {
            return null;
        }                
    }

    public void ShowAllGoods()
    {
        Console.WriteLine("Список товаров на складе: ");

        foreach (var item in _cells)
        {
            Console.WriteLine("Название товара: " + item.Good.Name());
            Console.WriteLine("количество: " + item.Count());
        }
    }
}

class Shop
{
    private readonly List<Warehouse> _warehouses;

    public Shop(Warehouse warehouse)
    {
        Warehouses = new List<Warehouse>();
    }

    public IReadOnlyList<Warehouse> Warehouses => _warehouses;
}

class Cart
{
    private readonly List<Cell> _cells;
    private Shop _shop;

    public Cart(Shop shop)
    {
        _shop = shop;
        _cells = new List<Cell>();
    }

    public void Add(Good good, int count)
    {
        if (true)
        {

        }
    }

    public void Order()
    {

    }

    public void ShowAllGoods()
    {
        Console.WriteLine("Список товаров в корзине: ");

        foreach (var cell in _cells)
        {
            Console.WriteLine("Название товара: " + cell.Good.Name());
            Console.WriteLine("количество: " + cell.Count());
        }
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