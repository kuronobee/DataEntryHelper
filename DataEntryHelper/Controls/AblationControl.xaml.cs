// DataEntryHelper/Controls/AblationControl.xaml.cs
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DataEntryHelper.Controls
{
    /// <summary>
    /// AblationControl.xaml の相互作用ロジック
    /// </summary>
    public partial class AblationControl : UserControl
    {
        public AblationControl()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            // ComboBoxの初期値を設定
            MappingSystemComboBox.SelectedIndex = 0;
            MappingRhythmComboBox.SelectedIndex = 0;
            PacingSiteComboBox.SelectedIndex = 0;
            PreMapComboBox.SelectedIndex = 0;
            ResultComboBox.SelectedIndex = 0;
        }

        // アブレーションデータの取得
        public AblationData GetAblationData()
        {
            return new AblationData
            {
                MappingSystem = MappingSystemComboBox.Text,
                MappingRhythm = MappingRhythmComboBox.Text,
                PacingSite = PacingSiteComboBox.Text,
                PreMap = PreMapComboBox.Text,
                ProcedureCount = ProcedureCountTextBox.Text,
                ProcedurePVI = PVICheckBox.IsChecked ?? false,
                ProcedurePosteriorWallIsolation = PosteriorWallIsolationCheckBox.IsChecked ?? false,
                ProcedureCFAE_FAAM = CFAEFAAMCheckBox.IsChecked ?? false,
                ProcedureOther = ProcedureOtherTextBox.Text,
                Result = ResultComboBox.Text,
                LVAs = LVAsTextBox.Text,
                VGLA = VGLATextBox.Text,
                MaxVoltageAnterior = MaxVoltageAnteriorTextBox.Text,
                MaxVoltageSeptum = MaxVoltageSeptumTextBox.Text,
                MaxVoltageRoof = MaxVoltageRoofTextBox.Text,
                MaxVoltageInf = MaxVoltageInfTextBox.Text,
                MaxVoltagePost = MaxVoltagePostTextBox.Text,
                MaxVoltageLat = MaxVoltageLatTextBox.Text,
                MaxVoltageMean = MaxVoltageMeanTextBox.Text,
                AblationSummary = AblationSummaryTextBlock.Text
            };
        }

        // データクリアメソッド
        public void ClearData()
        {
            // ComboBoxを初期化
            MappingSystemComboBox.SelectedIndex = 0;
            MappingRhythmComboBox.SelectedIndex = 0;
            PacingSiteComboBox.SelectedIndex = 0;
            PreMapComboBox.SelectedIndex = 0;
            ResultComboBox.SelectedIndex = 0;

            // チェックボックスを初期化
            PVICheckBox.IsChecked = false;
            PosteriorWallIsolationCheckBox.IsChecked = false;
            CFAEFAAMCheckBox.IsChecked = false;

            // テキストボックスをクリア
            ProcedureCountTextBox.Clear();
            ProcedureOtherTextBox.Clear();
            LVAsTextBox.Clear();
            VGLATextBox.Clear();
            MaxVoltageAnteriorTextBox.Clear();
            MaxVoltageSeptumTextBox.Clear();
            MaxVoltageRoofTextBox.Clear();
            MaxVoltageInfTextBox.Clear();
            MaxVoltagePostTextBox.Clear();
            MaxVoltageLatTextBox.Clear();
            MaxVoltageMeanTextBox.Clear();

            // サマリーをクリア
            AblationSummaryTextBlock.Text = string.Empty;
        }

        // アブレーション情報が変更されたときのイベントハンドラ
        private void UpdateAblationSummary(object? sender, EventArgs? e)
        {
            GenerateAblationSummary();
        }

        // アブレーションレポート要約生成
        private void GenerateAblationSummary()
        {
            StringBuilder summary = new StringBuilder();

            try
            {
                // マッピングシステム
                if (!string.IsNullOrEmpty(MappingSystemComboBox.Text))
                {
                    summary.AppendLine($"マッピングシステム: {MappingSystemComboBox.Text}");
                }

                // マッピングリズム
                if (!string.IsNullOrEmpty(MappingRhythmComboBox.Text))
                {
                    summary.AppendLine($"マッピングリズム: {MappingRhythmComboBox.Text}");
                }

                // ペーシング部位
                if (!string.IsNullOrEmpty(PacingSiteComboBox.Text))
                {
                    summary.AppendLine($"ペーシング部位: {PacingSiteComboBox.Text}");
                }

                // プレマップ
                if (!string.IsNullOrEmpty(PreMapComboBox.Text))
                {
                    summary.AppendLine($"プレマップ: {PreMapComboBox.Text}");
                }

                // 施行回数
                if (!string.IsNullOrEmpty(ProcedureCountTextBox.Text))
                {
                    summary.AppendLine($"施行回数: {ProcedureCountTextBox.Text}回");
                }

                // 処置内容
                summary.AppendLine("実施処置:");
                if (PVICheckBox.IsChecked ?? false)
                    summary.AppendLine("- PVI");
                if (PosteriorWallIsolationCheckBox.IsChecked ?? false)
                    summary.AppendLine("- 後壁隔離");
                if (CFAEFAAMCheckBox.IsChecked ?? false)
                    summary.AppendLine("- CFAE/FAAM");
                if (!string.IsNullOrEmpty(ProcedureOtherTextBox.Text))
                    summary.AppendLine($"- その他: {ProcedureOtherTextBox.Text}");

                // 結果
                if (!string.IsNullOrEmpty(ResultComboBox.Text))
                {
                    summary.AppendLine($"結果: {ResultComboBox.Text}");
                }

                // 電位情報
                summary.AppendLine("\n【電位情報】");

                // LVAs
                if (!string.IsNullOrEmpty(LVAsTextBox.Text))
                {
                    summary.AppendLine($"LVAs(<0.5mV): {LVAsTextBox.Text} cm²");
                }

                // VGLA
                if (!string.IsNullOrEmpty(VGLATextBox.Text))
                {
                    summary.AppendLine($"VGLA(voltage global lt. atria): {VGLATextBox.Text} mV");
                }

                // 各部位の最大電位
                bool hasVoltageData = false;
                StringBuilder voltageData = new StringBuilder("LA Mapping 最大電位: ");

                if (!string.IsNullOrEmpty(MaxVoltageAnteriorTextBox.Text))
                {
                    voltageData.Append($"前壁 {MaxVoltageAnteriorTextBox.Text}mV, ");
                    hasVoltageData = true;
                }
                if (!string.IsNullOrEmpty(MaxVoltageSeptumTextBox.Text))
                {
                    voltageData.Append($"中隔 {MaxVoltageSeptumTextBox.Text}mV, ");
                    hasVoltageData = true;
                }
                if (!string.IsNullOrEmpty(MaxVoltageRoofTextBox.Text))
                {
                    voltageData.Append($"天蓋部 {MaxVoltageRoofTextBox.Text}mV, ");
                    hasVoltageData = true;
                }
                if (!string.IsNullOrEmpty(MaxVoltageInfTextBox.Text))
                {
                    voltageData.Append($"下壁 {MaxVoltageInfTextBox.Text}mV, ");
                    hasVoltageData = true;
                }
                if (!string.IsNullOrEmpty(MaxVoltagePostTextBox.Text))
                {
                    voltageData.Append($"後壁 {MaxVoltagePostTextBox.Text}mV, ");
                    hasVoltageData = true;
                }
                if (!string.IsNullOrEmpty(MaxVoltageLatTextBox.Text))
                {
                    voltageData.Append($"側壁 {MaxVoltageLatTextBox.Text}mV, ");
                    hasVoltageData = true;
                }

                if (hasVoltageData)
                {
                    // 最後のカンマとスペースを削除
                    voltageData.Length -= 2;
                    summary.AppendLine(voltageData.ToString());
                }

                // 平均電位
                if (!string.IsNullOrEmpty(MaxVoltageMeanTextBox.Text))
                {
                    summary.AppendLine($"LA Mapping 平均電位: {MaxVoltageMeanTextBox.Text}mV");
                }
            }
            catch (Exception ex)
            {
                summary.AppendLine($"要約生成中にエラーが発生しました: {ex.Message}");
            }

            AblationSummaryTextBlock.Text = summary.ToString();
        }

        /// <summary>
        /// アブレーションデータを設定
        /// </summary>
        /// <param name="patientData">設定する患者データ</param>
        public void SetAblationData(PatientData patientData)
        {
            // マッピングシステム
            if (!string.IsNullOrEmpty(patientData.MappingSystem))
            {
                foreach (ComboBoxItem item in MappingSystemComboBox.Items)
                {
                    if (item.Content.ToString() == patientData.MappingSystem)
                    {
                        MappingSystemComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            // マッピングリズム
            if (!string.IsNullOrEmpty(patientData.MappingRhythm))
            {
                foreach (ComboBoxItem item in MappingRhythmComboBox.Items)
                {
                    if (item.Content.ToString() == patientData.MappingRhythm)
                    {
                        MappingRhythmComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            // ペーシング部位
            if (!string.IsNullOrEmpty(patientData.PacingSite))
            {
                foreach (ComboBoxItem item in PacingSiteComboBox.Items)
                {
                    if (item.Content.ToString() == patientData.PacingSite)
                    {
                        PacingSiteComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            // プレマップ
            if (!string.IsNullOrEmpty(patientData.PreMap))
            {
                foreach (ComboBoxItem item in PreMapComboBox.Items)
                {
                    if (item.Content.ToString() == patientData.PreMap)
                    {
                        PreMapComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            // 施行回数
            ProcedureCountTextBox.Text = patientData.ProcedureCount;

            // 処置内容
            PVICheckBox.IsChecked = patientData.ProcedurePVI;
            PosteriorWallIsolationCheckBox.IsChecked = patientData.ProcedurePosteriorWallIsolation;
            CFAEFAAMCheckBox.IsChecked = patientData.ProcedureCFAE_FAAM;
            ProcedureOtherTextBox.Text = patientData.ProcedureOther;

            // 結果
            if (!string.IsNullOrEmpty(patientData.Result))
            {
                foreach (ComboBoxItem item in ResultComboBox.Items)
                {
                    if (item.Content.ToString() == patientData.Result)
                    {
                        ResultComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            // 電位情報
            LVAsTextBox.Text = patientData.LVAs;
            VGLATextBox.Text = patientData.VGLA;

            // 最大電位
            MaxVoltageAnteriorTextBox.Text = patientData.MaxVoltageAnterior;
            MaxVoltageSeptumTextBox.Text = patientData.MaxVoltageSeptum;
            MaxVoltageRoofTextBox.Text = patientData.MaxVoltageRoof;
            MaxVoltageInfTextBox.Text = patientData.MaxVoltageInf;
            MaxVoltagePostTextBox.Text = patientData.MaxVoltagePost;
            MaxVoltageLatTextBox.Text = patientData.MaxVoltageLat;
            MaxVoltageMeanTextBox.Text = patientData.MaxVoltageMean;

            // 要約を更新
            UpdateAblationSummary(null, null);
        }

    }

    // アブレーションデータクラス
    public class AblationData
    {
        public string MappingSystem { get; set; } = "";
        public string MappingRhythm { get; set; } = "";
        public string PacingSite { get; set; } = "";
        public string PreMap { get; set; } = "";
        public string ProcedureCount { get; set; } = "";
        public bool ProcedurePVI { get; set; } = false;
        public bool ProcedurePosteriorWallIsolation { get; set; } = false;
        public bool ProcedureCFAE_FAAM { get; set; } = false;
        public string ProcedureOther { get; set; } = "";
        public string Result { get; set; } = "";
        public string LVAs { get; set; } = "";
        public string VGLA { get; set; } = "";
        public string MaxVoltageAnterior { get; set; } = "";
        public string MaxVoltageSeptum { get; set; } = "";
        public string MaxVoltageRoof { get; set; } = "";
        public string MaxVoltageInf { get; set; } = "";
        public string MaxVoltagePost { get; set; } = "";
        public string MaxVoltageLat { get; set; } = "";
        public string MaxVoltageMean { get; set; } = "";
        public string AblationSummary { get; set; } = "";
    }
}