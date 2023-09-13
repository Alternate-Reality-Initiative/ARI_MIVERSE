Example.scene is a scene where we have one Quad and a material on which we generate a new texture with some basic bitmap operations.


To get started all you need to do is add this at the beginning of your code:
using ProtoTurtle.BitmapDrawing;

Afterwards all your Texture2D instances will have the new extension methods. See Example.cs for a simple demonstration. Here's the snippet where we use the new methods on a Texture2D instance:

Texture2D texture = new Texture2D(512,512, TextureFormat.RGB24, false);
texture.wrapMode = TextureWrapMode.Clamp;
material.SetTexture(0, texture);
texture.DrawFilledRectangle(new Rect(0, 0, 120, 120), Color.green);
texture.Apply();


Available commands:

DrawPixel(position, color) - Draws a pixel but with the top left corner being position (x = 0, y = 0)
DrawLine(start, end, color) - Draws a line between two points
DrawCircle(position, radius, color) - Draws a circle
DrawFilledCircle(position, radius, color) - Draws a circle filled with a color
DrawRectangle(rectangle, color) - Draws a rectangle or a square
DrawFilledRectangle(rectangle, color) - Draws a rectangle or a square filled with a color
FloodFill(position, color) - Starts a flood fill of a certaing at the point


Please come visit the Unity forums thread if you have questions:
http://forum.unity3d.com/threads/released-bitmap-drawing-api-v0-8-open-source.251424/

This is also available on GitHub:
https://github.com/ProtoTurtle/UnityBitmapDrawing