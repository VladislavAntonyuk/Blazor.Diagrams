﻿using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Threading.Tasks;
using KeyboardEventArgs = Microsoft.AspNetCore.Components.Web.KeyboardEventArgs;
using MouseEventArgs = Microsoft.AspNetCore.Components.Web.MouseEventArgs;
using TouchEventArgs = Microsoft.AspNetCore.Components.Web.TouchEventArgs;
using TouchPoint = Blazor.Diagrams.Core.TouchPoint;
using WheelEventArgs = Microsoft.AspNetCore.Components.Web.WheelEventArgs;

namespace Blazor.Diagrams.Components
{
    public partial class DiagramCanvas : IDisposable
    {
        [CascadingParameter]
        public Diagram Diagram { get; set; }

        [Parameter]
        public RenderFragment Widgets { get; set; }

        [Parameter]
        public string Class { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected ElementReference elementReference;
        private DotNetObjectReference<DiagramCanvas> _reference;
        private bool _shouldReRender;

        private string LayerStyle
            => FormattableString.Invariant($"transform: translate({Diagram.Pan.X}px, {Diagram.Pan.Y}px) scale({Diagram.Zoom});");

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _reference = DotNetObjectReference.Create(this);
            Diagram.Changed += OnDiagramChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                Diagram.Container = await JSRuntime.GetBoundingClientRect(elementReference);
                await JSRuntime.ObserveResizes(elementReference, _reference);
            }
        }

        [JSInvokable]
        public void OnResize(Rectangle rect) => Diagram.SetContainer(rect);

        protected override bool ShouldRender()
        {
            if (_shouldReRender)
            {
                _shouldReRender = false;
                return true;
            }

            return false;
        }

        private void OnMouseDown(MouseEventArgs e) => Diagram.OnMouseDown(null, new Core.MouseEventArgs()
        {
	        CtrlKey = e.CtrlKey,
            ShiftKey = e.ShiftKey,
            Button = e.Button,
            ClientX = e.ClientX,
            ClientY = e.ClientY
        });

        private void OnMouseMove(MouseEventArgs e) => Diagram.OnMouseMove(null, new Core.MouseEventArgs()
        {
	        CtrlKey = e.CtrlKey,
	        ShiftKey = e.ShiftKey,
	        Button = e.Button,
	        ClientX = e.ClientX,
	        ClientY = e.ClientY
        });

        private void OnMouseUp(MouseEventArgs e) => Diagram.OnMouseUp(null, new Core.MouseEventArgs()
        {
	        CtrlKey = e.CtrlKey,
	        ShiftKey = e.ShiftKey,
	        Button = e.Button,
	        ClientX = e.ClientX,
	        ClientY = e.ClientY
        });

        private void OnKeyDown(KeyboardEventArgs e) => Diagram.OnKeyDown(new Core.KeyboardEventArgs(){AltKey = e.AltKey,
	        Code = e.Code, CtrlKey = e.CtrlKey, Key = e.Key, ShiftKey = e.ShiftKey});

        private void OnWheel(WheelEventArgs e) => Diagram.OnWheel(new Core.WheelEventArgs(){ClientX = e.ClientX, ClientY = e.ClientY, DeltaY = e.DeltaY});

        private void OnTouchStart(TouchEventArgs e) => Diagram.OnTouchStart(null, new Core.TouchEventArgs()
        {
	        CtrlKey = e.CtrlKey, ShiftKey = e.ShiftKey,
            ChangedTouches = e.ChangedTouches.Select(x=>new TouchPoint()
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

        private void OnTouchMove(TouchEventArgs e) => Diagram.OnTouchMove(null, new Core.TouchEventArgs()
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

        private void OnTouchEnd(TouchEventArgs e) => Diagram.OnTouchEnd(null, new Core.TouchEventArgs()
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
            _shouldReRender = true;
            StateHasChanged();
        }

        public void Dispose()
        {
            Diagram.Changed -= OnDiagramChanged;

            if (_reference == null)
                return;

            if (elementReference.Id != null)
                _ = JSRuntime.UnobserveResizes(elementReference);

            _reference.Dispose();
        }
    }
}
