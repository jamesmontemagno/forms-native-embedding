using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Graphics.Drawables;
using Android.Content.Res;
using Android.Animation;
using Android.Views.Animations;
using DrawableCompat = Android.Support.V4.Graphics.Drawable.DrawableCompat;

namespace NativeTest.Droid
{
    public class CheckableFab : FloatingActionButton, ICheckable
    {
        static readonly int[] CheckedStateSet = {
            Android.Resource.Attribute.StateChecked
        };
        bool chked;
        Drawable.ConstantState imgState;

        public CheckableFab(Context context) :
            base(context)
        {
            Initialize(context, null);

        }

        public CheckableFab(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize(context, attrs);
        }

        public CheckableFab(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize(context, attrs);
        }

        public CheckableFab(IntPtr handle, JniHandleOwnership own)
            : base(handle, own)
        {
        }

        void Initialize(Context context, IAttributeSet attrs)
        {
            //imgState = Drawable.GetConstantState();
        }

        public override void SetImageResource(int resId)
        {
            base.SetImageResource(resId);
            imgState = Drawable.GetConstantState();
        }

        public override bool PerformClick()
        {
            Toggle();
            return base.PerformClick();
        }

        public void Toggle()
        {
            JumpDrawablesToCurrentState();
            /* AnimatedVectorDrawable unfortunately keep its VectorDrawable child in
             * the same state everytime it re-runs animations instead of reset-ing.
             * This means the animation is screwed up, so we simply reset the drawable
             * everytime with a new copy
             */
            SetImageDrawable(imgState.NewDrawable(Resources));
            Checked = !Checked;
        }

        public bool Checked
        {
            get {
                return chked;
            }
            set {
                if (chked == value)
                    return;
                chked = value;
                RefreshDrawableState();
            }
        }

        public override int[] OnCreateDrawableState(int extraSpace)
        {
            var space = extraSpace + (Checked ? CheckedStateSet.Length : 0);
            var drawableState = base.OnCreateDrawableState(space);
            if (Checked)
                MergeDrawableStates(drawableState, CheckedStateSet);
            return drawableState;
        }
    }
}