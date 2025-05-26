using System;
using System.Collections.Generic;
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
        // エクスポートサービス
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
            // エクスポートサービスの初期化
            _exportService = new ExportService();

            // 患者リストを読み込み
            LoadPatientList();

            // 初期状態の設定
            UpdateButtonState();
            UpdateStatusDisplay();

            // 新規患者モードで開始
            StartNewPatientMode();
        }

        /// <summary>
        /// 患者リストの読み込み
        /// </summary>
        private void LoadPatientList()
        {
            List<PatientListItem> patients = _databaseService.GetPatientList();
            PatientDataGrid.ItemsSource = patients;
            UpdatePatientCount(patients.Count);
        }

        /// <summary>
        /// ボタン状態の更新
        /// </summary>
        private void UpdateButtonState()
        {
            bool isPatientSelected = PatientDataGrid.SelectedItem != null;
            DeleteButton.IsEnabled = isPatientSelected;
        }

        /// <summary>
        /// ステータス表示の更新
        /// </summary>
        private void UpdateStatusDisplay()
        {
            if (_isNewMode)
            {
                StatusTextBlock.Text = "新規患者モード";
                StatusTextBlock.Foreground = System.Windows.Media.Brushes.Green;
            }
            else if (!string.IsNullOrEmpty(_currentPatientId))
            {
                StatusTextBlock.Text = $"患者ID: {_currentPatientId}";
                StatusTextBlock.Foreground = System.Windows.Media.Brushes.Blue;
            }
            else
            {
                StatusTextBlock.Text = "患者を選択してください";
                StatusTextBlock.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        /// <summary>
        /// 患者数の表示更新
        /// </summary>
        private void UpdatePatientCount(int count)
        {
            PatientCountTextBlock.Text = $"登録患者数: {count}人";
        }

        /// <summary>
        /// 新規患者ボタンのクリックイベントハンドラ
        /// </summary>
        private void NewPatientButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewPatientMode();
        }

        /// <summary>
        /// 新規患者モードを開始
        /// </summary>
        private void StartNewPatientMode()
        {
            // 未保存の変更がある場合は確認
            if (!_isNewMode || !string.IsNullOrEmpty(_currentPatientId))
            {
                MessageBoxResult result = MessageBox.Show(
                    "新規患者モードに移行します。\n保存されていないデータは失われます。\n続行しますか？",
                    "新規患者モード",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }

            // 新規モードに設定
            _isNewMode = true;
            _currentPatientId = "";
            this.Title = "患者データ入力フォーム - 新規患者";

            // データグリッドの選択を解除
            PatientDataGrid.SelectedItem = null;

            // 全データクリア
            ClearAllData();

            // ステータス更新
            UpdateStatusDisplay();
        }

        /// <summary>
        /// 更新ボタンのクリックイベントハンドラ
        /// </summary>
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPatientList();
        }

        /// <summary>
        /// 削除ボタンのクリックイベントハンドラ
        /// </summary>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PatientDataGrid.SelectedItem is PatientListItem selectedPatient)
            {
                // 削除確認
                MessageBoxResult result = MessageBox.Show(
                    $"患者ID「{selectedPatient.Id}」のデータを削除します。\nこの操作は元に戻せません。続行しますか？",
                    "患者データ削除の確認",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    // データベースから削除
                    bool success = _databaseService.DeletePatient(selectedPatient.Id);

                    if (success)
                    {
                        MessageBox.Show("患者データを削除しました。", "削除完了", MessageBoxButton.OK, MessageBoxImage.Information);

                        // 削除された患者が現在選択中の患者だった場合
                        if (_currentPatientId == selectedPatient.Id)
                        {
                            StartNewPatientMode();
                        }

                        // リストを更新
                        LoadPatientList();
                    }
                }
            }
        }

        /// <summary>
        /// データグリッドの選択変更イベントハンドラ
        /// </summary>
        private void PatientDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonState();

            if (PatientDataGrid.SelectedItem is PatientListItem selectedPatient)
            {
                // 未保存の変更がある場合は確認
                MessageBoxResult result = MessageBox.Show(
                    $"患者ID「{selectedPatient.Id}」のデータを読み込みます。\n保存されていないデータは失われます。\n続行しますか？",
                    "患者データ読み込み",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    LoadPatientData(selectedPatient.Id);
                }
                else
                {
                    // 選択を解除
                    PatientDataGrid.SelectedItem = null;
                }
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

                // 編集モードに設定
                _isNewMode = false;
                _currentPatientId = patientId;
                this.Title = $"患者データ入力フォーム - 患者ID: {patientId}";

                // ステータス更新
                UpdateStatusDisplay();
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
                    PatientDataCtrl.SetPatientData(patientData);
                }

                // 心房細動情報の設定
                if (AtrialFibrillationCtrl != null)
                {
                    AtrialFibrillationCtrl.SetAtrialFibrillationData(patientData);
                    UpdateAtrialFibrillationRiskScores();
                }

                // 心エコー情報の設定
                if (EchocardiogramCtrl != null)
                {
                    EchocardiogramCtrl.SetEchocardiogramData(patientData);
                    UpdateEchocardiogramHeartFailureStatus();
                }

                // 血液検査情報の設定
                if (BloodTestCtrl != null)
                {
                    BloodTestCtrl.SetBloodTestData(patientData);
                    BloodTestCtrl.SetPatientAge(patientData.Age);
                }

                // 心電図・レントゲン情報の設定
                if (EcgXrayCtrl != null)
                {
                    EcgXrayCtrl.SetEcgXrayData(patientData);
                }

                // 薬物療法情報の設定
                if (MedicationCtrl != null)
                {
                    MedicationCtrl.SetMedicationData(patientData);
                }

                // アブレーション情報の設定
                if (AblationCtrl != null)
                {
                    AblationCtrl.SetAblationData(patientData);
                }

                // サンプリング情報の設定
                if (SamplingCtrl != null)
                {
                    SamplingCtrl.SetSamplingData(patientData);
                    UpdateSamplingBSA();
                }

                // T-TAS情報の設定
                if (TTASCtrl != null)
                {
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
                    UpdateAtrialFibrillationRiskScores();
                }
                else if (tabHeader == "心エコー")
                {
                    UpdateEchocardiogramHeartFailureStatus();
                }
                else if (tabHeader == "血液検査")
                {
                    UpdateFib4IndexAge();
                }
                else if (tabHeader == "サンプリング")
                {
                    UpdateSamplingBSA();
                }
            }
        }

        // 患者データリスク因子変更時のイベントハンドラ
        private void PatientDataCtrl_RiskFactorsChanged(object sender, EventArgs e)
        {
            UpdateAtrialFibrillationRiskScores();
            UpdateFib4IndexAge();
            UpdateEchocardiogramHeartFailureStatus();
        }

        // 心房細動リスクスコア更新
        private void UpdateAtrialFibrillationRiskScores()
        {
            if (AtrialFibrillationCtrl != null)
            {
                PatientData patientData = PatientDataCtrl.GetPatientData();
                AtrialFibrillationCtrl.UpdateRiskScores(patientData);
            }
        }

        // 心エコーの心不全情報更新
        private void UpdateEchocardiogramHeartFailureStatus()
        {
            if (EchocardiogramCtrl != null && PatientDataCtrl != null)
            {
                PatientData patientData = PatientDataCtrl.GetPatientData();
                bool hasHeartFailure = patientData.HeartFailure == "あり";
                EchocardiogramCtrl.UpdateHeartFailureStatus(hasHeartFailure);
            }
        }

        // サンプリングタブのBSA情報更新
        private void UpdateSamplingBSA()
        {
            if (SamplingCtrl != null && PatientDataCtrl != null)
            {
                PatientData patientData = PatientDataCtrl.GetPatientData();
                SamplingCtrl.SetBSA(patientData.BSA);
            }
        }

        // FIB-4インデックス計算用の年齢情報更新
        private void UpdateFib4IndexAge()
        {
            if (BloodTestCtrl != null && PatientDataCtrl != null)
            {
                PatientData patientData = PatientDataCtrl.GetPatientData();
                BloodTestCtrl.SetPatientAge(patientData.Age);
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

                    // 患者リストを更新
                    LoadPatientList();

                    // ステータス更新
                    UpdateStatusDisplay();
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
            StartNewPatientMode();
        }

        // すべてのデータをクリア
        private void ClearAllData()
        {
            PatientDataCtrl.ClearData();

            if (AtrialFibrillationCtrl != null)
                AtrialFibrillationCtrl.ClearData();

            if (EchocardiogramCtrl != null)
                EchocardiogramCtrl.ClearData();

            if (BloodTestCtrl != null)
                BloodTestCtrl.ClearData();

            if (EcgXrayCtrl != null)
                EcgXrayCtrl.ClearData();

            if (MedicationCtrl != null)
                MedicationCtrl.ClearData();

            if (AblationCtrl != null)
                AblationCtrl.ClearData();

            if (SamplingCtrl != null)
                SamplingCtrl.ClearData();

            if (TTASCtrl != null)
                TTASCtrl.ClearData();

            MainTabControl.SelectedIndex = 0;
        }

        // CSVエクスポートメニュー用のイベントハンドラ
        private void ExportCurrentPatientMenu_Click(object sender, RoutedEventArgs e)
        {
            if (_isNewMode || string.IsNullOrEmpty(_currentPatientId))
            {
                MessageBox.Show("現在の患者データが保存されていません。\n先にデータを保存してください。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show("現在の患者データをCSVに出力する前に保存しますか？", "保存確認", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Cancel)
                return;

            if (result == MessageBoxResult.Yes)
                SaveButton_Click(sender, e);

            _exportService.ExportPatientToCSV(_currentPatientId);
        }

        private void ExportAllPatientsMenu_Click(object sender, RoutedEventArgs e)
        {
            _exportService.ExportAllPatientsToCSV();
        }

        // 終了メニューのクリックイベントハンドラ
        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // ウィンドウ閉じる時のイベントハンドラ
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "アプリケーションを終了しますか？\n保存されていないデータは失われます。",
                "終了確認",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                base.OnClosing(e);
            }
        }
    }
}