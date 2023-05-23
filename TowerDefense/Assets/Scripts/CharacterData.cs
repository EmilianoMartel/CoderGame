using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    //variables iniciales
    [SerializeField] public float speedMovement = 1.0f;
    [SerializeField] public float maxLife = 100;
}
