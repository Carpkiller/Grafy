using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Grafy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GraphPane myPane = zg1.GraphPane;

            // Set the titles and axis labels
            myPane.Title.Text = "My Test Graph";
            myPane.XAxis.Title.Text = "X Value";
            myPane.YAxis.Title.Text = "My Y Axis";


            PointPairList list = new PointPairList();
            for (double x = 0; x < 36; x++)
            {
                double y = Math.Sin(x * Math.PI / 15.0);

                list.Add(x, y);
            }

            LineItem myCurve = myPane.AddCurve("My Curve", list, Color.Blue,
                                    SymbolType.Circle);
            zg1.AxisChange();
        }
    }
}
