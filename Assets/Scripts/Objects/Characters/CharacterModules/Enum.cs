using UnityEngine;

public enum CustomerType
{ 
    NormalCustomer, TroubleMakerCustomer, ThiefCustomer, SpecialCustomer, 
}

// ¿¬·É´ë
public enum AgeType
{ 
    Teenager, TwenThirty, FourFifty, Sixty,
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

public enum StageType
{ 
    stage1, stage2, stage3, stage4, stage5, stage6, stage7, stage8, stage9, stage10, 
    stage11, stage12, stage13, stage14, stage15
}

public enum ItemCreatePattern
{ 
    straight, zigzag, 
}

public enum PatternRules
{
    // º¯Àå   ÈÉÄ§    ÇÒÀÎ
    disguise, steal, discount,


}
