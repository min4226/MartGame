using UnityEngine;

public enum CustomerType
{ 
    NormalCustomer, TroubleMakerCustomer, ThiefCustomer, SpecialCustomer, 
}

public enum GameState
{ 
    PlayScene, DecoScene,
}


public enum AgeType
{ 
    Teenager, TwenThirty, FourFifty, Sixty,
}

  
public enum RudeCustomerBehaviorType
{
                      
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
    
    disguise, steal, discount,


}
