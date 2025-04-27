using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;


namespace DataEntryHelper.Services
{
    public static class CsvLabResultExtractor
    {
        /// <summary>
        /// CSV (OriginalName,OriginalUnit) を読み込んでマップを返す
        /// </summary>
        private static Dictionary<string, string> LoadTemplate(string csvPath)
        {
            var lines = File.ReadAllLines(csvPath);
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // 1行目はヘッダーなのでスキップ
            foreach (var line in lines.Skip(1))
            {
                // カンマで分割（OriginalName,OriginalUnit のみなので単純にOK）
                var parts = line.Split(',');
                if (parts.Length < 2) continue;

                var name = parts[0].Trim().Trim('"');
                var unit = parts[1].Trim().Trim('"');

                if (!map.ContainsKey(name))
                    map[name] = unit;
            }

            return map;
        }

        /// <summary>
        /// テキストから、CSVで指定された項目＋単位を持つ数値を抽出する
        /// </summary>
        [STAThread]
        public static IDictionary<string, string> ExtractFromText(string rawText)
        {
            var csvPath = "target_templates.csv";
            // ① テンプレート CSV を読み込む
            var templateMap = LoadTemplate(csvPath);

            // ② 結果辞書を初期化（空文字で埋める）
            var result = templateMap.Keys.ToDictionary(k => k, k => string.Empty, StringComparer.OrdinalIgnoreCase);

            // ④ 行ごとにパース
            var lines = rawText
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                // 「│」区切りのデータ行だけを対象
                if (!line.Contains("│")) continue;

                var parts = line.Split('│');
                if (parts.Length < 7) continue;

                var name = parts[1].Trim();  // OriginalName
                var value = parts[2].Trim();  // 結果
                var unit = parts[6].Trim();  // OriginalUnit

                // マップに登録されていて単位が一致すれば抽出
                if (templateMap.TryGetValue(name, out var expectedUnit)
                    && string.Equals(unit, expectedUnit, StringComparison.OrdinalIgnoreCase))
                {
                    result[name] = value;
                }
            }

            return result;
        }
    }
}
