﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sw = System.Windows;
using swc = System.Windows.Controls;
using mwc = Xceed.Wpf.Toolkit;
using Eto.Forms;

namespace Eto.Wpf.Forms.Controls
{
	public class TextStepperHandler : TextBoxHandler<mwc.ButtonSpinner, TextStepper, TextStepper.ICallback>, TextStepper.IHandler
	{
		public TextStepperHandler()
		{
			Control = new mwc.ButtonSpinner
			{
				Content = new mwc.WatermarkTextBox
				{
					BorderThickness = new sw.Thickness(0),
					BorderBrush = null,
					Padding = new sw.Thickness(0)
				}
			};
		}

		public override string PlaceholderText
		{
			get { return WatermarkTextBox.Watermark as string; }
			set { WatermarkTextBox.Watermark = value; }
		}

		public StepperValidDirections ValidDirection
		{
			get { return Control.ValidSpinDirection.ToEto(); }
			set { Control.ValidSpinDirection = value.ToWpf(); }
		}

		mwc.WatermarkTextBox WatermarkTextBox => (mwc.WatermarkTextBox)Control.Content;

		protected override swc.TextBox TextBox => (swc.TextBox)Control.Content;

		public override void AttachEvent(string id)
		{
			switch (id)
			{
				case TextStepper.StepEvent:
					Control.Spin += (sender, e) => Callback.OnStep(Widget, new StepperEventArgs(e.Direction == Xceed.Wpf.Toolkit.SpinDirection.Increase ? StepperDirection.Up : StepperDirection.Down));
					break;
				default:
					base.AttachEvent(id);
					break;
			}
		}
	}
}
