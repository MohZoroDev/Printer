using System;
using System.Collections.Generic;
using System.Threading;

namespace Extensions
{
	public class Printer
	{
		private static void PlacePixel(Pixel pixel)
		{
			Console.SetCursorPosition(pixel.Position.X, pixel.Position.Y);
			Console.Write(pixel.Value);
		}

		public static void Print(string[] word, int lineLength, int time, Vector2 vector, Style style)
		{
			int top = vector.Y;
			int left = vector.X;

			Pixel[,] pixels = new Pixel[Data.LinesCount, lineLength];

			for (int i = 0; i < word.Length; i++)
			{
				for (int j = 0; j < word[i].Length; j++)
				{
					Vector2 vec = new Vector2(left + j, top + i);
					Pixel px = new Pixel(vec, word[i][j]);

					pixels[i, j] = px;
				}
			}

			switch (style)
			{
				case Style.Horizontal:
					Horizontal(pixels, time, lineLength);
					break;
				case Style.Vertical:
					Vertical(pixels, time, lineLength);
					break;
				case Style.HorizontalMirror:
					HorizontalMirror(pixels, time, lineLength);
					break;
				case Style.VerticalMirror:
					VerticalMirror(pixels, time, lineLength);
					break;
				case Style.TrueFalse:
					TrueFalse(pixels, time, lineLength);
					break;
				case Style.HorizontalTrueFalse:
					HorizontalTrueFalse(pixels, time, lineLength);
					break;
				case Style.VerticalTrueFalse:
					VerticalTrueFalse(pixels, time, lineLength);
					break;
				case Style.Random:
					Random(pixels, time, lineLength);
					break;
				case Style.Corner:
					Corner(pixels, time, lineLength);
					break;
				case Style.Biangular:
					Biangular(pixels, time, lineLength);
					break;
				case Style.Quadrangle:
					Quadrangle(pixels, time, lineLength);
					break;
				case Style.FillOutIn:
					FillOutIn(pixels, time, lineLength);
					break;
				case Style.FillInOut:
					FillInOut(pixels, time, lineLength);
					break;
			}


			Console.SetCursorPosition(0, 0);
			Console.CursorVisible = false;
		}

		private static void Horizontal(Pixel[,] pixels, int time, int lineLength)
		{
			for (int i = 0; i < Data.LinesCount; i++)
			{
				for (int j = 0; j < lineLength; j++)
				{
					PlacePixel(pixels[i, j]);
					Thread.Sleep(time);
				}
			}
		}

		private static void Vertical(Pixel[,] pixels, int time, int lineLength)
		{
			for (int i = 0; i < lineLength; i++)
			{
				for (int j = 0; j < Data.LinesCount; j++)
				{
					PlacePixel(pixels[j, i]);
					Thread.Sleep(time);
				}
			}
		}

		private static void HorizontalMirror(Pixel[,] pixels, int time, int lineLength)
		{
			int half = Data.LinesCount / 2;

			for (int i = 0; i < half; i++)
			{
				for (int j = 0; j < lineLength; j++)
				{
					PlacePixel(pixels[i, j]);
					PlacePixel(pixels[Data.LinesCount - i - 1, lineLength - j - 1]);

					Thread.Sleep(time);
				}
			}

			if (Data.LinesCount % 2 != 0)
			{
				int l = 0, r = lineLength - 1;

				while (l < r)
				{
					PlacePixel(pixels[half, l++]);
					PlacePixel(pixels[half, r--]);

					Thread.Sleep(time);
				}
			}
		}

		private static void VerticalMirror(Pixel[,] pixels, int time, int lineLength)
		{
			int half = Data.LinesCount;

			for (int i = 0; i < lineLength / 2; i++)
			{
				for (int j = 0; j < Data.LinesCount; j++)
				{
					PlacePixel(pixels[j, i]);
					PlacePixel(pixels[Data.LinesCount - j - 1, lineLength - i - 1]);

					Thread.Sleep(time);
				}
			}

			if (lineLength % 2 != 0)
			{
				int l = 0, r = Data.LinesCount - 1;

				while (l < r)
				{
					PlacePixel(pixels[l++, half]);
					PlacePixel(pixels[r--, half]);

					Thread.Sleep(time);
				}
			}
		}

		private static void Random(Pixel[,] pixels, int time, int lineLength)
		{
			Random rand = new Random();

			for (int i = 0; i < Data.LinesCount; i++)
			{
				for (int j = 0; j < lineLength; j++)
				{
					int l = rand.Next(Data.LinesCount);
					int r = rand.Next(lineLength);

					Pixel temp = pixels[i, j];
					pixels[i, j] = pixels[l, r];
					pixels[l, r] = temp;
				}
			}

			for (int i = 0; i < Data.LinesCount; i++)
			{
				for (int j = 0; j < lineLength; j++)
				{
					PlacePixel(pixels[i, j]);
					Thread.Sleep(time);
				}
			}
		}

		private static void Corner(Pixel[,] pixels, int time, int lineLength)
		{
			int limit = Data.LinesCount * lineLength;
			int idx = 0;

			Pixel[] pattern = new Pixel[limit];

			int i = 0, j = 0;

			while (j < lineLength)
			{
				int m = j;

				while (i < Data.LinesCount && m >= 0)
				{
					pattern[idx] = pixels[i, m];
					pattern[limit - idx - 1] = pixels[Data.LinesCount - i - 1, lineLength - m - 1];

					i++;
					m--;

					idx++;
				}

				i = 0;
				j++;
			}

			for (int k = 0; k < limit; k++)
			{
				PlacePixel(pattern[k]);
				Thread.Sleep(time);
			}
		}

		private static void Biangular(Pixel[,] pixels, int time, int lineLength)
		{
			int limit = (Data.LinesCount * lineLength);
			int cnt = 0;

			bool[,] visited = new bool[Data.LinesCount, lineLength];
			int i = 0, j = 0;

			while (cnt < limit)
			{
				int m = j;

				while (i < Data.LinesCount && m >= 0 && cnt < limit)
				{
					PlacePixel(pixels[i, m]);
					PlacePixel(pixels[Data.LinesCount - i - 1, lineLength - m - 1]);

					if (visited[i, m] == false) cnt++;
					visited[i, m] = true;
					if (visited[Data.LinesCount - i - 1, lineLength - m - 1] == false) cnt++;
					visited[Data.LinesCount - i - 1, lineLength - m - 1] = true;

					i++;
					m--;

					Thread.Sleep(time);
				}

				i = 0;
				j++;
			}
		}

		private static void Quadrangle(Pixel[,] pixels, int time, int lineLength)
		{
			int limit = (Data.LinesCount * lineLength);
			int cnt = 0;

			bool[,] visited = new bool[Data.LinesCount, lineLength];

			int i = 0, j = 0;

			while (cnt < limit)
			{
				int m = j;

				while (i < Data.LinesCount / 2 + 1 && m >= 0 && cnt < limit)
				{
					PlacePixel(pixels[i, m]);
					PlacePixel(pixels[i, lineLength - m - 1]);
					PlacePixel(pixels[Data.LinesCount - i - 1, m]);
					PlacePixel(pixels[Data.LinesCount - i - 1, lineLength - m - 1]);

					if (visited[i, m] == false) { cnt++; }
					visited[i, m] = true;
					if (visited[i, lineLength - m - 1] == false) { cnt++; }
					visited[i, lineLength - m - 1] = true;
					if (visited[Data.LinesCount - i - 1, m] == false) { cnt++; }
					visited[Data.LinesCount - i - 1, m] = true;
					if (visited[Data.LinesCount - i - 1, lineLength - m - 1] == false) { cnt++; }
					visited[Data.LinesCount - i - 1, lineLength - m - 1] = true;

					i++;
					m--;

					Thread.Sleep(time);	
				}

				i = 0;
				j++;
			}
		}

		private static void FillInOut(Pixel[,] pixels, int time, int lineLength)
		{
			bool[,] visited = new bool[Data.LinesCount, lineLength];
			int limit = Data.LinesCount * lineLength;

			List<Pixel> pattern = new List<Pixel>();
			int cnt = 0;

			Vector2[] directions = new Vector2[4];

			directions[0] = new Vector2(0, 1);
			directions[1] = new Vector2(1, 0);
			directions[2] = new Vector2(0, -1);
			directions[3] = new Vector2(-1, 0);

			int i = 0, j = 0;

			int idx = 0;

			while (cnt < limit)
			{
				if (i >= Data.LinesCount || j >= lineLength || i < 0 || j < 0 || visited[i, j])
				{
					i -= directions[idx % 4].X;
					j -= directions[idx % 4].Y;

					idx++;

					i += directions[idx % 4].X;
					j += directions[idx % 4].Y;
				}

				pattern.Add(pixels[i, j]);
				visited[i, j] = true;

				i += directions[idx % 4].X;
				j += directions[idx % 4].Y;

				cnt++;
			}

			for (int k = limit - 1; k >= 0; k--)
			{
				PlacePixel(pattern[k]);
				Thread.Sleep(time);
			}
		}

		private static void FillOutIn(Pixel[,] pixels, int time, int lineLength)
		{
			bool[,] visited = new bool[Data.LinesCount, lineLength];

			int limit = Data.LinesCount * lineLength;
			int cnt = 0;

			Vector2[] directions = new Vector2[4];

			directions[0] = new Vector2(0, 1);
			directions[1] = new Vector2(1, 0);
			directions[2] = new Vector2(0, -1);
			directions[3] = new Vector2(-1, 0);

			int i = 0, j = 0;

			int idx = 0;

			while (cnt < limit)
			{
				if (i >= Data.LinesCount || j >= lineLength || i < 0 || j < 0 || visited[i, j])
				{
					i -= directions[idx % 4].X;
					j -= directions[idx % 4].Y;

					idx++;

					i += directions[idx % 4].X;
					j += directions[idx % 4].Y;
				}

				PlacePixel(pixels[i, j]);
				visited[i, j] = true;

				i += directions[idx % 4].X;
				j += directions[idx % 4].Y;

				cnt++;

				Thread.Sleep(time);
			}


		}

		private static void TrueFalse(Pixel[,] pixels, int time, int lineLength)
		{
			for (int i = 0; i < Data.LinesCount; i++)
			{
				for (int j = 0; j < lineLength; j += 2)
				{
					PlacePixel(pixels[i, j]);

					Thread.Sleep(time);
				}
			}

			for (int i = 0; i < Data.LinesCount; i++)
			{
				for (int j = 1; j < lineLength; j += 2)
				{
					PlacePixel(pixels[i, j]);

					Thread.Sleep(time);
				}
			}
		}

		private static void VerticalTrueFalse(Pixel[,] pixels, int time, int lineLength)
		{
			for (int i = 0; i < lineLength; i += 2)
			{
				for (int j = 0; j < Data.LinesCount; j++)
				{
					PlacePixel(pixels[j, i]);
					Thread.Sleep(time);
				}
			}

			for (int i = 1; i < lineLength; i += 2)
			{
				for (int j = 0; j < Data.LinesCount; j++)
				{
					PlacePixel(pixels[j, i]);
					Thread.Sleep(time);
				}
			}

		}

		private static void HorizontalTrueFalse(Pixel[,] pixels, int time, int lineLength)
		{
			for (int i = 0; i < Data.LinesCount; i += 2)
			{
				for (int j = 0; j < lineLength; j++)
				{
					PlacePixel(pixels[i, j]);
					Thread.Sleep(time);
				}
			}

			for (int i = 1; i < Data.LinesCount; i += 2)
			{
				for (int j = 0; j < lineLength; j++)
				{
					PlacePixel(pixels[i, j]);
					Thread.Sleep(time);
				}
			}

		}
	}
}
