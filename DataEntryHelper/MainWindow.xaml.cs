using System;
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
                else if (tabHeader == "サンプリング")
                {
                    // サンプリングタブが選択された場合にBSA情報を渡す
                    UpdateSamplingBSA();
                }
            }
        }
        private void UpdateSamplingBSA()
        {
            if (SamplingCtrl != null && PatientDataCtrl != null)
            {
                // 患者データから体表面積を取得
                PatientData patientData = PatientDataCtrl.GetPatientData();
                // BSA情報をサンプリングコントロールに渡す
                SamplingCtrl.SetBSA(patientData.BSA);
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

                // 薬物療法データを取得して結合
                if (MedicationCtrl != null)
                {
                    MedicationData medData = MedicationCtrl.GetMedicationData();

                    // βブロッカー
                    patientData.BetaBlocker = medData.BetaBlocker;
                    patientData.BetaBlockerName1 = medData.BetaBlockerName1;
                    patientData.BetaBlockerDose1 = medData.BetaBlockerDose1;
                    patientData.BetaBlockerName2 = medData.BetaBlockerName2;
                    patientData.BetaBlockerDose2 = medData.BetaBlockerDose2;
                    patientData.BetaBlockerName3 = medData.BetaBlockerName3;
                    patientData.BetaBlockerDose3 = medData.BetaBlockerDose3;

                    // CCB
                    patientData.CCB = medData.CCB;
                    patientData.CCBName1 = medData.CCBName1;
                    patientData.CCBDose1 = medData.CCBDose1;
                    patientData.CCBName2 = medData.CCBName2;
                    patientData.CCBDose2 = medData.CCBDose2;
                    patientData.CCBName3 = medData.CCBName3;
                    patientData.CCBDose3 = medData.CCBDose3;

                    // 抗不整脈薬
                    patientData.AntiArrhythmicDrug = medData.AntiArrhythmicDrug;
                    patientData.AntiArrhythmicDrugName1 = medData.AntiArrhythmicDrugName1;
                    patientData.AntiArrhythmicDrugDose1 = medData.AntiArrhythmicDrugDose1;
                    patientData.AntiArrhythmicDrugName2 = medData.AntiArrhythmicDrugName2;
                    patientData.AntiArrhythmicDrugDose2 = medData.AntiArrhythmicDrugDose2;
                    patientData.AntiArrhythmicDrugName3 = medData.AntiArrhythmicDrugName3;
                    patientData.AntiArrhythmicDrugDose3 = medData.AntiArrhythmicDrugDose3;

                    // DOAC
                    patientData.DOAC = medData.DOAC;
                    patientData.DOACName1 = medData.DOACName1;
                    patientData.DOACDose1 = medData.DOACDose1;
                    patientData.DOACName2 = medData.DOACName2;
                    patientData.DOACDose2 = medData.DOACDose2;
                    patientData.DOACName3 = medData.DOACName3;
                    patientData.DOACDose3 = medData.DOACDose3;

                    // VKA
                    patientData.VKA = medData.VKA;
                    patientData.VKADose = medData.VKADose;

                    // スタチン
                    patientData.Statin = medData.Statin;
                    patientData.StatinName1 = medData.StatinName1;
                    patientData.StatinDose1 = medData.StatinDose1;
                    patientData.StatinName2 = medData.StatinName2;
                    patientData.StatinDose2 = medData.StatinDose2;
                    patientData.StatinName3 = medData.StatinName3;
                    patientData.StatinDose3 = medData.StatinDose3;

                    // SGLT2i
                    patientData.SGLT2i = medData.SGLT2i;
                    patientData.SGLT2iName1 = medData.SGLT2iName1;
                    patientData.SGLT2iDose1 = medData.SGLT2iDose1;
                    patientData.SGLT2iName2 = medData.SGLT2iName2;
                    patientData.SGLT2iDose2 = medData.SGLT2iDose2;
                    patientData.SGLT2iName3 = medData.SGLT2iName3;
                    patientData.SGLT2iDose3 = medData.SGLT2iDose3;

                    // ACE/ARB/ARNi
                    patientData.RAAS = medData.RAAS;
                    patientData.RAASName1 = medData.RAASName1;
                    patientData.RAASDose1 = medData.RAASDose1;
                    patientData.RAASName2 = medData.RAASName2;
                    patientData.RAASDose2 = medData.RAASDose2;
                    patientData.RAASName3 = medData.RAASName3;
                    patientData.RAASDose3 = medData.RAASDose3;

                    // MRA
                    patientData.MRA = medData.MRA;
                    patientData.MRAName1 = medData.MRAName1;
                    patientData.MRADose1 = medData.MRADose1;
                    patientData.MRAName2 = medData.MRAName2;
                    patientData.MRADose2 = medData.MRADose2;
                    patientData.MRAName3 = medData.MRAName3;
                    patientData.MRADose3 = medData.MRADose3;

                    // 利尿薬
                    patientData.Diuretics = medData.Diuretics;
                    patientData.DiureticsName1 = medData.DiureticsName1;
                    patientData.DiureticsDose1 = medData.DiureticsDose1;
                    patientData.DiureticsName2 = medData.DiureticsName2;
                    patientData.DiureticsDose2 = medData.DiureticsDose2;
                    patientData.DiureticsName3 = medData.DiureticsName3;
                    patientData.DiureticsDose3 = medData.DiureticsDose3;

                    // 抗血小板薬
                    patientData.AntiplateletAgent = medData.AntiplateletAgent;
                    patientData.AntiplateletAgentName1 = medData.AntiplateletAgentName1;
                    patientData.AntiplateletAgentDose1 = medData.AntiplateletAgentDose1;
                    patientData.AntiplateletAgentName2 = medData.AntiplateletAgentName2;
                    patientData.AntiplateletAgentDose2 = medData.AntiplateletAgentDose2;
                    patientData.AntiplateletAgentName3 = medData.AntiplateletAgentName3;
                    patientData.AntiplateletAgentDose3 = medData.AntiplateletAgentDose3;

                    // その他の薬剤
                    patientData.OtherMedications = medData.OtherMedications;

                    // 薬物療法要約
                    patientData.MedicationSummary = medData.MedicationSummary;
                }

                // アブレーションデータを取得して結合（追加部分）
                if (AblationCtrl != null)
                {
                    AblationData ablationData = AblationCtrl.GetAblationData();
                    patientData.MappingSystem = ablationData.MappingSystem;
                    patientData.MappingRhythm = ablationData.MappingRhythm;
                    patientData.PacingSite = ablationData.PacingSite;
                    patientData.PreMap = ablationData.PreMap;
                    patientData.ProcedureCount = ablationData.ProcedureCount;
                    patientData.ProcedurePVI = ablationData.ProcedurePVI;
                    patientData.ProcedurePosteriorWallIsolation = ablationData.ProcedurePosteriorWallIsolation;
                    patientData.ProcedureCFAE_FAAM = ablationData.ProcedureCFAE_FAAM;
                    patientData.ProcedureOther = ablationData.ProcedureOther;
                    patientData.Result = ablationData.Result;
                    patientData.LVAs = ablationData.LVAs;
                    patientData.VGLA = ablationData.VGLA;
                    patientData.MaxVoltageAnterior = ablationData.MaxVoltageAnterior;
                    patientData.MaxVoltageSeptum = ablationData.MaxVoltageSeptum;
                    patientData.MaxVoltageRoof = ablationData.MaxVoltageRoof;
                    patientData.MaxVoltageInf = ablationData.MaxVoltageInf;
                    patientData.MaxVoltagePost = ablationData.MaxVoltagePost;
                    patientData.MaxVoltageLat = ablationData.MaxVoltageLat;
                    patientData.MaxVoltageMean = ablationData.MaxVoltageMean;
                    patientData.AblationSummary = ablationData.AblationSummary;
                }
                // サンプリングデータを取得して結合
                if (SamplingCtrl != null)
                {
                    SamplingData samplingData = SamplingCtrl.GetSamplingData();
                    patientData.LATotalADM = samplingData.LATotalADM;
                    patientData.LAMatureADM = samplingData.LAMatureADM;
                    patientData.LAATX = samplingData.LAATX;
                    patientData.LAMTADMRatio = samplingData.LAMTADMRatio;
                    patientData.CSTotalADM = samplingData.CSTotalADM;
                    patientData.CSMatureADM = samplingData.CSMatureADM;
                    patientData.CSATX = samplingData.CSATX;
                    patientData.CSMTADMRatio = samplingData.CSMTADMRatio;
                    patientData.FVTotalADM = samplingData.FVTotalADM;
                    patientData.FVMatureADM = samplingData.FVMatureADM;
                    patientData.FVATX = samplingData.FVATX;
                    patientData.PATotalADM = samplingData.PATotalADM;
                    patientData.PAMatureADM = samplingData.PAMatureADM;
                    patientData.PAATX = samplingData.PAATX;
                    patientData.DeltaTotalADM = samplingData.DeltaTotalADM;
                    patientData.DeltaMatureADM = samplingData.DeltaMatureADM;
                    patientData.DeltaATX = samplingData.DeltaATX;
                    patientData.CSLADeltaTotalADM = samplingData.CSLADeltaTotalADM;
                    patientData.CSLADeltaMatureADM = samplingData.CSLADeltaMatureADM;
                    patientData.CSLADeltaATX = samplingData.CSLADeltaATX;
                    patientData.DeltaTotalADMBSA = samplingData.DeltaTotalADMBSA;
                    patientData.TotalADMBSA = samplingData.TotalADMBSA;
                    patientData.DeltaTotalRatio = samplingData.DeltaTotalRatio;
                    patientData.DeltaMatureRatio = samplingData.DeltaMatureRatio;
                    patientData.SamplingSummary = samplingData.SamplingSummary;
                }
                // T-TASデータを取得して結合
                if (TTASCtrl != null)
                {
                    TTASData ttasData = TTASCtrl.GetTTASData();
                    patientData.TTASPL = ttasData.TTASPL;
                    patientData.TTASAR = ttasData.TTASAR;
                    patientData.TTASSummary = ttasData.TTASSummary;
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

            if (MedicationCtrl != null)
            {
                MedicationCtrl.ClearData();
            }

            // アブレーションデータクリア（追加部分）
            if (AblationCtrl != null)
            {
                AblationCtrl.ClearData();
            }
            // サンプリングデータクリア
            if (SamplingCtrl != null)
            {
                SamplingCtrl.ClearData();
            }
            // T-TASデータクリア
            if (TTASCtrl != null)
            {
                TTASCtrl.ClearData();
            }
            // 患者データタブに戻る
            MainTabControl.SelectedIndex = 0;
        }
    }
}