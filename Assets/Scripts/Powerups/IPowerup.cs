using UnityEngine;

public interface IPowerup
{
    void DestroyPowerup();
    void SpawnPowerup();
    void ApplyPowerup(MonoBehaviour i);

    public enum PowerupType
    {
        Coin = 0,
        MagicMushroom = 1,
        OneUpMushroom = 2,
        StarMan = 3,
        FireFlower = 4,
        Damage = 99,
        Default = -1
    }

    PowerupType powerupType
    {
        get;
    }

    bool hasSpawned
    {
        get;
    }
}


public interface IPowerupApplicable
{
    public void RequestPowerupEffect(IPowerup i);
}