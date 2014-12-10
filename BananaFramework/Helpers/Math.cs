using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BananaFramework.Helpers
{
	public class Math
	{
		public static float Lerp(float A, float B, float X)
		{
			return A + (B - A) * X;
		}

		public static float Qerp(float A, float B, float C, float X)
		{
			return Lerp(Lerp(A, B, X), Lerp(B, C, X), X);
		}

		public static float Clamp(float Min, float Max, float X)
		{
			return X > Max ? Max : (X < Min ? Min : X);
		}
	}
}
