using HexaGuessr.Models;
using HexaGuessr.Models.Constellations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HexaGuessr.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
        
		public MenuPage ()
		{
			InitializeComponent ();
            ConstellationsCanvas.SizeChanged += (s, e) => 
            {
                Node.canvasWidth = ConstellationsCanvas.Width;
                Node.canvasHeight = ConstellationsCanvas.Height;
            };
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            InitaliseNodes();

            Device.StartTimer(TimeSpan.FromMilliseconds(1000/60), () => { UpdateLoop(); return true; });

            Color backColor = Color.FromHex("#16a085");
            double hue = backColor.Hue;
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                try
                {
                    hue += 0.002;
                    if (hue >= 1)
                        hue = 0;

                    backColor = backColor.WithHue(hue);

                    ConstellationsCanvas.BackgroundColor = backColor;
                    GuessHexButton.TextColor = backColor;
                    GuessColourButton.TextColor = backColor;
                }
                catch { }
                return true;
            });
            /*BackgroundHolder.FadeTo(0, 10000);

            int currentColorIndex = 0;
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                if (currentColorIndex > 6)
                    currentColorIndex = 0;
                else
                    currentColorIndex++;


                if (currentColorIndex % 2 == 0)
                {
                    BackgroundColor = colorGenerator.NextColor();
                    BackgroundHolder.FadeTo(0, 10000);
                }
                else
                {
                    BackgroundHolder.BackgroundColor = colorGenerator.NextColor();
                    BackgroundHolder.FadeTo(1, 10000);
                }

                return true;
            });*/

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        
        const int nodeCount = 6;
        float speedMultiplyer = 0.03f;

        List<Node> nodes = new List<Node>();
        Dictionary<string, BoxView> nodeBoxes = new Dictionary<string, BoxView>();

        public void InitaliseNodes()
        {
            for (int i = 0; i < nodeCount; i++)
            {
                Node node = new Node();
                nodes.Add(node);

                BoxView boxView = new BoxView() { WidthRequest = node.Diameter, HeightRequest = node.Diameter, BackgroundColor = Color.White, Opacity = node.Diameter / 6};

                ConstellationsCanvas.Children.Add(boxView, node.Position);
                nodeBoxes.Add($"Node_{i}", boxView);

                for (int j = i + 1; j < nodeCount; j++)
                {

                    BoxView line = new BoxView()
                    {
                        BackgroundColor = Color.White,
                        WidthRequest = 1,
                        HeightRequest = 0,
                    };
                    
                    ConstellationsCanvas.Children.Add(line);
                    nodeBoxes.Add($"Line_{i}_{j}", line);
                }
            }
            
        }

        public void UpdateLoop()
        {
            try
            {
                for (int i = 0; i < nodeCount; i++)
                {
                    Node currentNode = nodes[i];

                    currentNode.Update(20 * speedMultiplyer);
                    BoxView nodeBoxView = nodeBoxes[$"Node_{i}"];
                    if (nodeBoxView != null)
                    {
                        AbsoluteLayout.SetLayoutBounds(nodeBoxView, new Rectangle(currentNode.Position, new Size(AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)));
                    }


                    for (int j = i + 1; j < nodeCount; j++)
                    {
                        Node passingNode = nodes[j];
                        double distance = currentNode.GetDistanceFrom(passingNode);
                        
                        if (distance <= 360 && distance > 0)
                        {
                            BoxView line;
                            if (!nodeBoxes.ContainsKey($"Line_{i}_{j}"))
                            {
                                line = new BoxView()
                                {
                                    BackgroundColor = Color.White,
                                    WidthRequest = 1,
                                };

                                ConstellationsCanvas.Children.Add(line);
                                nodeBoxes.Add($"Line_{i}_{j}", line);
                            }
                            else
                                line = nodeBoxes[$"Line_{i}_{j}"];

                            line.HeightRequest = distance;
                            line.Rotation = ((Math.Atan((passingNode.Position.Y - currentNode.Position.Y) / (passingNode.Position.X - currentNode.Position.X)) / Math.PI * 180) + 90);

                            AbsoluteLayout.SetLayoutBounds(line, new Rectangle(
                                ((currentNode.Position.X + passingNode.Position.X + currentNode.Diameter) / 2),
                                ((currentNode.Position.Y + passingNode.Position.Y - distance + currentNode.Diameter) / 2),
                                AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize
                            ));


                            //line.X2 = passingNode.Position.X + (passingNode.Diameter / 2);
                            //line.Y2 = passingNode.Position.Y + (passingNode.Diameter / 2);

                            line.Opacity = (48 / distance) - 0.1;
                        }
                        else
                        {
                            if (nodeBoxes.ContainsKey($"Line_{i}_{j}"))
                            {
                                ConstellationsCanvas.Children.Remove(nodeBoxes[$"Line_{i}_{j}"]);
                                nodeBoxes.Remove($"Line_{i}_{j}");
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

    }
}