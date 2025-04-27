using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DataEntryHelper.Controls
{
    /// <summary>
    /// BloodTestControl.xaml の相互作用ロジック
    /// </summary>
    public partial class BloodTestControl : UserControl
    {
        // 基準値の定義
        private readonly Dictionary<string, (double Min, double Max, string Unit, string Description)> referenceRanges =
            new Dictionary<string, (double Min, double Max, string Unit, string Description)>
            {
                // 蛋白・生化学
                {"TP", (6.5, 8.0, "g/dL", "総蛋白")},
                {"Alb", (3.8, 5.2, "g/dL", "アルブミン")},
                
                // 腎機能
                {"BUN", (8, 20, "mg/dL", "尿素窒素")},
                {"Cre", (0.6, 1.2, "mg/dL", "クレアチニン")},
                
                // 炎症・筋逸脱
                {"CRP", (0, 0.3, "mg/dL", "C反応性蛋白")},
                {"CK", (40, 200, "U/L", "クレアチンキナーゼ")},
                
                // 肝機能
                {"AST", (10, 35, "U/L", "アスパラギン酸アミノトランスフェラーゼ")},
                {"ALT", (5, 40, "U/L", "アラニンアミノトランスフェラーゼ")},
                
                // 脂質
                {"LDL", (70, 139, "mg/dL", "LDLコレステロール")},
                {"HDL", (40, 90, "mg/dL", "HDLコレステロール")},
                {"TG", (30, 149, "mg/dL", "中性脂肪")},
                
                // 血糖
                {"HbA1c", (4.6, 6.2, "%", "ヘモグロビンA1c")},
                {"Glu", (70, 109, "mg/dL", "血糖")},
                
                // 血算
                {"Hb", (13.0, 17.0, "g/dL", "ヘモグロビン")},
                {"WBC", (3500, 9000, "/μL", "白血球数")},
                {"Plt", (150000, 350000, "/μL", "血小板数")},
                
                // 凝固
                {"PT-INR", (0.8, 1.2, "-", "プロトロンビン時間国際標準比")},
                {"APTT", (25, 40, "秒", "活性化部分トロンボプラスチン時間")},
                
                // その他
                {"Fib4-i", (0, 1.45, "-", "線維化指数4")},
                {"UA", (3.0, 7.0, "mg/dL", "尿酸")},
                {"BNP", (0, 18.4, "pg/mL", "脳性ナトリウム利尿ペプチド")}
            };

        public BloodTestControl()
        {
            InitializeComponent();
        }

        // テキストボックスの値変更時のイベントハンドラ
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // FIB-4インデックスを計算
            CalculateFib4Index();

            // 異常値リストと臨床的意義を更新
            UpdateSummary();
        }

        // 検査結果要約の更新
        private void UpdateSummary()
        {
            // 異常値リストと臨床的意義をクリア
            StringBuilder abnormalValues = new StringBuilder();
            StringBuilder clinicalImplications = new StringBuilder();

            try
            {
                // 各検査値を評価
                EvaluateValue(TpTextBox, "TP", abnormalValues);
                EvaluateValue(AlbTextBox, "Alb", abnormalValues);
                EvaluateValue(BunTextBox, "BUN", abnormalValues);
                EvaluateValue(CreTextBox, "Cre", abnormalValues);
                EvaluateValue(CrpTextBox, "CRP", abnormalValues);
                EvaluateValue(CkTextBox, "CK", abnormalValues);
                EvaluateValue(AstTextBox, "AST", abnormalValues);
                EvaluateValue(AltTextBox, "ALT", abnormalValues);
                EvaluateValue(LdlTextBox, "LDL", abnormalValues);
                EvaluateValue(HdlTextBox, "HDL", abnormalValues);
                EvaluateValue(TgTextBox, "TG", abnormalValues);
                EvaluateValue(Hba1cTextBox, "HbA1c", abnormalValues);
                EvaluateValue(GluTextBox, "Glu", abnormalValues);
                EvaluateValue(HbTextBox, "Hb", abnormalValues);
                EvaluateValue(WbcTextBox, "WBC", abnormalValues);
                EvaluateValue(PltTextBox, "Plt", abnormalValues);
                EvaluateValue(PtInrTextBox, "PT-INR", abnormalValues);
                EvaluateValue(ApttTextBox, "APTT", abnormalValues);
                EvaluateValue(Fib4iTextBox, "Fib4-i", abnormalValues);
                EvaluateValue(UaTextBox, "UA", abnormalValues);
                EvaluateValue(BnpTextBox, "BNP", abnormalValues);

                // 臨床的意義の評価
                EvaluateClinicalImplications(clinicalImplications);

                // 異常値がない場合のメッセージ
                if (abnormalValues.Length == 0)
                {
                    abnormalValues.AppendLine("すべての検査値が基準範囲内です。");
                }

                // 異常値や臨床的意義がない場合のメッセージ
                if (clinicalImplications.Length == 0)
                {
                    clinicalImplications.AppendLine("特記すべき臨床的所見はありません。");
                }
            }
            catch (Exception ex)
            {
                abnormalValues.AppendLine($"計算中にエラーが発生しました: {ex.Message}");
            }

            // テキストブロックに反映
            AbnormalValuesTextBlock.Text = abnormalValues.ToString();
            ClinicalImplicationsTextBlock.Text = clinicalImplications.ToString();
        }

        private double _patientAge = 0;
        private bool _hasPatientAge = false;

        // 年齢情報を設定するメソッド
        public void SetPatientAge(string age)
        {
            if (double.TryParse(age, out double ageValue))
            {
                _patientAge = ageValue;
                _hasPatientAge = true;
                // 年齢が設定されたらFIB-4を再計算
                CalculateFib4Index();
            }
            else
            {
                _hasPatientAge = false;
            }
        }
        // FIB-4インデックス計算メソッド
        private void CalculateFib4Index()
        {
            if (_hasPatientAge &&
                double.TryParse(AstTextBox.Text, out double ast) &&
                double.TryParse(AltTextBox.Text, out double alt) &&
                double.TryParse(PltTextBox.Text, out double plt) &&
                plt > 0 && alt > 0)  // ALTが0だとルート計算でエラーになるため
            {
                try
                {
                    // FIB-4 = 年齢(years) × AST(U/L) / (血小板数(10^9/L) × √ALT(U/L))
                    double pltInCorrectUnit = plt / 1000; // 10^3/μL から 10^9/L への変換
                    double fib4 = (_patientAge * ast) / (pltInCorrectUnit * Math.Sqrt(alt));
                    Fib4iTextBox.Text = fib4.ToString("F2");
                }
                catch (Exception)
                {
                    Fib4iTextBox.Text = "";
                }
            }
        }
        // 個別の検査値を評価
        private void EvaluateValue(TextBox textBox, string testName, StringBuilder abnormalValues)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
                return;

            if (double.TryParse(textBox.Text, out double value) && referenceRanges.ContainsKey(testName))
            {
                var range = referenceRanges[testName];

                // 値が基準範囲外かどうかを判定
                if (value < range.Min)
                {
                    abnormalValues.AppendLine($"{range.Description}({testName}): {value} {range.Unit} ↓ [基準: {range.Min}-{range.Max}]");
                    textBox.Foreground = System.Windows.Media.Brushes.Blue; // 低値は青色で表示
                }
                else if (value > range.Max)
                {
                    abnormalValues.AppendLine($"{range.Description}({testName}): {value} {range.Unit} ↑ [基準: {range.Min}-{range.Max}]");
                    textBox.Foreground = System.Windows.Media.Brushes.Red; // 高値は赤色で表示
                }
                else
                {
                    textBox.Foreground = System.Windows.Media.Brushes.Black; // 正常値は黒色
                }
            }
        }

        // 臨床的意義の評価
        private void EvaluateClinicalImplications(StringBuilder implications)
        {
            // 貧血の評価
            if (TryGetDoubleValue(HbTextBox, out double hb) && hb < referenceRanges["Hb"].Min)
            {
                implications.AppendLine("貧血の所見があります。");
            }

            // 腎機能障害の評価
            if (TryGetDoubleValue(CreTextBox, out double cre) && cre > referenceRanges["Cre"].Max)
            {
                implications.AppendLine("腎機能障害の可能性があります。");
            }

            // 肝機能障害の評価
            if ((TryGetDoubleValue(AstTextBox, out double ast) && ast > referenceRanges["AST"].Max) ||
                (TryGetDoubleValue(AltTextBox, out double alt) && alt > referenceRanges["ALT"].Max))
            {
                implications.AppendLine("肝機能障害の可能性があります。");
            }

            // 炎症反応の評価
            if (TryGetDoubleValue(CrpTextBox, out double crp) && crp > referenceRanges["CRP"].Max)
            {
                implications.AppendLine("炎症反応が陽性です。");
            }

            // 糖尿病の評価
            if (TryGetDoubleValue(Hba1cTextBox, out double hba1c) && hba1c >= 6.5)
            {
                implications.AppendLine("HbA1cの上昇から糖尿病が疑われます。");
            }
            else if (TryGetDoubleValue(Hba1cTextBox, out double prediabetes) && prediabetes >= 5.7 && prediabetes < 6.5)
            {
                implications.AppendLine("HbA1cの軽度上昇から糖尿病前症が疑われます。");
            }

            // 脂質異常症の評価
            bool hasLipidAbnormality = false;

            if (TryGetDoubleValue(LdlTextBox, out double ldl) && ldl > referenceRanges["LDL"].Max)
            {
                hasLipidAbnormality = true;
            }

            if (TryGetDoubleValue(HdlTextBox, out double hdl) && hdl < referenceRanges["HDL"].Min)
            {
                hasLipidAbnormality = true;
            }

            if (TryGetDoubleValue(TgTextBox, out double tg) && tg > referenceRanges["TG"].Max)
            {
                hasLipidAbnormality = true;
            }

            if (hasLipidAbnormality)
            {
                implications.AppendLine("脂質異常症の所見があります。");
            }

            // 栄養状態の評価
            if (TryGetDoubleValue(AlbTextBox, out double alb) && alb < referenceRanges["Alb"].Min)
            {
                implications.AppendLine("低アルブミン血症があり、栄養状態が不良である可能性があります。");
            }

            // 心不全の評価
            if (TryGetDoubleValue(BnpTextBox, out double bnp) && bnp > referenceRanges["BNP"].Max)
            {
                if (bnp > 100)
                {
                    implications.AppendLine("BNPの著明な上昇から心不全が強く疑われます。");
                }
                else
                {
                    implications.AppendLine("BNPの軽度上昇があります。心機能評価が推奨されます。");
                }
            }
        }

        // TextBoxから数値を取得するユーティリティメソッド
        private bool TryGetDoubleValue(TextBox textBox, out double value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(textBox.Text))
                return false;

            return double.TryParse(textBox.Text, out value);
        }

        // 血液検査データの取得
        public BloodTestData GetBloodTestData()
        {
            return new BloodTestData
            {
                TP = TpTextBox.Text,
                Alb = AlbTextBox.Text,
                BUN = BunTextBox.Text,
                Cre = CreTextBox.Text,
                CRP = CrpTextBox.Text,
                CK = CkTextBox.Text,
                AST = AstTextBox.Text,
                ALT = AltTextBox.Text,
                LDL = LdlTextBox.Text,
                HDL = HdlTextBox.Text,
                TG = TgTextBox.Text,
                HbA1c = Hba1cTextBox.Text,
                Glu = GluTextBox.Text,
                Hb = HbTextBox.Text,
                WBC = WbcTextBox.Text,
                Plt = PltTextBox.Text,
                PTINR = PtInrTextBox.Text,
                APTT = ApttTextBox.Text,
                Fib4i = Fib4iTextBox.Text,
                UA = UaTextBox.Text,
                BNP = BnpTextBox.Text,
                AbnormalValues = AbnormalValuesTextBlock.Text,
                ClinicalImplications = ClinicalImplicationsTextBlock.Text
            };
        }

        // データクリアメソッド
        public void ClearData()
        {
            // すべてのテキストボックスをクリア
            TpTextBox.Clear();
            AlbTextBox.Clear();
            BunTextBox.Clear();
            CreTextBox.Clear();
            CrpTextBox.Clear();
            CkTextBox.Clear();
            AstTextBox.Clear();
            AltTextBox.Clear();
            LdlTextBox.Clear();
            HdlTextBox.Clear();
            TgTextBox.Clear();
            Hba1cTextBox.Clear();
            GluTextBox.Clear();
            HbTextBox.Clear();
            WbcTextBox.Clear();
            PltTextBox.Clear();
            PtInrTextBox.Clear();
            ApttTextBox.Clear();
            Fib4iTextBox.Clear();
            UaTextBox.Clear();
            BnpTextBox.Clear();

            // テキストブロックもクリア
            AbnormalValuesTextBlock.Text = string.Empty;
            ClinicalImplicationsTextBlock.Text = string.Empty;

            // テキストの色をリセット
            ResetTextColors();
        }

        // テキストの色をリセット
        private void ResetTextColors()
        {
            var textBoxes = new TextBox[]
            {
                TpTextBox, AlbTextBox, BunTextBox, CreTextBox, CrpTextBox, CkTextBox,
                AstTextBox, AltTextBox, LdlTextBox, HdlTextBox, TgTextBox, Hba1cTextBox,
                GluTextBox, HbTextBox, WbcTextBox, PltTextBox, PtInrTextBox, ApttTextBox,
                Fib4iTextBox, UaTextBox, BnpTextBox
            };

            foreach (var textBox in textBoxes)
            {
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        /// <summary>
        /// 血液検査データを設定
        /// </summary>
        /// <param name="patientData">設定する患者データ</param>
        public void SetBloodTestData(PatientData patientData)
        {
            // 蛋白・生化学
            TpTextBox.Text = patientData.TP;
            AlbTextBox.Text = patientData.Alb;

            // 腎機能
            BunTextBox.Text = patientData.BUN;
            CreTextBox.Text = patientData.Cre;

            // 炎症・筋逸脱
            CrpTextBox.Text = patientData.CRP;
            CkTextBox.Text = patientData.CK;

            // 肝機能
            AstTextBox.Text = patientData.AST;
            AltTextBox.Text = patientData.ALT;

            // 脂質
            LdlTextBox.Text = patientData.LDL;
            HdlTextBox.Text = patientData.HDL;
            TgTextBox.Text = patientData.TG;

            // 血糖
            Hba1cTextBox.Text = patientData.HbA1c;
            GluTextBox.Text = patientData.Glu;

            // 血算
            HbTextBox.Text = patientData.Hb;
            WbcTextBox.Text = patientData.WBC;
            PltTextBox.Text = patientData.Plt;

            // 凝固
            PtInrTextBox.Text = patientData.PTINR;
            ApttTextBox.Text = patientData.APTT;

            // その他
            Fib4iTextBox.Text = patientData.Fib4i;
            UaTextBox.Text = patientData.UA;
            BnpTextBox.Text = patientData.BNP;

            // TextChanged イベントを発生させて要約を更新
            TextBox_TextChanged(null, null);
        }

    }

    // 血液検査データクラス
    public class BloodTestData
    {
        public string TP { get; set; } = "";
        public string Alb { get; set; } = "";
        public string BUN { get; set; } = "";
        public string Cre { get; set; } = "";
        public string CRP { get; set; } = "";
        public string CK { get; set; } = "";
        public string AST { get; set; } = "";
        public string ALT { get; set; } = "";
        public string LDL { get; set; } = "";
        public string HDL { get; set; } = "";
        public string TG { get; set; } = "";
        public string HbA1c { get; set; } = "";
        public string Glu { get; set; } = "";
        public string Hb { get; set; } = "";
        public string WBC { get; set; } = "";
        public string Plt { get; set; } = "";
        public string PTINR { get; set; } = "";
        public string APTT { get; set; } = "";
        public string Fib4i { get; set; } = "";
        public string UA { get; set; } = "";
        public string BNP { get; set; } = "";
        public string AbnormalValues { get; set; } = "";
        public string ClinicalImplications { get; set; } = "";
    }
}