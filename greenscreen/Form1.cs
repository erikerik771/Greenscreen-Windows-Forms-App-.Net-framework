using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace greenscreen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = (Bitmap)pictureBox1.Image.Clone();
            Bitmap bmpRs = bmp1;
            Bitmap bmp2 =(Bitmap)pictureBox2.Image;
            Graphics g = Graphics.FromImage(bmpRs);
            Bitmap bmptr = new Bitmap(bmp2.Width, bmp2.Height);
            for (int y = 0; y < bmptr.Height; y++)
            {
                // ...and from left to right
                for (int x = 0; x < bmptr.Width; x++)
                {
                    // Determine the pixel color
                    Color camColor = bmp2.GetPixel(x, y);

                    // Every component (red, green, and blue) can have a value from 0 to 255, so determine the extremes
                    byte max = Math.Max(Math.Max(camColor.R, camColor.G), camColor.B);
                    byte min = Math.Min(Math.Min(camColor.R, camColor.G), camColor.B);

                    // Should the pixel be masked/replaced?
                    bool replace =
                        camColor.G != min // verde este cea mai mica valoare
                        && (camColor.G == max // green is the biggest 
                        || max - camColor.G < 8) // or at least almost the biggest value
                        && (max - min) > 26; // minimum difference between smallest/biggest value (avoid grays)

                    if (replace)
                        camColor = Color.Transparent;

                    // Set the output pixel
                    bmptr.SetPixel(x, y, camColor);
                }
            }
            g.DrawImage(bmptr, 0, 0, bmp1.Size.Width, bmp1.Size.Height);
            pictureBox3.Image = bmpRs;
        }

        //browse images

        private void btnBr_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = System.Drawing.Image.FromFile(dlg.FileName);
            }    
        }
        private void btnbr1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(dlg.FileName);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
