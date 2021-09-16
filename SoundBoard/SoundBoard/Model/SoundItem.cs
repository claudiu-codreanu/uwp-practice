using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoard.Model
{
	public class SoundItem
	{
		public string Category { get; set; }
		public string Name { get; set; }
		public string Image { get; set; }
		public string Sound { get; set; }


		public SoundItem(string category, string name)
		{
			Name = name;
			Category = category;

			Image = String.Format("/Assets/Images/{0}/{1}.png", category, name);
			Sound = String.Format("/Assets/Audio/{0}/{1}.wav", category, name);
		}


		public static List<SoundItem> GetSounds(string category)
		{
			return AllSounds.Where<SoundItem>(p => p.Category == category).ToList<SoundItem>();
		}

		public static void GetSounds(ObservableCollection<SoundItem> sounds, string category)
		{
			sounds.Clear();

			AllSounds
				.Where(p => p.Category == category)
				.ToList()
				.ForEach(p => sounds.Add(p));
		}


		public static void GetSounds( ObservableCollection<SoundItem> sounds)
		{
			sounds.Clear();
			AllSounds.ForEach(p => sounds.Add(p));
		}


		public static void GetSoundsByName( ObservableCollection<SoundItem> sounds, string name)
		{
			sounds.Clear();

			AllSounds
				.Where(p => p.Name.ToLower().StartsWith( name.ToLower() ) )
				.ToList()
				.ForEach(p => sounds.Add(p));
		}

		public static List<SoundItem> AllSounds
		{
			get
			{
				return new List<SoundItem>()
				{
					new SoundItem("Animals", "Cat"),
					new SoundItem("Animals", "Cow"),

					new SoundItem("Cartoons", "Gun"),
					new SoundItem("Cartoons", "Spring"),

					new SoundItem("Taunts", "Clock"),
					new SoundItem("Taunts", "LOL"),

					new SoundItem("Warnings", "Ship"),
					new SoundItem("Warnings", "Siren")
				};
			}
		}
	}
}
