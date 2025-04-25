using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DataEntryHelper.Controls
{
    /// <summary>
    /// MedicationControl.xaml の相互作用ロジック
    /// </summary>
    public partial class MedicationControl : UserControl
    {
        public MedicationControl()
        {
            InitializeComponent();
        }

        // 薬物療法情報が変更された時のイベントハンドラ
        private void UpdateMedicationSummary(object sender, EventArgs e)
        {
            GenerateMedicationSummary();
        }

        // 薬物療法要約の生成
        private void GenerateMedicationSummary()
        {
            StringBuilder summary = new StringBuilder();

            try
            {
                // βブロッカー
                if (BetaBlockerComboBox.Text == "あり")
                {
                    summary.AppendLine("【βブロッカー】");

                    // 薬剤1
                    if (!string.IsNullOrEmpty(BetaBlockerName1ComboBox.Text))
                    {
                        summary.Append($"・{BetaBlockerName1ComboBox.Text}");
                        if (!string.IsNullOrEmpty(BetaBlockerDose1TextBox.Text))
                            summary.Append($" {BetaBlockerDose1TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤2
                    if (!string.IsNullOrEmpty(BetaBlockerName2ComboBox.Text))
                    {
                        summary.Append($"・{BetaBlockerName2ComboBox.Text}");
                        if (!string.IsNullOrEmpty(BetaBlockerDose2TextBox.Text))
                            summary.Append($" {BetaBlockerDose2TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤3
                    if (!string.IsNullOrEmpty(BetaBlockerName3ComboBox.Text))
                    {
                        summary.Append($"・{BetaBlockerName3ComboBox.Text}");
                        if (!string.IsNullOrEmpty(BetaBlockerDose3TextBox.Text))
                            summary.Append($" {BetaBlockerDose3TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    summary.AppendLine();
                }

                // CCB
                if (CCBComboBox.Text == "あり")
                {
                    summary.AppendLine("【カルシウム拮抗薬】");

                    // 薬剤1
                    if (!string.IsNullOrEmpty(CCBName1ComboBox.Text))
                    {
                        summary.Append($"・{CCBName1ComboBox.Text}");
                        if (!string.IsNullOrEmpty(CCBDose1TextBox.Text))
                            summary.Append($" {CCBDose1TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤2
                    if (!string.IsNullOrEmpty(CCBName2ComboBox.Text))
                    {
                        summary.Append($"・{CCBName2ComboBox.Text}");
                        if (!string.IsNullOrEmpty(CCBDose2TextBox.Text))
                            summary.Append($" {CCBDose2TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤3
                    if (!string.IsNullOrEmpty(CCBName3ComboBox.Text))
                    {
                        summary.Append($"・{CCBName3ComboBox.Text}");
                        if (!string.IsNullOrEmpty(CCBDose3TextBox.Text))
                            summary.Append($" {CCBDose3TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    summary.AppendLine();
                }

                // 抗不整脈薬
                if (AntiArrhythmicDrugComboBox.Text == "あり")
                {
                    summary.AppendLine("【抗不整脈薬】");

                    // 薬剤1
                    if (!string.IsNullOrEmpty(AntiArrhythmicDrugName1ComboBox.Text))
                    {
                        summary.Append($"・{AntiArrhythmicDrugName1ComboBox.Text}");
                        if (!string.IsNullOrEmpty(AntiArrhythmicDrugDose1TextBox.Text))
                            summary.Append($" {AntiArrhythmicDrugDose1TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤2
                    if (!string.IsNullOrEmpty(AntiArrhythmicDrugName2ComboBox.Text))
                    {
                        summary.Append($"・{AntiArrhythmicDrugName2ComboBox.Text}");
                        if (!string.IsNullOrEmpty(AntiArrhythmicDrugDose2TextBox.Text))
                            summary.Append($" {AntiArrhythmicDrugDose2TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤3
                    if (!string.IsNullOrEmpty(AntiArrhythmicDrugName3ComboBox.Text))
                    {
                        summary.Append($"・{AntiArrhythmicDrugName3ComboBox.Text}");
                        if (!string.IsNullOrEmpty(AntiArrhythmicDrugDose3TextBox.Text))
                            summary.Append($" {AntiArrhythmicDrugDose3TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    summary.AppendLine();
                }

                // DOAC
                if (DOACComboBox.Text == "あり")
                {
                    summary.AppendLine("【DOAC】");

                    // 薬剤1
                    if (!string.IsNullOrEmpty(DOACName1ComboBox.Text))
                    {
                        summary.Append($"・{DOACName1ComboBox.Text}");
                        if (!string.IsNullOrEmpty(DOACDose1TextBox.Text))
                            summary.Append($" {DOACDose1TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤2
                    if (!string.IsNullOrEmpty(DOACName2ComboBox.Text))
                    {
                        summary.Append($"・{DOACName2ComboBox.Text}");
                        if (!string.IsNullOrEmpty(DOACDose2TextBox.Text))
                            summary.Append($" {DOACDose2TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤3
                    if (!string.IsNullOrEmpty(DOACName3ComboBox.Text))
                    {
                        summary.Append($"・{DOACName3ComboBox.Text}");
                        if (!string.IsNullOrEmpty(DOACDose3TextBox.Text))
                            summary.Append($" {DOACDose3TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    summary.AppendLine();
                }

                // VKA
                if (VKAComboBox.Text == "あり")
                {
                    summary.AppendLine("【VKA】");
                    summary.Append("・ワルファリン");
                    if (!string.IsNullOrEmpty(VKADoseTextBox.Text))
                        summary.Append($" {VKADoseTextBox.Text}mg");
                    summary.AppendLine();
                    summary.AppendLine();
                }

                // スタチン
                if (StatinComboBox.Text == "あり")
                {
                    summary.AppendLine("【スタチン】");

                    // 薬剤1
                    if (!string.IsNullOrEmpty(StatinName1ComboBox.Text))
                    {
                        summary.Append($"・{StatinName1ComboBox.Text}");
                        if (!string.IsNullOrEmpty(StatinDose1TextBox.Text))
                            summary.Append($" {StatinDose1TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤2
                    if (!string.IsNullOrEmpty(StatinName2ComboBox.Text))
                    {
                        summary.Append($"・{StatinName2ComboBox.Text}");
                        if (!string.IsNullOrEmpty(StatinDose2TextBox.Text))
                            summary.Append($" {StatinDose2TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤3
                    if (!string.IsNullOrEmpty(StatinName3ComboBox.Text))
                    {
                        summary.Append($"・{StatinName3ComboBox.Text}");
                        if (!string.IsNullOrEmpty(StatinDose3TextBox.Text))
                            summary.Append($" {StatinDose3TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    summary.AppendLine();
                }

                // SGLT2i
                if (SGLT2iComboBox.Text == "あり")
                {
                    summary.AppendLine("【SGLT2阻害薬】");

                    // 薬剤1
                    if (!string.IsNullOrEmpty(SGLT2iName1ComboBox.Text))
                    {
                        summary.Append($"・{SGLT2iName1ComboBox.Text}");
                        if (!string.IsNullOrEmpty(SGLT2iDose1TextBox.Text))
                            summary.Append($" {SGLT2iDose1TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤2
                    if (!string.IsNullOrEmpty(SGLT2iName2ComboBox.Text))
                    {
                        summary.Append($"・{SGLT2iName2ComboBox.Text}");
                        if (!string.IsNullOrEmpty(SGLT2iDose2TextBox.Text))
                            summary.Append($" {SGLT2iDose2TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤3
                    if (!string.IsNullOrEmpty(SGLT2iName3ComboBox.Text))
                    {
                        summary.Append($"・{SGLT2iName3ComboBox.Text}");
                        if (!string.IsNullOrEmpty(SGLT2iDose3TextBox.Text))
                            summary.Append($" {SGLT2iDose3TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    summary.AppendLine();
                }

                // ACE/ARB/ARNi
                if (RAASComboBox.Text == "あり")
                {
                    summary.AppendLine("【ACE/ARB/ARNi】");

                    // 薬剤1
                    if (!string.IsNullOrEmpty(RAASName1ComboBox.Text))
                    {
                        summary.Append($"・{RAASName1ComboBox.Text}");
                        if (!string.IsNullOrEmpty(RAASDose1TextBox.Text))
                            summary.Append($" {RAASDose1TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤2
                    if (!string.IsNullOrEmpty(RAASName2ComboBox.Text))
                    {
                        summary.Append($"・{RAASName2ComboBox.Text}");
                        if (!string.IsNullOrEmpty(RAASDose2TextBox.Text))
                            summary.Append($" {RAASDose2TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤3
                    if (!string.IsNullOrEmpty(RAASName3ComboBox.Text))
                    {
                        summary.Append($"・{RAASName3ComboBox.Text}");
                        if (!string.IsNullOrEmpty(RAASDose3TextBox.Text))
                            summary.Append($" {RAASDose3TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    summary.AppendLine();
                }

                // MRA
                if (MRAComboBox.Text == "あり")
                {
                    summary.AppendLine("【MRA】");

                    // 薬剤1
                    if (!string.IsNullOrEmpty(MRAName1ComboBox.Text))
                    {
                        summary.Append($"・{MRAName1ComboBox.Text}");
                        if (!string.IsNullOrEmpty(MRADose1TextBox.Text))
                            summary.Append($" {MRADose1TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤2
                    if (!string.IsNullOrEmpty(MRAName2ComboBox.Text))
                    {
                        summary.Append($"・{MRAName2ComboBox.Text}");
                        if (!string.IsNullOrEmpty(MRADose2TextBox.Text))
                            summary.Append($" {MRADose2TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤3
                    if (!string.IsNullOrEmpty(MRAName3ComboBox.Text))
                    {
                        summary.Append($"・{MRAName3ComboBox.Text}");
                        if (!string.IsNullOrEmpty(MRADose3TextBox.Text))
                            summary.Append($" {MRADose3TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    summary.AppendLine();
                }

                // 利尿薬
                if (DiureticsComboBox.Text == "あり")
                {
                    summary.AppendLine("【利尿薬】");

                    // 薬剤1
                    if (!string.IsNullOrEmpty(DiureticsName1ComboBox.Text))
                    {
                        summary.Append($"・{DiureticsName1ComboBox.Text}");
                        if (!string.IsNullOrEmpty(DiureticsDose1TextBox.Text))
                            summary.Append($" {DiureticsDose1TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤2
                    if (!string.IsNullOrEmpty(DiureticsName2ComboBox.Text))
                    {
                        summary.Append($"・{DiureticsName2ComboBox.Text}");
                        if (!string.IsNullOrEmpty(DiureticsDose2TextBox.Text))
                            summary.Append($" {DiureticsDose2TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤3
                    if (!string.IsNullOrEmpty(DiureticsName3ComboBox.Text))
                    {
                        summary.Append($"・{DiureticsName3ComboBox.Text}");
                        if (!string.IsNullOrEmpty(DiureticsDose3TextBox.Text))
                            summary.Append($" {DiureticsDose3TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    summary.AppendLine();
                }

                // 抗血小板薬
                if (AntiplateletAgentComboBox.Text == "あり")
                {
                    summary.AppendLine("【抗血小板薬】");

                    // 薬剤1
                    if (!string.IsNullOrEmpty(AntiplateletAgentName1ComboBox.Text))
                    {
                        summary.Append($"・{AntiplateletAgentName1ComboBox.Text}");
                        if (!string.IsNullOrEmpty(AntiplateletAgentDose1TextBox.Text))
                            summary.Append($" {AntiplateletAgentDose1TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤2
                    if (!string.IsNullOrEmpty(AntiplateletAgentName2ComboBox.Text))
                    {
                        summary.Append($"・{AntiplateletAgentName2ComboBox.Text}");
                        if (!string.IsNullOrEmpty(AntiplateletAgentDose2TextBox.Text))
                            summary.Append($" {AntiplateletAgentDose2TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    // 薬剤3
                    if (!string.IsNullOrEmpty(AntiplateletAgentName3ComboBox.Text))
                    {
                        summary.Append($"・{AntiplateletAgentName3ComboBox.Text}");
                        if (!string.IsNullOrEmpty(AntiplateletAgentDose3TextBox.Text))
                            summary.Append($" {AntiplateletAgentDose3TextBox.Text}mg");
                        summary.AppendLine();
                    }

                    summary.AppendLine();
                }

                // その他の薬剤
                if (!string.IsNullOrEmpty(OtherMedicationsTextBox.Text))
                {
                    summary.AppendLine("【その他】");
                    summary.AppendLine(OtherMedicationsTextBox.Text);
                }

                // 薬物療法なし
                if (summary.Length == 0)
                {
                    summary.AppendLine("薬物療法なし");
                }
            }
            catch (Exception ex)
            {
                summary.AppendLine($"要約生成中にエラーが発生しました: {ex.Message}");
            }

            // 要約をテキストブロックに反映
            MedicationSummaryTextBlock.Text = summary.ToString();
        }

        // 薬物療法データの取得
        public MedicationData GetMedicationData()
        {
            return new MedicationData
            {
                // βブロッカー
                BetaBlocker = BetaBlockerComboBox.Text,
                BetaBlockerName1 = BetaBlockerName1ComboBox.Text,
                BetaBlockerDose1 = BetaBlockerDose1TextBox.Text,
                BetaBlockerName2 = BetaBlockerName2ComboBox.Text,
                BetaBlockerDose2 = BetaBlockerDose2TextBox.Text,
                BetaBlockerName3 = BetaBlockerName3ComboBox.Text,
                BetaBlockerDose3 = BetaBlockerDose3TextBox.Text,

                // CCB
                CCB = CCBComboBox.Text,
                CCBName1 = CCBName1ComboBox.Text,
                CCBDose1 = CCBDose1TextBox.Text,
                CCBName2 = CCBName2ComboBox.Text,
                CCBDose2 = CCBDose2TextBox.Text,
                CCBName3 = CCBName3ComboBox.Text,
                CCBDose3 = CCBDose3TextBox.Text,

                // 抗不整脈薬
                AntiArrhythmicDrug = AntiArrhythmicDrugComboBox.Text,
                AntiArrhythmicDrugName1 = AntiArrhythmicDrugName1ComboBox.Text,
                AntiArrhythmicDrugDose1 = AntiArrhythmicDrugDose1TextBox.Text,
                AntiArrhythmicDrugName2 = AntiArrhythmicDrugName2ComboBox.Text,
                AntiArrhythmicDrugDose2 = AntiArrhythmicDrugDose2TextBox.Text,
                AntiArrhythmicDrugName3 = AntiArrhythmicDrugName3ComboBox.Text,
                AntiArrhythmicDrugDose3 = AntiArrhythmicDrugDose3TextBox.Text,

                // DOAC
                DOAC = DOACComboBox.Text,
                DOACName1 = DOACName1ComboBox.Text,
                DOACDose1 = DOACDose1TextBox.Text,
                DOACName2 = DOACName2ComboBox.Text,
                DOACDose2 = DOACDose2TextBox.Text,
                DOACName3 = DOACName3ComboBox.Text,
                DOACDose3 = DOACDose3TextBox.Text,

                // VKA
                VKA = VKAComboBox.Text,
                VKADose = VKADoseTextBox.Text,

                // スタチン
                Statin = StatinComboBox.Text,
                StatinName1 = StatinName1ComboBox.Text,
                StatinDose1 = StatinDose1TextBox.Text,
                StatinName2 = StatinName2ComboBox.Text,
                StatinDose2 = StatinDose2TextBox.Text,
                StatinName3 = StatinName3ComboBox.Text,
                StatinDose3 = StatinDose3TextBox.Text,

                // SGLT2i
                SGLT2i = SGLT2iComboBox.Text,
                SGLT2iName1 = SGLT2iName1ComboBox.Text,
                SGLT2iDose1 = SGLT2iDose1TextBox.Text,
                SGLT2iName2 = SGLT2iName2ComboBox.Text,
                SGLT2iDose2 = SGLT2iDose2TextBox.Text,
                SGLT2iName3 = SGLT2iName3ComboBox.Text,
                SGLT2iDose3 = SGLT2iDose3TextBox.Text,

                // ACE/ARB/ARNi
                RAAS = RAASComboBox.Text,
                RAASName1 = RAASName1ComboBox.Text,
                RAASDose1 = RAASDose1TextBox.Text,
                RAASName2 = RAASName2ComboBox.Text,
                RAASDose2 = RAASDose2TextBox.Text,
                RAASName3 = RAASName3ComboBox.Text,
                RAASDose3 = RAASDose3TextBox.Text,

                // MRA
                MRA = MRAComboBox.Text,
                MRAName1 = MRAName1ComboBox.Text,
                MRADose1 = MRADose1TextBox.Text,
                MRAName2 = MRAName2ComboBox.Text,
                MRADose2 = MRADose2TextBox.Text,
                MRAName3 = MRAName3ComboBox.Text,
                MRADose3 = MRADose3TextBox.Text,

                // 利尿薬
                Diuretics = DiureticsComboBox.Text,
                DiureticsName1 = DiureticsName1ComboBox.Text,
                DiureticsDose1 = DiureticsDose1TextBox.Text,
                DiureticsName2 = DiureticsName2ComboBox.Text,
                DiureticsDose2 = DiureticsDose2TextBox.Text,
                DiureticsName3 = DiureticsName3ComboBox.Text,
                DiureticsDose3 = DiureticsDose3TextBox.Text,

                // 抗血小板薬
                AntiplateletAgent = AntiplateletAgentComboBox.Text,
                AntiplateletAgentName1 = AntiplateletAgentName1ComboBox.Text,
                AntiplateletAgentDose1 = AntiplateletAgentDose1TextBox.Text,
                AntiplateletAgentName2 = AntiplateletAgentName2ComboBox.Text,
                AntiplateletAgentDose2 = AntiplateletAgentDose2TextBox.Text,
                AntiplateletAgentName3 = AntiplateletAgentName3ComboBox.Text,
                AntiplateletAgentDose3 = AntiplateletAgentDose3TextBox.Text,

                // その他の薬剤
                OtherMedications = OtherMedicationsTextBox.Text,

                // 薬物療法要約
                MedicationSummary = MedicationSummaryTextBlock.Text
            };
        }

        // データクリアメソッド
        public void ClearData()
        {
            // βブロッカー
            BetaBlockerComboBox.SelectedIndex = -1;
            BetaBlockerName1ComboBox.SelectedIndex = -1;
            BetaBlockerDose1TextBox.Clear();
            BetaBlockerName2ComboBox.SelectedIndex = -1;
            BetaBlockerDose2TextBox.Clear();
            BetaBlockerName3ComboBox.SelectedIndex = -1;
            BetaBlockerDose3TextBox.Clear();

            // CCB
            CCBComboBox.SelectedIndex = -1;
            CCBName1ComboBox.SelectedIndex = -1;
            CCBDose1TextBox.Clear();
            CCBName2ComboBox.SelectedIndex = -1;
            CCBDose2TextBox.Clear();
            CCBName3ComboBox.SelectedIndex = -1;
            CCBDose3TextBox.Clear();

            // 抗不整脈薬
            AntiArrhythmicDrugComboBox.SelectedIndex = -1;
            AntiArrhythmicDrugName1ComboBox.SelectedIndex = -1;
            AntiArrhythmicDrugDose1TextBox.Clear();
            AntiArrhythmicDrugName2ComboBox.SelectedIndex = -1;
            AntiArrhythmicDrugDose2TextBox.Clear();
            AntiArrhythmicDrugName3ComboBox.SelectedIndex = -1;
            AntiArrhythmicDrugDose3TextBox.Clear();

            // DOAC
            DOACComboBox.SelectedIndex = -1;
            DOACName1ComboBox.SelectedIndex = -1;
            DOACDose1TextBox.Clear();
            DOACName2ComboBox.SelectedIndex = -1;
            DOACDose2TextBox.Clear();
            DOACName3ComboBox.SelectedIndex = -1;
            DOACDose3TextBox.Clear();

            // VKA
            VKAComboBox.SelectedIndex = -1;
            VKADoseTextBox.Clear();

            // スタチン
            StatinComboBox.SelectedIndex = -1;
            StatinName1ComboBox.SelectedIndex = -1;
            StatinDose1TextBox.Clear();
            StatinName2ComboBox.SelectedIndex = -1;
            StatinDose2TextBox.Clear();
            StatinName3ComboBox.SelectedIndex = -1;
            StatinDose3TextBox.Clear();

            // SGLT2i
            SGLT2iComboBox.SelectedIndex = -1;
            SGLT2iName1ComboBox.SelectedIndex = -1;
            SGLT2iDose1TextBox.Clear();
            SGLT2iName2ComboBox.SelectedIndex = -1;
            SGLT2iDose2TextBox.Clear();
            SGLT2iName3ComboBox.SelectedIndex = -1;
            SGLT2iDose3TextBox.Clear();

            // ACE/ARB/ARNi
            RAASComboBox.SelectedIndex = -1;
            RAASName1ComboBox.SelectedIndex = -1;
            RAASDose1TextBox.Clear();
            RAASName2ComboBox.SelectedIndex = -1;
            RAASDose2TextBox.Clear();
            RAASName3ComboBox.SelectedIndex = -1;
            RAASDose3TextBox.Clear();

            // MRA
            MRAComboBox.SelectedIndex = -1;
            MRAName1ComboBox.SelectedIndex = -1;
            MRADose1TextBox.Clear();
            MRAName2ComboBox.SelectedIndex = -1;
            MRADose2TextBox.Clear();
            MRAName3ComboBox.SelectedIndex = -1;
            MRADose3TextBox.Clear();

            // 利尿薬
            DiureticsComboBox.SelectedIndex = -1;
            DiureticsName1ComboBox.SelectedIndex = -1;
            DiureticsDose1TextBox.Clear();
            DiureticsName2ComboBox.SelectedIndex = -1;
            DiureticsDose2TextBox.Clear();
            DiureticsName3ComboBox.SelectedIndex = -1;
            DiureticsDose3TextBox.Clear();

            // 抗血小板薬
            AntiplateletAgentComboBox.SelectedIndex = -1;
            AntiplateletAgentName1ComboBox.SelectedIndex = -1;
            AntiplateletAgentDose1TextBox.Clear();
            AntiplateletAgentName2ComboBox.SelectedIndex = -1;
            AntiplateletAgentDose2TextBox.Clear();
            AntiplateletAgentName3ComboBox.SelectedIndex = -1;
            AntiplateletAgentDose3TextBox.Clear();

            // その他の薬剤
            OtherMedicationsTextBox.Clear();

            // 薬物療法要約
            MedicationSummaryTextBlock.Text = string.Empty;
        }
    }

    // 薬物療法データクラス
    public class MedicationData
    {
        // βブロッカー
        public string BetaBlocker { get; set; } = "";
        public string BetaBlockerName1 { get; set; } = "";
        public string BetaBlockerDose1 { get; set; } = "";
        public string BetaBlockerName2 { get; set; } = "";
        public string BetaBlockerDose2 { get; set; } = "";
        public string BetaBlockerName3 { get; set; } = "";
        public string BetaBlockerDose3 { get; set; } = "";

        // CCB
        public string CCB { get; set; } = "";
        public string CCBName1 { get; set; } = "";
        public string CCBDose1 { get; set; } = "";
        public string CCBName2 { get; set; } = "";
        public string CCBDose2 { get; set; } = "";
        public string CCBName3 { get; set; } = "";
        public string CCBDose3 { get; set; } = "";

        // 抗不整脈薬
        public string AntiArrhythmicDrug { get; set; } = "";
        public string AntiArrhythmicDrugName1 { get; set; } = "";
        public string AntiArrhythmicDrugDose1 { get; set; } = "";
        public string AntiArrhythmicDrugName2 { get; set; } = "";
        public string AntiArrhythmicDrugDose2 { get; set; } = "";
        public string AntiArrhythmicDrugName3 { get; set; } = "";
        public string AntiArrhythmicDrugDose3 { get; set; } = "";

        // DOAC
        public string DOAC { get; set; } = "";
        public string DOACName1 { get; set; } = "";
        public string DOACDose1 { get; set; } = "";
        public string DOACName2 { get; set; } = "";
        public string DOACDose2 { get; set; } = "";
        public string DOACName3 { get; set; } = "";
        public string DOACDose3 { get; set; } = "";

        // VKA
        public string VKA { get; set; } = "";
        public string VKADose { get; set; } = "";

        // スタチン
        public string Statin { get; set; } = "";
        public string StatinName1 { get; set; } = "";
        public string StatinDose1 { get; set; } = "";
        public string StatinName2 { get; set; } = "";
        public string StatinDose2 { get; set; } = "";
        public string StatinName3 { get; set; } = "";
        public string StatinDose3 { get; set; } = "";

        // SGLT2i
        public string SGLT2i { get; set; } = "";
        public string SGLT2iName1 { get; set; } = "";
        public string SGLT2iDose1 { get; set; } = "";
        public string SGLT2iName2 { get; set; } = "";
        public string SGLT2iDose2 { get; set; } = "";
        public string SGLT2iName3 { get; set; } = "";
        public string SGLT2iDose3 { get; set; } = "";

        // ACE/ARB/ARNi
        public string RAAS { get; set; } = "";
        public string RAASName1 { get; set; } = "";
        public string RAASDose1 { get; set; } = "";
        public string RAASName2 { get; set; } = "";
        public string RAASDose2 { get; set; } = "";
        public string RAASName3 { get; set; } = "";
        public string RAASDose3 { get; set; } = "";

        // MRA
        public string MRA { get; set; } = "";
        public string MRAName1 { get; set; } = "";
        public string MRADose1 { get; set; } = "";
        public string MRAName2 { get; set; } = "";
        public string MRADose2 { get; set; } = "";
        public string MRAName3 { get; set; } = "";
        public string MRADose3 { get; set; } = "";

        // 利尿薬
        public string Diuretics { get; set; } = "";
        public string DiureticsName1 { get; set; } = "";
        public string DiureticsDose1 { get; set; } = "";
        public string DiureticsName2 { get; set; } = "";
        public string DiureticsDose2 { get; set; } = "";
        public string DiureticsName3 { get; set; } = "";
        public string DiureticsDose3 { get; set; } = "";

        // 抗血小板薬
        public string AntiplateletAgent { get; set; } = "";
        public string AntiplateletAgentName1 { get; set; } = "";
        public string AntiplateletAgentDose1 { get; set; } = "";
        public string AntiplateletAgentName2 { get; set; } = "";
        public string AntiplateletAgentDose2 { get; set; } = "";
        public string AntiplateletAgentName3 { get; set; } = "";
        public string AntiplateletAgentDose3 { get; set; } = "";

        // その他の薬剤
        public string OtherMedications { get; set; } = "";

        // 薬物療法要約
        public string MedicationSummary { get; set; } = "";
    }
}