using System;
using System.Windows;
using System.Windows.Media;

namespace DrawableSurfaceTest
{
    class DrawingSurface : FrameworkElement
    {
        public DrawingSurface()
        {
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            this.AddVisualChild(this.visual);
            this.AddLogicalChild(this.visual);
        }

 

        private DrawingVisual visual = new DrawingVisual();

        public int[] Points { get; set; }
        public int RowPointCount { get; set; } = 50;

        public Brush Background { get; set; } = Brushes.White;
        public int PointLifetime { get; set; } = 60;

        protected override int VisualChildrenCount => 1;
        protected override Visual GetVisualChild(int index) => this.visual;

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            var points = this.Points ?? new int[0];
            var rowCount = Math.Ceiling((double)points.Length / this.RowPointCount);
            var width = this.ActualWidth / this.RowPointCount;
            var height = this.ActualHeight / rowCount;
            var size = new Size(width, height);

            var ctx = this.visual.RenderOpen();
            //clear
            ctx.DrawRectangle(this.Background, new Pen(), new Rect(new Size(this.ActualWidth, this.ActualHeight)));

            for (int i = 0; i < points.Length; ++i)
            {
                if (points[i] > 0)
                {
                    var health = (double)points[i] / this.PointLifetime;
                    var intensity = (byte)(255 - (health * 255));
                    var cellColor = Color.FromRgb(intensity, intensity, intensity);
                    var row = Math.Floor((double)i / this.RowPointCount);
                    var col = i % this.RowPointCount;
                    var cellRect = new Rect(new Point(col * width, row * height), size);
                    ctx.DrawRectangle(new SolidColorBrush(cellColor), new Pen(), cellRect);
                    points[i]--;
                }
            }
            ctx.Close();
        }
    }
}
