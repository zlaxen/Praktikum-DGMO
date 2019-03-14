using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class PlayerData
{
    public enum Status { Alive, Death }

    private String _userName;
    private int _healthPoint;
    private int _manaPoint;
    private int _x;
    private int _y;
    private Status _condition;

    public PlayerData()
    {
        _userName = string.Empty;
        _condition = Status.Alive;
        _healthPoint = 1500;
        _manaPoint = 700;
        _x = 20;
        _y = 28;
    }

    public PlayerData(String userName, int healthPoint, int manaPoint, int X, int Y, Status Cond)
    {
        _userName = userName;
        _healthPoint = healthPoint;
        _manaPoint = manaPoint;
        _x = X;
        _y = Y;
        _condition = Cond;
    }

    public String UserName
    {
        get { return _userName; }
        set { _userName = value; }
    }

    public int HealthPoint
    {
        get { return _healthPoint; }
        set { _healthPoint = value; }
    }

    public int ManaPoint
    {
        get { return _manaPoint; }
        set { _manaPoint = value; }
    }

    public int X
    {
        get { return _x; }
        set { _x = value; }
    }

    public int Y
    {
        get { return _y; }
        set { _y = value; }
    }

    public Status Condition
    {
        get { return _condition; }
        set { _condition = value; }
    }
}