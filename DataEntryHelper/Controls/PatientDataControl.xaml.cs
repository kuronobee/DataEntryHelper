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

        /// <summary>
        /// 患者データを設定
        /// </summary>
        /// <param name="patientData">設定する患者データ</param>
        public void SetPatientData(PatientData patientData)
        {
            // テキストボックス・コンボボックスに値を設定
            IdTextBox.Text = patientData.Id;

            // 性別
            if (patientData.Gender == "男性")
                GenderComboBox.SelectedIndex = 0;
            else if (patientData.Gender == "女性")
                GenderComboBox.SelectedIndex = 1;
            else
                GenderComboBox.SelectedIndex = 0;

            // 基本情報
            AgeTextBox.Text = patientData.Age;
            HeightTextBox.Text = patientData.Height;
            WeightTextBox.Text = patientData.Weight;
            // BSA、BMIは計算値なので設定不要

            // バイタル
            SystolicBpTextBox.Text = patientData.SystolicBP;
            DiastolicBpTextBox.Text = patientData.DiastolicBP;
            HeartRateTextBox.Text = patientData.HeartRate;

            // リズム
            if (patientData.Rhythm == "整")
                RhythmComboBox.SelectedIndex = 0;
            else if (patientData.Rhythm == "不整")
                RhythmComboBox.SelectedIndex = 1;
            else
                RhythmComboBox.SelectedIndex = 0;

            // 生活歴
            AlcoholTextBox.Text = patientData.Alcohol;

            // 喫煙歴
            if (patientData.Smoking == "current")
                SmokingComboBox.SelectedIndex = 0;
            else if (patientData.Smoking == "past")
                SmokingComboBox.SelectedIndex = 1;
            else if (patientData.Smoking == "none")
                SmokingComboBox.SelectedIndex = 2;
            else
                SmokingComboBox.SelectedIndex = 2;

            // 疾患（あり/なしのコンボボックス）
            SetYesNoComboBox(HypertensionComboBox, patientData.Hypertension);
            SetYesNoComboBox(DiabetesComboBox, patientData.Diabetes);
            SetYesNoComboBox(DyslipidemiaComboBox, patientData.Dyslipidemia);
            SetYesNoComboBox(CkdComboBox, patientData.CKD);
            SetYesNoComboBox(StrokeComboBox, patientData.Stroke);
            SetYesNoComboBox(HeartFailureComboBox, patientData.HeartFailure);
            SetYesNoComboBox(VascularDiseaseComboBox, patientData.VascularDisease);
            SetYesNoComboBox(CoronaryIschemiaComboBox, patientData.CoronaryIschemia);
            SetYesNoComboBox(CardiomyopathyComboBox, patientData.Cardiomyopathy);
            SetYesNoComboBox(DementiaComboBox, patientData.Dementia);

            // その他
            OthersTextBox.Text = patientData.Others;

            // 計算フィールドを更新
            CalculateFields(null, null);
        }

        /// <summary>
        /// あり/なしコンボボックスの値を設定
        /// </summary>
        private void SetYesNoComboBox(ComboBox comboBox, string value)
        {
            if (value == "あり")
                comboBox.SelectedIndex = 0;
            else if (value == "なし")
                comboBox.SelectedIndex = 1;
            else
                comboBox.SelectedIndex = 1; // デフォルトは「なし」
        }
    }
}