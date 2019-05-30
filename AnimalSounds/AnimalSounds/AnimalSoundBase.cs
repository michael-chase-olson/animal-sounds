namespace AnimalSounds
{
    public abstract class AnimalSoundBase
    {
        public abstract string AnimalName { get; }
        public abstract string PlaySound();
    }
}