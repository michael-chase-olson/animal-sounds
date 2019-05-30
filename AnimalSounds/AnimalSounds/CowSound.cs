namespace AnimalSounds
{
    public class CowSound : AnimalSoundBase
    {
        public CowSound()
        {
            AnimalName = "Cow";
        }

        public override string AnimalName { get; }
        public override string PlaySound()
        {
            return "The cow goes Mooo";
        }
    }
}