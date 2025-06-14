﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DataEntryHelper.Controls
{
    /// <summary>
    /// AtrialFibrillationControl.xaml の相互作用ロジック
    /// </summary>
    public partial class AtrialFibrillationControl : UserControl
    {
        public AtrialFibrillationControl()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            // 心房細動タブのComboBoxの初期値設定
            AtrialFibrillationTypeComboBox.SelectedIndex = 0; // 空白
            AtrialFibrillationSymptomsComboBox.SelectedIndex = 0; // 空白
        }

        // 患者データに基づいてリスクスコアを更新
        public void UpdateRiskScores(PatientData patientData)
        {
            try
            {
                int chads2Score = 0;
                int cha2ds2VascScore = 0;
                StringBuilder details = new StringBuilder();

                // 年齢の評価
                if (int.TryParse(patientData.Age, out int age))
                {
                    // CHADS2: 75歳以上で1点
                    if (age >= 75)
                    {
                        chads2Score += 1;
                        details.AppendLine("年齢 ≥ 75歳: +1点 (CHADS2)");
                    }

                    // CHA2DS2-VASc: 65-74歳で1点、75歳以上で2点
                    if (age >= 75)
                    {
                        cha2ds2VascScore += 2;
                        details.AppendLine("年齢 ≥ 75歳: +2点 (CHA2DS2-VASc)");
                    }
                    else if (age >= 65)
                    {
                        cha2ds2VascScore += 1;
                        details.AppendLine("年齢 65-74歳: +1点 (CHA2DS2-VASc)");
                    }
                }

                // 高血圧
                if (patientData.Hypertension == "あり")
                {
                    chads2Score += 1;
                    cha2ds2VascScore += 1;
                    details.AppendLine("高血圧: +1点");
                }

                // 糖尿病
                if (patientData.Diabetes == "あり")
                {
                    chads2Score += 1;
                    cha2ds2VascScore += 1;
                    details.AppendLine("糖尿病: +1点");
                }

                // 脳卒中既往
                if (patientData.Stroke == "あり")
                {
                    chads2Score += 2;
                    cha2ds2VascScore += 2;
                    details.AppendLine("脳卒中既往: +2点");
                }

                // 心不全
                if (patientData.HeartFailure == "あり")
                {
                    chads2Score += 1;
                    cha2ds2VascScore += 1;
                    details.AppendLine("心不全: +1点");
                }

                // 血管疾患（CHA2DS2-VAScのみ）
                if (patientData.VascularDisease == "あり")
                {
                    cha2ds2VascScore += 1;
                    details.AppendLine("血管疾患: +1点 (CHA2DS2-VASc)");
                }

                // 性別（CHA2DS2-VAScのみ）
                if (patientData.Gender == "女性")
                {
                    cha2ds2VascScore += 1;
                    details.AppendLine("女性: +1点 (CHA2DS2-VASc)");
                }

                // スコアの表示
                Chads2ScoreTextBox.Text = chads2Score.ToString();
                Cha2ds2VascScoreTextBox.Text = cha2ds2VascScore.ToString();

                // スコア詳細の表示
                RiskScoreDetailsTextBlock.Text = details.ToString();

                // スコアに基づく脳卒中リスクとガイドラインの追加
                AddRiskGuidelines(chads2Score, cha2ds2VascScore);
            }
            catch (Exception ex)
            {
                // エラー処理
                MessageBox.Show($"スコア計算中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddRiskGuidelines(int chads2Score, int cha2ds2VascScore)
        {
            if (RiskScoreDetailsTextBlock != null) // NULLチェック
            {
                RiskScoreDetailsTextBlock.Text += "\n\n【CHADS2スコアに基づく年間脳卒中リスク】\n";
                Dictionary<int, string> chads2Risk = new Dictionary<int, string>
                {
                    {0, "1.9%"},
                    {1, "2.8%"},
                    {2, "4.0%"},
                    {3, "5.9%"},
                    {4, "8.5%"},
                    {5, "12.5%"},
                    {6, "18.2%"}
                };

                if (chads2Risk.ContainsKey(chads2Score))
                {
                    RiskScoreDetailsTextBlock.Text += $"スコア {chads2Score}: 年間脳卒中リスク {chads2Risk[chads2Score]}\n";
                }

                RiskScoreDetailsTextBlock.Text += "\n【CHA2DS2-VAScスコアに基づく抗凝固療法推奨】\n";
                if (cha2ds2VascScore == 0)
                {
                    RiskScoreDetailsTextBlock.Text += "抗凝固療法は推奨されない\n";
                }
                else if (cha2ds2VascScore == 1)
                {
                    RiskScoreDetailsTextBlock.Text += "抗凝固療法を考慮してもよい\n";
                }
                else
                {
                    RiskScoreDetailsTextBlock.Text += "抗凝固療法が推奨される\n";
                }
            }
        }

        // 心房細動データを取得
        public AtrialFibrillationData GetAtrialFibrillationData()
        {
            return new AtrialFibrillationData
            {
                AtrialFibrillationType = GetComboBoxSelectedText(AtrialFibrillationTypeComboBox),
                AtrialFibrillationSymptoms = GetComboBoxSelectedText(AtrialFibrillationSymptomsComboBox),
                Chads2Score = Chads2ScoreTextBox.Text,
                Cha2ds2VascScore = Cha2ds2VascScoreTextBox.Text
            };
        }

        // データクリアメソッド
        public void ClearData()
        {
            // スコアフィールドをクリア
            Chads2ScoreTextBox.Clear();
            Cha2ds2VascScoreTextBox.Clear();
            RiskScoreDetailsTextBlock.Text = string.Empty;

            // ComboBoxを初期値にリセット
            InitializeComboBoxes();
        }

        /// <summary>
        /// 心房細動データを設定
        /// </summary>
        /// <param name="patientData">設定する患者データ</param>
        public void SetAtrialFibrillationData(PatientData patientData)
        {
            // 心房細動タイプ
            SetComboBoxByText(AtrialFibrillationTypeComboBox, patientData.AtrialFibrillationType);

            // 心房細動の症状
            SetComboBoxByText(AtrialFibrillationSymptomsComboBox, patientData.AtrialFibrillationSymptoms);

            // スコアは計算値のため設定不要
        }

        /// <summary>
        /// ComboBoxから選択されたテキストを取得するヘルパーメソッド
        /// </summary>
        /// <param name="comboBox">対象のComboBox</param>
        /// <returns>選択されたテキスト（選択されていない場合は空文字）</returns>
        private string GetComboBoxSelectedText(ComboBox comboBox)
        {
            if (comboBox.SelectedItem == null)
                return "";

            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;
            if (selectedItem == null)
                return "";

            return selectedItem.Content.ToString();
        }

        /// <summary>
        /// ComboBoxにテキスト値を設定するヘルパーメソッド
        /// </summary>
        /// <param name="comboBox">対象のComboBox</param>
        /// <param name="text">設定するテキスト</param>
        private void SetComboBoxByText(ComboBox comboBox, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                comboBox.SelectedIndex = 0; // 空白を選択
                return;
            }

            // アイテムを検索して一致するものを選択
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                ComboBoxItem item = comboBox.Items[i] as ComboBoxItem;
                if (item != null && item.Content.ToString() == text)
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }

            // 一致するアイテムが見つからない場合は空白を選択
            comboBox.SelectedIndex = 0;
        }
    }

    // 心房細動データクラス
    public class AtrialFibrillationData
    {
        public string AtrialFibrillationType { get; set; } = "";
        public string AtrialFibrillationSymptoms { get; set; } = "";
        public string Chads2Score { get; set; } = "";
        public string Cha2ds2VascScore { get; set; } = "";
    }
}