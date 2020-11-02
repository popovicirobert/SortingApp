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
    class SortEngineMergeIterative : ISortEngine
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
            IterativeMergeSort(Heights.Length);

            if (IsSorted(Heights) == false)
                MessageBox.Show("Array is not sorted");
        }

        private void IterativeMergeSort(int Size)
        {
            for(int Length = 1; Length < Size; Length *= 2)
            {
                for(int i = 0; i + Length < Size; i += 2 * Length)
                {
                    int LeftIndex = i;
                    int RightIndex = i + Length;
                    int UpperIndex = Math.Min(i + 2 * Length, Size);
                    int Index = LeftIndex;

                    while(LeftIndex < i + Length && RightIndex < UpperIndex)
                    {
                        if (Heights[LeftIndex] < Heights[RightIndex])
                            NewHeights[Index++] = Heights[LeftIndex++];
                        else
                            NewHeights[Index++] = Heights[RightIndex++];
                    }

                    while (LeftIndex < i + Length)
                        NewHeights[Index++] = Heights[LeftIndex++];

                    while (RightIndex < UpperIndex)
                        NewHeights[Index++] = Heights[RightIndex++];

                    Index = i;
                    while(Index < UpperIndex)
                    {
                        GraphicsObject.FillRectangle(RedBrush,
                                                    Index, PanelHeight - Heights[Index],
                                                    1, Heights[Index]);

                        if (Index == UpperIndex - 1 || (Index - i) % SLEEP_PERIOD == 0)
                            Thread.Sleep(SLEEP_TIME);

                        Index++;
                    }

                    Index = i;
                    while(Index < UpperIndex)
                    {
                        Heights[Index] = NewHeights[Index];
                        Refresh(Index, Heights, GraphicsObject, PanelHeight);
                        Index++;
                    }
                }
            }
        }
    }
}
