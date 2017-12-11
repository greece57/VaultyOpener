using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VaultyOpener
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputPath = textBox1.Text;
            string outputPath = textBox2.Text;

            string[] filePaths = Directory.GetFiles(inputPath, "*.vdata", SearchOption.AllDirectories);

            int fileNr = 0;
            int vidNr = 0;
            foreach (string filePath in filePaths)
            {
                var firstLine = File.ReadAllLines(filePath)[0];

                if (firstLine.Contains("obscure"))
                {
                    string filePathOut = outputPath + "\\pic" + fileNr.ToString() + ".png";

                    FileStream inFs = new FileStream(filePath, FileMode.Open);
                    try
                    {
                        FileStream outFs = new FileStream(filePathOut, FileMode.CreateNew);
                        int hexIn;

                        for (int i = 0; i < 8; i++)
                        {
                            inFs.ReadByte();
                        }

                        for (int i = 0; (hexIn = inFs.ReadByte()) != -1; i++)
                        {
                            outFs.WriteByte((byte) hexIn);
                        }

                        inFs.Close();
                        outFs.Close();

                        fileNr++;
                    } catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                } else
                {
                    string filePathOut = outputPath + "\\vid" + vidNr.ToString() + ".mp4";

                    File.Copy(filePath, filePathOut);

                    vidNr++;
                }

            }


            MessageBox.Show("Converted " + fileNr.ToString() + " Files");
        }
    }
}
