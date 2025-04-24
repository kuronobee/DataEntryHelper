using DataEntryHelper.Entity;
using System.Collections.Generic;

namespace DataEntryHelper.Models
{
    public class PatientFieldModel
    {
        // Patient information fields
        public static readonly Field IdField = new Field
        {
            Id = "ID",
            Name = "ID",
            FieldType = FieldType.String
        };

        public static readonly Field GenderField = new Field
        {
            Id = "Gender",
            Name = "性別",
            FieldType = FieldType.Selection,
            FieldValues = new List<string> { "男性", "女性" }
        };

        public static readonly Field AgeField = new Field
        {
            Id = "Age",
            Name = "年齢",
            FieldType = FieldType.Number
        };

        public static readonly Field HeightField = new Field
        {
            Id = "Height",
            Name = "身長",
            FieldType = FieldType.Number,
            Unit = "cm"
        };

        public static readonly Field WeightField = new Field
        {
            Id = "Weight",
            Name = "体重",
            FieldType = FieldType.Number,
            Unit = "kg"
        };

        public static readonly Field BsaField = new Field
        {
            Id = "BSA",
            Name = "体表面積",
            FieldType = FieldType.Calculate,
            Unit = "m²",
            Relation = new List<Field> { HeightField, WeightField }
        };

        public static readonly Field BmiField = new Field
        {
            Id = "BMI",
            Name = "BMI",
            FieldType = FieldType.Calculate,
            Relation = new List<Field> { HeightField, WeightField }
        };

        // Vital signs fields
        public static readonly Field SystolicBpField = new Field
        {
            Id = "SystolicBP",
            Name = "収縮期BP",
            FieldType = FieldType.Number,
            Unit = "mmHg"
        };

        public static readonly Field DiastolicBpField = new Field
        {
            Id = "DiastolicBP",
            Name = "拡張期BP",
            FieldType = FieldType.Number,
            Unit = "mmHg"
        };

        public static readonly Field HeartRateField = new Field
        {
            Id = "HeartRate",
            Name = "入院時心拍数",
            FieldType = FieldType.Number,
            Unit = "/min"
        };

        public static readonly Field RhythmField = new Field
        {
            Id = "Rhythm",
            Name = "入院時リズム",
            FieldType = FieldType.Selection,
            FieldValues = new List<string> { "整", "不整" }
        };

        // Lifestyle and disease history fields
        public static readonly Field AlcoholField = new Field
        {
            Id = "Alcohol",
            Name = "アルコール",
            FieldType = FieldType.Number,
            Unit = "g/day"
        };

        public static readonly Field SmokingField = new Field
        {
            Id = "Smoking",
            Name = "喫煙歴",
            FieldType = FieldType.Selection,
            FieldValues = new List<string> { "current", "past", "none" }
        };

        public static readonly Field HypertensionField = new Field
        {
            Id = "Hypertension",
            Name = "高血圧",
            FieldType = FieldType.Selection,
            FieldValues = new List<string> { "あり", "なし" }
        };

        public static readonly Field DiabetesField = new Field
        {
            Id = "Diabetes",
            Name = "糖尿病",
            FieldType = FieldType.Selection,
            FieldValues = new List<string> { "あり", "なし" }
        };

        public static readonly Field DyslipidemiaField = new Field
        {
            Id = "Dyslipidemia",
            Name = "脂質異常症",
            FieldType = FieldType.Selection,
            FieldValues = new List<string> { "あり", "なし" }
        };

        public static readonly Field CkdField = new Field
        {
            Id = "CKD",
            Name = "CKD(<60ml/min)",
            FieldType = FieldType.Selection,
            FieldValues = new List<string> { "あり", "なし" }
        };

        public static readonly Field StrokeField = new Field
        {
            Id = "Stroke",
            Name = "stroke既往",
            FieldType = FieldType.Selection,
            FieldValues = new List<string> { "あり", "なし" }
        };

        public static readonly Field HeartFailureField = new Field
        {
            Id = "HeartFailure",
            Name = "心不全",
            FieldType = FieldType.Selection,
            FieldValues = new List<string> { "あり", "なし" }
        };

        public static readonly Field VascularDiseaseField = new Field
        {
            Id = "VascularDisease",
            Name = "Vascular Disease",
            FieldType = FieldType.Selection,
            FieldValues = new List<string> { "あり", "なし" }
        };

        public static readonly Field CoronaryIschemiaField = new Field
        {
            Id = "CoronaryIschemia",
            Name = "coronary ischemia history",
            FieldType = FieldType.Selection,
            FieldValues = new List<string> { "あり", "なし" }
        };

        public static readonly Field CardiomyopathyField = new Field
        {
            Id = "Cardiomyopathy",
            Name = "cardiomyopathy",
            FieldType = FieldType.Selection,
            FieldValues = new List<string> { "あり", "なし" }
        };

        public static readonly Field DementiaField = new Field
        {
            Id = "Dementia",
            Name = "dimentia",
            FieldType = FieldType.Selection,
            FieldValues = new List<string> { "あり", "なし" }
        };

        public static readonly Field OthersField = new Field
        {
            Id = "Others",
            Name = "others",
            FieldType = FieldType.String
        };

        // Get all fields as a list
        public static List<Field> GetAllFields()
        {
            return new List<Field>
            {
                IdField, GenderField, AgeField, HeightField, WeightField, BsaField, BmiField,
                SystolicBpField, DiastolicBpField, HeartRateField, RhythmField,
                AlcoholField, SmokingField, HypertensionField, DiabetesField, DyslipidemiaField,
                CkdField, StrokeField, HeartFailureField, VascularDiseaseField, CoronaryIschemiaField,
                CardiomyopathyField, DementiaField, OthersField
            };
        }
    }
}