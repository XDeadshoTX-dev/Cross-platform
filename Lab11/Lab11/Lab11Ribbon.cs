using static Lab11.LabsLibrary.Labs;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Word;
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

            Microsoft.Office.Interop.Word.Application wordApp = Globals.ThisAddIn.Application; // Access Word application
            Document activeDoc = wordApp.ActiveDocument;

            if (activeDoc == null)
            {
                MessageBox.Show("Please open a Word document before running the program.", "Error");
                return;
            }

            for (int indexLab = 1; indexLab <= 3; indexLab++)
            {
                string inputPath = $"../../FilesInput/INPUTLab{indexLab}.TXT";
                string outputPath = $"../../FilesOutput/OUTPUTLab{indexLab}.TXT";
                string results = $"Results for Lab {indexLab}:\n";

                switch (indexLab)
                {
                    case 1:
                        results += $"Build: {lab1.Build()}\n";
                        results += $"Test: {lab1.Test()}\n";
                        results += $"Run: {lab1.Run(inputPath, outputPath)}\n";
                        break;
                    case 2:
                        results += $"Build: {lab2.Build()}\n";
                        results += $"Test: {lab2.Test()}\n";
                        results += $"Run: {lab2.Run(inputPath, outputPath)}\n";
                        break;
                    case 3:
                        results += $"Build: {lab3.Build()}\n";
                        results += $"Test: {lab3.Test()}\n";
                        results += $"Run: {lab3.Run(inputPath, outputPath)}\n";
                        break;
                }

                activeDoc.Content.Text += results + "\n";
            }

            MessageBox.Show("Results have been added to the current document.", "Success");
        }
    }
}
