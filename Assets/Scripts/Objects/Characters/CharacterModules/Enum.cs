using UnityEngine;

public class Enum : CharacterModule
{
    public enum CustomerType
    { 
        None,
        NormalCustomer, RudeCustomer, ShopLifter, SpecialCustomer, 
        _Length
    }

    public enum RudeCustomerBehaviorType
    {
                           // ¶¯±ø
        Steal, ThrowMoney, Tantrum, LayCounter, 
        _Length
    }
}
