using Business;
using Business.Repositories;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace CrossCutting
{
    public class InteropExcel
    {

        public Utilitie<Clientes> ClientReadData(FileStream stream)
        {
            try
            {
                ISheet sheet;

                stream.Position = 0;
                if (sFileExtension == ".xls")
                {
                    HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                    sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                }
                else
                {
                    XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                    sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                }
                IRow headerRow = sheet.GetRow(0); //Get Header Row




                for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++) //Read Excel File
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;


                    Clientes cli = new Clientes();

                    cli.nome = row.GetCell(0).ToString();
                    cli.empresa = row.GetCell(1).ToString();
                    cli.email = row.GetCell(2).ToString();
                    cli.cnpj = row.GetCell(3).ToString();
                    cli.fonecomercial = row.GetCell(4).ToString();
                    cli.celular = row.GetCell(5).ToString();
                    cli.cep = row.GetCell(6).ToString();
                    cli.cidade = row.GetCell(7).ToString();
                    cli.estado = row.GetCell(8).ToString();
                    cli.codigo_acesso = null;

                    new RClientes().Insert(cli);




                }

                return new Utilitie<Clientes>("Erro ao Salvar dados no Banco", false);
            }
            catch(Exception ex)
            {

                return new Utilitie<Clientes>("Dados Salvos com Sucesso", true);

            }

            

        }

    }



    //static DataSet ReadExcelFile()
    //{
    //    DataSet ds = new DataSet();

    //    string connectionString = GetConnectionString();

    //    using (OleDbConnection conn = new OleDbConnection(connectionString))    
    //    {
    //        conn.Open();
    //        OleDbCommand cmd = new OleDbCommand();
    //        cmd.Connection = conn;

    //        cmd.CommandText = "SELECT * FROM [Plan1$]";

    //        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
    //        da.Fill(ds);

    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            // Console.WriteLine(ds.Tables[0].Rows[i].ItemArray[0].ToString());

    //            for (int j = 0; j < ds.Tables[0].Rows[i].ItemArray.Length; j++)
    //            {
    //                Console.WriteLine(ds.Tables[0].Rows[i].ItemArray[j].ToString());
    //            }

    //        }

    //        cmd = null;
    //        conn.Close();

    //    }

    //    return ds;
    //}

    //static string GetConnectionString()
    //{
    //    Dictionary<string, string> props = new Dictionary<string, string>();

    //    // XLSX - Excel 2007, 2010, 2012, 2013
    //    props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
    //    props["Extended Properties"] = "Excel 12.0 XML";
    //    props["Data Source"] = "C:\\Users\\dsantos3\\Desktop\\import.xlsx";

    //    // XLS - Excel 2003 and Older
    //    //props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
    //    //props["Extended Properties"] = "Excel 8.0";
    //    //props["Data Source"] = "C:\\MyExcel.xls";

    //    StringBuilder sb = new StringBuilder();

    //    foreach (KeyValuePair<string, string> prop in props)
    //    {
    //        sb.Append(prop.Key);
    //        sb.Append('=');
    //        sb.Append(prop.Value);
    //        sb.Append(';');
    //    }

    //    return sb.ToString();
    //}

    //public static string RemoveAccents(string input)
    //{
    //    return new string(
    //        input
    //        .Normalize(System.Text.NormalizationForm.FormD)
    //        .ToCharArray()
    //        .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
    //        .ToArray());
    //    // the normalization to FormD splits accented letters in accents+letters
    //    // the rest removes those accents (and other non-spacing characters)
    //}

}