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

            SizeChanged += (s, e) => 
            {
                Node.canvasWidth = Width;
                Node.canvasHeight = Height;
            };

            nodes = new List<Node>();
            nodeBoxes = new Dictionary<string, BoxView>();

            backColor = Color.FromHex("#16a085");
        }

        private Color backColor;
        private static Random rand = new Random();
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            PlayerInfo.CurrentScore = 0;
            PlayerInfo.CurrentRound = 0;

            TotalScoreLabel.Text = PlayerInfo.TotalMoons.ToString();
            if(PlayerInfo.TotalMoons >= 200 && !GuessColorButton.IsVisible)
            {
                GuessColorButton.IsVisible = true;
                GuessColorLock.IsVisible = false;
            }
            if (PlayerInfo.TotalMoons >= 500 && !MixedMarathonButton.IsVisible)
            {
                MixedMarathonButton.IsVisible = true;
                MixedMarathonLock.IsVisible = false;
            }

            double hue = backColor.Hue;

            // Trigger Update Loop
            Device.StartTimer(TimeSpan.FromMilliseconds(1000 / 60), () =>
            {
                if (nodes.Count == nodeCount)
                {
                    try
                    {
                        hue += 0.002;
                        if (hue >= 1)
                            hue = 0;

                        backColor = backColor.WithHue(hue);

                        ConstellationsCanvas.BackgroundColor = backColor;
                        GuessHexButton.TextColor = backColor;
                        GuessColorButton.TextColor = backColor;
                        MixedMarathonButton.TextColor = backColor;
                        LeaderBoardButton.TextColor = backColor;
                        
                        UpdateLoop();
                    }
                    catch { }
                }
                else
                    InitaliseNodes();

                return Navigation.NavigationStack.Last() == this || Device.RuntimePlatform == Device.Android;
            });

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ClearNodes();
        }
        
        int nodeCount = 6;
        float speedMultiplyer = 0.03f;

        List<Node> nodes;
        Dictionary<string, BoxView> nodeBoxes;

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

        public void ClearNodes()
        {
            ConstellationsCanvas.Children.Clear();
            nodes.Clear();
            nodeBoxes.Clear();
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
                            line.Opacity = (48 / distance) - 0.1;

                            AbsoluteLayout.SetLayoutBounds(line, new Rectangle(
                                ((currentNode.Position.X + passingNode.Position.X + currentNode.Diameter) / 2),
                                ((currentNode.Position.Y + passingNode.Position.Y - distance + currentNode.Diameter) / 2),
                                AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize
                            ));
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
        
        private async void GuessHexButton_Clicked(object sender, EventArgs e)
        {
            PlayerInfo.CurrentGameMode = GameMode.GuessHex;
            await Navigation.PushAsync(new GuessHexPage());
        }

        private async void GuessColorButton_Clicked(object sender, EventArgs e)
        {
            PlayerInfo.CurrentGameMode = GameMode.GuessColor;
            await Navigation.PushAsync(new GuessColorPage());
        }
        
        private async void MixedMarathonButton_Clicked(object sender, EventArgs e)
        {
            PlayerInfo.CurrentGameMode = GameMode.Mixed;
            if (rand.Next(0, 10) < 5)
                await Navigation.PushAsync(new GuessHexPage());
            else
                await Navigation.PushAsync(new GuessColorPage());
        }

        private async void LeaderBoardButton_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("hi");
            await Navigation.PushAsync(new LeaderBoardPage(backColor));
        }

    }
}