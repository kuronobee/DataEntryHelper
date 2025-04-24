using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Text.Json;
using DataEntryHelper.Controls;

namespace DataEntryHelper
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // タブ選択変更時のイベントハンドラ
        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem selectedTab = MainTabControl.SelectedItem as TabItem;
            if (selectedTab != null)
            {
                string tabHeader = selectedTab.Header.ToString();

                if (tabHeader == "心房細動")
                {
                    // 心房細動タブが選択された場合にスコア計算を更新
                    UpdateAtrialFibrillationRiskScores();
                }
                else if (tabHeader == "心エコー")
                {
                    // 心エコータブが選択された場合に心不全情報を更新
                    UpdateEchocardiogramHeartFailureStatus();
                }
            }
        }

        // 患者データリスク因子変更時のイベントハンドラ
        private void PatientDataCtrl_RiskFactorsChanged(object sender, EventArgs e)
        {
            // リスク因子が変更された場合、心房細動タブのスコアを更新
            UpdateAtrialFibrillationRiskScores();

            // 心不全情報も更新
            UpdateEchocardiogramHeartFailureStatus();
        }

        // 心房細動リスクスコア更新
        private void UpdateAtrialFibrillationRiskScores()
        {
            if (AtrialFibrillationCtrl != null)
            {
                // 患者データコントロールからデータを取得してリスクスコアを更新
                PatientData patientData = PatientDataCtrl.GetPatientData();
                AtrialFibrillationCtrl.UpdateRiskScores(patientData);
            }
        }

        // 心エコーの心不全情報更新
        private void UpdateEchocardiogramHeartFailureStatus()
        {
            if (EchocardiogramCtrl != null && PatientDataCtrl != null)
            {
                // 患者データから心不全情報を取得
                PatientData patientData = PatientDataCtrl.GetPatientData();
                bool hasHeartFailure = patientData.HeartFailure == "あり";

                // 心エコーコントロールに心不全情報を渡す
                EchocardiogramCtrl.UpdateHeartFailureStatus(hasHeartFailure);
            }
        }

        // 保存ボタンクリック時のイベントハンドラ
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 患者データコントロールからデータを取得
                PatientData patientData = PatientDataCtrl.GetPatientData();

                // 心房細動データを取得して結合
                if (AtrialFibrillationCtrl != null)
                {
                    AtrialFibrillationData afData = AtrialFibrillationCtrl.GetAtrialFibrillationData();
                    patientData.AtrialFibrillationType = afData.AtrialFibrillationType;
                    patientData.AtrialFibrillationSymptoms = afData.AtrialFibrillationSymptoms;
                    patientData.Chads2Score = afData.Chads2Score;
                    patientData.Cha2ds2VascScore = afData.Cha2ds2VascScore;
                }

                // 心エコーデータを取得して結合
                if (EchocardiogramCtrl != null)
                {
                    EchocardiogramData echoData = EchocardiogramCtrl.GetEchocardiogramData();
                    patientData.IVSd = echoData.IVSd;
                    patientData.LVPWd = echoData.LVPWd;
                    patientData.LVDd = echoData.LVDd;
                    patientData.LVDs = echoData.LVDs;
                    patientData.EDV = echoData.EDV;
                    patientData.ESV = echoData.ESV;
                    patientData.LAD = echoData.LAD;
                    patientData.LAV = echoData.LAV;
                    patientData.LVEF = echoData.LVEF;
                    patientData.HeartFailureType = echoData.HeartFailureType;
                    patientData.FocalAsynergy = echoData.FocalAsynergy;
                    patientData.VHD = echoData.VHD;
                    patientData.EWave = echoData.EWave;
                    patientData.AWave = echoData.AWave;
                    patientData.EARatio = echoData.EARatio;
                    patientData.EPrimeSept = echoData.EPrimeSept;
                    patientData.EEPrimeRatio = echoData.EEPrimeRatio;
                    patientData.TRPG = echoData.TRPG;
                    patientData.IVCInsp = echoData.IVCInsp;
                    patientData.IVCExp = echoData.IVCExp;
                    patientData.OtherDisorder = echoData.OtherDisorder;
                    patientData.EchoSummary = echoData.EchoSummary;
                }

                // 血液検査データを取得して結合
                if (BloodTestCtrl != null)
                {
                    BloodTestData bloodData = BloodTestCtrl.GetBloodTestData();
                    patientData.TP = bloodData.TP;
                    patientData.Alb = bloodData.Alb;
                    patientData.BUN = bloodData.BUN;
                    patientData.Cre = bloodData.Cre;
                    patientData.CRP = bloodData.CRP;
                    patientData.CK = bloodData.CK;
                    patientData.AST = bloodData.AST;
                    patientData.ALT = bloodData.ALT;
                    patientData.LDL = bloodData.LDL;
                    patientData.HDL = bloodData.HDL;
                    patientData.TG = bloodData.TG;
                    patientData.HbA1c = bloodData.HbA1c;
                    patientData.Glu = bloodData.Glu;
                    patientData.Hb = bloodData.Hb;
                    patientData.WBC = bloodData.WBC;
                    patientData.Plt = bloodData.Plt;
                    patientData.PTINR = bloodData.PTINR;
                    patientData.APTT = bloodData.APTT;
                    patientData.Fib4i = bloodData.Fib4i;
                    patientData.UA = bloodData.UA;
                    patientData.BNP = bloodData.BNP;
                    patientData.AbnormalValues = bloodData.AbnormalValues;
                    patientData.ClinicalImplications = bloodData.ClinicalImplications;
                }

                // 心電図・レントゲンデータを取得して結合
                if (EcgXrayCtrl != null)
                {
                    EcgXrayData ecgXrayData = EcgXrayCtrl.GetEcgXrayData();
                    patientData.ECG_HR = ecgXrayData.ECG_HR;
                    patientData.ECG_Rhythm = ecgXrayData.ECG_Rhythm;
                    patientData.ECG_Axis = ecgXrayData.ECG_Axis;
                    patientData.ECG_ConductionDisturbance = ecgXrayData.ECG_ConductionDisturbance;
                    patientData.ECG_STTChange = ecgXrayData.ECG_STTChange;
                    patientData.ECG_Comment = ecgXrayData.ECG_Comment;
                    patientData.ECG_Summary = ecgXrayData.ECG_Summary;
                    patientData.XP_CTR = ecgXrayData.XP_CTR;
                    patientData.XP_LungField = ecgXrayData.XP_LungField;
                    patientData.XP_CPAngle = ecgXrayData.XP_CPAngle;
                    patientData.XP_Summary = ecgXrayData.XP_Summary;
                }

                // ディレクトリがなければ作成
                string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PatientData");
                Directory.CreateDirectory(directoryPath);

                // タイムスタンプとIDを含むファイル名を生成
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = $"Patient_{patientData.Id}_{timestamp}.json";
                string filePath = Path.Combine(directoryPath, fileName);

                // シリアライズして保存
                string jsonData = JsonSerializer.Serialize(patientData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonData);

                MessageBox.Show($"患者データが保存されました。\nファイル: {filePath}", "保存完了", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // クリアボタンクリック時のイベントハンドラ
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // 全てのユーザーコントロールのデータをクリア
            PatientDataCtrl.ClearData();
            AtrialFibrillationCtrl.ClearData();

            if (EchocardiogramCtrl != null)
            {
                EchocardiogramCtrl.ClearData();
            }

            if (BloodTestCtrl != null)
            {
                BloodTestCtrl.ClearData();
            }

            if (EcgXrayCtrl != null)
            {
                EcgXrayCtrl.ClearData();
            }

            // 患者データタブに戻る
            MainTabControl.SelectedIndex = 0;
        }
    }
}