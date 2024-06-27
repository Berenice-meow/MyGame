namespace MyGame
{
    public interface ICharacterConfig
    {
        float MaxHp { get; }
        float LowHpCoefficient { get; }

        float MaxRadiansDelta { get; }
        float BaseSpeed { get; }
    }
}
