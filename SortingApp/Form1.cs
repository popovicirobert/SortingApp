using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace SortingApp
{
    public partial class Form1 : Form
    {
        private int[] Heights;
        private Graphics GraphicsObject;

        private ISortEngine SortEngine;
        private readonly SortEngineBubble BubbleEngine;
        private readonly SortEngineMerge MergeEngine;
        private readonly SortEngineQuick QuickEngine;
        private readonly SortEngineHeap HeapEngine;
        private readonly SortEngineRadix RadixEngine;
        private readonly SortEngineMergeIterative MergeIterativeEngine;

        private Thread SortingThread;
        private int PauseCounter;
        //private static ManualResetEvent MRE;
        
        public Form1()
        {
            InitializeComponent();

            this.CenterToScreen();

            //comboBox1.KeyDown += new KeyEventHandler(comboBox1_KeyDown);

            BubbleEngine = new SortEngineBubble();
            MergeEngine = new SortEngineMerge();
            QuickEngine = new SortEngineQuick();
            HeapEngine = new SortEngineHeap();
            RadixEngine = new SortEngineRadix();
            MergeIterativeEngine = new SortEngineMergeIterative();

            SortingThread = null;
            //MRE = new ManualResetEvent(false);

            PauseCounter = 0;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e) // EXIT
        {
            KillSortingThread();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) // RESET
        {
            KillSortingThread();

            GraphicsObject = panel1.CreateGraphics();
            int PanelWidth = panel1.Width;
            int PanelHeight = panel1.Height;


            GraphicsObject.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                                        0, 0, PanelWidth, PanelHeight);

            Random RandGenerator = new Random();
            Heights = new int[PanelWidth];

            for(int i = 0; i < PanelWidth; i++)
                Heights[i] = RandGenerator.Next(1, PanelHeight);
            
            for(int i = 0; i < PanelWidth; i++)
                GraphicsObject.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White),
                                             i, PanelHeight - Heights[i], 1, Heights[i]);
        }

        private void button2_Click(object sender, EventArgs e) // START
        {
            KillSortingThread();

            if (Heights != null && SortEngine != null)
            {
                SortingThread = new Thread(() => SortEngine.Sort(Heights, GraphicsObject, panel1.Height));
                SortingThread.Start();
            }
            else if (Heights == null)
            {
                MessageBox.Show("Press Reset button first",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else // SortEngine == null
            {
                MessageBox.Show("Choose a sorting algorithm first",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        /*private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox1.SelectedItem = "Aal";
                e.Handled = true;
                return;
            }
        }*/

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                SortEngine = BubbleEngine;
            
            if (comboBox1.SelectedIndex == 1)
                SortEngine = MergeEngine;
            
            if (comboBox1.SelectedIndex == 2)
                SortEngine = QuickEngine;
            
            if (comboBox1.SelectedIndex == 3)
                SortEngine = HeapEngine;
            
            if (comboBox1.SelectedIndex == 4)
                SortEngine = RadixEngine;

            if (comboBox1.SelectedIndex == 5)
                SortEngine = MergeIterativeEngine;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) // HELP
        {
            MessageBox.Show("1. Choose a sorting algortihm",
                            "Help",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            MessageBox.Show("2. Press Reset button to reset the array to be sorted",
                            "Help",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            MessageBox.Show("3. Press Start button to start the algortihm",
                            "Help",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void KillSortingThread()
        {
            if (SortingThread != null && SortingThread.IsAlive)
            {
                // If Thread isn't active resume it first and then kill it
                if (PauseCounter % 2 == 1)
                {
                    SortingThread.Resume();
                    PauseCounter++;
                }
                // thread could just have finished
                if(SortingThread.IsAlive)
                    SortingThread.Abort();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                KillSortingThread();

            base.OnFormClosing(e);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) // FILE
        {
            // do nothing
        }

        private void button3_Click(object sender, EventArgs e)  // Pause
        {
            if (SortingThread != null && SortingThread.IsAlive)
            {
                PauseCounter++;

                // Functions might be depreciated causing the program to fail
                // Try using ManualResetEvent instead
                if (PauseCounter % 2 == 0)
                    SortingThread.Resume();
                else
                    SortingThread.Suspend();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // do nothing
        }
    }
}
