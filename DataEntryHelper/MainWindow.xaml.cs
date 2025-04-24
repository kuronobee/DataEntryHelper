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
            if (selectedTab != null && selectedTab.Header.ToString() == "心房細動")
            {
                // 心房細動タブが選択された場合にスコア計算を更新
                UpdateAtrialFibrillationRiskScores();
            }
        }

        // 患者データリスク因子変更時のイベントハンドラ
        private void PatientDataCtrl_RiskFactorsChanged(object sender, EventArgs e)
        {
            // リスク因子が変更された場合、心房細動タブのスコアを更新
            UpdateAtrialFibrillationRiskScores();
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

            // 患者データタブに戻る
            MainTabControl.SelectedIndex = 0;
        }
    }
}