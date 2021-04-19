using Diagrams.Sample.Mobile.ViewModels;
using System.ComponentModel;
using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Models;
using Xamarin.Forms;

namespace Diagrams.Sample.Mobile.Views
{
	public partial class ItemDetailPage : ContentPage
	{
		private Diagram diagram;
		public ItemDetailPage()
		{
			InitializeComponent();
			BindingContext = new ItemDetailViewModel();
			diagram = new Diagram(new DiagramOptions());
			diagram.Nodes.Add(new NodeModel());
			diagram.Nodes.Add(new NodeModel());
			diagram.Nodes.Add(new NodeModel());
		}
	}
}