using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI.Controls;

namespace ApartmentsManager.Behaviors
{
    public static class SliderRangeMaxBindingBehavior
    {
        public static readonly DependencyProperty RangeMaxProperty =
            DependencyProperty.RegisterAttached("RangeMax",
                typeof(string), typeof(SliderRangeMaxBindingBehavior),
                new PropertyMetadata(string.Empty, OnThumbDragCompleted));

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach",
                typeof(bool), typeof(SliderRangeMaxBindingBehavior), new PropertyMetadata(false, Attach));

        private static readonly DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached("IsUpdating", typeof(bool),
                typeof(SliderRangeMaxBindingBehavior), null);


        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }

        public static bool GetAttach(DependencyObject dp)
        {
            return (bool) dp.GetValue(AttachProperty);
        }

        public static int GetRangeMax(DependencyObject dp)
        {
            return (int) dp.GetValue(RangeMaxProperty);
        }

        public static void SetRangeMax(DependencyObject dp, int value)
        {
            dp.SetValue(RangeMaxProperty, value);
        }

        private static bool GetIsUpdating(DependencyObject dp)
        {
            return (bool) dp.GetValue(IsUpdatingProperty);
        }

        private static void SetIsUpdating(DependencyObject dp, bool value)
        {
            dp.SetValue(IsUpdatingProperty, value);
        }

        private static void OnThumbDragCompleted(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var rangeSelector = sender as RangeSelector;
            // ReSharper disable once PossibleNullReferenceException
            rangeSelector.ThumbDragCompleted -= RangeMaxChanged;

            if (!GetIsUpdating(rangeSelector)) rangeSelector.RangeMax = (int) e.NewValue;
            rangeSelector.ThumbDragCompleted += RangeMaxChanged;
        }

        private static void Attach(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;

            if (passwordBox == null)
                return;

            if ((bool) e.OldValue) passwordBox.PasswordChanged -= RangeMaxChanged;

            if ((bool) e.NewValue) passwordBox.PasswordChanged += RangeMaxChanged;
        }

        private static void RangeMaxChanged(object sender, RoutedEventArgs e)
        {
            var rangeSelector = sender as RangeSelector;
            SetIsUpdating(rangeSelector, true);
            // ReSharper disable once PossibleNullReferenceException
            SetRangeMax(rangeSelector, Convert.ToInt32(rangeSelector.RangeMax));
            SetIsUpdating(rangeSelector, false);
        }
    }
}
