using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mist.Map
{
	public class TileCollision
	{
		public int XPos { get; set; }
		public int YPos { get; set; }


		private readonly int _tileSize = 16;

		public Rectangle Rectangle { get { return new Rectangle(XPos * _tileSize, YPos * _tileSize, _tileSize, _tileSize); } }

		public bool Intersect(Rectangle rectangle)
		{
			return Rectangle.Intersects(rectangle);
		}

		public TileCollision(int x, int y)
		{
			XPos = x;
			YPos = y;
		}
	}
}
