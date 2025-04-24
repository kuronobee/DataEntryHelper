using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntryHelper
{
    // 患者データを保持するクラス
    public class PatientData
    {
        // 患者情報
        public string Id { get; set; } = "";
        public string Gender { get; set; } = "";
        public string Age { get; set; } = "";
        public string Height { get; set; } = "";
        public string Weight { get; set; } = "";
        public string BSA { get; set; } = "";
        public string BMI { get; set; } = "";

        // バイタル
        public string SystolicBP { get; set; } = "";
        public string DiastolicBP { get; set; } = "";
        public string HeartRate { get; set; } = "";
        public string Rhythm { get; set; } = "";

        // 生活歴・疾患
        public string Alcohol { get; set; } = "";
        public string Smoking { get; set; } = "";
        public string Hypertension { get; set; } = "";
        public string Diabetes { get; set; } = "";
        public string Dyslipidemia { get; set; } = "";
        public string CKD { get; set; } = "";
        public string Stroke { get; set; } = "";
        public string HeartFailure { get; set; } = "";
        public string VascularDisease { get; set; } = "";
        public string CoronaryIschemia { get; set; } = "";
        public string Cardiomyopathy { get; set; } = "";
        public string Dementia { get; set; } = "";
        public string Others { get; set; } = "";

        // 心房細動情報
        public string AtrialFibrillationType { get; set; } = "";
        public string AtrialFibrillationSymptoms { get; set; } = "";
        public string Chads2Score { get; set; } = "";
        public string Cha2ds2VascScore { get; set; } = "";
    }
}