﻿using ColorWanted.screenshot.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ColorWanted.screenshot
{
    /// <summary>
    /// 扩展的 Canvas 组件
    /// </summary>
    public class ExtendedCanvas : Canvas
    {
        public Stack<DrawHistoryItem> History { get; private set; }

        private bool IsMouseDown;

        public bool EditEnabled { get; set; }

        /// <summary>
        /// 是否仅用于创建选区
        /// </summary>
        public bool MakeSelectionOnly { get; set; }

        public DrawShapes DrawShape { get; set; }
        public Color DrawColor { get; set; }

        public LineStyles LineStyle { get; set; }
        public int DrawWidth { get; set; }
        public System.Drawing.Font TextFont { get; set; }

        /// <summary>
        /// 当前的绘制
        /// </summary>
        private DrawRecord current;

        /// <summary>
        /// 移动
        /// </summary>
        private bool MoveMode;

        private Point MouseDownPoint;

        /// <summary>
        /// 绘图事件
        /// </summary>
        public event EventHandler<DrawEventArgs> OnDraw;

        /// <summary>
        /// 选区被双击时的事件
        /// </summary>
        public event EventHandler<AreaEventArgs> AreaDoubleClicked;

        public ExtendedCanvas()
        {
            History = new Stack<DrawHistoryItem>();
            BindEvent();
        }

        private void EmitDrawEvent(DrawState state, bool isEmpty = false)
        {
            if (OnDraw == null)
            {
                return;
            }
            OnDraw.Invoke(this, new DrawEventArgs()
            {
                DrawType = current.Shape,
                Shape = current.Element as Shape,
                IsEmpty = isEmpty,
                Area = current.ElementRect,
                State = state
            });
        }

        private void BindEvent()
        {
            MouseLeftButtonDown += OnMouseLeftButtonDown;
            MouseLeftButtonUp += OnMouseLeftButtonUp;
            MouseMove += OnMouseMove;
            MouseRightButtonDown += On_MouseRightButtonDown;
        }

        /// <summary>
        /// 绘制图形
        /// </summary>
        /// <param name="record"></param>
        public void Draw(DrawRecord record)
        {
            var element = record.GetElement();
            if (element == null)
            {
                return;
            }
            if (History.Any(item => item.Element.Equals(element)))
            {
                return;
            }
            Children.Add(element);
            History.Push(new DrawHistoryItem
            {
                Element = element,
                Record = record
            });
            GC.Collect();
        }

        /// <summary>
        /// 重做
        /// </summary>
        public void Redo()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 撤消
        /// </summary>
        public void Undo()
        {
            if (History.Count == 0)
            {
                return;
            }
            var element = History.Pop();
            Children.Remove(element.Element);
            EmitDrawEvent(DrawState.End, true);
        }

        #region 事件
        private void OnMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!EditEnabled)
            {
                return;
            }
            var point = e.GetPosition(this);
            if (current != null && MakeSelectionOnly && History.Count >= 0)
            {
                // 不在框内按下鼠标，不处理拖动
                if (!current.ElementRect.Contains(point))
                {
                    return;
                }
                if (e.ClickCount == 2)
                {
                    // 双击图形，触发双击事件
                    AreaDoubleClicked.Invoke(this, new AreaEventArgs(current.Rect));
                    return;
                }
                MouseDownPoint = point;
                MoveMode = true;
                IsMouseDown = true;
                EmitDrawEvent(DrawState.Start);
                return;
            }

            current = new DrawRecord
            {
                Shape = DrawShape,
                Color = DrawColor,
                Width = DrawWidth,
                TextFont = TextFont
            };
            current.Start = point;
            IsMouseDown = true;
            EmitDrawEvent(DrawState.Start);
        }

        private void OnMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!EditEnabled || !IsMouseDown)
            {
                return;
            }
            IsMouseDown = false;
            if (MoveMode)
            {
                current.Move(this, MouseDownPoint, e.GetPosition(this));
                MoveMode = false;
                EmitDrawEvent(DrawState.End);
                return;
            }
            current.End = e.GetPosition(this);
            Draw(current);
            EmitDrawEvent(DrawState.End);
        }

        private void OnMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!EditEnabled || !IsMouseDown)
            {
                return;
            }

            var point = e.GetPosition(this);
            if (MoveMode)
            {
                current.Move(this, MouseDownPoint, point);
                EmitDrawEvent(DrawState.Move);
                MouseDownPoint = point;
                return;
            }

            current.End = point;
            Draw(current);
            EmitDrawEvent(DrawState.Move);
        }

        private void On_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // 取消图形
            Undo();
        }
        #endregion
    }

    public class DrawEventArgs : EventArgs
    {
        public bool IsEmpty { get; set; }
        public DrawShapes DrawType { get; set; }
        public Shape Shape { get; set; }
        public Rect Area { get; set; }
        public DrawState State { get; set; }

    }

    public enum DrawState
    {
        Start,
        Move,
        End
    }
}
