using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    //variables iniciales
    [SerializeField] public float speedMovement = 1.0f;
    [SerializeField] public float maxLife = 100;

    //variables para el ataque
    [SerializeField] public LayerMask attackPointMask;
    [SerializeField] public float damage;
    [SerializeField] public float distanceAttack;
}