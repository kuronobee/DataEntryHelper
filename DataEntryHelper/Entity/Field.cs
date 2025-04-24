using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntryHelper.Entity
{
    public class Field
    {
        /// <summary>
        /// 識別ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// フィールド名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// フィールドと関連する他のフィールド
        /// </summary>
        public List<Field> Relation { get; set; } = new List<Field>();
        /// <summary>
        /// フィールドの単位
        /// </summary>
        public string? Unit { get; set; } = null;
        /// <summary>
        /// フィールドのタイプ
        /// </summary>
        public FieldType FieldType { get; set; }
        /// <summary>
        /// フィールドのタイプがSelectionだった場合の選択肢
        /// </summary>
        public List<string> FieldValues { get; set; } = new List<string>();
    }

    public enum FieldType
    {
        Number,
        Boolean,
        String,
        Selection,
        Calculate,
    }
}
