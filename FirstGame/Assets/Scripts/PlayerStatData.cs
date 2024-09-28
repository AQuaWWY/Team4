using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatData", menuName = "ScriptableObjects/PlayerStatData", order = 1)]
public class PlayerStatData : ScriptableObject
{
    public List<PlayerStatValue> moveSpeed, health, pickupRange, maxWeapons;
    public int moveSpeedLevelCount, healthLevelCount, pickupRangeLevelCount;
    public int moveSpeedLevel, healthLevel, pickupRangeLevel, maxWeaponsLevel;
    public List<Weapon> assignedWeapons;//激活的武器列表
    public List<Weapon> unassignedWeapons;//未分配的武器列表
    public List<Weapon> fullyLevelUpWeapons;//满级武器列表

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

    public void SaveWeapons(PlayerController controller)
    {
        assignedWeapons = new List<Weapon>();
        unassignedWeapons = new List<Weapon>();
        fullyLevelUpWeapons = new List<Weapon>();

        assignedWeapons.Clear();
        unassignedWeapons.Clear();
        fullyLevelUpWeapons.Clear();

        foreach (var weapon in controller.assignedWeapons)
        {
            assignedWeapons.Add(weapon);
            Debug.Log("Save " + weapon.name);
            Debug.Log("element is " + assignedWeapons[assignedWeapons.Count-1].name);
        }

        foreach (var weapon in controller.unassignedWeapons)
        {
            unassignedWeapons.Add(weapon);
            Debug.Log("Save " + weapon.name);
            Debug.Log("element is " + unassignedWeapons[unassignedWeapons.Count-1].name);
        }

        foreach (var weapon in controller.fullyLevelUpWeapons)
        {
            fullyLevelUpWeapons.Add(weapon);
            Debug.Log("Save " + weapon.name);
            Debug.Log("element is " + fullyLevelUpWeapons[fullyLevelUpWeapons.Count-1].name);
        }



    }

    public void LoadWeapons(PlayerController controller)
    {
        controller.assignedWeapons = new List<Weapon>();
        controller.unassignedWeapons = new List<Weapon>();
        controller.fullyLevelUpWeapons = new List<Weapon>();

        controller.assignedWeapons.Clear();
        controller.unassignedWeapons.Clear();
        controller.fullyLevelUpWeapons.Clear();

        foreach (var weapon in assignedWeapons)
        {
            controller.assignedWeapons.Add(weapon);
        }

        foreach (var weapon in unassignedWeapons)
        {
            controller.unassignedWeapons.Add(weapon);
        }

        foreach (var weapon in fullyLevelUpWeapons)
        {
            controller.fullyLevelUpWeapons.Add(weapon);
        }
    }
}