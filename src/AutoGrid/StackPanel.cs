using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AutoGrid
{
    public class StackPanel : Panel
    {
        protected override Size MeasureOverride(Size constraint)
        {
            UIElementCollection children = InternalChildren;

            double parentWidth = 0;   // Our current required width due to children thus far. 
            double parentHeight = 0;   // Our current required height due to children thus far.
            double accumulatedWidth = 0;   // Total width consumed by children. 
            double accumulatedHeight = 0;   // Total height consumed by children. 
            
            foreach (var child in children.OfType<UIElement>().Where(x => GetFill(x) == StackPanelFill.Auto))
            {
                Size childConstraint;             // Contains the suggested input constraint for this child.
                Size childDesiredSize;            // Contains the return size from child measure. 

                // Child constraint is the remaining size; this is total size minus size consumed by previous children.
                childConstraint = Orientation == Orientation.Horizontal
                    ? new Size(Math.Max(0.0, constraint.Width - accumulatedWidth),
                               Math.Max(accumulatedHeight, constraint.Height))
                    : new Size(Math.Max(accumulatedWidth, constraint.Width),
                               Math.Max(0.0, constraint.Height - accumulatedHeight));

                // Measure child.
                child.Measure(childConstraint);
                childDesiredSize = child.DesiredSize;
                
                if (Orientation == Orientation.Horizontal)
                {
                    accumulatedWidth += childDesiredSize.Width;
                    accumulatedHeight = Math.Max(accumulatedHeight, childDesiredSize.Height);
                }
                else
                {
                    accumulatedWidth = Math.Max(accumulatedWidth, childDesiredSize.Width);
                    accumulatedHeight += childDesiredSize.Height;
                }
            }

            var marginMultiplier = Math.Max(children.Count - 1, 0);
            var marginToAdd = MarginBetweenChildren*marginMultiplier;

            if (Orientation == Orientation.Horizontal)
            {
                accumulatedWidth += marginToAdd;
            }
            else
            {
                accumulatedHeight += marginToAdd;
            }

            foreach (var child in children.OfType<UIElement>().Where(x => GetFill(x) == StackPanelFill.Fill))
            {
                Size childConstraint;             // Contains the suggested input constraint for this child.
                Size childDesiredSize;            // Contains the return size from child measure. 
                
                // Child constraint is the remaining size; this is total size minus size consumed by previous children.
                childConstraint = Orientation == Orientation.Horizontal
                    ? new Size(Math.Max(0.0, constraint.Width - accumulatedWidth),
                               Math.Max(accumulatedHeight, constraint.Height))
                    : new Size(Math.Max(accumulatedWidth, constraint.Width),
                               Math.Max(0.0, constraint.Height - accumulatedHeight));

                // Measure child.
                child.Measure(childConstraint);
                childDesiredSize = child.DesiredSize;

                if (Orientation == Orientation.Horizontal)
                {
                    accumulatedWidth += childDesiredSize.Width;
                    accumulatedHeight = Math.Max(accumulatedHeight, childDesiredSize.Height);
                }
                else
                {
                    accumulatedWidth = Math.Max(accumulatedWidth, childDesiredSize.Width);
                    accumulatedHeight += childDesiredSize.Height;
                }
            }

            // Make sure the final accumulated size is reflected in parentSize. 
            parentWidth = Math.Max(parentWidth, accumulatedWidth);
            parentHeight = Math.Max(parentHeight, accumulatedHeight);

            return new Size(parentWidth, parentHeight);
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            UIElementCollection children = InternalChildren;
            int totalChildrenCount = children.Count;
            
            double accumulatedLeft = 0;
            double accumulatedTop = 0;

            var marginMultiplier = Math.Max(totalChildrenCount - 1, 0);
            var totalMarginToAdd = MarginBetweenChildren * marginMultiplier;
            var isHorizontal = Orientation == Orientation.Horizontal;

            double allAutoSizedSum = 0.0;
            int countOfFillTypes = 0;
            foreach (var child in children.OfType<UIElement>())
            {
                var fillType = GetFill(child);
                if (fillType == StackPanelFill.Fill)
                {
                    countOfFillTypes += 1;
                }
                else
                {
                    var desiredSize = isHorizontal ? child.DesiredSize.Width : child.DesiredSize.Height;
                    allAutoSizedSum += desiredSize;
                }
            }

            var remainingForFillTypes = isHorizontal
                ? Math.Max(0, arrangeSize.Width - allAutoSizedSum - totalMarginToAdd)
                : Math.Max(0, arrangeSize.Height - allAutoSizedSum - totalMarginToAdd);
            var fillTypeSize = remainingForFillTypes / countOfFillTypes;

            for (int i = 0; i < totalChildrenCount; ++i)
            {
                UIElement child = children[i];
                if (child == null) { continue; }
                var isLastChild = i == totalChildrenCount - 1;
                var marginToAdd = isLastChild ? 0 : MarginBetweenChildren;
                var fillType = GetFill(child);

                Size childDesiredSize = child.DesiredSize;
                Rect rcChild = new Rect(
                    accumulatedLeft,
                    accumulatedTop,
                    Math.Max(0.0, arrangeSize.Width - accumulatedLeft),
                    Math.Max(0.0, arrangeSize.Height - accumulatedTop));
                
                if (isHorizontal)
                {
                    rcChild.Width = fillType == StackPanelFill.Auto ? childDesiredSize.Width : fillTypeSize;
                    rcChild.Height = arrangeSize.Height;
                    accumulatedLeft += rcChild.Width + marginToAdd;
                }
                else
                {
                    rcChild.Width = arrangeSize.Width;
                    rcChild.Height = fillType == StackPanelFill.Auto ? childDesiredSize.Height : fillTypeSize;
                    accumulatedTop += rcChild.Height + marginToAdd;
                }

                child.Arrange(rcChild);
            }

            return arrangeSize;
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation", typeof(Orientation), typeof(StackPanel), 
            new FrameworkPropertyMetadata(
                Orientation.Vertical, 
                FrameworkPropertyMetadataOptions.AffectsArrange | 
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Orientation Orientation
        {
            get { return (Orientation) GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty MarginBetweenChildrenProperty = DependencyProperty.Register(
            "MarginBetweenChildren", typeof(double), typeof(StackPanel),
            new FrameworkPropertyMetadata(
                0.0, 
                FrameworkPropertyMetadataOptions.AffectsArrange | 
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double MarginBetweenChildren
        {
            get { return (double) GetValue(MarginBetweenChildrenProperty); }
            set { SetValue(MarginBetweenChildrenProperty, value); }
        }

        public static readonly DependencyProperty FillProperty = DependencyProperty.RegisterAttached(
            "Fill", typeof(StackPanelFill), typeof(StackPanel), 
            new FrameworkPropertyMetadata(
                StackPanelFill.Auto, 
                FrameworkPropertyMetadataOptions.AffectsArrange | 
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsParentArrange |
                FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        public static void SetFill(DependencyObject element, StackPanelFill value)
        {
            element.SetValue(FillProperty, value);
        }

        public static StackPanelFill GetFill(DependencyObject element)
        {
            return (StackPanelFill) element.GetValue(FillProperty);
        }
    }

    public enum StackPanelFill
    {
        Auto, Fill
    }
}
