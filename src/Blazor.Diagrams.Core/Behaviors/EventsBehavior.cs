using Blazor.Diagrams.Core.Models.Base;

namespace Blazor.Diagrams.Core.Behaviors
{
    public class EventsBehavior : Behavior
    {
        private bool _captureMouseMove;
        private int _mouseMovedCount;

        public EventsBehavior(Diagram diagram) : base(diagram)
        {
            Diagram.MouseDown += OnMouseDown;
            Diagram.MouseMove += OnMouseMove;
            Diagram.MouseUp += OnMouseUp;
        }

        private void OnMouseDown(Model model, MouseEventArgs e)
        {
            _captureMouseMove = true;
        }

        private void OnMouseMove(Model model, MouseEventArgs e)
        {
            if (!_captureMouseMove)
                return;

            _mouseMovedCount++;
        }

        private void OnMouseUp(Model model, MouseEventArgs e)
        {
            _captureMouseMove = false;
            if (_mouseMovedCount > 0)
            {
                _mouseMovedCount = 0;
                return;
            }

            Diagram.OnMouseClick(model, e);
        }

        public override void Dispose()
        {
            Diagram.MouseDown -= OnMouseDown;
            Diagram.MouseMove -= OnMouseMove;
            Diagram.MouseUp -= OnMouseUp;
        }
    }
}
