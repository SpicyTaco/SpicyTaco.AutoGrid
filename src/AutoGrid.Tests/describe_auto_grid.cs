using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FluentAssertions;
using FluentAssertions.Collections;
using Xunit;

namespace AutoGrid.Tests
{
    public class ParseShould
    {
        [MyTestFact]
        public void HaveGridLengthOfThreeWhenParsingNumbers()
        {
            var result = AutoGrid.Parse("3,3,3,3");
            result.ToList().ForEach(x =>
                x.Should().Be(new GridLength(3)));
        }

        [MyTestFact]
        public void HaveGridLengthOfThreeWhenParsingStars()
        {
            var result = AutoGrid.Parse("*,*,*,*");
            result.ToList().ForEach(x =>
                x.Should().Be(new GridLength(1, GridUnitType.Star)));
        }
    }

    public class AutoGridWithTwoStarColumnsShould
    {
        public AutoGridWithTwoStarColumnsShould()
        {
            _sut = new AutoGrid();
            _sut.Children.Add(new Button());
            _sut.Children.Add(new Button());
            _sut.Columns = "*,*";
            _sut.PerformLayout();
        }

        [MyTestFact] public void HaveOneRow() => _sut.RowDefinitions.Count.Should().Be(1);
        [MyTestFact] public void HaveTwoColumns() => _sut.ColumnDefinitions.Count.Should().Be(2);

        [MyTestFact] public void MakeFirstChildRowIndexZero() => Grid.GetRow(_sut.Children[0]).Should().Be(0);
        [MyTestFact] public void MakeFirstChildColumnIndexZero() => Grid.GetColumn(_sut.Children[0]).Should().Be(0);

        [MyTestFact] public void MakeSecondChildRowIndexZero() => Grid.GetRow(_sut.Children[1]).Should().Be(0);
        [MyTestFact] public void MakeSecondChildColumnIndexOne() => Grid.GetColumn(_sut.Children[1]).Should().Be(1);
        private readonly AutoGrid _sut;
    }

    public class AutoGridWithStarAndAutoColumnsAndVerticalOrientationShould
    {
        public AutoGridWithStarAndAutoColumnsAndVerticalOrientationShould()
        {
            _sut = new AutoGrid();
            _sut.Children.Add(new Button());
            _sut.Children.Add(new Button());
            _sut.Orientation = Orientation.Vertical;
            _sut.Columns = "*,Auto";
            _sut.PerformLayout();
        }

        [MyTestFact] public void HaveOneRow() => _sut.RowDefinitions.Count.Should().Be(1);
        [MyTestFact] public void HaveTwoColumns() => _sut.ColumnDefinitions.Count.Should().Be(2);

        [MyTestFact] public void MakeFirstChildRowIndexZero() => Grid.GetRow(_sut.Children[0]).Should().Be(0);
        [MyTestFact] public void MakeFirstChildColumnIndexZero() => Grid.GetColumn(_sut.Children[0]).Should().Be(0);

        [MyTestFact] public void MakeSecondChildRowIndexZero() => Grid.GetRow(_sut.Children[1]).Should().Be(0);
        [MyTestFact] public void MakeSecondChildColumnIndexOne() => Grid.GetColumn(_sut.Children[1]).Should().Be(1);
        private readonly AutoGrid _sut;
    }

    public class AutoGridWithStarColumnsAndVerticalOrientationShould
    {
        public AutoGridWithStarColumnsAndVerticalOrientationShould()
        {
            Subject = new AutoGrid();
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Orientation = Orientation.Vertical;
            Subject.Rows = "*,*";
            Subject.PerformLayout();
        }

        [MyTestFact] public void should_have_one_row() => Subject.RowDefinitions.Count.Should().Be(2);
        [MyTestFact] public void should_have_two_columns() => Subject.ColumnDefinitions.Count.Should().Be(1);

        [MyTestFact] public void should_make_first_child_row_index_zero() => Grid.GetRow(Subject.Children[0]).Should().Be(0);
        [MyTestFact] public void should_make_first_child_column_index_zero() => Grid.GetColumn(Subject.Children[0]).Should().Be(0);

        [MyTestFact] public void should_make_second_child_row_index_zero() => Grid.GetRow(Subject.Children[1]).Should().Be(1);
        [MyTestFact] public void should_make_second_child_column_index_one() => Grid.GetColumn(Subject.Children[1]).Should().Be(0);
        private AutoGrid Subject;
    }

    public class AutoGridWithRowAndColumnDefinitionsShould
    {
        public AutoGridWithRowAndColumnDefinitionsShould()
        {
            Subject = new AutoGrid();
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Rows = "10";
            Subject.Columns = "100,*";
            Subject.PerformLayout();
        }

        [MyTestFact] public void should_have_one_row() => Subject.RowDefinitions.Count.Should().Be(1);
        [MyTestFact] public void should_have_two_columns() => Subject.ColumnDefinitions.Count.Should().Be(2);

        [MyTestFact] public void should_make_first_child_column_index_zero() => Grid.GetColumn(Subject.Children[0]).Should().Be(0);
        [MyTestFact] public void should_make_second_child_column_index_one() => Grid.GetColumn(Subject.Children[1]).Should().Be(1);
        private AutoGrid Subject;
    }

    public class when_mixed_row_count_and_column_definition_are_set
    {
        public when_mixed_row_count_and_column_definition_are_set()
        {
            Subject = new AutoGrid();
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Columns = "100,*";
            Subject.PerformLayout();
        }

        [MyTestFact] public void should_have_one_row() => Subject.RowDefinitions.Count.Should().Be(1);
        [MyTestFact] public void should_have_two_columns() => Subject.ColumnDefinitions.Count.Should().Be(2);

        [MyTestFact] public void should_make_first_child_column_index_zero() => Grid.GetColumn(Subject.Children[0]).Should().Be(0);
        [MyTestFact] public void should_make_second_child_column_index_one() => Grid.GetColumn(Subject.Children[1]).Should().Be(1);
        private AutoGrid Subject;
    }

    public class when_mixed_row_definition_and_column_count_are_set
    {
        public when_mixed_row_definition_and_column_count_are_set()
        {
            Subject = new AutoGrid();
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Rows = "10";
            Subject.PerformLayout();
        }

        [MyTestFact] public void should_have_one_row() => Subject.RowDefinitions.Count.Should().Be(1);
        [MyTestFact] public void should_have_two_columns() => Subject.ColumnDefinitions.Count.Should().Be(2);

        [MyTestFact] public void should_make_first_child_column_index_zero() => Grid.GetColumn(Subject.Children[0]).Should().Be(0);
        [MyTestFact] public void should_make_second_child_column_index_one() => Grid.GetColumn(Subject.Children[1]).Should().Be(1);
        private AutoGrid Subject;
    }

    public class when_setting_one_row_height_and_adding_many_elements
    {
        public when_setting_one_row_height_and_adding_many_elements()
        {
            Subject = new AutoGrid();
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Columns = "100,*";
            Subject.PerformLayout();
        }

        [MyTestFact] public void should_have_one_row() => Subject.RowDefinitions.Count.Should().Be(3);
        [MyTestFact] public void should_have_two_columns() => Subject.ColumnDefinitions.Count.Should().Be(2);

        [MyTestFact] public void should_make_first_child_row_index_zero() => Grid.GetRow(Subject.Children[0]).Should().Be(0);
        [MyTestFact] public void should_make_first_child_column_index_zero() => Grid.GetColumn(Subject.Children[0]).Should().Be(0);

        [MyTestFact] public void should_make_second_child_row_index_zero() => Grid.GetRow(Subject.Children[1]).Should().Be(0);
        [MyTestFact] public void should_make_second_child_column_index_one() => Grid.GetColumn(Subject.Children[1]).Should().Be(1);

        [MyTestFact] public void should_make_third_child_row_index_one() => Grid.GetRow(Subject.Children[2]).Should().Be(1);
        [MyTestFact] public void should_make_third_child_column_index_zero() => Grid.GetColumn(Subject.Children[2]).Should().Be(0);

        [MyTestFact] public void should_make_forth_child_row_index_one() => Grid.GetRow(Subject.Children[3]).Should().Be(1);
        [MyTestFact] public void should_make_forth_child_column_index_one() => Grid.GetColumn(Subject.Children[3]).Should().Be(1);

        [MyTestFact] public void should_make_fifth_child_row_index_two() => Grid.GetRow(Subject.Children[4]).Should().Be(2);
        [MyTestFact] public void should_make_fifth_child_column_index_zero() => Grid.GetColumn(Subject.Children[4]).Should().Be(0);

        [MyTestFact] public void should_make_sixth_child_row_index_two() => Grid.GetRow(Subject.Children[5]).Should().Be(2);
        [MyTestFact] public void should_make_sixth_child_column_index_one() => Grid.GetColumn(Subject.Children[5]).Should().Be(1);
        private AutoGrid Subject;
    }

    public class when_rows_are_defined_and_adding_many_elements
    {
        public when_rows_are_defined_and_adding_many_elements()
        {
            Subject = new AutoGrid();
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Rows = "100,*";
            Subject.PerformLayout();
        }

        [MyTestFact] public void should_have_one_row() => Subject.RowDefinitions.Count.Should().Be(2);
        [MyTestFact] public void should_have_two_columns() => Subject.ColumnDefinitions.Count.Should().Be(3);

        [MyTestFact] public void should_make_first_child_row_index_zero() => Grid.GetRow(Subject.Children[0]).Should().Be(0);
        [MyTestFact] public void should_make_first_child_column_index_zero() => Grid.GetColumn(Subject.Children[0]).Should().Be(0);

        [MyTestFact] public void should_make_second_child_row_index_zero() => Grid.GetRow(Subject.Children[1]).Should().Be(0);
        [MyTestFact] public void should_make_second_child_column_index_one() => Grid.GetColumn(Subject.Children[1]).Should().Be(1);

        [MyTestFact] public void should_make_third_child_row_index_one() => Grid.GetRow(Subject.Children[2]).Should().Be(0);
        [MyTestFact] public void should_make_third_child_column_index_zero() => Grid.GetColumn(Subject.Children[2]).Should().Be(2);

        [MyTestFact] public void should_make_forth_child_row_index_one() => Grid.GetRow(Subject.Children[3]).Should().Be(1);
        [MyTestFact] public void should_make_forth_child_column_index_one() => Grid.GetColumn(Subject.Children[3]).Should().Be(0);

        [MyTestFact] public void should_make_fifth_child_row_index_two() => Grid.GetRow(Subject.Children[4]).Should().Be(1);
        [MyTestFact] public void should_make_fifth_child_column_index_zero() => Grid.GetColumn(Subject.Children[4]).Should().Be(1);

        [MyTestFact] public void should_make_sixth_child_row_index_two() => Grid.GetRow(Subject.Children[5]).Should().Be(1);
        [MyTestFact] public void should_make_sixth_child_column_index_one() => Grid.GetColumn(Subject.Children[5]).Should().Be(2);
        private AutoGrid Subject;
    }

    public class when_rows_are_defined_with_column_span_and_adding_many_elements
    {
        public when_rows_are_defined_with_column_span_and_adding_many_elements()
        {
            Subject = new AutoGrid();
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            var spannedElement = new Button();
            Grid.SetColumnSpan(spannedElement, 2);
            Subject.Children.Add(spannedElement);
            Subject.Columns = "100,*";
            Subject.PerformLayout();
        }

        [MyTestFact] public void should_have_one_row() => Subject.RowDefinitions.Count.Should().Be(3);
        [MyTestFact] public void should_have_two_columns() => Subject.ColumnDefinitions.Count.Should().Be(2);

        [MyTestFact] public void should_make_first_child_row_index_zero() => Grid.GetRow(Subject.Children[0]).Should().Be(0);
        [MyTestFact] public void should_make_first_child_column_index_zero() => Grid.GetColumn(Subject.Children[0]).Should().Be(0);

        [MyTestFact] public void should_make_second_child_row_index_zero() => Grid.GetRow(Subject.Children[1]).Should().Be(0);
        [MyTestFact] public void should_make_second_child_column_index_one() => Grid.GetColumn(Subject.Children[1]).Should().Be(1);

        [MyTestFact] public void should_make_third_child_row_index_one() => Grid.GetRow(Subject.Children[2]).Should().Be(1);
        [MyTestFact] public void should_make_third_child_column_index_zero() => Grid.GetColumn(Subject.Children[2]).Should().Be(0);

        [MyTestFact] public void should_make_forth_child_row_index_one() => Grid.GetRow(Subject.Children[3]).Should().Be(1);
        [MyTestFact] public void should_make_forth_child_column_index_one() => Grid.GetColumn(Subject.Children[3]).Should().Be(1);

        [MyTestFact] public void should_make_fifth_child_row_index_two() => Grid.GetRow(Subject.Children[4]).Should().Be(2);
        [MyTestFact] public void should_make_fifth_child_column_index_zero() => Grid.GetColumn(Subject.Children[4]).Should().Be(0);
        private AutoGrid Subject;
    }

    public class WhenRowSpanningWithMultipleColumns
    {
        public WhenRowSpanningWithMultipleColumns()
        {
            Subject = new AutoGrid
            {
                Columns = "Auto,Auto,Auto",
                Orientation = Orientation.Horizontal,
            };
            var tallButton = new Button();
            Grid.SetRowSpan(tallButton, 99);
            Subject.Children.Add(tallButton);
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.PerformLayout();
        }

        [MyTestFact] public void ShouldHaveTwoRows() => Subject.RowDefinitions.Count.Should().Be(2);
        [MyTestFact] public void ShouldHaveThreeColumns() => Subject.ColumnDefinitions.Count.Should().Be(3);

        [MyTestFact] public void ShouldHaveFirstChildAtZeroZero() => Subject.Children[0].Should().BeAtGridPosition(0, 0);
        [MyTestFact] public void ShouldHaveSecondChildAtOneZero() => Subject.Children[1].Should().BeAtGridPosition(0, 1);
        [MyTestFact] public void ShouldHaveThirdChildAtOneZero() => Subject.Children[2].Should().BeAtGridPosition(0, 2);
        [MyTestFact] public void ShouldHaveForthChildAtOneZero() => Subject.Children[3].Should().BeAtGridPosition(1, 1);
        [MyTestFact] public void ShouldHaveFifthChildAtOneZero() => Subject.Children[4].Should().BeAtGridPosition(1, 2);
        private AutoGrid Subject;
    }

    public class WhenRowSpanningWithMultipleColumnsVertical
    {
        public WhenRowSpanningWithMultipleColumnsVertical()
        {
            Subject = new AutoGrid
            {
                Rows = "Auto,Auto",
                Orientation = Orientation.Vertical,
            };
            var wideButton = new Button();
            Grid.SetColumnSpan(wideButton, 99);
            Subject.Children.Add(wideButton);
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.PerformLayout();
        }

        [MyTestFact] public void ShouldHaveTwoRows() => Subject.RowDefinitions.Count.Should().Be(2);
        [MyTestFact] public void ShouldHaveThreeColumns() => Subject.ColumnDefinitions.Count.Should().Be(3);

        [MyTestFact] public void ShouldHaveFirstChildAtZeroZero() => Subject.Children[0].Should().BeAtGridPosition(0,0);
        [MyTestFact] public void ShouldHaveSecondChildAtOneZero() => Subject.Children[1].Should().BeAtGridPosition(1,0);
        [MyTestFact] public void ShouldHaveThirdChildAtOneZero() => Subject.Children[2].Should().BeAtGridPosition(1,1);
        [MyTestFact] public void ShouldHaveForthChildAtOneZero() => Subject.Children[3].Should().BeAtGridPosition(1,2);
        [MyTestFact] public void ShouldHaveFifthChildAtOneZero() => Subject.Children[4].Should().BeAtGridPosition(1,3);

        private AutoGrid Subject;
    }

    public class when_rows_are_defined_and_adding_many_elements_with_one_missing
    {
        public when_rows_are_defined_and_adding_many_elements_with_one_missing()
        {
            Subject = new AutoGrid();
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Columns = "100,*";
            Subject.PerformLayout();
        }

        [MyTestFact] public void should_have_one_row() => Subject.RowDefinitions.Count.Should().Be(3);
        [MyTestFact] public void should_have_two_columns() => Subject.ColumnDefinitions.Count.Should().Be(2);

        [MyTestFact] public void should_make_first_child_row_index_zero() => Grid.GetRow(Subject.Children[0]).Should().Be(0);
        [MyTestFact] public void should_make_first_child_column_index_zero() => Grid.GetColumn(Subject.Children[0]).Should().Be(0);

        [MyTestFact] public void should_make_second_child_row_index_zero() => Grid.GetRow(Subject.Children[1]).Should().Be(0);
        [MyTestFact] public void should_make_second_child_column_index_one() => Grid.GetColumn(Subject.Children[1]).Should().Be(1);

        [MyTestFact] public void should_make_third_child_row_index_one() => Grid.GetRow(Subject.Children[2]).Should().Be(1);
        [MyTestFact] public void should_make_third_child_column_index_zero() => Grid.GetColumn(Subject.Children[2]).Should().Be(0);

        [MyTestFact] public void should_make_forth_child_row_index_one() => Grid.GetRow(Subject.Children[3]).Should().Be(1);
        [MyTestFact] public void should_make_forth_child_column_index_one() => Grid.GetColumn(Subject.Children[3]).Should().Be(1);

        [MyTestFact] public void should_make_fifth_child_row_index_two() => Grid.GetRow(Subject.Children[4]).Should().Be(2);
        [MyTestFact] public void should_make_fifth_child_column_index_zero() => Grid.GetColumn(Subject.Children[4]).Should().Be(0);
        private AutoGrid Subject;
    }

    public class when_adding_additional_elements_outside_auto_assignment
    {
        public when_adding_additional_elements_outside_auto_assignment()
        {
            Subject = new AutoGrid();
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            var additionalElement = new TextBlock();
            AutoGrid.SetAutoIndex(additionalElement, false);
            Grid.SetColumn(additionalElement, 0);
            Grid.SetRow(additionalElement, 0);
            Grid.SetColumnSpan(additionalElement, 1);
            Subject.Children.Add(additionalElement);
            Subject.Columns = "100,100";
            Subject.PerformLayout();
        }

        [MyTestFact] public void should_have_one_row() => Subject.RowDefinitions.Count.Should().Be(1);
        [MyTestFact] public void should_have_two_columns() => Subject.ColumnDefinitions.Count.Should().Be(2);

        [MyTestFact] public void should_make_first_child_row_index_zero() => Grid.GetRow(Subject.Children[0]).Should().Be(0);
        [MyTestFact] public void should_make_first_child_column_index_zero() => Grid.GetColumn(Subject.Children[0]).Should().Be(0);

        [MyTestFact] public void should_make_second_child_row_index_zero() => Grid.GetRow(Subject.Children[1]).Should().Be(0);
        [MyTestFact] public void should_make_second_child_column_index_one() => Grid.GetColumn(Subject.Children[1]).Should().Be(1);

        [MyTestFact] public void should_not_change_third_child_row_index() => Grid.GetRow(Subject.Children[2]).Should().Be(0);
        [MyTestFact] public void should_not_change_third_child_column_index() => Grid.GetColumn(Subject.Children[2]).Should().Be(0);
        private AutoGrid Subject;
    }

    public class when_overriding_row_height
    {
        public when_overriding_row_height()
        {
            Subject = new AutoGrid();
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            var additionalElement = new TextBlock();
            AutoGrid.SetRowHeightOverride(additionalElement, new GridLength(1, GridUnitType.Star));
            Subject.Children.Add(additionalElement);
            Subject.Columns = "*";
            Subject.RowHeight = GridLength.Auto;
            Subject.PerformLayout();
        }

        [MyTestFact] public void should_have_three_rows() => Subject.RowDefinitions.Count.Should().Be(3);
        [MyTestFact] public void should_have_one_column() => Subject.ColumnDefinitions.Count.Should().Be(1);

        [MyTestFact] public void should_make_first_child_row_height_auto() => Subject.RowDefinitions[0].Height.Should().Be(GridLength.Auto);
        [MyTestFact] public void should_make_second_child_row_height_auto() => Subject.RowDefinitions[1].Height.Should().Be(GridLength.Auto);
        [MyTestFact] public void should_make_third_child_row_height_star() => Subject.RowDefinitions[2].Height.Should().Be(new GridLength(1, GridUnitType.Star));
        private AutoGrid Subject;
    }

    public class when_overriding_column_width
    {
        public when_overriding_column_width()
        {
            Subject = new AutoGrid();
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            var additionalElement = new TextBlock();
            AutoGrid.SetColumnWidthOverride(additionalElement, new GridLength(1, GridUnitType.Star));
            Subject.Children.Add(additionalElement);
            Subject.Rows = "*";
            Subject.ColumnWidth = GridLength.Auto;
            Subject.PerformLayout();
        }

        [MyTestFact] public void should_have_one_row() => Subject.RowDefinitions.Count.Should().Be(1);
        [MyTestFact] public void should_have_three_columns() => Subject.ColumnDefinitions.Count.Should().Be(3);

        [MyTestFact] public void should_make_first_child_column_width_auto() => Subject.ColumnDefinitions[0].Width.Should().Be(GridLength.Auto);
        [MyTestFact] public void should_make_second_child_column_width_auto() => Subject.ColumnDefinitions[1].Width.Should().Be(GridLength.Auto);
        [MyTestFact] public void should_make_third_child_column_width_star() => Subject.ColumnDefinitions[2].Width.Should().Be(new GridLength(1, GridUnitType.Star));
        private AutoGrid Subject;
    }

    public class when_overriding_row_height_and_vertical
    {
        public when_overriding_row_height_and_vertical()
        {
            Subject = new AutoGrid();
            Subject.Orientation = Orientation.Vertical;
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            var additionalElement = new TextBlock();
            AutoGrid.SetRowHeightOverride(additionalElement, new GridLength(1, GridUnitType.Star));
            Subject.Children.Add(additionalElement);
            Subject.Columns = "*";
            Subject.RowHeight = GridLength.Auto;
            Subject.PerformLayout();
        }

        [MyTestFact] public void should_have_three_rows() => Subject.RowDefinitions.Count.Should().Be(3);
        [MyTestFact] public void should_have_one_column() => Subject.ColumnDefinitions.Count.Should().Be(1);

        [MyTestFact] public void should_make_first_child_row_height_auto() => Subject.RowDefinitions[0].Height.Should().Be(GridLength.Auto);
        [MyTestFact] public void should_make_second_child_row_height_auto() => Subject.RowDefinitions[1].Height.Should().Be(GridLength.Auto);
        [MyTestFact] public void should_make_third_child_row_height_star() => Subject.RowDefinitions[2].Height.Should().Be(new GridLength(1, GridUnitType.Star));
        private AutoGrid Subject;
    }

    public class when_overriding_column_width_and_vertical
    {
        public when_overriding_column_width_and_vertical()
        {
            Subject = new AutoGrid();
            Subject.Orientation = Orientation.Vertical;
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            var additionalElement = new TextBlock();
            AutoGrid.SetColumnWidthOverride(additionalElement, new GridLength(1, GridUnitType.Star));
            Subject.Children.Add(additionalElement);
            Subject.Rows = "*";
            Subject.ColumnWidth = GridLength.Auto;
            Subject.PerformLayout();
        }

        [MyTestFact] public void should_have_one_row() => Subject.RowDefinitions.Count.Should().Be(1);
        [MyTestFact] public void should_have_three_columns() => Subject.ColumnDefinitions.Count.Should().Be(3);

        [MyTestFact] public void should_make_first_child_column_width_auto() => Subject.ColumnDefinitions[0].Width.Should().Be(GridLength.Auto);
        [MyTestFact] public void should_make_second_child_column_width_auto() => Subject.ColumnDefinitions[1].Width.Should().Be(GridLength.Auto);
        [MyTestFact] public void should_make_third_child_column_width_star() => Subject.ColumnDefinitions[2].Width.Should().Be(new GridLength(1, GridUnitType.Star));
        private AutoGrid Subject;
    }
}