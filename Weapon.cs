
class Weapon
{
    private int _damage;
    private int _bullets;

    public int Damage => _damage;
    public int Bullets => _bullets;

    public Weapon(int damage, int bullets)
    {
        if (damage <= 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        if (bullets < 0)
            throw new ArgumentOutOfRangeException(nameof(bullets));

        _damage = damage;
        _bullets = bullets;
    }

    public void Fire(Player player)
    {
        if (_bullets > 0)
        {
            _bullets -= 1;
            player.GetDamage(this);
        }
        else
        {
            Console.WriteLine("Out of ammo!");
        }
    }
}

class Player
{
    private int _health;

    public int Health => _health;

    public void GetDamage(Weapon weapon)
    {
        if (IsAlive())
            _health -= weapon.Damage;
        else
            Console.WriteLine("Player is dead!");
    }

    private bool IsAlive()
    {
        if (_health > 0)
            return true;
        else
            return false;
    }
}

class Bot
{
    public Weapon Weapon;

    public void OnSeePlayer(Player player)
    {
        Weapon.Fire(player);
    }
}