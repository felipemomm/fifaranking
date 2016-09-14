using System;
using System.Collections;
using System.Windows.Input;

using Xamarin.Forms;

using Firebase.Xamarin.Database;

namespace FifaRanking
{
	public class CustomPicker : Picker
	{
		public static BindableProperty ItemsSourceProperty = BindableProperty.Create<CustomPicker, IEnumerable>(x => x.ItemsSource, default(IEnumerable), BindingMode.OneWay, null, OnItemsSourceChanged);

		public static BindableProperty SelectedItemProperty = BindableProperty.Create<CustomPicker, object>(x => x.SelectedItem, default(object), BindingMode.TwoWay, null, OnSelectedItemChanged);

		public static readonly BindableProperty ItemSelectedCommandProperty = BindableProperty.Create("ItemSelectedCommand", typeof(ICommand), typeof(CustomPicker), null, BindingMode.OneWay); 

		public IList ItemsSource
		{
			get { return (IList)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public object SelectedItem
		{
			get { return GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		public ICommand ItemSelectedCommand
		{
			get
			{ 
				return (ICommand)base.GetValue(ItemSelectedCommandProperty);
			}
			set
			{ 
				base.SetValue(ItemSelectedCommandProperty, value);
			}
		}

		public CustomPicker ()
		{
			SelectedIndexChanged += OnSelectedIndexChanged;
		}

		private static void OnItemsSourceChanged(BindableObject bindable, IEnumerable oldvalue, IEnumerable newvalue)
		{
			var picker = bindable as CustomPicker;
			picker.Items.Clear();
			if (newvalue != null)
			{
				foreach (var item in newvalue)
				{
					string text = GetKey(item);
					picker.Items.Add(text);
				}
			}
		}

		private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var picker = bindable as CustomPicker;

			if (newValue != null)
			{
				string text = GetKey(newValue);
				picker.SelectedIndex = picker.Items.IndexOf(text);
			}
		}

		private static string GetKey(object item)
		{
			string text;

			var firebaseObject = item as FirebaseObject<Player>;
			if (firebaseObject != null)
			{
				text = firebaseObject.Object.Name;
			}
			else
			{
				text = item.ToString();
			}

			return text;
		}

		private void OnSelectedIndexChanged(object sender, EventArgs ev)
		{
			if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
			{
				SelectedItem = null;
			}
			else
			{
				SelectedItem = ItemsSource[SelectedIndex];
				ICommand command = ItemSelectedCommand;
				if (command != null) 
				{
					command.Execute(SelectedItem);
				}
			}
		}
	}
}