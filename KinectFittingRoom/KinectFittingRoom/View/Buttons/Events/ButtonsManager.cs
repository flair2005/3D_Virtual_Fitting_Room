﻿using System.Windows;

namespace KinectFittingRoom.View.Buttons.Events
{
    /// <summary>
    /// Manages Hand Cursor
    /// </summary>
    public class ButtonsManager
    {
        #region Variables
        /// <summary>
        /// Instance of the HandCursorManager
        /// </summary>
        private static ButtonsManager _instance;
        /// <summary>
        /// Prevents from reinitializing the singleton
        /// </summary>
        private static bool _isInitialized;
        /// <summary>
        /// Last element hit by cursor
        /// </summary>
        private IInputElement _lastElement;
        #endregion Variables
        #region Properties
        /// <summary>
        /// Hand cursor manager instance
        /// </summary>
        public static ButtonsManager Instance
        {
            get
            {
                if (!_isInitialized)
                    _instance = Initialize();
                return _instance;
            }
        }
        #endregion Properties
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonsManager"/> class.
        /// </summary>
        private static ButtonsManager Initialize()
        {
            _isInitialized = true;
            return new ButtonsManager();
        }
        #endregion .ctor
        #region Methods
        /// <summary>
        /// Raises the cursor events.
        /// </summary>
        /// <param name="element">The ui element under the cursor.</param>
        /// <param name="cursorPosition">Cursor position.</param>
        public void RaiseCursorEvents(IInputElement element, Point cursorPosition)
        {
            element.RaiseEvent(new HandCursorEventArgs(KinectEvents.HandCursorMoveEvent, cursorPosition));
            if (element != _lastElement)
            {
                if (_lastElement != null)
                    _lastElement.RaiseEvent(new HandCursorEventArgs(KinectEvents.HandCursorLeaveEvent, cursorPosition));
                element.RaiseEvent(new HandCursorEventArgs(KinectEvents.HandCursorEnterEvent, cursorPosition));
            }
            _lastElement = element;
        }
        /// <summary>
        /// Raises the cursor leave event.
        /// </summary>
        /// <param name="cursorPosition">The cursor position.</param>
        public void RaiseCursorLeaveEvent(Point cursorPosition)
        {
            if (_lastElement == null) return;
            _lastElement.RaiseEvent(new HandCursorEventArgs(KinectEvents.HandCursorLeaveEvent, cursorPosition));
            _lastElement = null;
        }
        #endregion Methods
    }
}
