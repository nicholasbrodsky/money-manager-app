﻿namespace MoneyManagerApp.ViewModels
{
	public class MainViewModel : BaseViewModel
    {
		private string name = "Nicholas";
		public string Name
		{
			get { return name; }
			set { name = value; OnPropertyChanged(); }
		}
		public MainViewModel()
		{

		}
    }
}
