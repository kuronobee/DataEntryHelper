using System;

namespace DataEntryHelper
{
    /// <summary>
    /// 患者データを保持するクラス
    /// </summary>
    public class PatientData
    {
        #region 患者基本情報
        /// <summary>
        /// 患者ID
        /// </summary>
        public string Id { get; set; } = "";

        /// <summary>
        /// 性別
        /// </summary>
        public string Gender { get; set; } = "";

        /// <summary>
        /// 年齢
        /// </summary>
        public string Age { get; set; } = "";

        /// <summary>
        /// 身長(cm)
        /// </summary>
        public string Height { get; set; } = "";

        /// <summary>
        /// 体重(kg)
        /// </summary>
        public string Weight { get; set; } = "";

        /// <summary>
        /// 体表面積(m²)
        /// </summary>
        public string BSA { get; set; } = "";

        /// <summary>
        /// BMI
        /// </summary>
        public string BMI { get; set; } = "";
        #endregion

        #region バイタルサイン
        /// <summary>
        /// 収縮期血圧(mmHg)
        /// </summary>
        public string SystolicBP { get; set; } = "";

        /// <summary>
        /// 拡張期血圧(mmHg)
        /// </summary>
        public string DiastolicBP { get; set; } = "";

        /// <summary>
        /// 心拍数(/min)
        /// </summary>
        public string HeartRate { get; set; } = "";

        /// <summary>
        /// リズム
        /// </summary>
        public string Rhythm { get; set; } = "";
        #endregion

        #region 生活歴・既往歴
        /// <summary>
        /// アルコール摂取量(g/day)
        /// </summary>
        public string Alcohol { get; set; } = "";

        /// <summary>
        /// 喫煙歴
        /// </summary>
        public string Smoking { get; set; } = "";

        /// <summary>
        /// 高血圧
        /// </summary>
        public string Hypertension { get; set; } = "";

        /// <summary>
        /// 糖尿病
        /// </summary>
        public string Diabetes { get; set; } = "";

        /// <summary>
        /// 脂質異常症
        /// </summary>
        public string Dyslipidemia { get; set; } = "";

        /// <summary>
        /// 慢性腎臓病(CKD)
        /// </summary>
        public string CKD { get; set; } = "";

        /// <summary>
        /// 脳卒中既往
        /// </summary>
        public string Stroke { get; set; } = "";

        /// <summary>
        /// 心不全
        /// </summary>
        public string HeartFailure { get; set; } = "";

        /// <summary>
        /// 血管疾患
        /// </summary>
        public string VascularDisease { get; set; } = "";

        /// <summary>
        /// 冠動脈虚血性疾患
        /// </summary>
        public string CoronaryIschemia { get; set; } = "";

        /// <summary>
        /// 心筋症
        /// </summary>
        public string Cardiomyopathy { get; set; } = "";

        /// <summary>
        /// 認知症
        /// </summary>
        public string Dementia { get; set; } = "";

        /// <summary>
        /// その他疾患
        /// </summary>
        public string Others { get; set; } = "";
        #endregion

        #region 心房細動情報
        /// <summary>
        /// 心房細動タイプ
        /// </summary>
        public string AtrialFibrillationType { get; set; } = "";

        /// <summary>
        /// 心房細動の症状
        /// </summary>
        public string AtrialFibrillationSymptoms { get; set; } = "";

        /// <summary>
        /// CHADS2スコア
        /// </summary>
        public string Chads2Score { get; set; } = "";

        /// <summary>
        /// CHA2DS2-VAScスコア
        /// </summary>
        public string Cha2ds2VascScore { get; set; } = "";
        #endregion

        #region 心エコー情報
        /// <summary>
        /// 心室中隔厚(拡張期)(mm)
        /// </summary>
        public string IVSd { get; set; } = "";

        /// <summary>
        /// 左室後壁厚(拡張期)(mm)
        /// </summary>
        public string LVPWd { get; set; } = "";

        /// <summary>
        /// 左室拡張末期径(mm)
        /// </summary>
        public string LVDd { get; set; } = "";

        /// <summary>
        /// 左室収縮末期径(mm)
        /// </summary>
        public string LVDs { get; set; } = "";

        /// <summary>
        /// 左室拡張末期容積(ml)
        /// </summary>
        public string EDV { get; set; } = "";

        /// <summary>
        /// 左室収縮末期容積(ml)
        /// </summary>
        public string ESV { get; set; } = "";

        /// <summary>
        /// 左房径(mm)
        /// </summary>
        public string LAD { get; set; } = "";

        /// <summary>
        /// 左房容積(ml)
        /// </summary>
        public string LAV { get; set; } = "";

        /// <summary>
        /// 左室駆出率(%)
        /// </summary>
        public string LVEF { get; set; } = "";

        /// <summary>
        /// 心不全タイプ
        /// </summary>
        public string HeartFailureType { get; set; } = "";

        /// <summary>
        /// 局所壁運動異常
        /// </summary>
        public string FocalAsynergy { get; set; } = "";

        /// <summary>
        /// 弁膜症(中等度以上)
        /// </summary>
        public string VHD { get; set; } = "";

        /// <summary>
        /// E波(cm/s)
        /// </summary>
        public string EWave { get; set; } = "";

        /// <summary>
        /// A波(cm/s)
        /// </summary>
        public string AWave { get; set; } = "";

        /// <summary>
        /// E/A比
        /// </summary>
        public string EARatio { get; set; } = "";

        /// <summary>
        /// e'中隔(cm/s)
        /// </summary>
        public string EPrimeSept { get; set; } = "";

        /// <summary>
        /// E/e'比
        /// </summary>
        public string EEPrimeRatio { get; set; } = "";

        /// <summary>
        /// 三尖弁圧較差(mmHg)
        /// </summary>
        public string TRPG { get; set; } = "";

        /// <summary>
        /// 下大静脈径(吸気時)(mm)
        /// </summary>
        public string IVCInsp { get; set; } = "";

        /// <summary>
        /// 下大静脈径(呼気時)(mm)
        /// </summary>
        public string IVCExp { get; set; } = "";

        /// <summary>
        /// その他の所見
        /// </summary>
        public string OtherDisorder { get; set; } = "";

        /// <summary>
        /// 心エコーレポート概要
        /// </summary>
        public string EchoSummary { get; set; } = "";
        #endregion

        #region 血液検査情報
        /// <summary>
        /// 総蛋白(g/dL)
        /// </summary>
        public string TP { get; set; } = "";

        /// <summary>
        /// アルブミン(g/dL)
        /// </summary>
        public string Alb { get; set; } = "";

        /// <summary>
        /// 尿素窒素(mg/dL)
        /// </summary>
        public string BUN { get; set; } = "";

        /// <summary>
        /// クレアチニン(mg/dL)
        /// </summary>
        public string Cre { get; set; } = "";

        /// <summary>
        /// C反応性蛋白(mg/dL)
        /// </summary>
        public string CRP { get; set; } = "";

        /// <summary>
        /// クレアチンキナーゼ(U/L)
        /// </summary>
        public string CK { get; set; } = "";

        /// <summary>
        /// アスパラギン酸アミノトランスフェラーゼ(U/L)
        /// </summary>
        public string AST { get; set; } = "";

        /// <summary>
        /// アラニンアミノトランスフェラーゼ(U/L)
        /// </summary>
        public string ALT { get; set; } = "";

        /// <summary>
        /// LDLコレステロール(mg/dL)
        /// </summary>
        public string LDL { get; set; } = "";

        /// <summary>
        /// HDLコレステロール(mg/dL)
        /// </summary>
        public string HDL { get; set; } = "";

        /// <summary>
        /// 中性脂肪(mg/dL)
        /// </summary>
        public string TG { get; set; } = "";

        /// <summary>
        /// ヘモグロビンA1c(%)
        /// </summary>
        public string HbA1c { get; set; } = "";

        /// <summary>
        /// 血糖(mg/dL)
        /// </summary>
        public string Glu { get; set; } = "";

        /// <summary>
        /// ヘモグロビン(g/dL)
        /// </summary>
        public string Hb { get; set; } = "";

        /// <summary>
        /// 白血球数(/μL)
        /// </summary>
        public string WBC { get; set; } = "";

        /// <summary>
        /// 血小板数(/μL)
        /// </summary>
        public string Plt { get; set; } = "";

        /// <summary>
        /// プロトロンビン時間国際標準比(-)
        /// </summary>
        public string PTINR { get; set; } = "";

        /// <summary>
        /// 活性化部分トロンボプラスチン時間(秒)
        /// </summary>
        public string APTT { get; set; } = "";

        /// <summary>
        /// 線維化指数4(-)
        /// </summary>
        public string Fib4i { get; set; } = "";

        /// <summary>
        /// 尿酸(mg/dL)
        /// </summary>
        public string UA { get; set; } = "";

        /// <summary>
        /// 脳性ナトリウム利尿ペプチド(pg/mL)
        /// </summary>
        public string BNP { get; set; } = "";

        /// <summary>
        /// 異常値リスト
        /// </summary>
        public string AbnormalValues { get; set; } = "";

        /// <summary>
        /// 臨床的意義
        /// </summary>
        public string ClinicalImplications { get; set; } = "";
        #endregion
        #region 心電図・レントゲン情報
        /// <summary>
        /// 心拍数（心電図）
        /// </summary>
        public string ECG_HR { get; set; } = "";

        /// <summary>
        /// 調律（心電図）
        /// </summary>
        public string ECG_Rhythm { get; set; } = "";

        /// <summary>
        /// 電気軸（心電図）
        /// </summary>
        public string ECG_Axis { get; set; } = "";

        /// <summary>
        /// 伝導障害（心電図）
        /// </summary>
        public string ECG_ConductionDisturbance { get; set; } = "";

        /// <summary>
        /// ST-T変化（心電図）
        /// </summary>
        public string ECG_STTChange { get; set; } = "";

        /// <summary>
        /// コメント（心電図）
        /// </summary>
        public string ECG_Comment { get; set; } = "";

        /// <summary>
        /// 心胸郭比（レントゲン）
        /// </summary>
        public string XP_CTR { get; set; } = "";

        /// <summary>
        /// 肺野（レントゲン）
        /// </summary>
        public string XP_LungField { get; set; } = "";

        /// <summary>
        /// 肋骨横隔膜角（レントゲン）
        /// </summary>
        public string XP_CPAngle { get; set; } = "";
        /// <summary>
        /// 心電図所見要約
        /// </summary>
        public string ECG_Summary { get; set; } = "";

        /// <summary>
        /// レントゲン所見要約
        /// </summary>
        public string XP_Summary { get; set; } = "";
        #endregion
        #region 薬物療法情報
        /// <summary>
        /// βブロッカー使用有無
        /// </summary>
        public string BetaBlocker { get; set; } = "";

        /// <summary>
        /// βブロッカー薬剤名1
        /// </summary>
        public string BetaBlockerName1 { get; set; } = "";

        /// <summary>
        /// βブロッカー用量1(mg)
        /// </summary>
        public string BetaBlockerDose1 { get; set; } = "";

        /// <summary>
        /// βブロッカー薬剤名2
        /// </summary>
        public string BetaBlockerName2 { get; set; } = "";

        /// <summary>
        /// βブロッカー用量2(mg)
        /// </summary>
        public string BetaBlockerDose2 { get; set; } = "";

        /// <summary>
        /// βブロッカー薬剤名3
        /// </summary>
        public string BetaBlockerName3 { get; set; } = "";

        /// <summary>
        /// βブロッカー用量3(mg)
        /// </summary>
        public string BetaBlockerDose3 { get; set; } = "";

        /// <summary>
        /// カルシウム拮抗薬使用有無
        /// </summary>
        public string CCB { get; set; } = "";

        /// <summary>
        /// カルシウム拮抗薬薬剤名1
        /// </summary>
        public string CCBName1 { get; set; } = "";

        /// <summary>
        /// カルシウム拮抗薬用量1(mg)
        /// </summary>
        public string CCBDose1 { get; set; } = "";

        /// <summary>
        /// カルシウム拮抗薬薬剤名2
        /// </summary>
        public string CCBName2 { get; set; } = "";

        /// <summary>
        /// カルシウム拮抗薬用量2(mg)
        /// </summary>
        public string CCBDose2 { get; set; } = "";

        /// <summary>
        /// カルシウム拮抗薬薬剤名3
        /// </summary>
        public string CCBName3 { get; set; } = "";

        /// <summary>
        /// カルシウム拮抗薬用量3(mg)
        /// </summary>
        public string CCBDose3 { get; set; } = "";

        /// <summary>
        /// 抗不整脈薬使用有無
        /// </summary>
        public string AntiArrhythmicDrug { get; set; } = "";

        /// <summary>
        /// 抗不整脈薬薬剤名1
        /// </summary>
        public string AntiArrhythmicDrugName1 { get; set; } = "";

        /// <summary>
        /// 抗不整脈薬用量1(mg)
        /// </summary>
        public string AntiArrhythmicDrugDose1 { get; set; } = "";

        /// <summary>
        /// 抗不整脈薬薬剤名2
        /// </summary>
        public string AntiArrhythmicDrugName2 { get; set; } = "";

        /// <summary>
        /// 抗不整脈薬用量2(mg)
        /// </summary>
        public string AntiArrhythmicDrugDose2 { get; set; } = "";

        /// <summary>
        /// 抗不整脈薬薬剤名3
        /// </summary>
        public string AntiArrhythmicDrugName3 { get; set; } = "";

        /// <summary>
        /// 抗不整脈薬用量3(mg)
        /// </summary>
        public string AntiArrhythmicDrugDose3 { get; set; } = "";

        /// <summary>
        /// DOAC使用有無
        /// </summary>
        public string DOAC { get; set; } = "";

        /// <summary>
        /// DOAC薬剤名1
        /// </summary>
        public string DOACName1 { get; set; } = "";

        /// <summary>
        /// DOAC用量1(mg)
        /// </summary>
        public string DOACDose1 { get; set; } = "";

        /// <summary>
        /// DOAC薬剤名2
        /// </summary>
        public string DOACName2 { get; set; } = "";

        /// <summary>
        /// DOAC用量2(mg)
        /// </summary>
        public string DOACDose2 { get; set; } = "";

        /// <summary>
        /// DOAC薬剤名3
        /// </summary>
        public string DOACName3 { get; set; } = "";

        /// <summary>
        /// DOAC用量3(mg)
        /// </summary>
        public string DOACDose3 { get; set; } = "";

        /// <summary>
        /// VKA使用有無
        /// </summary>
        public string VKA { get; set; } = "";

        /// <summary>
        /// VKA用量(mg)
        /// </summary>
        public string VKADose { get; set; } = "";

        /// <summary>
        /// スタチン使用有無
        /// </summary>
        public string Statin { get; set; } = "";

        /// <summary>
        /// スタチン薬剤名1
        /// </summary>
        public string StatinName1 { get; set; } = "";

        /// <summary>
        /// スタチン用量1(mg)
        /// </summary>
        public string StatinDose1 { get; set; } = "";

        /// <summary>
        /// スタチン薬剤名2
        /// </summary>
        public string StatinName2 { get; set; } = "";

        /// <summary>
        /// スタチン用量2(mg)
        /// </summary>
        public string StatinDose2 { get; set; } = "";

        /// <summary>
        /// スタチン薬剤名3
        /// </summary>
        public string StatinName3 { get; set; } = "";

        /// <summary>
        /// スタチン用量3(mg)
        /// </summary>
        public string StatinDose3 { get; set; } = "";

        /// <summary>
        /// SGLT2阻害薬使用有無
        /// </summary>
        public string SGLT2i { get; set; } = "";

        /// <summary>
        /// SGLT2阻害薬薬剤名1
        /// </summary>
        public string SGLT2iName1 { get; set; } = "";

        /// <summary>
        /// SGLT2阻害薬用量1(mg)
        /// </summary>
        public string SGLT2iDose1 { get; set; } = "";

        /// <summary>
        /// SGLT2阻害薬薬剤名2
        /// </summary>
        public string SGLT2iName2 { get; set; } = "";

        /// <summary>
        /// SGLT2阻害薬用量2(mg)
        /// </summary>
        public string SGLT2iDose2 { get; set; } = "";

        /// <summary>
        /// SGLT2阻害薬薬剤名3
        /// </summary>
        public string SGLT2iName3 { get; set; } = "";

        /// <summary>
        /// SGLT2阻害薬用量3(mg)
        /// </summary>
        public string SGLT2iDose3 { get; set; } = "";

        /// <summary>
        /// ACE阻害薬/ARB/ARNi使用有無
        /// </summary>
        public string RAAS { get; set; } = "";

        /// <summary>
        /// ACE阻害薬/ARB/ARNi薬剤名1
        /// </summary>
        public string RAASName1 { get; set; } = "";

        /// <summary>
        /// ACE阻害薬/ARB/ARNi用量1(mg)
        /// </summary>
        public string RAASDose1 { get; set; } = "";

        /// <summary>
        /// ACE阻害薬/ARB/ARNi薬剤名2
        /// </summary>
        public string RAASName2 { get; set; } = "";

        /// <summary>
        /// ACE阻害薬/ARB/ARNi用量2(mg)
        /// </summary>
        public string RAASDose2 { get; set; } = "";

        /// <summary>
        /// ACE阻害薬/ARB/ARNi薬剤名3
        /// </summary>
        public string RAASName3 { get; set; } = "";

        /// <summary>
        /// ACE阻害薬/ARB/ARNi用量3(mg)
        /// </summary>
        public string RAASDose3 { get; set; } = "";

        /// <summary>
        /// MRA使用有無
        /// </summary>
        public string MRA { get; set; } = "";

        /// <summary>
        /// MRA薬剤名1
        /// </summary>
        public string MRAName1 { get; set; } = "";

        /// <summary>
        /// MRA用量1(mg)
        /// </summary>
        public string MRADose1 { get; set; } = "";

        /// <summary>
        /// MRA薬剤名2
        /// </summary>
        public string MRAName2 { get; set; } = "";

        /// <summary>
        /// MRA用量2(mg)
        /// </summary>
        public string MRADose2 { get; set; } = "";

        /// <summary>
        /// MRA薬剤名3
        /// </summary>
        public string MRAName3 { get; set; } = "";

        /// <summary>
        /// MRA用量3(mg)
        /// </summary>
        public string MRADose3 { get; set; } = "";

        /// <summary>
        /// 利尿薬使用有無
        /// </summary>
        public string Diuretics { get; set; } = "";

        /// <summary>
        /// 利尿薬薬剤名1
        /// </summary>
        public string DiureticsName1 { get; set; } = "";

        /// <summary>
        /// 利尿薬用量1(mg)
        /// </summary>
        public string DiureticsDose1 { get; set; } = "";

        /// <summary>
        /// 利尿薬薬剤名2
        /// </summary>
        public string DiureticsName2 { get; set; } = "";

        /// <summary>
        /// 利尿薬用量2(mg)
        /// </summary>
        public string DiureticsDose2 { get; set; } = "";

        /// <summary>
        /// 利尿薬薬剤名3
        /// </summary>
        public string DiureticsName3 { get; set; } = "";

        /// <summary>
        /// 利尿薬用量3(mg)
        /// </summary>
        public string DiureticsDose3 { get; set; } = "";

        /// <summary>
        /// 抗血小板薬使用有無
        /// </summary>
        public string AntiplateletAgent { get; set; } = "";

        /// <summary>
        /// 抗血小板薬薬剤名1
        /// </summary>
        public string AntiplateletAgentName1 { get; set; } = "";

        /// <summary>
        /// 抗血小板薬用量1(mg)
        /// </summary>
        public string AntiplateletAgentDose1 { get; set; } = "";

        /// <summary>
        /// 抗血小板薬薬剤名2
        /// </summary>
        public string AntiplateletAgentName2 { get; set; } = "";

        /// <summary>
        /// 抗血小板薬用量2(mg)
        /// </summary>
        public string AntiplateletAgentDose2 { get; set; } = "";

        /// <summary>
        /// 抗血小板薬薬剤名3
        /// </summary>
        public string AntiplateletAgentName3 { get; set; } = "";

        /// <summary>
        /// 抗血小板薬用量3(mg)
        /// </summary>
        public string AntiplateletAgentDose3 { get; set; } = "";

        /// <summary>
        /// その他の薬剤情報
        /// </summary>
        public string OtherMedications { get; set; } = "";

        /// <summary>
        /// 薬物療法要約
        /// </summary>
        public string MedicationSummary { get; set; } = "";
        #endregion
        // PatientData.cs に追加

        #region アブレーション情報
        /// <summary>
        /// マッピングシステム
        /// </summary>
        public string MappingSystem { get; set; } = "";

        /// <summary>
        /// マッピングリズム
        /// </summary>
        public string MappingRhythm { get; set; } = "";

        /// <summary>
        /// ペーシング部位
        /// </summary>
        public string PacingSite { get; set; } = "";

        /// <summary>
        /// プレマップ
        /// </summary>
        public string PreMap { get; set; } = "";

        /// <summary>
        /// 施行回数
        /// </summary>
        public string ProcedureCount { get; set; } = "";

        /// <summary>
        /// 処置内容: PVI
        /// </summary>
        public bool ProcedurePVI { get; set; } = false;

        /// <summary>
        /// 処置内容: 後壁隔離
        /// </summary>
        public bool ProcedurePosteriorWallIsolation { get; set; } = false;

        /// <summary>
        /// 処置内容: CFAE/FAAM
        /// </summary>
        public bool ProcedureCFAE_FAAM { get; set; } = false;

        /// <summary>
        /// 処置内容: その他
        /// </summary>
        public string ProcedureOther { get; set; } = "";

        /// <summary>
        /// 結果
        /// </summary>
        public string Result { get; set; } = "";

        /// <summary>
        /// LVAs(&lt;0.5mV)(cm^2)
        /// </summary>
        public string LVAs { get; set; } = "";

        /// <summary>
        /// V GLA(voltage global lt. atria)(mV)
        /// </summary>
        public string VGLA { get; set; } = "";

        /// <summary>
        /// LA Mapping Max voltage Anterior(mV)
        /// </summary>
        public string MaxVoltageAnterior { get; set; } = "";

        /// <summary>
        /// LA Mapping Max voltage Septum(mV)
        /// </summary>
        public string MaxVoltageSeptum { get; set; } = "";

        /// <summary>
        /// LA Mapping Max voltage Roof(mV)
        /// </summary>
        public string MaxVoltageRoof { get; set; } = "";

        /// <summary>
        /// LA Mapping Max voltage Inf(mV)
        /// </summary>
        public string MaxVoltageInf { get; set; } = "";

        /// <summary>
        /// LA Mapping Max voltage Post(mV)
        /// </summary>
        public string MaxVoltagePost { get; set; } = "";

        /// <summary>
        /// LA Mapping Max voltage Lat(mV)
        /// </summary>
        public string MaxVoltageLat { get; set; } = "";

        /// <summary>
        /// LA Mapping Max voltage mean(mV)
        /// </summary>
        public string MaxVoltageMean { get; set; } = "";

        /// <summary>
        /// アブレーションレポート概要
        /// </summary>
        public string AblationSummary { get; set; } = "";
        #endregion
    }
}