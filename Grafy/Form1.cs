using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Grafy
{
    public partial class Form1 : Form
    {
        Chart chart1;
        private Point positionDown;
            private Point positionUp;
        private int poc = 0;

        private double horneD;
        private double dolneD;
        private ToolTip tool;

        public Form1()
        {
            tool = new ToolTip();
            InitializeComponent();
        }

        private double f(int i)
        {
            var f1 = 59894 - (8128 * i) + (262 * i * i) - (1.6 * i * i * i);
            return f1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            var series1 = new Series
            {
                Name = "Series1",
                Color = Color.Green,
                IsVisibleInLegend = true,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Candlestick
            };
            var series2 = new Series
            {
                Name = "Series2",
                ChartType = SeriesChartType.Line,
                Color = Color.Black,
                BorderWidth = 2,
                //  IsVisibleInLegend = true,
                 IsXValueIndexed = false,
                
            };

            var series3 = new Series
            {
                Name = "Series3",
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 2,
                
            };

            chart1.Series.Add(series1); 
            chart1.Series.Add(series2);
            chart1.Series.Add(series3);

            for (int i = 0; i < 100; i++)
            {
                series1.Points.AddXY(i, f(i)+12000, f(i) - 15000, f(i) + 7000, f(i) - 7000);
            }
            chart1.Invalidate();

            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
        }

        private void chart1_Click(object sender, EventArgs e)
        {
        }

        private void chart1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {

            if (checkBox1.Checked)
            {

                poc = 1;
                positionDown = MousePosition;

                chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(new Point(e.X, e.Y), true);
                chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(new Point(e.X, e.Y), true);

                double pX = chart1.ChartAreas[0].CursorX.Position; //X Axis Coordinate of your mouse cursor
                double pY = chart1.ChartAreas[0].CursorY.Position; //Y Axis Coordinate of your mouse cursor
                var zacSur = pY.ToString();

                if (chart1.Series[2].Points.Count > 0)
                {
                    chart1.Series[2].Points.Clear();
                }

                chart1.Series[2].Points.AddXY(pX, pY);
                chart1.Series[2].Points.First().Label = zacSur;
                horneD = pY;
                chart1.Invalidate();
            }
            else
            {
            }
        }

        private void chart1_MouseUp_1(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("koncova - "+positionUp);
            if (checkBox1.Checked)
            {
                positionUp = MousePosition;
                poc = 0;
            }
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            
            tool.RemoveAll();
            chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(new Point(e.X, e.Y), true);
            chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(new Point(e.X, e.Y), true);
            double pX = chart1.ChartAreas[0].CursorX.Position; //X Axis Coordinate of your mouse cursor
            double pY = chart1.ChartAreas[0].CursorY.Position; //Y Axis Coordinate of your mouse cursor

            if (checkBox1.Checked)
            {
                if (poc == 1)
                {
                    positionUp = chart1.PointToScreen(MousePosition);

                    chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(new Point(e.X, e.Y), true);
                    chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(new Point(e.X, e.Y), true);

                    
                    if (chart1.Series[2].Points.Count == 2)
                    {
                        chart1.Series[2].Points.RemoveAt(1);
                    }
                    chart1.Series[2].Points.AddXY(pX, pY);
                    chart1.Series[2].Points.Last().Label = pY.ToString();
                    dolneD = pY;
                    label1.Text = pocitajCenu();

                 //   chart1.Series[1].valueX.Text = diagramCoordinates.DateTimeArgument.ToShortDateString();
                 //   chart1.valueY.Text = "$" + Math.Round(seriesValue);
                //    chart1.ChartAreas[0].AxisX. = true;
                    
                    chart1.Invalidate();
                }
            }

            //    tool.Show(pY.ToString(), this.chart1, new Point(chart1.Location.X + chart1.Width-130, e.Y));
            
            label2.Text = pY.ToString();
            label3.Text = pX.ToString();
            chart1.Invalidate();
        }

        private string pocitajCenu()
        {
            if (horneD > dolneD)
                return ((horneD - dolneD)*5).ToString("N2");

            return ((dolneD - horneD) * 5).ToString("C2");
        }

        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
            chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                chart1.ChartAreas[0].CursorX.IsUserEnabled = false;
                chart1.ChartAreas[0].CursorY.IsUserEnabled = false;
                chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
            }
            else
            {
                chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
                chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
                chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            }
        }
    }
}
