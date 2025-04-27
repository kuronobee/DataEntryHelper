using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DataEntryHelper.Controls
{
    /// <summary>
    /// SamplingControl.xaml の相互作用ロジック
    /// </summary>
    public partial class SamplingControl : UserControl
    {
        // BSA（体表面積）の値を保持するプロパティ
        private double _bsaValue = 0;

        public SamplingControl()
        {
            InitializeComponent();
        }

        // 入力テキストボックスの値が変更されたときのイベントハンドラ
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateValues();
            UpdateSamplingSummary();
        }

        // 患者の体表面積を設定するメソッド（MainWindowから呼び出される）
        public void SetBSA(string bsaString)
        {
            if (double.TryParse(bsaString, out double bsa))
            {
                _bsaValue = bsa;
                CalculateValues(); // BSAが更新されたら計算値を更新
                UpdateSamplingSummary();
            }
        }

        // 各種計算値を算出するメソッド
        private void CalculateValues()
        {
            try
            {
                // LA-m/t ADM比の計算
                if (double.TryParse(LAMatureAdmTextBox.Text, out double laMatureAdm) &&
                    double.TryParse(LATotalAdmTextBox.Text, out double laTotalAdm) &&
                    laTotalAdm > 0)
                {
                    double laMtRatio = laMatureAdm / laTotalAdm;
                    LAMTAdmRatioTextBox.Text = laMtRatio.ToString("F3");
                }
                else
                {
                    LAMTAdmRatioTextBox.Text = string.Empty;
                }

                // CS-m/t ADM比の計算
                if (double.TryParse(CSMatureAdmTextBox.Text, out double csMatureAdm) &&
                    double.TryParse(CSTotalAdmTextBox.Text, out double csTotalAdm) &&
                    csTotalAdm > 0)
                {
                    double csMtRatio = csMatureAdm / csTotalAdm;
                    CSMTAdmRatioTextBox.Text = csMtRatio.ToString("F3");
                }
                else
                {
                    CSMTAdmRatioTextBox.Text = string.Empty;
                }

                // Δt-ADM (CS - FV)の計算
                if (double.TryParse(CSTotalAdmTextBox.Text, out double csTotalAdmValue) &&
                    double.TryParse(FVTotalAdmTextBox.Text, out double fvTotalAdmValue))
                {
                    double deltaTotalAdm2 = csTotalAdmValue - fvTotalAdmValue;
                    DeltaTotalAdmTextBox.Text = deltaTotalAdm2.ToString("F2");
                }
                else
                {
                    DeltaTotalAdmTextBox.Text = string.Empty;
                }

                // Δm-ADM (CS - FV)の計算
                if (double.TryParse(CSMatureAdmTextBox.Text, out double csMatureAdmValue) &&
                    double.TryParse(FVMatureAdmTextBox.Text, out double fvMatureAdmValue))
                {
                    double deltaMatureAdm = csMatureAdmValue - fvMatureAdmValue;
                    DeltaMatureAdmTextBox.Text = deltaMatureAdm.ToString("F2");
                }
                else
                {
                    DeltaMatureAdmTextBox.Text = string.Empty;
                }

                // ΔATX (CS - FV)の計算
                if (double.TryParse(CSAtxTextBox.Text, out double csAtxValue) &&
                    double.TryParse(FVAtxTextBox.Text, out double fvAtxValue))
                {
                    double deltaAtx = csAtxValue - fvAtxValue;
                    DeltaAtxTextBox.Text = deltaAtx.ToString("F2");
                }
                else
                {
                    DeltaAtxTextBox.Text = string.Empty;
                }

                // CS-LA Δt-ADMの計算
                if (double.TryParse(CSTotalAdmTextBox.Text, out double csTotalValue) &&
                    double.TryParse(LATotalAdmTextBox.Text, out double laTotalValue))
                {
                    double csLaDeltaTotal = csTotalValue - laTotalValue;
                    CSLADeltaTotalAdmTextBox.Text = csLaDeltaTotal.ToString("F2");
                }
                else
                {
                    CSLADeltaTotalAdmTextBox.Text = string.Empty;
                }

                // CS-LA Δm-ADMの計算
                if (double.TryParse(CSMatureAdmTextBox.Text, out double csMatureValue) &&
                    double.TryParse(LAMatureAdmTextBox.Text, out double laMatureValue))
                {
                    double csLaDeltaMature = csMatureValue - laMatureValue;
                    CSLADeltaMatureAdmTextBox.Text = csLaDeltaMature.ToString("F2");
                }
                else
                {
                    CSLADeltaMatureAdmTextBox.Text = string.Empty;
                }

                // CS-LA ΔATXの計算
                if (double.TryParse(CSAtxTextBox.Text, out double csAtx) &&
                    double.TryParse(LAAtxTextBox.Text, out double laAtx))
                {
                    double csLaDeltaAtx = csAtx - laAtx;
                    CSLADeltaAtxTextBox.Text = csLaDeltaAtx.ToString("F2");
                }
                else
                {
                    CSLADeltaAtxTextBox.Text = string.Empty;
                }

                // Δt ADM/BSAの計算
                if (double.TryParse(DeltaTotalAdmTextBox.Text, out double deltaTotalAdm) &&
                    _bsaValue > 0)
                {
                    double deltaTotalAdmBsa = deltaTotalAdm / _bsaValue;
                    DeltaTotalAdmBSATextBox.Text = deltaTotalAdmBsa.ToString("F2");
                }
                else
                {
                    DeltaTotalAdmBSATextBox.Text = string.Empty;
                }

                // t-ADM/BSAの計算 (LA値使用)
                if (double.TryParse(LATotalAdmTextBox.Text, out double laTotalAdmValue) &&
                    _bsaValue > 0)
                {
                    double totalAdmBsa = laTotalAdmValue / _bsaValue;
                    TotalAdmBSATextBox.Text = totalAdmBsa.ToString("F2");
                }
                else
                {
                    TotalAdmBSATextBox.Text = string.Empty;
                }

                // Δt/t ADMの計算
                if (double.TryParse(DeltaTotalAdmTextBox.Text, out double deltaTotal) &&
                    double.TryParse(FVTotalAdmTextBox.Text, out double fvTotal) &&
                    fvTotal > 0)
                {
                    double deltaTotalRatio = deltaTotal / fvTotal;
                    DeltaTotalRatioTextBox.Text = deltaTotalRatio.ToString("F3");
                }
                else
                {
                    DeltaTotalRatioTextBox.Text = string.Empty;
                }

                // Δm/m ADMの計算
                if (double.TryParse(DeltaMatureAdmTextBox.Text, out double deltaMature) &&
                    double.TryParse(FVMatureAdmTextBox.Text, out double fvMature) &&
                    fvMature > 0)
                {
                    double deltaMatureRatio = deltaMature / fvMature;
                    DeltaMatureRatioTextBox.Text = deltaMatureRatio.ToString("F3");
                }
                else
                {
                    DeltaMatureRatioTextBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                // エラー処理
                MessageBox.Show($"計算中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // サンプリング結果の要約を生成するメソッド
        private void UpdateSamplingSummary()
        {
            StringBuilder summary = new StringBuilder();

            try
            {
                // 各サイトのADMレベルを要約
                summary.AppendLine("【各サイトのADMレベル】");

                // LA (左房)
                bool hasLaData = !string.IsNullOrEmpty(LATotalAdmTextBox.Text) || !string.IsNullOrEmpty(LAMatureAdmTextBox.Text);
                if (hasLaData)
                {
                    summary.Append("左房(LA): ");
                    if (!string.IsNullOrEmpty(LATotalAdmTextBox.Text))
                        summary.Append($"total ADM {LATotalAdmTextBox.Text}pg/ml");

                    if (!string.IsNullOrEmpty(LAMatureAdmTextBox.Text))
                    {
                        if (!string.IsNullOrEmpty(LATotalAdmTextBox.Text))
                            summary.Append(", ");
                        summary.Append($"mature ADM {LAMatureAdmTextBox.Text}pg/ml");
                    }

                    if (!string.IsNullOrEmpty(LAAtxTextBox.Text))
                    {
                        if (!string.IsNullOrEmpty(LATotalAdmTextBox.Text) || !string.IsNullOrEmpty(LAMatureAdmTextBox.Text))
                            summary.Append(", ");
                        summary.Append($"ATX {LAAtxTextBox.Text}pg/L");
                    }
                    summary.AppendLine();
                }

                // CS (冠静脈洞)
                bool hasCsData = !string.IsNullOrEmpty(CSTotalAdmTextBox.Text) || !string.IsNullOrEmpty(CSMatureAdmTextBox.Text);
                if (hasCsData)
                {
                    summary.Append("冠静脈洞(CS): ");
                    if (!string.IsNullOrEmpty(CSTotalAdmTextBox.Text))
                        summary.Append($"total ADM {CSTotalAdmTextBox.Text}pg/ml");

                    if (!string.IsNullOrEmpty(CSMatureAdmTextBox.Text))
                    {
                        if (!string.IsNullOrEmpty(CSTotalAdmTextBox.Text))
                            summary.Append(", ");
                        summary.Append($"mature ADM {CSMatureAdmTextBox.Text}pg/ml");
                    }

                    if (!string.IsNullOrEmpty(CSAtxTextBox.Text))
                    {
                        if (!string.IsNullOrEmpty(CSTotalAdmTextBox.Text) || !string.IsNullOrEmpty(CSMatureAdmTextBox.Text))
                            summary.Append(", ");
                        summary.Append($"ATX {CSAtxTextBox.Text}pg/L");
                    }
                    summary.AppendLine();
                }

                // 差分値の要約
                if (!string.IsNullOrEmpty(DeltaTotalAdmTextBox.Text) || !string.IsNullOrEmpty(DeltaMatureAdmTextBox.Text))
                {
                    summary.AppendLine("\n【ADM産生量の推定】");
                    if (!string.IsNullOrEmpty(DeltaTotalAdmTextBox.Text))
                        summary.AppendLine($"Δt-ADM (CS-FV): {DeltaTotalAdmTextBox.Text}pg/ml");
                    if (!string.IsNullOrEmpty(DeltaMatureAdmTextBox.Text))
                        summary.AppendLine($"Δm-ADM (CS-FV): {DeltaMatureAdmTextBox.Text}pg/ml");
                }

                // CS-LA差分の要約
                if (!string.IsNullOrEmpty(CSLADeltaTotalAdmTextBox.Text) || !string.IsNullOrEmpty(CSLADeltaMatureAdmTextBox.Text))
                {
                    summary.AppendLine("\n【左房-冠静脈洞間の差分】");
                    if (!string.IsNullOrEmpty(CSLADeltaTotalAdmTextBox.Text))
                        summary.AppendLine($"CS-LA Δt-ADM: {CSLADeltaTotalAdmTextBox.Text}pg/ml");
                    if (!string.IsNullOrEmpty(CSLADeltaMatureAdmTextBox.Text))
                        summary.AppendLine($"CS-LA Δm-ADM: {CSLADeltaMatureAdmTextBox.Text}pg/ml");
                    if (!string.IsNullOrEmpty(CSLADeltaAtxTextBox.Text))
                        summary.AppendLine($"CS-LA ΔATX: {CSLADeltaAtxTextBox.Text}pg/L");
                }

                // 比率の要約
                bool hasRatios = !string.IsNullOrEmpty(LAMTAdmRatioTextBox.Text) || !string.IsNullOrEmpty(CSMTAdmRatioTextBox.Text);
                if (hasRatios)
                {
                    summary.AppendLine("\n【ADM成熟比率】");
                    if (!string.IsNullOrEmpty(LAMTAdmRatioTextBox.Text))
                        summary.AppendLine($"LA m/t ADM比: {LAMTAdmRatioTextBox.Text}");
                    if (!string.IsNullOrEmpty(CSMTAdmRatioTextBox.Text))
                        summary.AppendLine($"CS m/t ADM比: {CSMTAdmRatioTextBox.Text}");
                }

                // BSA標準化値の要約
                if (!string.IsNullOrEmpty(DeltaTotalAdmBSATextBox.Text) || !string.IsNullOrEmpty(TotalAdmBSATextBox.Text))
                {
                    summary.AppendLine("\n【BSA標準化値】");
                    if (!string.IsNullOrEmpty(DeltaTotalAdmBSATextBox.Text))
                        summary.AppendLine($"Δt-ADM/BSA: {DeltaTotalAdmBSATextBox.Text}pg/ml/m²");
                    if (!string.IsNullOrEmpty(TotalAdmBSATextBox.Text))
                        summary.AppendLine($"t-ADM/BSA: {TotalAdmBSATextBox.Text}pg/ml/m²");
                }

                // 臨床的意義についてのコメント
                if (!string.IsNullOrEmpty(DeltaTotalAdmTextBox.Text) && double.TryParse(DeltaTotalAdmTextBox.Text, out double deltaTotalAdm))
                {
                    summary.AppendLine("\n【臨床的意義】");
                    if (deltaTotalAdm > 5.0)
                    {
                        summary.AppendLine("Δt-ADMは上昇しており、心臓でのADM産生亢進が示唆されます。");
                    }
                    else if (deltaTotalAdm < 2.0)
                    {
                        summary.AppendLine("Δt-ADMは正常範囲内です。");
                    }
                }

                if (!string.IsNullOrEmpty(CSLADeltaTotalAdmTextBox.Text) && double.TryParse(CSLADeltaTotalAdmTextBox.Text, out double csLaDeltaTotal))
                {
                    if (csLaDeltaTotal > 0)
                    {
                        summary.AppendLine("CS-LA間のADM濃度勾配が見られ、左房内でのADM消費が示唆されます。");
                    }
                }

                if (string.IsNullOrEmpty(summary.ToString()))
                {
                    summary.AppendLine("サンプリングデータが入力されていません。");
                }
            }
            catch (Exception ex)
            {
                summary.AppendLine($"要約生成中にエラーが発生しました: {ex.Message}");
            }

            SamplingSummaryTextBlock.Text = summary.ToString();
        }

        // サンプリングデータの取得メソッド
        public SamplingData GetSamplingData()
        {
            return new SamplingData
            {
                // LA (左房)データ
                LATotalADM = LATotalAdmTextBox.Text,
                LAMatureADM = LAMatureAdmTextBox.Text,
                LAATX = LAAtxTextBox.Text,
                LAMTADMRatio = LAMTAdmRatioTextBox.Text,

                // CS (冠静脈洞)データ
                CSTotalADM = CSTotalAdmTextBox.Text,
                CSMatureADM = CSMatureAdmTextBox.Text,
                CSATX = CSAtxTextBox.Text,
                CSMTADMRatio = CSMTAdmRatioTextBox.Text,

                // FV (大腿静脈)データ
                FVTotalADM = FVTotalAdmTextBox.Text,
                FVMatureADM = FVMatureAdmTextBox.Text,
                FVATX = FVAtxTextBox.Text,

                // PA (肺静脈)データ
                PATotalADM = PATotalAdmTextBox.Text,
                PAMatureADM = PAMatureAdmTextBox.Text,
                PAATX = PAAtxTextBox.Text,

                // 計算値
                DeltaTotalADM = DeltaTotalAdmTextBox.Text,
                DeltaMatureADM = DeltaMatureAdmTextBox.Text,
                DeltaATX = DeltaAtxTextBox.Text,
                CSLADeltaTotalADM = CSLADeltaTotalAdmTextBox.Text,
                CSLADeltaMatureADM = CSLADeltaMatureAdmTextBox.Text,
                CSLADeltaATX = CSLADeltaAtxTextBox.Text,
                DeltaTotalADMBSA = DeltaTotalAdmBSATextBox.Text,
                TotalADMBSA = TotalAdmBSATextBox.Text,
                DeltaTotalRatio = DeltaTotalRatioTextBox.Text,
                DeltaMatureRatio = DeltaMatureRatioTextBox.Text,

                // 要約
                SamplingSummary = SamplingSummaryTextBlock.Text
            };
        }

        // データクリアメソッド
        public void ClearData()
        {
            // LA (左房)
            LATotalAdmTextBox.Clear();
            LAMatureAdmTextBox.Clear();
            LAAtxTextBox.Clear();
            LAMTAdmRatioTextBox.Clear();

            // CS (冠静脈洞)
            CSTotalAdmTextBox.Clear();
            CSMatureAdmTextBox.Clear();
            CSAtxTextBox.Clear();
            CSMTAdmRatioTextBox.Clear();

            // FV (大腿静脈)
            FVTotalAdmTextBox.Clear();
            FVMatureAdmTextBox.Clear();
            FVAtxTextBox.Clear();

            // PA (肺静脈)
            PATotalAdmTextBox.Clear();
            PAMatureAdmTextBox.Clear();
            PAAtxTextBox.Clear();

            // 計算値
            DeltaTotalAdmTextBox.Clear();
            DeltaMatureAdmTextBox.Clear();
            DeltaAtxTextBox.Clear();
            CSLADeltaTotalAdmTextBox.Clear();
            CSLADeltaMatureAdmTextBox.Clear();
            CSLADeltaAtxTextBox.Clear();
            DeltaTotalAdmBSATextBox.Clear();
            TotalAdmBSATextBox.Clear();
            DeltaTotalRatioTextBox.Clear();
            DeltaMatureRatioTextBox.Clear();

            // 要約
            SamplingSummaryTextBlock.Text = string.Empty;
        }

        /// <summary>
        /// サンプリングデータを設定
        /// </summary>
        /// <param name="patientData">設定する患者データ</param>
        public void SetSamplingData(PatientData patientData)
        {
            // LA (左房)
            LATotalAdmTextBox.Text = patientData.LATotalADM;
            LAMatureAdmTextBox.Text = patientData.LAMatureADM;
            LAAtxTextBox.Text = patientData.LAATX;
            // LAMTADMRatio は計算値

            // CS (冠静脈洞)
            CSTotalAdmTextBox.Text = patientData.CSTotalADM;
            CSMatureAdmTextBox.Text = patientData.CSMatureADM;
            CSAtxTextBox.Text = patientData.CSATX;
            // CSMTADMRatio は計算値

            // FV (大腿静脈)
            FVTotalAdmTextBox.Text = patientData.FVTotalADM;
            FVMatureAdmTextBox.Text = patientData.FVMatureADM;
            FVAtxTextBox.Text = patientData.FVATX;

            // PA (肺静脈)
            PATotalAdmTextBox.Text = patientData.PATotalADM;
            PAMatureAdmTextBox.Text = patientData.PAMatureADM;
            PAAtxTextBox.Text = patientData.PAATX;

            // 計算値は自動計算されるため設定不要

            // TextChanged イベントを発生させて要約を更新
            TextBox_TextChanged(null, null);
        }

    }

    // サンプリングデータクラス
    public class SamplingData
    {
        // LA (左房)データ
        public string LATotalADM { get; set; } = "";
        public string LAMatureADM { get; set; } = "";
        public string LAATX { get; set; } = "";
        public string LAMTADMRatio { get; set; } = "";

        // CS (冠静脈洞)データ
        public string CSTotalADM { get; set; } = "";
        public string CSMatureADM { get; set; } = "";
        public string CSATX { get; set; } = "";
        public string CSMTADMRatio { get; set; } = "";

        // FV (大腿静脈)データ
        public string FVTotalADM { get; set; } = "";
        public string FVMatureADM { get; set; } = "";
        public string FVATX { get; set; } = "";

        // PA (肺静脈)データ
        public string PATotalADM { get; set; } = "";
        public string PAMatureADM { get; set; } = "";
        public string PAATX { get; set; } = "";

        // 計算値
        public string DeltaTotalADM { get; set; } = "";
        public string DeltaMatureADM { get; set; } = "";
        public string DeltaATX { get; set; } = "";
        public string CSLADeltaTotalADM { get; set; } = "";
        public string CSLADeltaMatureADM { get; set; } = "";
        public string CSLADeltaATX { get; set; } = "";
        public string DeltaTotalADMBSA { get; set; } = "";
        public string TotalADMBSA { get; set; } = "";
        public string DeltaTotalRatio { get; set; } = "";
        public string DeltaMatureRatio { get; set; } = "";

        // 要約
        public string SamplingSummary { get; set; } = "";
    }
}