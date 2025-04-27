// DataEntryHelper/Services/ExportService.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace DataEntryHelper.Services
{
    /// <summary>
    /// データエクスポート機能を提供するサービスクラス
    /// </summary>
    public class ExportService
    {
        private readonly DatabaseService _databaseService;

        public ExportService()
        {
            _databaseService = new DatabaseService();
        }

        /// <summary>
        /// 特定の患者データをCSVにエクスポート
        /// </summary>
        /// <param name="patientId">患者ID</param>
        /// <returns>エクスポートの成否</returns>
        public bool ExportPatientToCSV(string patientId)
        {
            try
            {
                // 患者データを取得
                PatientData patientData = _databaseService.LoadPatientData(patientId);

                if (patientData == null)
                {
                    MessageBox.Show($"患者ID「{patientId}」のデータが見つかりませんでした。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                // 保存ダイアログを表示
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSVファイル (*.csv)|*.csv",
                    FileName = $"Patient_{patientId}_{DateTime.Now:yyyyMMdd}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    // CSVを生成して保存
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                        // ヘッダー行とデータ行を書き込む
                        sw.WriteLine(GenerateCSVHeader());
                        sw.WriteLine(GenerateCSVData(patientData));
                    }

                    MessageBox.Show($"患者データをCSVファイルにエクスポートしました。\n{saveFileDialog.FileName}", "エクスポート完了", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CSVエクスポート中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// 全患者データをCSVにエクスポート
        /// </summary>
        /// <returns>エクスポートの成否</returns>
        public bool ExportAllPatientsToCSV()
        {
            try
            {
                // 患者リストを取得
                List<PatientListItem> patientList = _databaseService.GetPatientList();

                if (patientList == null || patientList.Count == 0)
                {
                    MessageBox.Show("エクスポートする患者データがありません。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                // 保存ダイアログを表示
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSVファイル (*.csv)|*.csv",
                    FileName = $"AllPatients_{DateTime.Now:yyyyMMdd}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    // CSVを生成して保存
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                        // ヘッダー行を書き込む
                        sw.WriteLine(GenerateCSVHeader());

                        // 各患者のデータを書き込む
                        foreach (PatientListItem item in patientList)
                        {
                            PatientData patientData = _databaseService.LoadPatientData(item.Id);
                            if (patientData != null)
                            {
                                sw.WriteLine(GenerateCSVData(patientData));
                            }
                        }
                    }

                    MessageBox.Show($"全患者データをCSVファイルにエクスポートしました。\n患者数: {patientList.Count}人\n{saveFileDialog.FileName}", "エクスポート完了", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CSVエクスポート中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        // GenerateCSVHeader()メソッドを修正 - 要約フィールドを削除

        /// <summary>
        /// CSVヘッダーを生成
        /// </summary>
        /// <returns>CSVヘッダー行</returns>
        private string GenerateCSVHeader()
        {
            List<string> headers = new List<string>
            {
                // 患者基本情報
                "ID", "性別", "年齢", "身長(cm)", "体重(kg)", "体表面積(m²)", "BMI",
                // バイタル
                "収縮期血圧(mmHg)", "拡張期血圧(mmHg)", "心拍数(/min)", "リズム",
                // 生活歴・疾患
                "アルコール(g/day)", "喫煙歴", "高血圧", "糖尿病", "脂質異常症", "CKD(<60ml/min)",
                "脳卒中既往", "心不全", "血管疾患", "冠動脈虚血性疾患", "心筋症", "認知症", "その他",
                // 心房細動情報
                "心房細動タイプ", "心房細動症状", "CHADS2スコア", "CHA2DS2-VAScスコア",
        
                // 心エコー情報
                "IVSd(mm)", "LVPWd(mm)", "LVDd(mm)", "LVDs(mm)", "EDV(ml)", "ESV(ml)",
                "LAD(mm)", "LAV(ml)", "LVEF(%)", "心不全タイプ", "局所壁運動異常", "弁膜症",
                "E波(cm/s)", "A波(cm/s)", "E/A", "e'中隔(cm/s)", "E/e'", "TR-PG(mmHg)",
                "IVC吸気時(mm)", "IVC呼気時(mm)", "心エコーその他",
        
                // 血液検査情報
                "TP(g/dL)", "Alb(g/dL)", "BUN(mg/dL)", "Cre(mg/dL)", "CRP(mg/dL)", "CK(U/L)",
                "AST(U/L)", "ALT(U/L)", "LDL(mg/dL)", "HDL(mg/dL)", "TG(mg/dL)", "HbA1c(%)",
                "Glu(mg/dL)", "Hb(g/dL)", "WBC(/μL)", "Plt(/μL)", "PT-INR", "APTT(秒)",
                "FIB4-index", "UA(mg/dL)", "BNP(pg/mL)",
        
                // 心電図・レントゲン情報
                "ECG_HR", "ECG_Rhythm", "ECG_Axis", "ECG_ConductionDisturbance", "ECG_STTChange", "ECG_Comment",
                "XP_CTR(%)", "XP_LungField", "XP_CPAngle",
        
                // 薬物療法情報（すべて記載）
                "βブロッカー", "βブロッカー薬剤名1", "βブロッカー用量1(mg)", "βブロッカー薬剤名2", "βブロッカー用量2(mg)", "βブロッカー薬剤名3", "βブロッカー用量3(mg)",
                "CCB", "CCB薬剤名1", "CCB用量1(mg)", "CCB薬剤名2", "CCB用量2(mg)", "CCB薬剤名3", "CCB用量3(mg)",
                "抗不整脈薬", "抗不整脈薬薬剤名1", "抗不整脈薬用量1(mg)", "抗不整脈薬薬剤名2", "抗不整脈薬用量2(mg)", "抗不整脈薬薬剤名3", "抗不整脈薬用量3(mg)",
                "DOAC", "DOAC薬剤名1", "DOAC用量1(mg)", "DOAC薬剤名2", "DOAC用量2(mg)", "DOAC薬剤名3", "DOAC用量3(mg)",
                "VKA", "VKA用量(mg)",
                "スタチン", "スタチン薬剤名1", "スタチン用量1(mg)", "スタチン薬剤名2", "スタチン用量2(mg)", "スタチン薬剤名3", "スタチン用量3(mg)",
                "SGLT2i", "SGLT2i薬剤名1", "SGLT2i用量1(mg)", "SGLT2i薬剤名2", "SGLT2i用量2(mg)", "SGLT2i薬剤名3", "SGLT2i用量3(mg)",
                "RAAS", "RAAS薬剤名1", "RAAS用量1(mg)", "RAAS薬剤名2", "RAAS用量2(mg)", "RAAS薬剤名3", "RAAS用量3(mg)",
                "MRA", "MRA薬剤名1", "MRA用量1(mg)", "MRA薬剤名2", "MRA用量2(mg)", "MRA薬剤名3", "MRA用量3(mg)",
                "利尿薬", "利尿薬薬剤名1", "利尿薬用量1(mg)", "利尿薬薬剤名2", "利尿薬用量2(mg)", "利尿薬薬剤名3", "利尿薬用量3(mg)",
                "抗血小板薬", "抗血小板薬薬剤名1", "抗血小板薬用量1(mg)", "抗血小板薬薬剤名2", "抗血小板薬用量2(mg)", "抗血小板薬薬剤名3", "抗血小板薬用量3(mg)",
                "その他薬剤",
        
                // アブレーション情報
                "MappingSystem", "MappingRhythm", "PacingSite", "PreMap", "ProcedureCount",
                "ProcedurePVI", "ProcedurePosteriorWallIsolation", "ProcedureCFAE_FAAM", "ProcedureOther",
                "Result", "LVAs", "VGLA", "MaxVoltageAnterior", "MaxVoltageSeptum", "MaxVoltageRoof",
                "MaxVoltageInf", "MaxVoltagePost", "MaxVoltageLat", "MaxVoltageMean",
        
                // サンプリング情報（すべて記載）
                "LATotalADM", "LAMatureADM", "LAATX", "LAMTADMRatio",
                "CSTotalADM", "CSMatureADM", "CSATX", "CSMTADMRatio",
                "FVTotalADM", "FVMatureADM", "FVATX",
                "PATotalADM", "PAMatureADM", "PAATX",
                "DeltaTotalADM", "DeltaMatureADM", "DeltaATX",
                "CSLADeltaTotalADM", "CSLADeltaMatureADM", "CSLADeltaATX",
                "DeltaTotalADMBSA", "TotalADMBSA", "DeltaTotalRatio", "DeltaMatureRatio",
        
                // T-TAS情報
                "TTASPL", "TTASAR"
            };

            return string.Join(",", headers);
        }

        /// <summary>
        /// 患者データからCSVデータ行を生成
        /// </summary>
        /// <param name="data">患者データ</param>
        /// <returns>CSVデータ行</returns>
        private string GenerateCSVData(PatientData data)
        {
            List<string> values = new List<string>
            {
                // 患者基本情報
                EscapeCSVField(data.Id),
                EscapeCSVField(data.Gender),
                EscapeCSVField(data.Age),
                EscapeCSVField(data.Height),
                EscapeCSVField(data.Weight),
                EscapeCSVField(data.BSA),
                EscapeCSVField(data.BMI),
        
                // バイタル
                EscapeCSVField(data.SystolicBP),
                EscapeCSVField(data.DiastolicBP),
                EscapeCSVField(data.HeartRate),
                EscapeCSVField(data.Rhythm),
        
                // 生活歴・疾患
                EscapeCSVField(data.Alcohol),
                EscapeCSVField(data.Smoking),
                EscapeCSVField(data.Hypertension),
                EscapeCSVField(data.Diabetes),
                EscapeCSVField(data.Dyslipidemia),
                EscapeCSVField(data.CKD),
                EscapeCSVField(data.Stroke),
                EscapeCSVField(data.HeartFailure),
                EscapeCSVField(data.VascularDisease),
                EscapeCSVField(data.CoronaryIschemia),
                EscapeCSVField(data.Cardiomyopathy),
                EscapeCSVField(data.Dementia),
                EscapeCSVField(data.Others),
        
                // 心房細動情報
                EscapeCSVField(data.AtrialFibrillationType),
                EscapeCSVField(data.AtrialFibrillationSymptoms),
                EscapeCSVField(data.Chads2Score),
                EscapeCSVField(data.Cha2ds2VascScore),
        
                // 心エコー情報
                EscapeCSVField(data.IVSd),
                EscapeCSVField(data.LVPWd),
                EscapeCSVField(data.LVDd),
                EscapeCSVField(data.LVDs),
                EscapeCSVField(data.EDV),
                EscapeCSVField(data.ESV),
                EscapeCSVField(data.LAD),
                EscapeCSVField(data.LAV),
                EscapeCSVField(data.LVEF),
                EscapeCSVField(data.HeartFailureType),
                EscapeCSVField(data.FocalAsynergy),
                EscapeCSVField(data.VHD),
                EscapeCSVField(data.EWave),
                EscapeCSVField(data.AWave),
                EscapeCSVField(data.EARatio),
                EscapeCSVField(data.EPrimeSept),
                EscapeCSVField(data.EEPrimeRatio),
                EscapeCSVField(data.TRPG),
                EscapeCSVField(data.IVCInsp),
                EscapeCSVField(data.IVCExp),
                EscapeCSVField(data.OtherDisorder),
        
                // 血液検査情報
                EscapeCSVField(data.TP),
                EscapeCSVField(data.Alb),
                EscapeCSVField(data.BUN),
                EscapeCSVField(data.Cre),
                EscapeCSVField(data.CRP),
                EscapeCSVField(data.CK),
                EscapeCSVField(data.AST),
                EscapeCSVField(data.ALT),
                EscapeCSVField(data.LDL),
                EscapeCSVField(data.HDL),
                EscapeCSVField(data.TG),
                EscapeCSVField(data.HbA1c),
                EscapeCSVField(data.Glu),
                EscapeCSVField(data.Hb),
                EscapeCSVField(data.WBC),
                EscapeCSVField(data.Plt),
                EscapeCSVField(data.PTINR),
                EscapeCSVField(data.APTT),
                EscapeCSVField(data.Fib4i),
                EscapeCSVField(data.UA),
                EscapeCSVField(data.BNP),
        
                // 心電図・レントゲン情報
                EscapeCSVField(data.ECG_HR),
                EscapeCSVField(data.ECG_Rhythm),
                EscapeCSVField(data.ECG_Axis),
                EscapeCSVField(data.ECG_ConductionDisturbance),
                EscapeCSVField(data.ECG_STTChange),
                EscapeCSVField(data.ECG_Comment),
                EscapeCSVField(data.XP_CTR),
                EscapeCSVField(data.XP_LungField),
                EscapeCSVField(data.XP_CPAngle),
        
                // 薬物療法情報（すべて記載）
                EscapeCSVField(data.BetaBlocker),
                EscapeCSVField(data.BetaBlockerName1),
                EscapeCSVField(data.BetaBlockerDose1),
                EscapeCSVField(data.BetaBlockerName2),
                EscapeCSVField(data.BetaBlockerDose2),
                EscapeCSVField(data.BetaBlockerName3),
                EscapeCSVField(data.BetaBlockerDose3),

                EscapeCSVField(data.CCB),
                EscapeCSVField(data.CCBName1),
                EscapeCSVField(data.CCBDose1),
                EscapeCSVField(data.CCBName2),
                EscapeCSVField(data.CCBDose2),
                EscapeCSVField(data.CCBName3),
                EscapeCSVField(data.CCBDose3),

                EscapeCSVField(data.AntiArrhythmicDrug),
                EscapeCSVField(data.AntiArrhythmicDrugName1),
                EscapeCSVField(data.AntiArrhythmicDrugDose1),
                EscapeCSVField(data.AntiArrhythmicDrugName2),
                EscapeCSVField(data.AntiArrhythmicDrugDose2),
                EscapeCSVField(data.AntiArrhythmicDrugName3),
                EscapeCSVField(data.AntiArrhythmicDrugDose3),

                EscapeCSVField(data.DOAC),
                EscapeCSVField(data.DOACName1),
                EscapeCSVField(data.DOACDose1),
                EscapeCSVField(data.DOACName2),
                EscapeCSVField(data.DOACDose2),
                EscapeCSVField(data.DOACName3),
                EscapeCSVField(data.DOACDose3),

                EscapeCSVField(data.VKA),
                EscapeCSVField(data.VKADose),

                EscapeCSVField(data.Statin),
                EscapeCSVField(data.StatinName1),
                EscapeCSVField(data.StatinDose1),
                EscapeCSVField(data.StatinName2),
                EscapeCSVField(data.StatinDose2),
                EscapeCSVField(data.StatinName3),
                EscapeCSVField(data.StatinDose3),

                EscapeCSVField(data.SGLT2i),
                EscapeCSVField(data.SGLT2iName1),
                EscapeCSVField(data.SGLT2iDose1),
                EscapeCSVField(data.SGLT2iName2),
                EscapeCSVField(data.SGLT2iDose2),
                EscapeCSVField(data.SGLT2iName3),
                EscapeCSVField(data.SGLT2iDose3),

                EscapeCSVField(data.RAAS),
                EscapeCSVField(data.RAASName1),
                EscapeCSVField(data.RAASDose1),
                EscapeCSVField(data.RAASName2),
                EscapeCSVField(data.RAASDose2),
                EscapeCSVField(data.RAASName3),
                EscapeCSVField(data.RAASDose3),

                EscapeCSVField(data.MRA),
                EscapeCSVField(data.MRAName1),
                EscapeCSVField(data.MRADose1),
                EscapeCSVField(data.MRAName2),
                EscapeCSVField(data.MRADose2),
                EscapeCSVField(data.MRAName3),
                EscapeCSVField(data.MRADose3),

                EscapeCSVField(data.Diuretics),
                EscapeCSVField(data.DiureticsName1),
                EscapeCSVField(data.DiureticsDose1),
                EscapeCSVField(data.DiureticsName2),
                EscapeCSVField(data.DiureticsDose2),
                EscapeCSVField(data.DiureticsName3),
                EscapeCSVField(data.DiureticsDose3),

                EscapeCSVField(data.AntiplateletAgent),
                EscapeCSVField(data.AntiplateletAgentName1),
                EscapeCSVField(data.AntiplateletAgentDose1),
                EscapeCSVField(data.AntiplateletAgentName2),
                EscapeCSVField(data.AntiplateletAgentDose2),
                EscapeCSVField(data.AntiplateletAgentName3),
                EscapeCSVField(data.AntiplateletAgentDose3),

                EscapeCSVField(data.OtherMedications),
        
                // アブレーション情報
                EscapeCSVField(data.MappingSystem),
                EscapeCSVField(data.MappingRhythm),
                EscapeCSVField(data.PacingSite),
                EscapeCSVField(data.PreMap),
                EscapeCSVField(data.ProcedureCount),
                EscapeCSVField(data.ProcedurePVI.ToString()),
                EscapeCSVField(data.ProcedurePosteriorWallIsolation.ToString()),
                EscapeCSVField(data.ProcedureCFAE_FAAM.ToString()),
                EscapeCSVField(data.ProcedureOther),
                EscapeCSVField(data.Result),
                EscapeCSVField(data.LVAs),
                EscapeCSVField(data.VGLA),
                EscapeCSVField(data.MaxVoltageAnterior),
                EscapeCSVField(data.MaxVoltageSeptum),
                EscapeCSVField(data.MaxVoltageRoof),
                EscapeCSVField(data.MaxVoltageInf),
                EscapeCSVField(data.MaxVoltagePost),
                EscapeCSVField(data.MaxVoltageLat),
                EscapeCSVField(data.MaxVoltageMean),
        
                // サンプリング情報（すべて記載）
                EscapeCSVField(data.LATotalADM),
                EscapeCSVField(data.LAMatureADM),
                EscapeCSVField(data.LAATX),
                EscapeCSVField(data.LAMTADMRatio),

                EscapeCSVField(data.CSTotalADM),
                EscapeCSVField(data.CSMatureADM),
                EscapeCSVField(data.CSATX),
                EscapeCSVField(data.CSMTADMRatio),

                EscapeCSVField(data.FVTotalADM),
                EscapeCSVField(data.FVMatureADM),
                EscapeCSVField(data.FVATX),

                EscapeCSVField(data.PATotalADM),
                EscapeCSVField(data.PAMatureADM),
                EscapeCSVField(data.PAATX),

                EscapeCSVField(data.DeltaTotalADM),
                EscapeCSVField(data.DeltaMatureADM),
                EscapeCSVField(data.DeltaATX),

                EscapeCSVField(data.CSLADeltaTotalADM),
                EscapeCSVField(data.CSLADeltaMatureADM),
                EscapeCSVField(data.CSLADeltaATX),

                EscapeCSVField(data.DeltaTotalADMBSA),
                EscapeCSVField(data.TotalADMBSA),
                EscapeCSVField(data.DeltaTotalRatio),
                EscapeCSVField(data.DeltaMatureRatio),
        
                // T-TAS情報
                EscapeCSVField(data.TTASPL),
                EscapeCSVField(data.TTASAR)
            };

            return string.Join(",", values);
        }
        /// <summary>
        /// CSV用にフィールドをエスケープ
        /// </summary>
        /// <param name="field">エスケープするフィールド</param>
        /// <returns>エスケープされたフィールド</returns>
        private string EscapeCSVField(string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return "";
            }

            // ダブルクォートを含む場合は、ダブルクォートをダブルクォートでエスケープし、フィールド全体をダブルクォートで囲む
            if (field.Contains("\"") || field.Contains(",") || field.Contains("\n"))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }

            return field;
        }
    }
}