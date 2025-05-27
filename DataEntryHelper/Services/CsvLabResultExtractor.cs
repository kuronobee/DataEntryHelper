using System;
using System.Collections.Generic;
using System.Linq;

namespace DataEntryHelper.Services
{
    public static class CsvLabResultExtractor
    {
        /// <summary>
        /// 検査項目のマッピング定義
        /// </summary>
        private static readonly Dictionary<string, string> LabItemMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"TP", "TP"},
            {"ALB", "Alb"},
            {"BUN", "BUN"},
            {"CRE", "Cre"},
            {"CRP", "CRP"},
            {"CK", "CK"},
            {"AST", "AST"},
            {"ALT", "ALT"},
            {"LDL-C", "LDL-C"},
            {"HDL-C", "HDL-C"},
            {"TG", "TG"},
            {"HbA1c", "HbA1c"},
            {"GLU", "Glu"},
            {"Hb", "Hb"},
            {"WBC", "WBC"},
            {"PLT", "Plt"},
            {"INR", "PT-INR"},
            {"APTT", "APTT"},
            {"UA", "UA"},
            {"BNP", "BNP"}
        };

        /// <summary>
        /// テキストから、マッピングで指定された項目の数値を抽出する
        /// </summary>
        public static IDictionary<string, string> ExtractFromText(string rawText)
        {
            // 結果辞書を初期化（空文字で埋める）
            var result = LabItemMapping.Values.ToDictionary(v => v, v => string.Empty, StringComparer.OrdinalIgnoreCase);

            // 行ごとにパース
            var lines = rawText
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                // 「│」区切りのデータ行だけを対象
                if (!line.Contains("│")) continue;

                var parts = line.Split('│');
                if (parts.Length < 3) continue; // 最低限、項目名と値が必要

                var itemName = parts[1].Trim();  // 検査項目名
                var value = parts[2].Trim();     // 結果値

                // マッピングに登録されている項目かチェック
                if (LabItemMapping.TryGetValue(itemName, out var mappedName))
                {
                    // 数値部分のみを抽出（単位や記号を除去）
                    var cleanValue = ExtractNumericValue(value);
                    if (!string.IsNullOrEmpty(cleanValue))
                    {
                        result[mappedName] = cleanValue;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 文字列から数値部分を抽出する
        /// </summary>
        /// <param name="value">抽出対象の文字列</param>
        /// <returns>抽出された数値文字列</returns>
        private static string ExtractNumericValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            // 数値、小数点、マイナス記号のみを抽出
            var numericChars = value.Where(c => char.IsDigit(c) || c == '.' || c == '-').ToArray();
            var numericString = new string(numericChars);

            // 有効な数値かチェック
            if (double.TryParse(numericString, out _))
            {
                return numericString;
            }

            return string.Empty;
        }
    }
}