using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace SortingApp
{
    class SortEngineRadix : ISortEngine
    {
        private int[] Heights;
        private Graphics GraphicsObject;
        private int PanelHeight;

        private const int BUCKET = 4;
        private const int NUMBITS = 32;

        private const int SLEEP_TIME = 5;

        public override void Sort(int[] _Heights, Graphics _GraphicsObject, int _PanelHeight)
        {
            Heights = _Heights;
            GraphicsObject = _GraphicsObject;
            PanelHeight = _PanelHeight;

            RepaintPanel(Heights, GraphicsObject, PanelHeight);
            RadixSort(Heights.Length);

            if (IsSorted(Heights) == false)
                MessageBox.Show("Array is not sorted");
        }

        private void RadixSort(int Size)
        {
            int[] Frequency = new int[1 << BUCKET];
            int[] NewHeights = new int[Size];

            for(int Step = 0; Step * BUCKET < NUMBITS && IsSorted(Heights) == false; Step++)
            {
                for(int i = 0; i < (1 << BUCKET); i++)
                    Frequency[i] = 0;
                
                for(int i = 0; i < Size; i++)
                    Frequency[getBits(Heights[i], Step)]++;
                
                for(int i = 1; i < (1 << BUCKET); i++)
                    Frequency[i] += Frequency[i - 1];
                
                for(int i = Size - 1; i >= 0; i--)
                {
                    int Index = getBits(Heights[i], Step);
                    NewHeights[--Frequency[Index]] = Heights[i];
                }

                for(int i = 0; i < Size; i++)
                {
                    Heights[i] = NewHeights[i];
                    Refresh(i, Heights, GraphicsObject, PanelHeight);
                    Thread.Sleep(SLEEP_TIME);
                }
            }
        }

        private int getBits(int Height, int BucketNumber)
        {
            return (Height >> (BucketNumber * BUCKET)) & ((1 << BUCKET) - 1);
        }
    }
}
