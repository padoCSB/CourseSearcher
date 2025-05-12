using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSearcher
{
    public delegate void MouseEvent(Point point);
    public delegate void MouseMoveEvent(Point point, bool isPressed = false);
    public class MouseHandler : IMessageFilter
    {
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_MOUSEDOWN = 0x0201;
        private const int WM_MOUSEEUP = 0x0202;

        public event MouseMoveEvent? MouseMove;
        public event MouseEvent? MouseDown;
        public event MouseEvent? MouseUp;

        #region IMessageFilter Members
        private bool isPressed = false;

        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_MOUSEMOVE:
                    MouseMove?.Invoke(Cursor.Position, isPressed);
                    break;
                case WM_MOUSEDOWN:
                    isPressed = true;
                    MouseDown?.Invoke(Cursor.Position);
                    break;
                case WM_MOUSEEUP:
                    isPressed = false;
                    MouseUp?.Invoke(Cursor.Position);
                    break;
            }
            // Always allow message to continue to the next filter control
            return false;
        }

        #endregion
    }
}
