namespace AnimalSounds
{
    public class DuckSound : AnimalSoundBase
    {
        public DuckSound()
        {
            AnimalName = "Duck";
        }
        public override string AnimalName { get; }
        public override string PlaySound()
        {
            return "The Duck goes quack";
        }
    }
}