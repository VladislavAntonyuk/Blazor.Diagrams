using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Linq;
using MouseEventArgs = Microsoft.AspNetCore.Components.Web.MouseEventArgs;
using TouchEventArgs = Microsoft.AspNetCore.Components.Web.TouchEventArgs;
using TouchPoint = Blazor.Diagrams.Core.TouchPoint;

namespace Blazor.Diagrams.Components
{
    public partial class LinkVertexWidget : IDisposable
    {
        private bool _shouldRender = true;

        [CascadingParameter] public Diagram Diagram { get; set; }
        [Parameter] public LinkVertexModel Vertex { get; set; }
        [Parameter] public string Color { get; set; }
        [Parameter] public string SelectedColor { get; set; }

        private string ColorToUse => Vertex.Selected ? SelectedColor : Color;

        public void Dispose()
        {
            Vertex.Changed -= OnVertexChanged;
        }

        protected override void OnInitialized()
        {
            Vertex.Changed += OnVertexChanged;
        }

        protected override bool ShouldRender()
        {
            if (_shouldRender)
            {
                _shouldRender = false;
                return true;
            }

            return false;
        }

        private void OnVertexChanged()
        {
            _shouldRender = true;
            StateHasChanged();
        }

        private void OnMouseDown(MouseEventArgs e) => Diagram.OnMouseDown(Vertex, new Core.MouseEventArgs()
        {
	        CtrlKey = e.CtrlKey,
	        ClientX = e.ClientX,
	        ClientY = e.ClientY,
	        ShiftKey = e.ShiftKey,
	        Button = e.Button
        });

        private void OnMouseUp(MouseEventArgs e) => Diagram.OnMouseUp(Vertex, new Core.MouseEventArgs()
        {
	        CtrlKey = e.CtrlKey,
	        ClientX = e.ClientX,
	        ClientY = e.ClientY,
	        ShiftKey = e.ShiftKey,
	        Button = e.Button
        });

        private void OnTouchStart(TouchEventArgs e) => Diagram.OnTouchStart(Vertex, new Core.TouchEventArgs()
        {
	        CtrlKey = e.CtrlKey,
	        ShiftKey = e.ShiftKey,
	        ChangedTouches = e.ChangedTouches.Select(x => new TouchPoint()
	        {
		        ClientY = x.ClientY,
		        ClientX = x.ClientX,
		        PageX = x.PageX,
		        PageY = x.PageY,
		        ScreenX = x.ScreenX,
		        ScreenY = x.ScreenY,
		        Identifier = x.Identifier
	        }).ToArray()
        });

        private void OnTouchEnd(TouchEventArgs e) => Diagram.OnTouchEnd(Vertex, new Core.TouchEventArgs()
        {
	        CtrlKey = e.CtrlKey,
	        ShiftKey = e.ShiftKey,
	        ChangedTouches = e.ChangedTouches.Select(x => new TouchPoint()
	        {
		        ClientY = x.ClientY,
		        ClientX = x.ClientX,
		        PageX = x.PageX,
		        PageY = x.PageY,
		        ScreenX = x.ScreenX,
		        ScreenY = x.ScreenY,
		        Identifier = x.Identifier
	        }).ToArray()
        });

        private void OnDoubleClick(MouseEventArgs e)
        {
            Vertex.Parent.Vertices.Remove(Vertex);
            Vertex.Parent.Refresh();
        }
    }
}
