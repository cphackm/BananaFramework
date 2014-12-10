using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BananaFramework.Helpers
{
	public class Math
	{
		/// <summary>
		/// Returns the value between A and B with coefficient X
		/// </summary>
		/// <param name="A">Initial value</param>
		/// <param name="B">End value</param>
		/// <param name="X">Interpolation coefficient from. 0 results in return value of A, 1 
		/// returns in return value of B</param>
		/// <returns></returns>
		public static float Lerp(float A, float B, float X)
		{
			return A + (B - A) * X;
		}

		/// <summary>
		/// Returns the quadratic interpolation between A and C with B as a control value and a coefficient of X
		/// </summary>
		/// <param name="A">Initial value</param>
		/// <param name="B">Control value</param>
		/// <param name="C">End value</param>
		/// <param name="X">Interpolation coefficient from. 0 results in return value of A, 1 
		/// results in return value of C</param>
		/// <returns></returns>
		public static float Qerp(float A, float B, float C, float X)
		{
			return Lerp(Lerp(A, B, X), Lerp(B, C, X), X);
		}

		/// <summary>
		/// Returns the value of X bounded by Min and Max
		/// </summary>
		/// <param name="Min">Minimum bounding value</param>
		/// <param name="Max">Maximum bounding value</param>
		/// <param name="X">Value to bind</param>
		/// <returns></returns>
		public static float Clamp(float Min, float Max, float X)
		{
			return X > Max ? Max : (X < Min ? Min : X);
		}
	}
}
