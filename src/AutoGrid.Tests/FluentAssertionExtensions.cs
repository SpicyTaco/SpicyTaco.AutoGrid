using System.Windows;
using System.Windows.Controls;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace AutoGrid.Tests
{
    public static class FluentAssertionExtensions
    {
        // ReSharper disable once InconsistentNaming
        public class UIElementAssertions : ReferenceTypeAssertions<UIElement, UIElementAssertions>
        {
            public UIElementAssertions(UIElement value)
            {
                Subject = value;
            }

            public AndConstraint<UIElementAssertions> BeAtGridPosition(
                int expectedRow, int expectedColumn, string because = "", params object[] becauseArgs)
            {
                var row = Grid.GetRow(Subject);
                var column = Grid.GetColumn(Subject);

                Execute.Assertion
                    .ForCondition(row == expectedRow && column == expectedColumn)
                    .BecauseOf(because, becauseArgs)
                    .FailWith($"Expected (row, column) of ({expectedRow},{expectedColumn}) but got ({row},{column}) {{reason}}");

                return new AndConstraint<UIElementAssertions>(this);
            }

            protected override string Context => "FrameworkElement";
        }

        public static UIElementAssertions Should(this UIElement subject)
        {
            return new UIElementAssertions(subject);
        }
    }
}