using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProj.Connection;
using NPOI.WP.UserModel;
using NPOI.XWPF.UserModel;

namespace HospitalProj.Export;

public class Word
{
    public void GenerateWordExportFile(List<List<string>> lines, string docxPath, string doctor, string patient)
    {
        XWPFDocument doc = new XWPFDocument();
        
        var paragraph = doc.CreateParagraph();

        var run = paragraph.CreateRun();
        run.SetText("Собирательный отчет по встречам:");
        run.FontSize = 18;
        run.AddTab();

        XWPFTable table = doc.CreateTable(lines.Count, lines.First().Count);

        //fill data cells
        for (int r = 0; r < lines.Count; r++)
        {
            for (int c = 0; c < lines[r].Count; c++)
            {
                XWPFTableCell cell = table.GetRow(r).GetCell(c);
                cell.SetText(lines[r][c]);
            }

            if (lines[r][1] == "groupCell")
            {
                table.Rows[r].MergeCells(0, lines[0].Count - 1);
            }
        }
        
        var paragraph2 = doc.CreateParagraph();
        var paragraph3 = doc.CreateParagraph();
        var paragraph4 = doc.CreateParagraph();
        
        var runPsycho = paragraph2.CreateRun();
        runPsycho.SetText($"Психолог: {doctor}");
        runPsycho.FontSize = 11;
        runPsycho.AddTab();
        
        var runPatient = paragraph3.CreateRun();
        runPatient.SetText($"Пациент: {patient}");
        runPatient.FontSize = 11;
        runPatient.AddTab();
        
        var runDateCreation = paragraph4.CreateRun();
        runDateCreation.SetText($"Дата создания отчета: {DateTime.Now.ToString("dd.MM.yyyy")}г.");
        runDateCreation.FontSize = 11;
        runDateCreation.AddTab();

        //create file
        try
        {
            using (FileStream file = File.Create(docxPath))
            {
                doc.Write(file);
            }
        }
        catch (IOException)
        {
            "Данный файл занят".Show("Ошибка");
        }
    }
}