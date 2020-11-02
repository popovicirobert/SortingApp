using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace SortingApp
{
    class ISortEngine
    {

        protected Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        protected Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        protected Brush RedBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);

        public virtual void Sort(int[] Heights, Graphics GraphicsObject, int PanelHeight)
        {
            // do nothing
        }

        private void Update(int Position, int Height, Graphics GraphicsObject, int PanelHeight)
        {
            GraphicsObject.FillRectangle(WhiteBrush, Position, PanelHeight - Height, 1, Height);
            GraphicsObject.FillRectangle(BlackBrush, Position, 0, 1, PanelHeight - Height);
        }

        // avoid small lines apearing on screen, generated probably by precision errors
        // issue fixed by repainting [Position - 1, Position + 1] instead of Position
        protected void Refresh(int Position, int[] Heights, Graphics GraphicsObject, int PanelHeight)
        {
            for (int i = Math.Max(0, Position - 1); i < Math.Min(Heights.Length, Position + 2); i++)
                Update(i, Heights[i], GraphicsObject, PanelHeight);
        }

        protected void Swap(int Position1, int Position2, ref int[] Heights,
                            Graphics GraphicsObject, int PanelHeight)
        {
            int CurrentHeight = Heights[Position1];
            Heights[Position1] = Heights[Position2];
            Heights[Position2] = CurrentHeight;

            Refresh(Position1, Heights, GraphicsObject, PanelHeight);
            Refresh(Position2, Heights, GraphicsObject, PanelHeight);
        }

        protected bool IsSorted(int[] Heights)
        {
            for(int i = 0; i + 1 < Heights.Length; i++)
            {
                if (Heights[i] > Heights[i + 1])
                    return false;
            }

            return true;
        }

        protected void RepaintPanel(int[] Heights, Graphics GraphicsObject, int PanelHeight)
        {
            for(int i = 0; i < Heights.Length; i++)
                Update(i, Heights[i], GraphicsObject, PanelHeight);
        }
    }
}
