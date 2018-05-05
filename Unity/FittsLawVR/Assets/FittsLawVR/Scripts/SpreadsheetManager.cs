using UnityEngine;

public class SpreadsheetManager : MonoBehaviour
{
    private ES2Spreadsheet sheet = new ES2Spreadsheet();
    private string resultsFile;

    public void CreateSpreadsheet(string participantID)
    {
        resultsFile = "Results/" + participantID + ".csv";

        if (!(ES2.Exists(resultsFile)))
        {
            sheet = new ES2Spreadsheet();
           
            sheet.SetCell(0, 0, "Trial Name");
            sheet.SetCell(1, 0, "Participant ID");
            sheet.SetCell(2, 0, "Participant Name");
            sheet.SetCell(3, 0, "Sample");
            sheet.SetCell(4, 0, "Elapsed Time");
            sheet.SetCell(5, 0, "Start Target Scale");
            sheet.SetCell(6, 0, "Goal Target Scale");
            sheet.SetCell(7, 0, "Arc Distance");
            sheet.SetCell(8, 0, "Start Target Origin");
            sheet.SetCell(9, 0, "Goal Target Origin");
            sheet.SetCell(10, 0, "Start Target Color");
            sheet.SetCell(11, 0, "Goal Target Color");
            sheet.SetCell(12, 0, "Cursor Position Over Time");

            sheet.Save(resultsFile);

            sheet.append = true;
        }
    }

    public void AppendNewRow(string[] data)
    {
        // When ES2Spreadsheet.append is set to true, the row can be left as 0. New Rows will be added to the spreadsheet.
        sheet.SetCell(0, 0, data[0]);
        sheet.SetCell(1, 0, data[1]);
        sheet.SetCell(2, 0, data[2]);
        sheet.SetCell(3, 0, data[3]);
        sheet.SetCell(4, 0, data[4]);
        sheet.SetCell(5, 0, data[5]);
        sheet.SetCell(6, 0, data[6]);
        sheet.SetCell(7, 0, data[7]);
        sheet.SetCell(8, 0, data[8]);
        sheet.SetCell(9, 0, data[9]);
        sheet.SetCell(10, 0, data[10]);
        sheet.SetCell(11, 0, data[11]);
        sheet.SetCell(12, 0, data[12]);

        sheet.Save(resultsFile);
    }

    public string GetResultsFile()
    {
        return resultsFile;
    }
}
