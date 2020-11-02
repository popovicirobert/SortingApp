using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SortingApp
{
    class SortEngineBubble : ISortEngine
    {
        private int[] Heights;
        private Graphics GraphicsObject;
        private int PanelHeight;

        public override void Sort(int[] _Heights, Graphics _GraphicsObject, int _PanelHeight)
        {
            Heights = _Heights;
            GraphicsObject = _GraphicsObject;
            PanelHeight = _PanelHeight;

            RepaintPanel(Heights, GraphicsObject, PanelHeight);
            BubbleSort(Heights.Length);

            if (IsSorted(Heights) == false)
                MessageBox.Show("Array is not sorted");
        }

        private void BubbleSort(int Size)
        {
            bool notSorted = true;

            while(notSorted == true)
            {
                notSorted = false;
                for(int i = 0; i + 1 < Size; i++)
                {
                    if(Heights[i] > Heights[i + 1])
                    {
                        Swap(i, i + 1, ref Heights, GraphicsObject, PanelHeight);
                        notSorted = true;
                    }
                }
            }
        }

    }
}
