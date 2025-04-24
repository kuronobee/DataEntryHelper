using System;
using System.Windows;
using System.Windows.Controls;

namespace DataEntryHelper.Controls
{
    /// <summary>
    /// PatientDataControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PatientDataControl : UserControl
    {
        // リスク評価が更新されたときのイベント
        public event EventHandler RiskFactorsChanged;

        public PatientDataControl()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            // ComboBoxの初期値設定
            GenderComboBox.SelectedIndex = 0;
            RhythmComboBox.SelectedIndex = 0;
            SmokingComboBox.SelectedIndex = 2; // noneをデフォルトに

            // はい/いいえのComboBox初期値設定
            HypertensionComboBox.SelectedIndex = 1; // "なし"をデフォルトに
            DiabetesComboBox.SelectedIndex = 1;
            DyslipidemiaComboBox.SelectedIndex = 1;
            CkdComboBox.SelectedIndex = 1;
            StrokeComboBox.SelectedIndex = 1;
            HeartFailureComboBox.SelectedIndex = 1;
            VascularDiseaseComboBox.SelectedIndex = 1;
            CoronaryIschemiaComboBox.SelectedIndex = 1;
            CardiomyopathyComboBox.SelectedIndex = 1;
            DementiaComboBox.SelectedIndex = 1;
        }

        // 身長・体重からBMIと体表面積を計算
        private void CalculateFields(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (double.TryParse(HeightTextBox.Text, out double height) &&
                    double.TryParse(WeightTextBox.Text, out double weight) &&
                    height > 0 && weight > 0)
                {
                    // 身長をcmからmに変換
                    double heightInMeters = height / 100.0;

                    // BMI計算: 体重(kg) / 身長(m)²
                    double bmi = weight / (heightInMeters * heightInMeters);
                    BmiTextBox.Text = bmi.ToString("F2");

                    // 体表面積の計算（Du Bois式使用）: 0.007184 * 身長(cm)^0.725 * 体重(kg)^0.425
                    double bsa = 0.007184 * Math.Pow(height, 0.725) * Math.Pow(weight, 0.425);
                    BsaTextBox.Text = bsa.ToString("F2");
                }
                else
                {
                    // 入力が無効の場合、計算フィールドをクリア
                    BmiTextBox.Text = string.Empty;
                    BsaTextBox.Text = string.Empty;
                }
            }
            catch
            {
                // 計算中のエラーを処理
                BmiTextBox.Text = string.Empty;
                BsaTextBox.Text = string.Empty;
            }
        }

        // リスク因子が変更されたときのイベントハンドラ
        private void RiskFactorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // リスク因子が変更されたことをメインウィンドウに通知
            RiskFactorsChanged?.Invoke(this, EventArgs.Empty);
        }

        // 年齢変更イベントハンドラ
        private void AgeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // 年齢変更時にもリスク評価を更新
            RiskFactorsChanged?.Invoke(this, EventArgs.Empty);
        }

        // データ取得メソッド（メインウィンドウから呼び出される）
        public PatientData GetPatientData()
        {
            return new PatientData
            {
                // 患者情報
                Id = IdTextBox.Text,
                Gender = GenderComboBox.Text,
                Age = AgeTextBox.Text,
                Height = HeightTextBox.Text,
                Weight = WeightTextBox.Text,
                BSA = BsaTextBox.Text,
                BMI = BmiTextBox.Text,

                // バイタル
                SystolicBP = SystolicBpTextBox.Text,
                DiastolicBP = DiastolicBpTextBox.Text,
                HeartRate = HeartRateTextBox.Text,
                Rhythm = RhythmComboBox.Text,

                // 生活歴・疾患
                Alcohol = AlcoholTextBox.Text,
                Smoking = SmokingComboBox.Text,
                Hypertension = HypertensionComboBox.Text,
                Diabetes = DiabetesComboBox.Text,
                Dyslipidemia = DyslipidemiaComboBox.Text,
                CKD = CkdComboBox.Text,
                Stroke = StrokeComboBox.Text,
                HeartFailure = HeartFailureComboBox.Text,
                VascularDisease = VascularDiseaseComboBox.Text,
                CoronaryIschemia = CoronaryIschemiaComboBox.Text,
                Cardiomyopathy = CardiomyopathyComboBox.Text,
                Dementia = DementiaComboBox.Text,
                Others = OthersTextBox.Text
            };
        }

        // データクリアメソッド
        public void ClearData()
        {
            // すべてのTextBoxフィールドをクリア
            IdTextBox.Clear();
            AgeTextBox.Clear();
            HeightTextBox.Clear();
            WeightTextBox.Clear();
            BsaTextBox.Clear();
            BmiTextBox.Clear();
            SystolicBpTextBox.Clear();
            DiastolicBpTextBox.Clear();
            HeartRateTextBox.Clear();
            AlcoholTextBox.Clear();
            OthersTextBox.Clear();

            // すべてのComboBoxをデフォルト値にリセット
            InitializeComboBoxes();

            // IDフィールドにフォーカスを設定
            IdTextBox.Focus();
        }
    }
}