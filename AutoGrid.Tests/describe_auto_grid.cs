using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Machine.Fakes;
using Machine.Specifications;
using Should;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local
namespace AutoGrid.Tests
{
    public class when_parsing_doubles : WithSubject<AutoGrid>
    {
        Because of = () => _result = AutoGrid.Parse("3,3,3,3");

        It should_be_all_threes = () =>
            _result.ToList().ForEach(x =>
                x.ShouldEqual(new GridLength(3)));

        static GridLength[] _result;
    }
    public class when_parsing_stars : WithSubject<AutoGrid>
    {
        Because of = () => _result = AutoGrid.Parse("*,*,*,*");

        It should_be_all_threes = () =>
            _result.ToList().ForEach(x =>
                x.ShouldEqual(new GridLength(1, GridUnitType.Star)));

        static GridLength[] _result;
    }

    public class when_row_and_column_count_set : WithSubject<AutoGrid>
    {
        Establish context = () =>
        {
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Columns = "*,*";
            Subject.PerformLayout();
        };

        It should_have_one_row = () => Subject.RowDefinitions.Count.ShouldEqual(1);
        It should_have_two_columns = () => Subject.ColumnDefinitions.Count.ShouldEqual(2);

        It should_make_first_child_row_index_zero = () => Grid.GetRow(Subject.Children[0]).ShouldEqual(0);
        It should_make_first_child_column_index_zero = () => Grid.GetColumn(Subject.Children[0]).ShouldEqual(0);

        It should_make_second_child_row_index_zero = () => Grid.GetRow(Subject.Children[1]).ShouldEqual(0);
        It should_make_second_child_column_index_one = () => Grid.GetColumn(Subject.Children[1]).ShouldEqual(1);
    }

    public class when_row_and_column_count_set_and_orientation_vertical : WithSubject<AutoGrid>
    {
        Establish context = () =>
        {
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Orientation = Orientation.Vertical;
            Subject.Columns = "*,Auto";
            Subject.PerformLayout();
        };

        It should_have_one_row = () => Subject.RowDefinitions.Count.ShouldEqual(1);
        It should_have_two_columns = () => Subject.ColumnDefinitions.Count.ShouldEqual(2);

        It should_make_first_child_row_index_zero = () => Grid.GetRow(Subject.Children[0]).ShouldEqual(0);
        It should_make_first_child_column_index_zero = () => Grid.GetColumn(Subject.Children[0]).ShouldEqual(0);

        It should_make_second_child_row_index_zero = () => Grid.GetRow(Subject.Children[1]).ShouldEqual(0);
        It should_make_second_child_column_index_one = () => Grid.GetColumn(Subject.Children[1]).ShouldEqual(1);
    }

    public class when_rows_are_defined_and_orientation_is_vertical : WithSubject<AutoGrid>
    {
        Establish context = () =>
        {
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Orientation = Orientation.Vertical;
            Subject.Rows = "*,*";
            Subject.PerformLayout();
        };

        It should_have_one_row = () => Subject.RowDefinitions.Count.ShouldEqual(2);
        It should_have_two_columns = () => Subject.ColumnDefinitions.Count.ShouldEqual(1);

        It should_make_first_child_row_index_zero = () => Grid.GetRow(Subject.Children[0]).ShouldEqual(0);
        It should_make_first_child_column_index_zero = () => Grid.GetColumn(Subject.Children[0]).ShouldEqual(0);

        It should_make_second_child_row_index_zero = () => Grid.GetRow(Subject.Children[1]).ShouldEqual(1);
        It should_make_second_child_column_index_one = () => Grid.GetColumn(Subject.Children[1]).ShouldEqual(0);
    }

    public class when_row_and_column_definitions_are_set : WithSubject<AutoGrid>
    {
        Establish context = () =>
        {
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Rows = "10";
            Subject.Columns = "100,*";
            Subject.PerformLayout();
        };

        It should_have_one_row = () => Subject.RowDefinitions.Count.ShouldEqual(1);
        It should_have_two_columns = () => Subject.ColumnDefinitions.Count.ShouldEqual(2);

        It should_make_first_child_column_index_zero = () => Grid.GetColumn(Subject.Children[0]).ShouldEqual(0);
        It should_make_second_child_column_index_one = () => Grid.GetColumn(Subject.Children[1]).ShouldEqual(1);
    }

    public class when_mixed_row_count_and_column_definition_are_set : WithSubject<AutoGrid>
    {
        Establish context = () =>
        {
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Columns = "100,*";
            Subject.PerformLayout();
        };

        It should_have_one_row = () => Subject.RowDefinitions.Count.ShouldEqual(1);
        It should_have_two_columns = () => Subject.ColumnDefinitions.Count.ShouldEqual(2);

        It should_make_first_child_column_index_zero = () => Grid.GetColumn(Subject.Children[0]).ShouldEqual(0);
        It should_make_second_child_column_index_one = () => Grid.GetColumn(Subject.Children[1]).ShouldEqual(1);
    }

    public class when_mixed_row_definition_and_column_count_are_set : WithSubject<AutoGrid>
    {
        Establish context = () =>
        {
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Rows = "10";
            Subject.PerformLayout();
        };

        It should_have_one_row = () => Subject.RowDefinitions.Count.ShouldEqual(1);
        It should_have_two_columns = () => Subject.ColumnDefinitions.Count.ShouldEqual(2);

        It should_make_first_child_column_index_zero = () => Grid.GetColumn(Subject.Children[0]).ShouldEqual(0);
        It should_make_second_child_column_index_one = () => Grid.GetColumn(Subject.Children[1]).ShouldEqual(1);
    }

    public class when_setting_one_row_height_and_adding_many_elements : WithSubject<AutoGrid>
    {
        Establish context = () =>
        {
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Columns = "100,*";
            Subject.PerformLayout();
        };

        It should_have_one_row = () => Subject.RowDefinitions.Count.ShouldEqual(3);
        It should_have_two_columns = () => Subject.ColumnDefinitions.Count.ShouldEqual(2);

        It should_make_first_child_row_index_zero = () => Grid.GetRow(Subject.Children[0]).ShouldEqual(0);
        It should_make_first_child_column_index_zero = () => Grid.GetColumn(Subject.Children[0]).ShouldEqual(0);

        It should_make_second_child_row_index_zero = () => Grid.GetRow(Subject.Children[1]).ShouldEqual(0);
        It should_make_second_child_column_index_one = () => Grid.GetColumn(Subject.Children[1]).ShouldEqual(1);

        It should_make_third_child_row_index_one = () => Grid.GetRow(Subject.Children[2]).ShouldEqual(1);
        It should_make_third_child_column_index_zero = () => Grid.GetColumn(Subject.Children[2]).ShouldEqual(0);

        It should_make_forth_child_row_index_one = () => Grid.GetRow(Subject.Children[3]).ShouldEqual(1);
        It should_make_forth_child_column_index_one = () => Grid.GetColumn(Subject.Children[3]).ShouldEqual(1);

        It should_make_fifth_child_row_index_two = () => Grid.GetRow(Subject.Children[4]).ShouldEqual(2);
        It should_make_fifth_child_column_index_zero = () => Grid.GetColumn(Subject.Children[4]).ShouldEqual(0);

        It should_make_sixth_child_row_index_two = () => Grid.GetRow(Subject.Children[5]).ShouldEqual(2);
        It should_make_sixth_child_column_index_one = () => Grid.GetColumn(Subject.Children[5]).ShouldEqual(1);
    }

    public class when_rows_are_defined_and_adding_many_elements : WithSubject<AutoGrid>
    {
        Establish context = () =>
        {
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Rows = "100,*";
            Subject.PerformLayout();
        };

        It should_have_one_row = () => Subject.RowDefinitions.Count.ShouldEqual(2);
        It should_have_two_columns = () => Subject.ColumnDefinitions.Count.ShouldEqual(3);

        It should_make_first_child_row_index_zero = () => Grid.GetRow(Subject.Children[0]).ShouldEqual(0);
        It should_make_first_child_column_index_zero = () => Grid.GetColumn(Subject.Children[0]).ShouldEqual(0);

        It should_make_second_child_row_index_zero = () => Grid.GetRow(Subject.Children[1]).ShouldEqual(0);
        It should_make_second_child_column_index_one = () => Grid.GetColumn(Subject.Children[1]).ShouldEqual(1);

        It should_make_third_child_row_index_one = () => Grid.GetRow(Subject.Children[2]).ShouldEqual(0);
        It should_make_third_child_column_index_zero = () => Grid.GetColumn(Subject.Children[2]).ShouldEqual(2);

        It should_make_forth_child_row_index_one = () => Grid.GetRow(Subject.Children[3]).ShouldEqual(1);
        It should_make_forth_child_column_index_one = () => Grid.GetColumn(Subject.Children[3]).ShouldEqual(0);

        It should_make_fifth_child_row_index_two = () => Grid.GetRow(Subject.Children[4]).ShouldEqual(1);
        It should_make_fifth_child_column_index_zero = () => Grid.GetColumn(Subject.Children[4]).ShouldEqual(1);

        It should_make_sixth_child_row_index_two = () => Grid.GetRow(Subject.Children[5]).ShouldEqual(1);
        It should_make_sixth_child_column_index_one = () => Grid.GetColumn(Subject.Children[5]).ShouldEqual(2);
    }

    public class when_rows_are_defined_with_column_span_and_adding_many_elements : WithSubject<AutoGrid>
    {
        Establish context = () =>
        {
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            var spannedElement = new Button();
            Grid.SetColumnSpan(spannedElement, 2);
            Subject.Children.Add(spannedElement);
            Subject.Columns = "100,*";
            Subject.PerformLayout();
        };

        It should_have_one_row = () => Subject.RowDefinitions.Count.ShouldEqual(3);
        It should_have_two_columns = () => Subject.ColumnDefinitions.Count.ShouldEqual(2);

        It should_make_first_child_row_index_zero = () => Grid.GetRow(Subject.Children[0]).ShouldEqual(0);
        It should_make_first_child_column_index_zero = () => Grid.GetColumn(Subject.Children[0]).ShouldEqual(0);

        It should_make_second_child_row_index_zero = () => Grid.GetRow(Subject.Children[1]).ShouldEqual(0);
        It should_make_second_child_column_index_one = () => Grid.GetColumn(Subject.Children[1]).ShouldEqual(1);

        It should_make_third_child_row_index_one = () => Grid.GetRow(Subject.Children[2]).ShouldEqual(1);
        It should_make_third_child_column_index_zero = () => Grid.GetColumn(Subject.Children[2]).ShouldEqual(0);

        It should_make_forth_child_row_index_one = () => Grid.GetRow(Subject.Children[3]).ShouldEqual(1);
        It should_make_forth_child_column_index_one = () => Grid.GetColumn(Subject.Children[3]).ShouldEqual(1);

        It should_make_fifth_child_row_index_two = () => Grid.GetRow(Subject.Children[4]).ShouldEqual(2);
        It should_make_fifth_child_column_index_zero = () => Grid.GetColumn(Subject.Children[4]).ShouldEqual(0);
    }

    public class when_rows_are_defined_and_adding_many_elements_with_one_missing : WithSubject<AutoGrid>
    {
        Establish context = () =>
        {
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Children.Add(new Button());
            Subject.Columns = "100,*";
            Subject.PerformLayout();
        };

        It should_have_one_row = () => Subject.RowDefinitions.Count.ShouldEqual(3);
        It should_have_two_columns = () => Subject.ColumnDefinitions.Count.ShouldEqual(2);

        It should_make_first_child_row_index_zero = () => Grid.GetRow(Subject.Children[0]).ShouldEqual(0);
        It should_make_first_child_column_index_zero = () => Grid.GetColumn(Subject.Children[0]).ShouldEqual(0);

        It should_make_second_child_row_index_zero = () => Grid.GetRow(Subject.Children[1]).ShouldEqual(0);
        It should_make_second_child_column_index_one = () => Grid.GetColumn(Subject.Children[1]).ShouldEqual(1);

        It should_make_third_child_row_index_one = () => Grid.GetRow(Subject.Children[2]).ShouldEqual(1);
        It should_make_third_child_column_index_zero = () => Grid.GetColumn(Subject.Children[2]).ShouldEqual(0);

        It should_make_forth_child_row_index_one = () => Grid.GetRow(Subject.Children[3]).ShouldEqual(1);
        It should_make_forth_child_column_index_one = () => Grid.GetColumn(Subject.Children[3]).ShouldEqual(1);

        It should_make_fifth_child_row_index_two = () => Grid.GetRow(Subject.Children[4]).ShouldEqual(2);
        It should_make_fifth_child_column_index_zero = () => Grid.GetColumn(Subject.Children[4]).ShouldEqual(0);
    }
}