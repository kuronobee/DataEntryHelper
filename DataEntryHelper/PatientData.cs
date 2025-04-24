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
    }
}