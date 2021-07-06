using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
namespace JCEx
{
    public class Demo2 : MonoBehaviour
    {
        private Rect mLabelRect;
        private bool mIsMove;
        private Vector2 mMousePos;
        private Rect mLabelLastRect;
        private bool mOpenOrClose;
        void Start()
        {
            mLabelRect.width = Screen.width * 0.07f;
            mLabelRect.height = mLabelRect.width * 0.5f;
            mLabelRect.x = Screen.width * 0.5f - mLabelRect.width * 0.5f;
            mLabelRect.y = 0;
            mLabelLastRect = mLabelRect;
        }


        private bool IsInRect(Rect rect, Vector2 pos)
        {
            return pos.x >= rect.x && pos.x <= rect.x + rect.width && pos.y >= rect.y && pos.y <= rect.y + rect.height;
        }


        private Vector2 MousePos
        {
            get
            {
                mMousePos = Input.mousePosition;
                mMousePos.y = Screen.height - mMousePos.y;
                return mMousePos;
            }
        }


        private bool IsMoved
        {
            get
            {
                return Vector2.Distance(new Vector2(mLabelRect.x, mLabelRect.y), new Vector2(mLabelLastRect.x, mLabelLastRect.y)) > 10f;
            }
        }


        void Update()
        {
            CalcFps();

            if (Input.GetMouseButton(0))
            {
                if(!mIsMove)
                    mLabelLastRect = mLabelRect;

                if (IsInRect(mLabelRect, MousePos))
                {
                    mIsMove = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (!IsMoved && IsInRect(mLabelRect, MousePos))
                {
                    mOpenOrClose = !mOpenOrClose;
                    if (mOpenOrClose)
                    {
                        DebugExInit.Execute();
                        DebugEx.OpenWindow();
                    }
                    else
                        DebugEx.CloseWindow();
                }
                mIsMove = false;
            }

            if (mIsMove)
            {
                var pos = MousePos;
                pos.x = pos.x - mLabelRect.width * 0.5f;
                mLabelRect.x = Mathf.Max(0, pos.x);
                mLabelRect.x = Mathf.Min(Screen.width - mLabelRect.width, mLabelRect.x);

                pos.y = pos.y - mLabelRect.height * 0.5f;
                mLabelRect.y = Mathf.Max(0, pos.y);
                mLabelRect.y = Mathf.Min(Screen.height - mLabelRect.height, mLabelRect.y);
            }

            
        }

        private double mFrames = 0;
        private string mStringFPS;
        private double mLastInterval;
        private double mFps;
        private float mUpdateInterval = 0.5f;
        private void CalcFps()
        {
            ++mFrames;
            float timeNow = Time.realtimeSinceStartup;
            if (timeNow > mLastInterval + mUpdateInterval)
            {
                mFps = mFrames / (timeNow - mLastInterval);
                mFrames = 0;
                mLastInterval = timeNow;
                mStringFPS = string.Format("{0}Fps", mFps.ToString("N2"));
            }
        }


        void OnGUI()
        {
            GUI.skin.button.fontSize = Mathf.CeilToInt(mLabelRect.height * 0.4f);
            GUI.Label(mLabelRect, mStringFPS, GUI.skin.button);
        }




    }
}