using UnityEngine;

public enum EquipmentType
{
    None = 0,
    Helmet,
    Chest,
    Gloves,
    Boots,
    Weapon1,
    Weapon2,
    Accessory1,
    Accessory2
}

[CreateAssetMenu]
public class EquippableItem : ItemData
{
    public int StrengthBonus;
    public int SpeedBonus;
    public int HealthBonus;

    [Space]
    [Range(0f, 1f)]
    public float StrengthPercentBonus;
    [Range(0f, 1f)]
    public float SpeedPercentBonus;
    [Range(0f, 1f)]
    public float HealthPercentBonus;

    [Space]
    public EquipmentType EquipmentType;

    public void Equip(PlayerStats c)
    {
        if (StrengthBonus != 0) c.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        if (SpeedBonus != 0) c.Speed.AddModifier(new StatModifier(SpeedBonus, StatModType.Flat, this));
        if (HealthBonus != 0) c.Health.AddModifier(new StatModifier(HealthBonus, StatModType.Flat, this));

        if (StrengthPercentBonus != 0) c.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModType.PercentMult, this));
        if (SpeedPercentBonus != 0) c.Speed.AddModifier(new StatModifier(SpeedPercentBonus, StatModType.PercentMult, this));
        if (HealthPercentBonus != 0) c.Health.AddModifier(new StatModifier(HealthPercentBonus, StatModType.PercentMult, this));
    }

    public void Unequip(PlayerStats c)
    {
        c.Strength.RemoveAllModifiersFromSource(this);
        c.Speed.RemoveAllModifiersFromSource(this);
        c.Health.RemoveAllModifiersFromSource(this);
    }
}
