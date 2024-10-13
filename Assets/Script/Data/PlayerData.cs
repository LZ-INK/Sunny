using UnityEngine;

[CreateAssetMenu(menuName = "Data File/Player Data")]
public class PlayerData : ScriptableObject
{
    [Range(1, 10)]
    public float jumpVelocity;

    [Range(1, 10)]
    public float Movespeed = 5;
    [Range(1, 10)]
    public int ClimbSpeed = 3;

    [Range(1, 10)]
    public int MaxJunpCount = 1;
}
