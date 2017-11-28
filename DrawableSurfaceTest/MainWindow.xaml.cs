using System;
using System.Windows;
using System.Windows.Input;

namespace DrawableSurfaceTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int rowWidth = 100;
        int[] points = new int[10000];

        public MainWindow()
        {
            InitializeComponent();
            this.surface.Points = points;
            this.surface.RowPointCount = rowWidth;

        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            var inputPanel = sender as FrameworkElement;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var pos = e.GetPosition(inputPanel);
                var rowCount = Math.Ceiling((double)this.points.Length / this.rowWidth);
                var rowHeight = inputPanel.ActualHeight / rowCount;
                var colWidth = inputPanel.ActualWidth / this.rowWidth;

                var row = Math.Floor((pos.Y / inputPanel.ActualHeight) * rowCount);
                var col = Math.Floor((pos.X / inputPanel.ActualWidth) * this.rowWidth);

                int i = (int)(row * rowWidth + col);
                this.points[i] = this.surface.PointLifetime;
            }
        }
    }
}
