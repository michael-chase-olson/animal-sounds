using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalSounds
{
    class Program
    {
        static void Main(string[] args)
        {
            var allAnimals = new List<AnimalSoundBase>(BuildAnimalSoundObjects());

            bool quit = false;

            while (!quit)
            {
                PrintMenu(allAnimals);

                var input = Console.ReadLine();
                ProcessInput(input, allAnimals, ref quit);
            }
        }

        private static IEnumerable<AnimalSoundBase> BuildAnimalSoundObjects()
        {
            var types = new List<Type>(GetAnimalSoundBaseSubTypes());

            return types.Select(type => Activator.CreateInstance(type) as AnimalSoundBase);
        }

        private static IEnumerable<Type> GetAnimalSoundBaseSubTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly =>
            {
                return assembly.GetTypes().Where(i => i.BaseType != null
                                                      && i.BaseType == typeof(AnimalSoundBase));
            });

        }

        private static void PrintMenu(IReadOnlyList<AnimalSoundBase> animals)
        {
            Console.WriteLine("Play a sound for:");
            for (var index = 0; index < animals.Count; index++)
            {
                var animal = animals[index];
                Console.WriteLine("\t{0} : {1}", index + 1, animal.AnimalName);
            }
            Console.WriteLine("\ta[A] : All Animals");
            Console.WriteLine("q[Q] : quite");
        }

        private static void ProcessInput(string input, IList<AnimalSoundBase> allAnimals, ref bool quit)
        {
            if (string.IsNullOrEmpty(input))
                return;

            if (input.ToUpper().Equals("Q"))
            {
                ProcessQuit(ref quit);
            }

            if (input.ToUpper().Equals("A"))
            {
                ProcessAll(allAnimals);
            }

            TryProcessNumber(allAnimals, input);
        }

        private static void ProcessQuit(ref bool quit)
        {
            quit = true;
        }

        private static void ProcessAll(IEnumerable<AnimalSoundBase> allAnimals)
        {
            foreach (var animalSoundBase in allAnimals)
            {
                Console.WriteLine(animalSoundBase.PlaySound());
            }
        }

        private static bool TryProcessNumber(IList<AnimalSoundBase> allAnimals, string input)
        {
            int number;
            var parseIntSuccess = int.TryParse(input, out number);
            if (parseIntSuccess)
            {
                if (number < allAnimals.Count + 1)
                {
                    Console.WriteLine(allAnimals[number - 1].PlaySound());
                }

                return true;
            }

            return false;
        }
    }
}
