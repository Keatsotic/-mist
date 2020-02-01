using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mist.Data
{
	public class SpriteSheetData
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		public List<string> AnimationName { get; private set; }
		public List<int[]> FrameArray { get; private set; }
		public List<float> FrameDuration { get; private set; }
		public List<bool> IsLooping { get; private set; }

		public SpriteSheetData(int width, int height, List<string> animationName, List<int[]> frameArray, List<float> frameDuration, List<bool> isLooping)
		{
			Width = width;
			Height = height;
			AnimationName = animationName;
			FrameArray = frameArray;
			FrameDuration = frameDuration;
			IsLooping = isLooping;
		}
	}
}
