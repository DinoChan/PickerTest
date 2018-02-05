using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace PickerTest
{
    [TemplatePart(Name = FlyoutName, Type = typeof(TimePickerFlyout))]
    public class MyTimePicker : Picker
    {
        private const string FlyoutName = "Flyout";

        /// <summary>
        ///     标识 Time 依赖属性。
        /// </summary>
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(TimeSpan), typeof(MyTimePicker), new PropertyMetadata(default(TimeSpan), OnTimeChanged));

        private TimePickerFlyout _timePickerFlyout;

        public MyTimePicker()
        {
            DefaultStyleKey = typeof(MyTimePicker);
        }

        /// <summary>
        ///     获取或设置Time的值
        /// </summary>
        public TimeSpan Time
        {
            get => (TimeSpan) GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        private static void OnTimeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as MyTimePicker;
            var oldValue = (TimeSpan) args.OldValue;
            var newValue = (TimeSpan) args.NewValue;
            if (oldValue != newValue)
                target.OnTimeChanged(oldValue, newValue);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _timePickerFlyout = GetTemplateChild(FlyoutName) as TimePickerFlyout;
            if (_timePickerFlyout != null)
                _timePickerFlyout.TimePicked += OnTimePicked;

            UpdateTimePicerFlyoutPickedTime();
            UpdateVisualState(false);
        }


        protected virtual void OnTimeChanged(TimeSpan oldValue, TimeSpan newValue)
        {
            UpdateTimePicerFlyoutPickedTime();
        }

        protected override void OnIsDropDownOpenChanged(bool oldValue, bool newValue)
        {
            base.OnIsDropDownOpenChanged(oldValue, newValue);
            if (newValue)
                UpdateTimePicerFlyoutPickedTime();
        }

        private void UpdateTimePicerFlyoutPickedTime()
        {
            if (_timePickerFlyout != null)
                _timePickerFlyout.Time = Time;
        }

        private void OnTimePicked(TimePickerFlyout sender, TimePickedEventArgs args)
        {
            Time = args.NewTime;
        }
    }
}