using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DataEntryHelper.Services;

namespace DataEntryHelper
{
    /// <summary>
    /// PatientListWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class PatientListWindow : Window
    {
        // データベースサービス
        private readonly DatabaseService _databaseService;

        // 選択された患者ID
        public string SelectedPatientId { get; private set; }

        // 新規患者かどうか
        public bool IsNewPatient { get; private set; }

        public PatientListWindow()
        {
            InitializeComponent();

            // データベースサービスの初期化
            _databaseService = new DatabaseService();

            // 患者リストの読み込み
            LoadPatientList();

            // ボタンの初期状態設定
            UpdateButtonState();
        }

        /// <summary>
        /// 患者リストの読み込み
        /// </summary>
        private void LoadPatientList()
        {
            List<PatientListItem> patients = _databaseService.GetPatientList();
            PatientDataGrid.ItemsSource = patients;
        }

        /// <summary>
        /// ボタン状態の更新
        /// </summary>
        private void UpdateButtonState()
        {
            bool isPatientSelected = PatientDataGrid.SelectedItem != null;

            // 患者が選択されていない場合は開くと削除ボタンを無効化
            OpenButton.IsEnabled = isPatientSelected;
            DeleteButton.IsEnabled = isPatientSelected;
        }

        /// <summary>
        /// 新規患者ボタンのクリックイベントハンドラ
        /// </summary>
        private void NewPatientButton_Click(object sender, RoutedEventArgs e)
        {
            IsNewPatient = true;
            DialogResult = true;
            Close();
        }

        /// <summary>
        /// 更新ボタンのクリックイベントハンドラ
        /// </summary>
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPatientList();
        }

        /// <summary>
        /// 開くボタンのクリックイベントハンドラ
        /// </summary>
        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            if (PatientDataGrid.SelectedItem is PatientListItem selectedPatient)
            {
                SelectedPatientId = selectedPatient.Id;
                IsNewPatient = false;
                DialogResult = true;
                Close();
            }
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
                        // リストを更新
                        LoadPatientList();
                    }
                }
            }
        }

        /// <summary>
        /// 閉じるボタンのクリックイベントハンドラ
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        /// <summary>
        /// データグリッドの選択変更イベントハンドラ
        /// </summary>
        private void PatientDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonState();
        }
    }
}