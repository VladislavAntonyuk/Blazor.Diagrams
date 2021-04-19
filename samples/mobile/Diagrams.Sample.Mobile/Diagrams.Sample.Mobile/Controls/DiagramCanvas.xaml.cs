using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazor.Diagrams.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Geometry;
using System;
using System.Linq;
using System.Threading.Tasks;
using Rectangle = Blazor.Diagrams.Core.Geometry.Rectangle;

namespace Diagrams.Sample.Mobile.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DiagramCanvas : ContentView, IDisposable
	{
		public DiagramCanvas()
		{
			InitializeComponent();
        }

        public Diagram Diagram { get; set; }
        
        private string LayerStyle
            => FormattableString.Invariant($"transform: translate({Diagram.Pan.X}px, {Diagram.Pan.Y}px) scale({Diagram.Zoom});");


        public void OnResize(Rectangle rect) => Diagram.SetContainer(rect);

        private void OnMouseDown(MouseEventArgs e) => Diagram.OnMouseDown(null, new Blazor.Diagrams.Core.MouseEventArgs()
        {
            CtrlKey = e.CtrlKey,
            ShiftKey = e.ShiftKey,
            Button = e.Button,
            ClientX = e.ClientX,
            ClientY = e.ClientY
        });

        private void OnMouseMove(MouseEventArgs e) => Diagram.OnMouseMove(null, new MouseEventArgs()
        {
            CtrlKey = e.CtrlKey,
            ShiftKey = e.ShiftKey,
            Button = e.Button,
            ClientX = e.ClientX,
            ClientY = e.ClientY
        });

        private void OnMouseUp(MouseEventArgs e) => Diagram.OnMouseUp(null, new MouseEventArgs()
        {
            CtrlKey = e.CtrlKey,
            ShiftKey = e.ShiftKey,
            Button = e.Button,
            ClientX = e.ClientX,
            ClientY = e.ClientY
        });

        private void OnKeyDown(KeyboardEventArgs e) => Diagram.OnKeyDown(new KeyboardEventArgs()
        {
            AltKey = e.AltKey,
            Code = e.Code,
            CtrlKey = e.CtrlKey,
            Key = e.Key,
            ShiftKey = e.ShiftKey
        });

        private void OnWheel(WheelEventArgs e) => Diagram.OnWheel(new WheelEventArgs() { ClientX = e.ClientX, ClientY = e.ClientY, DeltaY = e.DeltaY });

        private void OnTouchStart(TouchEventArgs e) => Diagram.OnTouchStart(null, new TouchEventArgs()
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

        private void OnTouchMove(TouchEventArgs e) => Diagram.OnTouchMove(null, new TouchEventArgs()
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

        private void OnTouchEnd(TouchEventArgs e) => Diagram.OnTouchEnd(null, new TouchEventArgs()
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

        private void OnDiagramChanged()
        {
        }

        public void Dispose()
        {
            Diagram.Changed -= OnDiagramChanged;
        }
	}
}