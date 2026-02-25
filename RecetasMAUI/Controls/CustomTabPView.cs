using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RecetasMAUI.Controls
{
    public class CustomTabbedView : ContentView
    {
        public static readonly BindableProperty TabsProperty =
            BindableProperty.Create(nameof(Tabs), typeof(ObservableCollection<TabItem>),
                typeof(CustomTabbedView), new ObservableCollection<TabItem>());
        public static readonly BindableProperty SelectedTabIndexProperty =
            BindableProperty.Create(nameof(SelectedTabIndex), typeof(int),
                typeof(CustomTabbedView), 0, propertyChanged: OnSelectedTabIndexChanged);
        public ObservableCollection<TabItem> Tabs
        {
            get => (ObservableCollection<TabItem>)GetValue(TabsProperty);
            set => SetValue(TabsProperty, value);
        }
        public int SelectedTabIndex
        {
            get => (int)GetValue(SelectedTabIndexProperty);
            set => SetValue(SelectedTabIndexProperty, value);
        }
        private Grid _mainGrid;
        private HorizontalStackLayout _tabStrip;
        private ContentView _contentArea;
        public CustomTabbedView()
        {
            BuildLayout();
        }
        private void BuildLayout()
        {
            _mainGrid = new Grid
            {
                RowDefinitions = new RowDefinitionCollection {
            new RowDefinition { Height = GridLength.Auto },
            new RowDefinition { Height = GridLength.Star }
        }
            };
            _tabStrip = new HorizontalStackLayout { Spacing = 0 };
            _contentArea = new ContentView();
            _mainGrid.Children.Add(_tabStrip);
            Grid.SetRow(_tabStrip, 0);
            _mainGrid.Children.Add(_contentArea);
            Grid.SetRow(_contentArea, 1);
            Content = _mainGrid;
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            RenderTabs();
        }
        private void RenderTabs()
        {
            _tabStrip.Children.Clear();
            _tabStrip.BackgroundColor = Color.FromRgba("#3f51b5");
            for (int i = 0; i < Tabs.Count; i++)
            {
                var tab = Tabs[i];
                var tabIndex = i;
                var button = new Button
                {
                    Text = tab.Title,
                    BackgroundColor = i == SelectedTabIndex ? Colors.Blue : Color.FromArgb("#ff481"),
                    TextColor = Colors.White,
                    CornerRadius = 0
                };
                button.Clicked += (s, e) => SelectedTabIndex = tabIndex;
                _tabStrip.Children.Add(button);
            }
            UpdateContent();
        }
        private static void OnSelectedTabIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomTabbedView)bindable;
            control.UpdateContent();
            control.UpdateTabStyles();
        }
        private void UpdateContent()
        {
            if (Tabs != null && SelectedTabIndex >= 0 && SelectedTabIndex < Tabs.Count)
            {
                _contentArea.Content = Tabs[SelectedTabIndex].Content;
            }
        }
        private void UpdateTabStyles()
        {
            for (int i = 0; i < _tabStrip.Children.Count; i++)
            {
                if (_tabStrip.Children[i] is Button button)
                {
                    button.BackgroundColor = i == SelectedTabIndex ? Colors.Blue : Colors.Gray;
                }
            }
        }
    }

    public class TabItem
    {
        public string Title { get; set; }
        public View Content { get; set; }
    }
}
