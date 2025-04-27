using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DataEntryHelper.Controls
{
    /// <summary>
    /// TTASControl.xaml の相互作用ロジック
    /// </summary>
    public partial class TTASControl : UserControl
    {
        // PL（血小板血栓形成能）の正常範囲
        private const double PL_LOWER_THRESHOLD = 71.5;
        private const double PL_UPPER_THRESHOLD = 137.9;

        // AR（血小板・凝固血栓形成能）の正常範囲
        private const double AR_LOWER_THRESHOLD = 1550.0;
        private const double AR_UPPER_THRESHOLD = 1810.0;

        public TTASControl()
        {
            InitializeComponent();
        }

        // 入力テキストボックスの値変更イベントハンドラ
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTTASSummary();
        }

        // T-TAS結果要約の更新メソッド
        private void UpdateTTASSummary()
        {
            StringBuilder summary = new StringBuilder();

            try
            {
                // PLの評価
                if (double.TryParse(TTASPLTextBox.Text, out double plValue))
                {
                    summary.AppendLine("【PL（血小板血栓形成能）の評価】");

                    if (plValue < PL_LOWER_THRESHOLD)
                    {
                        summary.AppendLine($"PL値: {plValue} - 基準値より低値");
                        summary.AppendLine("血小板機能が低下しており、抗血小板薬の効果が強く出ている可能性や、出血リスクが上昇している可能性があります。");
                    }
                    else if (plValue > PL_UPPER_THRESHOLD)
                    {
                        summary.AppendLine($"PL値: {plValue} - 基準値より高値");
                        summary.AppendLine("血小板機能が亢進しており、血栓形成リスクが上昇している可能性があります。");
                    }
                    else
                    {
                        summary.AppendLine($"PL値: {plValue} - 正常範囲内");
                        summary.AppendLine("血小板血栓形成能は正常範囲内です。");
                    }

                    summary.AppendLine();
                }

                // ARの評価
                if (double.TryParse(TTASARTextBox.Text, out double arValue))
                {
                    summary.AppendLine("【AR（血小板・凝固血栓形成能）の評価】");

                    if (arValue < AR_LOWER_THRESHOLD)
                    {
                        summary.AppendLine($"AR値: {arValue} - 基準値より低値");
                        summary.AppendLine("血小板・凝固因子による血栓形成能が低下しており、抗凝固薬や抗血小板薬の効果が強く出ている可能性や、出血リスクが上昇している可能性があります。");
                    }
                    else if (arValue > AR_UPPER_THRESHOLD)
                    {
                        summary.AppendLine($"AR値: {arValue} - 基準値より高値");
                        summary.AppendLine("血小板・凝固因子による血栓形成能が亢進しており、血栓形成リスクが上昇している可能性があります。");
                    }
                    else
                    {
                        summary.AppendLine($"AR値: {arValue} - 正常範囲内");
                        summary.AppendLine("血小板・凝固血栓形成能は正常範囲内です。");
                    }

                    summary.AppendLine();
                }

                // 総合評価
                if (double.TryParse(TTASPLTextBox.Text, out double pl) &&
                    double.TryParse(TTASARTextBox.Text, out double ar))
                {
                    summary.AppendLine("【総合評価】");

                    if (pl < PL_LOWER_THRESHOLD && ar < AR_LOWER_THRESHOLD)
                    {
                        summary.AppendLine("血小板機能と凝固機能の両方が低下しています。出血リスクに注意が必要です。");
                    }
                    else if (pl > PL_UPPER_THRESHOLD && ar > AR_UPPER_THRESHOLD)
                    {
                        summary.AppendLine("血小板機能と凝固機能の両方が亢進しています。血栓リスクに注意が必要です。");
                    }
                    else if (pl < PL_LOWER_THRESHOLD || ar < AR_LOWER_THRESHOLD)
                    {
                        summary.AppendLine("血栓形成能の低下が見られます。抗血栓薬の効果や出血リスクの評価が必要です。");
                    }
                    else if (pl > PL_UPPER_THRESHOLD || ar > AR_UPPER_THRESHOLD)
                    {
                        summary.AppendLine("血栓形成能の亢進が見られます。血栓リスクの評価が必要です。");
                    }
                    else
                    {
                        summary.AppendLine("総合的に血栓形成能は正常範囲内です。");
                    }
                }

                if (string.IsNullOrEmpty(summary.ToString()))
                {
                    summary.AppendLine("T-TASデータが入力されていません。");
                }
            }
            catch (Exception ex)
            {
                summary.AppendLine($"要約生成中にエラーが発生しました: {ex.Message}");
            }

            TTASSummaryTextBlock.Text = summary.ToString();
        }

        // T-TASデータの取得メソッド
        public TTASData GetTTASData()
        {
            return new TTASData
            {
                // T-TAS測定値
                TTASPL = TTASPLTextBox.Text,
                TTASAR = TTASARTextBox.Text,

                // 結果要約
                TTASSummary = TTASSummaryTextBlock.Text
            };
        }

        // データクリアメソッド
        public void ClearData()
        {
            // テキストボックスをクリア
            TTASPLTextBox.Clear();
            TTASARTextBox.Clear();

            // 要約をクリア
            TTASSummaryTextBlock.Text = string.Empty;
        }
    }

    // T-TASデータクラス
    public class TTASData
    {
        // T-TAS測定値
        public string TTASPL { get; set; } = "";
        public string TTASAR { get; set; } = "";

        // 結果要約
        public string TTASSummary { get; set; } = "";
    }
}