using UnityEngine;

  public enum CustomerType
  { 
      None,
      NormalCustomer, RudeCustomer, ShopLifter, SpecialCustomer, 
      _Length
  }

  // Áø»ó¼Õ´Ô Çàµ¿Å¸ÀÔ
  public enum RudeCustomerBehaviorType
  {
                         // ¶¯±ø
      Steal, ThrowMoney, Tantrum, LayCounter, 
      _Length
  }

  public enum TimeResult
  { 
      None, Success, Fail,
      _Length
  }
