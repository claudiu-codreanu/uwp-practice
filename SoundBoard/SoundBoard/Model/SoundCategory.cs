using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoard.Model
{
	public class SoundCategory
	{
		public string Name { get; set; }
		public string Image { get; set; }

		public SoundCategory( string name )
		{
			Name = name;
			Image = String.Format("/Assets/Icons/{0}.png", name);
		}
		public static List<SoundCategory> AllCategories
		{
			get
			{
				return new List<SoundCategory>
				{
					new SoundCategory("Animals"),
					new SoundCategory("Cartoons"),
					new SoundCategory("Taunts"),
					new SoundCategory("Warnings")
				};
			}
		}
	}
}
