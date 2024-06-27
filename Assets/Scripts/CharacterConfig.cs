using UnityEngine;

namespace MyGame
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = nameof(CharacterConfig))]
    public class CharacterConfig : ScriptableObject, ICharacterConfig
    {
        [field: SerializeField] public float MaxHp {  get; private set; }
        [field: SerializeField] public float LowHpCoefficient { get; private set; }

        [field: SerializeField] public float MaxRadiansDelta { get; private set; }
        [field: SerializeField] public float BaseSpeed { get; private set; }
    }
}
