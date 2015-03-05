using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BananaFramework
{
	public class FloatRectangle
	{
		protected float x;
		public float X
		{
			get
			{
				return x;
			}
		}
		protected float y;
		public float Y
		{
			get
			{
				return y;
			}
		}
		protected float w;
		public float Width
		{
			get
			{
				return w;
			}
		}
		protected float h;
		public float Height
		{
			get
			{
				return h;
			}
		}

		public float Right
		{
			get
			{
				return x + w;
			}
		}
		public float Left
		{
			get
			{
				return x;
			}
		}
		public float Top
		{
			get
			{
				return y;
			}
		}
		public float Bottom
		{
			get
			{
				return y + h;
			}
		}

		public FloatRectangle(float X, float Y, float Width, float Height)
		{
			x = X;
			y = Y;
			w = Width;
			h = Height;
		}

		public bool Intersects(FloatRectangle Rect)
		{
			return !(Right < Rect.Left) && !(Left > Rect.Right) && !(Bottom < Rect.Top) && !(Top > Rect.Bottom);
		}
	}
}
