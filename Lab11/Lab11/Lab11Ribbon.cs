using static Lab11.LabsLibrary.Labs;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lab11.LabsLibrary;

namespace Lab11
{
    public partial class Lab11Ribbon
    {
        private void Lab11Ribbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            Lab1 lab1 = new Lab1();
            Lab2 lab2 = new Lab2();
            Lab3 lab3 = new Lab3();
            for (int indexLab = 1; indexLab <= 3; indexLab++)
            {
                string inputPath = $"../../FilesInput/INPUTLab{indexLab}.TXT";
                string outputPath = $"../../FilesOutput/OUTPUTLab{indexLab}.TXT";
                switch (indexLab)
                {
                    case 1:
                        string lab1_resultBuild = lab1.Build();
                        string lab1_resultTest = lab1.Test();
                        string lab1_resultRun = lab1.Run(inputPath, outputPath);
                        break;
                    case 2:
                        string lab2_resultBuild = lab2.Build();
                        string lab2_resultTest = lab2.Test();
                        string lab2_resultRun = lab2.Run(inputPath, outputPath);
                        break;
                    case 3:
                        string lab3_resultBuild = lab3.Build();
                        string lab3_resultTest = lab3.Test();
                        string lab3_resultRun = lab3.Run(inputPath, outputPath);
                        break;
                }
            }
        }
    }
}
