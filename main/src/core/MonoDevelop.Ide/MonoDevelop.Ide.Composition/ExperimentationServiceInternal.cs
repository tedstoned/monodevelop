using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Utilities;

namespace MonoDevelop.Ide.Composition
{
	[Export (typeof (IExperimentationServiceInternal))]
	internal class ExperimentationServiceInternal : IExperimentationServiceInternal
	{
		public bool IsCachedFlightEnabled (string flightName) => false;
	}
}
