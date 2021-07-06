using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JCEx
{ 
    public class DebugWindow : MonoBehaviour
    {
        private Rect mRectDebugWin;
        private Vector2 mSpDebugWin;

        private Rect mRectButtonWin;
        private Vector2 mSpButtonWin;

        private Rect mRectRTWin;
        private Vector2 mSpRTWin;

        private Rect mRectBigRTWin;
        private Vector2 mSpBigRTWin;

        private Rect mRectLogWin;
        private Vector2 mSpLogWin;
        private Vector2 mLogTextAreaSize;

        private bool mIsOpenDebugWindow;
        private bool mIsOpenRTWindw;
        private bool mIsOpenLogWindow;
        private bool mIsOpenButtonWindow;

        private string mBigRTName;
        private long mLogTextLength;
        private static System.Text.StringBuilder mText = new System.Text.StringBuilder();

        void Start()
        {
            var min = Mathf.Min(Screen.width, Screen.height);
            //DebugWindow
            mRectDebugWin.width = min * 0.4f;
            mRectDebugWin.height = mRectDebugWin.width;
            mRectDebugWin.x = Screen.width * 0.05f;
            mRectDebugWin.y = Screen.height * 0.05f;

            //ButtonWindow
            mRectButtonWin.width = min * 0.4f;
            mRectButtonWin.height = mRectButtonWin.width;
            mRectButtonWin.x = Screen.width * 0.05f;
            mRectButtonWin.y = Screen.height * 0.95f - mRectButtonWin.height;

            //RTWindow
            mRectRTWin.width = min * 0.5f;
            mRectRTWin.height = min * 0.8f;
            mRectRTWin.x = Screen.width * 0.95f - mRectRTWin.width;
            mRectRTWin.y = Screen.height * 0.95f - mRectRTWin.height;

            //BigRTWindow
            mRectBigRTWin = new Rect(0, 0, Screen.width * 0.9f, Screen.height * 0.9f);
            mRectBigRTWin.width = Screen.width * 0.9f;
            mRectBigRTWin.height = Screen.height * 0.9f;
            mRectBigRTWin.x = (Screen.width - mRectBigRTWin.width) * 0.5f;
            mRectBigRTWin.y = (Screen.height - mRectBigRTWin.height) * 0.5f;

            //LogWindow
            mRectLogWin.width = min * 0.5f;
            mRectLogWin.height = min * 0.8f;
            mRectLogWin.x = Screen.width * 0.6f - mRectRTWin.width;
            mRectLogWin.y = Screen.height * 0.95f - mRectRTWin.height;

            mIsOpenDebugWindow = true;
        }

        void OnGUI()
        {
            if(mIsOpenDebugWindow)
                mRectDebugWin = GUI.Window(0, mRectDebugWin, DrawDebugWindow, "");

            if (mIsOpenButtonWindow)
                mRectButtonWin = GUI.Window(1, mRectButtonWin, DrawButtonWindow, "");

            if(mIsOpenRTWindw)
                mRectRTWin = GUI.Window(2, mRectRTWin, DrawRTWindow, "");

            if (mIsOpenLogWindow)
                mRectLogWin = GUI.Window(3, mRectLogWin, DrawLogWindow, "");

            if (!string.IsNullOrEmpty(mBigRTName))
                mRectBigRTWin = GUI.Window(100, mRectBigRTWin, DrawBigRTWindow, "");

        }

        private void DrawDebugWindow(int windowID)
        {
            var titleHeight = mRectDebugWin.height * 0.2f;
            var cellWidth = mRectDebugWin.width * 0.99f;
            var cellHeight = mRectDebugWin.height * 0.25f;

            GUI.skin.label.fontSize = Mathf.CeilToInt(titleHeight * 0.45f);
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(mRectDebugWin.width * 0.5f - cellWidth * 0.5f, 0, cellWidth, titleHeight), "<Debug Window>");

            var contentRect = new Rect((mRectDebugWin.width - cellWidth) * 0.5f, titleHeight, cellWidth, (mRectDebugWin.height - titleHeight) * 0.99f);
            GUILayout.BeginArea(contentRect);

            mSpDebugWin = GUI.BeginScrollView(new Rect(0, 0, contentRect.width, contentRect.height), mSpDebugWin, new Rect(0, 0, contentRect.width * 0.9f, cellHeight  * 4), false, false);
        
            GUI.skin.button.fontSize = Mathf.CeilToInt(cellHeight * 0.3f);
            var title = mIsOpenRTWindw ? "Close RT Window" : "Open RT Window";
            if (GUI.Button(new Rect(0, 0, cellWidth, cellHeight), title))
            {
                mIsOpenRTWindw = !mIsOpenRTWindw;
            }

            title = mIsOpenLogWindow ? "Close Log Window" : "Open Log Window";
            if (GUI.Button(new Rect(0, cellHeight, cellWidth, cellHeight), title))
            {
                mIsOpenLogWindow = !mIsOpenLogWindow;
            }

            title = mIsOpenButtonWindow ? "Close Button Window" : "Open Button Window";
            if (GUI.Button(new Rect(0, cellHeight * 2, cellWidth, cellHeight), title))
            {
                mIsOpenButtonWindow = !mIsOpenButtonWindow;
            }

            if (GUI.Button(new Rect(0, cellHeight * 3, cellWidth, cellHeight), "Close All Debug Windows"))
            {
                DebugEx.CloseWindow();
            }
            GUI.EndScrollView();
            GUILayout.EndArea();

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }

        private void DrawButtonWindow(int windowID)
        {
            var titleHeight = mRectButtonWin.height * 0.2f;
            var cellWidth = mRectDebugWin.width * 0.99f;
            var cellHeight = mRectButtonWin.height * 0.25f;

            GUI.skin.label.fontSize = Mathf.CeilToInt(titleHeight * 0.45f);
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(mRectDebugWin.width * 0.5f - cellWidth * 0.5f, 0, cellWidth, titleHeight), "<Button Window>");

            var contentRect = new Rect((mRectDebugWin.width - cellWidth) * 0.5f, titleHeight, cellWidth, (mRectDebugWin.height - titleHeight) * 0.99f);
            GUILayout.BeginArea(contentRect);

            mSpButtonWin = GUI.BeginScrollView(new Rect(0, 0, contentRect.width, contentRect.height), mSpButtonWin, new Rect(0, 0, contentRect.width * 0.9f, cellHeight * mBtnDatas.Count), false, false);

            GUI.skin.button.fontSize = Mathf.CeilToInt(cellHeight * 0.3f);
            for (int i = 0; i < mBtnDatas.Count; i++)
            {
                if (GUI.Button(new Rect(0, cellHeight * i, cellWidth, cellHeight), mBtnDatas[i].Title))
                {
                    mBtnDatas[i].Click();
                }
            }
            GUI.EndScrollView();
            GUILayout.EndArea();

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }

        private void DrawRTWindow(int windowID)
        {
            var titleHeight = mRectRTWin.height * 0.1f;
            var cellWidth = mRectRTWin.width * 0.99f;
            var cellHeight = cellWidth;

            GUI.skin.label.fontSize = Mathf.CeilToInt(titleHeight * 0.45f);
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(mRectRTWin.width * 0.5f - cellWidth * 0.5f, 0, cellWidth, titleHeight), "<RT Window>");


            var contentRect = new Rect((mRectRTWin.width - cellWidth) * 0.5f, titleHeight, cellWidth, (mRectRTWin.height - titleHeight) * 0.99f);
            GUILayout.BeginArea(contentRect);

            mSpRTWin = GUI.BeginScrollView(new Rect(0, 0, contentRect.width, contentRect.height), mSpRTWin, new Rect(0, 0, contentRect.width * 0.9f, cellHeight * mRTDatas.Count), false, false);
            int index = 0;
            foreach (var rtData in mRTDatas.Values)
            {
                if (GUI.Button(new Rect(0, cellHeight * index, cellWidth, cellHeight), rtData.RT))
                {
                    mIsOpenDebugWindow = false;
                    mIsOpenRTWindw = false;
                    mBigRTName = rtData.Name;
                }
                index++;
                GUI.skin.box.fontSize = Mathf.CeilToInt(cellHeight * 0.1f * 0.5f);
                GUI.Label(new Rect(0, cellHeight * index - cellHeight * 0.1f, cellWidth, cellHeight * 0.1f), rtData.Name, GUI.skin.box);
            }

            GUI.EndScrollView();

            GUILayout.EndArea();
            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));

        }

        private void DrawBigRTWindow(int windowId)
        {
            DebugRTData rtData = null;
            if (mRTDatas.TryGetValue(mBigRTName, out rtData))
            {
                var titleHeight = 0;

                var showRect = new Rect();
                showRect.width = mRectBigRTWin.width * 0.99f;
                showRect.height = (mRectBigRTWin.height - titleHeight) * 0.99f;
                showRect.x = (mRectBigRTWin.width - showRect.width) * 0.5f;
                showRect.y = (mRectBigRTWin.height - showRect.height) * 0.99f;

                var imgRect = new Rect(showRect.x, showRect.y, rtData.RT.width, rtData.RT.height);
                mSpBigRTWin = GUI.BeginScrollView(showRect, mSpBigRTWin, imgRect, false, false);

                if (GUI.Button(imgRect, rtData.RT))
                {
                    mBigRTName = null;
                    mIsOpenDebugWindow = true;
                    mIsOpenRTWindw = true;
                }
            
                GUI.EndScrollView();

                var nameRect = new Rect(showRect.x, showRect.y, showRect.width, showRect.height * 0.1f);
                GUI.skin.label.fontSize = Mathf.CeilToInt(nameRect.height * 0.5f);
                GUI.Label(nameRect, rtData.Name);

            
            }

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }

        private void DrawLogWindow(int windowId)
        {
            var titleHeight = mRectRTWin.height * 0.1f;
            var cellWidth = mRectRTWin.width * 0.99f;

            GUI.skin.label.fontSize = Mathf.CeilToInt(titleHeight * 0.45f);
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(mRectRTWin.width * 0.5f - cellWidth * 0.5f, 0, cellWidth, titleHeight), "<Log Window>");

            var contentRect = new Rect((mRectRTWin.width - cellWidth) * 0.5f, titleHeight, cellWidth, (mRectRTWin.height - titleHeight) * 0.99f);
            GUILayout.BeginArea(contentRect);

            mSpLogWin = GUILayout.BeginScrollView(mSpLogWin, GUILayout.Width(contentRect.width));
            
            GUI.skin.textArea.fontSize = Mathf.CeilToInt(contentRect.width * 0.08f);
            var text = mText.ToString();
            if(mText.Length > 0)
                GUILayout.TextArea(text, GUILayout.Width(contentRect.width * 0.96f));
            if (text.Length != mLogTextLength)
            {
                mSpLogWin.y = GUI.skin.textArea.CalcHeight(new GUIContent(text), contentRect.width * 0.96f);
            }
            mLogTextLength = text.Length;

            GUILayout.EndScrollView();

            GUILayout.EndArea();
            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }


        public class DebugButtonData
        {
            public string Title { private set; get; }
            private System.Action mOnClick;
            public DebugButtonData(string title, System.Action onClick)
            {
                Title = title;
                mOnClick = onClick;
            }
            public void Click()
            {
                if (mOnClick != null)
                    mOnClick();
            }
        }
        private static List<DebugButtonData> mBtnDatas = new List<DebugButtonData>();

        public class DebugRTData
        {
            public Texture RT { get; set; }
            public string Name { get; set; }
            public DebugRTData(string name, Texture rt)
            {
                Name = name;
                RT = rt;
            }
        }

        private static Dictionary<string, DebugRTData> mRTDatas = new Dictionary<string, DebugRTData>();

        public static void AddButton(string btnName,System.Action action)
        {
            mBtnDatas.Add(new DebugButtonData(btnName, action));
        }

        public static void ShowTexture(string texName,Texture texture)
        {
            DebugRTData rt = null;
            if (mRTDatas.TryGetValue(texName, out rt))
                rt.RT = texture;
            else
                mRTDatas.Add(texName, new DebugRTData(texName, texture));

        }

        public static bool UnShowTexture(string texName)
        {
            return mRTDatas.Remove(texName);
        }

        public static void Log(object message)
        {
            if (mText.Length == 0)
                mText.Append(message);
            else
                mText.Append(string.Format("\n{0}", message));
        }

        public static void LogFormat(string format,params object[] args)
        {
            try
            {
                Log(string.Format(format, args));
            }
            catch (System.Exception e)
            {
                Log(e.ToString());
            }
        }


    

    }
}