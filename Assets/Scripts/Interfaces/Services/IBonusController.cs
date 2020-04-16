using UnityEngine;

public interface IBonusController : IService
{
    void DoBonus(BonusTypes bonusType, int value);
    void DoBonus(BonusTypes bonusType, Object changeObject, int value);

    void RemoveBonus(BonusTypes bonusType, Object changeObject);
}