using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace haltest1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        HDevelopExport HD = new HDevelopExport();

        private void button1_Click(object sender, EventArgs e)
        {
            HD.RunHalcon(hWindowControl1.HalconWindow);

            int fenzhi1 = 0;

            int fenzhi2 = 0;

        }
    }
}
