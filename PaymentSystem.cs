using System;

class Program
{
    static void Main(string[] args)
    {
        //Выведите платёжные ссылки для трёх разных систем платежа: 
        //pay.system1.ru/order?amount=12000RUB&hash={MD5 хеш ID заказа}
        //order.system2.ru/pay?hash={MD5 хеш ID заказа + сумма заказа}
        //system3.com/pay?amount=12000&curency=RUB&hash={SHA-1 хеш сумма заказа + ID заказа + секретный ключ от системы}

        Order newOrder = new Order(001, 12000);
        PaymentSystem paymentSystem1 = new PaymentSystem(true);
        PaymentSystem paymentSystem2 = new PaymentSystem(true,true);
        PaymentSystem paymentSystem3 = new PaymentSystem(false, true, true, false, true);

        string paymentSystem1Link = paymentSystem1.GetPaymentLink(newOrder);
        string paymentSystem2Link = paymentSystem2.GetPaymentLink(newOrder);
        string paymentSystem2Link = paymentSystem2.GetPaymentLink(newOrder);
    }
}

public class PaymentSystem : IPaymentSystem
{
    private string _paymentLink;
    private bool _isOrderIdMd5;
    private bool _isOrderAmountSha1;
    private bool _secureKey;
    private bool _isIdConcat;
    private bool _isAmountConcat;

    public PaymentSystem(bool isOrderIdMd5 = false, bool isOrderAmountSha1 = false, bool _isIdConcat = false, bool _isAmountConcat = false, bool secureKey = false)
    {        
        _isOrderIdMd5 = isOrderIdMd5;
        _isOrderAmountSha1 = isOrderAmountSha1;
        _secureKey = secureKey;

        if (_isOrderIdMd5 == false && _isOrderAmountSha1 == false)
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    public string GetPaymentLink(Order order)
    {
        if (_isOrderIdMd5)
            _paymentLink += GenerateHashMd5(order.Id.ToString);
        else
            _paymentLink += GenerateHashSha1(order.Amount.ToString);

        if (isIdConcat)
            _paymentLink += order.Id.ToString();

        if (isAmountConcat)
            _paymentLink += order.Amount.ToString();

        if (_secureKey)
            _paymentLink += GenerateSecureKey(order);

        return _paymentLink;
    }

    private string GenerateHashMd5(string inputString)
    {
        string hash = inputString + "MD5_hash";        
        return hash;
    }

    private string GenerateHashSha1(string inputString)
    {
        string hash = inputString + "SHA1_hash";
        return hash;
    }

    private string GenerateSecureKey(Order order)
    {
        string hash = "Secure_Key";
        return hash;
    }
}

public class Order
{
    public readonly int Id;
    
    public readonly int Amount;

    public Order(int id, int amount) => (Id, Amount) = (id, amount);
}

public interface IPaymentSystem
{
    public string GetPaymentLink(Order order); 
}