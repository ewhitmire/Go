using Go.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace Go
{
    public sealed class GoGrid : Grid
    {
        public GoGrid()
        {
            Loaded += GoGrid_Loaded;
        }

        void GoGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Redraw();
        }

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(GoGrid), new PropertyMetadata(5));

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cols.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(GoGrid), new PropertyMetadata(5));

        private static void OnSizeChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e
        )
        {
            var instance = d as GoGrid;
            if (instance != null)
            {
            instance.Redraw();
            }
        }


        private void Redraw()
        {
            double rowThickness = 10;
            double fakeHeight = rowThickness * (Rows - 1);
            double fakeWidth = rowThickness * (Columns - 1);


            SolidColorBrush lineBrush = new SolidColorBrush();
            lineBrush.Color = Colors.Blue;

            this.Children.Clear();

            Viewbox b = new Viewbox();
            b.StretchDirection = StretchDirection.Both;
            b.Stretch = Stretch.Uniform;
            Canvas canvas = new Canvas();
            b.Child = canvas;
            this.Children.Add(b);

            canvas.Height = fakeHeight;
            canvas.Width = fakeWidth;

            for (int r = 0; r < Rows; r++)
            {
                Line l = new Line();
                l.X1 = 0;
                l.X2 = fakeWidth;
                l.Y1 = rowThickness * r;
                l.Y2 = l.Y1;

                // Set Line's width and color
                l.StrokeThickness = .1;
                l.Stroke = lineBrush;

                canvas.Children.Add(l);
            }

            for (int c = 0; c < Columns; c++)
            {
                Line l = new Line();
                l.X1 = rowThickness * c;
                l.X2 = l.X1;
                l.Y1 = 0;
                l.Y2 = fakeHeight;

                // Set Line's width and color
                l.StrokeThickness = .1;
                l.Stroke = lineBrush;

                canvas.Children.Add(l);
            }

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    StonePosition stone = new StonePosition(r, c);
                    Canvas.SetTop(stone, rowThickness * r);
                    Canvas.SetLeft(stone, rowThickness * c);
                    canvas.Children.Add(stone);

                    stone.SetBinding(StonePosition.CommandProperty,
                            new Binding { Path = new PropertyPath("MoveCommand") });
                    stone.CommandParameter = new Space(r, c);
                }
            }
        }
    }

}
