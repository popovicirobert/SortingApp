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
    class SortEngineHeap : ISortEngine
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
            HeapSort(Heights.Length);

            if (IsSorted(Heights) == false)
                MessageBox.Show("Array is not sorted");
        }

        private void HeapSort(int Size)
        {
            SortedSet<Tuple<int, int>> HeapObject = new SortedSet<Tuple<int, int>>();
            for (int i = 0; i < Size; i++)
                HeapObject.Add(new Tuple<int, int>(Heights[i], i));
            
            for (int i = 0; i < Size; i++)
            {
                Tuple<int, int> CurrentObject = HeapObject.Min();

                HeapObject.Remove(CurrentObject);
                if (CurrentObject.Item2 != i)
                {
                    HeapObject.Remove(new Tuple<int, int>(Heights[i], i));
                    HeapObject.Add(new Tuple<int, int>(Heights[i], CurrentObject.Item2));
                }

                Swap(i, CurrentObject.Item2, ref Heights, GraphicsObject, PanelHeight);

                Thread.Sleep(SLEEP_TIME);
            }
        }

    }
}
