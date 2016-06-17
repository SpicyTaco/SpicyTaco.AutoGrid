using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using FluentAssertions;
using Machine.Fakes;
using Machine.Specifications;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local
namespace AutoGrid.Tests
{
    class when_no_children_size_should_be_zero : WithSubject<StackPanel>
    {
        Establish context = () =>
        {
            Subject.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
        };

        It should_have_desired_size_of_zero = () => Subject.DesiredSize.ShouldBeEquivalentTo(new Size());
    }

    class when_one_child_size_should_be_child_size : WithSubject<StackPanel>
    {
        Establish context = () =>
        {
            var uiElement = new Button
            {
                Width = 100,
                Height = 100
            };
            new System.Windows.Controls.StackPanel();
            Subject.Children.Add(uiElement);
        };

        Because of = () => Subject.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

        It should_have_desired_size_of_zero = () => Subject.DesiredSize.ShouldBeEquivalentTo(new Size(100, 100));
    }

    class when_many_children : WithSubject<StackPanel>
    {
        Establish context = () =>
        {
            var uiElements = Enumerable.Range(0, 3)
                .Select(x => new Button { Width = 100, Height = 100 });
            foreach (var uiElement in uiElements)
            {
                Subject.Children.Add(uiElement);
            }
        };

        Because of = () => Subject.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

        class as_horizontal
        {
            Establish context = () =>
            {
                Subject.Orientation = Orientation.Horizontal;
            };

            It should_have_desired_size = () => Subject.DesiredSize.ShouldBeEquivalentTo(new Size(300, 100));
        }

        class as_vertical
        {
            Establish context = () =>
            {
                Subject.Orientation = Orientation.Vertical;
            };

            It should_have_desired_size = () => Subject.DesiredSize.ShouldBeEquivalentTo(new Size(100, 300));
        }
    }
}
