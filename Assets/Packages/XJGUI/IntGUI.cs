﻿namespace XJGUI
{
    public class IntGUI : NumericGUI<int>
    {
        #region Property

        protected override bool TextIsCorrect
        {
            get
            {
                int value;

                if (!int.TryParse(this.text, out value))
                {
                    return false;
                }

                if (value < base.MinValue || base.MaxValue < value)
                {
                    return false;
                }

                return true;
            }
        }

        #endregion Property

        #region Constructor

        public IntGUI() : base() { }

        public IntGUI(string title) : base(title) { }
 
        public IntGUI(string title, int value) : base(title, value) { }

        public IntGUI(string title, int value, int min, int max) : base(title, value, min, max) { }

        #endregion Constructor

        #region Method

        protected override void Initialize()
        {
            base.Initialize();
            base.MinValue = XJGUILayout.DefaultMinValueInt;
            base.MaxValue = XJGUILayout.DefaultMaxValueInt;
            base.Value    = XJGUILayout.DefaultValueInt;
        }

        #endregion Method
    }
}