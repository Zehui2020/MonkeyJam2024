using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponItem")]
public class WeaponItem : Item
{
    [SerializeField] private PlayerController.WeaponType weaponType;

    [TextArea(3, 10)]
    public string description1;

    [TextArea(3, 10)]
    public string description2; 

    [TextArea(3, 10)]
    public string description3;

    public override void Initialize()
    {
        base.Initialize();
        base.IncrementStack();
        PlayerController.Instance.EquipWeapon(weaponType);
    }

    public override void IncrementStack()
    {
        base.IncrementStack();
        PlayerController.Instance.EquipWeapon(weaponType);
    }

    public override string GetDescription()
    {
        if (itemStack == 0)
            return "<b><size=45>Level " + (itemStack + 1).ToString() + "</size></b>\n" + description;
        else if (itemStack == 1)
            return "<b><size=45>Level " + (itemStack + 1).ToString() + "</size></b>\n" + description1;
        else if (itemStack == 2)
            return "<b><size=45>Level " + (itemStack + 1).ToString() + "</size></b>\n" + description2;
        else
            return "<b><size=45>Level 4+</size></b>" + description3;
    }
}