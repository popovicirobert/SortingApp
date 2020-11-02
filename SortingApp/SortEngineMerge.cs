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
    class SortEngineMerge : ISortEngine
    {
        private int[] Heights;
        private int[] NewHeights;
        private Graphics GraphicsObject;
        private int PanelHeight;

        private const int SLEEP_PERIOD = 3;
        private const int SLEEP_TIME = 1;

        public override void Sort(int[] _Heights, Graphics _GraphicsObject, int _PanelHeight)
        {
            Heights = _Heights;
            GraphicsObject = _GraphicsObject;
            PanelHeight = _PanelHeight;

            NewHeights = new int[Heights.Length];

            RepaintPanel(Heights, GraphicsObject, PanelHeight);
            MergeSort(0, Heights.Length - 1);

            if (IsSorted(Heights) == false)
                MessageBox.Show("Array is not sorted");
        }

        private void MergeSort(int LeftIndex, int RightIndex)
        {
            if (LeftIndex == RightIndex) return;

            int MiddleIndex = (LeftIndex + RightIndex) / 2;

            MergeSort(LeftIndex, MiddleIndex);
            MergeSort(MiddleIndex + 1, RightIndex);

            int Index = LeftIndex;
            int NewLeftIndex = LeftIndex;
            int NewRightIndex = MiddleIndex + 1;
            while(NewLeftIndex <= MiddleIndex && NewRightIndex <= RightIndex)
            {
                if (Heights[NewLeftIndex] < Heights[NewRightIndex])
                    NewHeights[Index++] = Heights[NewLeftIndex++];
                else
                    NewHeights[Index++] = Heights[NewRightIndex++];
            }
            while(NewLeftIndex <= MiddleIndex)
                NewHeights[Index++] = Heights[NewLeftIndex++];
            
            while(NewRightIndex <= RightIndex)
                NewHeights[Index++] = Heights[NewRightIndex++];
            
            for(int i = LeftIndex; i <= RightIndex; i++)
            {
                GraphicsObject.FillRectangle(RedBrush, i, PanelHeight - Heights[i], 1, Heights[i]);

                if(i == RightIndex || (i - LeftIndex) % SLEEP_PERIOD == 0)
                    Thread.Sleep(SLEEP_TIME);
            }
            for (int i = LeftIndex; i <= RightIndex; i++)
            {
                Heights[i] = NewHeights[i];
                Refresh(i, Heights, GraphicsObject, PanelHeight);
            }
        }

    }
}
