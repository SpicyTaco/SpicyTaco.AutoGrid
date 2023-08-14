using System.Windows;
using System.Windows.Controls;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Xunit;
using Xunit.Extensions;

namespace AutoGrid.Tests
{
    public static class FluentAssertionExtensions
    {
        // ReSharper disable once InconsistentNaming
        public class UIElementAssertions : ReferenceTypeAssertions<UIElement, UIElementAssertions>
        {
#if NET5_0_OR_GREATER
            public UIElementAssertions(UIElement value) : base(value)
            {
            }
#else
            public UIElementAssertions(UIElement value)
            {
                Subject = value;
            }
#endif
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


#if NET5_0_OR_GREATER
            protected override string Identifier => "FrameworkElement";
#else
            protected override string Context => "FrameworkElement";
#endif
        }

        public static UIElementAssertions Should(this UIElement subject)
        {
            return new UIElementAssertions(subject);
        }
#if NET5_0_OR_GREATER
        public static AndConstraint<ObjectAssertions> ShouldBeEquivalentTo<T>(this T t, T value)
        {
            return t.Should().BeEquivalentTo(t);
        }
#endif
    }
}