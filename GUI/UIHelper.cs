using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EverAI
{
    public static class UIHelper
    {
        private static float
           x, y,
           width, height,
           margin,
           controlHeight,
           controlDist,
           nextControlY;

        public static void Begin(string text, float _x, float _y, float _width, float _height, float _margin, float _controlHeight, float _controlDist)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            margin = _margin;
            controlHeight = _controlHeight;
            controlDist = _controlDist;
            nextControlY = 20f;
            GUI.Box(new Rect(x, y, width, height), text);
        }

        private static Rect NextControlRect()
        {
            Rect r = new Rect(x + margin, nextControlY, width - margin * 2, controlHeight);
            nextControlY += controlHeight + controlDist;
            return r;
        }

        public static string MakeEnable(string text, bool state)
        {
            return string.Format("{0}{1}", text, state ? "ON" : "OFF");
        }

        public static bool Button(string text, bool state)
        {
            return Button(MakeEnable(text, state));
        }

        public static bool Button(string text)
        {
            return GUI.Button(NextControlRect(), text);
        }

        public static void Label(string text, float value, int decimals = 2)
        {
            Label(string.Format("{0}{1}", text, Math.Round(value, 2).ToString()));
        }

        public static void Label(string text)
        {
            GUI.Label(NextControlRect(), text);
        }

        public static float Slider(float val, float min, float max)
        {
            return GUI.HorizontalSlider(NextControlRect(), val, min, max);
        }

        public static Color black = new Color(0, 0, 0, 1);
        public static Color white = new Color(1, 1, 1, 1);
        public static void DrawOutline(Rect position, String text, GUIStyle style, Color outColor, Color inColor)
        {
            var backupStyle = style;
            style.normal.textColor = outColor;
            position.x--;
            GUI.Label(position, text, style);
            position.x += 2;
            GUI.Label(position, text, style);
            position.x--;
            position.y--;
            GUI.Label(position, text, style);
            position.y += 2;
            GUI.Label(position, text, style);
            position.y--;
            style.normal.textColor = inColor;
            GUI.Label(position, text, style);
            style = backupStyle;
        }

        public static void DrawOutline(Rect position, String text, GUIStyle style)
        {
            var backupStyle = style;
            style.normal.textColor = black;
            position.x--;
            GUI.Label(position, text, style);
            position.x += 2;
            GUI.Label(position, text, style);
            position.x--;
            position.y--;
            GUI.Label(position, text, style);
            position.y += 2;
            GUI.Label(position, text, style);
            position.y--;
            style.normal.textColor = white;
            GUI.Label(position, text, style);
            style = backupStyle;
        }

        public static void DrawOutline(Rect position, String text)
        {
            var backupStyle = GUI.skin.label;
            GUIStyle style = GUI.skin.label;
            style.normal.textColor = black;
            position.x--;
            GUI.Label(position, text, style);
            position.x += 2;
            GUI.Label(position, text, style);
            position.x--;
            position.y--;
            GUI.Label(position, text, style);
            position.y += 2;
            GUI.Label(position, text, style);
            position.y--;
            style.normal.textColor = white;
            GUI.Label(position, text, style);
            style = backupStyle;
        }

        public static void RectFilled(float x, float y, float width, float height, Texture2D text)
        {
            GUI.DrawTexture(new Rect(x, y, width, height), text);
        }

        public static void RectOutlined(float x, float y, float width, float height, Texture2D text, float thickness = 1f)
        {
            RectFilled(x, y, thickness, height, text);
            RectFilled(x + width - thickness, y, thickness, height, text);
            RectFilled(x + thickness, y, width - thickness * 2f, thickness, text);
            RectFilled(x + thickness, y + height - thickness, width - thickness * 2f, thickness, text);
        }

        public static void Box(float x, float y, float width, float height, Texture2D text, float thickness = 1f)
        {
            RectOutlined(x - width / 2f, y - height, width, height, text, thickness);
        }


        static public Texture2D RedTextureStatic = new Texture2D(2, 2, TextureFormat.ARGB32, false);
        public static void Box(float x, float y, float width, float height, float thickness = 1f)
        {
            RectOutlined(x - width / 2f, y - height, width, height, RedTextureStatic, thickness);
        }
    }
}
