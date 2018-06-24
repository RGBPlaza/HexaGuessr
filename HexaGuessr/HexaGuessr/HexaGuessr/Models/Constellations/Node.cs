using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HexaGuessr.Models.Constellations
{
    class Node
    {
        // Mechanics
        public Vec2 Dir;
        public Point Position;
        public double Velocity;

        // Visuals
        static Random rand = new Random();
        public double Diameter;

        public static double canvasWidth;
        public static double canvasHeight;

        public Node()
        {
            Position = new Point(rand.NextDouble() * (canvasWidth - Diameter), Math.Max(rand.NextDouble() * (canvasHeight - Diameter), (canvasHeight * 0.2)));
            Dir = new Vec2(rand.Next(-32, 33), rand.Next(-32, 33));
            Velocity = (rand.NextDouble() * 2) + 1;
            Diameter = (rand.NextDouble() * 2) + 4;
        }

        public void ResetMechanics()
        {
            int sideToStart = rand.Next(0, 3);
            if (sideToStart == 0)
            {
                Position = new Point(-Diameter, Math.Max(rand.NextDouble() * (canvasHeight - Diameter), (canvasHeight * 0.2)));
                Dir = new Vec2(rand.Next(0, 33), rand.Next(-32, 33));
            }
            else if (sideToStart == 1)
            {
                Position = new Point(rand.NextDouble() * (canvasWidth - Diameter), canvasHeight);
                Dir = new Vec2(rand.Next(-32, 33), rand.Next(-32, 0));
            }
            else
            {
                Position = new Point(canvasWidth, Math.Max(rand.NextDouble() * (canvasHeight - Diameter), (canvasHeight * 0.2)));
                Dir = new Vec2(rand.Next(-32, 0), rand.Next(-32, 33));
            }

            Velocity = (rand.NextDouble() * 2) + 1;
            Diameter = (rand.NextDouble() * 2) + 4;
        }

        public double GetDistanceFrom(Node node)
        {
            double dx = node.Position.X - Position.X;
            double dy = node.Position.Y - Position.Y;

            return Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
        }

        public void Update(double deltaTime)
        {

            double angle = Math.Atan(Dir.Y / Dir.X);
            double hyp = deltaTime * Velocity;
            
            Position.X += hyp * Math.Cos(angle);
            Position.Y += hyp * Math.Sin(angle);

            if (Position.X < -Diameter || Position.X > canvasWidth || Position.Y < -Diameter || Position.Y > canvasHeight)
                ResetMechanics();
        }
    }
}
