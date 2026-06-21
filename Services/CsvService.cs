using AEC.Models;
using System.Text;

namespace AEC.Services
{
    public class CsvService
    {
        public byte[] GerarCsvEnderecos(List<Endereco> enderecos)
        {
            var csv = new StringBuilder();

            csv.AppendLine("Id;CEP;Logradouro;Numero;Complemento;Bairro;Cidade;UF");

            foreach (var endereco in enderecos)
            {
                csv.AppendLine(
                    $"{endereco.Id};" +
                    $"{TratarCampoCsv(endereco.Cep)};" +
                    $"{TratarCampoCsv(endereco.Logradouro)};" +
                    $"{TratarCampoCsv(endereco.Numero)};" +
                    $"{TratarCampoCsv(endereco.Complemento)};" +
                    $"{TratarCampoCsv(endereco.Bairro)};" +
                    $"{TratarCampoCsv(endereco.Cidade)};" +
                    $"{TratarCampoCsv(endereco.Uf)}"
                );
            }

            return Encoding.UTF8.GetPreamble()
                .Concat(Encoding.UTF8.GetBytes(csv.ToString()))
                .ToArray();
        }

        private string TratarCampoCsv(string? campo)
        {
            if (string.IsNullOrEmpty(campo))
            {
                return "";
            }

            campo = campo.Replace("\"", "\"\"");

            if (campo.Contains(";") || campo.Contains("\"") || campo.Contains("\n"))
            {
                return $"\"{campo}\"";
            }

            return campo;
        }
    }
}