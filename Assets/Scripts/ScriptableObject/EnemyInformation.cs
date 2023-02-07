using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "Informations/Enemy Information")]
public class EnemyInformation : ScriptableObject
{
    [Header("Agressive")]
    public float detectionDistance;
    public float pursuitRange;
    
    [Header("Exp")]
    public float givingExp;
}