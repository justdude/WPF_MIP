﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using GalaSoft.MvvmLight;

namespace MIP.MVVM
{
	/// <summary>
	/// Provides common functionality for ViewModel classes
	/// </summary>
	public abstract class AdwancedViewModelBase : ViewModelBase
	{
		#region Fields

		private bool mvIsBusy;
		private bool mvIsLoading;
		private string mvToken;
		private bool mvIsEnabled;

		#endregion

		#region Properties

		public Dispatcher CurrentDispatcher { get; set; }

		public bool IsBusy
		{
			get
			{
				return mvIsBusy;
			}
			set
			{
				if (value == mvIsBusy)
					return;

				mvIsBusy = value;

				RaisePropertyChanged<bool>(() => IsBusy);
			}
		}

		private string statusText;

		public virtual string StatusText
		{
			get { return statusText; }
			set
			{
				if (value == statusText)
					return;

				statusText = value;

				RaisePropertyChanged<string>(() => StatusText);
			}
		}

		public virtual bool IsLoading
		{
			get
			{
				return mvIsLoading;
			}
			set
			{
				if (value == mvIsLoading)
					return;

				mvIsLoading = value;

				RaisePropertyChanged<bool>(() => IsLoading);
			}
		}

		public virtual bool IsEnabled
		{
			get
			{
				return mvIsEnabled;
			}
			set
			{
				if (value == mvIsEnabled)
					return;

				mvIsEnabled = value;

				RaisePropertyChanged<bool>(() => IsEnabled);
			}
		}


		public virtual string Token
		{
			get
			{
				return mvToken;
			}
			set
			{
				if (mvToken == value)
					return;

				mvToken = value;

				OnTokenChanged();

				RaisePropertyChanged<string>(() => Token);
			}

		}

		public string ParentToken { get; set; }

		#endregion

		#region .Ctr

		public AdwancedViewModelBase()
		{
			CurrentDispatcher = Application.Current.Dispatcher;
		}
		#endregion

		#region Helpers methods
		public void Execute(Action action)
		{
			if (action == null)
				return;

			CurrentDispatcher.Invoke(action);
		}

		public void BeginExecute(Action action)
		{
			if (CurrentDispatcher == null || action == null)
				return;

			CurrentDispatcher.BeginInvoke(action);
		}

		public void BeginExecute(DispatcherPriority priority, Action action)
		{
			if (CurrentDispatcher == null || action == null)
				return;

			CurrentDispatcher.BeginInvoke(action, priority);
		}

		//protected void OnPropertyChanged(string propertyName)
		//{
		//	PropertyChangedEventHandler handler = PropertyChanged;

		//	if (handler != null)
		//	{
		//		handler(this, new PropertyChangedEventArgs(propertyName));
		//	}
		//}

		#endregion

		#region Public methods

		public void Clean()
		{
			OnCleanup();
		}

		#endregion

		#region Methods virtual
		protected virtual void OnTokenChanged()
		{
			
		}

		public virtual void RefreshCommands()
		{ }

		protected virtual void RefreshPrivate()
		{
		}

		protected virtual void OnCleanup()
		{
			Cleanup();
		}

		public virtual void Refresh()
		{
			RefreshPrivate();
		}

		#endregion

		#region Events

		//public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region IReceiver Members


		#endregion

	}
}
