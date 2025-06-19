using System.Collections.Concurrent;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CargoTransportation
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeVisuals();
            SimulationManager.Initialize(this);
        }

        Label bulldozerLabel = new Label();
        PictureBox bulldozerBox = new PictureBox();
        PictureBox[] loaderBoxes = new PictureBox[2];
        PictureBox[] truckBoxes = new PictureBox[4];

        System.Windows.Forms.Timer animationTimer = new System.Windows.Forms.Timer();
        Point bulldozerPosition = new Point(10, 60);
        int direction = 1;
        Random rnd = new();

        private ConcurrentDictionary<PictureBox, string> truckStates = new();
        private ConcurrentDictionary<PictureBox, string> loaderStates = new();

        public void InitializeVisuals()
        {
            this.Text = "Моделирование перевозки песка";
            this.Size = new Size(1000, 600);
            this.BackColor = Color.WhiteSmoke;

            Button startButton = new Button
            {
                Text = "Старт",
                Location = new Point(10, 10),
                Size = new Size(80, 30)
            };
            startButton.Click += startButton_Click;
            this.Controls.Add(startButton);

            Label mineLabel = new Label { Text = "Карьер", Location = new Point(50, 500), Size = new Size(80, 20) };
            Label loadLabel = new Label { Text = "Погрузка", Location = new Point(350, 500), Size = new Size(80, 20) };
            Label unloadLabel = new Label { Text = "Разгрузка", Location = new Point(700, 500), Size = new Size(80, 20) };
            this.Controls.Add(mineLabel);
            this.Controls.Add(loadLabel);
            this.Controls.Add(unloadLabel);

            
            bulldozerBox.Size = new Size(40, 40);
            bulldozerBox.Location = bulldozerPosition;
            bulldozerBox.BackColor = Color.Gray;
            this.Controls.Add(bulldozerBox);

       
            for (int i = 0; i < 2; i++)
            {
                loaderBoxes[i] = new PictureBox
                {
                    Size = new Size(30, 30),
                    Location = new Point(350, 100 + i * 50),
                    BackColor = Color.Blue
                };
                loaderStates[loaderBoxes[i]] = "idle";
                this.Controls.Add(loaderBoxes[i]);
            }

            for (int i = 0; i < 4; i++)
            {
                truckBoxes[i] = new PictureBox
                {
                    Size = new Size(50, 25),
                    Location = new Point(350, 100 + i * 40),
                    BackColor = Color.Brown
                };
                truckStates[truckBoxes[i]] = "loading";
                this.Controls.Add(truckBoxes[i]);
            }

            animationTimer.Interval = 100;
            animationTimer.Tick += (s, e) => AnimateVehicles();
            animationTimer.Start();

            logBox.Multiline = true;
            logBox.ScrollBars = ScrollBars.Vertical;
            logBox.Location = new Point(10, 300);
            logBox.Size = new Size(960, 250);
            this.Controls.Add(logBox);
        }

        private void AnimateVehicles()
        {
            
            bulldozerPosition.X += 5 * direction;
            if (bulldozerPosition.X > 300 || bulldozerPosition.X < 10)
                direction *= -1;
            bulldozerBox.Location = bulldozerPosition;
            int a = -1;
            

            foreach (var truck in truckBoxes)
            {
                if (truckStates[truck] == "moving")
                {
                    var p = truck.Location;
                    p.X += 5;
                    if (p.X > 900)
                    {
                        p.X = 350;
                        truckStates[truck] = "loading";
                    }
                    truck.Location = p;
                }
            }
        }

        public void LogToUI(string text)
        {
            if (InvokeRequired)
                Invoke(new Action(() => logBox.AppendText(text + "\r\n")));
            else
                logBox.AppendText(text + "\r\n");
        }

        public void UpdateStatus(string type, int id, string status)
        {
            this.Invoke((Action)(() =>
            {
                Color color = status switch
                {
                    "работает" => Color.LightGreen,
                    "отдых" => Color.Orange,
                    _ => Color.LightGray
                };

                if (type == "loader" && id < loaderBoxes.Length)
                {
                    loaderBoxes[id].BackColor = color;
                    loaderStates[loaderBoxes[id]] = status == "работает" ? "working" : "idle";
                }
                else if (type == "truck" && id < truckBoxes.Length)
                {
                    truckBoxes[id].BackColor = color;
                    truckStates[truckBoxes[id]] = status;
                }
            }));
        }

        private async void startButton_Click(object sender, EventArgs e)
        {
            await SimulationManager.StartSimulation();
        }
    }

    

   

    
    

    

}
