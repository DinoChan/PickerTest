using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace PickerTest
{
    [TemplatePart(Name = CalendarName, Type = typeof(CalendarView))]
    public class MyDatePicker : Picker

    {
        private const string CalendarName = "Calendar";

        /// <summary>
        ///     标识 DateTime 依赖属性。
        /// </summary>
        public static readonly DependencyProperty DateTimeProperty =
            DependencyProperty.Register("DateTime", typeof(DateTime), typeof(MyDatePicker), new PropertyMetadata(DateTime.Now, OnDateTimeChanged));

        private static void OnDateTimeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as MyDatePicker;
            var oldValue = (DateTime)args.OldValue;
            var newValue = (DateTime)args.NewValue;
            if (oldValue != newValue)
                target.OnDateTimeChanged(oldValue, newValue);
        }

        /// <summary>
        ///     标识 CalendarViewStyle 依赖属性。
        /// </summary>
        public static readonly DependencyProperty CalendarViewStyleProperty =
            DependencyProperty.Register("CalendarViewStyle", typeof(Style), typeof(MyDatePicker), new PropertyMetadata(null, OnCalendarViewStyleChanged));

        private static void OnCalendarViewStyleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as MyDatePicker;
            var oldValue = (Style)args.OldValue;
            var newValue = (Style)args.NewValue;
            if (oldValue != newValue)
                target.OnCalendarViewStyleChanged(oldValue, newValue);
        }

        public MyDatePicker()
        {
            DefaultStyleKey = typeof(MyDatePicker);
        }


        /// <summary>
        ///     获取或设置DateTime的值
        /// </summary>
        public DateTime DateTime
        {
            get => (DateTime) GetValue(DateTimeProperty);
            set => SetValue(DateTimeProperty, value);
        }

        /// <summary>
        ///     获取或设置CalendarViewStyle的值
        /// </summary>
        public Style CalendarViewStyle
        {
            get => (Style) GetValue(CalendarViewStyleProperty);
            set => SetValue(CalendarViewStyleProperty, value);
        }

        private CalendarView _calendar;

       
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _calendar = GetTemplateChild(CalendarName) as CalendarView;
            SetCalendarSelectedDate();
            UpdateVisualState(false);
        }

        protected override void OnAccept(RoutedEventArgs e)
        {
            base.OnAccept(e);
            if (_calendar != null && _calendar.SelectedDates.Any())
                DateTime = _calendar.SelectedDates.First().DateTime;
        }

        protected virtual void OnDateTimeChanged(DateTime oldValue, DateTime newValue)
        {
            SetCalendarSelectedDate();
        }

        protected virtual void OnCalendarViewStyleChanged(Style oldValue, Style newValue)
        {
        }

        protected override void OnIsDropDownOpenChanged(bool oldValue, bool newValue)
        {
            base.OnIsDropDownOpenChanged(oldValue, newValue);

            if (newValue)
                SetCalendarSelectedDate();
        }

        private void SetCalendarSelectedDate()
        {
            if (_calendar != null)
            {
                _calendar.SelectedDates.Clear();
                _calendar.SelectedDates.Add(DateTime);
            }
        }
    }
}