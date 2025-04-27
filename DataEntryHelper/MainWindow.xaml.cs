using System;
using System.Windows;
using System.Windows.Controls;
using DataEntryHelper.Controls;
using DataEntryHelper.Services;

namespace DataEntryHelper
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // データベースサービス
        private readonly DatabaseService _databaseService;
        private readonly ExportService _exportService;

        // 現在の患者ID
        private string _currentPatientId = "";

        // 新規モードかどうか
        private bool _isNewMode = true;

        public MainWindow()
        {
            InitializeComponent();

            // データベースサービスの初期化
            _databaseService = new DatabaseService();
            // CSV出力サービスの初期化
            _exportService = new ExportService();
            // 患者リスト画面を表示
            ShowPatientListWindow();
        }

        /// <summary>
        /// 患者リスト画面を表示
        /// </summary>
        private void ShowPatientListWindow()
        {
            PatientListWindow patientListWindow = new PatientListWindow();

            if (patientListWindow.ShowDialog() == true)
            {
                if (patientListWindow.IsNewPatient)
                {
                    // 新規患者モード
                    _isNewMode = true;
                    _currentPatientId = "";
                    ClearAllData();
                }
                else
                {
                    // 既存患者データ読み込みモード
                    _isNewMode = false;
                    _currentPatientId = patientListWindow.SelectedPatientId;
                    LoadPatientData(_currentPatientId);
                }
            }
            else
            {
                // キャンセルされた場合はアプリケーションを終了
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// 患者データを読み込む
        /// </summary>
        private void LoadPatientData(string patientId)
        {
            // データベースから患者データを読み込む
            PatientData patientData = _databaseService.LoadPatientData(patientId);

            if (patientData != null)
            {
                // 各コントロールにデータを設定
                SetDataToControls(patientData);

                // タイトルを更新
                this.Title = $"患者データ入力フォーム - 患者ID: {patientId}";
            }
            else
            {
                MessageBox.Show($"患者ID「{patientId}」のデータが見つかりませんでした。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 患者データを各コントロールに設定
        /// </summary>
        private void SetDataToControls(PatientData patientData)
        {
            // すべてのデータをクリア
            ClearAllData();

            try
            {
                // 患者基本情報の設定
                if (PatientDataCtrl != null)
                {
                    // このメソッドを実装する必要がある
                    PatientDataCtrl.SetPatientData(patientData);
                }

                // 心房細動情報の設定
                if (AtrialFibrillationCtrl != null)
                {
                    // このメソッドを実装する必要がある
                    AtrialFibrillationCtrl.SetAtrialFibrillationData(patientData);
                    // スコア更新
                    UpdateAtrialFibrillationRiskScores();
                }

                // 心エコー情報の設定
                if (EchocardiogramCtrl != null)
                {
                    // このメソッドを実装する必要がある
                    EchocardiogramCtrl.SetEchocardiogramData(patientData);
                    // 心不全情報更新
                    UpdateEchocardiogramHeartFailureStatus();
                }

                // 血液検査情報の設定
                if (BloodTestCtrl != null)
                {
                    // このメソッドを実装する必要がある
                    BloodTestCtrl.SetBloodTestData(patientData);
                    BloodTestCtrl.SetPatientAge(patientData.Age);
                }

                // 心電図・レントゲン情報の設定
                if (EcgXrayCtrl != null)
                {
                    // このメソッドを実装する必要がある
                    EcgXrayCtrl.SetEcgXrayData(patientData);
                }

                // 薬物療法情報の設定
                if (MedicationCtrl != null)
                {
                    // このメソッドを実装する必要がある
                    MedicationCtrl.SetMedicationData(patientData);
                }

                // アブレーション情報の設定
                if (AblationCtrl != null)
                {
                    // このメソッドを実装する必要がある
                    AblationCtrl.SetAblationData(patientData);
                }

                // サンプリング情報の設定
                if (SamplingCtrl != null)
                {
                    // このメソッドを実装する必要がある
                    SamplingCtrl.SetSamplingData(patientData);
                    // BSA情報を更新
                    UpdateSamplingBSA();
                }

                // T-TAS情報の設定
                if (TTASCtrl != null)
                {
                    // このメソッドを実装する必要がある
                    TTASCtrl.SetTTASData(patientData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"データ読み込み中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                else if (tabHeader == "血液検査")
                {
                    // 血液検査タブが選択された場合に年齢情報を更新
                    UpdateFib4IndexAge();
                }
                else if (tabHeader == "サンプリング")
                {
                    // サンプリングタブが選択された場合にBSA情報を渡す
                    UpdateSamplingBSA();
                }
            }
        }

        // 患者データリスク因子変更時のイベントハンドラ
        private void PatientDataCtrl_RiskFactorsChanged(object sender, EventArgs e)
        {
            // リスク因子が変更された場合、心房細動タブのスコアを更新
            UpdateAtrialFibrillationRiskScores();
            // 心房細動・心不全の更新の後に
            UpdateFib4IndexAge();
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

        // サンプリングタブのBSA情報更新
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

        // 保存ボタンクリック時のイベントハンドラ
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 患者データコントロールからデータを取得
                PatientData patientData = PatientDataCtrl.GetPatientData();

                // 患者IDが空白の場合は警告
                if (string.IsNullOrWhiteSpace(patientData.Id))
                {
                    MessageBox.Show("患者IDが入力されていません。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                    MainTabControl.SelectedIndex = 0; // 患者データタブに戻る
                    return;
                }

                // 新規モードの場合は現在の患者IDを更新
                if (_isNewMode)
                {
                    _currentPatientId = patientData.Id;
                    _isNewMode = false;
                    this.Title = $"患者データ入力フォーム - 患者ID: {_currentPatientId}";
                }
                else if (_currentPatientId != patientData.Id)
                {
                    // 既存データ編集モードでIDが変更された場合は警告
                    MessageBoxResult result = MessageBox.Show(
                        $"患者IDが変更されました。\n元のID: {_currentPatientId}\n新しいID: {patientData.Id}\n\n新しいIDで保存しますか？",
                        "患者ID変更の確認",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        _currentPatientId = patientData.Id;
                        this.Title = $"患者データ入力フォーム - 患者ID: {_currentPatientId}";
                    }
                    else
                    {
                        // 元のIDに戻す
                        patientData.Id = _currentPatientId;
                    }
                }

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

                // アブレーションデータを取得して結合
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

                // データベースに保存
                bool success = _databaseService.SavePatientData(patientData);

                if (success)
                {
                    MessageBox.Show($"患者データを保存しました。\n患者ID: {patientData.Id}", "保存完了", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // クリアボタンクリック時のイベントハンドラ
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "現在のデータをクリアして新規患者モードに移行しますか？\n保存されていないデータは失われます。",
                "データクリアの確認",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // 新規モードに設定
                _isNewMode = true;
                _currentPatientId = "";
                this.Title = "患者データ入力フォーム";

                // 全データクリア
                ClearAllData();
            }
        }

        // すべてのデータをクリア
        private void ClearAllData()
        {
            // 全てのユーザーコントロールのデータをクリア
            PatientDataCtrl.ClearData();

            if (AtrialFibrillationCtrl != null)
            {
                AtrialFibrillationCtrl.ClearData();
            }

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

            // アブレーションデータクリア
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

        // メニューバーの「患者リスト」ボタンクリック時のイベントハンドラ
        private void PatientListMenu_Click(object sender, RoutedEventArgs e)
        {
            // 現在のデータに未保存の変更がある場合は確認
            MessageBoxResult result = MessageBox.Show(
                "患者リストを開きます。\n保存されていないデータは失われる可能性があります。\n続行しますか？",
                "患者リストを開く",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                ShowPatientListWindow();
            }
        }
        // FIB-4インデックス計算用の年齢情報更新
        private void UpdateFib4IndexAge()
        {
            if (BloodTestCtrl != null && PatientDataCtrl != null)
            {
                // 患者データから年齢を取得
                PatientData patientData = PatientDataCtrl.GetPatientData();
                // 血液検査コントロールに年齢情報を渡す
                BloodTestCtrl.SetPatientAge(patientData.Age);
            }
        }
        // 終了メニューのクリックイベントハンドラ
        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            Close(); // ウィンドウを閉じる（OnClosingイベントが発生）
        }

        // ウィンドウ閉じる時のイベントハンドラ
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // 閉じる前に確認ダイアログを表示
            MessageBoxResult result = MessageBox.Show(
                "アプリケーションを終了しますか？\n保存されていないデータは失われます。",
                "終了確認",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true; // 終了をキャンセル
            }
            else
            {
                base.OnClosing(e); // 通常の終了処理
            }
        }

        // 血液検査インポートメニューのクリックイベントハンドラ
        private void ImportBloodTestFromClipboard_Click(object sender, RoutedEventArgs e)
        {
            if (BloodTestCtrl != null)
            {
                // 血液検査タブに切り替え
                MainTabControl.SelectedIndex = 3; // 血液検査タブのインデックスに合わせて調整

                // インポート実行
                BloodTestCtrl.ImportFromClipboard();
            }
        }

        private void ExportCurrentPatientMenu_Click(object sender, RoutedEventArgs e)
        {
            // 新規モードの場合や患者IDが設定されていない場合はエラー
            if (_isNewMode || string.IsNullOrEmpty(_currentPatientId))
            {
                MessageBox.Show("現在の患者データが保存されていません。\n先にデータを保存してください。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 現在の患者データを保存確認
            MessageBoxResult result = MessageBox.Show("現在の患者データをCSVに出力する前に保存しますか？", "保存確認", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Cancel)
            {
                return;
            }

            if (result == MessageBoxResult.Yes)
            {
                // 保存ボタンのクリックイベントを呼び出す
                SaveButton_Click(sender, e);
            }

            // CSVエクスポート
            _exportService.ExportPatientToCSV(_currentPatientId);
        }

        private void ExportAllPatientsMenu_Click(object sender, RoutedEventArgs e)
        {
            // 全患者データをCSVエクスポート
            _exportService.ExportAllPatientsToCSV();
        }
    }
}