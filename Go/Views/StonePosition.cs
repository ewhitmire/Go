using Go.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace Go
{
   public sealed class StonePosition : Button
   {

      public int Row { get; private set; }

      public int Column { get; private set; }

      public StonePosition(int row, int column)
      {
         this.DefaultStyleKey = typeof(StonePosition);
         this.Row = row;
         this.Column = column;
      }

      /// <summary>
      /// Gets or sets the visual state of the space.
      /// </summary>
      public StoneState SpaceState
      {
          get { return (StoneState)GetValue(SpaceStateProperty); }
          set { SetValue(SpaceStateProperty, value); }
      }

      /// <summary>
      /// Identifier for the SpaceState dependency property.
      /// </summary>
      public static readonly DependencyProperty SpaceStateProperty =
          DependencyProperty.Register("SpaceState",
          typeof(StoneState), typeof(StonePosition),
          new PropertyMetadata(StoneState.None, SpaceStateChanged));

      /// <summary>
      /// Updates the visual state of the space to match the changed SpaceState value.
      /// </summary>
      /// <param name="d">The source of the property change.</param>
      /// <param name="e">Details about the property change.</param>
      private static void SpaceStateChanged(DependencyObject d,
          DependencyPropertyChangedEventArgs e)
      {
          (d as StonePosition).UpdateSpaceState();
      }

      /// <summary>
      /// Updates the visual state of the space, optionally using animated transitions.
      /// </summary>
      private void UpdateSpaceState()
      {
          String pieceState = SpaceState.ToString();
          VisualStateManager.GoToState(this, pieceState, true);
      }
   }
}
