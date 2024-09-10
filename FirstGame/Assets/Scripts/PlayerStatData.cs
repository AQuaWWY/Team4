using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatData", menuName = "ScriptableObjects/PlayerStatData", order = 1)]
public class PlayerStatData : ScriptableObject
{
    public List<PlayerStatValue> moveSpeed, health, pickupRange, maxWeapons;
    public int moveSpeedLevelCount, healthLevelCount, pickupRangeLevelCount;
    public int moveSpeedLevel, healthLevel, pickupRangeLevel, maxWeaponsLevel;

    public void SaveData(PlayerStatController controller)
    {
        moveSpeed = new List<PlayerStatValue>(controller.moveSpeed);
        health = new List<PlayerStatValue>(controller.health);
        pickupRange = new List<PlayerStatValue>(controller.pickupRange);
        maxWeapons = new List<PlayerStatValue>(controller.maxWeapons);

        moveSpeedLevelCount = controller.moveSpeedLevelCount;
        healthLevelCount = controller.healthLevelCount;
        pickupRangeLevelCount = controller.pickupRangeLevelCount;

        moveSpeedLevel = controller.moveSpeedLevel;
        healthLevel = controller.healthLevel;
        pickupRangeLevel = controller.pickupRangeLevel;
        maxWeaponsLevel = controller.maxWeaponsLevel;
    }

    public void LoadData(PlayerStatController controller)
    {
        controller.moveSpeed = new List<PlayerStatValue>(moveSpeed);
        controller.health = new List<PlayerStatValue>(health);
        controller.pickupRange = new List<PlayerStatValue>(pickupRange);
        controller.maxWeapons = new List<PlayerStatValue>(maxWeapons);

        controller.moveSpeedLevelCount = moveSpeedLevelCount;
        controller.healthLevelCount = healthLevelCount;
        controller.pickupRangeLevelCount = pickupRangeLevelCount;

        controller.moveSpeedLevel = moveSpeedLevel;
        controller.healthLevel = healthLevel;
        controller.pickupRangeLevel = pickupRangeLevel;
        controller.maxWeaponsLevel = maxWeaponsLevel;
    }
}