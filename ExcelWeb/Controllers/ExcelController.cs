using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ClosedXML.Excel;
using System.IO;

namespace BulkyWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExcelController : ControllerBase
    {
        [HttpPost]
        public IActionResult UploadExcel([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            using (var workbook = new XLWorkbook(file.OpenReadStream()))
            {
                foreach (IXLWorksheet worksheet in workbook.Worksheets)
                {
                    // Create a new CSV file for each worksheet
                    var csvFilePath = Path.Combine(Path.GetTempPath(), $"{worksheet.Name}.csv");
                    worksheet.RowsUsed().Select(row => string.Join(",", row.Cells().Select(cell => cell.Value))).ToList().ForEach(row => System.IO.File.AppendAllText(csvFilePath, row + "\n"));
                }
            }

            return Ok("CSV files created successfully");
        }
    }
}
