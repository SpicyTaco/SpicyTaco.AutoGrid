using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace AutoGrid.Tests
{
    public class StackPanelMeasureShould
    {
        [MyTestFact]
        public void HaveDesiredSizeOfZeroWhenNoChildren()
        {
            var subject = new StackPanel();
            subject.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            subject.DesiredSize.ShouldBeEquivalentTo(new Size());
        }

        [MyTestFact]
        public void HaveDesiredSizeOfOneHundredWithChild()
        {
            var subject = new StackPanel();
            var uiElement = new Button
            {
                Width = 100,
                Height = 100
            };
            subject.Children.Add(uiElement);
            subject.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            subject.DesiredSize.ShouldBeEquivalentTo(new Size(100, 100));
        }

        [MyTestTheory]
        [InlineData(1, Orientation.Vertical, 100, 100)]
        [InlineData(3, Orientation.Vertical, 100, 300)]
        [InlineData(3, Orientation.Horizontal, 300, 100)]
        public void HaveDesiredSizeOfWithManyChildren(
            int numOfChildren, Orientation orientation, int expectedWidth, int expectedHeight)
        {
            var subject = new StackPanel {Orientation = orientation};
            Enumerable.Range(0, numOfChildren)
                .Select(x => new Button { Width = 100, Height = 100 })
                .ToList()
                .ForEach(x => subject.Children.Add(x));
            subject.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            subject.DesiredSize.ShouldBeEquivalentTo(new Size(expectedWidth, expectedHeight));
        }

        [MyTestFact]
        public void SplitSpaceWhenTwoFills()
        {
            var subject = new StackPanel();
            var innerLeft = new StackPanel();
            var innerRight = new StackPanel();
            StackPanel.SetFill(innerLeft, StackPanelFill.Fill);
            StackPanel.SetFill(innerRight, StackPanelFill.Fill);
            subject.Children.Add(innerLeft);
            subject.Children.Add(innerRight);
            subject.Orientation = Orientation.Horizontal;
            var uiElement = new Button
            {
                Content = "Foo"
            };
            var uiElement2 = new Button
            {
                Content = "Bar"
            };
            innerLeft.Children.Add(uiElement);
            innerRight.Children.Add(uiElement2);

            subject.Arrange(new Rect(new Size(800, 19.96)));

            innerLeft.ActualWidth.ShouldBeEquivalentTo(400);
            innerRight.ActualWidth.ShouldBeEquivalentTo(400);
        }
    }
}
