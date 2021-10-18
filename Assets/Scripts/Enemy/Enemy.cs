using UnityEngine;

[CreateAssetMenu (fileName = "New Enemy", menuName = "ScriptableObjects/Enemy")]
public class Enemy : ScriptableObject
{
    public float maxChasingDistance;
    public float targetLockDistance;    
    public float targetLockDelay;
    public float speed;
    public int lives;
}
