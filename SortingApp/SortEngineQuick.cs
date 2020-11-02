using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SortingApp
{
    class SortEngineQuick : ISortEngine
    {
        private int[] Heights;
        private Graphics GraphicsObject;
        private int PanelHeight;

        private const int SLEEP_TIME = 5;

        public override void Sort(int[] _Heights, Graphics _GraphicsObject, int _PanelHeight)
        {
            Heights = _Heights;
            GraphicsObject = _GraphicsObject;
            PanelHeight = _PanelHeight;

            RepaintPanel(Heights, GraphicsObject, PanelHeight);
            QuickSort(0, Heights.Length - 1);

            if(IsSorted(Heights) == false)
                MessageBox.Show("Array is not sorted");
        }

        private void QuickSort(int LeftIndex, int RightIndex)
        {
            int PivotHeight = Heights[(LeftIndex + RightIndex) / 2];
            int NewLeftIndex = LeftIndex;
            int NewRightIndex = RightIndex;

            while (NewLeftIndex <= NewRightIndex)
            {
                while (Heights[NewLeftIndex] < PivotHeight) 
                    NewLeftIndex++;

                while (Heights[NewRightIndex] > PivotHeight) 
                    NewRightIndex--;

                if (NewLeftIndex <= NewRightIndex)
                {
                    GraphicsObject.FillRectangle(RedBrush, 
                                                NewLeftIndex,
                                                PanelHeight - Heights[NewLeftIndex],
                                                1, 
                                                Heights[NewLeftIndex]);

                    GraphicsObject.FillRectangle(RedBrush,
                                                NewRightIndex,
                                                PanelHeight - Heights[NewRightIndex],
                                                1,
                                                Heights[NewRightIndex]);

                    Thread.Sleep(SLEEP_TIME);

                    Swap(NewLeftIndex, NewRightIndex, ref Heights, GraphicsObject, PanelHeight);
                    NewLeftIndex++;
                    NewRightIndex--;
                }
            }

            if (LeftIndex < NewRightIndex) 
                QuickSort(LeftIndex, NewRightIndex);

            if (RightIndex > NewLeftIndex) 
                QuickSort(NewLeftIndex, RightIndex);
        }

    }
}
