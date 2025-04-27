using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DataEntryHelper.Controls
{
    /// <summary>
    /// EchocardiogramControl.xaml の相互作用ロジック
    /// </summary>
    public partial class EchocardiogramControl : UserControl
    {
        public EchocardiogramControl()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            // ComboBoxの初期値を設定
            FocalAsynergyComboBox.SelectedIndex = 1; // "なし"をデフォルトに
            VhdComboBox.SelectedIndex = 1; // "なし"をデフォルトに
        }

        // LVEF（左室駆出率）の変更イベントハンドラ
        private void LvefTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateHeartFailureType();
        }

        // 心不全タイプの計算メソッド
        private void CalculateHeartFailureType()
        {
            try
            {
                // 心不全タイプの判定には患者基本情報の心不全フラグ情報が必要
                if (float.TryParse(LvefTextBox.Text, out float lvef))
                {
                    // HFrEF: 心不全かつLVEF < 40%
                    // HFmrEF: 心不全かつLVEF 40-49%
                    // HFpEF: 心不全かつLVEF ≥ 50%
                    if (lvef < 40)
                    {
                        HeartFailureTypeTextBox.Text = "HFrEF";
                    }
                    else if (lvef >= 40 && lvef <= 49)
                    {
                        HeartFailureTypeTextBox.Text = "HFmrEF";
                    }
                    else if (lvef >= 50)
                    {
                        HeartFailureTypeTextBox.Text = "HFpEF";
                    }
                    else
                    {
                        HeartFailureTypeTextBox.Text = "";
                    }
                }
                else
                {
                    HeartFailureTypeTextBox.Text = "";
                }

                // 心エコーレポート概要を更新
                UpdateEchoSummary();
            }
            catch
            {
                HeartFailureTypeTextBox.Text = "";
            }
        }

        // 拡張能パラメータ（E/AおよびE/e'）の計算
        private void CalculateDiastolicParameters(object sender, TextChangedEventArgs e)
        {
            try
            {
                // E/A比の計算
                if (float.TryParse(EWaveTextBox.Text, out float eWave) &&
                    float.TryParse(AWaveTextBox.Text, out float aWave) &&
                    aWave > 0)
                {
                    float eaRatio = eWave / aWave;
                    EaRatioTextBox.Text = eaRatio.ToString("F2");
                }
                else
                {
                    EaRatioTextBox.Text = "";
                }

                // E/e'比の計算
                if (float.TryParse(EWaveTextBox.Text, out float E) &&
                    float.TryParse(EPrimeSeptTextBox.Text, out float ePrime) &&
                    ePrime > 0)
                {
                    float eePrimeRatio = E / ePrime;
                    EePrimeRatioTextBox.Text = eePrimeRatio.ToString("F2");
                }
                else
                {
                    EePrimeRatioTextBox.Text = "";
                }

                // 心エコーレポート概要を更新
                UpdateEchoSummary();
            }
            catch
            {
                EaRatioTextBox.Text = "";
                EePrimeRatioTextBox.Text = "";
            }
        }

        // 心エコーレポート概要の更新
        private void UpdateEchoSummary()
        {
            StringBuilder summary = new StringBuilder();

            try
            {
                // 左室サイズと壁厚
                bool hasLvData = !string.IsNullOrEmpty(LvddTextBox.Text) || !string.IsNullOrEmpty(LvdsTextBox.Text);
                bool hasWallData = !string.IsNullOrEmpty(IvsdTextBox.Text) || !string.IsNullOrEmpty(LvpwdTextBox.Text);

                if (hasLvData || hasWallData)
                {
                    summary.AppendLine("【左室形態】");

                    if (hasLvData)
                    {
                        summary.Append("左室径: ");
                        if (!string.IsNullOrEmpty(LvddTextBox.Text))
                            summary.Append($"拡張期 {LvddTextBox.Text}mm");

                        if (!string.IsNullOrEmpty(LvdsTextBox.Text))
                        {
                            if (!string.IsNullOrEmpty(LvddTextBox.Text))
                                summary.Append(", ");
                            summary.Append($"収縮期 {LvdsTextBox.Text}mm");
                        }
                        summary.AppendLine();
                    }

                    if (hasWallData)
                    {
                        summary.Append("壁厚: ");
                        if (!string.IsNullOrEmpty(IvsdTextBox.Text))
                            summary.Append($"中隔 {IvsdTextBox.Text}mm");

                        if (!string.IsNullOrEmpty(LvpwdTextBox.Text))
                        {
                            if (!string.IsNullOrEmpty(IvsdTextBox.Text))
                                summary.Append(", ");
                            summary.Append($"後壁 {LvpwdTextBox.Text}mm");
                        }
                        summary.AppendLine();
                    }
                }

                // 左室機能
                if (!string.IsNullOrEmpty(LvefTextBox.Text))
                {
                    summary.AppendLine("【左室機能】");
                    summary.AppendLine($"LVEF: {LvefTextBox.Text}%");

                    if (!string.IsNullOrEmpty(HeartFailureTypeTextBox.Text))
                    {
                        string heartFailureTypeJp = "";
                        switch (HeartFailureTypeTextBox.Text)
                        {
                            case "HFrEF":
                                heartFailureTypeJp = "駆出率低下型";
                                break;
                            case "HFmrEF":
                                heartFailureTypeJp = "駆出率軽度低下型";
                                break;
                            case "HFpEF":
                                heartFailureTypeJp = "駆出率保持型";
                                break;
                        }

                        if (!string.IsNullOrEmpty(heartFailureTypeJp))
                            summary.AppendLine($"分類: {HeartFailureTypeTextBox.Text}（{heartFailureTypeJp}心不全）");
                    }

                    if (FocalAsynergyComboBox.Text == "あり")
                    {
                        summary.AppendLine("局所壁運動異常あり");
                    }
                }

                // 拡張能
                bool hasDiastolicData = !string.IsNullOrEmpty(EWaveTextBox.Text) ||
                                        !string.IsNullOrEmpty(AWaveTextBox.Text) ||
                                        !string.IsNullOrEmpty(EaRatioTextBox.Text) ||
                                        !string.IsNullOrEmpty(EePrimeRatioTextBox.Text);

                if (hasDiastolicData)
                {
                    summary.AppendLine("【拡張能】");

                    if (!string.IsNullOrEmpty(EWaveTextBox.Text))
                        summary.Append($"E波: {EWaveTextBox.Text}cm/s");

                    if (!string.IsNullOrEmpty(AWaveTextBox.Text))
                    {
                        if (!string.IsNullOrEmpty(EWaveTextBox.Text))
                            summary.Append(", ");
                        summary.Append($"A波: {AWaveTextBox.Text}cm/s");
                    }

                    if (!string.IsNullOrEmpty(EaRatioTextBox.Text))
                    {
                        if (!string.IsNullOrEmpty(EWaveTextBox.Text) || !string.IsNullOrEmpty(AWaveTextBox.Text))
                            summary.Append(", ");
                        summary.Append($"E/A: {EaRatioTextBox.Text}");
                    }

                    summary.AppendLine();

                    if (!string.IsNullOrEmpty(EPrimeSeptTextBox.Text))
                        summary.Append($"e'中隔: {EPrimeSeptTextBox.Text}cm/s");

                    if (!string.IsNullOrEmpty(EePrimeRatioTextBox.Text))
                    {
                        if (!string.IsNullOrEmpty(EPrimeSeptTextBox.Text))
                            summary.Append(", ");
                        summary.Append($"E/e': {EePrimeRatioTextBox.Text}");

                        // E/e'による拡張能障害の評価
                        float eePrime;
                        if (float.TryParse(EePrimeRatioTextBox.Text, out eePrime))
                        {
                            if (eePrime > 14)
                                summary.Append(" (拡張能障害の可能性が高い)");
                            else if (eePrime < 8)
                                summary.Append(" (拡張能障害の可能性は低い)");
                            else
                                summary.Append(" (境界域)");
                        }
                    }

                    summary.AppendLine();
                }

                // 右心系
                bool hasRightHeartData = !string.IsNullOrEmpty(TrPgTextBox.Text) ||
                                         !string.IsNullOrEmpty(IvcInspTextBox.Text) ||
                                         !string.IsNullOrEmpty(IvcExpTextBox.Text);

                if (hasRightHeartData)
                {
                    summary.AppendLine("【右心系】");

                    if (!string.IsNullOrEmpty(TrPgTextBox.Text))
                        summary.AppendLine($"TR-PG: {TrPgTextBox.Text}mmHg");

                    if (!string.IsNullOrEmpty(IvcInspTextBox.Text) || !string.IsNullOrEmpty(IvcExpTextBox.Text))
                    {
                        summary.Append("IVC径: ");
                        if (!string.IsNullOrEmpty(IvcExpTextBox.Text))
                            summary.Append($"呼気時 {IvcExpTextBox.Text}mm");

                        if (!string.IsNullOrEmpty(IvcInspTextBox.Text))
                        {
                            if (!string.IsNullOrEmpty(IvcExpTextBox.Text))
                                summary.Append(", ");
                            summary.Append($"吸気時 {IvcInspTextBox.Text}mm");
                        }

                        // IVCの呼吸性変動を計算
                        if (!string.IsNullOrEmpty(IvcExpTextBox.Text) && !string.IsNullOrEmpty(IvcInspTextBox.Text))
                        {
                            if (float.TryParse(IvcExpTextBox.Text, out float ivcExp) &&
                                float.TryParse(IvcInspTextBox.Text, out float ivcInsp) &&
                                ivcExp > 0)
                            {
                                float collapsibility = (ivcExp - ivcInsp) / ivcExp * 100;
                                summary.Append($", 虚脱率 {collapsibility:F0}%");
                            }
                        }

                        summary.AppendLine();
                    }
                }

                // その他の所見
                if (!string.IsNullOrEmpty(OtherDisorderTextBox.Text))
                {
                    summary.AppendLine("【その他】");
                    summary.AppendLine(OtherDisorderTextBox.Text);
                }

                // 弁膜症
                if (VhdComboBox.Text == "あり")
                {
                    if (summary.Length > 0)
                        summary.AppendLine();
                    summary.AppendLine("※中等度以上の弁膜症あり");
                }
            }
            catch
            {
                summary.AppendLine("レポート生成中にエラーが発生しました。");
            }

            EchoSummaryTextBlock.Text = summary.ToString();
        }

        // 心エコーデータの取得
        public EchocardiogramData GetEchocardiogramData()
        {
            return new EchocardiogramData
            {
                IVSd = IvsdTextBox.Text,
                LVPWd = LvpwdTextBox.Text,
                LVDd = LvddTextBox.Text,
                LVDs = LvdsTextBox.Text,
                EDV = EdvTextBox.Text,
                ESV = EsvTextBox.Text,
                LAD = LadTextBox.Text,
                LAV = LavTextBox.Text,
                LVEF = LvefTextBox.Text,
                HeartFailureType = HeartFailureTypeTextBox.Text,
                FocalAsynergy = FocalAsynergyComboBox.Text,
                VHD = VhdComboBox.Text,
                EWave = EWaveTextBox.Text,
                AWave = AWaveTextBox.Text,
                EARatio = EaRatioTextBox.Text,
                EPrimeSept = EPrimeSeptTextBox.Text,
                EEPrimeRatio = EePrimeRatioTextBox.Text,
                TRPG = TrPgTextBox.Text,
                IVCInsp = IvcInspTextBox.Text,
                IVCExp = IvcExpTextBox.Text,
                OtherDisorder = OtherDisorderTextBox.Text,
                EchoSummary = EchoSummaryTextBlock.Text
            };
        }

        // 心不全情報の更新（メインウィンドウから呼び出す）
        public void UpdateHeartFailureStatus(bool hasHeartFailure)
        {
            // 心不全ステータスが変更された場合に心不全タイプを再計算
            CalculateHeartFailureType();
        }

        // データクリアメソッド
        public void ClearData()
        {
            // 全てのTextBoxをクリア
            IvsdTextBox.Clear();
            LvpwdTextBox.Clear();
            LvddTextBox.Clear();
            LvdsTextBox.Clear();
            EdvTextBox.Clear();
            EsvTextBox.Clear();
            LadTextBox.Clear();
            LavTextBox.Clear();
            LvefTextBox.Clear();
            HeartFailureTypeTextBox.Clear();
            EWaveTextBox.Clear();
            AWaveTextBox.Clear();
            EaRatioTextBox.Clear();
            EPrimeSeptTextBox.Clear();
            EePrimeRatioTextBox.Clear();
            TrPgTextBox.Clear();
            IvcInspTextBox.Clear();
            IvcExpTextBox.Clear();
            OtherDisorderTextBox.Clear();
            EchoSummaryTextBlock.Text = string.Empty;

            // ComboBoxを初期値にリセット
            InitializeComboBoxes();
        }

        /// <summary>
        /// 心エコーデータを設定
        /// </summary>
        /// <param name="patientData">設定する患者データ</param>
        public void SetEchocardiogramData(PatientData patientData)
        {
            // 左室パラメータ
            IvsdTextBox.Text = patientData.IVSd;
            LvpwdTextBox.Text = patientData.LVPWd;
            LvddTextBox.Text = patientData.LVDd;
            LvdsTextBox.Text = patientData.LVDs;
            EdvTextBox.Text = patientData.EDV;
            EsvTextBox.Text = patientData.ESV;

            // 左房パラメータ
            LadTextBox.Text = patientData.LAD;
            LavTextBox.Text = patientData.LAV;

            // 収縮機能
            LvefTextBox.Text = patientData.LVEF;
            // HeartFailureTypeは計算値

            // 壁運動異常
            if (patientData.FocalAsynergy == "あり")
                FocalAsynergyComboBox.SelectedIndex = 0;
            else if (patientData.FocalAsynergy == "なし")
                FocalAsynergyComboBox.SelectedIndex = 1;

            // 弁膜症
            if (patientData.VHD == "あり")
                VhdComboBox.SelectedIndex = 0;
            else if (patientData.VHD == "なし")
                VhdComboBox.SelectedIndex = 1;

            // 拡張能
            EWaveTextBox.Text = patientData.EWave;
            AWaveTextBox.Text = patientData.AWave;
            // E/A比は計算値
            EPrimeSeptTextBox.Text = patientData.EPrimeSept;
            // E/e'比は計算値

            // 右心系
            TrPgTextBox.Text = patientData.TRPG;
            IvcInspTextBox.Text = patientData.IVCInsp;
            IvcExpTextBox.Text = patientData.IVCExp;

            // その他の所見
            OtherDisorderTextBox.Text = patientData.OtherDisorder;
        }
    }

    // 心エコーデータクラス
    public class EchocardiogramData
    {
        public string IVSd { get; set; } = "";
        public string LVPWd { get; set; } = "";
        public string LVDd { get; set; } = "";
        public string LVDs { get; set; } = "";
        public string EDV { get; set; } = "";
        public string ESV { get; set; } = "";
        public string LAD { get; set; } = "";
        public string LAV { get; set; } = "";
        public string LVEF { get; set; } = "";
        public string HeartFailureType { get; set; } = "";
        public string FocalAsynergy { get; set; } = "";
        public string VHD { get; set; } = "";
        public string EWave { get; set; } = "";
        public string AWave { get; set; } = "";
        public string EARatio { get; set; } = "";
        public string EPrimeSept { get; set; } = "";
        public string EEPrimeRatio { get; set; } = "";
        public string TRPG { get; set; } = "";
        public string IVCInsp { get; set; } = "";
        public string IVCExp { get; set; } = "";
        public string OtherDisorder { get; set; } = "";
        public string EchoSummary { get; set; } = "";
    }
}