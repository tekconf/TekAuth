
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;

namespace TekConf.Mobile.Core
{
	public class KittenGenerator
	{
		private readonly List<string> _names = new List<string>
		{
			"Tiddles",
			"Amazon",
			"Pepsi",
			"Solomon",
			"Butler",
			"Snoopy",
			"Harry",
			"Holly",
			"Paws",
			"Polly",
			"Dixie",
			"Fern",
			"Cousteau",
			"Frankenstein",
			"Bazooka",
			"Casanova",
			"Fudge",
			"Comet"
		};

		private readonly Random _random = new Random();

		public Kitten CreateNewKitten()
		{
			return new Kitten
			{
				Name = _names[_random.Next(_names.Count)],
				ImageUrl = string.Format("http://placekitten.com/{0}/{0}", _random.Next(20) + 300)
			};
		}
	}
	public class DogGenerator
	{
		private readonly List<string> _names = new List<string>
		{
			"Buddy"
			,
			"Toby"
			,
			"Ace"
			,
			"AJ"
			,
			"Max"
			,
			"Aztec"
			,
			"Jake"
			,
			"Byron"
			,
			"Axel"
			,
			"Bentley"
			,
			"Cooper"
			,
			"Fuzzy"
			,
			"Bandit"
			,
			"Bear"
			,
			"Charlie"
			,
			"Duke"
			,
			"Marley"
			,
			"Rocky"
			,
			"Shadow"
			,
			"Biscuit"
			,
			"Blaze"
			,
			"Rocky"
			,
			"Buzz"
			,
			"Oreo"
			,
			"Benji"
		};

		private readonly Random _random = new Random();

		public Dog CreateNewDog()
		{
			return new Dog
			{
				Name = _names[_random.Next(_names.Count)],
				ImageUrl = string.Format("http://placedog.com/{0}/{0}", _random.Next(20) + 300)
			};
		}
	}
	public class Animal
	{
	}
	public class Dog : Animal
	{
		public string Name { get; set; }
		public string ImageUrl { get; set; }
	}
	public class Kitten : Animal
	{
		public string Name { get; set; }
		public string ImageUrl { get; set; }
	}
	public class BaseSampleViewModel : MvxViewModel
	{
		private readonly DogGenerator _dogGenerator = new DogGenerator();
		private readonly KittenGenerator _kittenGenerator = new KittenGenerator();

		protected Kitten CreateKitten()
		{
			return _kittenGenerator.CreateNewKitten();
		}

		protected Kitten CreateKittenNamed(string name)
		{
			var kitten = CreateKitten();
			kitten.Name = name;
			return kitten;
		}

		protected IEnumerable<Kitten> CreateKittens(int count)
		{
			for (var i = 0; i < count; i++)
			{
				yield return CreateKitten();
			}
		}

		protected Dog CreateDog()
		{
			return _dogGenerator.CreateNewDog();
		}

		protected IEnumerable<Dog> CreateDogs(int count)
		{
			for (var i = 0; i < count; i++)
			{
				yield return CreateDog();
			}
		}
	}
	public class PolymorphicListItemTypesViewModel : BaseSampleViewModel
	{
		private List<Animal> _animals;

		public PolymorphicListItemTypesViewModel()
		{
			var animals = new List<Animal>();
			for (var i = 0; i < 10; i++)
			{
				animals.Add(i % 2 == 0 ? CreateDog() : (Animal)CreateKitten());
			}
			Animals = animals;
		}

		public List<Animal> Animals
		{
			get { return _animals; }
			set
			{
				_animals = value;
				RaisePropertyChanged(() => Animals);
			}
		}
	}
}