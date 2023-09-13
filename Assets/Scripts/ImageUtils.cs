using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ImageUtils
{
	public struct Point
	{

		public int x;
		public int y;

		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}

	public static void FloodFill(Texture2D readTexture, Texture2D writeTexture, Color sourceColor, float tollerance, int x, int y)
	{ 
		var targetColor = new Color(0,0,0,0);
		var q = new Queue<Point>(readTexture.width * readTexture.height);
		q.Enqueue(new Point(x, y));
		int iterations = 0;

		var width = readTexture.width;
		var height = readTexture.height;
		while (q.Count > 0)
		{
			Debug.Log("q:" + q.Count);
			var point = q.Dequeue();
			var x1 = point.x;
			var y1 = point.y;
			if (q.Count > width * height)
			{
				throw new System.Exception("The algorithm is probably looping. Queue size: " + q.Count);
			}

			if (writeTexture.GetPixel(x1, y1) == targetColor)
			{
				continue;
			}

			writeTexture.SetPixel(x1, y1, targetColor);


			var newPoint = new Point(x1 + 1, y1);
			if (CheckValidity(readTexture, readTexture.width, readTexture.height, newPoint, sourceColor, tollerance))
			{
				q.Enqueue(newPoint);
				Debug.Log("right");
			}

			newPoint = new Point(x1 - 1, y1);
			if (CheckValidity(readTexture, readTexture.width, readTexture.height, newPoint, sourceColor, tollerance))
			{
				q.Enqueue(newPoint);
				Debug.Log("left");
			}

			newPoint = new Point(x1, y1 + 1);
			if (CheckValidity(readTexture, readTexture.width, readTexture.height, newPoint, sourceColor, tollerance)) 
			{ 
				q.Enqueue(newPoint);
				Debug.Log("up");
			}

			newPoint = new Point(x1, y1 - 1);
			if (CheckValidity(readTexture, readTexture.width, readTexture.height, newPoint, sourceColor, tollerance))
            {
				q.Enqueue(newPoint);
				Debug.Log("down");
			}

			iterations++;
		}
	}

	static bool CheckValidity(Texture2D texture, int width, int height, Point p, Color sourceColor, float tollerance)
	{
		if (p.x < 0 || p.x >= width)
		{
			return false;
		}
		if (p.y < 0 || p.y >= height)
		{
			return false;
		}

		var color = texture.GetPixel(p.x, p.y);

		var distance = Mathf.Abs(color.r - sourceColor.r) + Mathf.Abs(color.g - sourceColor.g) + Mathf.Abs(color.b - sourceColor.b);
		return distance <= tollerance;
	}
}