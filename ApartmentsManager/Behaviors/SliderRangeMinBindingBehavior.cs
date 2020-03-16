using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI.Controls;

namespace ApartmentsManager.Behaviors
{
    public static class SliderRangeMinBindingBehavior
    {
        public static readonly DependencyProperty RangeMinProperty =
            DependencyProperty.RegisterAttached("RangeMin",
                typeof(string), typeof(SliderRangeMinBindingBehavior),
                new PropertyMetadata(string.Empty, OnThumbDragCompleted));

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach",
                typeof(bool), typeof(SliderRangeMinBindingBehavior), new PropertyMetadata(false, Attach));

        private static readonly DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached("IsUpdating", typeof(bool),
                typeof(SliderRangeMinBindingBehavior), null);


        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }

        public static bool GetAttach(DependencyObject dp)
        {
            return (bool) dp.GetValue(AttachProperty);
        }

        public static int GetRangeMin(DependencyObject dp)
        {
            return (int) dp.GetValue(RangeMinProperty);
        }

        public static void SetRangeMin(DependencyObject dp, int value)
        {
            dp.SetValue(RangeMinProperty, value);
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
            rangeSelector.ThumbDragCompleted -= RangeMinChanged;

            if (!GetIsUpdating(rangeSelector)) rangeSelector.RangeMin = (int) e.NewValue;
            rangeSelector.ThumbDragCompleted += RangeMinChanged;
        }

        private static void Attach(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;

            if (passwordBox == null)
                return;

            if ((bool) e.OldValue) passwordBox.PasswordChanged -= RangeMinChanged;

            if ((bool) e.NewValue) passwordBox.PasswordChanged += RangeMinChanged;
        }

        private static void RangeMinChanged(object sender, RoutedEventArgs e)
        {
            var rangeSelector = sender as RangeSelector;
            SetIsUpdating(rangeSelector, true);
            // ReSharper disable once PossibleNullReferenceException
            SetRangeMin(rangeSelector, Convert.ToInt32(rangeSelector.RangeMin));
            SetIsUpdating(rangeSelector, false);
        }
    }
}
