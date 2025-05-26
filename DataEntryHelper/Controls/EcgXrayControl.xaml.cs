using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DataEntryHelper.Controls
{
    /// <summary>
    /// EcgXrayControl.xaml の相互作用ロジック
    /// </summary>
    public partial class EcgXrayControl : UserControl
    {
        public EcgXrayControl()
        {
            InitializeComponent();
        }

        // 心電図情報が変更されたときのイベントハンドラ
        private void UpdateEcgSummary(object sender, EventArgs e)
        {
            GenerateEcgSummary();
        }

        // レントゲン情報が変更されたときのイベントハンドラ
        private void UpdateXraySummary(object sender, EventArgs e)
        {
            GenerateXraySummary();
        }

        // 心電図レポート要約生成
        private void GenerateEcgSummary()
        {
            StringBuilder summary = new StringBuilder();

            try
            {
                // 各項目のデータを取得
                string hr = HrTextBox.Text.Trim();
                string rhythm = GetComboBoxText(RhythmComboBox);
                string axis = GetComboBoxText(AxisComboBox);
                string conductionDisturbance = GetComboBoxText(ConductionDisturbanceComboBox);
                string stTChange = GetComboBoxText(StTChangeComboBox);
                string comment = EcgCommentTextBox.Text.Trim();

                // 心拍数
                if (!string.IsNullOrEmpty(hr))
                {
                    summary.AppendLine($"心拍数: {hr} bpm");
                }

                // 調律
                if (!string.IsNullOrEmpty(rhythm))
                {
                    summary.AppendLine($"調律: {rhythm}");

                    // 調律異常の臨床的意義
                    if (rhythm == "心房細動")
                    {
                        summary.AppendLine("※心房細動による塞栓症リスクの評価が必要です");
                    }
                }

                // 電気軸
                if (!string.IsNullOrEmpty(axis))
                {
                    summary.AppendLine($"電気軸: {axis}");

                    // 軸偏位の臨床的意義
                    if (axis == "LAD")
                    {
                        summary.AppendLine("※左軸偏位：左室肥大や左脚ブロックなどによる可能性があります");
                    }
                    else if (axis == "RAD")
                    {
                        summary.AppendLine("※右軸偏位：右室肥大や右脚ブロックなどによる可能性があります");
                    }
                    else if (axis == "no man's land")
                    {
                        summary.AppendLine("※著明な左軸偏位：左後枝ブロックの可能性があります");
                    }
                }

                // 伝導障害
                if (!string.IsNullOrEmpty(conductionDisturbance) && conductionDisturbance != "なし")
                {
                    summary.AppendLine($"伝導障害: {conductionDisturbance}");

                    // 伝導障害の臨床的意義
                    if (conductionDisturbance == "LBBB")
                    {
                        summary.AppendLine("※左脚ブロック：冠動脈疾患、弁膜症、心筋症などによる可能性があります");
                    }
                    else if (conductionDisturbance == "RBBB")
                    {
                        summary.AppendLine("※右脚ブロック：先天性心疾患、肺性心などによる可能性があります");
                    }
                }

                // ST-T変化
                if (!string.IsNullOrEmpty(stTChange))
                {
                    summary.AppendLine($"ST-T変化: {stTChange}");

                    // ST-T変化の臨床的意義
                    if (stTChange == "あり")
                    {
                        summary.AppendLine("※ST-T変化：虚血性心疾患、電解質異常、薬剤の影響などによる可能性があります");
                    }
                }

                // コメント
                if (!string.IsNullOrEmpty(comment))
                {
                    summary.AppendLine($"所見: {comment}");
                }

                // 総合評価
                if (summary.Length > 0)
                {
                    summary.AppendLine();
                    bool hasSignificantAbnormality = false;

                    // 重要な異常の有無を判定
                    if (rhythm == "心房細動" ||
                        conductionDisturbance == "LBBB" ||
                        stTChange == "あり")
                    {
                        hasSignificantAbnormality = true;
                    }

                    // 総合所見を追加
                    if (hasSignificantAbnormality)
                    {
                        summary.AppendLine("【総合評価】臨床的に重要な異常を認めます");
                    }
                    else if (rhythm == "洞調律" &&
                             (axis == "正常" || string.IsNullOrEmpty(axis)) &&
                             (conductionDisturbance == "なし" || string.IsNullOrEmpty(conductionDisturbance)) &&
                             (stTChange == "なし" || string.IsNullOrEmpty(stTChange)))
                    {
                        summary.AppendLine("【総合評価】明らかな異常を認めません");
                    }
                    else
                    {
                        summary.AppendLine("【総合評価】軽度の異常を認めます");
                    }
                }
            }
            catch (Exception ex)
            {
                summary.AppendLine($"レポート生成中にエラーが発生しました: {ex.Message}");
            }

            // テキストブロックに反映
            EcgSummaryTextBlock.Text = summary.ToString();
        }

        // レントゲンレポート要約生成
        private void GenerateXraySummary()
        {
            StringBuilder summary = new StringBuilder();

            try
            {
                // 各項目のデータを取得
                string ctr = CtrTextBox.Text.Trim();
                string lungField = GetComboBoxText(LungFieldComboBox);
                string cpAngle = GetComboBoxText(CpAngleComboBox);

                // CTR
                if (!string.IsNullOrEmpty(ctr))
                {
                    summary.AppendLine($"CTR: {ctr}%");

                    // CTRの評価
                    if (double.TryParse(ctr, out double ctrValue))
                    {
                        if (ctrValue > 50)
                        {
                            summary.AppendLine($"※心拡大あり（基準値: ≤50%）");

                            if (ctrValue > 60)
                            {
                                summary.AppendLine("※高度な心拡大を認めます");
                            }
                            else
                            {
                                summary.AppendLine("※軽度～中等度の心拡大を認めます");
                            }
                        }
                        else
                        {
                            summary.AppendLine("※心拡大なし");
                        }
                    }
                }

                // 肺野
                if (!string.IsNullOrEmpty(lungField))
                {
                    summary.AppendLine($"肺野: {lungField}");

                    // 肺野所見の評価
                    if (lungField == "not clear")
                    {
                        summary.AppendLine("※肺うっ血、肺水腫、肺炎、間質性肺疾患などの可能性があります");
                    }
                }

                // CP angle
                if (!string.IsNullOrEmpty(cpAngle))
                {
                    summary.AppendLine($"CP angle: {cpAngle}");

                    // CP angle所見の評価
                    if (cpAngle == "dull")
                    {
                        summary.AppendLine("※胸水貯留の可能性があります");
                    }
                }

                // 総合評価
                if (summary.Length > 0)
                {
                    summary.AppendLine();
                    bool hasSignificantAbnormality = false;

                    // 重要な異常の有無を判定
                    if ((double.TryParse(ctr, out double ctrVal) && ctrVal > 55) ||
                        lungField == "not clear" ||
                        cpAngle == "dull")
                    {
                        hasSignificantAbnormality = true;
                    }

                    // 総合所見を追加
                    if (hasSignificantAbnormality)
                    {
                        summary.AppendLine("【総合評価】臨床的に重要な異常を認めます");
                    }
                    else if ((double.TryParse(ctr, out double ctrNormal) && ctrNormal <= 50) &&
                             (lungField == "clear" || string.IsNullOrEmpty(lungField)) &&
                             (cpAngle == "sharp" || string.IsNullOrEmpty(cpAngle)))
                    {
                        summary.AppendLine("【総合評価】明らかな異常を認めません");
                    }
                    else
                    {
                        summary.AppendLine("【総合評価】軽度の異常を認めます");
                    }
                }
            }
            catch (Exception ex)
            {
                summary.AppendLine($"レポート生成中にエラーが発生しました: {ex.Message}");
            }

            // テキストブロックに反映
            XraySummaryTextBlock.Text = summary.ToString();
        }

        // ComboBoxからテキスト取得のヘルパーメソッド
        private string GetComboBoxText(ComboBox comboBox)
        {
            ComboBoxItem? selectedItem = comboBox.SelectedItem as ComboBoxItem;

            return selectedItem?.Content?.ToString() ?? "";
        }

        // 心電図・レントゲンデータの取得
        public EcgXrayData GetEcgXrayData()
        {
            return new EcgXrayData
            {
                // 心電図データ
                ECG_HR = HrTextBox.Text,
                ECG_Rhythm = GetComboBoxText(RhythmComboBox),
                ECG_Axis = GetComboBoxText(AxisComboBox),
                ECG_ConductionDisturbance = GetComboBoxText(ConductionDisturbanceComboBox),
                ECG_STTChange = GetComboBoxText(StTChangeComboBox),
                ECG_Comment = EcgCommentTextBox.Text,
                ECG_Summary = EcgSummaryTextBlock.Text,

                // レントゲンデータ
                XP_CTR = CtrTextBox.Text,
                XP_LungField = GetComboBoxText(LungFieldComboBox),
                XP_CPAngle = GetComboBoxText(CpAngleComboBox),
                XP_Summary = XraySummaryTextBlock.Text
            };
        }

        // データクリアメソッド
        public void ClearData()
        {
            // 心電図データクリア
            HrTextBox.Clear();
            RhythmComboBox.SelectedIndex = -1;
            AxisComboBox.SelectedIndex = -1;
            ConductionDisturbanceComboBox.SelectedIndex = -1;
            StTChangeComboBox.SelectedIndex = -1;
            EcgCommentTextBox.Clear();

            // レントゲンデータクリア
            CtrTextBox.Clear();
            LungFieldComboBox.SelectedIndex = -1;
            CpAngleComboBox.SelectedIndex = -1;

            // 要約クリア
            EcgSummaryTextBlock.Text = string.Empty;
            XraySummaryTextBlock.Text = string.Empty;
        }

        /// <summary>
        /// 心電図・レントゲンデータを設定
        /// </summary>
        /// <param name="patientData">設定する患者データ</param>
        public void SetEcgXrayData(PatientData patientData)
        {
            // 心電図データ
            HrTextBox.Text = patientData.ECG_HR;

            // リズム
            if (!string.IsNullOrEmpty(patientData.ECG_Rhythm))
            {
                foreach (ComboBoxItem item in RhythmComboBox.Items)
                {
                    if (item.Content.ToString() == patientData.ECG_Rhythm)
                    {
                        RhythmComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            // 電気軸
            if (!string.IsNullOrEmpty(patientData.ECG_Axis))
            {
                foreach (ComboBoxItem item in AxisComboBox.Items)
                {
                    if (item.Content.ToString() == patientData.ECG_Axis)
                    {
                        AxisComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            // 伝導障害
            if (!string.IsNullOrEmpty(patientData.ECG_ConductionDisturbance))
            {
                foreach (ComboBoxItem item in ConductionDisturbanceComboBox.Items)
                {
                    if (item.Content.ToString() == patientData.ECG_ConductionDisturbance)
                    {
                        ConductionDisturbanceComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            // ST-T変化
            if (!string.IsNullOrEmpty(patientData.ECG_STTChange))
            {
                foreach (ComboBoxItem item in StTChangeComboBox.Items)
                {
                    if (item.Content.ToString() == patientData.ECG_STTChange)
                    {
                        StTChangeComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            EcgCommentTextBox.Text = patientData.ECG_Comment;

            // レントゲンデータ
            CtrTextBox.Text = patientData.XP_CTR;

            // Lung field
            if (!string.IsNullOrEmpty(patientData.XP_LungField))
            {
                foreach (ComboBoxItem item in LungFieldComboBox.Items)
                {
                    if (item.Content.ToString() == patientData.XP_LungField)
                    {
                        LungFieldComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            // CP angle
            if (!string.IsNullOrEmpty(patientData.XP_CPAngle))
            {
                foreach (ComboBoxItem item in CpAngleComboBox.Items)
                {
                    if (item.Content.ToString() == patientData.XP_CPAngle)
                    {
                        CpAngleComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            // 要約を更新
            UpdateEcgSummary(null, null);
            UpdateXraySummary(null, null);
        }

    }

    // 心電図・レントゲンデータクラス
    public class EcgXrayData
    {
        // 心電図データ
        public string ECG_HR { get; set; } = "";
        public string ECG_Rhythm { get; set; } = "";
        public string ECG_Axis { get; set; } = "";
        public string ECG_ConductionDisturbance { get; set; } = "";
        public string ECG_STTChange { get; set; } = "";
        public string ECG_Comment { get; set; } = "";
        public string ECG_Summary { get; set; } = "";

        // レントゲンデータ
        public string XP_CTR { get; set; } = "";
        public string XP_LungField { get; set; } = "";
        public string XP_CPAngle { get; set; } = "";
        public string XP_Summary { get; set; } = "";
    }
}